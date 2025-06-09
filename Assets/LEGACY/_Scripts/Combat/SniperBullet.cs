using UnityEngine;

public class SniperBullet : MonoBehaviour
{
    public float DamageAmount = 0f;
    public bool isCrit;
    private TrailRenderer tr;

    void Start(){
        tr = GetComponent<TrailRenderer>();
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.TryGetComponent<HealthSystem>(out HealthSystem h) && other.tag != "Player"){
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
        if(other.tag == "Bounds"){
            tr.Clear();
            gameObject.SetActive(false);
        }
    }


}
