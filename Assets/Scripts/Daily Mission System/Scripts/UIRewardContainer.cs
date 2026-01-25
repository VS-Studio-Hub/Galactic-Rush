using UnityEngine;
using UnityEngine.UI;
using TMPro;


namespace Venthan.DailyMission
{
    public class UIRewardContainer : MonoBehaviour
    {
        [Header(" Elements ")]
        [SerializeField] private Image rewardImage;
        [SerializeField] private TextMeshProUGUI rewardLabel;

        public void Configure(Sprite icon, string label)
        {
            rewardImage.sprite = icon;
            rewardLabel.text = label;
        }
    }
}