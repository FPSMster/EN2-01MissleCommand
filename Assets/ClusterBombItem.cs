using UnityEngine;

public class ClusterBombItem : ItemBase
{
    [SerializeField]
    private Explosion explosionPrefab_;

    private bool isGet = false;

    private float explosionEmmitionTimer_ = 3;

    private float explosionInterval_ = 0.2f;
    private float explosionTimer_ = 0.0f;

    private SpriteRenderer renderer_;

    public override void Get()
    {

        if (TryGetComponent(out renderer_))
        {
            renderer_.enabled = false;
        }

        collider_.enabled = false;

        transform.GetChild(0).gameObject.SetActive(false);

        isGet = true;
    }

    protected override void Update()
    {

        if (!isGet)
        {
            base.Update();
            return;
        }
        explosionTimer_ -= Time.deltaTime;

        if (explosionEmmitionTimer_ <= 0) { Destroy(gameObject); }

        UpdateClusterExplosion();
    }

    private void UpdateClusterExplosion()
    {
        explosionEmmitionTimer_ -= Time.deltaTime;
        if (explosionTimer_ > 0) { return; }

        float randomWidth = 2;
        Vector3 offset = new Vector3(
            Random.Range(-randomWidth, randomWidth),
            Random.Range(-randomWidth, randomWidth),
            0
          );

        Instantiate(
          explosionPrefab_,
          transform.position + offset,
          Quaternion.identity
        );
        explosionTimer_ += explosionInterval_;
    }

}