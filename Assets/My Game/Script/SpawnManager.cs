using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _asteroid;
    [SerializeField]
    private GameObject[] powerups;
    //[SerializeField]
    //private float _spawnTimeEnemy = 0f;
    [SerializeField]
    private GameObject _enemyContainer;
    //[SerializeField]
    //private float _spawnTimeAsteroid = 0f;
    [SerializeField]
    private GameObject _asteroidContainer;
    //[SerializeField]
    //private float _spawnTimePowerup = 0f;
    [SerializeField]
    private GameObject _PowerupContainer;
    private bool _stopSpawning = false;
    void Start()
    {
        StartCoroutine("SpawnEnemy");
        StartCoroutine("SpawnAsteroid");
        StartCoroutine(SpawnRandomPowerup());
    }

    IEnumerator SpawnEnemy ()
    {
        while (_stopSpawning == false)
        {
            float _spawnTimeEnemy = Random.Range(0.1f, 1.5f);
            GameObject newEnemy = Instantiate(_enemyPrefab, new Vector3(12f, Random.Range(-4.5f, 4.5f), 0f), Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;

            yield return new WaitForSeconds( _spawnTimeEnemy );
        }
    }
    IEnumerator SpawnAsteroid()
    {
        while (_stopSpawning == false)
        {
            float _spawnTimeAsteroid = Random.Range(2f, 5f);
            yield return new WaitForSeconds(_spawnTimeAsteroid);
            GameObject newAsteroid = Instantiate(_asteroid, new Vector2(Random.Range(0f, 12f), 7.2f), Quaternion.identity);
            newAsteroid.transform.parent = _asteroidContainer.transform;
        }  
    }
    IEnumerator SpawnRandomPowerup()
    {
        while (_stopSpawning == false)
        {
            float _spawnTimePowerUp = Random.Range (10f, 17f);
            yield return new WaitForSeconds( _spawnTimePowerUp);
            int randompowerups = Random.Range(0, 2);
            GameObject newPowerup = Instantiate( powerups[randompowerups] , new Vector2(11.5f, Random.Range(-4.5f, 4.5f)), Quaternion.identity);
            newPowerup.transform.parent = _PowerupContainer.transform;
        }
    }
    public void OnPlayerDeath ()
    {
        _stopSpawning = true;
    }
}
