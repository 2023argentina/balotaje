using System;
using System.Collections.Generic;

namespace Core
{
    public static class Shared
    {
        public static class SurveyData
        {
            public static float MileiWeight;
            public static float MassaWeight;
            public static int AmountOfQuestionsAnswered;
        }

        public class TelemetryEvent
        {
            public string EventName;
            public Dictionary<string, object> EventParameters;
        }

        public class AmountOfQuestionsAnswered : TelemetryEvent
        {
            public AmountOfQuestionsAnswered(int amount)
            {
                EventName = "amountOfQuestionsAnswered";
                EventParameters = new Dictionary<string, object>() {{"intAmount", amount}};
            }
        }
        
        public class SurveyCandidateResult : TelemetryEvent
        {
            public SurveyCandidateResult(string name)
            {
                EventName = "surveyCandidateResult";
                EventParameters = new Dictionary<string, object>() {{"candidateName", name}};
            }
        }
        
        public class SurveyCompleted : TelemetryEvent
        {
            public SurveyCompleted()
            {
                EventName = "surveyCompleted";
                EventParameters = new ();
            }
        }
        
        public class SurveyRestarted : TelemetryEvent
        {
            public SurveyRestarted()
            {
                EventName = "surveyRestarted";
                EventParameters = new ();
            }
        }
        
        public class AnswerTheyLeft : TelemetryEvent
        {
            public AnswerTheyLeft(int answerID)
            {
                EventName = "answerTheyLeft";
                EventParameters = new (){{"answerID", answerID}};
            }
        }
    }
}