using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class ScoreText : MonoBehaviour
{

    private int score_;

    private TMP_Text scoreText_;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        score_ = 0;
        scoreText_ = GetComponent<TMP_Text>();
    }

    public void SetScore(int score)
    {
        score_ = score;
        UpdateScoreText();
    }


    private void UpdateScoreText() { }


    // Update is called once per frame
    void Update()
    {
        
    }
}
