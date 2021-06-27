using System.Collections;
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
    private GameObject _asteroid2;
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
        StartCoroutine(SpawnRandomPowerup());
    }
    IEnumerator SpawnEnemy ()
    {
        do
        {
            float _spawnTimeEnemy = Random.Range(0.5f, 1.5f); //makespawntime random
            while (_stopSpawning == false )
            {
                GameObject newEnemy = Instantiate(_enemyPrefab, new Vector3(12f, Random.Range(-4.5f, 4.5f), 0f), Quaternion.identity) ;
                newEnemy.transform.parent = _enemyContainer.transform;
                yield return new WaitForSeconds(_spawnTimeEnemy);
            }
        }
        while (_stopSpawning == false);
    }
    IEnumerator SpawnEnemy2()
    {
        do
        {
            float _spawnTimeEnemy2 = Random.Range(2f, 5f); //makespawntime random
            while (_stopSpawning == false)
            {
                GameObject newEnemy = Instantiate(_enemyPrefab2, new Vector2(11.5f, Random.Range(-4.5f, 4.5f)), Quaternion.identity);
                newEnemy.transform.parent = _enemyContainer.transform;
                yield return new WaitForSeconds(_spawnTimeEnemy2);
            }
        }
        while (_stopSpawning == false);
    }
    IEnumerator SpawnAsteroid()
    {
        do
        {
            int _spawnTimeAsteroid = Random.Range(7, 10); //makespawntime random
            while (_stopSpawning == false)
            {
                GameObject newAsteroid = Instantiate(_asteroid, new Vector2(Random.Range(-8f, 0f), 7.2f), Quaternion.identity);
                newAsteroid.transform.parent = _asteroidContainer.transform;
                yield return new WaitForSeconds(_spawnTimeAsteroid);
            }
        }
        while (_stopSpawning == false);
    }
    IEnumerator SpawnAsteroid2()
    {
        do
        {
            int _spawnTimeAsteroid = Random.Range(7, 10); //makespawntime random
            while (_stopSpawning == false)
            {
                GameObject newAsteroid = Instantiate(_asteroid2, new Vector2(Random.Range(-8f, 0f), -7.2f), Quaternion.identity);
                newAsteroid.transform.parent = _asteroidContainer.transform;
                yield return new WaitForSeconds(_spawnTimeAsteroid);
            }
        }
        while (_stopSpawning == false);
    }
    public void AddEnemyInstance()
    {
        _enemyInstance++;
        if (_enemyInstance == 50)
        {
            ActivateSpawnAsteroid();
        }
        else if (_enemyInstance == 100)
        {
            ActivateSpawnEnemy2();
        }
        else if (_enemyInstance == 150)
        {
            ActivateSpawnAsteroid2();
        } //need to add more new type enemies for enemy == 200
    }
    void ActivateSpawnAsteroid()
    {
        StartCoroutine(SpawnAsteroid());
    }
    void ActivateSpawnEnemy2()
    {
        StartCoroutine(SpawnEnemy2());
    }
    void ActivateSpawnAsteroid2()
    {
        StartCoroutine(SpawnAsteroid2());
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
