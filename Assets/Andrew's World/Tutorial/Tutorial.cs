using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CameronsWorld;

public class Tutorial : MonoBehaviour
{
    public GameObject prompt;

    public void Activate() => StartCoroutine(DoActivate());

    private IEnumerator DoActivate()
    {
        prompt.SetActive(true);
        yield break;
    }
}

