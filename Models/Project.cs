namespace SolarSystems.Models
{
    public class Project
    {
        public int Id { get; set; }
        public int projectUserId { get; set; }
        public int status { get; set; }

        public Project(int Id, int projectUserId, int status = 0) 
        {
            this.Id = Id;
            this.projectUserId = projectUserId;
            this.status = status;
        }

    }
}
