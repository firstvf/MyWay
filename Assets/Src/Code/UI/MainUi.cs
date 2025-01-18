using Assets.Src.Code.Controllers;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Src.Code.UI
{
    public class MainUi : MonoBehaviour
    {
        [SerializeField] private DataController _data;
        [SerializeField] private Transform _mainUi;
        [SerializeField] private Button _updateButton;
        [SerializeField] private Button _increaseScoreButton;
        [SerializeField] private Text _score;
        [SerializeField] private Text _welcomeMessage;

        private bool _isRequireToUseCachedScore;
        private int _cachedScore;

        private void Awake()
        {
            _updateButton.onClick.AddListener(UpdateBundle);
            _increaseScoreButton.onClick.AddListener(IncreaseScore);
        }

        private void Start()
        {
            _data.OnLoadDataHandler += RefreshUi;
        }

        private void RefreshUi()
        {
            _welcomeMessage.text = _data.WelcomeMessage.Message;
            _score.text = _data.Settings.Score.ToString();
            _increaseScoreButton.GetComponent<Image>().sprite = _data.SpriteBundle;
            _updateButton.interactable = true;
            _isRequireToUseCachedScore = false;
        }

        private void UpdateBundle()
        {
            _updateButton.interactable = false;
            _cachedScore = _data.Settings.Score;
            _isRequireToUseCachedScore = true;
            _data.Load();
        }

        private void IncreaseScore()
        {
            if (!_isRequireToUseCachedScore)
            {
                _data.Settings.Score++;
                _score.text = _data.Settings.Score.ToString();
            }
            else
            {
                _cachedScore++;
                _score.text = _cachedScore.ToString();
            }
        }

        private void OnDestroy()
        {
            _data.OnLoadDataHandler -= RefreshUi;
            _updateButton.onClick.RemoveListener(UpdateBundle);
            _increaseScoreButton.onClick.RemoveListener(IncreaseScore);
        }
    }
}