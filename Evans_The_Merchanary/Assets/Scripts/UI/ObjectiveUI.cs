using TMPro;
using UnityEngine;

public class ObjectiveUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;


    public void OnTextChanged(TextSO textSO)
    {
        textMeshProUGUI.text = textSO.text;
    }
}
