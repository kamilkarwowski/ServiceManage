using ServiceManagement.DAL;
using ServiceManagement.Models;

namespace ServiceManagement.Services
{
    public class AreaService
    {
        private readonly AreaRepository _areaRepository;
        public AreaService(AreaRepository areaRepository)
        {
            _areaRepository = areaRepository;
        }

        public Area? GetById(int id)
        {
            Area? area = _areaRepository.GetById(id);
            return area;
        }

        public List<Area> GetAll()
        {
            List<Area> areas = _areaRepository.GetAll().ToList();
            return areas;
        }

        public Area Add(Area area)
        {
            Area? newArea = _areaRepository.AddAndSaveChanges(area);

            return newArea;
        }

        public void Update(Area area)
        {
            _areaRepository.UpdateAndSaveChanges(area);
        }

        public void Delete(int id)
        {
            Area? area = _areaRepository.GetById(id);

            _areaRepository.RemoveByIdAndSaveChanges(id);
        }
    }
}
