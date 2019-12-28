// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ImageUpload.cs" company="Bridgelabz">
//   Copyright © 2019 Company="BridgeLabz"
// </copyright>
// <creator name="Ajay Lodale"/>
// --------------------------------------------------------------------------------------------------------------------
namespace CommonLayer.Model
{ 
    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Microsoft.AspNetCore.Http;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ImageUpload
    {

        public Cloudinary cloudinary;
       public string CLOUD_NAME = "drldti95x";
       public string API_KEY = "154737591514573";
       public string API_SECCRET_KEY = "vFRKdgnEXABywr-vTnX9jalg0Kw";
       public string UploadImage(IFormFile formFile)
       {
           try
           {
               var file = formFile.FileName;
               var stream = formFile.OpenReadStream();
               var uploadParams = new ImageUploadParams()
               {
                   File = new FileDescription(file, stream)
               };
               var uploadResult = cloudinary.Upload(uploadParams);
               return uploadResult.Uri.ToString();
           }
           catch (Exception E)
           {
               throw new Exception(E.Message);
           }
       }

    }
}


