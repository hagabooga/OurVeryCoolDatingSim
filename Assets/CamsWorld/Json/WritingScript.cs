using System.Collections.Generic;
using System.Linq;
namespace CameronsWorld
{

    public class WritingScript
    {
        public IList<DialogueScene> Scenes { get; }

        public WritingScript(JsonWritingScript json)
        {
            Scenes = json.Scenes.Select(x => new DialogueScene(x)).ToList();
        }
    }
}