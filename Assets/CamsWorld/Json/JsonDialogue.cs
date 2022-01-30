using System.Collections.Generic;

namespace CameronsWorld
{
    public class JsonDialogue
    {
        public string Background { get; set; }
        public string Music { get; set; }
        public string Speaker { get; set; }
        public string CharacterAppear { get; set; }
        public string SpecialAction { get; set; }


        public string Text { get; set; }
        public string ThoughtWorld { get; set; }
        public List<JsonDialogueOption> Options { get; set; }
        public List<JsonDialogueOption> ThoughtWorldOptions { get; set; }

    }
}