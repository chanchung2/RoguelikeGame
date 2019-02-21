using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyController : MonoBehaviour
{
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    void Start()
    {

    }

    void Update()
    {
        BodyChange();
    }

    private void BodyChange()
    {
        if (Input.GetKey(KeyCode.A))
        {
            spriteRenderer.flipX = true;
            anim.Play("Body_SideWalk");
        }
        else if (Input.GetKey(KeyCode.D))
        {
            spriteRenderer.flipX = false;
            anim.Play("Body_SideWalk");
        }
        else if (Input.GetKey(KeyCode.W))
        {
            spriteRenderer.flipX = false;
            anim.Play("Body_BackWalk");
        }
        else if (Input.GetKey(KeyCode.S))
        {
            spriteRenderer.flipX = false;
            anim.Play("Body_Walk");
        }
        else
        {
            spriteRenderer.flipX = false;
            anim.Play("Body_Idle");
        }
    }
}
