using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using UnityEngine;

namespace CameronsWorld
{
    public class WritingScriptLoader : MonoBehaviour
    {
        public void Start()
        {
            var x = Load();
            foreach (var scene in x.Scenes)
            {
                foreach (var item in scene.Dialogues)
                {
                    print(item.Text + " " + item.Speaker);
                }
            }
        }

        public static WritingScript Load()
        {
            return new WritingScript(JsonSerializer.Deserialize<JsonWritingScript>(
                File.ReadAllText(
                    Path.Combine(
                        Application.streamingAssetsPath,
                        "WritingScript.json")))
            );
        }
    }
}