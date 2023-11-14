using UnityEngine;

namespace Utilities
{
    public class UnityLogAdapter : ILog
    {
        public void Log(string message)
        {
            Debug.Log(message);
        }
    }
}