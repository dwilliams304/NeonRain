using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    public Rigidbody2D rb;
    public Camera mainCam;

    Vector2 moveDir;
    Vector2 mousePos;

    //MoveSpeed
    private float _moveSpeed = 9f;

    //Dash variables
    private float _dashSpeed = 35f;
    private float _dashDuration = 0.2f;
    private float _dashCoolDown = 1.75f;
    private bool _isDashing;
    private bool _canDash;




    public bool isReloading = false;

    void OnEnable(){
        CorruptionManager.moveSpeedModifier += ChangeMoveSpeed;
    }
    void OnDisable(){
        CorruptionManager.moveSpeedModifier -= ChangeMoveSpeed;
    }



    void Start(){
        _canDash = true;
    }

    void ChangeMoveSpeed(float moveSpeedModifier){
        _moveSpeed += moveSpeedModifier;
    }


    void Update(){
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        
        if(_isDashing){
            return;
        }

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");


        if(Input.GetButtonDown("Jump") && _canDash){
            StartCoroutine(Dash());
        }

        moveDir = new Vector2(moveX, moveY).normalized;
    }


    void FixedUpdate(){
        Vector2 mouseDir = mousePos - rb.position;
        float angle = Mathf.Atan2(mouseDir.y, mouseDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
        if(_isDashing){
            return;
        }
        rb.velocity = new Vector2(moveDir.x * _moveSpeed, moveDir.y * _moveSpeed);


    }


    private IEnumerator Dash(){
        _canDash = false;
        _isDashing = true;
        rb.velocity = new Vector2(moveDir.x * _dashSpeed, moveDir.y * _dashSpeed);
        yield return new WaitForSeconds(_dashDuration);
        _isDashing = false;
        UIManager.uiManagement.DashCoolDownBar(_dashCoolDown);
        yield return new WaitForSeconds(_dashCoolDown);
        _canDash = true;
    }
}