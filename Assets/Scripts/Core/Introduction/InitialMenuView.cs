using System;
using Core.Menues;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Introduction
{
    public class InitialMenuView: GameMenuBase
    {
        [SerializeField] private Button startButton;
        
        public event Action StartMenuCompleted;

        public override void Initialize()
        {
            this.gameObject.SetActive(true);
        }
        
        public void Show()
        {
            startButton.onClick.AddListener(OnButtonPressed);
        }

        private void OnButtonPressed()
        {
            // TODO: Apply some type of delay
            gameObject.SetActive(false);
            StartMenuCompleted?.Invoke();
        }
    }
}