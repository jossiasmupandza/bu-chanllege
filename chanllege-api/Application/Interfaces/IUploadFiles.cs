using System.Reflection.Metadata;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Application.Interfaces
{
    public interface IUploadFiles
    {
        Task<Domain.Document> UploadDocuments(IFormFile file);
        Task RemoveDocuments(Domain.Document document);
    }
}