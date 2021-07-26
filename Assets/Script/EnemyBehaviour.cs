using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    private float _speedEnemy1 = 5f;
    private float _speedEnemy2 = 6f;
    private float _speedAsteroid1 = 1.5f;
    private float _speedAsteroid2 = 1.5f;
    [SerializeField]
    private int _behaviourID;
    [SerializeField]
    private GameObject _explosionPrefab;
    private SpawnManager _spawnManager;
    private Player _player;
    // Start is called before the first frame update
    void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        if (_spawnManager == null)
        {
            Debug.LogError("Spawn Manager is NULL");
        }
        if (_player == null)
        {
            _player = GameObject.Find("Player").GetComponent<Player>();
        }
    }
    // Update is called once per frame
    void Update()
    {
        AllEnemyBehaviour();
    }
    void AllEnemyBehaviour()
    {
        switch (_behaviourID)
        {
            case 0:
                transform.Translate(Vector3.left * _speedEnemy1 * Time.deltaTime);
                if (transform.position.x < -11f)
                {
                    transform.position = new Vector3(12f, Random.Range(-4.2f, 4f), 0f);
                }
                break;
            case 1:
                transform.Translate(Vector3.left * _speedEnemy2 * Time.deltaTime);
                if (transform.position.x < -11f)
                {
                    transform.position = new Vector3(12f, Random.Range(-4.2f, 4f), 0f);
                }
                break;
            case 2:
                transform.Translate(new Vector2(-1f, -1f) * _speedAsteroid1 * Time.deltaTime);
                transform.Rotate(new Vector3(0, 0, 5) * 3 * Time.deltaTime);
                if (transform.position.y < -7.2f)
                {
                    transform.position = new Vector2(Random.Range(-12f, 0f), 7.2f);
                }
                break;
            case 3:
                transform.Translate(new Vector2(-1f, 1f) * _speedAsteroid2 * Time.deltaTime);
                transform.Rotate(new Vector3(0, 0, -5) * 3 * Time.deltaTime);
                if (transform.position.y > 7.2f)
                {
                    transform.position = new Vector3(Random.Range(-12f, 0f), -7.2f);
                }
                break;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            _player.Damage();
            GameObject explosion = Instantiate(_explosionPrefab, transform.position + new Vector3(0f, 0f, 0f), Quaternion.identity);
            Destroy(this.gameObject);
        }
        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            if (_player != null)
            {
                _spawnManager.AddEnemyInstance();
                switch (_behaviourID)
                {
                    case 0:
                        _player.AddScore(25);
                        break;
                    case 1:
                        _player.AddScore(100);
                        break;
                    case 2:
                        _player.AddScore(75);
                        break;
                    case 3:
                        _player.AddScore(75);
                        break;
                }
              //_player.AddScore(25);
              
            }
            GameObject explosion = Instantiate(_explosionPrefab, transform.position + new Vector3(0f, 0f, 0f), Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
