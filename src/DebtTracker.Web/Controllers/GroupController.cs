using DebtTracker.BLL.Interfaces;
using DebtTracker.BLL.Models;
using DebtTracker.DAL.Models;
using DebtTracker.Web.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DebtTracker.Web.Controllers
{
    public class GroupController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IProfileService _profileService;
        private readonly IGroupService _groupService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="signInManager"></param>
        /// <param name="profileService"></param>
        public GroupController(IProfileService profileService, IGroupService groupService, UserManager<User> userManager)
        {
            _profileService = profileService ?? throw new ArgumentNullException(nameof(profileService));
            _groupService = groupService ?? throw new ArgumentNullException(nameof(groupService));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        /// <summary>
        /// Get user groups
        /// </summary>
        /// <returns>View List groups</returns>
        public async Task<IActionResult> Index()
        {
            var username = User.Identity.Name;
            var user = await _userManager.FindByNameAsync(username);
            var profile = await _profileService.GetProfileByUserId(user.Id);
            var groupDtos = await _groupService.GetGroups(profile.Id);

            var groupsViewsModels = new List<GroupViewModel>();
            
            foreach (var groupDto in groupDtos)
            {
                groupsViewsModels.Add(new GroupViewModel
                {
                    Id = groupDto.Id,
                    Title = groupDto.Title,
                    Description = groupDto.Description,
                });
            }
            return View(groupsViewsModels);
        }

        /// <summary>
        /// Create group
        /// </summary>
        /// <returns>View create group</returns>
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Create group 
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Rezult create</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GroupActionViewModel model)
        {
            if (ModelState.IsValid)
            {
                var username = User.Identity.Name;
                var user = await _userManager.FindByNameAsync(username);
                var profile = await _profileService.GetProfileByUserId(user.Id);

                var groupDto = new GroupsDto
                {
                    ProfileId = profile.Id,
                    Title = model.Title,
                    Description = model.Description,
                    
                };

                await _groupService.AddAsync(groupDto);

                return RedirectToAction("Index", "Group");
            }

            return View(model);
        }

        /// <summary>
        /// Detail view
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Detail view about group</returns>
        public async Task<IActionResult> Detail(int id)
        {
            var groupDto = await _groupService.GetGroupAsync(id);
            var profilesDto = await _groupService.GetAsyncProfilesByGroup(id);

            var groupViewModel = new GroupViewModel
            {
                Id = groupDto.Id,
                Title = groupDto.Title,
                Description = groupDto.Description,
                Profiles = profilesDto,
            };

            return View(groupViewModel);
        }
    }
}
