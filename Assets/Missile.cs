using UnityEngine;
using UnityEngine.Assertions;

public class Missile : MonoBehaviour
{
    [SerializeField]
    private Explosion explosionPrefab_;

    [SerializeField]
    private float speed_;

    private Vector3 velocity_;

    private GameObject reticle_;

    public void Setup(GameObject reticle)
    {
        reticle_ = reticle;

        if (reticle.transform.position != transform.position)
        {
            SetupVelocity();

            LookAtReticle();
        }
        else
        {
            Explosion();
        }

    }

    private void SetupVelocity()
    {
        
        Vector3 direction = (
          reticle_.transform.position - transform.position
        );

        Assert.IsTrue(direction != Vector3.zero);
        
        direction = direction.normalized;
        
        velocity_ = direction * speed_;
    }


    private void LookAtReticle()
    {
        
        float angle = Mathf.Atan2(
          velocity_.y,
          velocity_.x
        ) * Mathf.Rad2Deg;
        
        angle -= 90;
        
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
    private void Explosion()
    {
        
        Instantiate(
          explosionPrefab_,
          transform.position,
          Quaternion.identity
        );
        
        Destroy(reticle_);
        
        Destroy(gameObject);
    }


    // Update is called once per frame
    void Update()
    {
        float distanceSqr = Vector3.SqrMagnitude(
          reticle_.transform.position - transform.position
        );
        Vector3 velocityDeltaTime =
          velocity_ * Time.deltaTime;
        float velocityDistanceSqr =
          Vector3.SqrMagnitude(velocityDeltaTime);
        
        if (distanceSqr >= velocityDistanceSqr)
        {
            transform.position += velocityDeltaTime;
            return;
        }
        
        transform.position = reticle_.transform.position;
        Explosion();
    }

   

}
