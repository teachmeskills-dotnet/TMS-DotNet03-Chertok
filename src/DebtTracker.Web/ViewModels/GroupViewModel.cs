using DebtTracker.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

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

        public string GroupUrl { get; set; }
    }
}
