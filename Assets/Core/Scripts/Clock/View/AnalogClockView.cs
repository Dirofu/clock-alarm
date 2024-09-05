using UnityEngine;

namespace Core.Scripts.Clock
{
    public class AnalogClockView : ClockView
    {
        [SerializeField] private RectTransform _hourArrow;
        [SerializeField] private RectTransform _minuteArrow;
        [SerializeField] private RectTransform _secondArrow;

        public const int OneMinuteDegree = 6;
        public const int OneHourDegree = 30;
        public const int ClockDegrees = 360;

        public override void SetTime(int hour, int minute, int second)
        {
            SetArrowRotation(second, OneMinuteDegree, ref _secondArrow);
            SetArrowRotation(minute, OneMinuteDegree, ref _minuteArrow);
            SetArrowRotation(hour, OneHourDegree, ref _hourArrow);

            _hourArrow.eulerAngles = new Vector3(0, 0, _hourArrow.eulerAngles.z + OneHourDegree * (GetRotationForArrow(minute, OneMinuteDegree) / ClockDegrees));
        }

        private void SetArrowRotation(float value, float degree, ref RectTransform arrow)
        {
            float currentArrowRotation = GetRotationForArrow(value, degree);
            arrow.eulerAngles = new Vector3(0, 0, currentArrowRotation);
        }

        private float GetRotationForArrow(float value, float degreesByUnit)
        {
            return value * degreesByUnit * -1;
        }
    }
}