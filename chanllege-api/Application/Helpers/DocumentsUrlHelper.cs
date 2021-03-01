using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Persistence;

namespace Application.Helpers
{
    public class DocumentsUrlHelper : IDocumentsUrl
    {
        private readonly IConfiguration _configuration;

        public DocumentsUrlHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<Domain.Document> GetDocumentUrl(Domain.Document document, string basePath)
        {
            var apiUrl = _configuration["ApiUrl"];
            var url = apiUrl + $"{basePath}/{document.Question.Id}/document/{document.Url}";
            document.Url = url;

            return document;
        }
    }
}