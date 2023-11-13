// using System.ComponentModel.DataAnnotations;
//
// #pragma warning disable CS8603
//
// namespace Api.Validation;
//
// /// <summary>
// /// Проверка, что переданные похожие авторы корректны
// /// </summary>
// public class CorrectSimilarAuthorAttribute : ValidationAttribute
// {
//     /// <inheritdoc />
//     protected override ValidationResult IsValid(object? value, ValidationContext context)
//     {
//         if (value == null)
//         {
//             return ValidationResult.Success;
//         }
//
//         if (value is not List<string> authorNameList)
//         {
//             return ValidationResult.Success;
//         }
//
//         var repository = context.GetRequiredService<ISimilarAuthorRepository>();
//
//         if (authorNameList
//             .Select(authorName => repository.ExistsAsync(x => x.Name == authorName).Result)
//             .All(exists => exists))
//         {
//             return ValidationResult.Success;
//         }
//
//         return new ValidationResult("Some of provided authors don't are invalid");
//     }
// }