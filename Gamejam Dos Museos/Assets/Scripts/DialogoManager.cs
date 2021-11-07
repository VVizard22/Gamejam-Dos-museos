using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogoManager : MonoBehaviour
{
    public Dialogo dialogo;
    Queue<string> sentences;
    public GameObject dialoguePanel;
    public TextMeshProUGUI displayText;
    string activeSentence;
    public float typingSpeed;
    AudioSource miAudio;
    public AudioClip speakSound;
    bool dialogoActivado = false;
    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        miAudio = GetComponent<AudioSource>();
    }

    void StartDialogue()
    {
        sentences.Clear();
        foreach (string sentence in dialogo.sentenceList)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }
    void DisplayNextSentence()
    {
        if (sentences.Count <= 0)
        {
            displayText.text = activeSentence;
            return;
        }
        activeSentence = sentences.Dequeue();
        displayText.text = activeSentence;
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Trofeo"))
        {
            dialoguePanel.SetActive(true);
            dialogoActivado = true;
            StartDialogue();
        }
    }
    private void Update()
    {
        if (dialogoActivado)
        {
            if (Input.GetKeyDown("space"))
            {
                DisplayNextSentence();
            }
        }

    }
    private void OnTriggerExit2D(Collider2D obj)
    {
        if (obj.CompareTag("Trofeo"))
            dialoguePanel.SetActive(false);
    }

}