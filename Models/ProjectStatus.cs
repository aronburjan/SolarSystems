namespace SolarSystems.Models
{
    public class ProjectStatus
    {
        public int Id { get; set; }
        public string DateTime { get; set; }
        public string status { get; set; }
        //  New
        //  Draft   
        //  Wait
        //  Scheduled
        //  InProgress
        //  Completed
        //  Failed
    }
}
