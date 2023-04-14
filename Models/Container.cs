namespace SolarSystems.Models
{
    public class Container
    {
        public int Id { get; set; }
        public int containerRow { get; set; }
        public int containerColumn { get; set; }
        public int containerNumber { get; set; }

        public int totalSpace { get; set; } = 100;
        public int freeSpace { get; set; } = 100;
        public int quantityInContainer { get; set; } //component db
        public virtual Component? Component { get; set; } //component id
    }
}
