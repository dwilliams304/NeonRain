using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float DamageAmount = 0f;

    void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.TryGetComponent<HealthSystem>(out HealthSystem h)){
            h.DecreaseCurrentHealth(DamageAmount);
        }
        GameObject impactEffect = ObjectPooler.current.GetPooledBulletImpact();
        if(impactEffect == null) {
            Debug.LogWarning("NO BULLET IMPACT");
            return;
        }
        impactEffect.transform.position = gameObject.transform.position;
        impactEffect.SetActive(true);
        impactEffect.GetComponent<ParticleSystem>().Play();
        gameObject.SetActive(false);
    }


}
