﻿using DebtTracker.BLL.Interfaces;
using DebtTracker.BLL.Models;
using DebtTracker.DAL.Models;
using DebtTracker.Web.Models;
using DebtTracker.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DebtTracker.Web.Controllers
{
    /// <summary>
    /// Group controller.
    /// </summary>
    public class GroupController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IProfileService _profileService;
        private readonly IGroupService _groupService;
        private readonly ITransactionsService _transactionsService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="signInManager"></param>
        /// <param name="profileService"></param>
        /// <param name="transactionsService"></param>
        public GroupController(IProfileService profileService, IGroupService groupService, UserManager<User> userManager, ITransactionsService transactionsService)
        {
            _profileService = profileService ?? throw new ArgumentNullException(nameof(profileService));
            _groupService = groupService ?? throw new ArgumentNullException(nameof(groupService));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _transactionsService = transactionsService ?? throw new ArgumentNullException(nameof(transactionsService));
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
                    Description = groupDto.Description
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
                    //Currency
                    CurrencyType = 933
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
            var transactionsDto = await _transactionsService.GetTransactionsAsync(id);

            var scoreList = await _transactionsService.HowMatchPayAsync(id);
            var usersScore = await _transactionsService.ScoreAsync(id);
            var userScore = new List<UsersScore>();
            var resultScore = new List<UsersScore>();
            var transactionsUser = new List<UsersTransaction>();

            var groupUrl = Url.Action(
                        "AddUser",
                        "Group",
                        new { groupHash = groupDto.Guid },
                        protocol: HttpContext.Request.Scheme);

            foreach (var transaction in scoreList)
            {
                var user = await _profileService.GetProfileById(transaction.Creditor);
                userScore.Add(new UsersScore
                {
                    LastNameCreditor = user.LastName,
                    FirstNameCreditor = user.FirstName,
                    Summ = transaction.Summ
                });
            }

            foreach (var score in usersScore)
            {
                var creditor = await _profileService.GetProfileById(score.Creditor);
                var debitor = await _profileService.GetProfileById(score.Debitor);
                resultScore.Add(new UsersScore
                {
                    LastNameCreditor = creditor.LastName,
                    FirstNameCreditor = creditor.FirstName,
                    MiddleNameCreditor = creditor.MiddleName,
                    LastNameDebitor = debitor.LastName,
                    FirstNameDebitor = debitor.FirstName,
                    MiddleNameDebitor = debitor.MiddleName,
                    Summ = score.Summ
                });
            }

            foreach (var transaction in transactionsDto)
            {
                var transactionStatus = await _transactionsService.CheckUsersInTransactionAsync(transaction.Id);
                transactionsUser.Add(new UsersTransaction
                {
                    Id = transaction.Id,
                    Description = transaction.Description,
                    Comment = transaction.Comment,
                    Amount = transaction.Amount,
                    CurrencyType = transaction.CurrencyType,
                    CreationTime = transaction.CreationTime,
                    ProfileId = transaction.ProfileId,
                    GroupId = transaction.GroupId,
                    Guid = transaction.Guid,
                    Status = transactionStatus
                });
            }

            var groupViewModel = new GroupViewModel
            {
                Id = groupDto.Id,
                Title = groupDto.Title,
                Description = groupDto.Description,
                Profiles = profilesDto,
                Transactions = transactionsUser,
                GroupUrl = groupUrl,
                Scores = userScore,
                UsersScore = resultScore
            };

            return View(groupViewModel);
        }

        /// <summary>
        /// Edit group
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Return view model group</returns>
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var groupDto = await _groupService.GetGroupAsync(id);

            var groupActionViewModel = new GroupActionViewModel
            {
                Id = groupDto.Id,
                Title = groupDto.Title,
                Description = groupDto.Description,
            };

            return View(groupActionViewModel);
        }

        /// <summary>
        /// Edit group
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Result change group</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(GroupActionViewModel model)
        {
            if (ModelState.IsValid)
            {
                var groupDto = new GroupsDto
                {
                    Id = model.Id,
                    Title = model.Title,
                    Description = model.Description,
                };

                await _groupService.Edit(groupDto);

                return RedirectToAction("Detail", "Group", new { id = groupDto.Id });
            }
            return View(model);
        }

        /// <summary>
        /// Detail view
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Detail view about group</returns>
        [Authorize]
        public async Task<IActionResult> AddUser([FromQuery] Guid groupHash)
        {
            var username = User.Identity.Name;
            var user = await _userManager.FindByNameAsync(username);
            var profile = await _profileService.GetProfileByUserId(user.Id);
            var groupDto = await _groupService.GetGroupByGuidAsync(groupHash);

            var groupProfileDto = new GroupProfilesDto
            {
                ProfileId = profile.Id,
                GroupId = groupDto.Id,
            };

            var checkDouble = await _groupService.CheckDoubleAsyncProfileToGroup(groupProfileDto);

            if (checkDouble)
            {
                await _groupService.AddAsyncProfileToGroup(groupProfileDto);
                return RedirectToAction("Index", "Group");
            }
            return Content("Данный пользователь уже присутствует в группе");
        }
    }
}