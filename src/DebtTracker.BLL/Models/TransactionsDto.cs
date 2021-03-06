﻿using System;

namespace DebtTracker.BLL.Models
{
    /// <summary>
    /// Transactions transport models
    /// </summary>
    public class TransactionsDto
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
    }
}