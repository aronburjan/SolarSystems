using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SolarSystems.Models
{
    
    public class Project
    {
        /*public Project()
        {
            this.Component = new HashSet<Component>();
        }*/
        public int Id { get; set; } //row identification (unique)

        public int HourlyLaborRate { get; set; }

        public int LaborTime { get; set; }

        public string? ProjectLocation { get; set; }

        public string? ProjectDescription { get; set; } 
        public string? CustomerName { get; set; }

        public string? CurrentStatus { get; set; } = "New";

        public int? totalPrice { get; set; } = 0;

        public bool canBeScheduled { get; set; } = false;

        //public virtual ICollection<Component> Component { get; set; }
        public ICollection<ProjectStatus>? ProjectStatuses { get; set; }
        public ICollection<Component>? Components { get; set; }

        public ICollection<ProjectComponent> ProjectComponents { get; set; }




    }
}
