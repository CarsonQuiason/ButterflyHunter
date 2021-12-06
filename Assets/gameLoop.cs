using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameLoop : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] entities;

    void Start()
    {
        InvokeRepeating("spawnObject", 1f, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawnObject()
    {
        int xDir = Random.Range(0, 2) == 0 ? -10 : 10;
        Instantiate(entities[Random.Range(0, entities.Length)], new Vector3(xDir, Random.Range(-10, 10), 0), new Quaternion(0,0,0,0));
    }
}
