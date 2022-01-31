using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using CameronsWorld.Utility;

public class MainMenu : MonoBehaviour
{
    private enum SceneName
    {
        MainMenu,
        Help,
        Gameplay,
        Credits,
    };

    [SerializeField] AudioClip clip;

    void Start()
    {
        SoundManager.Instance.PlayBGM(clip);

    }


}
