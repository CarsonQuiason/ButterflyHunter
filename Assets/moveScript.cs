using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveScript : MonoBehaviour
{
    private Vector3 _startPosition;
    private Vector3 _newPosition;
    private float sinSpeed = 1f;
    private float moveSpeed = .01f;
    private float xPos = 0;

    void Start()
    {
        _startPosition = transform.position;
    }

    void Update()
    {
        xPos += moveSpeed;
        _newPosition = _startPosition + new Vector3(xPos, Mathf.Sin(Time.time * sinSpeed), 0.0f);
        transform.position = _newPosition;
    }

}
