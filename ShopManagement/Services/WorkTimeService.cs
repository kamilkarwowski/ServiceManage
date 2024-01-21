using ServiceManagement.DAL;
using ServiceManagement.Models;

namespace ServiceManagement.Services
{
    public class WorkTimeService
    {
        private readonly WorkTimeRepository _workTimeRepository;
        public WorkTimeService(WorkTimeRepository workTimeRepository)
        {
            _workTimeRepository = workTimeRepository;
        }

        public WorkTime? GetById(int id)
        {
            WorkTime? workTime = _workTimeRepository.GetById(id);
            return workTime;
        }

        public List<WorkTime> GetAll()
        {
            List<WorkTime> worktimes = _workTimeRepository.GetAll().ToList();
            return worktimes;
        }

        public WorkTime Add(WorkTime workTime)
        {
            WorkTime? newWorkTime = _workTimeRepository.AddAndSaveChanges(workTime);

            return newWorkTime;
        }

        public void Update(WorkTime workTime)
        {
            _workTimeRepository.UpdateAndSaveChanges(workTime);
        }

        public void Delete(int id)
        {
            WorkTime? WorkTime = _workTimeRepository.GetById(id);

            _workTimeRepository.RemoveByIdAndSaveChanges(id);
        }


    }
}