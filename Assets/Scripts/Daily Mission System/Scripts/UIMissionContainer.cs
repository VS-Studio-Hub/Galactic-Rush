using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Venthan.DailyMission
{
    public class UIMissionContainer : MonoBehaviour
    {
        [Header(" Xp Section")]
        [SerializeField] private Image rewardImage;
        [SerializeField] private TextMeshProUGUI rewardText;

        [Header(" Slider Section ")]
        [SerializeField] private TextMeshProUGUI labelText;
        [SerializeField] private Slider progressSlider;
        [SerializeField] private TextMeshProUGUI progressText;

        [Header(" Button Section ")]
        [SerializeField] private GameObject inProgress;
        [SerializeField] private Button claimButton;
        [SerializeField] private GameObject checkIcon;

        private Mission mission;

        public void Configure (Mission mission, Action callback)
        {
            this.mission = mission;

            rewardImage.sprite = mission.Data.Icon;
            rewardText.text = "x" + mission.Data.RewardXp;
            labelText.text = GetMissionLabel(mission.Data);

            claimButton.onClick.RemoveAllListeners();
            claimButton.onClick.AddListener(() => callback?.Invoke());

            UpdateVisuals();
        }

        public void UpdateVisuals()
        {
            progressText.text = mission.ProgressString;
            progressSlider.value = mission.Progress;

            if (mission.Progress >= 1)
                Complete();
            else
                UnComplete();
        }

        private void Complete()
        {
            claimButton.gameObject.SetActive(true);
            inProgress.SetActive(false);
            checkIcon.SetActive(false);
        }

        private void UnComplete()
        {
            inProgress.SetActive(true);
            claimButton.gameObject.SetActive(false);
            checkIcon.SetActive(false);
        }

        public void Claim()
        {
            inProgress.SetActive(false);
            claimButton.gameObject.SetActive(false);
            checkIcon.SetActive(true);
        }

        private string GetMissionLabel(MissionData data)
        {
            switch (data.Type)
            {
                case EMissionType.Click:
                    return $"Click {data.Target} times";
                case EMissionType.PressUp:
                    return $"Press up {data.Target} times";
                case EMissionType.PressDown:
                    return $"Press down {data.Target} times";
                default:
                    Debug.LogError("No label for this mission type: " + data.Type);
                    return "";
            }
        }
    }
}