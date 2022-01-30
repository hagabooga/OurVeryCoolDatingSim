using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CameronsWorld
{

    public class WritingScript
    {
        class DialogueRecord
        {
            public IList<Dialogue> Dialogues { get; }
            public int Index { get; set; }

            public int AwaitingOption { get; set; } = 0;

            public DialogueRecord(IList<Dialogue> dialogues, int index = 0)
            {
                Dialogues = dialogues;
                Index = index;
            }

            public Dialogue GetCurrent()
            {
                return Dialogues[Index];
            }

            public Dialogue GetNext()
            {
                var current = GetCurrent();
                if (AwaitingOption == 0 && (current.Options.Count != 0 || current.Options.Count != 0))
                {
                    AwaitingOption = 1;
                    return Dialogues[Index];
                }
                return Dialogues[Index++];
            }
        }

        public IList<DialogueScene> Scenes { get; }


        Stack<DialogueRecord> Dialogues { get; } = new Stack<DialogueRecord>();

        public WritingScript(JsonWritingScript json)
        {
            Scenes = json.Scenes.Select(x => new DialogueScene(x)).ToList();
            for (int i = Scenes.Count - 1; i >= 0; i--)
            {
                Dialogues.Push(new DialogueRecord(Scenes[i].Dialogues));
            }
        }

        public Dialogue GetNext(int? optionIndex = null, bool? isThoughtWorldOption = null)
        {
            DialogueRecord dialogueRecord = Dialogues.Peek();
            if (dialogueRecord.Index == dialogueRecord.Dialogues.Count)
            {
                Dialogues.Pop();
                if (!Dialogues.TryPeek(out dialogueRecord))
                {
                    Debug.Log("GO TO CREDITS SCENE!!!!!!");
                    return null;
                }
            }
            var currentDialogue = dialogueRecord.GetNext();
            if (dialogueRecord.AwaitingOption != 0 && dialogueRecord.AwaitingOption != 2)
            {
                dialogueRecord.AwaitingOption++;
                return currentDialogue;
            }
            if (optionIndex.HasValue)
            {
                if (!isThoughtWorldOption.HasValue)
                {
                    throw new Exception("Please give bool parameter if is a thought world option!");
                }
                var dialogueOptions = isThoughtWorldOption.Value ?
                    currentDialogue.ThoughtWorldOptions : currentDialogue.Options;
                if (dialogueOptions.Count == 0)
                {
                    throw new Exception("Option Index given but the dialogue has no options!");
                }
                Dialogues.Push(new DialogueRecord(dialogueOptions[optionIndex.Value].Dialogues));
                currentDialogue = Dialogues.Peek().GetNext();
            }
            else
            {
                if (currentDialogue.Options.Count != 0 || currentDialogue.Options.Count != 0)
                {
                    throw new Exception("Dialogue has options but wasnt picked!");
                }
            }
            return currentDialogue;
        }

    }
}