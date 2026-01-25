using UnityEngine;
using System;

namespace Venthan.DailyMission
{
    [Serializable]
    public class Mission
    {
        public static Action<Mission> updated;
        public static Action<Mission> completed;

        private MissionData data;
        public MissionData Data => data;

        private bool isComplete;
        public bool IsComplete => isComplete;

        private bool isClaimed;
        public bool IsClaimed => isClaimed;

        private int amount;
        public int Amount
        {
            get => amount;
            set
            {
                amount = Mathf.Min(value, data.Target);
                updated?.Invoke(this);

                if (!isComplete && amount >= data.Target)
                    Complete();
            }
        }

        public float Progress => (float)amount / data.Target;
        public string ProgressString => amount + " / " + data.Target;
        public EMissionType Type => data.Type;

        public Mission(MissionData data)
        {
            this.data = data;
            amount = 0;
            isComplete = false;
            isClaimed = false;
        }

        // Rebuild from save
        public Mission(MissionData data, int amount, bool claimed)
        {
            this.data = data;
            this.amount = Mathf.Min(amount, data.Target);

            if (this.amount >= data.Target)
                isComplete = true;

            isClaimed = claimed;
        }

        private void Complete()
        {
            isComplete = true;
            completed?.Invoke(this);
        }

        public void Claim()
        {
            if (!isComplete) return;
            isClaimed = true;
        }
    }
}
