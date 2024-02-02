using DG.Tweening;
using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Upgrade {

    [Header("References")]
    [SerializeField] private Image backgroundImage;
    [SerializeField] private TMP_Text keybindText;
    [SerializeField] private TMP_Text costText;

    [Header("Settings")]
    [SerializeField] private int cost;
    [SerializeField] private int costMultiplier;
    [SerializeField] private int totalStages;
    private int currStage; // indexed from 0

    [Header("Animations")]
    [SerializeField] private float popoutDuration;
    [SerializeField] private float popoutScale;
    private Vector3 startScale;

    // start function
    public void SetKeybindText(string keybind) {

        currStage = 0;

        startScale = backgroundImage.transform.localScale;

        keybindText.text = keybind;
        UpdateCostText();

    }

    public bool CanPurchase() {

        return currStage < totalStages; // can't purchase if at max stage

    }

    public void Purchase() {

        if (!CanPurchase()) return;

        backgroundImage.transform.DOScale(popoutScale, popoutDuration / 2f).OnComplete(() => backgroundImage.transform.DOScale(startScale, popoutDuration / 2f));

        cost *= costMultiplier; // increase cost

        if (currStage <= totalStages)
            currStage++;

        UpdateCostText();

    }

    private void UpdateCostText() {

        if (!CanPurchase()) {

            costText.text = "Max";
            return;

        }

        costText.text = "$" + cost;

    }

    public int GetCost() { return cost; }

    public int GetTotalStages() { return totalStages; }

    public int GetCurrentStage() { return currStage; }

}
