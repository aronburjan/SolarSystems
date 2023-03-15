namespace SolarSystems.Models
{
    public class Container
    {
        public int Id { get; set; }
        public int containerRow { get; set; }
        public int containerColumn { get; set; }
        public int containerNumber { get; set; }
        public int ComponentId { get; set; } //id of component in container
        public int quantityInContainer { get; set; }

        Container(int Id, int containerRow, int containerColumn, int containerNumber, int ComponentId, int quantityInContainer)
        {
            this.Id = Id;
            this.containerRow = containerRow;
            this.containerColumn = containerColumn;
            this.containerNumber = containerNumber;
            this.ComponentId = ComponentId;
            this.quantityInContainer = quantityInContainer;
        }
    }
}
