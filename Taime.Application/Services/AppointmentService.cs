using System.Threading.Tasks;
using Taime.Application.Data.MySql.Entities;
using Taime.Application.Data.MySql.Repositories;
using Taime.Application.Utils.Attributes;
using Taime.Application.Utils.Services;

namespace Taime.Application.Services
{
    [InjectionType(InjectionType.Scoped)]
    public class AppointmentService : BaseService
    {
        private readonly AppointmentRepository _appointmentRepository;

        public AppointmentService(AppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<ResultData> GetByUser(int userid)
        {
            var data = await _appointmentRepository.ReadAsync(x=> x.UserId == userid);
            return SuccessData(data);
        }

        public async Task<ResultData> Create(AppointmentEntity request)
        {
            await _appointmentRepository.CreateAsync(request);
            return SuccessData(request);
        }
    }
}