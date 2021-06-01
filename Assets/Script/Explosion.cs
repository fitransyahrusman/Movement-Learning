using UnityEngine;

public class Explosion : MonoBehaviour
{
    void Update()
    {
        Destroy(this.gameObject, 3.25f);   
    }
}
