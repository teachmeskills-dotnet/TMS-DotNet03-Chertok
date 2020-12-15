using DebtTracker.BLL.Interfaces;
using DebtTracker.BLL.Models;
using DebtTracker.Common.Interfaces;
using DebtTracker.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DebtTracker.BLL.Services
{
    /// <inheritdoc cref="IProfileService<T>"/>
    public class ProfileService : IProfileService
    {
        private readonly IRepository<Profile> _repository;

        public ProfileService(IRepository<Profile> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task AddAsync(ProfileDto profile)
        {
            if (profile is null)
            {
                throw new ArgumentNullException(nameof(profile));
            }
            var userProfile = new Profile
            {
                UserId = profile.UserId,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                MiddleName = profile.MiddleName,
            };

            await _repository.AddAsync(userProfile);
            await _repository.SaveChangesAsync();
        }

        public async Task Edit(ProfileDto profile)
        {
            if (profile is null)
            {
                throw new ArgumentNullException(nameof(profile));
            }

            var editProfile = await _repository.GetEntityAsync(q => q.Id.Equals(profile.Id));
            editProfile.FirstName = profile.FirstName;
            editProfile.MiddleName = profile.MiddleName;
            editProfile.LastName = profile.LastName;
            _repository.Update(editProfile);
            await _repository.SaveChangesAsync();
        }

        public async Task<ProfileDto> GetProfileByUserId(string userId)
        {
            if (userId is null)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            var profiles = await _repository.GetAll().AsNoTracking().ToListAsync();
            var profileDataModel = profiles.FirstOrDefault(c => c.UserId.Equals(userId));
            if (profileDataModel is null)
            {
                return new ProfileDto();
            }

            var profile = new ProfileDto
            {
                UserId = profileDataModel.UserId,
                FirstName = profileDataModel.FirstName,
                LastName = profileDataModel.LastName,
                MiddleName = profileDataModel.MiddleName,
            };
            profile.Id = profileDataModel.Id;
            return profile;
        }

        public async Task<ProfileDto> GetProfileById(int profileId)
        {
            var profileDataModel = await _repository.GetEntityAsync(transactionModel => transactionModel.Id == profileId);

            if (profileDataModel is null)
            {
                return new ProfileDto();
            }

            var profile = new ProfileDto
            {
                UserId = profileDataModel.UserId,
                FirstName = profileDataModel.FirstName,
                LastName = profileDataModel.LastName,
                MiddleName = profileDataModel.MiddleName,
            };
            profile.Id = profileDataModel.Id;
            return profile;
        }
    }
}