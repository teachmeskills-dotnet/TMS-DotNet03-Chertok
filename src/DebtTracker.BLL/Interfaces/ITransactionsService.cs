using DebtTracker.BLL.Models;
using DebtTracker.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DebtTracker.BLL.Interfaces
{
    public interface ITransactionsService
    {
        /// <summary>
        /// Add transaction
        /// </summary>
        /// <param name="profile">Dto model</param>
        Task AddAsync(TransactionsDto transaction);

        /// <summary>
        /// Edit transaction
        /// </summary>
        /// <param name="profile">Dto model</param>
        Task EditAsync(TransactionsDto transaction);

        /// <summary>
        /// Get transaction
        /// </summary>
        /// <param name="id">Transaction id</param>
        Task<TransactionsDto> GetTransactionAsync(int id);

        /// <summary>
        /// Get transactions
        /// </summary>
        /// <param name="groupId">Get transactions by group Id</param>
        Task<IEnumerable<TransactionsDto>> GetTransactionsAsync(int groupId);

        /// <summary>
        /// Delete transaction
        /// </summary>
        /// <param name="id">Transaction id</param>
        Task DeleteTransactionAsync(TransactionsDto transaction);

        /// <summary>
        /// Add binding user to transaction
        /// </summary>
        /// <param name="id">transaction id</param>
        /// <param name="profileId">profile id</param>
        /// <returns>Rezult</returns>
        Task AddUserToTransactionAsync(TransactionProfilesDto transactionProfiles);

        /// <summary>
        /// Delete binding user in transaction
        /// </summary>
        /// <param name="id">Transaction id</param>
        /// <param name="profileId"> profile id</param>
        /// <returns></returns>
        Task DeleteUserToTransactionAsync(TransactionProfilesDto transactionProfiles);

        /// <summary>
        /// Get summ transactions by group
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns>List sum</returns>
        Task<IEnumerable<Score>> HowMatchPayAsync(int groupId);
    }
}
