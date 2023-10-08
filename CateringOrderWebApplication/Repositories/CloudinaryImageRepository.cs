﻿using System.Net;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace CateringOrderWebApplication.Repositories
{
    public class CloudinaryImageRepository : IImageRepository
    {
        private readonly IConfiguration _configuration;
        private readonly Account _account;

        public CloudinaryImageRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _account = new Account
            {
                Cloud = configuration.GetSection("Cloudinary")["CloudName"],
                ApiSecret = configuration.GetSection("Cloudinary")["ApiSecret"],
                ApiKey = configuration.GetSection("Cloudinary")["ApiKey"],
            };
        }

        public async Task<string> UploadAsync(IFormFile file)
        {
            var client = new Cloudinary(_account);

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.FileName, file.OpenReadStream()),
                DisplayName = file.FileName,
            };

            var uploadResult = await client.UploadAsync(uploadParams);

            if (uploadResult != null
                && uploadResult.StatusCode == HttpStatusCode.OK)
            {
                return uploadResult.SecureUrl.ToString();
            }

            return null;
        }
    }
}