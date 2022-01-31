using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Flicker : MonoBehaviour
{

    [SerializeField] Image image;
    [SerializeField] Sprite bedroom, thoughtBed;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GG());
    }

    private IEnumerator GG()
    {
        while (true)
        {
            image.sprite = bedroom;
            yield return new WaitForSeconds(4);
            image.sprite = thoughtBed;
            yield return new WaitForSeconds(0.1f);
            image.sprite = bedroom;
            yield return new WaitForSeconds(0.1f);
            image.sprite = thoughtBed;
            yield return new WaitForSeconds(0.1f);
            image.sprite = bedroom;
            yield return new WaitForSeconds(0.1f);
            image.sprite = thoughtBed;
            yield return new WaitForSeconds(0.1f);
            image.sprite = bedroom;
            yield return new WaitForSeconds(0.1f);
            image.sprite = thoughtBed;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
