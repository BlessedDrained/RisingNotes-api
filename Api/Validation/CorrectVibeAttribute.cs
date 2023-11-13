// using System.ComponentModel.DataAnnotations;
//
// namespace Api.Validation;
//
// /// <summary>
// /// Проверка, что введенные виды настроений корректны
// /// </summary>
// public class CorrectVibeAttribute : ValidationAttribute
// {
//     /// <inheritdoc />
//     protected override ValidationResult IsValid(object? value, ValidationContext context)
//     {
//         if (value == null)
//         {
//             return ValidationResult.Success;
//         }
//
//         if (value is not List<string> vibeList)
//         {
//             return ValidationResult.Success;
//         }
//
//         var repository = context.GetRequiredService<IVibeRepository>();
//
//         if (vibeList
//             .Select(authorName => repository.ExistsAsync(x => x.Name == authorName).Result)
//             .All(exists => exists))
//         {
//             return ValidationResult.Success;
//         }
//
//         return new ValidationResult("Some of provided vibes don't are invalid");
//     }
// }
