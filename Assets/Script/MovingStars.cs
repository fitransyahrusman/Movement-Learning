using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingStars : MonoBehaviour
{
    // Start is called before the first frame update
    private float _speed = 0.5f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Moving();
        if (transform.position.x < -32.5f)
        {
            transform.position = new Vector3(37.5f, 0f, 0f);
        }
    }
    void Moving()
    {
        transform.Translate(Vector3.left * _speed * Time.deltaTime);
    }
}
