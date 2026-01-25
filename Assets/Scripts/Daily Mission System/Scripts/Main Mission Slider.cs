using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Coffee.UIExtensions;
using Venthan.ModernUISystem;

namespace Venthan.DailyMission
{

    public class MainMissionSlider : MonoBehaviour
    {
        [Header(" Elements ")]
        [SerializeField] private Slider slider;
        [SerializeField] private UISliderItem itemPrefab;
        [SerializeField] private UISliderItem diamondItemPrefab;
        [SerializeField] private RectTransform itemsParent;
        [SerializeField] private UIRewardPopup rewardPopup;
        [SerializeField] private Sprite currencyIcon;

        private TextMeshProUGUI xpText;
        private List<UISliderItem> sliderItems = new List<UISliderItem>();

        [Header(" Data ")]
        [SerializeField] private RewardGroupData data;

        private int lastRewardIndex;
        private bool[] itemsOpened;

        private const string sliderSaveKey = "DailyMissionSlider";

        [Header(" Actions ")]
        public static Action<UIParticleAttractor> attractorInitialized;
        public static Action<RewardEntryData[]> rewarded;

        private void Awake()
        {
            MissionManager.xpUpdated += OnXpUpdated;
            MissionManager.reset += ResetSelf;
        }

        private void OnDestroy()
        {
            MissionManager.xpUpdated -= OnXpUpdated;
            MissionManager.reset -= ResetSelf;
        }

        IEnumerator Start()
        {
            yield return null;

            Load();
            Init();
        }

        private void Init()
        {
            GenerateSliderItems();
            InitSlider();
            UpdateVisuals(MissionManager.instance.Xp);
            CheckForRewards();
        }

        private void GenerateSliderItems()
        {
            itemsParent.Clear();
            sliderItems.Clear();

            // XP display item
            UISliderItem diamondItem = Instantiate(diamondItemPrefab, itemsParent);
            diamondItem.Configure(currencyIcon, "0");
            xpText = diamondItem.Text;

            attractorInitialized?.Invoke(diamondItem.GetComponent<UIParticleAttractor>());

            // Reward milestones
            for (int i = 0; i < data.RewardMilestoneDatas.Length; i++)
            {
                RewardMilestoneData milestone = data.RewardMilestoneDatas[i];
                UISliderItem item = Instantiate(itemPrefab, itemsParent);
                item.Configure(milestone.icon, milestone.requiredXP.ToString());

                int index = i;
                item.Button.onClick.AddListener(() => HandleItemPressed(index));

                sliderItems.Add(item);
            }

            PlaceItems();
        }

        private void HandleItemPressed(int index)
        {
            if (index >= lastRewardIndex) return;
            if (itemsOpened[index]) return;

            OpenReward(index);
        }

        private void OpenReward(int index)
        {
            itemsOpened[index] = true;

            UIRewardPopup popup = PopupManager.Show(rewardPopup);
            popup.Configure(data.RewardMilestoneDatas[index].rewards);

            Save();
            rewarded?.Invoke(data.RewardMilestoneDatas[index].rewards);
        }

        private void PlaceItems()
        {
            float width = itemsParent.rect.width;
            float spacing = width / (itemsParent.childCount - 1);
            Vector2 start = (Vector2)itemsParent.position - Vector2.right * width / 2f;

            for (int i = 0; i < itemsParent.childCount; i++)
                itemsParent.GetChild(i).position = start + spacing * i * Vector2.right;
        }

        private void InitSlider()
        {
            slider.minValue = 0;
            slider.maxValue = data.RewardMilestoneDatas[data.RewardMilestoneDatas.Length - 1].requiredXP;
            slider.value = 0;
        }

        private void OnXpUpdated(int xp)
        {
            UpdateVisuals(xp);
            CheckForRewards();
        }

        private void UpdateVisuals(int xp)
        {
            slider.value = xp;
            xpText.text = xp.ToString();
        }

        private void CheckForRewards()
        {
            while (lastRewardIndex < data.RewardMilestoneDatas.Length &&
                   slider.value >= data.RewardMilestoneDatas[lastRewardIndex].requiredXP)
            {
                lastRewardIndex++;
            }

            Save();
        }

        private void Load()
        {
            if (!PlayerPrefs.HasKey(sliderSaveKey))
            {
                lastRewardIndex = 0;
                itemsOpened = new bool[data.RewardMilestoneDatas.Length];
                return;
            }

            string json = PlayerPrefs.GetString(sliderSaveKey);
            SliderSaveData save = JsonUtility.FromJson<SliderSaveData>(json);

            lastRewardIndex = save.lastRewardIndex;
            itemsOpened = save.itemsOpened;

            // Safety if data size changed
            if (itemsOpened == null || itemsOpened.Length != data.RewardMilestoneDatas.Length)
                itemsOpened = new bool[data.RewardMilestoneDatas.Length];
        }

        private void Save()
        {
            SliderSaveData save = new SliderSaveData();
            save.lastRewardIndex = lastRewardIndex;
            save.itemsOpened = itemsOpened;

            PlayerPrefs.SetString(sliderSaveKey, JsonUtility.ToJson(save));
            PlayerPrefs.Save();
        }

        private void ResetSelf()
        {
            PlayerPrefs.DeleteKey(sliderSaveKey);
            PlayerPrefs.Save();

            lastRewardIndex = 0;
            itemsOpened = new bool[data.RewardMilestoneDatas.Length];

            Init();
        }
    }
}
