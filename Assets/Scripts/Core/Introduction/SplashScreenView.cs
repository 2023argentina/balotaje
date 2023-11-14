using System;
using Core.Menues;
using UnityEngine;

namespace Core.Introduction
{
    public class SplashScreenView: GameMenuBase
    {
        public event Action SplashScreenVisualizationCompleted;

        public override void Initialize()
        {
        }
        
        public void Show()
        {
            Debug.Log("Splash Screen Shown");
            
            FinishSplashScreen();
        }

        private void FinishSplashScreen()
        {
            SplashScreenVisualizationCompleted?.Invoke();
        }
    }
}