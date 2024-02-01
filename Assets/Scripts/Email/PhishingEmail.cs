using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhishingEmail : Email {

    [Header("Statistics")]
    [SerializeField] private float reputationLoss;

    private void OnCollisionEnter2D(Collision2D collision) {

        if (collision.gameObject.CompareTag("Company")) { // collider is defender

            gameManager.RemoveCompanyReputation(reputationLoss);
            Destroy(gameObject);

        } else if (collision.gameObject.CompareTag("Defender")) {  // collider is defender

            Destroy(gameObject);

        }
    }
}
