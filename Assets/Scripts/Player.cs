using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.5f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private float _fireRate = 0.5f;
    [SerializeField]
    private float _canFire = -1f;
    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnManager;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private bool _isTripleShotActive = false;
    [SerializeField]
    private float _speedBoost = 8.5f;
    [SerializeField]
    private bool _isSpeedBoostActive = false;
    [SerializeField]
    private bool _isShieldActive = false;
    [SerializeField]
    private GameObject _shieldVisualizer;


    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);

        _spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();

        if (_spawnManager == null)
        {
            Debug.LogError("Spawn Manager is null");

        }
    }



    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }



    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        if (_isSpeedBoostActive == false)
        {
            transform.Translate(direction * _speed * Time.deltaTime);
        }
        else if (_isSpeedBoostActive == true)
        {
            transform.Translate(direction * _speedBoost * Time.deltaTime);
        }




        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.96f, 0), 0);

        if (transform.position.x >= 11)
            transform.position = new Vector3(-11, transform.position.y, 0);
        else if (transform.position.x <= -11)
            transform.position = new Vector3(11, transform.position.y, 0);


    }
    void FireLaser()
    {
        _canFire = Time.time + _fireRate;

        if (_isTripleShotActive == true)
        {

            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
        }
        else
        {

            //Debug.Log("Space Key Pressed");
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1f, 0), Quaternion.identity);
        }

    }

    public void Damage()
    {
        if (_isShieldActive == true)
        {
            _isShieldActive = false;
            _shieldVisualizer.SetActive(false);
            return;

        }
        _lives -= 1;

        if (_lives <= 0)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }

    public void TripleShotActive()
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSecondsRealtime(5.0f);
        _isTripleShotActive = false;
    }
    public void SpeedBoostActive()
    {
        _isSpeedBoostActive = true;
        StartCoroutine(SpeedBoostRoutine());
    }

    IEnumerator SpeedBoostRoutine()
    {
        yield return new WaitForSecondsRealtime(5.0f);
        _isSpeedBoostActive = false;
    }
    public void ShieldsActive()
    {
        _isShieldActive = true;
        _shieldVisualizer.SetActive(true);
    }


}
