using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private MeleeEnemy meleeEnemyPrefab;
    [SerializeField] private GameObject rangedEnemyHolderPrefab;
    [SerializeField] private float spawnInterval = 5f;
    [SerializeField] private PlayerMovement player;
    [SerializeField] private RoomBounds room1Bounds;
    [SerializeField] private RoomBounds room2Bounds;
    [SerializeField] private float groundOffset = 1f;
    [SerializeField] private LayerMask groundLayer;

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            int randomEnemy = Random.Range(0, 2); // Choose between 0 and 1

            RoomBounds activeRoomBounds = GetActiveRoomBounds();

            // Calculate the new spawn position
            float xOffset = 3f;
            float newX = Mathf.Clamp(player.transform.position.x + xOffset, activeRoomBounds.GetBounds().min.x, activeRoomBounds.GetBounds().max.x);
            Vector3 spawnPosition = new Vector3(newX, transform.position.y, transform.position.z);

            // Get the correct y-position for spawning the enemy
            float groundY = GetGroundYPosition(spawnPosition);
            spawnPosition.y = groundY;
            
            if (randomEnemy == 0)
            {
                Enemy meleeEnemy = meleeEnemyPrefab.Clone();
                meleeEnemy.transform.position = spawnPosition;
            }
            else
            {
                GameObject rangedEnemyHolder = Instantiate(rangedEnemyHolderPrefab);
                rangedEnemyHolder.transform.position = spawnPosition;
            }
        }
    }

    private RoomBounds GetActiveRoomBounds()
    {
        if (room1Bounds.GetBounds().Contains(player.transform.position))
        {
            return room1Bounds;
        }
        else
        {
            return room2Bounds;
        }
    }

    private float GetGroundYPosition(Vector3 spawnPosition)
    {
        RaycastHit2D hit = Physics2D.Raycast(spawnPosition, Vector2.down, Mathf.Infinity, groundLayer);

        if (hit.collider != null)
        {
            return hit.point.y + groundOffset;
        }

        return spawnPosition.y;
    }
}
