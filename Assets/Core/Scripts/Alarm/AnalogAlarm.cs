using Core.Scripts.Arrow;
using Core.Scripts.Clock;
using UnityEngine;

namespace Core.Scripts.Alarm
{
    public class AnalogAlarm : AlarmBase
    {
        [SerializeField] private AlarmCurator _curator;
        [SerializeField] private ArrowMover _hoursArrow;
        [SerializeField] private ArrowMover _minutesArrow;

        private void OnEnable()
        {
            _hoursArrow.Rotate += SetTime;
            _minutesArrow.Rotate += SetTime;
        }

        private void OnDisable()
        {
            _hoursArrow.Rotate -= SetTime;
            _minutesArrow.Rotate -= SetTime;
        }

        public void SetTime(float rotate, ArrowType type)
        {
            switch (type)
            {
                case ArrowType.Hours:
                    Hours = (int)GetTimeFromRotation(rotate, AnalogClockView.OneHourDegree);
                    break;
                case ArrowType.Minutes:
                    Minutes = (int)GetTimeFromRotation(rotate, AnalogClockView.OneMinuteDegree);
                    break;
            }

            _curator.SetTime(Hours, Minutes);
        }

        private float GetTimeFromRotation(float rotation, float degreesByUnit)
        {
            return (rotation / degreesByUnit) * -1;
        }
    }
}