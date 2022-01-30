using UnityEngine;

namespace CameronsWorld
{
    public partial class Spotlight
    {
        public interface IView
        {
            float VignetteIntensity { get; set; }

            void ToggleLensActive(bool yes);
            void ToggleLensImage(bool yes);
            void ToggleLensEffectsActive(bool yes);
            void ToggleRealWorldActive(bool yes);
            void ToggleRedWorldActive(bool yes);
            void ToggleRedWorldRaycast(bool yes);

            void SetLensGlobalPosition(Vector2 vector2);
            void SetRedWorldPosition(Vector3 vector3);
            void SetRedWorldAlpha(float alpha);


            Vector2 RedWorldGlobalPosition { get; }
        }
    }
}