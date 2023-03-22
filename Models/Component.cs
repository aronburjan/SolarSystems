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
        public int maxQuantity { get; set; }

        public int price { get; set; }
        public virtual ICollection<Container>? Containers { get; set; }

        public ICollection<Project>? Projects { get; set; }

    }
}
