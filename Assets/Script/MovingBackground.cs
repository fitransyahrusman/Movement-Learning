using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBackground : MonoBehaviour
{

    private float _speed = 2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Moving();
        if (transform.position.x < -37f)
        {
            transform.position = new Vector3(44.5f, 0f, 0f);
        }
    }
    void Moving()
    {
        transform.Translate(Vector3.left * _speed * Time.deltaTime);
    }
}
