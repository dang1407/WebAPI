using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Application
{
    public interface ICloudinaryService
    {
        string? UpLoadImageToCloudinaryAsync(IFormFile imageFile, string folderName);
        string DeleteImageInCloudinaryAsync(string imageUrl);
    }
}
