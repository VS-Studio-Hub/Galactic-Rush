using UnityEngine;

namespace Venthan.DailyMission
{
    [CreateAssetMenu(fileName = "Reward Group Data", menuName = "Scriptable Object/Venthan/Daily Missions/Reward Group Data", order = -100000)]
    public class RewardGroupData : ScriptableObject
    {
        [Header("Data")]
        [SerializeField] private RewardMilestoneData[] rewardMilestoneDatas;
        public RewardMilestoneData[] RewardMilestoneDatas => rewardMilestoneDatas;
    }

    [System.Serializable]
    public struct RewardMilestoneData
    {
        public Sprite icon;
        public RewardEntryData[] rewards;
        public int requiredXP;
    }

    [System.Serializable]
    public struct RewardEntryData
    {
        public ERewardType type;
        public int amount;
    }
}