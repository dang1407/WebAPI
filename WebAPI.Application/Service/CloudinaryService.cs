using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Http;


namespace WebAPI.Application
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary _cloudinary;
        public CloudinaryService() 
        {
            Account cloudinaryAccount = new Account(
                "dmbzrlrot",
                "829873173592494",
                "Sn2ob3jVhNgiSozr1HRtZDt5Euo"
            );

            _cloudinary = new Cloudinary(cloudinaryAccount);
        }  
        public string DeleteImageInCloudinaryAsync(string imageUrl)
        {
            // Extract public ID from Cloudinary URL
            var publicId = GetPublicIdFromUrl(imageUrl);

            if (string.IsNullOrEmpty(publicId))
            {
                return "Invalid image URL";
            }

            var deleteParams = new DeletionParams(publicId)
            {
                ResourceType = ResourceType.Image
            };

            var deletionResult = _cloudinary.Destroy(deleteParams);

            if (deletionResult.Result == "ok")
            {
                return "Image deleted successfully";
            }
            else
            {
                return "Failed to delete image";
            }
        }

        public string? UpLoadImageToCloudinaryAsync(IFormFile imageFile, string folderName = "Garage")
        {
            try
            {
                if (imageFile == null ||  imageFile .Length == 0)
                {
                    return "";
                }

                using (var stream = 
                    imageFile .OpenReadStream())
                {
                    var publicId = $"{folderName}/" + Path.GetFileNameWithoutExtension(
                        imageFile .FileName);
                    var uploadParams = new ImageUploadParams
                    {
                        File = new FileDescription(
                            imageFile .Name, stream),
                        PublicId = publicId,
                        //Transformation = new Transformation().Width(500).Height(500).Crop("fill"),
                    };

                    var uploadResult = _cloudinary.Upload(uploadParams);

                    // Get the image URL from the Cloudinary response
                    string imageUrl = uploadResult.Uri.ToString();

                    // You can now use the 'imageUrl' as needed

                    return imageUrl;
                }
            }
            catch
            {
                return null;
            }
        }

       
        

        private string GetPublicIdFromUrl(string imageUrl)
        {
            // Example Cloudinary URL: "https://res.cloudinary.com/your_cloud_name/image/upload/your_folder_name/your_public_id.jpg"
            // Extract public ID from the URL
            var publicIdIndex = imageUrl.LastIndexOf("/") + 1;
            var extensionIndex = imageUrl.LastIndexOf(".");

            if (publicIdIndex >= 0 && extensionIndex >= 0)
            {
                return imageUrl.Substring(publicIdIndex, extensionIndex - publicIdIndex);
            }

            return null;
        }
    }
}
