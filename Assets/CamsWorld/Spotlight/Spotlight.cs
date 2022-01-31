using CameronsWorld.Utility;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using TMPro;
namespace CameronsWorld
{
    public partial class Spotlight : MonoBehaviour
    {
        [SerializeField] Canvas realWorld;
        [SerializeField] CanvasGroup redWorld;
        [SerializeField] Canvas lensEffects;
        [SerializeField] Image lens, border;
        [SerializeField] Volume volume;
        [SerializeField] SpotlightModel model;
        [SerializeField] TMP_Text phoneCount, thoughtPhoneCount;


        void Start()
        {
            IView view = new View(realWorld,
                                  redWorld,
                                  lensEffects,
                                  lens,
                                  volume,
                                  phoneCount,
                                thoughtPhoneCount,
                                border
                                  );
            Presenter presenter = new Presenter(model, view, this);
        }

    }
}