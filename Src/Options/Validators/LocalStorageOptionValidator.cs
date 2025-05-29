using Microsoft.Extensions.Options;

namespace OneDriveFileBackuper.Options.Validators
{
    /// <summary>
    /// Validator backup storage option
    /// </summary>
    public class LocalStorageOptionValidator : IValidateOptions<LocalStorageOption>
    {
        /// <summary>
        /// Validate backup storage option
        /// </summary>
        /// <param name="name">Name</param>
        /// <param name="options">Backup storage option</param>
        /// <returns>Returns success if option is valid</returns>
        public ValidateOptionsResult Validate(string? name, LocalStorageOption options)
        {
            if (string.IsNullOrEmpty(options.Path))
                return ValidateOptionsResult.Fail("Path is required.");

            return ValidateOptionsResult.Success;
        }
    }
}
