using System.Threading.Tasks;
using Domain;

namespace Application.Interfaces
{
    public interface IDocumentsUrl
    {
        Task<Document> GetDocumentUrl(Document document, string basePath);
    }
}