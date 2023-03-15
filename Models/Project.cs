namespace SolarSystems.Models
{
    public class Project
    {
        public int Id { get; set; } //row identification (unique)

        public int projectNumber { get; set; } //project identification (not unique)
        public int UserId { get; set; }

        public int ComponentId { get; set; } //needed component Id

        public int neededComponentQuantity { get; set; }

        public int availableComponentQuantity { get; set; }

        public int status { get; set; }

        public Project(int Id, int projectNumber, int UserId, int ComponentId, int availableComponentQuantity, int neededComponentQuantity, int status = 0) 
        {
            this.Id = Id;
            this.projectNumber = projectNumber;
            this.UserId = UserId;
            this.availableComponentQuantity = availableComponentQuantity;
            this.ComponentId = ComponentId;
            this.neededComponentQuantity = neededComponentQuantity;
            this.status = status;
        }


    }
}
