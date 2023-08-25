
using Equipment.Application.Parameters;
using Equipment.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Equipment.Application.DTOs.Tool
{
    public class ToolDTO : RequestParameter
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public string Location { get; set; }


        public bool retired { get; set; }
    }
}
