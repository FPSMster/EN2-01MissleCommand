using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]


public class ScoreEffect : MonoBehaviour
{

    [SerializeField]
    float upSpeed_ = 1;

    [SerializeField]
    float aliveTime_ = 1;

    float alivedTimer_ = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        alivedTimer_ += Time.deltaTime;
        if (alivedTimer_ >= aliveTime_) { Destroy(gameObject); }

        transform.Translate(Vector3.up * upSpeed_ * Time.deltaTime);

    }

    public void SetScore(int score)
    {
        GetComponent<TMP_Text>().text = score.ToString();

    }

}
