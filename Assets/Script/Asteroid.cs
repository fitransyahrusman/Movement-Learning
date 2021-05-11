using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private float _speed = 1.5f;
    private Player _player;
   

    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>(); //this create error/null after player dead
    }

    void Update()
    {
        transform.Translate(new Vector2 (-1f,-1f) * _speed * Time.deltaTime);
        if (transform.position.y < - 7.2f)
        {
            transform.position = new Vector2(Random.Range(0f, 12f), 7.2f);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            _player.Damage();
            Destroy(this.gameObject);
        }
        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            if (_player != null)
            {
                _player.AddScore(50);
            }
            Destroy(this.gameObject);
        }
    }
}
