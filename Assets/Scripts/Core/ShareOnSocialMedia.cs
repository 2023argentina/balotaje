using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Core
{
    public interface ISocialMediaShare
    {
        public void ShareResult();
    }
    
    public class ShareOnSocialMedia : MonoBehaviour, ISocialMediaShare
    {
        [SerializeField] private GameObject panelShare;

        [DllImport("__Internal")]
        private static extern void CopyToClipboardAndShare(string textToCopy);

        private const string shareText = "https://gonzaloescamilla.github.io/balotaje-2023/";
        
        [ContextMenu("Share")]
        public void ShareResult()
        {
            CopyToClipboardAndShare(shareText.Trim());
        }

        private IEnumerator TakeScreenShotAndShare()
        {
            yield return new WaitForEndOfFrame();
            Texture2D texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
            texture.ReadPixels(new Rect(0,0,Screen.width,Screen.height),0,0);
            texture.Apply();

            string path = Path.Combine(Application.temporaryCachePath, "sharedResult.png");
            File.WriteAllBytes(path, texture.EncodeToPNG());
            
            Destroy(texture);
        }
    }
}