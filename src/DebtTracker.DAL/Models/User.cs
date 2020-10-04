using Microsoft.AspNetCore.Identity;

namespace DebtTracker.DAL.Models
{
    /// <summary>
    /// User
    /// </summary>
    public class User : IdentityUser
    {
        /// <summary>
        /// Navigation to Profile
        /// </summary>
        public Profile Profile { get; set; }
    }
}
