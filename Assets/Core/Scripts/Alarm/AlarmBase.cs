using UnityEngine;

namespace Core.Scripts.Alarm
{
    public class AlarmBase : MonoBehaviour
    {
        public int Hours { get; protected set; }
        public int Minutes { get; protected set; }
    }
}