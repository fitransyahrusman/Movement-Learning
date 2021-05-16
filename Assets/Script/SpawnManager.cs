using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] 
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyPrefab2;
    [SerializeField]
    private GameObject _asteroid;
    [SerializeField]
    private GameObject[] powerups;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject _asteroidContainer;
    [SerializeField]
    private GameObject _PowerupContainer;
    [SerializeField]
    private bool _stopSpawning = false;
    [SerializeField]
    private int _enemyInstance = 0;
    

    public void StartSpawning()
    {
        StartCoroutine("SpawnEnemy");
        StartCoroutine("SpawnAsteroid");
        StartCoroutine(SpawnRandomPowerup());
        StartCoroutine(SpawnEnemy2());
    }

    IEnumerator SpawnEnemy ()
    {
        do
        {
            float _spawnTimeEnemy = Random.Range(0.5f, 1f); //makespawntime random
            while (_stopSpawning == false && _enemyInstance < 51)
            {
                GameObject newEnemy = Instantiate(_enemyPrefab, new Vector3(12f, Random.Range(-4.5f, 4.5f), 0f), Quaternion.identity) ;
                newEnemy.transform.parent = _enemyContainer.transform;
                yield return new WaitForSeconds(_spawnTimeEnemy);
                _enemyInstance++;
            }
        }
        while (_stopSpawning == false);
    }
    IEnumerator SpawnEnemy2()
    {
        while (_stopSpawning == false)
        {
            int _spawnTimeEnemy2 = Random.Range(20,25); //makespawntime random
            yield return new WaitForSeconds(_spawnTimeEnemy2);
            GameObject newEnemy1 = Instantiate(_enemyPrefab2, new Vector2(11.5f, Random.Range(-4.5f, 4.5f)), Quaternion.identity);
            GameObject newEnemy2 = Instantiate(_enemyPrefab2, new Vector2(11.5f, Random.Range(-4.5f, 4.5f)), Quaternion.identity);
            GameObject newEnemy3 = Instantiate(_enemyPrefab2, new Vector2(11.5f, Random.Range(-4.5f, 4.5f)), Quaternion.identity);
            newEnemy1.transform.parent = _enemyContainer.transform;
            newEnemy2.transform.parent = _enemyContainer.transform;
            newEnemy3.transform.parent = _enemyContainer.transform;
        }
    }
    IEnumerator SpawnAsteroid()
    {
        while (_stopSpawning == false)
        {
            int _spawnTimeAsteroid = Random.Range(10, 15); //makespawntime random
            yield return new WaitForSeconds(_spawnTimeAsteroid);
            GameObject newAsteroid = Instantiate(_asteroid, new Vector2(Random.Range(-8f, 0f), 7.2f), Quaternion.identity);
            newAsteroid.transform.parent = _asteroidContainer.transform;
        }  
    }
    IEnumerator SpawnRandomPowerup()
    {
        while (_stopSpawning == false)
        {
            int _spawnTimePowerUp = Random.Range(15, 20); //makespawntime random
            yield return new WaitForSeconds(_spawnTimePowerUp);
            int randompowerups = Random.Range (0,3 );
            GameObject newPowerup = Instantiate( powerups[randompowerups] , new Vector2(11.5f, Random.Range(-4.5f, 4.5f)), Quaternion.identity);
            newPowerup.transform.parent = _PowerupContainer.transform;
        }
    }
    public void OnPlayerDeath()
    {
        _stopSpawning=true;
    }
}
