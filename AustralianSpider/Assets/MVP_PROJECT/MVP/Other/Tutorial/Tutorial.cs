using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public class Tutorial : IIdentify
{
    public bool IsActive() => isActive;
    public string GetID() => ID;

    [SerializeField] private string ID;
    [SerializeField] private List<TutorialText> tutorialTexts = new List<TutorialText>();

    private IEnumerator typingText;
    private IEnumerator cleaningText;

    private bool isActive;

    public void Activate()
    {
        ClearAllCoroutines();
        isActive = true;
        typingText = DisplayTypingText();
        Coroutines.Start(typingText);
    }

    public void Deactivate()
    {
        ClearAllCoroutines();
        isActive = false;
        Debug.Log("Activate clean");

        cleaningText = DisplayCleaningText();
        Coroutines.Start(cleaningText);
    }

    private void ClearAllCoroutines()
    {
        if (typingText != null)
            Coroutines.Stop(typingText);
        if (cleaningText != null)
            Coroutines.Stop(cleaningText);
    }

    private IEnumerator DisplayTypingText()
    {
        for (int i = 0; i < tutorialTexts.Count; i++)
        {
            yield return StartTyping(tutorialTexts[i].TextComponent, tutorialTexts[i].TextDescription, tutorialTexts[i].TimePause);
        }
    }

    private IEnumerator DisplayCleaningText()
    {
        for (int i = tutorialTexts.Count - 1; i >= 0; i--)
        {
            Debug.Log("пгирто");
            yield return StartCleaning(tutorialTexts[i].TextComponent, tutorialTexts[i].TextDescription, tutorialTexts[i].TimePause);
        }
    }

    private IEnumerator StartTyping(TextMeshProUGUI textComponent, string text, float timePause)
    {
        textComponent.text = "";

        foreach(char letter in text)
        {
            textComponent.text += letter;
            yield return new WaitForSeconds(0.02f);
        }

        yield return new WaitForSeconds(timePause);
    }

    private IEnumerator StartCleaning(TextMeshProUGUI textComponent, string text, float timePause)
    {
        textComponent.text = text;

        Debug.Log(textComponent.text);

        for (int i = text.Length; i >= 0; i--)
        {
            textComponent.text = text[..i];
            yield return new WaitForSeconds(0.02f);
        }

        yield return new WaitForSeconds(timePause);
    }
}

[System.Serializable]
public class TutorialText
{
    [SerializeField] private TextMeshProUGUI textComponent;
    [SerializeField] private string textDescription;
    [SerializeField] private float timePause;

    public TextMeshProUGUI TextComponent => textComponent;
    public string TextDescription => textDescription;
    public float TimePause => timePause;
}
