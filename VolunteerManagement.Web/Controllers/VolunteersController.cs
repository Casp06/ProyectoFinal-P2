using Microsoft.AspNetCore.Mvc;
using VolunteerManagement.Application.Services;
using VolunteerManagement.Domain.Entities;

namespace VolunteerManagement.Web.Controllers
{
    public class VolunteersController : Controller
    {
        private readonly VolunteerService _volunteerService;

        public VolunteersController(VolunteerService volunteerService)
        {
            _volunteerService = volunteerService;
        }

        public IActionResult Index()
        {
            var volunteers = _volunteerService.GetAllVolunteers();
            return View(volunteers);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Volunteer volunteer)
        {
            if (ModelState.IsValid)
            {
                _volunteerService.AddVolunteer(volunteer);
                return RedirectToAction(nameof(Index));
            }
            return View(volunteer);
        }

        public IActionResult Details(int id)
        {
            var volunteer = _volunteerService.GetVolunteerById(id);
            if (volunteer == null)
            {
                return NotFound();
            }
            return View(volunteer);
        }

        // Acción para editar un voluntario
        public IActionResult Edit(int id)
        {
            var volunteer = _volunteerService.GetVolunteerById(id);
            if (volunteer == null)
            {
                return NotFound();
            }
            return View(volunteer);
        }

        [HttpPost]
        public IActionResult Edit(Volunteer volunteer)
        {
            if (ModelState.IsValid)
            {
                _volunteerService.UpdateVolunteer(volunteer);
                return RedirectToAction(nameof(Index));
            }
            return View(volunteer);
        }

        // Acción para eliminar un voluntario
        public IActionResult Delete(int id)
        {
            var volunteer = _volunteerService.GetVolunteerById(id);
            if (volunteer == null)
            {
                return NotFound();
            }
            return View(volunteer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var volunteer = _volunteerService.GetVolunteerById(id);
            if (volunteer == null)
            {
                return NotFound();
            }

            _volunteerService.DeleteVolunteer(id);
            return RedirectToAction(nameof(Index)); // Redirige a la lista de actividades
        }
    }
}
