using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    
    [SerializeField]
    private GameObject[] _powerups;
    [SerializeField]
    private GameObject[] _enemy;
    [SerializeField]
    private GameObject _container;
    [SerializeField]
    private bool _stopSpawning = false;
    [SerializeField]
    private int _enemyInstance = 0;
    public void StartSpawning() // call from start cube script when the start cube destroyed
    {
        StartCoroutine("SpawnEnemy");
        StartCoroutine(SpawnRandomPowerup());
    }
    public void AddEnemyInstance() //call in enemybehaviour when laser hit enemy collider
    {
        _enemyInstance++;
        switch (_enemyInstance)
        {
            case 25:
                StartCoroutine(SpawnAsteroid());
                break;
            case 75:
                StartCoroutine(SpawnEnemy2());
                break;
            case 100:
                StartCoroutine(SpawnAsteroid2());
                break;
        }
    }
    IEnumerator SpawnEnemy ()
    {
        do
        {
            float _spawnTimeEnemy = Random.Range(0.5f, 1.5f); //makespawntime random
            while (_stopSpawning == false )
            {
                GameObject newEnemy = Instantiate(_enemy[0], new Vector3(12f, Random.Range(-4.5f, 4.5f), 0f), Quaternion.identity) ;
                newEnemy.transform.parent = _container.transform;
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
                GameObject newEnemy = Instantiate(_enemy[1], new Vector2(11.5f, Random.Range(-4.5f, 4.5f)), Quaternion.identity);
                newEnemy.transform.parent =_container .transform;
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
                GameObject newAsteroid = Instantiate(_enemy[2], new Vector2(Random.Range(-8f, 0f), 7.2f), Quaternion.identity);
                newAsteroid.transform.parent = _container .transform;
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
                GameObject newAsteroid = Instantiate(_enemy[3], new Vector2(Random.Range(-8f, 0f), -7.2f), Quaternion.identity);
                newAsteroid.transform.parent = _container .transform;
                yield return new WaitForSeconds(_spawnTimeAsteroid);
            }
        }
        while (_stopSpawning == false);
    }

    IEnumerator SpawnRandomPowerup()
    {
        do
        {
            int _spawnTimePowerUp = Random.Range(15, 20); //makespawntime random
            while (_stopSpawning == false)
            {
                yield return new WaitForSeconds(_spawnTimePowerUp);
                int randompowerups = Random.Range(0, 3);
                GameObject newPowerup = Instantiate(_powerups[randompowerups], new Vector2(11.5f, Random.Range(-4.5f, 4.5f)), Quaternion.identity);
                newPowerup.transform.parent = _container.transform;
            }
        }
        while (_stopSpawning == false);
    }
    public void OnPlayerDeath()
    {
        _stopSpawning=true;
    }
}
