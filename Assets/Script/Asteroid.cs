using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private float _speed = 1.5f;
    private Player _player;
    [SerializeField]
    private GameObject _explosionPrefab;
    private SpawnManager _spawnManager;
   
    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>(); //this create error/null after player dead
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        if (_spawnManager == null)
        {
            Debug.LogError("Spawn Manager is NULL");
        }
    }
    void Update()
    {
       transform.Translate(new Vector2 (-1f,-1f) * _speed * Time.deltaTime);
        transform.Rotate(new Vector3 (0,0,5) * 3 * Time.deltaTime);
        if (transform.position.y < - 7.2f )
        {
            transform.position = new Vector2(Random.Range(-12f, 0f), 7.2f);
        }
        /*for next asteroid (below) 
        transform.Translate(new Vector2(-1f, 1f) * _speed * Time.deltaTime);
        transform.Rotate(new Vector3(0, 0, -5) * 3 * Time.deltaTime);
        if (transform.position.y > 7.2f )
        {
            transform.position = new Vector3(Random.Range(-12f, 0f), -7.2f);
        } */
    }
    private void OnTriggerEnter2D(Collider2D other)
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
                _player.AddScore(50);
                _spawnManager.AddEnemyInstance();
            }
            GameObject explosion = Instantiate(_explosionPrefab, transform.position + new Vector3(0f, 0f, 0f), Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
