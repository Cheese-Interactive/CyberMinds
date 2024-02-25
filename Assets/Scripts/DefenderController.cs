using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DefenderController : MonoBehaviour {

    [Header("References")]
    //[SerializeField] private SpriteRenderer firewall;
    private GameManager gameManager;
    private GameUIController uiController;
    private Rigidbody2D rb;

    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float smoothDuration;
    private float horizontalInput;

    [Header("Size Upgrade")]
    [SerializeField] private Upgrade sizeUpgrade;
    [SerializeField] private float sizeLerpDuration;
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

    [Header("Death")]
    [SerializeField] private ParticleSystem destroyEffect;

    [Header("Keybinds")]
    [SerializeField] private KeyCode sizeUpgradeKey;
    [SerializeField] private KeyCode filterUpgradeKey;
    [SerializeField] private KeyCode revenueUpgradeKey;
    //[SerializeField] private KeyCode firewallUpgradeKey;

    private void Start() {

        gameManager = FindObjectOfType<GameManager>();
        uiController = FindObjectOfType<GameUIController>();
        rb = GetComponent<Rigidbody2D>();

        sizeUpgrade.SetKeybindText(sizeUpgradeKey.ToString());
        filterUpgrade.SetKeybindText(filterUpgradeKey.ToString());
        revenueUpgrade.SetKeybindText(revenueUpgradeKey.ToString());
        //firewallUpgrade.SetKeybindText(firewallUpgradeKey.ToString());

        startScale = transform.localScale.x;
        //firewall.gameObject.SetActive(false);

    }

    private void Update() {

        horizontalInput = Input.GetAxis("Horizontal");

        //// mouse control
        //Vector3 position = new Vector2(Mathf.SmoothDamp(transform.position.x, Camera.main.ScreenToWorldPoint(Input.mousePosition).x, ref currVelocity, smoothDuration), transform.position.y);
        //float distance = transform.position.z - Camera.main.transform.position.z;

        //position.x = Mathf.Clamp(position.x, Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance)).x + (transform.localScale.x / 2f), Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance)).x - (transform.localScale.x / 2f));
        //transform.position = position;

        // upgrades
        if (Input.GetKeyDown(sizeUpgradeKey)) {

            if (gameManager.CanAfford(sizeUpgrade.GetCost())) { // can afford

                sizeUpgrade.Purchase();
                gameManager.RemoveRevenue(sizeUpgrade.GetCost());
                transform.DOScale((Vector2.one * startScale) + (Vector2.one * ((minScale - startScale) * ((float) sizeUpgrade.GetCurrentStage() / (float) sizeUpgrade.GetTotalStages()))), sizeLerpDuration);

                uiController.UpdateUpgradesLayout(); // update upgrades layout

            }
        }

        if (Input.GetKeyDown(filterUpgradeKey)) {

            if (gameManager.CanAfford(filterUpgrade.GetCost())) { // can afford

                filterUpgrade.Purchase();
                gameManager.RemoveRevenue(filterUpgrade.GetCost());
                gameManager.IncreaseFilterChance(((float) filterUpgrade.GetCurrentStage() / (float) filterUpgrade.GetTotalStages()) * maxFilterChance);

                uiController.UpdateUpgradesLayout(); // update upgrades layout

            }
        }

        if (Input.GetKeyDown(revenueUpgradeKey)) {

            if (gameManager.CanAfford(revenueUpgrade.GetCost())) { // can afford

                revenueUpgrade.Purchase();
                gameManager.RemoveRevenue(revenueUpgrade.GetCost());
                gameManager.IncreaseRevenueMultiplier(((float) revenueUpgrade.GetCurrentStage() / (float) revenueUpgrade.GetTotalStages()) * maxRevenueMultiplier);

                uiController.UpdateUpgradesLayout(); // update upgrades layout

            }
        }

        //if (Input.GetKeyDown(firewallUpgradeKey)) {

        //    if (gameManager.CanAfford(firewallUpgrade.GetCost())) { // can afford

        //        // if first stage, activate firewall
        //        if (firewallUpgrade.GetCurrentStage() == 0) {

        //            firewall.color = new Color(firewall.color.r, firewall.color.g, firewall.color.b, 0f); // reset alpha for fade
        //            firewall.gameObject.SetActive(true);
        //            firewall.DOFade(1f, firewallFadeDuration);

        //        }

        //        firewallUpgrade.Purchase();
        //        gameManager.RemoveRevenue(firewallUpgrade.GetCost());

        //        gameManager.IncreaseFirewallChance(((float) firewallUpgrade.GetCurrentStage() / (float) firewallUpgrade.GetTotalStages()) * maxFirewallChance);

        //        uiController.UpdateUpgradesLayout(); // update upgrades layout

        //    }
        //}
    }

    private void FixedUpdate() {

        // move defender
        rb.velocity = Vector2.right * horizontalInput * moveSpeed;

    }

    public void Kill() {

        Instantiate(destroyEffect, transform.position, Quaternion.Euler(0f, 0f, 0f)); // spawn destroy effect
        Destroy(gameObject);

    }
}
