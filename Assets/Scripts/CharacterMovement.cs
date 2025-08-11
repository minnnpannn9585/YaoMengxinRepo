using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator anim;
    public float speed = 0.4f;

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if(vertical > 0)
        {
            anim.SetBool("isMoving", true);
            anim.SetBool("isFront", false);
        }
        else if(vertical < 0)
        {
            anim.SetBool("isMoving", true);
            anim.SetBool("isFront", true);
        }
        else if(vertical == 0)
        {
            anim.SetBool("isMoving", false);
        }


        rb.velocity = (new Vector2(vertical * Mathf.Cos(Mathf.PI / 6), vertical * Mathf.Sin(Mathf.PI / 6)) + new Vector2(horizontal, -horizontal) )* speed;
    }
}
