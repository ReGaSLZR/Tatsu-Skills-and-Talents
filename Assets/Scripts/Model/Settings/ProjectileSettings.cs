using UnityEngine;

[System.Serializable]
public class ProjectileSettings
{
    
	public GameObject prefab;

	//TODO ren
	//public Transform spawnPoint;
	
	public uint spawnCount;
	public float spawnInterval;

	public GameObject targetHitVFX;

	public ProjectileSettings(GameObject prefab, uint spawnCount, 
		float spawnInterval, GameObject targetHitVFX)
	{ 
		this.prefab = prefab;
		this.spawnCount = spawnCount;
		this.spawnInterval = spawnInterval;
		this.targetHitVFX = targetHitVFX;
	}
	
}
