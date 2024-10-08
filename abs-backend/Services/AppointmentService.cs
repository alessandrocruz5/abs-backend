using abs_backend.Models;
using abs_backend.Repositories;

namespace abs_backend.Services
{
    public class AppointmentService
    {
        private readonly IAppointmentRepository _repository;

        public AppointmentService(IAppointmentRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentsAsync()
        {
            return await _repository.GetAppointmentsAsync();
        }

        public async Task<Appointment> GetAppointmentByIdAsync(int id)
        {
            return await _repository.GetAppointmentByIdAsync(id);
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentsByCustomerIdAsync(int customerId)
        {
            return await _repository.GetAppointmentsByCustomerIdAsync(customerId);
        }

        public async Task<Appointment> CreateAppointmentAsync(Appointment appointment)
        {
            return await _repository.CreateAppointmentAsync(appointment);
        }

        public async Task<Appointment> UpdateAppointmentAsync(Appointment appointment)
        {
            return await _repository.UpdateAppointmentAsync(appointment);
        }

        public async Task DeleteAppointmentAsync(int id)
        {
            await _repository.DeleteAppointmentAsync(id);
        }
    }
}
