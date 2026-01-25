using UnityEngine;
using System.Collections.Generic;
using System;
using Coffee.UIExtensions;
using System.Collections;

namespace Venthan.DailyMission
{
    // Helper class to handle array serialization
    [Serializable]
    public class MissionSaveData
    {
        public int[] amounts;
        public bool[] claimedStates;
        public int xp;
    }

    [RequireComponent(typeof(UIMissionManager))]
    [RequireComponent(typeof(MissionTimer))]
    public class MissionManager : MonoBehaviour
    {
        public static MissionManager instance;

        [Header("Components")]
        private UIMissionManager ui;
        private MissionTimer timer;

        [Header("Data")]
        [SerializeField] private MissionData[] missionsDatas;
        List<Mission> activeMissions = new List<Mission>();

        private int xp;
        public int Xp => xp;

        [Header(" Actions ")]
        public static Action<int> xpUpdated;
        public static Action reset;

        [Header(" Effects ")]
        [SerializeField] private ParticleSystem diamondParticles;
        [SerializeField] private Transform particlesParent;
        private UIParticleAttractor diamondParticlesAttractor;

        [Header(" Save / Load Keys ")]
        private const string missionSaveKey = "MainMissionData";
        private bool shouldSave;

        void Awake()
        {
            if (instance == null) instance = this;
            else Destroy(gameObject);

            ui = GetComponent<UIMissionManager>();
            timer = GetComponent<MissionTimer>();

            Mission.updated += OnMissionUpdated;
            Mission.completed += OnMissionCompleted;
            MainMissionSlider.attractorInitialized += OnAttractorInitialized;

            StartCoroutine(SaveCoroutine());
        }

        private void OnDestroy()
        {
            Mission.updated -= OnMissionUpdated;
            Mission.completed -= OnMissionCompleted;
            MainMissionSlider.attractorInitialized -= OnAttractorInitialized;
        }

        private void OnAttractorInitialized(UIParticleAttractor attractor)
        {
            diamondParticlesAttractor = attractor;
            diamondParticlesAttractor.onAttracted.AddListener(OnDiamondParticleAttracted);
        }

        void Start()
        {
            Load();

            // Initialize missions with loaded data
            for (int i = 0; i < missionsDatas.Length; i++)
            {
                // Ensure arrays match mission data length
                int amt = (i < activeMissionsAmount.Length) ? activeMissionsAmount[i] : 0;
                bool claimed = (i < activeMissionsClaimed.Length) ? activeMissionsClaimed[i] : false;

                activeMissions.Add(new Mission(missionsDatas[i], amt, claimed));
            }

            ui.Init(activeMissions.ToArray());
            timer.Init(this);
        }

        // Temporary storage for Start() initialization logic
        private int[] activeMissionsAmount;
        private bool[] activeMissionsClaimed;

        void Update()
        {
            if (Input.GetMouseButtonDown(0)) Increment(EMissionType.Click, 1);
            else if (Input.GetKeyDown(KeyCode.UpArrow)) Increment(EMissionType.PressUp, 1);
            else if (Input.GetKeyDown(KeyCode.DownArrow)) Increment(EMissionType.PressDown, 1);
        }

        public void HandleMissionClaimed(int index)
        {
            activeMissions[index].Claim();
            int particleCount = activeMissions[index].Data.RewardXp;

            Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
            ParticleSystem diamondParticlesInstance = Instantiate(diamondParticles, screenCenter, Quaternion.identity, particlesParent);
            diamondParticlesAttractor.AddParticleSystem(diamondParticlesInstance);

            diamondParticlesInstance.emission.SetBurst(0, new ParticleSystem.Burst(0, particleCount));
            diamondParticlesInstance.Play();

            Save();
        }

        private void OnDiamondParticleAttracted()
        {
            xp++;
            xpUpdated?.Invoke(xp);
            shouldSave = true;
        }

        public void ResetMissions()
        {
            xp = 0;
            PlayerPrefs.DeleteKey(missionSaveKey);

            activeMissions.Clear();
            for (int i = 0; i < missionsDatas.Length; i++)
                activeMissions.Add(new Mission(missionsDatas[i]));

            ui.Init(activeMissions.ToArray());
            reset?.Invoke();
        }

        public static void Increment(EMissionType missionType, int addend)
        {
            bool changed = false;
            for (int i = 0; i < instance.activeMissions.Count; i++)
            {
                if (instance.activeMissions[i].IsComplete || instance.activeMissions[i].IsClaimed) continue;

                if (instance.activeMissions[i].Type == missionType)
                {
                    instance.activeMissions[i].Amount += addend;
                    changed = true;
                }
            }
            if (changed) instance.Save();
        }

        private void Load()
        {
            activeMissionsAmount = new int[missionsDatas.Length];
            activeMissionsClaimed = new bool[missionsDatas.Length];

            if (PlayerPrefs.HasKey(missionSaveKey))
            {
                string json = PlayerPrefs.GetString(missionSaveKey);
                MissionSaveData data = JsonUtility.FromJson<MissionSaveData>(json);

                activeMissionsAmount = data.amounts;
                activeMissionsClaimed = data.claimedStates;
                xp = data.xp;
            }
        }

        private void Save()
        {
            MissionSaveData data = new MissionSaveData();
            data.amounts = new int[activeMissions.Count];
            data.claimedStates = new bool[activeMissions.Count];
            data.xp = xp;

            for (int i = 0; i < activeMissions.Count; i++)
            {
                data.amounts[i] = activeMissions[i].Amount;
                data.claimedStates[i] = activeMissions[i].IsClaimed;
            }

            string json = JsonUtility.ToJson(data);
            PlayerPrefs.SetString(missionSaveKey, json);
            PlayerPrefs.Save();
        }

        IEnumerator SaveCoroutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(5);
                if (shouldSave)
                {
                    Save();
                    shouldSave = false;
                }
            }
        }

        private void OnMissionUpdated(Mission mission) => ui.UpdateMission(activeMissions.IndexOf(mission));
        private void OnMissionCompleted(Mission mission) { }
    }
}