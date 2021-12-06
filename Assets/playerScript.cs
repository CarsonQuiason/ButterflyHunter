using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour
{
    private Vector3 _target;
    public Camera Camera;
    public GameObject playerPref;
    public bool whirlwind = false;

    float t1 = 0f;
    float t2 = 0f;
    float duration = 1f;
    Vector3 startScale;
    Vector3 targetScale = Vector3.one * 2;

    // Start is called before the first frame update
    void Start()
    {
        startScale = playerPref.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        _target = Camera.ScreenToWorldPoint(Input.mousePosition);
        _target.z = 0;
        transform.position = Vector3.MoveTowards(transform.position, _target, 1);

        if(whirlwind)
        {
            Vector3 newZ = playerPref.transform.position;
            newZ.z = -10;
            playerPref.transform.position = newZ;
            t1 += Time.deltaTime / duration;
            Vector3 newScale = Vector3.Lerp(startScale, targetScale, t1);
            playerPref.transform.localScale = newScale;
            if(playerPref.transform.localScale == targetScale)
            {
                t2 += Time.deltaTime / duration;
                newScale = Vector3.Lerp(targetScale, startScale, t2);
                playerPref.transform.localScale = newScale;
                if(playerPref.transform.localScale == startScale)
                {
                    t1 = t2 = 0f;
                    newZ = playerPref.transform.position;
                    newZ.z = 0;
                    playerPref.transform.position = newZ;
                    whirlwind = false;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch(other.gameObject.tag)
        {
            case "whirlwind":
                whirlwind = true;
                Destroy(other.gameObject);
                break;
            case "bird":
                Debug.Log("bird");
                Destroy(other.gameObject);
                break;
            case "butterfly":
                Debug.Log("butterfly");
                Destroy(other.gameObject);
                break;
            case "balloon":
                Debug.Log("balloon");
                Destroy(other.gameObject);
                break;
        }
    }
}
