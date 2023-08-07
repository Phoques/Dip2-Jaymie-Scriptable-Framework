using System;
using UnityEngine;

namespace Dialogue
{
    [CreateAssetMenu(menuName = "Dialogue/Conversation", fileName = "New Conversation")]
    public class Conversation : ScriptableObject
    {
        [Serializable]
        public struct ActionInfo
        {
            public DialogueActions action;
            public string buttonLabel;
            public int nextChosenLineOfDialogue;
        }

        //This is gross and messy, Jaymie wants us to rebuild this as homework.
        [Serializable]
        public struct DialogueLine
        {
            public Speaker speaker;
            [TextArea(1,8)] public string dialogue;
            public ActionInfo[] actions;
        }
        [SerializeField] private DialogueLine[] _lines;
        public DialogueLine[] GetDialogueLines => _lines;
        public int currentLineIndex = 0;

    }

    public enum DialogueActions
    {
        Next,
        DiscoverNext,
        Branch,
        DiscoverBranch,
        Quest,
        Shop,
        Bye
    }

}


