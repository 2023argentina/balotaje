using UnityEngine;

namespace Core.Menues
{
    public abstract class GameMenuBase: MonoBehaviour
    {
        /// <summary>
        /// Initialize is always executed when retrieved from menu provider.
        /// </summary>
        public abstract void Initialize();
    }
}