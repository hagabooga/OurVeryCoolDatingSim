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

            public Presenter(Model model, IView view)
            {
                Model = model;
                View = view;
                Instance.StartCoroutine(Update());
            }

            IEnumerator Update()
            {
                while (true)
                {
                    View.SetLensPosition(Input.mousePosition);

                    yield return null;
                }
            }
        }
    }
}