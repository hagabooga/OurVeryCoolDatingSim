using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using CameronsWorld.Utility;

public class TheClockIsTicking : MonoBehaviour
{
    [SerializeField] Image phone, thoughtPhone;
    [SerializeField] TMPro.TMP_Text count;
    [SerializeField] GameObject prompt;

    [SerializeField] Vector2 finalPos = new Vector2(-100, -100);
    [SerializeField] Vector2 startingPos = new Vector2(-860, -440);
    [SerializeField] private AudioClip clip;

    public void Activate() => StartCoroutine(DoActivate());

    private IEnumerator DoActivate()
    {
        SoundManager.Instance.Play(clip);
        phone.gameObject.SetActive(true);
        phone.rectTransform.anchoredPosition = startingPos;
        yield return phone.rectTransform.DOSizeDelta(new Vector2(250, 250), 0.8f).WaitForCompletion();
        yield return phone.rectTransform.DOSizeDelta(new Vector2(150, 150), 1).WaitForCompletion();
        yield return phone.rectTransform.DOAnchorPos(finalPos, 1).WaitForCompletion();
        count.gameObject.SetActive(true);
        thoughtPhone.gameObject.SetActive(true);
        prompt.SetActive(true);
    }

}
