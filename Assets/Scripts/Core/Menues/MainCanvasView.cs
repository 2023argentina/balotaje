using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace Core.Menues
{
    public class MainCanvasView : MonoBehaviour, IRootCanvasProvider
    {
        [SerializeField] private Canvas rootCanvas;
        [SerializeField] private CanvasScaler canvasScaler;
        [SerializeField] private TextMeshProUGUI headerText;
        
        public Canvas RootCanvas => rootCanvas;
        public TextMeshProUGUI HeaderText => headerText;

        private void Awake()
        {
            if (WebGLMobileHelper.IsRunningOnMobile())
            {
                canvasScaler.referenceResolution = new Vector2(1080, 1920);
                headerText.fontSize = 90;
            }
        }
    }
}