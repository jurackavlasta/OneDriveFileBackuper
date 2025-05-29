using Microsoft.Extensions.Options;
using OneDrivePhotoDownloader.Options;

namespace OneDriveFileBackuper.Options.Validators
{
    /// <summary>
    /// Validator OneDrive option
    /// </summary>
    public class OneDriveOptionValidator : IValidateOptions<OneDriveOption>
    {
        /// <summary>
        /// Validate OneDrive option
        /// </summary>
        /// <param name="name">Name</param>
        /// <param name="options">One Drive option</param>
        /// <returns>Returns success if option is valid</returns>
        public ValidateOptionsResult Validate(string? name, OneDriveOption options)
        {
            if (string.IsNullOrEmpty(options.Drive))
                return ValidateOptionsResult.Fail("Drive is required.");

            if (string.IsNullOrEmpty(options.FilesFolder))
                return ValidateOptionsResult.Fail("File folder is required.");

            return ValidateOptionsResult.Success;
        }
    }
}
