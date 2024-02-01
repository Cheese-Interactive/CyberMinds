using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Email : MonoBehaviour {

    [Header("References")]
    protected DefenderController defenderController;
    protected GameManager gameManager;
    private Rigidbody2D rb;

    [Header("Drag")]
    [SerializeField] protected float minDrag;
    [SerializeField] protected float maxDrag;

    private void Start() {

        defenderController = FindObjectOfType<DefenderController>();
        gameManager = FindObjectOfType<GameManager>();
        rb = GetComponent<Rigidbody2D>();

        rb.drag = Random.Range(minDrag, maxDrag);

    }
}
