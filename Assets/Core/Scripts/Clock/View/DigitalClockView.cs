using TMPro;
using UnityEngine;

namespace Core.Scripts.Clock
{
    public class DigitalClockView : ClockView
    {
        [SerializeField] private TMP_Text _time;

        public override void SetTime(int hour, int minute, int second)
        {
            _time.text = $"{FormatValueToString(hour)}:{FormatValueToString(minute)}:{FormatValueToString(second)}";
        }

        private string FormatValueToString(int value) => value < 10 ? $"0{value}" : value.ToString();
    }
}
