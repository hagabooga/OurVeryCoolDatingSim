using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections.Generic;

namespace CameronsWorld
{
    public partial class Spotlight
    {
        public class Presenter
        {
            SpotlightModel Model { get; }
            IView View { get; }
            public Spotlight Spotlight { get; }
            Vector2 RedWorldStartingPosition { get; }

            bool firstTimeleftShiftPressed = false;
            IList<Tween> doOnceWhenLeftShiftPressedTweens = new List<Tween>();

            public Presenter(SpotlightModel model, IView view, Spotlight spotlight)
            {
                Model = model;
                View = view;
                Spotlight = spotlight;
                View.ToggleLensImage(false);
                RedWorldStartingPosition = View.RedWorldGlobalPosition;
                Spotlight.StartCoroutine(Update());
                Model.OnUseChanged += UpdatePhone;
            }

            void UpdatePhone()
            {
                View.SetPhoneCounter(Model.Uses);
            }

            IEnumerator Update()
            {
                while (true)
                {
                    PresentLens();
                    yield return null;
                }
            }

            IEnumerator DoOnceWhenLeftShiftPressed()
            {
                firstTimeleftShiftPressed = true;
                var tween = DOTween.To(() => View.VignetteIntensity,
                           x => View.VignetteIntensity = x,
                           Model.VignetteIntensity,
                           Model.TransitionDuration);
                doOnceWhenLeftShiftPressedTweens.Add(tween);
                yield return null;
            }


            private void PresentLens()
            {
                bool isLeftShiftPressed = Input.GetKey(KeyCode.LeftShift);
                View.ToggleLensImage(isLeftShiftPressed);
                View.ToggleLensEffectsActive(isLeftShiftPressed);
                View.ToggleRedWorldRaycast(isLeftShiftPressed);
                if (isLeftShiftPressed)
                {
                    if (!firstTimeleftShiftPressed)
                    {
                        Spotlight.StartCoroutine(DoOnceWhenLeftShiftPressed());
                    }
                    View.SetLensGlobalPosition(Input.mousePosition);
                    View.SetRedWorldPosition(RedWorldStartingPosition);
                    View.SetRedWorldAlpha(1);
                }
                else
                {
                    foreach (var tween in doOnceWhenLeftShiftPressedTweens)
                    {
                        tween.Kill();
                    }
                    doOnceWhenLeftShiftPressedTweens.Clear();
                    View.VignetteIntensity = 0;
                    firstTimeleftShiftPressed = false;
                    View.SetRedWorldAlpha(0);
                }
            }
        }
    }
}