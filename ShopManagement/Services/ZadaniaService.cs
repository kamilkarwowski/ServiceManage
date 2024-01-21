using ServiceManagement.Models;
using ServiceManagement.DAL;

namespace ServiceManagement.Services
{
    public class ZadaniaService
    {
        private readonly ZadanieRepository _zadanieRepository;  
        public ZadaniaService(ZadanieRepository ZadanieRepository)
        {
            _zadanieRepository = ZadanieRepository;
        }

        public Zadanie? GetById(int id)
        {
            Zadanie? Zadanie = _zadanieRepository.GetById(id);
            return Zadanie;
        }

        public List<Zadanie> GetAll() 
        {
        List<Zadanie> Zadania = _zadanieRepository.GetAll().ToList();
            return Zadania;
        }

        public Zadanie Add(Zadanie Zadanie) 
        {
        Zadanie? newZadanie = _zadanieRepository.AddAndSaveChanges(Zadanie);
            return newZadanie;
        }

        public void Update(Zadanie Zadanie) 
        {
            _zadanieRepository.UpdateAndSaveChanges(Zadanie);
        }
        public void Delete(int id) 
        {
            Zadanie? Zadanie = _zadanieRepository.GetById(id);
            _zadanieRepository.RemoveByIdAndSaveChanges(id);

        }

        
    }
}
