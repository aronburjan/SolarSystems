using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SolarSystems.Models
{
    public class Project
    {
        
        [ForeignKey("User")]
        public int UserId { get; set; }
        public int Id { get; set; } //row identification (unique)


        public int projectNumber { get; set; } //project identification (not unique)
        

        public int ComponentId { get; set; } //needed component Id

        public int neededComponentQuantity { get; set; }

        public int availableComponentQuantity { get; set; }

        public int status { get; set; }
        public virtual User User { get; set; }
        


    }
}
