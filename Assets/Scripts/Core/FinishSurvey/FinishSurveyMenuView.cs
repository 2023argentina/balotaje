﻿using System;
using Core.Menues;
using Telemetry;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace Core.FinishSurvey
{
    public class FinishSurveyMenuView : GameMenuBase
    {
        [SerializeField] private Button restartButton;
        [SerializeField] private Button shareButton;
        [SerializeField] private Image backgroundMilei;
        [SerializeField] private Image backgroundMassa;
        [SerializeField] private TextMeshProUGUI percentageText;

        public event Action RestartButtonClicked;
        public event Action ShareButtonClicked;

        private ITelemetrySender _telemetrySender;
        
        public override void Initialize()
        {
            this.gameObject.SetActive(true);

            restartButton.onClick.AddListener(OnRestartButtonClicked);
            shareButton.onClick.AddListener(OnSharedButtonClicked);

            _telemetrySender = ServiceLocator.Instance.GetService<ITelemetrySender>();
        }

        private void OnRestartButtonClicked()
        {
            this.gameObject.SetActive(false);

            RestartButtonClicked?.Invoke();
        }
        
        private void OnSharedButtonClicked()
        {
            this.gameObject.SetActive(false);

            ShareButtonClicked?.Invoke();
        }

        public void ShowResult()
        {
            if (Shared.SurveyData.MassaWeight >= Shared.SurveyData.MileiWeight)
            {
                Shared.SurveyCandidateResult resultEventMassa = new Shared.SurveyCandidateResult("Massa");
                _telemetrySender.Send(resultEventMassa.EventName, resultEventMassa.EventParameters);
                
                backgroundMassa.gameObject.SetActive(true);
                backgroundMilei.gameObject.SetActive(false);

                int resultPercentageMassa = (int)((Shared.SurveyData.MassaWeight * 100) / 19); //TODO: Replace magic number by amount of questions
                percentageText.text = $"(Coincidencias: {resultPercentageMassa}%)";
                
                return;
            }
            
            int resultPercentageMilei = (int)((Shared.SurveyData.MileiWeight * 100) / 19); //TODO: Replace magic number by amount of questions
            percentageText.text = $"(Coincidencias: {resultPercentageMilei}%)";
            
            Shared.SurveyCandidateResult resultEventMilei = new Shared.SurveyCandidateResult("Milei");
            _telemetrySender.Send(resultEventMilei.EventName, resultEventMilei.EventParameters);
            
            backgroundMassa.gameObject.SetActive(false);
            backgroundMilei.gameObject.SetActive(true);
        }

        private void OnDestroy()
        {
            restartButton.onClick.RemoveAllListeners();
            shareButton.onClick.RemoveAllListeners();
        }
    }
}