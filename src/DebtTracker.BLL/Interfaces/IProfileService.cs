using DebtTracker.BLL.Models;
using System.Threading.Tasks;

namespace DebtTracker.BLL.Interfaces
{
    /// <summary>
    /// Service from controle user profile
    /// </summary>
    public interface IProfileService
    {
        /// <summary>
        /// Add profile from user identity
        /// </summary>
        /// <param name="profile">Dto model</param>
        Task AddAsync(ProfileDto profile);

        /// <summary>
        /// Edit profile
        /// </summary>
        /// <param name="profile">Dto model</param>
        Task Edit(ProfileDto profile);

        /// <summary>
        /// Get profile from base
        /// </summary>
        /// <param name="userId">Search profil by UserId key</param>
        Task<ProfileDto> GetProfileByUserId(string userId);
    }
}
