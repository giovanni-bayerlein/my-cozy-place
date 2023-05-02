using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{

    public TMP_Text nameText;
    public TMP_Text dialogueText;

    public GameObject DialogueBox;

    public GameObject Background;

    private Queue<string> sentences;

    // Use this for initialization
    void Awake()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        DialogueBox.SetActive(true);

        if (dialogue.ActivateBackground) Background.SetActive(true);
        else Background.SetActive(false);

        nameText.SetText(dialogue.name);

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        var text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            text += letter;

            dialogueText.SetText(text);
            yield return null;
        }
    }

    void EndDialogue()
    {
        DialogueBox.SetActive(false);
    }

}