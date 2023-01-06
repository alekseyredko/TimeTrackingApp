using System.ComponentModel;
using System.Text.Json;
using TimeTrackingApp.Converters;

namespace TimeTrackingApp.Models
{
    [TypeConverter(typeof(TrackingEventTypeModelConverter))]
    public class TrackingEventTypeModel
    {
        public Guid Id { get; set; }
        public string EventType { get; set; }
        public string? Description { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);            
        }

        public static TrackingEventTypeModel FromJson(string json)
        {
            return JsonSerializer.Deserialize<TrackingEventTypeModel>(json);
        }
    }
}
