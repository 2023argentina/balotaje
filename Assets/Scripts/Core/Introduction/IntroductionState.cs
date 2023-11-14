using Core.Menues;
using Core.Survey;
using UnityEngine;
using Utilities;

namespace Core.Introduction
{
    public sealed class IntroductionState : GameState
    {
        private InitialMenuView _initialMenuView;
        
        public IntroductionState(GameStateController controller) : base(controller)
        {
            Debug.Log("Introduction State");
            
            _initialMenuView = ServiceLocator.Instance.GetService<IMenuInstanceProvider>().GetMenuInstance<InitialMenuView>();

            if (!_initialMenuView)
            {
                Debug.LogError("The SplashScreen prefab does not contain SplashScreenController component. " +
                               "Make sure that you include it.");
            }
        }
        
        public override void Enter()
        {
            _initialMenuView.StartMenuCompleted += OnStartMenuCompleted;
            _initialMenuView.Show();
        }

        private void OnStartMenuCompleted()
        {
            _initialMenuView.StartMenuCompleted -= OnStartMenuCompleted;
            Exit();
        }
        
        public override void Update()
        {
            // Note: Does nothing.
        }

        protected override void Exit()
        {
            _controller.SwitchState<SurveyState>();
        }
    }
}