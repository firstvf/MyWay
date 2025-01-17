using Assets.Src.Code.Data;
using Assets.Src.Code.Data.Jsons;
using System;
using UnityEngine;

namespace Assets.Src.Code.Controllers
{
    public class DataController : MonoBehaviour
    {
        public static DataController Instance { get; private set; }
        public Settings Settings { get; set; }
        public WelcomeMessage WelcomeMessage { get;private set; }
        public Action OnLoadDataHandler { get; set; }

        private IDataService _dataService;
        private readonly string _settingsKey = "Settings";
        private readonly string _welcomeMessageKey = "WelcomeMessage";

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                _dataService = new JsonToFileService();
                Settings = new();
                WelcomeMessage = new();
                return;
            }

            Destroy(gameObject);
        }

        public void Load()
        {
            _dataService.Load<Settings>(_settingsKey, data =>
            {
                Settings.Score = data.Score;
            });

            _dataService.Load<WelcomeMessage>(_welcomeMessageKey, data =>
            {
                WelcomeMessage.Message = data.Message;
                OnLoadDataHandler?.Invoke();
            });
        }

        public void Save()
        => _dataService.Save(_settingsKey, Settings);
    }
}