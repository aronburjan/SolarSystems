using Azure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SolarSystems.Models
{
    public class ProjectComponent
    {
        public int ProjectId { get; set; }
        public int ComponentId { get; set; }

        public Project Project { get; set; }
        public Component Component { get; set; }

        public int Quantity { get; set; }
    }
}
