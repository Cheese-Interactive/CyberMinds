using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUIController : MonoBehaviour {

    [Header("Menu Buttons")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button instructionsButton;
    [SerializeField] private Button quitButton;

    [Header("Scenes")]
    [SerializeField] private string instructionsScene;
    [SerializeField] private string gameScene;

    private void Start() {

        playButton.onClick.AddListener(() => SceneManager.LoadScene(gameScene));
        instructionsButton.onClick.AddListener(() => SceneManager.LoadScene(instructionsScene));
        quitButton.onClick.AddListener(() => Application.Quit());

    }
}
