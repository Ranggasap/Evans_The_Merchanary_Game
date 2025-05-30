using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class BossBattleTextManager : MonoBehaviour
{
    [Header("Text Manager Properties")]
    [SerializeField] private List<TextSO> sequenceTextSO;

    [Header("Event Channels")]
    [SerializeField] private UnityEvent<TextSO> changeText;

    [Header("Debugging Mode")]
    [SerializeField] private int currentTextSequence = 0;
    [SerializeField] private bool isTextReadyToChange = false;
    [SerializeField] private bool isIntroAlreadyOver = false;

    private void Start()
    {
        Debug.Log($"Start: {sequenceTextSO[currentTextSequence].text}");
        ChangeText(sequenceTextSO[currentTextSequence]);
    }

    public void ChangeText(TextSO textSO)
    {
        if (isTextReadyToChange) return;

        changeText.Invoke(textSO);

    }

    public void OnTriggerChangeText(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        if (currentTextSequence == sequenceTextSO.Count - 1 && !isIntroAlreadyOver)
        {
            isIntroAlreadyOver = true;
            SceneManager.LoadScene("BossBattle");
            return;
        }

        if (!isTextReadyToChange)
            {
                changeText.Invoke(null);
            }
            else
            {
                isTextReadyToChange = false;
                currentTextSequence++;
                ChangeText(sequenceTextSO[currentTextSequence]);
                Debug.Log($"Text: {sequenceTextSO[currentTextSequence].text}");
            }

    }

    public void OnTextStatusChanged(bool newStatus)
    {
        isTextReadyToChange = newStatus;
    }

}