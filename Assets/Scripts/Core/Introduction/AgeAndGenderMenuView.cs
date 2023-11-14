using System;
using Core.Menues;
using UnityEngine;

namespace Core.Introduction
{
    public class AgeAndGenderMenuView: GameMenuBase
    {
        public event Action ActionCompleted;

        public override void Initialize()
        {
        }
        
        public void Show()
        {
            Debug.Log("Gender Menu Shown");
            
            ActionCompletedByUser();
        }

        private void ActionCompletedByUser()
        {
            Debug.LogWarning("here");
            ActionCompleted?.Invoke();
        }
    }
}