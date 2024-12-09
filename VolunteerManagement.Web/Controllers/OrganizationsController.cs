using Microsoft.AspNetCore.Mvc;
using VolunteerManagement.Application.Services;
using VolunteerManagement.Domain.Entities;

namespace VolunteerManagement.Web.Controllers
{
    public class OrganizationsController : Controller
    {
        private readonly OrganizationService _organizationService;

        public OrganizationsController(OrganizationService organizationService)
        {
            _organizationService = organizationService;
        }

        // Lista de organizaciones
        public IActionResult Index()
        {
            var organizations = _organizationService.GetAllOrganizations();
            return View(organizations);
        }

        // Vista para crear una organización
        public IActionResult Create()
        {
            return View();
        }

        // Acción para guardar una nueva organización
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Organization organization)
        {
            if (ModelState.IsValid)
            {
                _organizationService.AddOrganization(organization);
                return RedirectToAction(nameof(Index));
            }
            return View(organization);
        }

        // Vista para editar una organización
        public IActionResult Edit(int id)
        {
            var organization = _organizationService.GetAllOrganizations()
                .FirstOrDefault(o => o.Id == id);

            if (organization == null)
            {
                return NotFound();
            }

            return View(organization);
        }

        // Acción para guardar los cambios al editar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Organization organization)
        {
            if (id != organization.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                _organizationService.UpdateOrganization(organization);
                return RedirectToAction(nameof(Index));
            }

            return View(organization);
        }

        // Confirmación para eliminar una organización
        public IActionResult Delete(int id)
        {
            var organization = _organizationService.GetAllOrganizations()
                .FirstOrDefault(o => o.Id == id);

            if (organization == null)
            {
                return NotFound();
            }

            return View(organization);
        }

        // Acción para eliminar una organización
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _organizationService.DeleteOrganization(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
