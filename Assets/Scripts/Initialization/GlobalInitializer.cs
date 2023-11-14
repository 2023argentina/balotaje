using System;
using Core;
using Core.Audio;
using Core.Menues;
using Core.Survey;
using Cysharp.Threading.Tasks;
using Login;
using Telemetry;
using Unity.Services.Core;
using Unity.Services.Core.Environments;
using UnityEngine;
using Utilities;

namespace Initialization
{
    public class GlobalInitializer : MonoBehaviour
    {
        [SerializeField] private GameStateController gameStateController; 
        [SerializeField] private MainCanvasView mainCanvasView;
        [SerializeField] private AudioManager audioManager;    
        [SerializeField] private ShareOnSocialMedia shareOnSocialMedia;    
        [SerializeField] private TextAsset jsonData;
        
        private const string UGS_ENVIRONMENT_NAME = "development"; 
        
        private void Awake()
        {
            Initialize();
        }
    
        private async UniTaskVoid Initialize()
        {
            await InitializeUGS();
            InitializeServices();
            
            await UniTask.Delay(TimeSpan.FromSeconds(1));
            
            LogIn();
            gameStateController.Initialize();
        }
    
        private async UniTask<string> InitializeUGS()
        {
            try
            {
                var options = new InitializationOptions().SetEnvironmentName(UGS_ENVIRONMENT_NAME);
                await UnityServices.InitializeAsync(options);
                Debug.Log("Unity Game Services Initialized");
            }
            catch (Exception exception)
            {
                // Note: An error occurred during initialization.
                throw new Exception(exception.Message);
            }
    
            return null;
        }
        
        private void InitializeServices()
        {
            Debug.Log("Initialize other services");
            
            ITelemetrySender telemetrySender = new UnityAnalyticsManager();
            ServiceLocator.Instance.RegisterService<ITelemetrySender>(telemetrySender);
    
            ILoginService loginService = new LogInAnonymousController();
            ServiceLocator.Instance.RegisterService<ILoginService>(loginService);

            ISurveyDataProvider surveyParser = new SurveyDataProvider(jsonData.text);
            ServiceLocator.Instance.RegisterService<ISurveyDataProvider>(surveyParser);
            
            ServiceLocator.Instance.RegisterService<IRootCanvasProvider>(mainCanvasView);
            
            ServiceLocator.Instance.RegisterService<IMenuInstanceProvider>(new MenuInstanceFactory());
            
            ServiceLocator.Instance.RegisterService<IGameAudioPlayer>(audioManager);
            ServiceLocator.Instance.RegisterService<ISocialMediaShare>(shareOnSocialMedia);
        }
        
        private void LogIn()
        {
            ServiceLocator.Instance.GetService<ILoginService>().LogIn();
        }
    }
}

