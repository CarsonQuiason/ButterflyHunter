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
    private float sinSpeed = 1f;
    private float moveSpeed;
    private float posInc = 0;
    private float direction = 0;
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
        moveSpeed = Random.Range(.001f, .005f);

        switch (this.gameObject.tag)
        {
            case "balloon":
                mt = MovementType.balloon;
                break;
            case "bird":
                mt = MovementType.bird;
                break;
            case "butterfly":
                mt = MovementType.butterfly;
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
            _newPosition = _startPosition + new Vector3(posInc * direction, Mathf.Sin(Time.time * sinSpeed), 0.0f);
            transform.position = _newPosition;
        }

        if(transform.position.magnitude > 15)
        {
            Destroy(this.gameObject);
        }
    }

}
