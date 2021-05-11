using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float _speed = 5f;
    private Player _player;

    private void Start()
    {
          _player = GameObject.Find("Player").GetComponent<Player>();
    }

    void Update()
    {
       transform.Translate(Vector3.left * _speed *Time.deltaTime);

       if (transform.position.x < -11f)
       {
           transform.position = new Vector3 (12f, Random.Range(-4.2f, 4f), 0f);  
       }       
    }
    void OnTriggerEnter2D (Collider2D other) 
    {
        if (other.tag == "Player")
        {
            _player.Damage();
            Destroy(this.gameObject);
        }
        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            if ( _player != null)
            {
                 _player.AddScore(25);
            }
               Destroy(this.gameObject);
        }
    }
}   
