using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Button))]
public class UISceneSwitcher : MonoBehaviour
{

    private enum SceneType
    {
        MainMenu,
        Help,
        DialogueTest,
        Credits,
    };

    [SerializeField]
    private SceneType destinationScene = SceneType.MainMenu;
    private GameObject helperPanel;

    private Animator screenTransition;
    public void Awake()
    {
        Button thisButton = GetComponent<Button>();
        thisButton.onClick.AddListener(() =>
        {
            StartCoroutine(loadScene());
        });
    }

    private IEnumerator loadScene()
    {
        screenTransition.SetTrigger("fadeOut");
        yield return new WaitForSecondsRealtime(1f);
        SceneManager.LoadScene(destinationScene.ToString());
        yield return false;
    }
    void Start()
    {
        helperPanel = GameObject.Find("BlackFadePanel");
        screenTransition = helperPanel.GetComponent<Animator>();
        screenTransition.SetTrigger("fadeIn");
    }

}
