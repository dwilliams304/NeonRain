using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float DamageAmount = 0f;

    void OnCollisionEnter2D(Collision2D collision){
        gameObject.SetActive(false);
        if(collision.gameObject.TryGetComponent<HealthSystem>(out HealthSystem h)){
            h.DecreaseCurrentHealth(DamageAmount);
        }
    }


}
