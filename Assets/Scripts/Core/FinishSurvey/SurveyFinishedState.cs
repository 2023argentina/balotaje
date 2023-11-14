using Core.Introduction;
using Core.Menues;
using Telemetry;
using Utilities;

namespace Core.FinishSurvey
{
    public sealed class SurveyFinishedState : GameState
    {
        private IRootCanvasProvider _rootCanvasProvider;
        private FinishSurveyMenuView _finishSurveyMenu;
        
        private ISocialMediaShare _mediaShare;
        private ITelemetrySender _telemetrySender;
        
        public SurveyFinishedState(GameStateController controller) : base(controller)
        {
            _rootCanvasProvider = ServiceLocator.Instance.GetService<IRootCanvasProvider>();
            _finishSurveyMenu = ServiceLocator.Instance.GetService<IMenuInstanceProvider>()
                .GetMenuInstance<FinishSurveyMenuView>();

            _telemetrySender = ServiceLocator.Instance.GetService<ITelemetrySender>();
            
            _mediaShare = ServiceLocator.Instance.GetService<ISocialMediaShare>();
        }
        
        public override void Enter()
        {
            _rootCanvasProvider.HeaderText.text = "Balotaje 2023";
            _finishSurveyMenu.RestartButtonClicked += OnRestartButtonClicked;
            _finishSurveyMenu.ShareButtonClicked += OnShareButtonClicked;
            _finishSurveyMenu.ShowResult();

            Shared.AmountOfQuestionsAnswered amountOfQuestionsAnsweredEvent =
                new Shared.AmountOfQuestionsAnswered(Shared.SurveyData.AmountOfQuestionsAnswered);
            _telemetrySender.Send(amountOfQuestionsAnsweredEvent.EventName, amountOfQuestionsAnsweredEvent.EventParameters);

            Shared.SurveyCompleted surveyCompletedEvent = new Shared.SurveyCompleted();
            _telemetrySender.Send(surveyCompletedEvent.EventName);
        }

        private void OnRestartButtonClicked()
        {
            _finishSurveyMenu.RestartButtonClicked -= OnRestartButtonClicked;

            Shared.SurveyRestarted surveyRestartedEvent = new Shared.SurveyRestarted();
            _telemetrySender.Send(surveyRestartedEvent.EventName);
            
            _controller.SwitchState<IntroductionState>();
        }
        
        private void OnShareButtonClicked()
        {
            _finishSurveyMenu.ShareButtonClicked -= OnShareButtonClicked;

            _mediaShare.ShareResult();
            _controller.SwitchState<IntroductionState>();
        }
        
        public override void Update()
        {
        }

        protected override void Exit()
        {
            // Exits the application if possible.
        }
    }
}