using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Email : MonoBehaviour {

    [Header("References")]
    protected DefenderController defenderController;
    protected SpriteRenderer spriteRenderer;
    protected GameManager gameManager;
    protected new Collider2D collider;
    protected Rigidbody2D rb;

    [Header("Drag")]
    [SerializeField] protected float minDrag;
    [SerializeField] protected float maxDrag;

    [Header("Destroy")]
    [SerializeField] protected ParticleSystem destroyEffect;

    private void Start() {

        defenderController = FindObjectOfType<DefenderController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameManager = FindObjectOfType<GameManager>();
        collider = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();

        rb.drag = Random.Range(minDrag, maxDrag);

    }

    protected void SelfDestruct() {

        Instantiate(destroyEffect, transform.position, Quaternion.Euler(0f, 0f, 0f)); // spawn destroy effect
        Destroy(gameObject);

    }
}
