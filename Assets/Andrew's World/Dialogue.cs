using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using TMPro;

public class Dialogue : MonoBehaviour
{
    
    private float letterDisplayDelay = 0.025f; // speed of letter scroll; decrease to quicken
    [SerializeField] private string dialogueText;
    [SerializeField] private GameObject succeedingDialogue;
    [SerializeField] private TextMeshProUGUI textBoxText = null;
    [SerializeField] private GameObject continueButton = null;
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
    }
    public IEnumerator DisplayDialogue() {
        continueButton.SetActive(false);
        string curText = "";
        for (int i = 0; i < dialogueText.Length; i++) {
            curText += dialogueText[i];
            textBoxText.SetText(curText);
            yield return new WaitForSeconds(currentLetterDisplayDelay);
        }
        if (succeedingDialogue)
            continueButton.SetActive(true);
    }

    private void InteractOnDialogue(InputAction.CallbackContext ctx) {
        if (!continueButton.activeSelf) {
            currentLetterDisplayDelay = 0f;
        } else {
            CloseDialogue();
        }
    }
    private void CloseDialogue() {
        if (succeedingDialogue) {
            succeedingDialogue.SetActive(true); 
            gameObject.SetActive(false); // don't remove dialogue if user needs to make a choice
        }
    }
}
