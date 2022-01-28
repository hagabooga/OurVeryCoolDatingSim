using CameronsWorld.Utility;
using UnityEngine;
using UnityEngine.UI;

namespace CameronsWorld
{
    public partial class Spotlight : Singleton<Spotlight>, ICoroutiner
    {
        [SerializeField] Canvas realWorld;
        [SerializeField] Canvas redWorld;
        [SerializeField] Canvas lensEffects;
        [SerializeField] Image lens;

        protected override void Start()
        {
            base.Start();
            Model model = new Model(10);
            IView view = new View(realWorld, redWorld, lensEffects, lens);
            Presenter presenter = new Presenter(model, view);
        }

    }
}