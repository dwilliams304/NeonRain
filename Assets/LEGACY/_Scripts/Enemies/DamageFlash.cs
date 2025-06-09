using System.Collections;
using UnityEngine;

public class DamageFlash : MonoBehaviour
{
    [SerializeField] private Color _flashColor = Color.white;
    [SerializeField] private float _flashDuration = 0.25f;

    private SpriteRenderer[] _spriteRenderers;
    private Material[] _mat;

    private Coroutine _damageFlashCor;

    void Awake(){
        _spriteRenderers = GetComponentsInChildren<SpriteRenderer>();

        Initialize();
    }


    void Initialize(){
        _mat = new Material[_spriteRenderers.Length];

        for(int i = 0; i < _spriteRenderers.Length; i++){
            _mat[i] = _spriteRenderers[i].material;
        }
    }

    public void CallDamageFlash(){
        _damageFlashCor = StartCoroutine(DoDamageFlash());
    }


    IEnumerator DoDamageFlash(){
        SetFlashColor();

        float currentFlashAmnt;
        float elapsedTime = 0f;

        while(elapsedTime < _flashDuration){
            elapsedTime += Time.deltaTime;
            currentFlashAmnt = Mathf.Lerp(1f, 0f, (elapsedTime / _flashDuration));
            SetFlashAmount(currentFlashAmnt);
            yield return null;
        }
    }

    void SetFlashColor(){
        for(int i = 0; i < _mat.Length; i++){
            _mat[i].SetColor("_FlashColor", _flashColor);
        }
    }

    void SetFlashAmount(float amount){
        for(int i = 0; i < _mat.Length; i++){
            _mat[i].SetFloat("_FlashAmount", amount);
        }
    }
}
