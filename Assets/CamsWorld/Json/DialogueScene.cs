using System.Collections.Generic;
using System.Linq;

namespace CameronsWorld
{
    public class DialogueScene
    {
        public string Name { get; }
        public IList<Dialogue> Dialogues { get; }

        public DialogueScene(JsonDialogueScene json)
        {
            Name = json.Name;
            Dialogues = json.Dialogues.Select(x => new Dialogue(x)).ToList();
        }
    }
}