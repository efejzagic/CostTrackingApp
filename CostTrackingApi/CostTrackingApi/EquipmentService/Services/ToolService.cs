using AutoMapper;
using EquipmentService.DTO.Tool;
using EquipmentService.Interfaces;
using EquipmentService.Models;

namespace EquipmentService.Services
{
    public class ToolService
    {
        private readonly IMapper _mapper;
        private readonly IToolRepository _toolRepo;
        public ToolService(IMapper mapper, IToolRepository toolRepo)
        {
            _mapper = mapper;
            _toolRepo = toolRepo;
        }

        public async Task<IEnumerable<ToolDTO>> GetAll()
        {
            var tools = await _toolRepo.GetAll();
            return _mapper.Map<IEnumerable<Tool>, IEnumerable<ToolDTO>>(tools);
        }

        public async Task<ToolDTO> GetById (int id)
        {
            var tool = await _toolRepo.GetById(id);
            return _mapper.Map<Tool, ToolDTO>(tool);
        }

        public async Task<ToolDTO> GetByTitle(string name)
        {
            var tool = await _toolRepo.GetByName(name);
            return _mapper.Map<Tool, ToolDTO>(tool);
        }

        public async Task<ToolDTO> Create(ToolCreateDTO tool)
        {
            var mapped = _mapper.Map<Tool>(tool);
            var newTool = await _toolRepo.Create(mapped);
            return _mapper.Map<Tool, ToolDTO>(newTool);
        }

        public async Task<ToolDTO> Edit(ToolEditDTO tool)
        {
            var mapped = _mapper.Map<Tool>(tool);
            var newTool = await _toolRepo.Edit(mapped);
            return _mapper.Map<Tool, ToolDTO>(newTool);
        }

        public async Task<bool> Delete(int id)
        {
            var response = await _toolRepo.Delete(id);
            return response;
        }
        public async Task<bool> SoftDelete(int id)
        {
            var response = await _toolRepo.SoftDelete(id);
            return response;
        }
    }
}
