using UnityEngine;
using UnityEngine.Events;

public class StartCube : MonoBehaviour
{
    [SerializeField]
    private GameObject _explosionPrefab;
    private SpawnManager _startSpawning;
    private Player _player;
    void Start()
    {
        _startSpawning = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        if (_startSpawning == null)
        {
            Debug.LogError("Spawn Manager is NULL");
        }
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null)
        {
            Debug.LogError("Player is NULL");
        }
    }
    void Update()
    {
        transform.Rotate(new Vector3(0f, 0f, 5f) * 5f * Time.deltaTime);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            GameObject explosion = Instantiate(_explosionPrefab, transform.position + new Vector3(0f, 0f, 0f), Quaternion.identity);
            _startSpawning.StartSpawning();
            _player.RemoveStartText();
            Destroy(this.gameObject);
        }
    }   
}
