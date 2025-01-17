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
        public WelcomeMessage WelcomeMessage { get; private set; }
        public Action OnLoadDataHandler { get; set; }

        private IDataService _dataService;
        private readonly string _settingsKey = "Settings";
        private readonly string _welcomeMessageKey = "WelcomeMessage";
        private readonly string _welcomeMessageUrl = "https://raw.githubusercontent.com/firstvf/MyWay/refs/heads/main/Assets/StreamingAssets/WelcomeMessage.json";

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                _dataService = new JsonToFileService();
                return;
            }

            Destroy(gameObject);
        }

        public void Load()
        {
            _dataService.Load<Settings>(_settingsKey, data =>
            {
                Settings = new()
                {
                    Score = data.Score
                };
            });

            StartCoroutine(_dataService.LoadUrl<WelcomeMessage>(_welcomeMessageKey, _welcomeMessageUrl, data =>
            {
                WelcomeMessage = new()
                {
                    Message = data.Message
                };
                OnLoadDataHandler?.Invoke();
            }));
        }

        public void Save()
        => _dataService.Save(_settingsKey, Settings);
    }
}