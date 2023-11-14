using System.Collections.Generic;

namespace Telemetry
{
    public sealed class TelemetrySenderEmpty : ITelemetrySender
    {
        public void Send(string eventID)
        {
        }

        public void Send(string eventID, Dictionary<string, object> eventData)
        {
            throw new System.NotImplementedException();
        }
    }
}