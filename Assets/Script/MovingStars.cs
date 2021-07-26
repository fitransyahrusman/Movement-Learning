using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingStars : MonoBehaviour
{
    private float _speed = 0.5f;
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
