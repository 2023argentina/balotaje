using System;
using Core.Introduction;
using UnityEngine;

namespace Core
{
    public class GameStateController : MonoBehaviour
    {
        private GameState _currentGameState;
        
        public void Initialize()
        {
            StartFSM();
        }
        
        private void Update()
        {
            if (_currentGameState == null)
            {
                return;
            }
            
            _currentGameState.Update();
        }

        private void StartFSM()
        {
            SwitchState<IntroductionState>();
        }

        public void SwitchState<T>() where T : GameState
        {
            Debug.Log($"GameState: Switching from {_currentGameState} to {typeof(T).Name}");
            
            _currentGameState = (T)Activator.CreateInstance(typeof(T), new object[1]{this});
            _currentGameState.Enter();
        }
    }
}