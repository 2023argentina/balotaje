using TMPro;
using UnityEngine;

namespace Core.Menues
{
    public interface IRootCanvasProvider
    {
        public Canvas RootCanvas { get; }
        public TextMeshProUGUI HeaderText { get; }
    }
}