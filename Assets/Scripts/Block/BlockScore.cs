using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BlockScore : MonoBehaviour
{
    private TMP_Text _text;

    public int Score { get; private set; }

    private void Start()
    {
        _text = GetComponentInChildren<TMP_Text>();
        SetScore();
    }

    private void SetScore() => _text.text = Score.ToString();

    private void IncreaseScore() => Score += Score;

    public void SetNextScore()
    {
        IncreaseScore();
        SetScore();
    }

    public void SetStartSettings(int startScore)
    {
        Score = startScore;
    }
}
