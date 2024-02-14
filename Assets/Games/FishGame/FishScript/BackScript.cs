using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackScript : MonoBehaviour
{
    [SerializeField] private Vector2 MovementSpeed;
    private Vector2 offset;
    private Material material;
    private Rigidbody2D rb;
    private void Awake()
    {
        material = GetComponent<SpriteRenderer>().material;
        rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        offset = (rb.velocity.x * 0.1f) * MovementSpeed * Time.deltaTime;
        material.mainTextureOffset += offset;
    }
}
