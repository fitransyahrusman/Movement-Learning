using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float _speedRight = 7.5f;
    private float _speedLeft = 4f;
    private float _speedVertical = 4f;
    private float _speedMultiplier = 2f;
    private float _firerate = 0.5f;
    private float _tripleFirerate = 1.25f;
    private float _canfire = -1f;
    private SpawnManager _spawnManager;
    [SerializeField]
    private int _lives = 3;
    [SerializeField]
    private GameObject _laserprefab;
    [SerializeField]
    private GameObject _laserContainer;
    [SerializeField]
    private bool _isTripleShotActive = false;
    [SerializeField]
    private bool _isSpeedActive = false;
    
    void Start()
    {   
      transform.position = new Vector3 (-7, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        
        if (_spawnManager == null)
        {
            Debug.Log("Spawn Manager is NULL");
        }
    } 
    void Update()
    {
      codeMovement();

     if (Time.time > _canfire)
     {
        codeFiring();
     }
    }

    void codeMovement ()
    {
        //input
        float rightInput = Input.GetAxis("Right");
        float leftInput = Input.GetAxis("Left");
        float VerticalInput = Input.GetAxis("Vertical");

        //movement
        /*if (_isSpeedActive ==false)
         { */
        transform.Translate(Vector3.right * rightInput * _speedRight * Time.deltaTime);
        transform.Translate(Vector3.right * leftInput * _speedLeft * Time.deltaTime);
        transform.Translate(Vector3.up * VerticalInput * _speedVertical * Time.deltaTime);
         /*}
         else               ------- IF ELSE STEMENT NOT NEEDED BECAUSE WE MODIFY DIRECTLY IN PUBLIC METHOD AND COOLDOWN ROUTINE -----
         {  
            transform.Translate(Vector3.right * rightInput * _speedRight * _speedMultiplier * Time.deltaTime);
            transform.Translate(Vector3.right * leftInput * _speedLeft * _speedMultiplier * Time.deltaTime);
            transform.Translate(Vector3.up * VerticalInput * _speedVertical * _speedMultiplier * Time.deltaTime);
         }*/

        //bounds
        transform.position = new Vector3 (transform.position.x, Mathf.Clamp(transform.position.y,-4.808401f, 4.808401f),0f );
        transform.position = new Vector3 (Mathf.Clamp(transform.position.x,-8.382592f ,5f), transform.position.y, 0f);
    }

    void codeFiring ()
    {
        if (_isTripleShotActive == true)
        {
            _canfire = Time.time + _tripleFirerate;
            GameObject newLaserTriple1 = Instantiate(_laserprefab, transform.position + new Vector3(1.25f, 0f, 0f), Quaternion.Euler (0f,0f,0f));
            _canfire = Time.time + _tripleFirerate;
            GameObject newLaserTriple2 = Instantiate(_laserprefab, transform.position + new Vector3(1.5f, 0.25f, 0f), Quaternion.Euler(0f, 0f, 5f));
            _canfire = Time.time + _tripleFirerate;
            GameObject newLaserTriple3 = Instantiate(_laserprefab, transform.position + new Vector3(1.5f, -0.25f, 0f), Quaternion.Euler(0f, 0f, -5f));
            newLaserTriple1.transform.parent = _laserContainer.transform;
            newLaserTriple2.transform.parent = _laserContainer.transform;
            newLaserTriple3.transform.parent = _laserContainer.transform;
        }
        else //if (_isTripleShotActive == false)
        {
            _canfire = Time.time + _firerate;
            GameObject newLaser = Instantiate(_laserprefab, transform.position + new Vector3(1.25f, 0f, 0f), Quaternion.Euler(0f,0f,0f));
            newLaser.transform.parent = _laserContainer.transform;
        }
    }
    public void Damage()
    {
        _lives--;
        if (_lives <1) //playerdeath
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }
    public void TripleShotActive ()
    {
        _isTripleShotActive = true;
        StartCoroutine(CoolDownTripleShot());
    }
    IEnumerator CoolDownTripleShot()
    {
        yield return new WaitForSeconds(10f);
        _isTripleShotActive = false;
    }
    public void SpeedActive ()
    {
        _isSpeedActive = true;
        _speedRight *= _speedMultiplier; //this cause if statement not necessary
        _speedLeft *= _speedMultiplier;
        _speedVertical *= _speedMultiplier; 
        StartCoroutine(CoolDownSpeed());
    }
    IEnumerator CoolDownSpeed ()
    {
        yield return new WaitForSeconds(10f);
        _isSpeedActive = false;
        _speedRight /= _speedMultiplier; //this cause if statement not necessary
        _speedLeft /= _speedMultiplier;
        _speedVertical /= _speedMultiplier;
    }
}
   

