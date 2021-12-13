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
    private float posYInc = 0f;
    private float direction = 0f;
    private float amp = 0f;
    private float ySpeed = 0f;
    private float t = 0f;
    private SpriteRenderer mySpriteRenderer;
    Vector3 currentPos;
    Vector3 targetPos;
    Vector3 interpolatedPosition;

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
            mySpriteRenderer.flipX = true;
        }

        posInc = Random.Range(.001f, .005f);

        switch (this.gameObject.tag)
        {
            case "balloon":
                moveSpeed = Random.Range(.003f, .008f);
                break;
            case "bird":
                sinSpeed = Random.Range(1f, 2f);
                moveSpeed = Random.Range(.001f, .005f);
                amp = Random.Range(1f, 3f);
                break;
            case "butterfly":
                sinSpeed = Random.Range(4f, 5f);
                moveSpeed = Random.Range(.005f, .01f);
                amp = Random.Range(3f, 5f);
                break;
            case "airplane":
                moveSpeed = Random.Range(.025f, .05f);
                ySpeed = Random.Range(.025f, .05f);
                Quaternion rot = this.transform.rotation;
                rot.y = 140;
                this.transform.rotation = rot;
                targetPos = currentPos;
                currentPos = transform.position;
                targetPos.x = Random.Range(currentPos.x, currentPos.x + (5 * direction));
                targetPos.y = Random.Range(currentPos.y - 2, currentPos.y + 2);
                break;
        }

    }

    void Awake()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (!gameLoop.paused)
        {
            switch (this.gameObject.tag)
            {
                case "balloon":
                    posInc += moveSpeed;
                    _newPosition = _startPosition + new Vector3(0, posInc, 0);
                    transform.position = _newPosition;
                    if (transform.position.magnitude > 15)
                    {
                        Destroy(this.gameObject);
                    }
                    break;
                case "bird":
                    posInc += moveSpeed;
                    _newPosition = _startPosition + new Vector3(posInc * direction, Mathf.Sin(Time.time * sinSpeed) * amp, 0.0f);
                    transform.position = _newPosition;
                    if (transform.position.magnitude > 15)
                    {
                        Destroy(this.gameObject);
                    }
                    break;
                case "butterfly":
                    posInc += moveSpeed;
                    _newPosition = _startPosition + new Vector3(posInc * direction, Mathf.Sin(Time.time * sinSpeed) * amp, 0.0f);
                    transform.position = _newPosition;
                    if (transform.position.magnitude > 15)
                    {
                        Destroy(this.gameObject);
                    }
                    break;
                case "airplane":
                    t += Time.deltaTime;
                    transform.position = Vector3.Lerp(currentPos, targetPos, t / 1f);
                    if (transform.position == targetPos)
                    {
                        t = 0f;
                        currentPos = transform.position;
                        targetPos.x = Random.Range(currentPos.x, currentPos.x + (5 * direction));
                        targetPos.y = Random.Range(currentPos.y - 2, currentPos.y + 2);
                        Quaternion rot = this.transform.rotation;
                        float yDif = targetPos.y - currentPos.y;
                        if (yDif > 0)
                        {
                            Debug.Log("going up " + yDif);
                            rot.x = 0;
                            rot.y = 0;
                            rot.z += Mathf.Abs(yDif);
                            this.transform.rotation = rot;
                        }
                        else if (yDif < 0)
                        {
                            Debug.Log("going down " + yDif);
                            rot.x = 0;
                            rot.y = 0;
                            rot.z -= Mathf.Abs(yDif);
                            this.transform.rotation = rot;
                        }
                    }
                    if (transform.position.magnitude > 15)
                    {
                        Destroy(this.gameObject);
                    }
                    break;
            }
        }
        
    }

}
