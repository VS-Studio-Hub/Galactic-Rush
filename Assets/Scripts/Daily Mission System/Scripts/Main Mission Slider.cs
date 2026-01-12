using System;
using UnityEngine;
using UnityEngine.UI;
using Coffee.UIExtensions;
using System.Collections;
using TMPro;

namespace Venthan.DailyMission
{
    public class MainMissionSlider : MonoBehaviour
    {

        [Header(" Elements ")]
        [SerializeField] private Slider slider;
        [SerializeField] private UISliderItem itemPrefab;
        [SerializeField] private UISliderItem diamondItemPrefab;
        [SerializeField] private RectTransform itemsParent;
        private TextMeshProUGUI xpText;

        [Header(" Settings ")]
        [SerializeField] private Sprite currencyIcon;

        [Header(" Data ")]
        [SerializeField] private RewardGroupData data;
        private int lastRewardIndex;

        [Header(" Actions ")]
        public static Action<UIParticleAttractor> attractorInitialized;

        private void Awake()
        {
            MissionManager.xpUpdated += OnXpUpdated;
        }

        private void OnDestroy()
        {
            MissionManager.xpUpdated -= OnXpUpdated;
        }

        private void OnXpUpdated(int xp)
        {
            slider.value = xp;
            xpText.text = xp.ToString();

            CheckForRewards();
        }

        IEnumerator Start()
        {
            yield return null;

            lastRewardIndex = 0;
            Init();
        }


        void Update()
        {

        }
        private void Init()
        {
            GenerateSliderItems();
            InitSlider();
        }

        private void GenerateSliderItems()
        {
            itemsParent.Clear();

            UISliderItem diamondItem = Instantiate(diamondItemPrefab, itemsParent);
            diamondItem.Configure(currencyIcon, 0.ToString());

            xpText = diamondItem.Text;

            attractorInitialized?.Invoke(diamondItem.GetComponent<UIParticleAttractor>());

            for (int i = 0; i < data.RewardMilestoneDatas.Length; i++)
            {
                RewardMilestoneData milestoneData = data.RewardMilestoneDatas[i];

                UISliderItem itemInstance = Instantiate(itemPrefab, itemsParent);
                itemInstance.Configure(milestoneData.icon, milestoneData.requiredXP.ToString());

                int _i = i;
                itemInstance.Button.onClick.AddListener(() => HandleSliderItemPressed(_i));
            }

            PlaceItems();
        }

        private void HandleSliderItemPressed(int index)
        {
            Debug.Log($"Slider item {index} was pressed.");
        }
        private void PlaceItems()
        {
            float width = itemsParent.rect.width;
            float spacing = width / (itemsParent.childCount - 1);

            Vector2 startPosition = (Vector2)itemsParent.position - Vector2.right * width / 2;

            for(int i = 0; i < itemsParent.childCount; i++)
                itemsParent.GetChild(i).position = startPosition + spacing * i * Vector2.right;
        }

        private void InitSlider()
        {
            slider.minValue = 0;
            slider.maxValue = data.RewardMilestoneDatas[data.RewardMilestoneDatas.Length - 1].requiredXP;

            slider.value = 0;
        }

        private void CheckForRewards()
        {
            if(lastRewardIndex > data.RewardMilestoneDatas.Length - 1)
                return;

            if (slider.value >= data.RewardMilestoneDatas[lastRewardIndex].requiredXP)
                EnableReward();
        }

        private void EnableReward()
        {
            UISliderItem item = itemsParent.GetChild(lastRewardIndex + 1).GetComponent<UISliderItem>();
            //item.Animate();

            lastRewardIndex++;
        }
    }
}

