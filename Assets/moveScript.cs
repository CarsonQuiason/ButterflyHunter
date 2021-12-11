using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovementType
{
    balloon,
    bird,
    butterfly
}

public class moveScript : MonoBehaviour
{
    private Vector3 _startPosition;
    private Vector3 _newPosition;
    private float sinSpeed = 0f;
    private float moveSpeed = 0f;
    private float posInc = 0f;
    private float direction = 0f;
    private float amp = 0f;
    private MovementType mt;

    void Start()
    {
        _startPosition = transform.position;
        if(_startPosition.x <= -10)
        {
            direction = 1;
        }
        else
        {
            direction = -1;
        }

        posInc = Random.Range(.001f, .005f);

        switch (this.gameObject.tag)
        {
            case "balloon":
                mt = MovementType.balloon;
                moveSpeed = Random.Range(.003f, .008f);
                break;
            case "bird":
                mt = MovementType.bird;
                sinSpeed = Random.Range(1f, 2f);
                moveSpeed = Random.Range(.001f, .005f);
                amp = Random.Range(1f, 3f);
                break;
            case "butterfly":
                mt = MovementType.butterfly;
                sinSpeed = Random.Range(4f, 5f);
                moveSpeed = Random.Range(.005f, .01f);
                amp = Random.Range(3f, 5f);
                break;
        }

    }

    void Update()
    {
        if(mt == MovementType.balloon)
        {
            posInc += moveSpeed;
            _newPosition = _startPosition + new Vector3(0, posInc, 0);
            transform.position = _newPosition;
        }
        else if(mt == MovementType.bird)
        {
            posInc += moveSpeed;
            _newPosition = _startPosition + new Vector3(posInc * direction, Mathf.Sin(Time.time * sinSpeed) * amp, 0.0f);
            transform.position = _newPosition;
        }
        else if(mt == MovementType.butterfly)
        {
            posInc += moveSpeed;
            _newPosition = _startPosition + new Vector3(posInc * direction, Mathf.Sin(Time.time * sinSpeed) * amp, 0.0f);
            transform.position = _newPosition;
        }

        if(transform.position.magnitude > 15)
        {
            Destroy(this.gameObject);
        }
    }

}
