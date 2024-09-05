using Core.Scripts.Clock;
using UnityEngine;

namespace Core.Scripts.Utils
{
    [RequireComponent(typeof(TimeSynchronizer))]
    public class FocusFixer : MonoBehaviour
    {
        private TimeSynchronizer _synchronizer;

        private void Awake()
        {
            _synchronizer = GetComponent<TimeSynchronizer>();
        }

        private void OnApplicationFocus(bool focus)
        {
            if (focus == true)
            {
                _synchronizer.SyncTimeWithServer();
            }
        }
    }
}