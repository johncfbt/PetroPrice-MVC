using PetroPrice_MVC.Models;

namespace PetroPrice_MVC.Services
{
    public interface IPetrolStationService
    {
        List<PetrolStation> GetAllPetrolStations();
        List<PetrolStation> SearchPetrolStations(string searchTerm);
        PetrolStation GetPetrolStationById(int id);
        int Create(PetrolStation product);
        int Delete(int Id);
        int Update(PetrolStation product);
    }
}
