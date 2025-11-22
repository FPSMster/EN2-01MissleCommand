using UnityEngine;

[RequireComponent(typeof(Collider2D))]

abstract public class ItemBase : MonoBehaviour
{

    
    [SerializeField]
    protected float speed_ = 3;
    
    protected Camera camera_;
    
    protected Collider2D collider_;
   
    private void Awake()
    {
        camera_ = Camera.main;
        collider_ = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        transform.Translate(Vector3.right * speed_ * Time.deltaTime);

        
        float worldScreenRight =
          camera_.orthographicSize * camera_.aspect;
        
        float boundsSize = collider_.bounds.size.x;
        
        if (transform.position.x > worldScreenRight + boundsSize)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Explosion")) { Get(); }
    }

    public abstract void Get();

}
