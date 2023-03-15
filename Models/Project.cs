namespace SolarSystems.Models
{
    public class Project
    {
        public int Id { get; set; } //row identification (unique)

        public int projectNumber { get; set; } //project identification (not unique)
        public int projectUserId { get; set; }

        public int neededComponentId { get; set; }

        public int neededComponentQuantity { get; set; }

        public int status { get; set; }

        public Project(int Id, int projectNumber, int projectUserId, int neededComponentId, int neededComponentQuantity, int status = 0) 
        {
            this.Id = Id;
            this.projectNumber = projectNumber;
            this.projectUserId = projectUserId;
            this.neededComponentId = neededComponentId;
            this.neededComponentQuantity = neededComponentQuantity;
            this.status = status;
        }


    }
}
