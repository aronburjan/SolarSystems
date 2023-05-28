using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SolarSystems.Models
{
    public class Component
    {
        /*public Component()
        {
            this.Project = new HashSet<Project>();
        }*/
        public int Id { get; set; }
        public string? componentName { get; set; }
        public int maxStack { get; set; }

        public int price { get; set; }

        public int available { get; set; } = 0;

        [JsonIgnore]
        public virtual ICollection<Container>? Containers { get; set; }

        public ICollection<Project>? Projects { get; set; }
        [JsonIgnore] //this may or may not break something
        public ICollection<ProjectComponent>? ProjectComponents { get; set; }

    }
}
