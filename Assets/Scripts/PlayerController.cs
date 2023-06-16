using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    public Rigidbody2D rb;
    public Camera mainCam;

    private PlayerStats _playerStats;

    Vector2 movementVector;
    Vector2 mousePos;

    void Awake(){
        _playerStats = GetComponent<PlayerStats>();
        
    }

    void Start(){
        
    }


    void Update(){
        movementVector.x = Input.GetAxisRaw("Horizontal");
        movementVector.y = Input.GetAxis("Vertical");

        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
    }


    void FixedUpdate(){
        rb.MovePosition(rb.position + movementVector * _playerStats.MoveSpeed * Time.fixedDeltaTime);

        Vector2 mouseDir = mousePos - rb.position;
        float angle = Mathf.Atan2(mouseDir.y, mouseDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;

    }
}