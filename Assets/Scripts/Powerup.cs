using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Powerup : MonoBehaviour {

    [Header("References")]
    protected DefenderController defenderController;
    protected SpriteRenderer spriteRenderer;
    protected new Collider2D collider;

    [Header("Settings")]
    [SerializeField] protected float powerupLength;

    private void Awake() {

        defenderController = FindObjectOfType<DefenderController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<Collider2D>();

    }

    protected abstract void OnTriggerEnter2D(Collider2D collision);

    protected void DisableCollisions() {

        spriteRenderer.enabled = false;
        collider.enabled = false;

    }

    protected abstract void ResetEffect();

}
