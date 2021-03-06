﻿using System;

namespace DebtTracker.Web.Models
{
    /// <summary>
    /// User transaction model.
    /// </summary>
    public class UsersTransaction
    {
        /// <inheritdoc/>
        public int Id { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Comment
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Amount
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// CurrencyType
        /// </summary>
        public int CurrencyType { get; set; }

        /// <summary>
        /// CreationTime
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <inheritdoc/>
        public int ProfileId { get; set; }

        /// <inheritdoc/>
        public int GroupId { get; set; }

        /// <summary>
        /// Transaction guid
        /// </summary>
        public Guid Guid { get; set; }

        /// <summary>
        /// Transaction status
        /// </summary>
        public bool Status { get; set; }
    }
}