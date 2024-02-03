using DG.Tweening;
using DG.Tweening.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [Header("References")]
    private DefenderController defenderController;
    private GameUIController uiController;

    [Header("Company Reputation")]
    [SerializeField] private float maxCompanyReputation;
    private float companyReputation;

    [Header("Sponsors")]
    private int sponsorCount;

    [Header("Revenue")]
    [SerializeField] private float revenueCooldown;
    private float revenueMultiplier;
    private int totalRevenue;
    private Coroutine revenueCoroutine;

    [Header("Filter")]
    private float filterChance;

    [Header("Firewall")]
    private float firewallChance;

    [Header("Game Over")]
    [SerializeField] private float timeScaleLerpDuration;
    private bool isGameOver;

    [Header("Emails")]
    [SerializeField] private Transform emailSpawnsParent;
    [SerializeField] private Email[] emails;
    [SerializeField] private float emailSpawnRate;
    private Coroutine emailCoroutine;

    private void Awake() {

        defenderController = FindObjectOfType<DefenderController>();
        uiController = FindObjectOfType<GameUIController>();

        // company reputation
        companyReputation = maxCompanyReputation; // run in awake so defenderUIController can access it in start

        // revenue
        revenueMultiplier = 1;

        emailCoroutine = StartCoroutine(SpawnEmails());
        revenueCoroutine = StartCoroutine(GenerateRevenue());

    }

    private void OnApplicationQuit() {

        DOTween.KillAll();
        Destroy(FindObjectOfType<DOTweenComponent>());

    }

    public void AddCompanyReputation(float reputation) {

        if (isGameOver) return; // don't add reputation if game is over

        companyReputation += reputation;

        // clamp company reputation
        companyReputation = Mathf.Clamp(companyReputation, 0f, maxCompanyReputation);

        uiController.UpdateReputationSlider(); // update slider

    }

    public void RemoveCompanyReputation(float reputation) {

        if (isGameOver) return; // don't remove reputation if game is over

        companyReputation -= reputation;

        if (companyReputation <= 0)
            EndGame();

        uiController.UpdateReputationSlider(); // update slider

    }

    public float GetCompanyReputation() { return companyReputation; }

    public void AddSponsors(int sponsors) {

        if (isGameOver) return; // don't add sponsors if game is over

        sponsorCount += sponsors;
        uiController.UpdateSponsorText(sponsorCount); // update sponsor text

    }

    public void RemoveSponsors(int sponsors) {

        if (isGameOver) return; // don't remove sponsors if game is over

        sponsorCount -= sponsors;

        // clamp sponsor count
        if (sponsorCount < 0)
            sponsorCount = 0;

        uiController.UpdateSponsorText(sponsorCount); // update sponsor text

    }

    public void IncreaseRevenueMultiplier(float revenueMultiplier) {

        if (isGameOver) return; // don't increase revenue multiplier if game is over

        this.revenueMultiplier += revenueMultiplier;

    }

    public void RemoveRevenue(int revenue) {

        if (isGameOver) return; // don't remove revenue if game is over

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
            totalRevenue += (int) (sponsorCount * revenueMultiplier);
            uiController.UpdateRevenueText(totalRevenue); // update revenue text

        }
    }

    public void IncreaseFilterChance(float filterChance) {

        if (isGameOver) return; // don't increase filter chance if game is over

        this.filterChance += filterChance;

    }

    public float GetFilterChance() { return filterChance; }

    public void IncreaseFirewallChance(float firewallChance) {

        if (isGameOver) return; // don't increase firewall chance if game is over

        this.firewallChance += firewallChance;

    }

    public float GetFirewallChance() { return firewallChance; }

    private IEnumerator SpawnEmails() {

        while (true) {

            yield return new WaitForSeconds(1f / emailSpawnRate);
            Instantiate(emails[Random.Range(0, emails.Length)], emailSpawnsParent.GetChild(Random.Range(0, emailSpawnsParent.childCount)).position, Quaternion.identity);

        }
    }

    private void EndGame() {

        StopCoroutine(emailCoroutine); // stop email spawning
        StopCoroutine(revenueCoroutine); // stop revenue spawning

        isGameOver = true;
        defenderController.Kill();
        DOVirtual.Float(Time.timeScale, 0f, timeScaleLerpDuration, (x) => Time.timeScale = x).OnComplete(() => uiController.ShowGameOverScreen()).SetEase(Ease.Linear).SetUpdate(true); // lerp time scale to 0

    }

    public bool IsGameOver() { return isGameOver; }

}
