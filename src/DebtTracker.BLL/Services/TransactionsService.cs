using DebtTracker.BLL.Interfaces;
using DebtTracker.BLL.Models;
using DebtTracker.Common.Constants;
using DebtTracker.Common.Interfaces;
using DebtTracker.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DebtTracker.BLL.Services
{
    /// <inheritdoc cref="ITransactionsService<T>"/>
    public class TransactionsService : ITransactionsService
    {
        private readonly IRepository<Transactions> _repository;
        private readonly IRepository<TransactionProfiles> _repositoryTransactionProfiles;
        public TransactionsService(IRepository<Transactions> repository, IRepository<TransactionProfiles> repositoryTransactionProfiles)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _repositoryTransactionProfiles = repositoryTransactionProfiles ?? throw new ArgumentNullException(nameof(repositoryTransactionProfiles));
        }

        public async Task AddAsync(TransactionsDto transaction)
        {
            if (transaction is null)
            {
                throw new ArgumentNullException(nameof(transaction));
            }

            var transactionGuid = Guid.NewGuid();

            var transactionModel = new Transactions
            {
                Description = transaction.Description,
                Comment = transaction.Comment,
                Amount = transaction.Amount,
                CurrencyType = Convert.ToInt32(transaction.CurrencyType),
                CreationTime = transaction.CreationTime,
                ProfileId = transaction.ProfileId,
                GroupId = transaction.GroupId,
                Guid = transactionGuid
            };

            await _repository.AddAsync(transactionModel);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteTransactionAsync(TransactionsDto transactionDto)
        {
            var transactionsProfile = await _repositoryTransactionProfiles
                .GetAll()
                .AsNoTracking()
                .Where(transaction => transaction.TransactionId == transactionDto.Id)
                .ToListAsync();

            if (transactionsProfile.Any())
            {
                foreach (var transactionProfile in transactionsProfile)
                {
                    _repositoryTransactionProfiles.Delete(transactionProfile);
                    await _repositoryTransactionProfiles.SaveChangesAsync();
                }
            }

            var transactionModel = await _repository.GetEntityAsync(transactionModel => transactionModel.Id == transactionDto.Id);
            if (transactionModel is null)
            {
                return;
            }

            _repository.Delete(transactionModel);
            await _repositoryTransactionProfiles.SaveChangesAsync();
        }

        public async Task EditAsync(TransactionsDto transaction)
        {
            var transactionModel = await _repository.GetEntityAsync(transactionModel => transactionModel.Id == transaction.Id);
            if (transaction is null)
            {
                return;
            }
            transactionModel.Description = transaction.Description;
            transactionModel.Comment = transaction.Comment;
            transactionModel.Amount = transaction.Amount;
            _repository.Update(transactionModel);
            await _repositoryTransactionProfiles.SaveChangesAsync();
        }

        public async Task<TransactionsDto> GetTransactionAsync(int id)
        {
            var transactionModel = await _repository.GetEntityAsync(transactionModel => transactionModel.Id == id);
            if (transactionModel is null)
            {
                return new TransactionsDto();
            }

            var transactionDto = new TransactionsDto
            {
                Id = transactionModel.Id,
                Description = transactionModel.Description,
                Comment = transactionModel.Comment,
                Amount = transactionModel.Amount,
                CurrencyType = transactionModel.CurrencyType,
                CreationTime = transactionModel.CreationTime,
                ProfileId = transactionModel.ProfileId,
                GroupId = transactionModel.GroupId,
                Guid = transactionModel.Guid
            };

            return transactionDto;
        }

        public async Task<IEnumerable<TransactionsDto>> GetTransactionsAsync(int groupId)
        {
            var transactionsDtos = new List<TransactionsDto>();
            var transactions = await _repository
                .GetAll()
                .AsNoTracking()
                .Where(transaction => transaction.GroupId == groupId)
                .ToListAsync();

            if (!transactions.Any())
            {
                return transactionsDtos;
            }

            foreach (var transaction in transactions)
            {
                transactionsDtos.Add(new TransactionsDto
                {
                    Id = transaction.Id,
                    Description = transaction.Description,
                    Comment = transaction.Comment,
                    Amount = transaction.Amount,
                    CurrencyType = transaction.CurrencyType,
                    CreationTime = transaction.CreationTime,
                    ProfileId = transaction.ProfileId,
                    GroupId = transaction.GroupId,
                    Guid = transaction.Guid
                });
            }

            return transactionsDtos;
        }

        public async Task AddUserToTransactionAsync(TransactionProfilesDto transactionProfiles)
        {
            var transaction = await _repository.GetEntityAsync(transaction => transaction.Id == transactionProfiles.TransactionId);
            if (transaction is null)
            {
                return;
            }

            var transactionProfileModel = new TransactionProfiles
            {
                TransactionId = transaction.Id,
                ProfileId = transactionProfiles.ProfileId

            };

            var transactionProfile = await _repositoryTransactionProfiles.GetEntityAsync(
                transactionprofile => transactionprofile.ProfileId == transactionProfileModel.ProfileId
                && transactionprofile.TransactionId == transactionProfileModel.TransactionId);

            if (transactionProfile != null)
            {
                return;
            }

            await _repositoryTransactionProfiles.AddAsync(transactionProfileModel);
            await _repositoryTransactionProfiles.SaveChangesAsync();
        }

        public async Task DeleteUserToTransactionAsync(TransactionProfilesDto transactionProfiles)
        {
            var transactionProfileModel = await _repositoryTransactionProfiles.GetEntityAsync(transaction => transaction.TransactionId == transactionProfiles.TransactionId && transaction.ProfileId == transactionProfiles.ProfileId);
            if (transactionProfileModel is null)
            {
                return;
            }

            _repositoryTransactionProfiles.Delete(transactionProfileModel);
            await _repositoryTransactionProfiles.SaveChangesAsync();
        }

        public async Task<IEnumerable<Score>> HowMatchPayAsync(int groupId)
        {
            var transactionsSumms = new List<Score>();
            var transactions = await _repository
                .GetAll()
                .AsNoTracking()
                .Where(transaction => transaction.GroupId == groupId)
                .ToListAsync();

            if (!transactions.Any())
            {
                return transactionsSumms;
            }

            foreach (var transaction in transactions)
            {
                transactionsSumms.Add(new Score
                {
                    Creditor = transaction.ProfileId,
                    Summ = transaction.Amount
                });
            }

            var usersSumms = transactionsSumms
                .GroupBy(summ => summ.Creditor, summ => summ.Summ)
                .Select(summ => new Score { Creditor = summ.Key, Summ = summ.Sum() });

            return usersSumms;
        }

        public async Task<IEnumerable<Score>> ScoreAsync(int groupId)
        {
            var transactionsAmounth = new List<Score>();
            var transactions = await _repository
                .GetAll()
                .AsNoTracking()
                .Where(transaction => transaction.GroupId == groupId)
                .ToListAsync();

            if (!transactions.Any())
            {
                return transactionsAmounth;
            }

            foreach (var transaction in transactions)
            {
                var transactionUser = await _repositoryTransactionProfiles
                    .GetAll()
                    .AsNoTracking()
                    .Where(transactionmodel => transactionmodel.TransactionId == transaction.Id)
                    .ToListAsync();

                if (transactionUser.Any())
                {
                    var amounth = transaction.Amount / transactionUser.Count;
                    foreach (var microtransaction in transactionUser)
                    {
                        if (microtransaction.ProfileId != transaction.ProfileId)
                        {
                            transactionsAmounth.Add(new Score
                            {
                                Creditor = transaction.ProfileId,
                                Debitor = microtransaction.ProfileId,
                                Summ = amounth
                            });
                        }
                    }
                }              
            }

            var usersScore = transactionsAmounth
                .GroupBy(summ => new { summ.Creditor, summ.Debitor }, summ => summ.Summ)
                .Select(summ => new Score { Creditor = summ.Key.Creditor, Debitor = summ.Key.Debitor, Summ = summ.Sum() });

            var scoreResult = new List<Score>();
            foreach (var scoretransaction in usersScore)
            {
                var userScore = usersScore.FirstOrDefault(p => p.Creditor == scoretransaction.Debitor && p.Debitor == scoretransaction.Creditor);

                if (userScore != null)
                {
                    if (scoretransaction.Summ != userScore.Summ)
                    {
                        if (scoretransaction.Summ > userScore.Summ)
                        {
                            var score = scoretransaction.Summ - userScore.Summ;
                            scoreResult.Add(new Score
                            {
                                Creditor = scoretransaction.Creditor,
                                Debitor = scoretransaction.Debitor,
                                Summ = Math.Round(score, ScoreConstants.numbersAfterСomma)
                            });
                        }
                    }
                }
                else {
                    scoreResult.Add(new Score
                    {
                        Creditor = scoretransaction.Creditor,
                        Debitor = scoretransaction.Debitor,
                        Summ = Math.Round(scoretransaction.Summ, ScoreConstants.numbersAfterСomma)
                    });
                }
            }

            return scoreResult;
        }
    }
}
