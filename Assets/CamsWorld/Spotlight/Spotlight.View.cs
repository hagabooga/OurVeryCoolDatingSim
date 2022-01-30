using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

namespace CameronsWorld
{
    public partial class Spotlight
    {
        public class View : IView
        {
            Canvas RealWorld { get; }
            CanvasGroup RedWorld { get; }
            Canvas LensEffects { get; }
            Image Lens { get; }
            Volume VolumeProfile { get; }
            Vignette Vignette { get; }

            public View(Canvas realWorld,
                        CanvasGroup redWorld,
                        Canvas lensEffects,
                        Image lens,
                        Volume volumeProfile)
            {
                RealWorld = realWorld;
                RedWorld = redWorld;
                LensEffects = lensEffects;
                Lens = lens;
                VolumeProfile = volumeProfile;

                if (VolumeProfile.profile.TryGet<Vignette>(out Vignette vignette))
                {
                    Vignette = vignette;
                }
                else
                {
                    Debug.Log("Did not find Vignette override!!");
                }
            }

            public Vector2 RedWorldGlobalPosition => RedWorld.transform.position;

            public float VignetteIntensity
            {
                get => Vignette.intensity.value;
                set => Vignette.intensity.value = value;
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

            public void SetLensGlobalPosition(Vector2 vector2)
            {
                Lens.rectTransform.position = vector2;
            }

            public void SetRedWorldPosition(Vector3 vector3)
            {
                RedWorld.transform.position = vector3;
            }

            public void ToggleLensImage(bool yes)
            {
                Lens.enabled = yes;
            }
            public void SetRedWorldAlpha(float value)
            {
                RedWorld.alpha = value;
            }

            public void ToggleRedWorldRaycast(bool yes)
            {
                RedWorld.blocksRaycasts = yes;
            }
        }
    }
}