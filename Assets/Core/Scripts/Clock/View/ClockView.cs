using UnityEngine;

namespace Core.Scripts.Clock
{
    public abstract class ClockView : MonoBehaviour
    {
        public abstract void SetTime(int hour, int minute, int second);
    }
}