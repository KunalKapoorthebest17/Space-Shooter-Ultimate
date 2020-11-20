using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 2f;

    private Player _player;

    private Animator _anim;

    [SerializeField]
    private AudioSource _audioSource;

    [SerializeField]
    private float _fireRate = 3.0f;

    private float _canFire = -1f;

    [SerializeField]
    private GameObject _laserPrefab;

    // Start is called before the first frame update
    void Start()
    {
        //transform.position = new Vector3(0, 6.0f , 0);

        _player = GameObject.Find("Player").GetComponent<Player>();

        if (_player == null)
        {
            Debug.LogError("Player is null");
        }

        _anim = GetComponent<Animator>();

        if (_anim == null)
        {
            Debug.LogError("Animator is null");
        }

        _audioSource = GetComponent<AudioSource>();

        if (_audioSource == null)
        {
            Debug.LogError("Animator is null");
        }

    }

    // Update is called once per frame
    void Update()
    {

        CalculateMovement();
        FireLaserAtPlayer();


    }

    void CalculateMovement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        float randomX = Random.Range(-10f, 10f);
        if (transform.position.y <= -5.5)
            transform.position = new Vector3(randomX, 6.5f, 0);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hit : " + other.transform.name);

        if (other.gameObject.CompareTag("Player"))
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
            }
            _anim.SetTrigger("OnEnemyDeath");
            _speed = 0;
            _audioSource.Play();
            Destroy(this.gameObject, 1.4f);
            /* if(transform.position.y == -5.5f)
             {
                 Destroy(this.gameObject);
             }
             else
             {
                 Destroy(this.gameObject, 2.8f);
             }*/
        }
        else if (other.gameObject.CompareTag("Laser"))
        {
            Destroy(other.gameObject);
            if (_player != null)
            {
                _player.AddScore(10);
            }
            _anim.SetTrigger("OnEnemyDeath");
            _speed = 0;
            _audioSource.Play();
            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject, 1.4f);
            /*  if (transform.position.y == -5.5f)
              {
                  Destroy(this.gameObject);
              }
              else
              {
                  Destroy(this.gameObject, 2.8f);
              }*/
        }
    }
    void FireLaserAtPlayer()
    {

        if (Time.time > _canFire)
        {

            _fireRate = Random.Range(3f, 7f);
            _canFire = Time.time + _fireRate;
            GameObject enemyLaser = Instantiate(_laserPrefab, transform.position, Quaternion.identity);
            Laser[] lasers = enemyLaser.GetComponentsInChildren<Laser>();
            for (int i = 0; i < lasers.Length; i++)
            {
                lasers[i].AssignEnemyLaser();
            }
        }




    }


}
