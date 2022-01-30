using System;
using System.Collections.Generic;
using System.Linq;

namespace CameronsWorld
{
    public class Dialogue
    {
        public GlobalVars.Background? Background { get; }
        public GlobalVars.Music? Music { get; }
        public GlobalVars.Character? Speaker { get; }
        public GlobalVars.Character? CharacterAppear { get; }
        public GlobalVars.SpecialAction? SpecialAction { get; }


        public string Text { get; }
        public string ThoughtWorldText { get; }
        public IList<DialogueOption> Options { get; }
        public IList<DialogueOption> ThoughtWorldOptions { get; }

        public Dialogue(JsonDialogue json)
        {
            Background = GetEnum<GlobalVars.Background>(json.Background);
            Music = GetEnum<GlobalVars.Music>(json.Music);
            Speaker = GetEnum<GlobalVars.Character>(json.Speaker);
            CharacterAppear = GetEnum<GlobalVars.Character>(json.CharacterAppear);
            SpecialAction = GetEnum<GlobalVars.SpecialAction>(json.SpecialAction);


            Text = json.Text;
            ThoughtWorldText = json.ThoughtWorldText;
            Options = json.Options.Select(x => new DialogueOption(x)).ToList();
            ThoughtWorldOptions = json.ThoughtWorldOptions.Select(x => new DialogueOption(x)).ToList();
        }

        private static T? GetEnum<T>(string s) where T : struct
        {
            if (s == null)
                return null;
            return Enum.Parse<T>(s);
        }
    }
}