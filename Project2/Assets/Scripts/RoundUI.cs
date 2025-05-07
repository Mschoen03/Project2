using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoundUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI roundText;

    public void UpdateRound(int round)
    {
        roundText.text = $"Round: {round}";
    }
}
