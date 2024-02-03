using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUIController : MonoBehaviour {

    [Header("References")]
    [SerializeField] private RectTransform mainMenuLayout;

    [Header("Menu Buttons")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;

    [Header("Loading Screen")]
    [SerializeField] private CanvasGroup loadingScreen;
    [SerializeField] private float loadingFadeDuration;

    [Header("Scenes")]
    [SerializeField] private int gameSceneIndex;
    private AsyncOperation levelLoadOperation;

    private void Start() {

        LayoutRebuilder.ForceRebuildLayoutImmediate(mainMenuLayout); // force rebuild layout

        // menu buttons
        playButton.onClick.AddListener(LoadLevel);
        quitButton.onClick.AddListener(() => Application.Quit());

        // loading screen
        loadingScreen.gameObject.SetActive(true);
        loadingScreen.DOFade(0f, loadingFadeDuration).SetEase(Ease.OutCirc).SetUpdate(true).OnComplete(() => loadingScreen.gameObject.SetActive(false)); // set update to true to update while game pauses when game ends, deactivate loading screen after fade out

    }

    private void LoadLevel() {

        levelLoadOperation = SceneManager.LoadSceneAsync(gameSceneIndex);
        levelLoadOperation.allowSceneActivation = false;

        ShowLoadingScreen();

    }

    private void ShowLoadingScreen() {

        LayoutRebuilder.MarkLayoutForRebuild(loadingScreen.GetComponent<RectTransform>()); // force rebuild layout

        loadingScreen.gameObject.SetActive(true);
        loadingScreen.DOFade(1f, loadingFadeDuration).SetEase(Ease.OutCirc).SetUpdate(true).OnComplete(() => levelLoadOperation.allowSceneActivation = true); // set update to true to update while game pauses when game ends

    }
}
