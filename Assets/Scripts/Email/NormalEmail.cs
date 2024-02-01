using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEmail : Email {

    [Header("Statistics")]
    [SerializeField] private float reputationGain;
    [SerializeField] private float reputationLoss;
    [SerializeField] private int sponsorsGain;
    [SerializeField] private int sponsorsLoss;

    private void OnCollisionEnter2D(Collision2D collision) {

        if (collision.gameObject.CompareTag("Company")) { // collider is company

            gameManager.AddCompanyReputation(reputationGain);
            gameManager.AddSponsors(sponsorsGain);
            Destroy(gameObject);

        } else if (collision.gameObject.CompareTag("Defender")) {  // collider is defender

            if (defenderController.HasFilter()) { // destroy with no losses because defender has filter powerup

                Destroy(gameObject);
                return;

            }

            gameManager.RemoveCompanyReputation(reputationLoss);
            gameManager.RemoveSponsors(sponsorsLoss);
            Destroy(gameObject);

        }
    }
}
