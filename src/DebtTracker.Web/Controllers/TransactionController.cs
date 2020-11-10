using DebtTracker.BLL.Interfaces;
using DebtTracker.BLL.Models;
using DebtTracker.DAL.Models;
using DebtTracker.Web.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DebtTracker.Web.Controllers
{
    public class TransactionController : Controller
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
        public TransactionController(IProfileService profileService, IGroupService groupService, UserManager<User> userManager, ITransactionsService transactionsService)
        {
            _profileService = profileService ?? throw new ArgumentNullException(nameof(profileService));
            _groupService = groupService ?? throw new ArgumentNullException(nameof(groupService));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _transactionsService = transactionsService ?? throw new ArgumentNullException(nameof(transactionsService));
        }

        /// <summary>
        /// Index view
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Detail view about transaction</returns>
        public async Task<IActionResult> Index(int id)
        {
            var groupDto = await _transactionsService.GetTransactionAsync(id);

            var transactionViewModel = new TransactionViewModel
            {
                Id = groupDto.Id,
                Description = groupDto.Description,
                Comment = groupDto.Comment,
                Amount = groupDto.Amount,
                CurrencyType = groupDto.CurrencyType,
                CreationTime = groupDto.CreationTime,
                ProfileId = groupDto.ProfileId,
                GroupId = groupDto.GroupId,
            };

            return View(transactionViewModel);
        }

        /// <summary>
        /// Create transaction
        /// </summary>
        /// <returns>View create transaction</returns>
        [HttpGet]
        public IActionResult Create(int id)
        {
            var transactionActionViewModel = new TransactionActionViewModel
            {
                GroupId = id
            };
            return View(transactionActionViewModel);
        }

        /// <summary>
        /// Create transaction 
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Rezult create</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TransactionActionViewModel model)
        {
            if (ModelState.IsValid)
            {
                var username = User.Identity.Name;
                var user = await _userManager.FindByNameAsync(username);
                var profile = await _profileService.GetProfileByUserId(user.Id);

                var transactionDto = new TransactionsDto
                {
                    Description = model.Description,
                    Comment = model.Comment,
                    Amount = model.Amount,
                    //Currency
                    CurrencyType = 933,
                    CreationTime = DateTime.Now,
                    ProfileId = profile.Id,
                    GroupId = model.GroupId,

                };

                await _transactionsService.AddAsync(transactionDto);

                return RedirectToAction("Index", "Group");
            }

            return View(model);
        }

        /// <summary>
        /// Edit transaction
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Return view model transaction</returns>
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var tarnsactionDto = await _transactionsService.GetTransactionAsync(id);

            var transactionActionViewModel = new TransactionActionViewModel
            {
                Id = tarnsactionDto.Id,
                Description = tarnsactionDto.Description,
                Comment = tarnsactionDto.Comment,
                Amount = tarnsactionDto.Amount
            };

            return View(transactionActionViewModel);
        }

        /// <summary>
        /// Edit transaction
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Result change transaction</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TransactionActionViewModel model)
        {
            if (ModelState.IsValid)
            {
                var transactionDto = new TransactionsDto
                {   
                    Id = model.Id,
                    Description = model.Description,
                    Comment = model.Comment,
                    Amount = model.Amount
                };

                await _transactionsService.EditAsync(transactionDto);

                return RedirectToAction("Index", "Transaction", new { id = transactionDto.Id });
            }
            return View(model);
        }
    }
}
