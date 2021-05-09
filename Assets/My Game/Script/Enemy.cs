using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float _speed = 5f;
    
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
            Player player = other.transform.GetComponent<Player>();
            if (player != null) //nullchecking first
            {
                player.Damage();
                Destroy(this.gameObject);
            }  
        }
        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}   
