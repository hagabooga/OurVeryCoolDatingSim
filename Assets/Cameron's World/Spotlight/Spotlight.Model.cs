using System;

namespace CameronsWorld
{
    public partial class Spotlight
    {
        public class Model
        {
            public event Action OnUseChanged;

            public int Uses { get; private set; }
            public float VignetteIntensity { get; }
            public float TransitionDuration { get; }

            public Model(int uses, float vignetteIntensity, float transitionDuration)
            {
                Uses = uses;
                VignetteIntensity = vignetteIntensity;
                TransitionDuration = transitionDuration;
            }

            public void AddUse(int amount = 1)
            {
                Uses++;
            }

            public void RemoveUse(int amount = 1)
            {
                Uses++;
            }
        }
    }
}