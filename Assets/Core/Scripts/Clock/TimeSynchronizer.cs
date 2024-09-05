using Core.Scripts.Network.API;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Core.Scripts.Clock
{
    public class TimeSynchronizer : MonoBehaviour
    {
        [SerializeField] private ClockCurator _clockCurator;

        private string[] _timeApis = new string[2] { 
            "https://www.timeapi.io/api/time/current/zone?timeZone=Europe%2FMoscow",
            "https://api.ipgeolocation.io/timezone?apiKey=d7ba1b4a455b47f389a31d104ec3c07b&tz=Europe/Moscow",
        };

        private int _currentApi = 0;

        public void SyncTimeWithServer()
        {
            StartCoroutine(SetCurrentTime(_timeApis[_currentApi]));
        }

        private IEnumerator SetCurrentTime(string api)
        {
            TimeAPI time = null;
            string url = api;
            int timeOut = 3;

            UnityWebRequest timeRequest = UnityWebRequest.Get(url);
            timeRequest.timeout = timeOut;

            yield return timeRequest.SendWebRequest();

            if (timeRequest.result == UnityWebRequest.Result.Success)
            {
                if (_currentApi == 0)
                {
                    time = JsonUtility.FromJson<TimeAPI>(timeRequest.downloadHandler.text);
                }
                else
                {
                    var unixApi = JsonUtility.FromJson<UnixAPI>(timeRequest.downloadHandler.text);
                    time = GetTimeAPIByUnixTime(unixApi.time_24);
                }
                
                _clockCurator.StartCalculatingTime(time);
            }
            else
            {
                _currentApi++;

                if (_currentApi >= _timeApis.Length)
                    _currentApi = 0;

                SyncTimeWithServer();
            }
        }

        private TimeAPI GetTimeAPIByUnixTime(string time)
        {
            TimeAPI api = new()
            {
                milliSeconds = 0,
            };

            api.hour = int.Parse(time.Substring(0, 2));
            api.minute = int.Parse(time.Substring(3, 2));
            api.seconds = int.Parse(time.Substring(6, 2));

            return api;
        }
    }
}