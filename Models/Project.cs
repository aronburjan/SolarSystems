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

        //public virtual ICollection<Component> Component { get; set; }
        public ICollection<ProjectStatus>? ProjectStatuses { get; set; }
        public ICollection<Component>? Components { get; set; }



    }
}
