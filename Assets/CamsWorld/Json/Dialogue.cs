using System;
using System.Collections.Generic;

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
        public string ThoughtWorld { get; }
        public IList<JsonDialogueOption> Options { get; }
        public IList<JsonDialogueOption> ThoughtWorldOptions { get; }

        public Dialogue(JsonDialogue json)
        {
            Background = GetEnum<GlobalVars.Background>(json.Background);
            Music = GetEnum<GlobalVars.Music>(json.Music);
            Speaker = GetEnum<GlobalVars.Character>(json.Speaker);
            CharacterAppear = GetEnum<GlobalVars.Character>(json.CharacterAppear);
            SpecialAction = GetEnum<GlobalVars.SpecialAction>(json.SpecialAction);


            Text = json.Text;
            ThoughtWorld = json.ThoughtWorld;
            Options = json.Options;
            ThoughtWorldOptions = json.ThoughtWorldOptions;
        }

        private static T? GetEnum<T>(string s) where T : struct
        {
            if (s == null)
                return null;
            return Enum.Parse<T>(s);
        }
    }
}