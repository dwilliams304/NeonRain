using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float DamageAmount = 0f;
    public bool isCrit;

    private TrailRenderer tr;

    void Start() => tr = GetComponent<TrailRenderer>();

    void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.TryGetComponent<HealthSystem>(out HealthSystem h) && collision.gameObject.tag != "Player"){
            h.DecreaseCurrentHealth(DamageAmount, isCrit);
        }
        GameObject impactEffect = ObjectPooler.current.GetPooledBulletImpact();
        if(impactEffect == null) {
            Debug.LogWarning("NO BULLET IMPACT");
            return;
        }
        impactEffect.transform.position = gameObject.transform.position;
        impactEffect.SetActive(true);
        impactEffect.GetComponent<ParticleSystem>().Play();
        tr.Clear();
        gameObject.SetActive(false);
    }


}
