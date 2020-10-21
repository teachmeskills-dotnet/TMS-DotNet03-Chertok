﻿namespace DebtTracker.BLL.Models
{
    /// <summary>
    /// Transport model from group
    /// </summary>
    public class GroupsDto
    {
        /// <inheritdoc/>
        public int Id { get; set; }

        /// <summary>
        /// Title Group
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }

        /// <inheritdoc/>
        public int ProfileId { get; set; }
    }
}