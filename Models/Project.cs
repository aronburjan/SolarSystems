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


        public int projectNumber { get; set; } //project identification (not unique)
        
        public int neededComponentQuantity { get; set; }

        public int availableComponentQuantity { get; set; }

        //public virtual ICollection<Component> Component { get; set; }
        public ICollection<ProjectStatus> ProjectStatuses { get; set; }



    }
}
