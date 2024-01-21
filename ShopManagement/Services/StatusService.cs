using ServiceManagement.DAL;
using ServiceManagement.Models;

namespace ServiceManagement.Services
{
    public class StatusService
    {
        private readonly StatusRepository _statusRepository;

        public StatusService(StatusRepository statusRepository)
        {
            _statusRepository = statusRepository;
        }

        public Status? GetById (int id)
        {
            Status? status = _statusRepository.GetById(id);

            return status;
        }
        public List<Status> GetAll()
        {
            List<Status> statuses = _statusRepository.GetAll().ToList();
            return statuses;
        }

        public Status Add(Status status)
        {
            Status? newStatus = _statusRepository.AddAndSaveChanges(status);
            return newStatus;

        }
        public void Update(Status status)
        {
            _statusRepository.UpdateAndSaveChanges(status);
        }

        public void Delete(int id)
        {
            Status? status = _statusRepository.GetById(id);
            _statusRepository.RemoveByIdAndSaveChanges(id);
        }
    }
}
