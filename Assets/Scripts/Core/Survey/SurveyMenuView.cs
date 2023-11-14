using System;
using System.Linq;
using Core.Menues;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utilities;
using Random = UnityEngine.Random;

namespace Core.Survey
{
    [Serializable]
    public struct SurveyButtonTextPair
    {
        public Button ButtonAnswer;
        public TextMeshProUGUI TextAnswer;
    }
    
    [Serializable]
    public struct SurveyUILayout
    {
        public SurveyButtonTextPair[] SurveyButtonTextPairs;
    }
    
    public class SurveyMenuView : GameMenuBase
    {
        [SerializeField] private TextMeshProUGUI textQuestion;
        [SerializeField] private GameObject buttonsContainerTwo;
        [SerializeField] private SurveyUILayout surveyUILayoutTwo;
        [SerializeField] private GameObject buttonsContainerFour;
        [SerializeField] private SurveyUILayout surveyUILayoutFour;

        private IRootCanvasProvider _rootCanvasProvider;
        
        public event Action SurveyCompleted;
        private SurveyDTO _surveyDto;

        private int _amountOfAllSteps;
        private int _currentStepIndex;
        
        private enum SurveyType
        {
            TwoAnswers,
            FourAnswers
        }

        private SurveyType _currentSurveyType;
        
        public override void Initialize()
        {
            this.gameObject.SetActive(true);
            _amountOfAllSteps = 0;
            _currentStepIndex = 0;
            
            _surveyDto = ServiceLocator.Instance.GetService<ISurveyDataProvider>().SurveyData;
            _rootCanvasProvider = ServiceLocator.Instance.GetService<IRootCanvasProvider>();
            
            _amountOfAllSteps = _surveyDto.questions.Count;
            
            _surveyDto.questions = _surveyDto.questions.OrderBy(t => Random.Range(0,100)).ToList();
            
            ShowCurrentStep();
        }

        private void ShowCurrentStep()
        {
            SetCurrentStepType();
            ShuffleAnswers();
            SubscribeToButtonsEvent();
            DisplayTexts();
        }

        private void SetCurrentStepType()
        {
            int amountOfAnswersCurrentStep = _surveyDto.questions[_currentStepIndex].answers.Count; 
            if (amountOfAnswersCurrentStep == 2)
            {
                _currentSurveyType = SurveyType.TwoAnswers;
            }
            else if(amountOfAnswersCurrentStep == 4)
            {
                _currentSurveyType = SurveyType.FourAnswers;
            }
        }
        
        private void ShuffleAnswers()
        {
            _surveyDto.questions[_currentStepIndex].answers = 
                _surveyDto.questions[_currentStepIndex].answers.OrderBy(t => Random.Range(0, 100)).ToList();
        }

        private void SubscribeToButtonsEvent()
        {
            surveyUILayoutTwo.SurveyButtonTextPairs[0].ButtonAnswer.onClick.RemoveAllListeners();
            surveyUILayoutTwo.SurveyButtonTextPairs[1].ButtonAnswer.onClick.RemoveAllListeners();

            surveyUILayoutFour.SurveyButtonTextPairs[0].ButtonAnswer.onClick.RemoveAllListeners();
            surveyUILayoutFour.SurveyButtonTextPairs[1].ButtonAnswer.onClick.RemoveAllListeners();
            surveyUILayoutFour.SurveyButtonTextPairs[2].ButtonAnswer.onClick.RemoveAllListeners();
            surveyUILayoutFour.SurveyButtonTextPairs[3].ButtonAnswer.onClick.RemoveAllListeners();
            
            switch (_currentSurveyType)
            {
                case SurveyType.TwoAnswers:
                    surveyUILayoutTwo.SurveyButtonTextPairs[0].ButtonAnswer.onClick.AddListener(() => OnAnswerButtonPressed(0));
                    surveyUILayoutTwo.SurveyButtonTextPairs[1].ButtonAnswer.onClick.AddListener(() => OnAnswerButtonPressed(1));
                    break;
                case SurveyType.FourAnswers:
                    surveyUILayoutFour.SurveyButtonTextPairs[0].ButtonAnswer.onClick.AddListener(() => OnAnswerButtonPressed(0));
                    surveyUILayoutFour.SurveyButtonTextPairs[1].ButtonAnswer.onClick.AddListener(() => OnAnswerButtonPressed(1));
                    surveyUILayoutFour.SurveyButtonTextPairs[2].ButtonAnswer.onClick.AddListener(() => OnAnswerButtonPressed(2));
                    surveyUILayoutFour.SurveyButtonTextPairs[3].ButtonAnswer.onClick.AddListener(() => OnAnswerButtonPressed(3));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        private void DisplayTexts()
        {
            textQuestion.text = _surveyDto.questions[_currentStepIndex].questionText;
            _rootCanvasProvider.HeaderText.text = $"Pregunta {_currentStepIndex + 1} de {_amountOfAllSteps}";
            
            Debug.Log($"Question ID: {_surveyDto.questions[_currentStepIndex].questionID}");

            
            switch (_currentSurveyType)
            {
                case SurveyType.TwoAnswers:
                    buttonsContainerTwo.SetActive(true);
                    buttonsContainerFour.SetActive(false);
                    
                    surveyUILayoutTwo.SurveyButtonTextPairs[0].TextAnswer.text =
                        _surveyDto.questions[_currentStepIndex].answers[0].answerText;
                    
                    surveyUILayoutTwo.SurveyButtonTextPairs[1].TextAnswer.text =
                        _surveyDto.questions[_currentStepIndex].answers[1].answerText;
                    break;
                case SurveyType.FourAnswers:
                    buttonsContainerTwo.SetActive(false);
                    buttonsContainerFour.SetActive(true);
                    
                    surveyUILayoutFour.SurveyButtonTextPairs[0].TextAnswer.text =
                        _surveyDto.questions[_currentStepIndex].answers[0].answerText;
                    
                    surveyUILayoutFour.SurveyButtonTextPairs[1].TextAnswer.text =
                        _surveyDto.questions[_currentStepIndex].answers[1].answerText;
                    
                    surveyUILayoutFour.SurveyButtonTextPairs[2].TextAnswer.text =
                        _surveyDto.questions[_currentStepIndex].answers[2].answerText;
                    
                    surveyUILayoutFour.SurveyButtonTextPairs[3].TextAnswer.text =
                        _surveyDto.questions[_currentStepIndex].answers[3].answerText;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        private void OnAnswerButtonPressed(int buttonIndex)
        {
            switch (_currentSurveyType)
            {
                case SurveyType.TwoAnswers:
                    surveyUILayoutTwo.SurveyButtonTextPairs[buttonIndex].ButtonAnswer
                        .GetComponent<ButtonAudioHelper>().PlaySound();
                    break;
                case SurveyType.FourAnswers:
                    surveyUILayoutFour.SurveyButtonTextPairs[buttonIndex].ButtonAnswer
                        .GetComponent<ButtonAudioHelper>().PlaySound();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            Shared.SurveyData.MassaWeight += _surveyDto.questions[_currentStepIndex].answers[buttonIndex].MassaPercetage;
            Shared.SurveyData.MileiWeight += _surveyDto.questions[_currentStepIndex].answers[buttonIndex].MileiPercentage;
            Shared.SurveyData.AmountOfQuestionsAnswered++;
            
            _currentStepIndex++;

            if (_currentStepIndex >= _amountOfAllSteps)
            {
                EndSurvey();

                return;
            }
            
            ShowCurrentStep();
        }

        private void EndSurvey()
        {
            this.gameObject.SetActive(false);
            SurveyCompletedInternal();
        }

        private void SurveyCompletedInternal()
        {
            Debug.LogWarning($"Massa weight is: {Shared.SurveyData.MassaWeight}");
            Debug.LogWarning($"Milei weight is: {Shared.SurveyData.MileiWeight}");
            SurveyCompleted?.Invoke();
        }
    }
}