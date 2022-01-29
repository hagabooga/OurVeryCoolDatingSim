using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using TMPro;

public class Dialogue : MonoBehaviour
{
    
    private float letterDisplayDelay = 0.025f; // speed of letter scroll; decrease to quicken
    [SerializeField] private string realWorldDialogueText;
    [SerializeField] private string thoughtWorldDialogueText;
    [SerializeField] private GameObject succeedingDialogue;
    [SerializeField] private bool fadesIn = false;
    [SerializeField] private bool fadesOut = false;
    [SerializeField] private GlobalVars.Character currentSpeaker;

    // references to components
    [SerializeField] private TextMeshProUGUI realWorldTextBoxText = null;
    [SerializeField] private TextMeshProUGUI thoughtWorldTextBoxText = null;

    [SerializeField] private GameObject realWorldContinueButton = null;
    [SerializeField] private GameObject thoughtWorldContinueButton = null;
    [SerializeField] private GameObject realWorldBlackPanel;
    [SerializeField] private GameObject thoughtWorldBlackPanel;
    [SerializeField] private TextMeshProUGUI realWorldNamePlaque;
    [SerializeField] private TextMeshProUGUI thoughtWorldNamePlaque;
    private DialogueActions dialogueActions;
    private float currentLetterDisplayDelay = 0f;
    private void Awake() {
        dialogueActions = new DialogueActions();
    }
    private void OnEnable() {
        dialogueActions.Enable();
    }
    private void OnDisable() {
        dialogueActions.Disable();
    }
    private void Start() {
        currentLetterDisplayDelay = letterDisplayDelay;
        StartCoroutine(DisplayDialogue());
        dialogueActions.DialogueBox.Continue.performed += InteractOnDialogue;

        // fade transition
        if (fadesIn) {
            thoughtWorldBlackPanel.SetActive(true);
            thoughtWorldBlackPanel.GetComponent<Animator>().SetTrigger("fadeIn");
            realWorldBlackPanel.SetActive(true);
            realWorldBlackPanel.GetComponent<Animator>().SetTrigger("fadeIn");
        }

        // name plaque assignment
        thoughtWorldNamePlaque.SetText(GlobalVars.GetCharacterName(currentSpeaker));
        realWorldNamePlaque.SetText(GlobalVars.GetCharacterName(currentSpeaker));
    }
    public IEnumerator DisplayDialogue() {
        thoughtWorldContinueButton.SetActive(false);
        realWorldContinueButton.SetActive(false);

        string realWorldText = "";
        string thoughtWorldText = "";
        int minLength = realWorldDialogueText.Length > thoughtWorldDialogueText.Length ? thoughtWorldDialogueText.Length : realWorldDialogueText.Length;
        for (int i = 0; i < minLength; i++) {
            realWorldText += realWorldDialogueText[i];
            thoughtWorldText += thoughtWorldDialogueText[i];
            realWorldTextBoxText.SetText(realWorldText);
            thoughtWorldTextBoxText.SetText(thoughtWorldText);
            yield return new WaitForSeconds(currentLetterDisplayDelay);
        }
        if (realWorldDialogueText.Length > thoughtWorldDialogueText.Length)
        {
            for (int i = minLength + 1; i < realWorldDialogueText.Length; i++)
            {
                realWorldText += realWorldDialogueText[i];
                realWorldTextBoxText.SetText(realWorldText);
                yield return new WaitForSeconds(currentLetterDisplayDelay);
            }
        } else
        {
            for (int i = minLength + 1; i < thoughtWorldDialogueText.Length; i++)
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
        if (succeedingDialogue)
        {
            buttonToActivate.SetActive(true);
        }
    }

    private void InteractOnDialogue(InputAction.CallbackContext ctx) {
        if (!realWorldContinueButton.activeSelf || !thoughtWorldContinueButton.activeSelf) {
            currentLetterDisplayDelay = 0f;
        } else {
            CloseDialogue();
        }
    }
    private void CloseDialogue() {
        if (fadesOut)
        {
            thoughtWorldBlackPanel.SetActive(false);
            thoughtWorldBlackPanel.GetComponent<Animator>().SetTrigger("fadeOut");
            realWorldBlackPanel.SetActive(false);
            realWorldBlackPanel.GetComponent<Animator>().SetTrigger("fadeOut");
        }
        if (succeedingDialogue) {
            succeedingDialogue.SetActive(true); 
            gameObject.SetActive(false); // don't remove dialogue if user needs to make a choice
        }
    }
}
