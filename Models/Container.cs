namespace SolarSystems.Models
{
    public class Container
    {
        public int Id { get; set; }
        public int containerRow { get; set; }
        public int containerColumn { get; set; }
        public int containerNumber { get; set; }
        
        public int quantityInContainer { get; set; }
        public virtual Component Component { get; set; }
    }
}
