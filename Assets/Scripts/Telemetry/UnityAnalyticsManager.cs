using System.Collections.Generic;
using Unity.Services.Analytics;

namespace Telemetry
{
    public sealed class UnityAnalyticsManager : ITelemetrySender
    {
        public UnityAnalyticsManager()
        {
            AnalyticsService.Instance.StartDataCollection();
        }
        
        public void Send(string eventID)
        {
            AnalyticsService.Instance.CustomData(eventID);
        }
        
        public void Send(string eventID, Dictionary<string, object> eventData)
        {
            AnalyticsService.Instance.CustomData(eventID, eventData);
        }
    }
}