namespace SolarSystems.Models
{
    public class Component
    {
        public Component()
        {
            this.Project = new HashSet<Project>();
        }

        public int Id { get; set; }
        public string? componentName { get; set; }

        public int maxQuantity { get; set; }

        public Component(int id, string? componentName, int maxQuantity)
        {
            this.Id = id;
            this.componentName = componentName;
            this.maxQuantity = maxQuantity;
        }

        public virtual ICollection<Container> Containers { get; set; }

        public virtual ICollection<Project> Project { get; set; }

    }
}
