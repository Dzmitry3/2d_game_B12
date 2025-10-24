using System;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    public Action<bool> OnGroundStateChange;
    
    [SerializeField]  LayerMask groundLayer;
    [SerializeField]  float rayCastDistance = 0.6f;
    public bool bIsGrounded;


    private void Update()
    {
        CheckGround();
    }
    public void CheckGround()
    {
        bool hit = Physics2D.Raycast(transform.position, Vector2.down, rayCastDistance, groundLayer);
        if (hit != bIsGrounded)
        {
            bIsGrounded = hit;
            OnGroundStateChange?.Invoke(bIsGrounded);
        }

        Debug.DrawLine(transform.position, Vector2.down * rayCastDistance, bIsGrounded ? Color.green : Color.red);
    }
}