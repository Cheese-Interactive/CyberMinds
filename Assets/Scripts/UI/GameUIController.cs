using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour {

    [Header("References")]
    private GameManager gameManager;

    [Header("Statistics")]
    [SerializeField] private RectTransform statisticsLayout;

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

    [Header("Upgrades")]
    [SerializeField] private RectTransform upgradesLayout;

    [Header("Loading Screen")]
    [SerializeField] private CanvasGroup loadingScreen;
    [SerializeField] private float loadingFadeDuration;

    [Header("Game Over Screen")]
    [SerializeField] private CanvasGroup gameOverScreen;
    [SerializeField] private float gameOverFadeDuration;
    [SerializeField] private Button replayButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button quitButton;
    private AsyncOperation levelLoadOperation;

    [Header("Scenes")]
    [SerializeField] private int mainMenuSceneIndex;

    private void Start() {

        gameManager = FindObjectOfType<GameManager>();

        // company reputation
        reputationSlider.maxValue = gameManager.GetCompanyReputation(); // set to max at this point
        reputationSlider.value = reputationSlider.maxValue; // set value to max value

        // loading screen
        loadingScreen.gameObject.SetActive(true);
        loadingScreen.DOFade(0f, loadingFadeDuration).SetEase(Ease.OutCirc).SetUpdate(true).OnComplete(() => loadingScreen.gameObject.SetActive(false)); // set update to true to update while game pauses when game ends, deactivate loading screen after fade out

        // game over screen
        gameOverScreen.alpha = 0; // set alpha to 0 for fade in
        gameOverScreen.gameObject.SetActive(false); // disable game over screen

        replayButton.onClick.AddListener(ReplayLevel);
        mainMenuButton.onClick.AddListener(LoadMainMenu);
        quitButton.onClick.AddListener(() => Application.Quit());

        // update layouts
        UpdateStatisticsLayout();
        UpdateUpgradesLayout();

    }

    public void UpdateReputationSlider() {

        DOTween.Kill(reputationTween);
        reputationTween = reputationSlider.DOValue(gameManager.GetCompanyReputation(), reputationUpdateDuration).SetUpdate(true); // set update to true to update when game ends

        UpdateStatisticsLayout();

    }

    public void UpdateSponsorText(int sponsors) {

        DOTween.Kill(sponsorsTween);
        sponsorsTween = DOVirtual.Int(prevSponsors, sponsors, revenueUpdateDuration, (x) => sponsorText.text = "Sponsors: " + x);
        prevSponsors = sponsors;

        UpdateStatisticsLayout();

    }

    public void UpdateRevenueText(int revenue) {

        DOTween.Kill(revenueTween);
        revenueTween = DOVirtual.Int(prevRevenue, revenue, revenueUpdateDuration, (x) => revenueText.text = "Revenue: $" + x);
        prevRevenue = revenue;

        UpdateStatisticsLayout();

    }

    private void UpdateStatisticsLayout() {

        LayoutRebuilder.ForceRebuildLayoutImmediate(statisticsLayout); // force rebuild layout

    }

    public void UpdateUpgradesLayout() {

        LayoutRebuilder.ForceRebuildLayoutImmediate(upgradesLayout); // force rebuild layout

    }

    private void ReplayLevel() {

        levelLoadOperation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        levelLoadOperation.allowSceneActivation = false;

        ShowLoadingScreen();

    }

    private void LoadMainMenu() {

        levelLoadOperation = SceneManager.LoadSceneAsync(mainMenuSceneIndex);
        levelLoadOperation.allowSceneActivation = false;

        ShowLoadingScreen();

    }

    private void ShowLoadingScreen() {

        LayoutRebuilder.MarkLayoutForRebuild(loadingScreen.GetComponent<RectTransform>()); // force rebuild layout

        loadingScreen.gameObject.SetActive(true);
        loadingScreen.DOFade(1f, loadingFadeDuration).SetEase(Ease.OutCirc).SetUpdate(true).OnComplete(() => {

            Time.timeScale = 1f; // unpause game
            levelLoadOperation.allowSceneActivation = true;

        }); // set update to true to update while game pauses when game ends
    }

    public void ShowGameOverScreen() {

        LayoutRebuilder.ForceRebuildLayoutImmediate(gameOverScreen.GetComponent<RectTransform>()); // force rebuild layout

        gameOverScreen.gameObject.SetActive(true);
        gameOverScreen.DOFade(1f, gameOverFadeDuration).SetEase(Ease.OutCirc).SetUpdate(true); // set update to true to update while game pauses when game ends

    }
}
