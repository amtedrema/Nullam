using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Nullam.Validators
{
	public class CorrectIdCodeAttribute : ValidationAttribute
	{
		public override bool IsValid(object? value)
		{
			if (value == null)
				return false;

			string serializedValue = JsonConvert.SerializeObject(value);
			return ValidateIdCode(serializedValue);
		}

		private static bool ValidateIdCode(string value)
		{
			var multiplier1 = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 1 };
			var multiplier2 = new int[] { 3, 4, 5, 6, 7, 8, 9, 1, 2, 3 };

			int[] codeArray = value.Select(x => int.Parse(x.ToString())).ToArray();

			if(codeArray.Length != 11)
				return false;

			var check = codeArray[10];
			var total = 0;

			// Do the first run.
			for (int i = 0; i < 10; i++)
			{
				total += codeArray[i] * multiplier1[i];
			}

			int mod = total % 11;

			// If modulus is ten we need a second run.
			if (mod == 10)
			{
				total = 0;
				for (int i = 0; i < 10; i++)
				{
					total += codeArray[i] * multiplier2[i];
				}
				mod = total % 11;

				// If modulus is still ten revert to 0.
				if (mod == 10)
				{
					mod = 0;
				}
			}

			return check == mod;
		}
	}
}
