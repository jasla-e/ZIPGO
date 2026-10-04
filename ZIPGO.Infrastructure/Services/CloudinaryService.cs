using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using ZIPGO.Application.Services;

namespace ZIPGO.Infrastructure.Services
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryService(Cloudinary cloudinary)
        {
            _cloudinary = cloudinary;
        }

        public async Task<string> UploadImage(
            Stream imageStream,
            string fileName)
        {
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(
                    fileName,
                    imageStream
                ),

                Folder = "ZIPGO/products"
            };

            var result = await _cloudinary.UploadAsync(uploadParams);

            if (result.Error != null)
            {
                throw new Exception(
                    $"Cloudinary upload failed: {result.Error.Message}"
                );
            }

            if (result.SecureUrl == null)
            {
                throw new Exception(
                    "Cloudinary upload completed but no image URL was returned."
                );
            }

            return result.SecureUrl.ToString();
        }
    }
}