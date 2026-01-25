using Venthan.ModernUISystem;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace Venthan.DailyMission
{
    public class UIRewardPopup : UIPopupBase
    {
        [Header(" Animation ")]
        [SerializeField] private Image background;
        [SerializeField] private RectTransform mainContainer;
        [SerializeField] private float animationDuration;
        //[SerializeField] private LeanTweenType openEase;

        [Header(" Reward Elements ")]
        [SerializeField] private Button closeButton;

        [Header(" Settings ")]
        [SerializeField] private UIRewardContainer rewardContainerPrefab;
        [SerializeField] private Transform rewardContainersParent;
        [SerializeField] private Sprite placeholderIcon;

        [Header(" Settings ")]
        private bool isClosing;

        // Start is called before the first frame update
        void Start()
        {
            closeButton.onClick.RemoveAllListeners();
            closeButton.onClick.AddListener(Close);

            Open();
        }
        

        public void Configure(RewardEntryData[] rewards)
        {
            for (int i = 0; i < rewards.Length; i++)
            {
                RewardEntryData data = rewards[i];

                UIRewardContainer containerInstance = Instantiate(rewardContainerPrefab, rewardContainersParent);
                containerInstance.Configure(placeholderIcon, data.amount.ToString());
            }
        }


        private void Open()
        {
            //float backgroundTargetAlpha = background.color.a;
            //background.color = Color.clear;

            //LeanTween.alpha(background.rectTransform,  backgroundTargetAlpha, animationDuration).setRecursive(false);

            mainContainer.localScale = Vector3.zero;
            //LeanTween.scale(mainContainer, Vector3.one, animationDuration).setEase(openEase);
        }

        public void Close()
        {
            if (isClosing)
                return;

            isClosing = true;

            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            closed?.Invoke(this);
        }
    }

}