using System.Collections.Generic;
using System.Linq;
using VolunteerManagement.Domain.Entities;
using VolunteerManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace VolunteerManagement.Application.Services
{
    public class OrganizationService
    {
        private readonly VolunteerManagementDbContext _context;

        public OrganizationService(VolunteerManagementDbContext context)
        {
            _context = context;
        }

        // Obtener todas las organizaciones
        public IEnumerable<Organization> GetAllOrganizations()
        {
            return _context.Organizations.ToList();
        }
        public Organization GetOrganizationById(int id)
        {
            return _context.Organizations.FirstOrDefault(o => o.Id == id);
        }

        // Agregar una nueva organización
    public void AddOrganization(Organization organization)
    {
        try
        {
            _context.Organizations.Add(organization);
            _context.SaveChanges();
        }
        catch (DbUpdateException ex)
        {
            // Log del error
            Console.WriteLine($"Error al guardar la organización: {ex.InnerException?.Message}");
            throw;
        }
    }


        // Actualizar una organización existente
        public void UpdateOrganization(Organization organization)
        {
            var existingOrg = _context.Organizations.Find(organization.Id);
            if (existingOrg != null)
            {
                existingOrg.Name = organization.Name;
                existingOrg.Address = organization.Address;
                existingOrg.PhoneNumber = organization.PhoneNumber;

                _context.SaveChanges(); // Guarda los cambios en la base de datos
            }
        }

        // Eliminar una organización
        public void DeleteOrganization(int id)
        {
            var organization = _context.Organizations.Find(id);
            if (organization != null)
            {
                _context.Organizations.Remove(organization);
                _context.SaveChanges(); // Guarda los cambios en la base de datos
            }
        }
    }
}
