using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering; // Agregar esta directiva
using VolunteerManagement.Application.Services;
using VolunteerManagement.Domain.Entities;
using System.Linq;

namespace VolunteerManagement.Web.Controllers
{
    public class ActivitiesController : Controller
    {
        private readonly ActivityService _activityService;
        private readonly OrganizationService _organizationService;

        public ActivitiesController(ActivityService activityService, OrganizationService organizationService)
        {
            _activityService = activityService;
            _organizationService = organizationService;
        }

        public IActionResult Index()
        {
            var activities = _activityService.GetAllActivities();
            return View(activities);
        }
    public IActionResult Create()
    {
        var organizations = _organizationService.GetAllOrganizations();
        if (organizations == null || !organizations.Any())
        {
            throw new InvalidOperationException("No hay organizaciones disponibles.");
        }

        ViewBag.Organizations = organizations;
        return View();
    }


    [HttpPost]
    public IActionResult Create(Activity activity)
    {
        if (ModelState.IsValid)
        {
            // Validar que la organización seleccionada existe
            var organization = _organizationService.GetOrganizationById(activity.OrganizationId);
            if (organization == null)
            {
                ModelState.AddModelError("OrganizationId", "La organización seleccionada no es válida.");
                ViewBag.Organizations = _organizationService.GetAllOrganizations();
                return View(activity);
            }

            // Agregar la actividad
            _activityService.AddActivity(activity);
            return RedirectToAction(nameof(Index));
        }

        // Si el modelo no es válido
        ViewBag.Organizations = _organizationService.GetAllOrganizations();
        return View(activity);
    }

        public IActionResult Details(int id)
        {
            var activity = _activityService.GetActivityById(id);
            if (activity == null)
            {
                return NotFound();
            }
            return View(activity);
        }

        public IActionResult Edit(int id)
        {
            var activity = _activityService.GetActivityById(id);
            if (activity == null)
            {
                return NotFound();
            }
            ViewBag.Organizations = _organizationService.GetAllOrganizations();
            return View(activity);
        }

// Acción para guardar los cambios de la actividad
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Activity activity)
        {
            if (ModelState.IsValid)
            {
                _activityService.UpdateActivity(activity);
                return RedirectToAction(nameof(Index)); // Redirige al listado de actividades después de editar
            }
            return View(activity); // Si hay errores, vuelve a la vista con el modelo incorrecto
        }

        public IActionResult Delete(int id)
        {
            var activity = _activityService.GetActivityById(id);
            if (activity == null)
            {
                return NotFound();
            }
            return View(activity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var activity = _activityService.GetActivityById(id);
            if (activity == null)
            {
                return NotFound();
            }

            _activityService.DeleteActivity(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
