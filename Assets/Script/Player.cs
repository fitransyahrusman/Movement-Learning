using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    private float _speedRight = 7.5f;
    private float _speedLeft = 4f;
    private float _speedVertical = 4f;
    private float _speedMultiplier = 2f;
    private float _firerate = 0.5f;
    private float _canfire = -1f;
    [SerializeField]
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
    private GameObject _damageLeft, _damageRight;
    [SerializeField]
    private int _score;
    [SerializeField]
    private Text _startGameText;
    [SerializeField]
    private GameObject _explosionPrefab;
    private UIManager _uiManager;
    //private Enemy _enemy;
    [SerializeField]
    private AudioClip _laserSound;
    private AudioSource _audioSource;
    private Animator _anim;

   
    private float _horizontalInput;
    [SerializeField]
    private float _verticalInput;
    
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            Debug.LogError("Audio source is NULL");
        }
        else
        {
            _audioSource.clip = _laserSound;
        }
        _shieldVisualizer.SetActive(false); //makesure visualizer always off
        _damageLeft.SetActive(false);
        _damageRight.SetActive(false);
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
        _startGameText.gameObject.SetActive(true);
        StartCoroutine(CubeStartFlicker());
        //_enemy = GameObject.Find("Enemy").GetComponent<Enemy>();
        //if (_enemy == null)
        //{
        //    Debug.LogError("Failure calling Enemy from Spawn Manager");
        //}

        if (_anim == null)
        {
            _anim = GetComponent<Animator>();
        }
    } 
    void Update()
    {
      codeMovement();
     if (Time.time > _canfire)
     {
        codeFiring();
            
     }
        animationMovement();
    }
    void codeMovement ()
    {
        //input
       
        _horizontalInput = CrossPlatformInputManager.GetAxis("Horizontal") ; // Input.GetAxis("Right");
        //float leftInput = CrossPlatformInputManager.GetAxis("left"); // Input.GetAxis("Left");
        _verticalInput  = CrossPlatformInputManager.GetAxis("Vertical"); // Input.GetAxis("Vertical");
        //movement
        transform.Translate(Vector3.right * _horizontalInput * _speedVertical * Time.deltaTime);
        // call animator left here
        
        
        
         
        
       // transform.Translate(Vector3.right * leftInput * _speedLeft * Time.deltaTime);
        transform.Translate(Vector3.up * _verticalInput * _speedVertical * Time.deltaTime);
        //call animator right here
      
        //bounds
        transform.position = new Vector3 (transform.position.x, Mathf.Clamp(transform.position.y,-4.808401f, 4.808401f),0f );
        transform.position = new Vector3 (Mathf.Clamp(transform.position.x,-8.382592f ,5f), transform.position.y, 0f);
    }

    void animationMovement()
    {
        if (_verticalInput > 0f)
        {
            _anim.SetBool("PlayerTurnleft", true);
            _anim.SetBool("PlayerTurnright", false);
        }
        else if (_verticalInput < 0f)
        {
            _anim.SetBool("PlayerTurnleft", false);
            _anim.SetBool("PlayerTurnright", true);
        }
        else
        {
            _anim.SetBool("PlayerTurnleft", false);
            _anim.SetBool("PlayerTurnright", false);
        }
    }
    void codeFiring ()
    {
        if (_isTripleShotActive == true)
        {
            _audioSource.Play();
            _canfire = Time.time + _firerate;
            GameObject newLaserTriple1 = Instantiate(_laserprefab, transform.position + new Vector3(1.25f, 0f, 0f), Quaternion.Euler (0f,0f,0f));
            _audioSource.Play();
            _canfire = Time.time + _firerate;
            GameObject newLaserTriple2 = Instantiate(_laserprefab, transform.position + new Vector3(1.5f, 0.25f, 0f), Quaternion.Euler(0f, 0f, 5f));
            _audioSource.Play();
            _canfire = Time.time + _firerate;
            GameObject newLaserTriple3 = Instantiate(_laserprefab, transform.position + new Vector3(1.5f, -0.25f, 0f), Quaternion.Euler(0f, 0f, -5f));
            newLaserTriple1.transform.parent = _laserContainer.transform;
            newLaserTriple2.transform.parent = _laserContainer.transform;
            newLaserTriple3.transform.parent = _laserContainer.transform;
        }
        else 
        {
            _audioSource.Play();
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
        _uiManager.UpdateLives(_lives); //lives display
        if (_lives == 2)
        {
            _damageLeft.SetActive(true);
        }
        else if (_lives == 1)
        {
            _damageRight.SetActive(true);
        }
        else if (_lives ==0 ) //playerdeath
        {
            _spawnManager.OnPlayerDeath();
            GameObject explosion = Instantiate(_explosionPrefab, transform.position + new Vector3(0f, 0f, 0f), Quaternion.identity);
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
        yield return new WaitForSeconds(15f);
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
        yield return new WaitForSeconds(15f);
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
    IEnumerator CubeStartFlicker()
    {
        while (true)
        {
            _startGameText.text = "Shoot the Enemy to start!";
            yield return new WaitForSeconds(0.25f);
            _startGameText.text = "";
            yield return new WaitForSeconds(0.25f);
        }
    }
    public void RemoveStartText()
    {
        _startGameText.gameObject.SetActive(false);
    }
}
   

