using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetroPrice_MVC.Models;
using PetroPrice_MVC.Services;

namespace PetroPrice_MVC.Controllers
{
    [Route("api/")]
    [ApiController]
    public class PetrolStationControllerAPI : ControllerBase
    {
        private readonly PetrolStationsDAO _petrolStationsDAO;
        public PetrolStationControllerAPI(PetrolStationsDAO petrolStationsDAO)
        {
            _petrolStationsDAO = petrolStationsDAO;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PetrolStation>> Index()
        {
            List<PetrolStation> petrolStations = _petrolStationsDAO.GetAllPetrolStations();
            return petrolStations;
        }

        [HttpGet("Details/{Id}")]
        public ActionResult <PetrolStation> Details (int Id)
        {
            PetrolStation PetrolStation = _petrolStationsDAO.GetPetrolStationById(Id);
            return PetrolStation;
        }

        [HttpPost("Create")]
        // post action
        // expect a product in json format in the body of the request
        public ActionResult <int> Create(PetrolStation p)
        {
            int newId = _petrolStationsDAO.Create(p);
            return Ok(newId); // Return OK (200) status with the new ID
        }

        [HttpPut("ProcessEdit")]
        // put request
        // expect a json formatted object in the body of the request. id number must match the item being modified.
        public ActionResult <PetrolStation> ProcessEdit(PetrolStation p)
        {
            _petrolStationsDAO.Update(p);
            PetrolStation updatedPetrolStation = _petrolStationsDAO.GetPetrolStationById(p.Id);
            return updatedPetrolStation;
        }

        [HttpDelete("Delete/{Id}")]
        public ActionResult <int> Delete (int Id)
        {
            return _petrolStationsDAO.Delete(Id);
        }

    }
}
