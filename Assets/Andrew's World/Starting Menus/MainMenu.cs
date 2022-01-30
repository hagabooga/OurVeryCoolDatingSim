using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private enum SceneName
    {
        MainMenu,
        Help,
        Gameplay,
        Credits,
    };

    [SerializeField] Button start, help, credits;
    [SerializeField] Image fader;

    void Start()
    {
        print("GG");
        start.onClick.AddListener(() => LoadScene(SceneName.Gameplay));
        help.onClick.AddListener(() => LoadScene(SceneName.Help));
        credits.onClick.AddListener(() => LoadScene(SceneName.Credits));
    }

    private IEnumerator LoadScene(SceneName sceneName)
    {
        var tween = fader.DOColor(Color.black, 1);
        yield return tween.WaitForCompletion();
        SceneManager.LoadScene(sceneName.ToString());
    }

}
