using System;
using UnityEngine;

namespace CameronsWorld
{
    [CreateAssetMenu(fileName = "Model", menuName = "Cameron's World/Spotlight.Model.So", order = 1)]
    public class SpotlightModel : ScriptableObject
    {
        [SerializeField] int startingUses;
        [SerializeField] float vignetteIntensity, transitionDuration;

        public int StartingUses { get => startingUses; }
        public float VignetteIntensity { get => vignetteIntensity; }
        public float TransitionDuration { get => transitionDuration; }

        public int Uses { get; private set; }

        public event Action OnUseChanged;

        public void AddUse(int amount = 1)
        {
            Uses -= amount;
            OnUseChanged.Invoke();
        }

        public void RemoveUse(int amount = 1)
        {
            Uses -= amount;
            OnUseChanged.Invoke();
        }
    }
}