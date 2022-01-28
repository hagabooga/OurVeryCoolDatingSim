using UnityEngine;
using UnityEngine.UI;

namespace CameronsWorld
{
    public partial class Spotlight
    {
        public class View : IView
        {
            Canvas RealWorld { get; }
            Canvas RedWorld { get; }
            Canvas LensEffects { get; }
            Image Lens { get; }

            public View(Canvas realWorld,
                        Canvas redWorld,
                        Canvas lensEffects,
                        Image lens)
            {
                RealWorld = realWorld;
                RedWorld = redWorld;
                LensEffects = lensEffects;
                Lens = lens;
            }

            public void ToggleRealWorldActive(bool yes)
            {
                RealWorld.gameObject.SetActive(yes);
            }

            public void ToggleRedWorldActive(bool yes)
            {
                RedWorld.gameObject.SetActive(yes);
            }

            public void ToggleLensEffectsActive(bool yes)
            {
                LensEffects.gameObject.SetActive(yes);
            }

            public void ToggleLensActive(bool yes)
            {
                Lens.gameObject.SetActive(yes);
            }

            public void SetLensPosition(Vector2 vector2)
            {
                Lens.rectTransform.anchoredPosition = vector2;
            }
        }
    }
}