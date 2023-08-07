using Dialogue;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Conversation convo;

    public void Start()
    {
        DialogueMaster.StartConversation(convo);
    }
}
