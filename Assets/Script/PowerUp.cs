using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private float _speed = 7.5f;
    [SerializeField]
    private int powerupsID; //0=3ple shoot, 1=speed, 2=shield.
    [SerializeField]
    private AudioClip _powerupSound;
    void Update()
    {
        transform.Translate(new Vector2(-1f, 0f) * _speed * Time.deltaTime);
        if (transform.position.x < -11.5f)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                AudioSource.PlayClipAtPoint(_powerupSound, transform.position);
                switch (powerupsID)
                {
                    case 0:
                        player.TripleShotActive();
                        break;
                    case 1:
                        player.SpeedActive();
                        break;
                    case 2:
                        player.ShieldActive();
                        break;
                }
            }
            Destroy(this.gameObject);
        }
    }
}
