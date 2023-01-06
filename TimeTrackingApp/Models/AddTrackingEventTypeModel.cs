using System.ComponentModel.DataAnnotations;

namespace TimeTrackingApp.Models
{
    public class AddTrackingEventTypeModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage ="Shouldn't be empty!")]
        public string EventType { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Description { get; set; }
    }
}