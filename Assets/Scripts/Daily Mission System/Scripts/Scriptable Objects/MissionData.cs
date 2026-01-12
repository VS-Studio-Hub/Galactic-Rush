using UnityEngine;

namespace Venthan.DailyMission
{
    [CreateAssetMenu(fileName = "Mission Data", menuName = "Scriptable Object/Venthan/Daily Mission/Mission Data", order = -100000)]
    public class MissionData : ScriptableObject
    {
        [SerializeField] private EMissionType type;
        public EMissionType Type => type;

        [SerializeField] private int target;
        public int Target => target;

        [SerializeField] private int rewardXp;
        public int RewardXp => rewardXp;

        [SerializeField] private Sprite icon;
        public Sprite Icon => icon;
    }
}

