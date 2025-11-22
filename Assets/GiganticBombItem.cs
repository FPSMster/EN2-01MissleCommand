using UnityEngine;

public class GiganticBombItem : ItemBase
{
    [SerializeField]
    Explosion giganticExplosionPrefab_;

   
    public override void Get()
    {
        Instantiate(
          giganticExplosionPrefab_,
          transform.position,
          Quaternion.identity
        );
        Destroy(gameObject);
    }

}

