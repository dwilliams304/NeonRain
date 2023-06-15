using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;
    public Camera mainCam;



    Vector2 movementVector;
    Vector2 mousePos;


    void Update(){
        movementVector.x = Input.GetAxisRaw("Horizontal");
        movementVector.y = Input.GetAxis("Vertical");

        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
    }


    void FixedUpdate(){
        rb.MovePosition(rb.position + movementVector * moveSpeed * Time.fixedDeltaTime);

        Vector2 mouseDir = mousePos - rb.position;
        float angle = Mathf.Atan2(mouseDir.y, mouseDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;

    }
}
