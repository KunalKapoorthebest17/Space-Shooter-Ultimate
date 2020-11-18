using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeBehavior : MonoBehaviour
{

    private Transform _transform;
    private float _shakeDuration = 0f;
    private float _shakeMagnitude = 0.7f;
    private float _dampingSpeed = 2.0f;
    Vector3 _initialPosition;

    void Awake()
    {
        if (transform == null)
        {
            _transform = GetComponent<Transform>();
        }
    }

    void OnEnable()
    {
        _initialPosition = transform.localPosition;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_shakeDuration > 0)
        {
            transform.localPosition = _initialPosition + Random.insideUnitSphere * _shakeMagnitude;

            _shakeDuration -= Time.deltaTime * _dampingSpeed;
        }
        else
        {
            _shakeDuration = 0f;
            transform.localPosition = _initialPosition;
        }
    }

    public void TriggerShake()
    {
        _shakeDuration = 1.0f;
    }
}

