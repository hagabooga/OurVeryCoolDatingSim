using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace CameronsWorld
{
    public class WritingScriptLoader : MonoBehaviour
    {
        WritingScript WritingScript { get; set; }

        public void Start()
        {
            WritingScript = Load();
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                print(WritingScript.GetNext().Text);
            }
            if (Input.GetKeyDown(KeyCode.B))
            {
                print(WritingScript.GetNext(0, true).Text);
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                print(WritingScript.GetNext(0, false).Text);
            }
        }

        public static WritingScript Load()
        {
            return null;
        }
    }
}