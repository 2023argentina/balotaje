using UnityEngine;

namespace Utilities
{
    public static class WebGLMobileHelper
    {
        public static bool IsRunningOnMobile() =>
            Application.platform == RuntimePlatform.WebGLPlayer && Application.isMobilePlatform;
    }
}