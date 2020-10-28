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
        Task<GroupsDto> GetGroupAsync(int id);

        /// <summary>
        /// Get groups from base
        /// </summary>
        /// <param name="profileId">Get groups by profileId key</param>
        Task<IEnumerable<GroupsDto>> GetGroups(int profileId);

        /// <summary>
        /// Get users in group
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns>List profiles</returns>
        Task<IEnumerable<ProfileDto>> GetAsyncProfilesByGroup(int groupId);

    }
}
