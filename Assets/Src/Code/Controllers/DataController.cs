using Assets.Src.Code.Data;
using Assets.Src.Code.Data.Jsons;
using System;
using System.IO;
using UnityEngine;

namespace Assets.Src.Code.Controllers
{
    public class DataController : MonoBehaviour
    {
        public static DataController Instance { get; private set; }
        public Settings Settings { get; set; }
        public WelcomeMessage WelcomeMessage { get; private set; }
        public Action OnLoadDataHandler { get; set; }
        public Sprite SpriteBundle { get; private set; }

        private IDataService _dataService;
        private IAssetBundleLoader _assetBundleLoader;
        private readonly string _settingsKey = "Settings";
        private readonly string _welcomeMessageKey = "WelcomeMessage";
        private readonly string _welcomeMessageUrl = "https://raw.githubusercontent.com/firstvf/MyWay/refs/heads/main/Assets/StreamingAssets/WelcomeMessage.json";
        private readonly string _assetBundleUrl = "https://github.com/firstvf/MyWay/raw/refs/heads/main/AssetsBundles/background";

        private int _totalTask;
        private int _completedTask;
        private bool _isAllTasksCompleted => _completedTask == _totalTask;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                _dataService = new JsonToFileService();
                _assetBundleLoader = new AssetBundleLoader();
                return;
            }

            Destroy(gameObject);
        }

        public void Load()
        {
            _totalTask = 3;
            _completedTask = 0;

            _dataService.Load<Settings>(_settingsKey, data =>
            {
                Settings = new()
                {
                    Score = data.Score
                };
                CallLoadDataHandler();
            });

            StartCoroutine(_dataService.LoadUrl<WelcomeMessage>(_welcomeMessageKey, _welcomeMessageUrl, data =>
            {
                WelcomeMessage = new()
                {
                    Message = data.Message
                };
                CallLoadDataHandler();
            }));

            StartCoroutine(_assetBundleLoader.LoadUrl<Sprite>("background", _assetBundleUrl, "Background_sprite", data =>
            {
                SpriteBundle = data;
                CallLoadDataHandler();
            }));
        }

        private void CallLoadDataHandler()
        {
            _completedTask++;

            if (_isAllTasksCompleted)
                OnLoadDataHandler?.Invoke();
        }

        private void OnApplicationQuit()
        {
            _dataService.Save(_settingsKey, Settings);
        }
    }
}