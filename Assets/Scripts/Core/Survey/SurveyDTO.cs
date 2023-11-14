using System;
using System.Collections.Generic;

namespace Core.Survey
{
    [Serializable]
    public class AnswersDTO
    {
        public string answerText;
        public float MileiPercentage;
        public float MassaPercetage;
    }

    [Serializable]
    public class QuestionsDTO
    {
        public int questionID;
        public string questionText;
        public List<AnswersDTO> answers;
    }

    [Serializable]
    public class SurveyDTO
    {
        public List<QuestionsDTO> questions;
    }
}