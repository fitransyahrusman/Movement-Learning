using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4f;
    
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
            /* other.transform.GetComponent<Player>().Damage();
           Destroy(this.gameObject);
           ----------------------------*/
            //lets do null checking
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
                Debug.Log("hit");
            }  
        }
        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}   
