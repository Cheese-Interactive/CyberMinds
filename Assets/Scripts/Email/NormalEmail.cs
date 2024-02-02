using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEmail : Email {

    [Header("Statistics")]
    [SerializeField] private float reputationGain;
    [SerializeField] private float reputationLoss;
    [SerializeField] private int sponsorsGain;
    [SerializeField] private int sponsorsLoss;

    [Header("Deflection")]
    [SerializeField] private float deflectionForce;

    [Header("Animation")]
    [SerializeField] private float fadeDuration;

    private void OnCollisionEnter2D(Collision2D collision) {

        print(collision.collider.name);

        if (collision.collider.CompareTag("Company")) { // collider is company

            // good
            gameManager.AddCompanyReputation(reputationGain);
            gameManager.AddSponsors(sponsorsGain);
            SelfDestruct();

        } else if (collision.collider.CompareTag("Defender")) {  // collider is defender

            // check if defender filters email
            int filterRoll = Random.Range(0, 100);

            if (filterRoll < gameManager.GetFilterChance()) { // destroy with no losses because defender filters email

                // good
                collider.enabled = false;
                spriteRenderer.DOFade(0f, fadeDuration).OnComplete(() => SelfDestruct());
                return;

            }

            // bad
            gameManager.RemoveCompanyReputation(reputationLoss);
            gameManager.RemoveSponsors(sponsorsLoss);
            SelfDestruct();

        } else if (collision.collider.CompareTag("Firewall")) {  // collider is firewall

            // check if firewall deflects email
            int firewallRoll = Random.Range(0, 100);

            if (firewallRoll < gameManager.GetFirewallChance()) { // destroy with no losses because firewall deflects email

                // good
                collider.enabled = false;
                rb.AddForce(Vector2.up * deflectionForce, ForceMode2D.Impulse);
                spriteRenderer.DOFade(0f, fadeDuration).OnComplete(() => SelfDestruct());
                return;

            }

            // bad
            gameManager.RemoveCompanyReputation(reputationLoss);
            gameManager.RemoveSponsors(sponsorsLoss);
            SelfDestruct();

        }
    }
}
