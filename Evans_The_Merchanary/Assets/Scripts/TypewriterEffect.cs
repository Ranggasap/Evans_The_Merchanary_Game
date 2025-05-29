using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class TypewriterEffect : MonoBehaviour
{
    [Header("Typewriter Effect Properties")]
    [SerializeField] private TextMeshProUGUI dialogueBoxTextUI;
    [SerializeField] private string text;
    [SerializeField] private float characterPerSeconds = 20;
    [SerializeField] private float interpunctuationDelayDuration = 0.5f;
    [SerializeField] private float closeDialogueDuration = 1f;

    [Header("SFX")]
    [SerializeField] private AudioClip typewriterSFX;

    [Header("Event Channels")]
    [SerializeField] private UnityEvent<bool> onTextFullyDisplayed;

    [Header("Debugging Mode")]
    [SerializeField] private int currentVisibleCharacterIndex = 0;
    [SerializeField] private bool isDebuggingMode = false;
    [SerializeField] private bool isTextFullyDisplayed = true;

    private Coroutine typewriterCoroutine;
    private WaitForSeconds simpleDelay;
    private WaitForSeconds interpunctuationDelay;
    private WaitForSeconds closeDialogue;

    private void Awake()
    {
        simpleDelay = new WaitForSeconds(1 / characterPerSeconds);
        interpunctuationDelay = new WaitForSeconds(interpunctuationDelayDuration);
        closeDialogue = new WaitForSeconds(closeDialogueDuration);
    }

    public void SetText(string newText)
    {
        dialogueBoxTextUI.text = newText;
        dialogueBoxTextUI.maxVisibleCharacters = 0;
        currentVisibleCharacterIndex = 0;
        isTextFullyDisplayed = false;

        Debug.Log($"Set Text Start : {newText}");
        typewriterCoroutine = StartCoroutine(Typewriter());
    }

    private void Start()
    {
        if (isDebuggingMode)
        {
            SetText(text);
        }
    }

    private IEnumerator Typewriter()
    {
        SFXManager.instance.PlaySound(typewriterSFX, transform.position);
        Debug.Log("Typewritter Start");
        yield return null;

        TMP_TextInfo textInfo = dialogueBoxTextUI.textInfo;

        while (currentVisibleCharacterIndex < textInfo.characterCount)
        {

            char character = textInfo.characterInfo[currentVisibleCharacterIndex].character;

            dialogueBoxTextUI.maxVisibleCharacters++;

            if (
                character == '?' || character == '.' || character == ',' || character == ':' ||
                character == ';' || character == '!' || character == '-'
            )
            {
                yield return interpunctuationDelay;
            }
            else
            {
                yield return simpleDelay;
            }

            currentVisibleCharacterIndex++;
        }

        if (currentVisibleCharacterIndex == textInfo.characterCount)
        {
            SFXManager.instance.StopSound();
            isTextFullyDisplayed = true;
            onTextFullyDisplayed.Invoke(isTextFullyDisplayed);
        }
    }

    private IEnumerator CloseDialogueBox()
    {
        yield return closeDialogue;
        dialogueBoxTextUI.text = "";
    }

    public void TriggerToSkipText(TextSO newTextSO)
    {
        if (isTextFullyDisplayed)
        {
            SetText(newTextSO.text);
            return;
        }

        dialogueBoxTextUI.maxVisibleCharacters = dialogueBoxTextUI.text.Length;
        isTextFullyDisplayed = true;

        if (typewriterCoroutine != null)
        {
            StopCoroutine(typewriterCoroutine);
        }

        SFXManager.instance.StopSound();
        onTextFullyDisplayed.Invoke(isTextFullyDisplayed);
    }

}
