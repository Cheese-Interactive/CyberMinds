using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DefenderController : MonoBehaviour {

    [Header("References")]
    private GameManager gameManager;

    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float smoothDuration;
    private float currVelocity;

    [Header("Filter")]
    private bool hasFilter;

    [Header("Size Upgrade")]
    [SerializeField] private Upgrade sizeUpgrade;
    [SerializeField] private float finalScale;
    private float startScale; // scale has to be uniform

    [Header("Filter Upgrade")]
    [SerializeField] private Upgrade filterUpgrade;

    [Header("Filter Upgrade")]
    [SerializeField] private Upgrade revenueUpgrade;

    [Header("Keybinds")]
    [SerializeField] private KeyCode sizeUpgradeKey;
    [SerializeField] private KeyCode filterUpgradeKey;
    [SerializeField] private KeyCode revenueUpgradeKey;

    private void Start() {

        gameManager = FindObjectOfType<GameManager>();

        sizeUpgrade.SetKeybindText(sizeUpgradeKey.ToString());
        startScale = transform.localScale.x;

        filterUpgrade.SetKeybindText(filterUpgradeKey.ToString());

        revenueUpgrade.SetKeybindText(revenueUpgradeKey.ToString());

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
                transform.localScale = (Vector3.one * startScale) + (Vector3.one * ((finalScale - startScale) * ((float) sizeUpgrade.GetCurrentStage() / (float) sizeUpgrade.GetTotalStages())));

            }
        }

        if (Input.GetKeyDown(filterUpgradeKey)) {

            if (gameManager.CanAfford(filterUpgrade.GetCost())) { // can afford

                filterUpgrade.Purchase();
                gameManager.RemoveRevenue(filterUpgrade.GetCost());
                SetHasFilter(true);

            }
        }

        if (Input.GetKeyDown(revenueUpgradeKey)) {

            if (gameManager.CanAfford(revenueUpgrade.GetCost())) { // can afford

                revenueUpgrade.Purchase();
                gameManager.RemoveRevenue(revenueUpgrade.GetCost());
                gameManager.IncreaseRevenueMultiplier(1);

            }
        }
    }

    public void SetHasFilter(bool hasFilter) {

        this.hasFilter = hasFilter;

    }

    public bool HasFilter() { return hasFilter; }

}
