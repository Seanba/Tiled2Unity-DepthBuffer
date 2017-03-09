using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadaxBahaviour : MonoBehaviour
{
    private static float MovePixelsPerSecond = 60.0f;

    private void Update()
    {
        Animator animator = GetComponent<Animator>();

        Vector2 input = Vector2.zero;
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        // Y input trumps X. Not diagnoal input.
        if (input.x != 0)
        {
            input.y = 0;
        }

        // Are we walking or standing?
        if (input.SqrMagnitude() > 0)
        {
            animator.SetFloat("Speed", 1.0f);
        }
        else
        {
            animator.SetFloat("Speed", 0.0f);
        }

        Vector2 dv = Vector2.zero;

        // Which direction are we facing?
        if (input.x == 0 && input.y == 0)
        {
            // Not moving. Do not update direction.
        }
        if (input.x != 0)
        {
            // Either left or right
            animator.SetFloat("Direction_x", input.x);
            animator.SetFloat("Direction_y", 0);

            dv.x = 1.0f * Mathf.Sign(input.x);
            dv.y = 0.5f * -Mathf.Sign(input.x);
        }
        else if (input.y != 0)
        {
            // Either up or down
            animator.SetFloat("Direction_x", 0);
            animator.SetFloat("Direction_y", input.y);

            dv.x = 1.0f * Mathf.Sign(input.y);
            dv.y = 0.5f * Mathf.Sign(input.y);
        }

        if (dv.SqrMagnitude() > 0)
        {
            dv.Normalize();
            this.gameObject.transform.Translate(dv * Time.deltaTime * MovePixelsPerSecond);
        }
    }

}
