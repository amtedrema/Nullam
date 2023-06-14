using System.ComponentModel.DataAnnotations;

namespace Nullam.Validators
{
	public class FutureDateAttribute : ValidationAttribute
	{
		public override bool IsValid(object? value)
		{
			return value != null && (DateTime)value > DateTime.Now;
		}
	}
}
