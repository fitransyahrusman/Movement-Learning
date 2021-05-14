using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCube : MonoBehaviour
{
    [SerializeField]
    private GameObject _explosionPrefab;
    private UIManager _cubeStart;
  
    void Start()
    {
        _cubeStart = GameObject.Find("Canvas").GetComponent<UIManager>();
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
            Destroy(this.gameObject);
            _cubeStart.gameObject.SetActive(false);
        }
    }
}
