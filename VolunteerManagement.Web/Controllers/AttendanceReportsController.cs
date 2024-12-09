using Microsoft.AspNetCore.Mvc;
using VolunteerManagement.Application.Services;  // Asegúrate de que la clase AttendanceReportService esté disponible
using VolunteerManagement.Infrastructure.Data;
using VolunteerManagement.Domain.Entities; // Asegúrate de importar las entidades correctas
using Microsoft.AspNetCore.Mvc.Rendering;

namespace VolunteerManagement.Web.Controllers
{
    public class AttendanceReportsController : Controller
    {
        private readonly AttendanceReportService _attendanceReportService;

        public AttendanceReportsController(AttendanceReportService attendanceReportService)
        {
            _attendanceReportService = attendanceReportService;
        }

        // Acción para obtener todos los reportes de asistencia
    public IActionResult Index()
    {
        var volunteers = _attendanceReportService.GetVolunteers();
        var activities = _attendanceReportService.GetActivities();
        var reports = _attendanceReportService.GetAll();

        var viewModel = new AttendanceReportViewModel
        {
            Reports = reports,
            Volunteers = volunteers,
            Activities = activities
        };

        return View(viewModel);
    }



        // Acción para ver los detalles de un reporte de asistencia específico
        public IActionResult Details(int id)
        {
            var report = _attendanceReportService.GetReportById(id);
            if (report == null)
            {
                return NotFound();
            }
            return View(report);
        }

        // Acción para crear un nuevo reporte de asistencia
    public IActionResult Create()
    {
        // Asegúrate de que obtienes la lista de voluntarios y actividades correctamente
        var volunteers = _attendanceReportService.GetVolunteers();
        var activities = _attendanceReportService.GetActivities();

        // Si alguna de estas colecciones es null, inicialízala para evitar NullReferenceException
        ViewBag.Volunteers = volunteers ?? new List<Volunteer>();
        ViewBag.Activities = activities ?? new List<Activity>();

        return View();
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(AttendanceReport report)
    {
        if (ModelState.IsValid)
        {
            _attendanceReportService.AddReport(report);
            return RedirectToAction(nameof(Index));
        }

        // Si el modelo no es válido, recargar los datos para la vista
        var volunteers = _attendanceReportService.GetVolunteers();
        var activities = _attendanceReportService.GetActivities();

        ViewBag.Volunteers = new SelectList(volunteers, "Id", "FirstName");
        ViewBag.Activities = new SelectList(activities, "Id", "Name");

        return View(report);
    }


        // Acción para editar un reporte de asistencia
        public IActionResult Edit(int id)
        {
            var report = _attendanceReportService.GetReportById(id);
            if (report == null)
            {
                return NotFound();
            }
            return View(report);
        }

        // Acción POST para actualizar un reporte de asistencia
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, AttendanceReport report)
        {
            if (id != report.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _attendanceReportService.UpdateReport(report);
                return RedirectToAction(nameof(Index));
            }
            return View(report);
        }

        // Acción para eliminar un reporte de asistencia
    public IActionResult Delete(int id)
    {
        var report = _attendanceReportService.GetById(id);
        if (report == null) return NotFound();

        var volunteerName = _attendanceReportService.GetVolunteerName(report.VolunteerId);
        var activityName = _attendanceReportService.GetActivityName(report.ActivityId);

        ViewBag.VolunteerName = volunteerName;
        ViewBag.ActivityName = activityName;

        return View(report);
    }


        // Acción POST para eliminar un reporte de asistencia
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _attendanceReportService.DeleteReport(id);
            return RedirectToAction(nameof(Index));
        }
    }



}
