using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public void TriggerDialogue(Dialogue dialogue)
    {
        FindObjectOfType<DialogueController>().StartDialogue(dialogue);
    }

}