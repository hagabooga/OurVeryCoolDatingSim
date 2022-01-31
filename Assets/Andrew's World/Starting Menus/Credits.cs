using System.Collections;
using System.Collections.Generic;
using CameronsWorld.Utility;
using UnityEngine;

public class Credits : MonoBehaviour
{
    [SerializeField] private AudioClip clip;

    // Start is called before the first frame update
    void Start()
    {
        SoundManager.Instance.PlayBGM(clip)
;
    }


}
