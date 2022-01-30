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
            Vector2 RedWorldStartingPosition { get; }

            bool firstTimeleftShiftPressed = false;
            IList<Tween> doOnceWhenLeftShiftPressedTweens = new List<Tween>();

            public Presenter(SpotlightModel model, IView view)
            {
                Model = model;
                View = view;
                View.ToggleLensActive(false);
                RedWorldStartingPosition = View.RedWorldGlobalPosition;
                Instance.StartCoroutine(Update());
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
                View.ToggleLensActive(isLeftShiftPressed);
                View.ToggleLensEffectsActive(isLeftShiftPressed);
                if (isLeftShiftPressed)
                {
                    if (!firstTimeleftShiftPressed)
                    {
                        Instance.StartCoroutine(DoOnceWhenLeftShiftPressed());
                    }
                    View.SetLensGlobalPosition(Input.mousePosition);
                    View.SetRedWorldPosition(RedWorldStartingPosition);
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
                }
            }
        }
    }
}