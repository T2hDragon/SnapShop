using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.Extensions.Localization;

namespace SnapShop.Form.Validation
{
    public class Password : ValidationAttribute
    {
        public int MinimumLowerCases { get; set; } = 1;
        public int MinimumUpperCases { get; set; } = 1;
        public int MinimumNumbers { get; set; } = 2;
        public int MinimumSpecialCharacters { get; set; } = 1;

        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null && value is string str)
            {
                var lowerCasesCount = str.Count(char.IsLower);
                var upperCasesCount = str.Count(char.IsUpper);
                var numbersCount = str.Count(char.IsDigit);
                var specialCharactersCount = str.Count(char.IsSymbol) + str.Count(char.IsPunctuation);

                var errorMessages = new List<string>();

                if (lowerCasesCount < MinimumLowerCases)
                    errorMessages.Add($"At least {MinimumLowerCases} lowercase character(s) required.");

                if (upperCasesCount < MinimumUpperCases)
                    errorMessages.Add($"At least {MinimumUpperCases} uppercase character(s) required.");

                if (numbersCount < MinimumNumbers)
                    errorMessages.Add($"At least {MinimumNumbers} number(s) required.");

                if (specialCharactersCount < MinimumSpecialCharacters)
                    errorMessages.Add($"At least {MinimumSpecialCharacters} special character(s) required.");

                if (errorMessages.Count > 0)
                {
                    var errorMessageBuilder = new StringBuilder();
                    errorMessageBuilder.AppendLine($"The field {validationContext.DisplayName} must meet the following requirements:");

                    foreach (var errorMessage in errorMessages)
                    {
                        errorMessageBuilder.AppendLine("- " + errorMessage);
                    }

                    return new ValidationResult(errorMessageBuilder.ToString());
                }
            }

            return ValidationResult.Success!;
        }
    }
}
