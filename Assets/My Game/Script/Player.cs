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
    private int _lives = 3;
    private SpawnManager _spawnManager;
    [SerializeField]
    private GameObject _laserprefab;
    [SerializeField]
    private GameObject _laserContainer;
    [SerializeField]
    private bool _isTripleShotActive = false;
    [SerializeField]
    private bool _isSpeedActive = false;
    [SerializeField]
    private bool _isShieldActive = false;
    [SerializeField]
    private GameObject _shieldVisualizer;
    [SerializeField]
    private int _score;
    private UIManager _uiManager;
    
    void Start()
    {
        _shieldVisualizer.SetActive(false); //makesure visualizer always off
        transform.position = new Vector3 (-7, 0, 0); //starting position
        
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>(); //communication with spawn manager for null checking
        if (_spawnManager == null)
        {
            Debug.LogError ("Spawn Manager is NULL");
        }

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_uiManager == null)
        {
            Debug.LogError("The UI Manager script is NULL");
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
        transform.Translate(Vector3.right * rightInput * _speedRight * Time.deltaTime);
        transform.Translate(Vector3.right * leftInput * _speedLeft * Time.deltaTime);
        transform.Translate(Vector3.up * VerticalInput * _speedVertical * Time.deltaTime);
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
        if (_isShieldActive == true)
        {
            _isShieldActive = false; //shieldcooldown
            _shieldVisualizer.SetActive(false); //turn of visualizer
            return;
        }
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
    public void ShieldActive ()
    {
        _isShieldActive = true;
        _shieldVisualizer.SetActive(true);
    }
    public void AddScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }
}
   

