﻿namespace BinaryPlate.Application.Common.Extensions;

public static class ValidationExtensions
{
    #region Public Methods

    public static Dictionary<string, string> ToApplicationResult(this IEnumerable<ValidationFailure> validationFailures)
    {
        var rnd = new Random();
        return validationFailures.ToDictionary(validationFailure => validationFailure.PropertyName == string.Empty ? rnd.Next().ToString() : validationFailure.PropertyName, validationFailure => validationFailure.ErrorMessage);
    }

    public static string ToSerializedResult(this IEnumerable<ValidationFailure> validationFailures)
    {
        var errorBuilder = new StringBuilder();

        foreach (var validationFailure in validationFailures)
        {
            errorBuilder.AppendLine(validationFailure.ErrorMessage);

            errorBuilder.AppendLine("||");
        }
        return errorBuilder.ToString();
    }

    public static string ToSerializedResult(this IEnumerable<ValidationResult> dbEntityValidationResults)
    {
        var errorBuilder = new StringBuilder();

        foreach (var dbEntityValidationResult in dbEntityValidationResults)
        {
            foreach (var error in dbEntityValidationResult.Errors)
            {
                errorBuilder.AppendLine($"{error.PropertyName}: {error.ErrorMessage}");

                errorBuilder.AppendLine("||");
            }
        }
        return errorBuilder.ToString();
    }

    #endregion Public Methods
}