using Microsoft.Extensions.Options;
namespace OneDriveFileBackuper.Options.Validators
{
    /// <summary>
    /// Validator entra id option
    /// </summary>
    public class EntraIdOptionValidator : IValidateOptions<EntraIdOption>
    {
        /// <summary>
        /// Validate EntraId option
        /// </summary>
        /// <param name="name">Name</param>
        /// <param name="options">EntraId option</param>
        /// <returns>Returns success if option is valid</returns>
        public ValidateOptionsResult Validate(string? name, EntraIdOption options)
        {
            if (string.IsNullOrEmpty(options.ClientId))
                return ValidateOptionsResult.Fail("ClientId is required.");

            return ValidateOptionsResult.Success;
        }
    }
}
