using AutoMapper;
using EquipmentService.DTO.Maintenance;
using EquipmentService.Interfaces;
using EquipmentService.Models;

namespace EquipmentService.Services
{
    public class MaintenanceService
    {
        private readonly IMapper _mapper;
        private readonly IMaintenanceRepository _maintenanceRepo;
        public MaintenanceService(IMapper mapper, IMaintenanceRepository maintenanceRepo)
        {
            _mapper = mapper;
            _maintenanceRepo = maintenanceRepo;
        }


        public async Task<IEnumerable<MaintenanceDTO>> GetAll()
        {
            var entity = await _maintenanceRepo.GetAll();
            return _mapper.Map<IEnumerable<Maintenance>, IEnumerable<MaintenanceDTO>>(entity);
        }

        public async Task<MaintenanceDTO> GetById(int id)
        {
            var entity = await _maintenanceRepo.GetById(id);
            return _mapper.Map<Maintenance, MaintenanceDTO>(entity);
        }

        public async Task<MaintenanceDTO> GetByTitle(string title)
        {
            var entity = await _maintenanceRepo.GetByTitle(title);
            return _mapper.Map<Maintenance, MaintenanceDTO>(entity);
        }

      

        public async Task<MaintenanceDTO> Create(Maintenance model)
        {
            var entity = await _maintenanceRepo.Create(model);
            return _mapper.Map<Maintenance, MaintenanceDTO>(entity);
        }

        public async Task<MaintenanceDTO> Edit(Maintenance model)
        {
            var entity = await _maintenanceRepo.Edit(model);
            return _mapper.Map<Maintenance, MaintenanceDTO>(entity);
        }

        public async Task<bool> Delete(int id)
        {
            var response = await _maintenanceRepo.Delete(id);
            return response;
        }

        public async Task<bool> SoftDelete(int id)
        {
            var response = await _maintenanceRepo.SoftDelete(id);
            return response;
        }
    }
}
