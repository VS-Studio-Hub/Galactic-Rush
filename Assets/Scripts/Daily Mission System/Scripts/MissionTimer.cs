using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

namespace Venthan.DailyMission
{
    [Serializable]
    public class WorldTimeResponse
    {
        public string utc_datetime;
    }

    public class MissionTimer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI timerText;

        private MissionManager missionManager;

        private const string startTimeKey = "DailyMissionStartTime";
        private const string endTimeKey = "DailyMissionEndTime";

        private DateTime startTime;
        private DateTime endTime;

        private DateTime trustedNetworkTime;
        private float timeAtLastNetworkCheck;

        private DateTime CurrentRealTime => trustedNetworkTime.AddSeconds(Time.realtimeSinceStartup - timeAtLastNetworkCheck);
        private TimeSpan TimeLeft => endTime - CurrentRealTime;

        public void Init(MissionManager manager)
        {
            missionManager = manager;
            LoadTimes();
            StartCoroutine(GetNetworkTime());
        }

        private IEnumerator GetNetworkTime()
        {
            using (UnityWebRequest req = UnityWebRequest.Get("https://worldtimeapi.org/api/timezone/Etc/UTC"))
            {
                yield return req.SendWebRequest();

                if (req.result == UnityWebRequest.Result.Success)
                {
                    var res = JsonUtility.FromJson<WorldTimeResponse>(req.downloadHandler.text);
                    trustedNetworkTime = DateTime.Parse(res.utc_datetime, null, System.Globalization.DateTimeStyles.RoundtripKind);
                }
                else
                {
                    trustedNetworkTime = DateTime.UtcNow;
                }

                timeAtLastNetworkCheck = Time.realtimeSinceStartup;
                FinishInit();
            }
        }

        private void FinishInit()
        {
            if (startTime == DateTime.MinValue || CurrentRealTime >= endTime)
                StartNewDay(CurrentRealTime);
            else
                StartCoroutine(TimerLoop());
        }

        private void StartNewDay(DateTime now)
        {
            startTime = now;
            endTime = now.Date.AddDays(1);
            SaveTimes();
            StartCoroutine(TimerLoop());
        }

        private IEnumerator TimerLoop()
        {
            while (true)
            {
                if (CurrentRealTime >= endTime)
                {
                    missionManager.ResetMissions();
                    StartNewDay(CurrentRealTime);
                    yield break;
                }

                timerText.text = FormatTime(TimeLeft);
                yield return new WaitForSeconds(1);
            }
        }

        private void SaveTimes()
        {
            PlayerPrefs.SetString(startTimeKey, startTime.ToString("o"));
            PlayerPrefs.SetString(endTimeKey, endTime.ToString("o"));
            PlayerPrefs.Save();
        }

        private void LoadTimes()
        {
            if (PlayerPrefs.HasKey(startTimeKey))
                DateTime.TryParse(PlayerPrefs.GetString(startTimeKey), null, System.Globalization.DateTimeStyles.RoundtripKind, out startTime);

            if (PlayerPrefs.HasKey(endTimeKey))
                DateTime.TryParse(PlayerPrefs.GetString(endTimeKey), null, System.Globalization.DateTimeStyles.RoundtripKind, out endTime);
        }

        private string FormatTime(TimeSpan t)
        {
            if (t.TotalSeconds <= 0) return "00:00:00";
            return $"{(int)t.TotalHours:D2}:{t.Minutes:D2}:{t.Seconds:D2}";
        }
    }
}
