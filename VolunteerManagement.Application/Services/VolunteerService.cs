using VolunteerManagement.Domain.Entities;
using VolunteerManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace VolunteerManagement.Application.Services
{
    public class VolunteerService
    {
        private readonly VolunteerManagementDbContext _context;

        public VolunteerService(VolunteerManagementDbContext context)
        {
            _context = context;
        }

        // Obtener todos los voluntarios
        public List<Volunteer> GetAllVolunteers()
        {
            return _context.Volunteers.ToList();
        }

        // Obtener un voluntario por su ID
        public Volunteer GetVolunteerById(int id)
        {
            return _context.Volunteers.FirstOrDefault(v => v.Id == id);
        }

        // Agregar un nuevo voluntario
        public void AddVolunteer(Volunteer volunteer)
        {
            _context.Volunteers.Add(volunteer);
            _context.SaveChanges();
        }

        public void UpdateVolunteer(Volunteer volunteer)
        {
            _context.Volunteers.Update(volunteer);
            _context.SaveChanges();
        }

        public void DeleteVolunteer(int id)
        {
            var volunteer = _context.Volunteers.FirstOrDefault(v => v.Id == id);
            if (volunteer != null)
            {
                _context.Volunteers.Remove(volunteer);
                _context.SaveChanges();
            }
        }

    }
}
