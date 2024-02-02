using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhishingEmail : Email {

    [Header("Statistics")]
    [SerializeField] private float reputationLoss;

    private void OnCollisionEnter2D(Collision2D collision) {

        if (collision.collider.CompareTag("Company")) { // collider is defender

            // bad
            gameManager.RemoveCompanyReputation(reputationLoss);
            SelfDestruct();

        } else if (collision.collider.CompareTag("Defender") || collision.collider.CompareTag("Firewall")) {  // collider is defender or firewall

            // good
            SelfDestruct();

        }
    }
}
