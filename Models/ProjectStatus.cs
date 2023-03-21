namespace SolarSystems.Models
{
    public class ProjectStatus
    {
        public int Id { get; set; }
        public string DateTime { get; set; }

        public int status { get; set; }
        public Project Project { get; set; }
    }
}
