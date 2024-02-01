using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DefenderUIController : MonoBehaviour {

    [Header("References")]
    private GameManager defenderGameManager;

    [Header("Company Reputation")]
    [SerializeField] private Slider reputationSlider;
    [SerializeField] private float reputationUpdateDuration;
    private Tweener reputationTween;

    [Header("Sponsors")]
    [SerializeField] private TMP_Text sponsorText;
    [SerializeField] private float sponsorsUpdateDuration;
    private Tweener sponsorsTween;
    private int prevSponsors;

    [Header("Revenue")]
    [SerializeField] private TMP_Text revenueText;
    [SerializeField] private float revenueUpdateDuration;
    private Tweener revenueTween;
    private int prevRevenue;

    private void Start() {

        defenderGameManager = FindObjectOfType<GameManager>();

        // company reputation
        reputationSlider.maxValue = defenderGameManager.GetCompanyReputation(); // set to max at this point
        reputationSlider.value = reputationSlider.maxValue; // set value to max value

    }

    public void UpdateReputationSlider() {

        DOTween.Kill(reputationTween);
        reputationTween = reputationSlider.DOValue(defenderGameManager.GetCompanyReputation(), reputationUpdateDuration);

    }

    public void UpdateSponsorText(int sponsors) {

        DOTween.Kill(sponsorsTween);
        sponsorsTween = DOVirtual.Int(prevSponsors, sponsors, revenueUpdateDuration, (x) => sponsorText.text = "Sponsors: " + x);
        prevSponsors = sponsors;

    }

    public void UpdateRevenueText(int revenue) {

        DOTween.Kill(revenueTween);
        revenueTween = DOVirtual.Int(prevRevenue, revenue, revenueUpdateDuration, (x) => revenueText.text = "Revenue: $" + x);
        prevRevenue = revenue;

    }
}
