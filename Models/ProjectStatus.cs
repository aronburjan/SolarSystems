using System.ComponentModel.DataAnnotations.Schema;

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
        [ForeignKey("ProjectId")]
        public int ProjectId { get; set; }

        public virtual Project? Project { get; set; } //component id
    }
}
