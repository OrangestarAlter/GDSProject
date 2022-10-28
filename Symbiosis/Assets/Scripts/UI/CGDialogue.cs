using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CGDialogue : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private GameObject tips;
    [SerializeField] private Text title;
    [SerializeField] private Text dialogue;
    [SerializeField] private Image blackEffect;
    [SerializeField] private int nextScene;

    private Queue<string> sentences;
    private string sentence;
    private bool isDisplaying = false;
    private bool isTyping = false;

    // Update is called once per frame
    void Update()
    {
        if (isDisplaying && Input.GetMouseButtonDown(0))
            if (isTyping)
            {
                StopAllCoroutines();
                dialogue.text = sentence;
                isTyping = false;
                tips.SetActive(true);
            }
            else
                NextSentence();
    }

    public void ShowDialogue(Dialogue dialogue)
    {
        StartCoroutine(StartDialogue(dialogue, 2f));
    }

    IEnumerator StartDialogue(Dialogue dialogue, float duration)
    {
        blackEffect.color = new Color(0, 0, 0, 1f);
        float timer = 0;
        while (timer < duration)
        {
            blackEffect.color = new Color(0, 0, 0, Mathf.Lerp(1f, 0, timer / duration));
            timer += Time.deltaTime;
            yield return null;
        }
        blackEffect.color = new Color(0, 0, 0, 0);
        dialogueBox.SetActive(true);
        title.text = dialogue.title;
        sentences = new Queue<string>();
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        NextSentence();
        isDisplaying = true;
    }

    private void NextSentence()
    {
        if (sentences.Count > 0)
        {
            sentence = sentences.Dequeue();
            StartCoroutine(TypeSentence(0.05f));
        }
        else
        {
            StartCoroutine(EndDialogue(2f));
        }
    }

    IEnumerator TypeSentence(float interval)
    {
        tips.SetActive(false);
        isTyping = true;
        dialogue.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogue.text += letter;
            yield return new WaitForSeconds(interval);
        }
        isTyping = false;
        tips.SetActive(true);
    }

    IEnumerator EndDialogue(float duration)
    {
        isDisplaying = false;
        dialogueBox.SetActive(false);
        blackEffect.color = new Color(0, 0, 0, 0);
        float timer = 0;
        while (timer < duration)
        {
            blackEffect.color = new Color(0, 0, 0, Mathf.Lerp(0, 1f, timer / duration));
            timer += Time.deltaTime;
            yield return null;
        }
        blackEffect.color = new Color(0, 0, 0, 1f);
        SceneManager.LoadScene(nextScene);
    }
}
