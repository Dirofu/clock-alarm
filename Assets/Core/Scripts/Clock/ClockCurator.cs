using Core.Scripts.Alarm;
using Core.Scripts.Network.API;
using System.Collections;
using UnityEngine;

namespace Core.Scripts.Clock
{
    public class ClockCurator : MonoBehaviour
    {
        [SerializeField] private ClockView[] _clocks;
        [SerializeField] private TimeSynchronizer _synchronizer;
        [SerializeField] private AlarmCurator _alarm;

        private int _hours = 0;
        private int _minutes = 0;
        private int _seconds = 0;

        private Coroutine _calculateCoroutine = null;

        public int Hours => _hours;
        public int Minutes => _minutes;
        public int Seconds => _seconds;

        public const int HourLimitMin = 0;
        public const int HourLimitMax = 24;

        public const int MinuteLimitMin = 0;
        public const int MinuteLimitMax = 60;

        private void Start()
        {
            _synchronizer.SyncTimeWithServer();
        }

        public void StartCalculatingTime(TimeAPI time)
        {
            if (_calculateCoroutine != null)
                StopCoroutine(_calculateCoroutine);

            SetTimePermanently(time);

            _calculateCoroutine = StartCoroutine(CalculateTime(time));

            IEnumerator CalculateTime(TimeAPI time)
            {
                WaitForSeconds waitForSecond = new WaitForSeconds(1f);

                float milliSecondOut = 1f - time.milliSeconds;
                yield return new WaitForSeconds(milliSecondOut);

                while (true)
                {
                    IncreaseTime();
                    _alarm.AlarmTick();
                    UpdateView();
                    yield return waitForSecond;
                }
            }
        }

        private void SetTimePermanently(TimeAPI time)
        {
            _hours = time.hour;
            _minutes = time.minute;
            _seconds = time.seconds;

            UpdateView();
        }

        private void IncreaseTime()
        {
            _seconds++;

            if (CheckValueLimits(MinuteLimitMin, MinuteLimitMax, ref _seconds) == false)
                _minutes++;
            
            if (CheckValueLimits(MinuteLimitMin, MinuteLimitMax, ref _minutes) == false)
            {
                _hours++;
                _synchronizer.SyncTimeWithServer();
            }

            CheckValueLimits(HourLimitMin, HourLimitMax, ref _hours);
        }

        private void UpdateView()
        {
            foreach (var clock in _clocks)
                clock.SetTime(_hours, _minutes, _seconds);
        }

        public static bool CheckValueLimits(int min, int max, ref int value)
        {
            if (value >= max)
            {
                value = min;
                return false;
            }
            else if (value < min)
            {
                value = min;
                return false;
            }

            return true;
        }
    }
}