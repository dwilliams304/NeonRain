using UnityEngine;

public class OnMouseScale : MonoBehaviour
{
    void OnEnable() => transform.localScale = new Vector2(1f, 1f);
    public void MouseEnter() => transform.localScale = new Vector2(1.1f, 1.1f);
    public void MouseExit() => transform.localScale = new Vector2(1f, 1f);
}
