using UnityEngine;

public class EnemyPool : Pool
{
	[SerializeField] private int initialCount;
	[SerializeField] private CircleCollider2D[] spawnPoints;

	private void Awake()
	{
		for (int i = 0; i < initialCount; ++i)
		{
			CircleCollider2D spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
			Vector2 circlePos = Random.insideUnitCircle * spawnPoint.radius;
			Vector3 spawnPos = new Vector3(circlePos.x, circlePos.y, 0.0f) + spawnPoint.transform.position;
			AddObject(Random.Range(0, m_prefabs.Length), spawnPos);
		}
	}
}
