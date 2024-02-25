using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

    [SerializeField] private string sceneName;

    private void Start() {

        // rebuild all layouts
        foreach (LayoutGroup group in FindObjectsOfType<LayoutGroup>())
            LayoutRebuilder.ForceRebuildLayoutImmediate(group.GetComponent<RectTransform>());

    }

    public void ChangeScene() {

        SceneManager.LoadScene(sceneName);

    }
}
