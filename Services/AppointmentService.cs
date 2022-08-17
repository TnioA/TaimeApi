using System.Threading.Tasks;
using TaimeApi.Data.MySql.Entities;
using TaimeApi.Data.MySql.Repositories;
using TaimeApi.Enums;
using TaimeApi.Utils.Attributes;
using TaimeApi.Utils.Services;

namespace TaimeApi.Services
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