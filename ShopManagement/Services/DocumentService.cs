using ServiceManagement.DAL;
using ServiceManagement.Models;

namespace ServiceManagement.Services
{
    public class DocumentService
    {
        private readonly DocumentRepository _documentRepository;
        public DocumentService(DocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }

        public Document? GetById(int id)
        {
            Document? document = _documentRepository.GetById(id);
            return document;
        }

        public List<Document> GetAll()
        {
            List<Document> documents = _documentRepository.GetAll().ToList();
            return documents;
        }

        public Document Add(Document document)
        {
            Document? newDocument = _documentRepository.AddAndSaveChanges(document);

            return newDocument;
        }

        public void Update(Document document)
        {
            _documentRepository.UpdateAndSaveChanges(document);
        }

        public void Delete(int id)
        {
            Document? document = _documentRepository.GetById(id);
            if (document != null)
                _documentRepository.RemoveByIdAndSaveChanges(id);
        }

        public List<Document> GetPublicDocument()
        {
            List<Document> documents = _documentRepository.GetAll()
                .Where(x => x.IsPrivate == false).ToList();

            return documents;
        }
    }
}