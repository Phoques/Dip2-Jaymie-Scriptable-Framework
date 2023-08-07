using System.Collections.Generic; // Lists and Dictionaries
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;

namespace Dialogue
{
    public class DialogueMaster : MonoBehaviour
    {
        private static DialogueMaster _dialogueMasterInstance;
        [SerializeField] private GameObject _dialoguePanel;
        [SerializeField] private Text _speakersName, _dialogueText;

        [SerializeField] private Image _speakerSprite;
        [SerializeField] private GameObject _buttonPrefab;
        [SerializeField] private Transform _buttonParent;
        private List<Button> _buttons = new List<Button>();
        private Conversation _currentConversation;

        private void Awake()
        {
            if (_dialogueMasterInstance != null && _dialogueMasterInstance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _dialogueMasterInstance = this;
            }
        }

        public static void StartConversation (Conversation conversation)
        {
            _dialogueMasterInstance._dialoguePanel.SetActive(true);
            _dialogueMasterInstance._currentConversation = conversation;
            _dialogueMasterInstance._currentConversation.currentLineIndex = 0;
            _dialogueMasterInstance._speakersName.text = "";
            _dialogueMasterInstance._speakerSprite = null;
            _dialogueMasterInstance._dialogueText.text = "";

            foreach (Button button in _dialogueMasterInstance._buttons)
            {
                button.gameObject.SetActive(false);
            }
            _dialogueMasterInstance.UpdateDisplay();
        }

        private void EndConversation() => _dialoguePanel.SetActive(false);

        private void JumpTo(int index)
        {
            _currentConversation.currentLineIndex = index;
            UpdateDisplay();
        }

        private void ResetButtons()
        {
            if(_buttons.Count > 0)
            {
                foreach (Button button in _buttons)
                {
                    Destroy(button.gameObject);
                }
                    _buttons.Clear();
            }
        }

        private void UpdateDisplay()
        {
            ResetButtons();
            Conversation.DialogueLine line = _currentConversation.GetDialogueLines[_currentConversation.currentLineIndex];

            _speakersName.text = line.speaker.GetName;
            _speakerSprite.sprite = line.speaker.GetSprite;
            _dialogueText.text = line.dialogue;
            _speakerSprite.color = line.speaker.GetKnownCharacterColour;

            foreach(Conversation.ActionInfo action in line.actions)
            {
                Button buttonEvent = null;
                buttonEvent = Instantiate(_buttonPrefab, _buttonParent).GetComponent<Button>();
                int target = 0;
                buttonEvent.gameObject.SetActive(true);

                switch (action.action) // May be hard to replicate, but went Switch, then tab, change to action.action,. then tab I think, then arrow down.
                {
                    case DialogueActions.Next:
                        target = _currentConversation.currentLineIndex++;
                        buttonEvent.onClick.AddListener(() => JumpTo(target));
                        break;

                    case DialogueActions.DiscoverNext:
                        target = _currentConversation.currentLineIndex++;
                        buttonEvent.onClick.AddListener(() => JumpTo(target));
                        line.speaker.known.Value = !line.speaker.known.Value;
                        break;

                    case DialogueActions.Branch:
                        target = action.nextChosenLineOfDialogue;
                        buttonEvent.onClick.AddListener(() => JumpTo(target));
                        break;

                    case DialogueActions.DiscoverBranch:
                        target = action.nextChosenLineOfDialogue;
                        buttonEvent.onClick.AddListener(() => JumpTo(target));
                        line.speaker.known.Value |= !line.speaker.known.Value;
                        break;

                    case DialogueActions.Quest:
                        Debug.Log("Set Quest");
                        break;

                    case DialogueActions.Shop:
                        Debug.Log("Open Shop");

                        break;

                    case DialogueActions.Bye:
                        buttonEvent.onClick.AddListener(() => EndConversation()); // The buttons on click event is this. (Unity buttons looses events when made into prefabs, hence this?)
                        break;

                    default:
                        break;
                }
                _buttons.Add(buttonEvent);
                buttonEvent.GetComponentInChildren<Text>().text = action.buttonLabel;

            }
        }

       

    }
}

