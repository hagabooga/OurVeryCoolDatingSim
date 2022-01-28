using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace CameronsWorld
{
    public partial class Spotlight
    {
        public class Presenter
        {
            Model Model { get; }
            IView View { get; }

            Vector2 RedWorldStartingPosition { get; }

            public Presenter(Model model, IView view)
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

            private void PresentLens()
            {
                bool isLeftShiftPressed = Input.GetKey(KeyCode.LeftShift);
                View.ToggleLensActive(isLeftShiftPressed);
                View.ToggleLensEffectsActive(isLeftShiftPressed);
                View.VignetteIntensity = 0;
                if (isLeftShiftPressed)
                {
                    View.VignetteIntensity = Model.VignetteIntensity;
                    View.SetLensGlobalPosition(Input.mousePosition);
                    View.SetRedWorldPosition(RedWorldStartingPosition);
                }
            }
        }
    }
}