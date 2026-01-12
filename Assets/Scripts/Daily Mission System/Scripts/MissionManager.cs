using UnityEngine;
using System.Collections.Generic;
using System;
using Coffee.UIExtensions;

namespace Venthan.DailyMission
{
    [RequireComponent(typeof(UIMissionManager))]
    public class MissionManager : MonoBehaviour
    {
        public static MissionManager instance;

        [Header("Components")]
        private UIMissionManager ui;

        [Header("Data")]
        [SerializeField] private MissionData[] missionsDatas;
        List<Mission> activeMissions = new List<Mission>();


        private int xp;
        public int Xp => xp;

        [Header(" Actions ")]
        public static Action<int> xpUpdated;

        [Header(" Effects ")]
        [SerializeField] private ParticleSystem diamondParticles;
        [SerializeField] private Transform particlesParent;
        private UIParticleAttractor diamondParticlesAttractor;


        void Awake()
        {
            if (instance == null)
                instance = this;
            else
                Destroy(gameObject);

            ui = GetComponent<UIMissionManager>();

            Mission.updated += OnMissionUpdated;
            Mission.completed += OnMissionCompleted;


            MainMissionSlider.attractorInitialized += OnAttractorInitialized;
        }

        private void OnDestroy()
        {
            Mission.updated -= OnMissionUpdated;
            Mission.completed -= OnMissionCompleted;

            MainMissionSlider.attractorInitialized += OnAttractorInitialized;
        }

        private void OnAttractorInitialized(UIParticleAttractor attractor)
        {
            diamondParticlesAttractor = attractor;
            diamondParticlesAttractor.onAttracted.AddListener(OnDiamondParticleAttracted);
        }

        void Start()
        {
            for(int i = 0; i < missionsDatas.Length; i++)
                activeMissions.Add(new Mission(missionsDatas[i]));
            
            ui.Init(activeMissions.ToArray());
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
                Increment(EMissionType.Click, 1);

            else if(Input.GetKeyDown(KeyCode.UpArrow))
                Increment(EMissionType.PressUp, 1);

            else if(Input.GetKeyDown(KeyCode.DownArrow))
                Increment(EMissionType.PressDown, 1);
        }

        private void OnMissionUpdated(Mission mission)
        {
            ui.UpdateMission(activeMissions.IndexOf(mission));
        }

        private void OnMissionCompleted(Mission mission)
        {

        }

        public void HandleMissionClaimed(int index)
        {
            Mission mission = activeMissions[index];
            mission.Claim();

            int particleCount = mission.Data.RewardXp;

            Vector2 screenCenter = new Vector2(Screen.width /  2, Screen.height / 2);
            ParticleSystem diamondParticlesInstance = Instantiate(diamondParticles, screenCenter, Quaternion.identity, particlesParent);

            diamondParticlesAttractor.AddParticleSystem(diamondParticlesInstance);

            diamondParticlesInstance.emission.SetBurst(0, new ParticleSystem.Burst(0, particleCount));
            diamondParticlesInstance.Play();

        }


        private void OnDiamondParticleAttracted()
        {
            xp++;
            xpUpdated?.Invoke(xp);
        }

        public static void Increment(EMissionType missionType, int addend)
        {
            for(int i = 0; i < instance.activeMissions.Count; i++)
            {
                if (instance.activeMissions[i].IsComplete || instance.activeMissions[i].IsClaimed)
                    continue;
             

                if (instance.activeMissions[i].Type == missionType)
                    instance.activeMissions[i].Amount += addend;
            }
        }
    }
}