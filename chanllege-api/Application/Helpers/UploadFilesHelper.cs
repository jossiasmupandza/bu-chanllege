using System;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Application.Errors;
using Application.Interfaces;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Persistence;

namespace Application.Helpers
{
    public class UploadFilesHelper : IUploadFiles
    {
        private readonly IConfiguration _configuration;
        private readonly IHostEnvironment _environment;
        
        public UploadFilesHelper(IConfiguration configuration, IHostEnvironment environment)
        {
            _configuration = configuration;
            _environment = environment;
        }

        public async Task<Domain.Document> UploadDocuments(IFormFile file)
        {
            var  document = new Document();
            try
            {
                // foreach (var file in files)
                // {
                    var uploadDir = _configuration["UploadDir"];
                    var root = "/";
                    if (uploadDir[0] != '/') root = _environment.ContentRootPath;
                    var finalUploadDir = Path.Combine(root, uploadDir);
                    var ext = Path.GetExtension(file.FileName);
                    var fileToken = $"{GenerateSecureString(20)}{ext}";

                    if (file.Length > 0)
                    {
                        using (var fileStream = new FileStream(Path.Combine(finalUploadDir, fileToken), FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);

                            document = new Document
                            {
                                Name = file.FileName,
                                Url = fileToken
                            };

                            return document;
                        }
                
                    //}
                }
                
            }
            catch (Exception e)
            {
                throw new RestException(HttpStatusCode.BadRequest, $"Error Uploading file :{e}");
            }

            return document;
        }

        public async Task RemoveDocuments(Domain.Document document)
        {
            Console.WriteLine(document.Name + "Removed!");
        }

        public static string MakeBase64UrlSafe(string input)
        {
            return input.TrimEnd('=').Replace('+', '-').Replace('/', '_');
        }
        
        public static string GenerateSecureString(int bytes)
        {
            using (var rng = RandomNumberGenerator.Create())
            {
                var tokenData = new byte[bytes];
                rng.GetBytes(tokenData);

                return MakeBase64UrlSafe(Convert.ToBase64String(tokenData));
            }
        }
    }
}