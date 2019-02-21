using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadController : MonoBehaviour
{
    [SerializeField]
    private Sprite head_Front;
    [SerializeField]
    private Sprite head_Side;
    [SerializeField]
    private Sprite head_Back;

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HeadChange();
    }

    private void HeadChange()  // 1. 이동만 하는경우, 2. 공격만 하는 경우, 3. 이동과 공격을 하는 경우 등 상황에 맞는 Sprite 설정.
    {
        if (Input.GetKey(KeyCode.LeftArrow))  // 공격키에 대한 조건문 -> 이동과 공격이 모두 입력되면 공격에 대한 조건을 우선 처리한다. (이동중에서도 공격 방향을 바라봄)
        {
            spriteRenderer.flipX = true;
            spriteRenderer.sprite = head_Side;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            spriteRenderer.flipX = false;
            spriteRenderer.sprite = head_Side;
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            spriteRenderer.flipX = false;
            spriteRenderer.sprite = head_Back;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            spriteRenderer.flipX = false;
            spriteRenderer.sprite = head_Front;
        }
        else if (Input.GetKey(KeyCode.A))   // 이동키에 대한 조건문 -> 이동 방향에 맞게 바라봄.  
        {
            spriteRenderer.flipX = true;
            spriteRenderer.sprite = head_Side;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            spriteRenderer.flipX = false;
            spriteRenderer.sprite = head_Side;
        }
        else if (Input.GetKey(KeyCode.W))
        {
            spriteRenderer.flipX = false;
            spriteRenderer.sprite = head_Back;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            spriteRenderer.flipX = false;
            spriteRenderer.sprite = head_Front;
        }
        else
        {
            spriteRenderer.flipX = false;
            spriteRenderer.sprite = head_Front;
        }
    }
}
