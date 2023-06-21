using System.ComponentModel.DataAnnotations;

namespace Nullam.Validators
{
    /// <summary>
    /// Validates that a date is in the future
    /// </summary>
    public class FutureDateAttribute : ValidationAttribute
	{
        /// <summary>
        /// Determines whether the specified value is a future date
        /// </summary>
        /// <param name="value">The value to validate</param>
        /// <returns>True if the value is a future date; otherwise, false</returns>
        public override bool IsValid(object? value)
		{
			return value != null && (DateTime)value > DateTime.Now;
		}
	}
}
