using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.5f;
    

    // Start is called before the first frame update
    void Start()
    {
        //transform.position = new Vector3(0, 6.0f, 0);  //Moving enemy to top of screen
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        float randomX = Random.Range(-10f, 10f);                        // Getting a random X position
        if (transform.position.y <= -5.5)
            transform.position = new Vector3(randomX, 6.5f, 0);

    }

   private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hit : " + other.transform.name);

        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>(); //obtaining Player Script component as it has lives in it
            if (player != null)                    // Null Checking for errors
            {
                player.Damage();
            }
            Destroy(this.gameObject);
        }

        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }

}
