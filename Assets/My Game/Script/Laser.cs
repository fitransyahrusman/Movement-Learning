using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private float _speed = 9f;

    void Update()
    {   
        transform.Translate( new Vector2(1f,0f) * _speed * Time.deltaTime);

        if (transform.position.x > 12f)
        {
          Destroy(this.gameObject);   
        }
    }
}
