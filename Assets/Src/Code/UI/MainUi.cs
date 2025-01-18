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

        private void Start()
        {
            DataController.Instance.OnLoadDataHandler += RefreshUi;
        }

        private void RefreshUi()
        {
            _welcomeMessage.text = DataController.Instance.WelcomeMessage.Message;
            _score.text = DataController.Instance.Settings.Score.ToString();
            _increaseScoreButton.GetComponent<Image>().sprite = DataController.Instance.SpriteBundle;            
            _updateButton.interactable = true;
        }

        private void UpdateBundle()
        {
            _updateButton.interactable = false;
            DataController.Instance.Load();
        }

        private void IncreaseScore()
        {
            DataController.Instance.Settings.Score++;
            _score.text = DataController.Instance.Settings.Score.ToString();
        }

        private void OnDestroy()
        {
            DataController.Instance.OnLoadDataHandler -= RefreshUi;
            _updateButton.onClick.RemoveListener(UpdateBundle);
            _increaseScoreButton.onClick.RemoveListener(IncreaseScore);
        }
    }
}