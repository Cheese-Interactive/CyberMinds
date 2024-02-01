using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [Header("References")]
    private DefenderUIController uiController;

    [Header("Company Reputation")]
    [SerializeField] private float maxCompanyReputation;
    private float companyReputation;

    [Header("Sponsors")]
    private int sponsorCount;

    [Header("Revenue")]
    [SerializeField] private float revenueCooldown;
    private int revenueMultiplier;
    private int totalRevenue;

    [Header("Emails")]
    [SerializeField] private Transform emailSpawnsParent;
    [SerializeField] private Email[] emails;
    [SerializeField] private float emailSpawnRate;

    private void Awake() {

        uiController = FindObjectOfType<DefenderUIController>();

        // company reputation
        companyReputation = maxCompanyReputation; // run in awake so defenderUIController can access it in start

        // revenue
        revenueMultiplier = 1;

        StartCoroutine(SpawnEmails());
        StartCoroutine(GenerateRevenue());

    }

    public void AddCompanyReputation(float reputation) {

        companyReputation += reputation;

        // clamp company reputation
        companyReputation = Mathf.Clamp(companyReputation, 0f, maxCompanyReputation);

        uiController.UpdateReputationSlider(); // update slider

    }

    public void RemoveCompanyReputation(float reputation) {

        companyReputation -= reputation;

        if (companyReputation <= 0)
            EndGame();

        uiController.UpdateReputationSlider(); // update slider

    }

    public float GetCompanyReputation() { return companyReputation; }

    public void AddSponsors(int sponsors) {

        sponsorCount += sponsors;
        uiController.UpdateSponsorText(sponsorCount); // update sponsor text

    }

    public void RemoveSponsors(int sponsors) {

        sponsorCount -= sponsors;

        // clamp sponsor count
        if (sponsorCount < 0)
            sponsorCount = 0;

        uiController.UpdateSponsorText(sponsorCount); // update sponsor text

    }

    public void IncreaseRevenueMultiplier(int revenueMultiplier) {

        this.revenueMultiplier += revenueMultiplier;

    }

    public void RemoveRevenue(int revenue) {

        totalRevenue -= revenue;

        // clamp revenue
        if (totalRevenue < 0)
            totalRevenue = 0;

        uiController.UpdateRevenueText(totalRevenue); // update revenue text

    }

    public bool CanAfford(int cost) {

        return totalRevenue >= cost;

    }

    private IEnumerator GenerateRevenue() {

        while (true) {

            yield return new WaitForSeconds(revenueCooldown);
            totalRevenue += sponsorCount * revenueMultiplier;
            uiController.UpdateRevenueText(totalRevenue); // update revenue text

        }
    }

    private IEnumerator SpawnEmails() {

        while (true) {

            yield return new WaitForSeconds(1f / emailSpawnRate);
            Instantiate(emails[Random.Range(0, emails.Length)], emailSpawnsParent.GetChild(Random.Range(0, emailSpawnsParent.childCount)).position, Quaternion.identity);

        }
    }

    private void EndGame() {

    }
}
