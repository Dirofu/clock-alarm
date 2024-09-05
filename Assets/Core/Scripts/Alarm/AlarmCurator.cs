using Core.Scripts.Clock;
using System;
using UnityEngine;

namespace Core.Scripts.Alarm
{
    public class AlarmCurator : MonoBehaviour
    {
        [SerializeField] private ClockCurator _clock;

        public int Hours { get; protected set; } = -1;
        public int Minutes { get; protected set; } = -1;

        public event Action AlarmTime = delegate { };

        public void SetTime(int hours, int minutes)
        {
            Hours = hours; 
            Minutes = minutes;
        }

        public void AlarmTick()
        {
            if (CheckAlarmTime() == true)
            {
                AlarmTime.Invoke();
                Hours = -1; 
                Minutes = -1;
            }
        }

        protected bool CheckAlarmTime() => _clock.Hours == Hours && _clock.Minutes == Minutes;
    }
}