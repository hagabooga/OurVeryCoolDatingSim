using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System;
using TMPro;
using CameronsWorld;
using CameronsWorld.Utility;
using DG.Tweening;

public class Dialogue : MonoBehaviour
{

    private float letterDisplayDelay = 0.025f; // speed of letter scroll; decrease to quicken
    [SerializeField] private bool fadesIn = false;
    [SerializeField] private bool fadesOut = false;

    // references to components
    [SerializeField] private TextMeshProUGUI realWorldTextBoxText = null;
    [SerializeField] private TextMeshProUGUI thoughtWorldTextBoxText = null;
    [SerializeField] private GameObject realWorldContinueButton = null;
    [SerializeField] private GameObject thoughtWorldContinueButton = null;
    [SerializeField] private GameObject realWorldBlackPanel;
    [SerializeField] private GameObject thoughtWorldBlackPanel;
    [SerializeField] private TextMeshProUGUI realWorldNamePlaque;
    [SerializeField] private TextMeshProUGUI thoughtWorldNamePlaque;
    [SerializeField] private GameObject realWorldDialogueBox;
    [SerializeField] private GameObject thoughtWorldDialogueBox;
    [SerializeField] private GameObject realWorldBG;
    [SerializeField] private GameObject thoughtWorldBG;
    [SerializeField] private GameObject realWorldSingleOption;
    [SerializeField] private GameObject thoughtWorldSingleOption;
    [SerializeField] private GameObject realWorldDoubleOption;
    [SerializeField] private GameObject thoughtWorldDoubleOption;

    [SerializeField] Image realCam, thoughtCam, realYun, thoughtYun;


    // references to UI elements
    [SerializeField] private List<Sprite> dialogueBoxes; // first 3 are chars, 4th is headerless, 5th is thought world, 6th is thought world headerless
    [SerializeField] private List<Sprite> backgroundArtRealWorld;
    [SerializeField] private List<Sprite> backgroundArtThoughtWorld;
    [SerializeField] private List<Sprite> continueButtons; // first 3 are chars, 4th is default

    [SerializeField] List<Sprite> yunSprites;
    [SerializeField] List<AudioClip> backgroundMusics;






    [SerializeField] TheClockIsTicking theClockIsTicking;
    [SerializeField] Tutorial tutorial;


    private DialogueActions dialogueActions;
    private float currentLetterDisplayDelay = 0f;
    private WritingScript writingScript;
    private bool proceedingDisabled; // stop ppl from proceeding when choices are available
    private void Awake()
    {
        dialogueActions = new DialogueActions();
    }
    private void OnEnable()
    {
        dialogueActions.Enable();
    }
    private void OnDisable()
    {
        dialogueActions.Disable();
    }
    private void Start()
    {
        writingScript = WritingScriptLoader.Load();
        currentLetterDisplayDelay = letterDisplayDelay;
        dialogueActions.DialogueBox.Continue.performed += InteractOnDialogue;
        CameronsWorld.Dialogue currentDialogue = writingScript.GetNext();
        SetDialogueValues(currentDialogue);

        // fade transition
        /*if (fadesIn) {
            thoughtWorldBlackPanel.SetActive(true);
            thoughtWorldBlackPanel.GetComponent<Animator>().SetTrigger("fadeIn");
            realWorldBlackPanel.SetActive(true);
            realWorldBlackPanel.GetComponent<Animator>().SetTrigger("fadeIn");
        }*/

    }
    public IEnumerator DisplayDialogue(string realWorldDialogueText, string thoughtWorldDialogueText)
    {
        if (thoughtWorldDialogueText == null)
            thoughtWorldDialogueText = realWorldDialogueText;
        thoughtWorldContinueButton.SetActive(false);
        realWorldContinueButton.SetActive(false);
        currentLetterDisplayDelay = 0.025f;
        string realWorldText = "";
        string thoughtWorldText = "";
        int minLength = realWorldDialogueText.Length > thoughtWorldDialogueText.Length ? thoughtWorldDialogueText.Length : realWorldDialogueText.Length;
        for (int i = 0; i < minLength; i++)
        {
            realWorldText += realWorldDialogueText[i];
            thoughtWorldText += thoughtWorldDialogueText[i];
            realWorldTextBoxText.SetText(realWorldText);
            thoughtWorldTextBoxText.SetText(thoughtWorldText);
            yield return new WaitForSeconds(currentLetterDisplayDelay);
        }
        if (realWorldDialogueText.Length > thoughtWorldDialogueText.Length)
        {
            for (int i = minLength; i < realWorldDialogueText.Length; i++)
            {
                realWorldText += realWorldDialogueText[i];
                realWorldTextBoxText.SetText(realWorldText);
                yield return new WaitForSeconds(currentLetterDisplayDelay);
            }
        }
        else
        {
            for (int i = minLength; i < thoughtWorldDialogueText.Length; i++)
            {
                thoughtWorldText += thoughtWorldDialogueText[i];
                thoughtWorldTextBoxText.SetText(thoughtWorldText);
                yield return new WaitForSeconds(currentLetterDisplayDelay);
            }
        }

        addContinueButtonIfDialogueFollows(thoughtWorldContinueButton);
        addContinueButtonIfDialogueFollows(realWorldContinueButton);
    }

    private void addContinueButtonIfDialogueFollows(GameObject buttonToActivate)
    {
        buttonToActivate.SetActive(true);
    }

    private void InteractOnDialogue(InputAction.CallbackContext ctx)
    {
        if (!realWorldContinueButton.activeSelf || !thoughtWorldContinueButton.activeSelf)
        {
            currentLetterDisplayDelay = 0f;
        }
        else
        {
            MoveToNextDialogue();
        }
    }
    private void MoveToNextDialogue()
    {

        /*if (fadesOut)
        {
            thoughtWorldBlackPanel.SetActive(false);
            thoughtWorldBlackPanel.GetComponent<Animator>().SetTrigger("fadeOut");
            realWorldBlackPanel.SetActive(false);
            realWorldBlackPanel.GetComponent<Animator>().SetTrigger("fadeOut");
        }*/
        if (proceedingDisabled)
        {
            return;
        }
        Debug.Log("Querying a");
        CameronsWorld.Dialogue currentDialogue = writingScript.GetNext();
        Debug.Log("real options a: " + currentDialogue.Options.Count);
        Debug.Log("thought options a" + currentDialogue.ThoughtWorldOptions.Count);
        ActivateSpecialAction(currentDialogue.SpecialAction);
        if (currentDialogue != null)
        {
            SetDialogueValues(currentDialogue);
        }
    }
    private void MoveToNextDialogue(int optionIndex, bool isThoughtWorldOption)
    {

        /*if (fadesOut)
        {
            thoughtWorldBlackPanel.SetActive(false);
            thoughtWorldBlackPanel.GetComponent<Animator>().SetTrigger("fadeOut");
            realWorldBlackPanel.SetActive(false);
            realWorldBlackPanel.GetComponent<Animator>().SetTrigger("fadeOut");
        }*/
        if (proceedingDisabled)
        {
            return;
        }
        CameronsWorld.Dialogue currentDialogue = writingScript.GetNext(optionIndex, isThoughtWorldOption);
        ActivateSpecialAction(currentDialogue.SpecialAction);
        Debug.Log("real options s: " + currentDialogue.Options.Count);
        Debug.Log("thought options s" + currentDialogue.ThoughtWorldOptions.Count);
        if (currentDialogue != null)
        {
            SetDialogueValues(currentDialogue);
        }
    }
    private void SetDialogueValues(CameronsWorld.Dialogue dialogue)
    {
        if (dialogue.Text != null)
        {
            StartCoroutine(DisplayDialogue(dialogue.Text, dialogue.ThoughtWorldText));
        }
        SetDialogueOptions(dialogue.Options, dialogue.ThoughtWorldOptions);
        SetDialogueSpeaker(dialogue.Speaker);
        SetDialogueBackground(dialogue.Background);
        SetDialogueMusic(dialogue.Music);
        SetCharacterAppear(dialogue.CharacterAppear);
    }

    private void SetCharacterAppear(GlobalVars.Character? characterAppear)
    {
        if (!characterAppear.HasValue)
        {
            return;
        }
        switch (characterAppear.Value)
        {
            case GlobalVars.Character.Cam:
                foreach (var cam in new[] { realCam, thoughtCam })
                {
                    cam.gameObject.SetActive(true);
                    cam.rectTransform.anchoredPosition = new Vector2(1000, -25);
                    cam.rectTransform.DOAnchorPosX(-130, 1);
                }
                break;
            case GlobalVars.Character.Yun:
                foreach (var cam in new[] { realYun, thoughtYun })
                {
                    cam.gameObject.SetActive(true);
                    cam.rectTransform.anchoredPosition = new Vector2(-118, -25);
                    cam.rectTransform.DOAnchorPosX(650, 1);
                }
                break;
        }
    }

    private void SetDialogueMusic(GlobalVars.Music? music)
    {
        if (!music.HasValue)
        {
            return;
        }
        switch (music.Value)
        {
            case GlobalVars.Music.Stop:
                SoundManager.Instance.StopAll();
                break;
            default:
                SoundManager.Instance.Play(backgroundMusics[((int)music.Value)]);
                break;
        }
    }

    private void SetDialogueSpeaker(GlobalVars.Character? character)
    {
        if (character != null)
        {
            realWorldDialogueBox.GetComponent<Image>().sprite = dialogueBoxes[(int)character];
            thoughtWorldDialogueBox.GetComponent<Image>().sprite = dialogueBoxes[4];
            realWorldContinueButton.GetComponent<Image>().sprite = continueButtons[(int)character];
            thoughtWorldContinueButton.GetComponent<Image>().sprite = continueButtons[(int)character];
            // name plaque assignment
            thoughtWorldNamePlaque.SetText(GlobalVars.GetCharacterName(character));
            realWorldNamePlaque.SetText(GlobalVars.GetCharacterName(character));
        }
        else
        {
            realWorldDialogueBox.GetComponent<Image>().sprite = dialogueBoxes[3];
            thoughtWorldDialogueBox.GetComponent<Image>().sprite = dialogueBoxes[5];
            realWorldContinueButton.GetComponent<Image>().sprite = continueButtons[3];
            thoughtWorldContinueButton.GetComponent<Image>().sprite = continueButtons[3];
            // name plaque assignment
            thoughtWorldNamePlaque.SetText("");
            realWorldNamePlaque.SetText("");
        }
    }
    private void SetDialogueBackground(GlobalVars.Background? background)
    {
        if (background != null)
        {
            realWorldBG.GetComponent<Image>().sprite = backgroundArtRealWorld[(int)background];
            thoughtWorldBG.GetComponent<Image>().sprite = backgroundArtThoughtWorld[(int)background];
        }
    }
    private void SetDialogueOptions(IList<DialogueOption> realWorldOptions, IList<DialogueOption> thoughtWorldOptions)
    {
        realWorldSingleOption.SetActive(false);
        realWorldDoubleOption.SetActive(false);
        thoughtWorldSingleOption.SetActive(false);
        thoughtWorldDoubleOption.SetActive(false);
        if (realWorldOptions.Count > 0 || thoughtWorldOptions.Count > 0)
        {
            proceedingDisabled = true;
        }
        if (realWorldOptions.Count != 0)
        {
            if (realWorldOptions.Count == 1)
            {
                realWorldSingleOption.SetActive(true);
                realWorldSingleOption.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = realWorldOptions[0].Text;
            }
            else
            {
                realWorldDoubleOption.SetActive(true);
                realWorldDoubleOption.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = realWorldOptions[0].Text;
                realWorldDoubleOption.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = realWorldOptions[1].Text;
            }
        }
        if (thoughtWorldOptions.Count != 0)
        {
            if (thoughtWorldOptions.Count == 1)
            {
                thoughtWorldSingleOption.SetActive(true);
                thoughtWorldSingleOption.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = thoughtWorldOptions[0].Text;

            }
            else
            {
                thoughtWorldDoubleOption.SetActive(true);
                thoughtWorldDoubleOption.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = thoughtWorldOptions[0].Text;
                thoughtWorldDoubleOption.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = thoughtWorldOptions[1].Text;
            }
        }
    }
    public void selectOption(int optionIndexEncrypted)
    {
        // 0 = first real option, 1 = second real option, 2 = first thought option, 3 = second thought option
        proceedingDisabled = false;
        MoveToNextDialogue(optionIndexEncrypted % 2, optionIndexEncrypted / 2 > 0);
    }

    public void ActivateSpecialAction(GlobalVars.SpecialAction? action)
    {
        if (!action.HasValue)
        {
            return;
        }
        switch (action)
        {
            case GlobalVars.SpecialAction.Tutorial:
                tutorial.Activate();
                break;
            case GlobalVars.SpecialAction.TheClockIsTicking:
                theClockIsTicking.Activate();
                break;
        }
    }


}
