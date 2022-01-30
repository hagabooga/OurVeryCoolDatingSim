using CameronsWorld.Utility;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
namespace CameronsWorld
{
    public partial class Spotlight : MonoBehaviour
    {
        [SerializeField] Canvas realWorld;
        [SerializeField] CanvasGroup redWorld;
        [SerializeField] Canvas lensEffects;
        [SerializeField] Image lens;
        [SerializeField] Volume volume;
        [SerializeField] SpotlightModel model;


        void Start()
        {
            IView view = new View(realWorld,
                                  redWorld,
                                  lensEffects,
                                  lens,
                                  volume
                                  );
            Presenter presenter = new Presenter(model, view, this);
        }

    }
}