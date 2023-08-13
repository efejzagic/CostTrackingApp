using AutoMapper;
using EquipmentService.DTO.Machinery;
using EquipmentService.Interfaces;
using EquipmentService.Models;
using System.Collections.Generic;

namespace EquipmentService.Services
{
    public class MachineryService
    {

        private readonly IMapper _mapper;
        private readonly IMachineryRepository _machineryRepo;
        public MachineryService(IMapper mapper, IMachineryRepository machineryRepo)
        {
            _mapper = mapper;
            _machineryRepo = machineryRepo;
        }

        public async Task<IEnumerable<MachineryDTO>> GetAll ()
        {
            var machinery = await _machineryRepo.GetAll ();
            return _mapper.Map< IEnumerable <Machinery> , IEnumerable <MachineryDTO>>(machinery);
        }

        public async Task<MachineryDTO> GetById(int id)
        {
            var machinery = await _machineryRepo.GetById(id);
            return _mapper.Map<Machinery, MachineryDTO>(machinery);
        }

        public async Task<MachineryDTO> GetByName(string name)
        {
            var machinery = await _machineryRepo.GetByName(name);
            return _mapper.Map<Machinery, MachineryDTO>(machinery);
        }

        public async Task<MachineryDTO> Create(MachineryCreateDTO machinery)
        {
            var mapped = _mapper.Map<Machinery>(machinery);
            //datecreated
            var newMachinery = await _machineryRepo.Create(mapped);
            return _mapper.Map<Machinery, MachineryDTO>(newMachinery);
        }

        public async Task<MachineryDTO> Edit(MachineryEditDTO machinery)
        {
            var mapped = _mapper.Map<Machinery>(machinery);
            //datecreated
            var newMachinery = await _machineryRepo.Edit(mapped);
            return _mapper.Map<Machinery, MachineryDTO>(newMachinery);
        }
        public async Task<bool> Delete(int id)
        {
            return await _machineryRepo.Delete(id);
        }
        public async Task<bool> SoftDelete(int id)
        {
            return await _machineryRepo.SoftDelete(id);
        }


    }
}
