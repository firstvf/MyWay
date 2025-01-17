using Assets.Src.Code.Controllers;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Src.Code.UI
{
    public class MainUi : MonoBehaviour
    {
        [SerializeField] private Transform _mainUi;
        [SerializeField] private Button _updateButton;
        [SerializeField] private Button _increaseScoreButton;
        [SerializeField] private Text _score;
        [SerializeField] private Text _welcomeMessage;

        private void Awake()
        {
            _updateButton.onClick.AddListener(UpdateBundle);
            _increaseScoreButton.onClick.AddListener(IncreaseScore);
        }

        public void OpenMainUi()
        {
            _welcomeMessage.text = DataController.Instance.WelcomeMessage.Message;
            _score.text = DataController.Instance.Settings.Score.ToString();
            _mainUi.gameObject.SetActive(true);
        }

        private void UpdateBundle()
        => DataController.Instance.Save();

        private void IncreaseScore()
        {
            DataController.Instance.Settings.Score++;
            _score.text = DataController.Instance.Settings.Score.ToString();
        }

        private void OnDestroy()
        {
            _updateButton.onClick.RemoveListener(UpdateBundle);
            _increaseScoreButton.onClick.RemoveListener(IncreaseScore);
        }
    }
}