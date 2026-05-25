using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public string spawnID;

    private void Start()
    {
        if (Movement.Instance == null) return;

        if (GameManager.Instance.lastSpawnID == spawnID)
        {
            Movement.Instance.transform.position = new Vector3(
                transform.position.x,
                transform.position.y,
                0f
            );
        }
    }
}