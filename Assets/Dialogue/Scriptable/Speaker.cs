using UnityEngine;
using Reference;
using Sprite = Reference.Sprite; // This is because, we are using Systems... and something? It conflicts with some functions like Random.Range etc.


namespace Dialogue
{
///<summary> This will allow you to control the information that is needed to be displayed for a speaker during a conversation </summary>
    [CreateAssetMenu(menuName = "Dialogue/Speaker", fileName = "NewSpeaker")]
    public class Speaker : ScriptableObject
    {
        [Tooltip("Is the speaker known to the Player? yes or no.")]
        public Bool known;
        [Tooltip("The name of the NPC or PC speaking")]
        public String speakerName;

        public String GetName
        {
            get
            {
                speakerName.useConstant = !known;

                if (known)
                {
                    return speakerName;
                }
                else
                {
                    speakerName.Value = "????";
                    return speakerName;
                }
            }
        }

        [SerializeField] private Sprite speakerSprite;
        public Sprite GetSprite
        {
            get { return speakerSprite; }
        }

        [SerializeField] private Color knownCharacterColour = Color.white;

        public Color GetKnownCharacterColour
        {
            get
            {
                if (!known)
                {
                    return knownCharacterColour = Color.black;
                }
                else
                {
                    return knownCharacterColour = Color.white;
                }
            }
        }


    }
}

