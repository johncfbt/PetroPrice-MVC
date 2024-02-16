using Microsoft.AspNetCore.Mvc;
using PetroPrice_MVC.Models;
using PetroPrice_MVC.Services;

namespace PetroPrice_MVC.Controllers
{
    public class PetrolStationController : Controller
    {
        private readonly PetrolStationsDAO _petrolStationsDAO;
        public PetrolStationController(PetrolStationsDAO petrolStationsDAO)
        {
            _petrolStationsDAO = petrolStationsDAO;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PetrolStation petrolStation)
        {
            if (ModelState.IsValid)
            {
                int newId = _petrolStationsDAO.Create(petrolStation);
                if (newId != -1)
                {
                    // If creation is successful, redirect to the index action
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // Handle error if creation fails
                    return View(petrolStation);
                }
            }
            return View("Index",petrolStation);
        }

        public IActionResult Index()
        {
            List<PetrolStation> petrolStations = _petrolStationsDAO.GetAllPetrolStations();
            return View(petrolStations);
        }

        public IActionResult Details(int id)
        {
            PetrolStation PetrolStation = _petrolStationsDAO.GetPetrolStationById(id);
            if (PetrolStation == null)
            {
                return NotFound();
            }
            return View(PetrolStation);
        }

        public IActionResult ShowOnePetrolStationJSON(int id)
        {
            PetrolStation PetrolStation = _petrolStationsDAO.GetPetrolStationById(id);
            if (PetrolStation == null)
            {
                return NotFound();
            }
            // Trim whitespace from the existing values
            PetrolStation.Name = PetrolStation.Name?.Trim(); // Trim whitespace from Name field
            PetrolStation.Address = PetrolStation.Address?.Trim(); // Trim whitespace from Address field
            return Json(PetrolStation);
        }

        public IActionResult Edit(int id)
        {
            // Retrieve the petrol station details by id
            PetrolStation petrolStation = _petrolStationsDAO.GetPetrolStationById(id);

            if (petrolStation == null)
            {
                return NotFound(); // Return 404 if petrol station not found
            }

            // Trim whitespace from the existing values
            petrolStation.Name = petrolStation.Name?.Trim(); // Trim whitespace from Name field
            petrolStation.Address = petrolStation.Address?.Trim(); // Trim whitespace from Address field

            return View(petrolStation); // Pass petrol station details to the view
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(PetrolStation petrolStation)
        {
            if (ModelState.IsValid)
            {
                // Update the petrol station details in the database
                _petrolStationsDAO.Update(petrolStation);
                return RedirectToAction(nameof(Index)); // Redirect to index action after successful update
            }

            return View(petrolStation); // Return to the edit view with validation errors
        }

        public virtual IActionResult ProcessEditReturnPartial(PetrolStation petrolStation)
        {
            if (ModelState.IsValid)
            {
                // Update the petrol station details in the database
                _petrolStationsDAO.Update(petrolStation);
                return PartialView("_petrolStationCard", petrolStation); // Redirect to partial view card action after successful update
            }
                return View(petrolStation); // Return to the edit view with validation errors
        }

        public IActionResult Delete(int Id)
        {
            // Retrieve the petrol station details by id
            PetrolStation petrolStation = _petrolStationsDAO.GetPetrolStationById(Id);

            if (petrolStation == null)
            {
                return NotFound(); // Return 404 if petrol station not found
            }

            _petrolStationsDAO.Delete(Id);
            return View("Index", _petrolStationsDAO.GetAllPetrolStations());
        }

        public IActionResult SearchResults(string searchTerm)
        {
            List<PetrolStation> foundPetrolStations = _petrolStationsDAO.SearchPetrolStations(searchTerm);
            return View("index", foundPetrolStations);
        }

    }
}
