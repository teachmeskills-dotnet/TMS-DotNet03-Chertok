using DebtTracker.BLL.Models;
using DebtTracker.Web.Models;
using System.Collections.Generic;

namespace DebtTracker.Web.ViewModels
{
    public class GroupViewModel
    {
        /// <summary>
        /// Identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Users
        /// </summary>
        public IEnumerable<ProfileDto> Profiles { get; set; }

        /// <summary>
        /// Url from add to group
        /// </summary>
        public string GroupUrl { get; set; }

        /// <summary>
        /// Users
        /// </summary>
        public IEnumerable<UsersTransaction> Transactions { get; set; }

        /// <summary>
        /// Scores
        /// </summary>
        public IEnumerable<UsersScore> Scores { get; set; }

        /// <summary>
        /// Scores
        /// </summary>
        public IEnumerable<UsersScore> UsersScore { get; set; }
    }
}