using System.Collections.Generic;

namespace Telemetry
{
    public interface ITelemetrySender
    {
        void Send(string eventID);
        public void Send(string eventID, Dictionary<string, object> eventData);
    }
}