using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    public string sceneToLoad;
    public string targetSpawnID;

    private static bool isTransitioning = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isTransitioning) return;
        if (!other.CompareTag("Player")) return;

        if (string.IsNullOrEmpty(sceneToLoad))
        {
            Debug.LogError("Scene To Load belum diisi!");
            return;
        }

        isTransitioning = true;

        if (GameManager.Instance != null)
            GameManager.Instance.lastSpawnID = targetSpawnID;

        SceneManager.LoadScene(sceneToLoad);
    }

    private void OnEnable()
    {
        Invoke(nameof(ResetTransition), 0.5f);
    }

    private void ResetTransition()
    {
        isTransitioning = false;
    }
}