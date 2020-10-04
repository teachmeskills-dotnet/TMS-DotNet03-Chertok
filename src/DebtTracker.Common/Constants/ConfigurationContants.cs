using System;
using System.Collections.Generic;
using System.Text;

namespace DebtTracker.Common.Constants
{
    /// <summary>
    /// Configuration contants.
    /// </summary>
    public static class ConfigurationContants
    {
        /// <summary>
        /// Custom date format.
        /// </summary>
        public const string SqlDateFormat = "date";

        /// <summary>
        /// Min lenght for string field.
        /// </summary>
        public const int SqlMaxLengthShort = 63;

        /// <summary>
        /// Standart lenght for string field.
        /// </summary>
        public const int SqlMaxLengthMedium = 127;

        /// <summary>
        /// Max lenght for string field.
        /// </summary>
        public const int SqlMaxLengthLong = 255;
    }
}
