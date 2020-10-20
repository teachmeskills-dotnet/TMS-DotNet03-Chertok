using DebtTracker.BLL.Models;
using System.Threading.Tasks;

namespace DebtTracker.BLL.Interfaces
{
    public interface IProfileService
    {
        Task AddAsync(ProfileDto profile);

        Task Edit(ProfileDto profile);

        Task<ProfileDto> GetProfileByUserId(string userId);
    }
}
