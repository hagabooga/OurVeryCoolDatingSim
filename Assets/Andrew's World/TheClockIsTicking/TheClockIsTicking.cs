using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TheClockIsTicking : MonoBehaviour
{
    [SerializeField] Image phone;
    [SerializeField] TMPro.TMP_Text count;
    [SerializeField] GameObject prompt;

    [SerializeField] Vector2 finalPos = new Vector2(-100, -100);
    [SerializeField] Vector2 startingPos = new Vector2(-860, -440);

    public void Activate() => StartCoroutine(DoActivate());

    private IEnumerator DoActivate()
    {
        phone.gameObject.SetActive(true);
        phone.rectTransform.anchoredPosition = startingPos;
        yield return phone.rectTransform.DOSizeDelta(new Vector2(250, 250), 0.8f).WaitForCompletion();
        yield return phone.rectTransform.DOSizeDelta(new Vector2(150, 150), 1).WaitForCompletion();
        yield return phone.rectTransform.DOAnchorPos(finalPos, 1).WaitForCompletion();
        count.gameObject.SetActive(true);
        prompt.SetActive(true);
    }

}
