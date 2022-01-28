using UnityEngine;

namespace CameronsWorld
{
    public partial class Spotlight
    {
        public interface IView
        {
            void ToggleLensActive(bool yes);
            void ToggleLensEffectsActive(bool yes);
            void ToggleRealWorldActive(bool yes);
            void ToggleRedWorldActive(bool yes);
            void SetLensPosition(Vector2 vector2);
        }
    }
}