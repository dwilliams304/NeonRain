using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    public Rigidbody2D rb;
    public Camera mainCam;

    [SerializeField] private PlayerStats _playerStats;

    Vector2 moveDir;
    Vector2 mousePos;

    private float _moveSpeed;
    private bool _isDashing;
    private bool _canDash;

    public bool isReloading = false;
    void Awake(){
        //_playerStats = PlayerStats.playerStats;
        //_combat = Combat.combat;
    }

    void Start(){
        _moveSpeed = _playerStats.MoveSpeed;
        _canDash = true;
        
    }


    void Update(){
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        if(_isDashing){
            return;
        }

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxis("Vertical");


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
        rb.velocity = new Vector2(moveDir.x * PlayerStats.playerStats.MoveSpeed, moveDir.y * PlayerStats.playerStats.MoveSpeed);


    }


    private IEnumerator Dash(){
        _canDash = false;
        _isDashing = true;
        rb.velocity = new Vector2(moveDir.x * _playerStats.DashSpeed, moveDir.y * _playerStats.DashSpeed);
        yield return new WaitForSeconds(_playerStats.DashDuration);
        _isDashing = false;
        yield return new WaitForSeconds(_playerStats.DashCoolDown);
        _canDash = true;
    }
}