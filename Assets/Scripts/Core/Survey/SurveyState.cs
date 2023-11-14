using Core.FinishSurvey;
using Core.Menues;
using Utilities;

namespace Core.Survey
{
    public sealed class SurveyState : GameState
    {
        private SurveyMenuView _surveyMenuView;
        
        public SurveyState(GameStateController controller) : base(controller)
        {
            _surveyMenuView = ServiceLocator.Instance.GetService<IMenuInstanceProvider>()
                .GetMenuInstance<SurveyMenuView>();
        }
        
        public override void Enter()
        {
            Shared.SurveyData.MassaWeight = 0;
            Shared.SurveyData.MileiWeight = 0;
            Shared.SurveyData.AmountOfQuestionsAnswered = 0;
            
            _surveyMenuView.SurveyCompleted += OnSurveyCompleted;
            _surveyMenuView.Initialize();
        }

        private void OnSurveyCompleted()
        {
            _surveyMenuView.SurveyCompleted -= OnSurveyCompleted;
            Exit();
        }

        public override void Update()
        {
        }

        protected override void Exit()
        {
            _controller.SwitchState<SurveyFinishedState>();
        }
    }
}