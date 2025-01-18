using Assets.Src.Code.Controllers;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Src.Code.UI
{
    public class LoadSceneUi : MonoBehaviour
    {
        [SerializeField] private DataController _data;
        [SerializeField] private Transform _loadSceneUi;
        [SerializeField] private Transform _mainUi;
        [SerializeField] private Slider _slider;
        private Tween _loadTween;

        private void Start()
        {
            _loadSceneUi.gameObject.SetActive(true);

            _data.OnLoadDataHandler += SetTweens;
            _data.Load();
            _loadTween = _slider.DOValue(0.6f, 2f).SetEase(Ease.InQuart);
        }

        private void SetTweens()
        {
            _loadTween.Kill();

            _slider.DOValue(1, 0.5f).SetEase(Ease.Linear)
                .OnComplete(() => OnCompleteLogic());
        }

        private void OnCompleteLogic()
        {
            _data.OnLoadDataHandler -= SetTweens;
            _loadSceneUi.gameObject.SetActive(false);
            _mainUi.gameObject.SetActive(true);
        }
    }
}