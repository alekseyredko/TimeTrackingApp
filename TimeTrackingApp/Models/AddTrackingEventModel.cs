using System.ComponentModel.DataAnnotations;
using TimeTrackingApp.Domain.Entities;

namespace TimeTrackingApp.Models
{
    public class AddTrackingEventModel
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        public TrackingEventTypeModel[] EventTypes { get; set; }
    }
}
