using CameronsWorld.Utility;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
namespace CameronsWorld
{
    public partial class Spotlight : Singleton<Spotlight>
    {
        [SerializeField] Canvas realWorld;
        [SerializeField] Canvas redWorld;
        [SerializeField] Canvas lensEffects;
        [SerializeField] Image lens;
        [SerializeField] Volume volume;

        protected override void Start()
        {
            base.Start();
            Model model = new Model(10);
            IView view = new View(realWorld,
                                  redWorld,
                                  lensEffects,
                                  lens,
                                  volume
                                  );
            Presenter presenter = new Presenter(model, view);
        }

    }
}