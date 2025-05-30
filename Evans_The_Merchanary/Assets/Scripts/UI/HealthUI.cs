using TMPro;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;

    public void OnHealthChanged(int newHealth)
    {
        textMeshProUGUI.text = $"HEALTH : {newHealth}";
    }
}
