using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameLoop : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] entities;
    public GameObject whirlwind;

    void Start()
    {
        InvokeRepeating("spawnObject", 1f, 2f);
        InvokeRepeating("spawnWhirlwind", 1f, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawnObject()
    {
        GameObject entity = entities[Random.Range(0, entities.Length)];
        float xDir = 0;
        if(entity.tag == "balloon")
        {
            xDir = Random.Range(-7, 7);
            Instantiate(entity, new Vector3(xDir, -5, 0), new Quaternion(0, 0, 0, 0));
        }
        else if(entity.tag == "bird")
        {
            xDir = Random.Range(0, 2) == 0 ? -10 : 10;
            Instantiate(entity, new Vector3(xDir, Random.Range(-5, 5), 0), new Quaternion(0, 0, 0, 0));
        }
    }

    public void spawnWhirlwind()
    {
        Instantiate(whirlwind, new Vector3(Random.Range(-5, 5), Random.Range(-3, 6), 0), new Quaternion(0, 0, 0, 0));
    }

}
