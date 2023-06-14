using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Nullam.Validators
{
	public class CorrectIdCodeAttribute : ValidationAttribute
	{
		public override bool IsValid(object? value)
		{
			if(value != null)
			{
				string s = JsonConvert.SerializeObject(value);
				return ValidateIdCode(s);
			}
			else
				return false;
		}

		private static bool ValidateIdCode(string code)
		{
			var multiplier1 = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 1 };
			var multiplier2 = new int[] { 3, 4, 5, 6, 7, 8, 9, 1, 2, 3 };

			var codeArray = code.ToCharArray();

			var control = Convert.ToInt32(char.GetNumericValue(codeArray[10]));
			var total = 0;

			/* Do the first run. */
			for (int i = 0; i < 10; i++)
			{
				total += Convert.ToInt32(char.GetNumericValue(codeArray[i])) * multiplier1[i];
			}

			int mod = total % 11;

			/* If modulus is ten we need a second run. */
			total = 0;
			if (10 == mod)
			{
				for (int i = 0; i < 10; i++)
				{
					total += code[i] * multiplier2[i];
				}
				mod = total % 11;

				/* If modulus is still ten revert to 0. */
				if (10 == mod)
				{
					mod = 0;
				}
			}

			return control == mod;
		}
	}
}
