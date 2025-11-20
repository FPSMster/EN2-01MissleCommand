using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class GameManager : MonoBehaviour
{

    [SerializeField, Header("Prefabs")]
    private Explosion explosionPrefab_;

    [SerializeField]
    private Meteor meteorPrefab_;

    [SerializeField, Header("MeteorSpawner")]

    private BoxCollider2D ground_;

    [SerializeField]

    private GameObject reticlePrefab_;

    [SerializeField]

    private Missile missilePrefab_;

    [SerializeField]
    private float meteorInterval_ = 1;

    private float meteorTimer_;

    private Camera mainCamera_;

    [SerializeField]
    private List<Transform> spawnPositions_;

    [SerializeField, Header("ScoreUISettings")]

    private ScoreText scoreText_;

    private int score_;

    [SerializeField, Header("LifeUISettings")]

    private LifeBar lifeBar_;

    [SerializeField]
    private float maxLife_ = 10;

    private float life_;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        GameObject mainCameraObject = GameObject.FindGameObjectWithTag("MainCamera");

        bool isGetComponent = mainCameraObject.TryGetComponent(out mainCamera_);

        Assert.IsTrue(isGetComponent,"MainCameraにCameraコンポーネントがありません");

        Assert.IsTrue(spawnPositions_.Count > 0, "spawnPositions_に要素が一つもありません。");
        foreach (Transform t in spawnPositions_)
        {
            Assert.IsNotNull(t, "spawnPositions_にNullが含まれています");

        }

        ResetLife();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) { GenerateMissile(); }

        UpdateMeteorTimer();
    }

   


    private void GenerateMissile()
    {

        Vector3 clickPosition =
        mainCamera_.ScreenToWorldPoint(Input.mousePosition);
        clickPosition.z = 0;

        GameObject reticle = Instantiate(
          reticlePrefab_, clickPosition, Quaternion.identity);

        Vector3 launchPosiion = new Vector3(0, -3, 0);
        Missile missile = Instantiate(
          missilePrefab_, launchPosiion, Quaternion.identity);
        missile.Setup(reticle);

    }


    public void AddScore(int point)
    {
        score_ += point;
        scoreText_.SetScore(score_);
    }

    public void Damage(int point) 
    {
        life_ -= point;
        UpdateLifeBar();
    }

   

    private void UpdateMeteorTimer() 
    {
        meteorTimer_-= Time.deltaTime;
        if (meteorTimer_ > 0) { return; }
        meteorTimer_ += meteorInterval_;
        GenerateMeteor();
    }

    private void GenerateMeteor() 
    {
        int max = spawnPositions_.Count;
        int posIndex = UnityEngine.Random.Range(0, max);
        Vector3 spawnPosition = spawnPositions_[posIndex].position;

        Meteor meteor=Instantiate(meteorPrefab_, spawnPosition, Quaternion.identity);
        meteor.Setup(ground_, this, explosionPrefab_);  

    }

    private void ResetLife()
    {
        life_ = maxLife_;

        UpdateLifeBar();
    }

    private void UpdateLifeBar()
    {
        float lifeRatio = Mathf.Clamp01(life_ / maxLife_);

        lifeBar_.SetGaugeRatio(lifeRatio);
    }

}

