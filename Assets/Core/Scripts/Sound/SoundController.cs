using Core.Scripts.Alarm;
using UnityEngine;

namespace Core.Scripts.Sound
{
    public class SoundController : MonoBehaviour
    {
        [SerializeField] private AudioSource _source;
        [SerializeField] private AlarmCurator _alarm;

        private void OnEnable()
        {
            _alarm.AlarmTime += PlayAlarmSound;
        }

        private void OnDisable()
        {
            _alarm.AlarmTime -= PlayAlarmSound;
        }

        private void PlayAlarmSound() => _source.Play();
    }
}