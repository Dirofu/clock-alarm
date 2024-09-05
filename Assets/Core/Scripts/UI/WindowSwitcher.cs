using UnityEngine;
using UnityEngine.UI;

namespace Core.Scripts.UI
{
    public class WindowSwitcher : MonoBehaviour
    {
        [SerializeField] private Button _timeButton;
        [SerializeField] private Button _alarmButton;

        [SerializeField] private GameObject _timeMenu;
        [SerializeField] private GameObject _alarmMenu;

        private void OnEnable()
        {
            _timeButton.onClick.AddListener(OpenTimeMenu);
            _alarmButton.onClick.AddListener(OpenAlarmMenu);
        }

        private void OnDisable()
        {
            _timeButton.onClick.RemoveListener(OpenTimeMenu);
            _alarmButton.onClick.RemoveListener(OpenAlarmMenu);
        }

        private void OpenTimeMenu()
        {
            _alarmMenu.SetActive(false);
            _timeMenu.SetActive(true);
        }

        private void OpenAlarmMenu()
        {
            _timeMenu.SetActive(false);
            _alarmMenu.SetActive(true);
        }
    }
}