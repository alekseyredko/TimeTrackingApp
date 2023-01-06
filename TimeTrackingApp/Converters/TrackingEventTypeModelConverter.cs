using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Text.Json;
using TimeTrackingApp.Domain.Entities;
using TimeTrackingApp.Models;

namespace TimeTrackingApp.Converters
{
    public class TrackingEventTypeModelConverter: TypeConverter
    {
        public override object? CreateInstance(ITypeDescriptorContext? context, IDictionary propertyValues)
        {
            return base.CreateInstance(context, propertyValues);
        }

        public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
        {
            if (sourceType == typeof(string)) 
            {
                return true;
            }

            if (sourceType == typeof(TrackingEventTypeModel))
            {
                return true;
            }

            return base.CanConvertFrom(context, sourceType);
        }

        public override bool CanConvertTo(ITypeDescriptorContext? context, Type? destinationType)
        {
            if (destinationType == typeof(TrackingEventTypeModel))
            {
                return true;
            }

            if (destinationType == typeof(string))
            {
                return true;
            }

            return base.CanConvertTo(context, destinationType);
        }

        public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
        {
            if (value is string)
            {
                return JsonSerializer.Deserialize<TrackingEventTypeModel>(value as string);
            }

            if (value is TrackingEventTypeModel)
            {
                return JsonSerializer.Serialize(value as TrackingEventTypeModel);
            }

            return base.ConvertFrom(context, culture, value);
        }

        public override object? ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                TrackingEventTypeModel trackingEventType = (TrackingEventTypeModel)value;
                return JsonSerializer.Serialize(trackingEventType);
            }

            if (destinationType == typeof(TrackingEventTypeModel))
            {
                return JsonSerializer.Deserialize<TrackingEventTypeModel>(value as string);
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
