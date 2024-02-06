using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheckScript : MonoBehaviour
{

    public static bool IsGrounded;
    private void Update()
    {
        Debug.Log(IsGrounded);
    }
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IsGrounded = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        IsGrounded = false;
    }
}
 