using System.Collections.Generic;
using System.Linq;

namespace CameronsWorld
{
    public class DialogueOption
    {
        public string Text { get; }
        public IList<Dialogue> Dialogues { get; }

        public DialogueOption(JsonDialogueOption json)
        {
            Text = json.Text;
            Dialogues = json.Dialogues.Select(x => new Dialogue(x)).ToList();
        }
    }
}