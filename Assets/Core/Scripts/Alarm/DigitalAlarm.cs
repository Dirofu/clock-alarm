using Core.Scripts.Clock;
using TMPro;
using UnityEngine;

namespace Core.Scripts.Alarm
{
    public class DigitalAlarm : AlarmBase
    {
        [SerializeField] private AlarmCurator _curator;
        [SerializeField] private TMP_InputField _hourInput;
        [SerializeField] private TMP_InputField _minuteInput;

        private void OnEnable()
        {
            _hourInput.onValueChanged.AddListener(SetValueInHourText);
            _minuteInput.onValueChanged.AddListener(SetValueInMinuteText);
        }

        private void OnDisable()
        {
            _hourInput.onValueChanged.RemoveListener(SetValueInHourText);
            _minuteInput.onValueChanged.RemoveListener(SetValueInMinuteText);
        }
            
        private void SetValueInHourText(string text) => SetValueInText(ref _hourInput, text, ClockCurator.HourLimitMin, ClockCurator.HourLimitMax, true);
        private void SetValueInMinuteText(string text) => SetValueInText(ref _minuteInput, text, ClockCurator.MinuteLimitMin, ClockCurator.MinuteLimitMax, false);

        private void SetValueInText(ref TMP_InputField field, string text, int min, int max, bool isHour)
        {
            if (CheckValueLimits(text, min, max, isHour) == true)
            {
                field.text = text;
            }
            else
            {
                field.text = string.Empty;
            }

            _curator.SetTime(Hours, Minutes);
        }

        private bool CheckValueLimits(string time, int min, int max, bool isHour)
        {
            if (int.TryParse(time, out int value) == true)
            {
                if (isHour == true)
                    Hours = value;
                else
                    Minutes = value;

                return ClockCurator.CheckValueLimits(min, max, ref value);
            }
            else
            {
                if (isHour == true)
                    Hours = 0;
                else
                    Minutes = 0;

                return false;
            }
        }
    }
}