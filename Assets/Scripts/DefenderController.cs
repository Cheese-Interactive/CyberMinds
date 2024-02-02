using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DefenderController : MonoBehaviour {

    [Header("References")]
    [SerializeField] private SpriteRenderer firewall;
    private GameManager gameManager;

    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float smoothDuration;
    private float currVelocity;

    [Header("Size Upgrade")]
    [SerializeField] private Upgrade sizeUpgrade;
    [SerializeField] private float minScale;
    private float startScale; // scale has to be uniform

    [Header("Filter Upgrade")]
    [SerializeField] private Upgrade filterUpgrade;
    [SerializeField][Range(0f, 100f)] private float maxFilterChance;

    [Header("Revenue Upgrade")]
    [SerializeField] private Upgrade revenueUpgrade;
    [SerializeField] private float maxRevenueMultiplier;

    [Header("Firewall Upgrade")]
    [SerializeField] private Upgrade firewallUpgrade;
    [SerializeField][Range(0f, 100f)] private int maxFirewallChance;
    [SerializeField] private float firewallFadeDuration;

    [Header("Keybinds")]
    [SerializeField] private KeyCode sizeUpgradeKey;
    [SerializeField] private KeyCode filterUpgradeKey;
    [SerializeField] private KeyCode revenueUpgradeKey;
    [SerializeField] private KeyCode firewallUpgradeKey;

    private void Start() {

        gameManager = FindObjectOfType<GameManager>();

        firewall.gameObject.SetActive(false);

        sizeUpgrade.SetKeybindText(sizeUpgradeKey.ToString());
        startScale = transform.localScale.x;

        filterUpgrade.SetKeybindText(filterUpgradeKey.ToString());

        revenueUpgrade.SetKeybindText(revenueUpgradeKey.ToString());

        firewallUpgrade.SetKeybindText(firewallUpgradeKey.ToString());

    }

    private void Update() {

        Vector3 position = new Vector2(Mathf.SmoothDamp(transform.position.x, Camera.main.ScreenToWorldPoint(Input.mousePosition).x, ref currVelocity, smoothDuration), transform.position.y);
        float distance = transform.position.z - Camera.main.transform.position.z;

        position.x = Mathf.Clamp(position.x, Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance)).x + (transform.localScale.x / 2f), Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance)).x - (transform.localScale.x / 2f));
        transform.position = position;

        // upgrades
        if (Input.GetKeyDown(sizeUpgradeKey)) {

            if (gameManager.CanAfford(sizeUpgrade.GetCost())) { // can afford

                sizeUpgrade.Purchase();
                gameManager.RemoveRevenue(sizeUpgrade.GetCost());
                transform.localScale = (Vector3.one * startScale) + (Vector3.one * ((minScale - startScale) * ((float) sizeUpgrade.GetCurrentStage() / (float) sizeUpgrade.GetTotalStages())));

            }
        }

        if (Input.GetKeyDown(filterUpgradeKey)) {

            if (gameManager.CanAfford(filterUpgrade.GetCost())) { // can afford

                filterUpgrade.Purchase();
                gameManager.RemoveRevenue(filterUpgrade.GetCost());
                gameManager.IncreaseFilterChance(((float) filterUpgrade.GetCurrentStage() / (float) filterUpgrade.GetTotalStages()) * maxFilterChance);

            }
        }

        if (Input.GetKeyDown(revenueUpgradeKey)) {

            if (gameManager.CanAfford(revenueUpgrade.GetCost())) { // can afford

                revenueUpgrade.Purchase();
                gameManager.RemoveRevenue(revenueUpgrade.GetCost());
                gameManager.IncreaseRevenueMultiplier(((float) revenueUpgrade.GetCurrentStage() / (float) revenueUpgrade.GetTotalStages()) * maxRevenueMultiplier);

            }
        }

        if (Input.GetKeyDown(firewallUpgradeKey)) {

            if (gameManager.CanAfford(firewallUpgrade.GetCost())) { // can afford

                // if first stage, activate firewall
                if (firewallUpgrade.GetCurrentStage() == 0) {

                    firewall.color = new Color(firewall.color.r, firewall.color.g, firewall.color.b, 0f); // reset alpha for fade
                    firewall.gameObject.SetActive(true);
                    firewall.DOFade(1f, firewallFadeDuration);

                }

                firewallUpgrade.Purchase();
                gameManager.RemoveRevenue(firewallUpgrade.GetCost());

                gameManager.IncreaseFirewallChance(((float) firewallUpgrade.GetCurrentStage() / (float) firewallUpgrade.GetTotalStages()) * maxFirewallChance);

            }
        }
    }
}
