using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour
{
    private Vector3 _target;
    public Camera Camera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _target = Camera.ScreenToWorldPoint(Input.mousePosition);
        _target.z = 0;
        transform.position = Vector3.MoveTowards(transform.position, _target, 1);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
    }
}
