using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float DamageAmount = 0f;

    void OnCollisionEnter2D(Collision2D collision){
        gameObject.SetActive(false);
        if(collision.gameObject.TryGetComponent<HealthBehavior>(out HealthBehavior h)){
            h.DecreaseCurrentHealth(DamageAmount);
        }
    }


}
