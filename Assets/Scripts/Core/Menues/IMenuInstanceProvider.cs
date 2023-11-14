using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Core.FinishSurvey;
using Core.Introduction;
using Core.Survey;
using UnityEngine;
using Utilities;

namespace Core.Menues
{
    public interface IMenuInstanceProvider
    {
        T GetMenuInstance<T>() where T : GameMenuBase;
    }

    public class MenuInstanceFactory : IMenuInstanceProvider
    {
        private string _defaultMenuName = "DefaultMenu";
        private string _splashMenuPrefabName = "SplashScreenMenu";
        private string _initialMenuPrefabName = "InitialMenu";
        private string _surveyMenuPrefabName = "SurveyMenu";
        private string _ageGenderMenuPrefabName = "GenderAndAgeMenu";
        private string _finishSurveyMenuPrefabName = "FinishSurveyMenu";

        private Dictionary<string, GameMenuBase> _catchedMenues = new();

        private readonly Dictionary<Type, string> _menuTypes;

        public MenuInstanceFactory()
        {
            if (WebGLMobileHelper.IsRunningOnMobile())
            {
                _defaultMenuName = "DefaultMenu"; //TODO: When menu created replace with mobile
                _splashMenuPrefabName = "SplashScreenMenu"; //TODO: When menu created replace with mobile
                _initialMenuPrefabName = "InitialMenuMobile";
                _surveyMenuPrefabName = "SurveyMenuMobile"; //TODO: When menu created replace with mobile
                _ageGenderMenuPrefabName = "GenderAndAgeMenu"; //TODO: When menu created replace with mobile
                _finishSurveyMenuPrefabName = "FinishSurveyMenuMobile"; //TODO: When menu created replace with mobile
            }
            
            _menuTypes = new ()
            {
                {typeof(SplashScreenView), _splashMenuPrefabName},
                {typeof(InitialMenuView), _initialMenuPrefabName},
                {typeof(SurveyMenuView), _surveyMenuPrefabName},
                {typeof(AgeAndGenderMenuView), _ageGenderMenuPrefabName},
                {typeof(FinishSurveyMenuView), _finishSurveyMenuPrefabName}
            };
        }
        
        public T GetMenuInstance<T>() where T : GameMenuBase
        {
            string prefabName = _defaultMenuName;
            if (_menuTypes.ContainsKey(typeof(T)))
            {
                _menuTypes.TryGetValue(typeof(T), out prefabName);
            }
            
            if (_catchedMenues.ContainsKey(typeof(T).ToString()))
            {
                _catchedMenues.TryGetValue(typeof(T).ToString(), out var menu);
                menu.Initialize();
                return menu as T;
            }
            
            var menuPrefab = Resources.Load<GameObject>(prefabName);
            if (menuPrefab == null)
            {
                Debug.LogError("The SplashScreen prefab failed to load, please make sure it exist in the right path");
                return null;
            }

            var rootCanvasProvider = ServiceLocator.Instance.GetService<IRootCanvasProvider>();

            GameMenuBase menuBaseInstance;
            
            var splashScreenInstance = GameObject.Instantiate(menuPrefab, rootCanvasProvider.RootCanvas.transform);
            menuBaseInstance = splashScreenInstance.GetComponent<GameMenuBase>();
            
            _catchedMenues.Add(menuBaseInstance.GetType().ToString(), menuBaseInstance);
            
            menuBaseInstance.Initialize();
            return menuBaseInstance as T;
        }
    }
}