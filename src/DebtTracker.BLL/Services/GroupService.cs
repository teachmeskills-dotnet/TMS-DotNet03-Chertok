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
        private readonly IRepository<Profile> _repositoryProfile;

        public GroupService(IRepository<Groups> repository, IRepository<GroupProfiles> repositoryGroupProfiles, IRepository<Profile> repositoryProfil)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _repositoryGroupProfiles = repositoryGroupProfiles ?? throw new ArgumentNullException(nameof(repositoryGroupProfiles));
            _repositoryProfile = repositoryProfil ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task AddAsync(GroupsDto group)
        {
            if (group is null)
            {
                throw new ArgumentNullException(nameof(group));
            }
            var groupGuid = Guid.NewGuid();
            var groupModel = new Groups
            {
                ProfileId = group.ProfileId,
                Title = group.Title,
                Description = group.Description,
                Guid = groupGuid,
            };

            await _repository.AddAsync(groupModel);
            await _repository.SaveChangesAsync();

            var getGroup = await _repository.GetEntityWithoutTrackingAsync(group => group.Title == groupModel.Title && group.Guid == groupModel.Guid);

            var groupProfileModel = new GroupProfiles
            {
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
            editGroup.Title = group.Title;
            editGroup.Description = group.Description;
            _repository.Update(editGroup);
            await _repository.SaveChangesAsync();
        }

        public async Task<GroupsDto> GetGroupAsync(int id)
        {
            var group = await _repository.GetEntityWithoutTrackingAsync(group => group.Id == id);
            if (group is null)
            {
                return new GroupsDto();
            }

            var groupDto = new GroupsDto
            {
                Id = group.Id,
                Title = group.Title,
                Description = group.Description,
                Guid = group.Guid
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

        public async Task<IEnumerable<ProfileDto>> GetAsyncProfilesByGroup(int groupId)
        {
            var profileDtos = new List<ProfileDto>();
            var groupsProfileDtos = new List<GroupProfilesDto>();
            var GroupProfilesDtos = await _repositoryGroupProfiles
                .GetAll()
                .AsNoTracking()
                .Where(groupe => groupe.GroupId == groupId)
                .ToListAsync();

            if (!GroupProfilesDtos.Any())
            {
                return profileDtos;
            }

            foreach (var profileDto in GroupProfilesDtos)
            {
                groupsProfileDtos.Add(new GroupProfilesDto
                {
                    Id = profileDto.Id,
                    ProfileId = profileDto.ProfileId,
                    GroupId = profileDto.GroupId
                });
            }

            foreach (var profile in groupsProfileDtos)
            {
                var ProfileDtos = await _repositoryProfile
                .GetAll()
                .AsNoTracking()
                .Where(profil => profil.Id == profile.ProfileId)
                .ToListAsync();

                if (!ProfileDtos.Any())
                {
                    return profileDtos;
                }
                foreach (var profileDto in ProfileDtos)
                {
                    profileDtos.Add(new ProfileDto
                    {
                        Id = profileDto.Id,
                        FirstName = profileDto.FirstName,
                        LastName = profileDto.LastName,
                        MiddleName = profileDto.MiddleName,
                        UserId = profileDto.UserId,
                    });
                };
            }

            return profileDtos;
        }

        public async Task AddAsyncProfileToGroup(GroupProfilesDto groupProfiles)
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

        public async Task<GroupsDto> GetGroupByGuidAsync(Guid guid)
        {
            var group = await _repository.GetEntityWithoutTrackingAsync(group => group.Guid == guid);
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

        public async Task<bool> CheckDoubleAsyncProfileToGroup(GroupProfilesDto groupProfiles)
        {
            if (groupProfiles is null)
            {
                throw new ArgumentNullException(nameof(groupProfiles));
            }

            var getUserGroup = await _repositoryGroupProfiles.GetEntityWithoutTrackingAsync(groupProfile => groupProfile.ProfileId == groupProfiles.ProfileId && groupProfile.GroupId == groupProfiles.GroupId);
            if (getUserGroup != null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}