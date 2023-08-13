using AutoMapper;
using EquipmentService.DTO.MachineryServicing;
using EquipmentService.Interfaces;
using EquipmentService.Models;

namespace EquipmentService.Services
{
    public class ToolServicingService
    {
        private readonly IToolServicingRepository _toolServicingRepo;
        private readonly IMapper _mapper;
        public ToolServicingService(IToolServicingRepository toolServicingRepo, IMapper mapper)
        {
            _toolServicingRepo = toolServicingRepo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ToolServicingDTO>> GetAll()
        {
            var entity = await _toolServicingRepo.GetAll();
            return _mapper.Map<IEnumerable<ToolServicing>, IEnumerable<ToolServicingDTO>>(entity);
        }

        public async Task<ToolServicingDTO> GetById(int id)
        {
            var entity = await _toolServicingRepo.GetById(id);
            return _mapper.Map<ToolServicing, ToolServicingDTO>(entity);
        }

        public async Task<ToolServicingDTO> GetByTitle(string title)
        {
            var entity = await _toolServicingRepo.GetByTitle(title);
            return _mapper.Map<ToolServicing, ToolServicingDTO>(entity);
        }

        public async Task<ToolServicingDTO> GetByServiceDate(DateTime serviceDate)
        {
            var entity = await _toolServicingRepo.GetByServiceDate(serviceDate);
            return _mapper.Map<ToolServicing, ToolServicingDTO>(entity);
        }

        public async Task<ToolServicingDTO> Create(ToolServicingCreateDTO model)
        {
            var mapped = _mapper.Map<ToolServicing>(model);
            var entity = await _toolServicingRepo.Create(mapped);
            return _mapper.Map<ToolServicing, ToolServicingDTO>(entity);
        }

        public async Task<ToolServicingDTO> Edit(ToolServicingEditDTO model)
        {
            var mapped = _mapper.Map<ToolServicing>(model);
            var entity = await _toolServicingRepo.Edit(mapped);
            return _mapper.Map<ToolServicing, ToolServicingDTO>(entity);
        }

        public async Task<bool> Delete(int id)
        {
            var response = await _toolServicingRepo.Delete(id);
            return response;
        }

        public async Task<bool> SoftDelete(int id)
        {
            var response = await _toolServicingRepo.SoftDelete(id);
            return response;
        }
    }
}
