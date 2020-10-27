using DebtTracker.BLL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DebtTracker.BLL.Interfaces
{
    public interface IGroupService
    {
        /// <summary>
        /// Add group
        /// </summary>
        /// <param name="profile">Dto model</param>
        Task AddAsync(GroupsDto group);

        /// <summary>
        /// Edit group
        /// </summary>
        /// <param name="profile">Dto model</param>
        Task Edit(GroupsDto group);

        /// <summary>
        /// Get group
        /// </summary>
        /// <param name="id"></param>
        /// <param name="profileId"></param>
        Task<GroupsDto> GetGroupAsync(int id, int profileId);
        /// <summary>
        /// Get groups from base
        /// </summary>
        /// <param name="userId">Search profil by UserId key</param>
        Task<IEnumerable<GroupsDto>> GetGroups(int profileId);

        Task AddAsyncGroupProfile(GroupProfilesDto groupProfiles);
    }
}
