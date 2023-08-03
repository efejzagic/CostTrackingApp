using AutoMapper;
using EquipmentService.DTO.MachineryServicing;
using EquipmentService.Interfaces;
using EquipmentService.Models;

namespace EquipmentService.Services
{
    public class MachineryServicingService
    {
        private readonly IMachineryServicingRepository _machineryServicingRepo;
        private readonly IMapper _mapper;
        public MachineryServicingService(IMachineryServicingRepository machineryServicingRepo, IMapper mapper)
        {
            _machineryServicingRepo = machineryServicingRepo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MachineryServicingDTO>> GetAll()
        {
            var entity = await _machineryServicingRepo.GetAll();
            return _mapper.Map<IEnumerable<MachineryServicing>, IEnumerable<MachineryServicingDTO>>(entity);
        }

        public async Task<MachineryServicingDTO> GetById(int id)
        {
            var entity = await _machineryServicingRepo.GetById(id);
            return _mapper.Map<MachineryServicing, MachineryServicingDTO>(entity);
        }

        public async Task<MachineryServicingDTO> GetByTitle(string title)
        {
            var entity = await _machineryServicingRepo.GetByTitle(title);
            return _mapper.Map<MachineryServicing, MachineryServicingDTO>(entity);
        }

        public async Task<MachineryServicingDTO> GetByServiceDate(DateTime serviceDate)
        {
            var entity = await _machineryServicingRepo.GetByServiceDate(serviceDate);
            return _mapper.Map<MachineryServicing, MachineryServicingDTO>(entity);
        }

        public async Task<MachineryServicingDTO> Create(MachineryServicingCreateDTO model)
        {
            var mapped = _mapper.Map<MachineryServicing>(model);
            var entity = await _machineryServicingRepo.Create(mapped);
            return _mapper.Map<MachineryServicing, MachineryServicingDTO>(entity);
        }

        public async Task<MachineryServicingDTO> Edit(MachineryServicingEditDTO model)
        {
            var mapped = _mapper.Map<MachineryServicing>(model);
            var entity = await _machineryServicingRepo.Edit(mapped);
            return _mapper.Map<MachineryServicing, MachineryServicingDTO>(entity);
        }

        public async Task<bool> Delete(int id)
        {
            var response = await _machineryServicingRepo.Delete(id);
            return response;
        }

        public async Task<bool> SoftDelete(int id)
        {
            var response = await _machineryServicingRepo.SoftDelete(id);
            return response;
        }
    }
}
