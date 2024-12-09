using VolunteerManagement.Domain.Entities;
using VolunteerManagement.Infrastructure.Data;

namespace VolunteerManagement.Application.Services
{
    public class ActivityService
    {
        private readonly VolunteerManagementDbContext _context;

        public ActivityService(VolunteerManagementDbContext context)
        {
            _context = context;
        }

        public List<Activity> GetAllActivities()
        {
            return _context.Activities.ToList();
        }

        public Activity GetActivityById(int id)
        {
            return _context.Activities.FirstOrDefault(a => a.Id == id);
        }   

    public void AddActivity(Activity activity)
    {
        try
        {
            _context.Activities.Add(activity);
            _context.SaveChanges();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al guardar la actividad: {ex.Message}");
            throw;
        }
    }


        public void UpdateActivity(Activity activity)
        {
            _context.Activities.Update(activity);
            _context.SaveChanges();
        }

        public void DeleteActivity(int id)
        {
            var activity = _context.Activities.FirstOrDefault(a => a.Id == id);
            if (activity != null)
            {
                _context.Activities.Remove(activity);
                _context.SaveChanges();
            }
        }

    }
}
