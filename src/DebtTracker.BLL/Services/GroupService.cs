using DebtTracker.BLL.Interfaces;
using DebtTracker.BLL.Models;
using DebtTracker.Common.Interfaces;
using DebtTracker.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DebtTracker.BLL.Services
{
    /// <inheritdoc cref="IGroupService<T>"/>
    public class GroupService : IGroupService
    {
        private readonly IRepository<Groups> _repository;
        private readonly IRepository<GroupProfiles> _repositoryGroupProfiles;

        public GroupService(IRepository<Groups> repository,IRepository<GroupProfiles> repositoryGroupProfiles)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _repositoryGroupProfiles = repositoryGroupProfiles ?? throw new ArgumentNullException(nameof(repositoryGroupProfiles));
        }

        public async Task AddAsync(GroupsDto group)
        {
            if (group is null)
            {
                throw new ArgumentNullException(nameof(group));
            }
            var groupModel = new Groups
            {
                ProfileId = group.ProfileId,
                Title = group.Title,
                Description = group.Description,
            };
           
            await _repository.AddAsync(groupModel);
            await _repository.SaveChangesAsync();

            var getGroup = await _repository.GetEntityWithoutTrackingAsync(group => group.Title == groupModel.Title && group.ProfileId == groupModel.ProfileId && group.Description == groupModel.Description);

            var groupProfileModel = new GroupProfiles {
               ProfileId = getGroup.ProfileId,
               GroupId = getGroup.Id
            };

            await _repositoryGroupProfiles.AddAsync(groupProfileModel);
            await _repository.SaveChangesAsync();
        }

        public async Task Edit(GroupsDto group)
        {
            if (group is null)
            {
                throw new ArgumentNullException(nameof(group));
            };
            var editGroup = await _repository.GetEntityAsync(q => q.Id.Equals(group.Id));
            editGroup.ProfileId = group.ProfileId;
            editGroup.Title = group.Title;
            editGroup.Description = group.Description;
            _repository.Update(editGroup);
            await _repository.SaveChangesAsync();

        }

        public async Task<GroupsDto> GetGroupAsync(int id, int profileId)
        {
            var group = await _repository.GetEntityWithoutTrackingAsync(group => group.Id == id && group.ProfileId == profileId);
            if (group is null)
            {
                return new GroupsDto();
            }

            var groupDto = new GroupsDto
            {
                Id = group.Id,
                Title = group.Title,
                Description = group.Description
            };

            return groupDto;
        }

        public async Task<IEnumerable<GroupsDto>> GetGroups(int profileId)
        {
            var groupDtos = new List<GroupsDto>();
            var groupsProfileDtos = new List<GroupProfilesDto>();
            var GroupProfilesDtos = await _repositoryGroupProfiles
                .GetAll()
                .AsNoTracking()
                .Where(groupe => groupe.ProfileId == profileId)
                .ToListAsync();
            if (!GroupProfilesDtos.Any())
            {
                return groupDtos;
            }

            foreach (var groupDto in GroupProfilesDtos)
            {
                groupsProfileDtos.Add(new GroupProfilesDto
                {
                    Id = groupDto.Id,
                    ProfileId = groupDto.ProfileId,
                    GroupId = groupDto.GroupId
                });
            }

            foreach (var group in groupsProfileDtos)
            {
                var GroupDtos = await _repository
                .GetAll()
                .AsNoTracking()
                .Where(groupe => groupe.Id == group.GroupId)
                .ToListAsync();

                if (!GroupDtos.Any())
                {
                    return groupDtos;
                }
                foreach (var groupdto in GroupDtos)
                {
                    groupDtos.Add(new GroupsDto
                    {
                        Id = groupdto.Id,
                        Title = groupdto.Title,
                        Description = groupdto.Description
                    });
                };

            }

            return groupDtos;
        }

        public async Task AddAsyncGroupProfile(GroupProfilesDto groupProfiles)
        {
            if (groupProfiles is null)
            {
                throw new ArgumentNullException(nameof(groupProfiles));
            }
            var groupProfilesModel = new GroupProfiles
            {
                ProfileId = groupProfiles.ProfileId,
                GroupId = groupProfiles.GroupId
            };

            await _repositoryGroupProfiles.AddAsync(groupProfilesModel);
            await _repositoryGroupProfiles.SaveChangesAsync();
        }


        //public async Task<GroupsDto> GetGroupId(GroupsDto groupsDto)
        //{
        //    if (groupsDto is null)
        //    {
        //        throw new ArgumentNullException(nameof(groupsDto));

        //    }
        //    var group = await _repository.GetEntityWithoutTrackingAsync(group => groupsDto.Id == group.Id && groupsDto.Title == group.Title);
        //    if (group is null)
        //    {
        //        return new GroupsDto();
        //    }

        //    var groupDto = new GroupsDto
        //    {
        //        Id = group.Id,
        //        Title = group.Title,
        //        Description = group.Description
        //    };

        //    return groupDto;
        //}
    }
}
