using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    public GameObject playerPrefab;
    public Transform defaultSpawnPoint;

    private void Start()
    {
        if (Movement.Instance == null)
        {
            GameObject player = Instantiate(
                playerPrefab,
                defaultSpawnPoint.position,
                Quaternion.identity
            );

            player.transform.position = new Vector3(
                defaultSpawnPoint.position.x,
                defaultSpawnPoint.position.y,
                0f
            );
        }
    }
}