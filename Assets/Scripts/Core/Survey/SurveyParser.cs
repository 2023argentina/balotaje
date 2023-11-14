using UnityEngine;

namespace Core.Survey
{
    public interface ISurveyDataProvider
    {
        public SurveyDTO SurveyData { get;}
    }

    public sealed class SurveyDataProvider : ISurveyDataProvider
    {
        public SurveyDTO SurveyData => JsonUtility.FromJson<SurveyDTO>(_jsonData);

        private readonly string _jsonData;
        
        public SurveyDataProvider(string jsonData)
        {
            _jsonData = jsonData;
        }
    }
}