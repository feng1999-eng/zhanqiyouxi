using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 选关界面人物简单的控制器脚本
/// </summary>
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();    
        GameAPP.CameraManager.SetPos(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        if (h == 0)
        {
            animator.Play("idle");
        }
        else
        {
            if (h>0&&transform.localScale.x<0 || h<0&&transform.localScale.x>0)
            {
                Flip();
            }
            Vector3 pos = transform.position + Vector3.right * h *moveSpeed * Time.deltaTime;
            pos.x = Mathf.Clamp(pos.x, -32, 24);
            transform.position = pos;
            GameAPP.CameraManager.SetPos(transform.position);
            animator.Play("move");
        }
    }
    //转向
    public void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
