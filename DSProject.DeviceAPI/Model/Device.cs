using System.ComponentModel.DataAnnotations;

namespace DSProject.DeviceAPI.Model
{
    public class Device
    {
        [Key]
        public Guid Id { get; set; }  
        public string Description { get; set; } 
        public string Address { get; set; }
        public double MaximumHourlyEnergyConsumption { get; set; }
        public Guid? UserId { get; set; }
    }
}
