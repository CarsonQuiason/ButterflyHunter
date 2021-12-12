using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class playerScript : MonoBehaviour
{
    private Vector3 _target;
    public Camera Camera;
    public GameObject playerPref;
    public bool whirlwind = false;
    Image imageFeedback;
    SpriteRenderer hitStun;

    float t1 = 0f;
    float t2 = 0f;
    float duration = 1f;
    Vector3 startScale;
    Vector3 targetScale = Vector3.one * 2;

    private bool canMove = true;

    // Start is called before the first frame update
    void Start()
    {
        startScale = playerPref.transform.localScale;
        imageFeedback = this.gameObject.transform.Find("Canvas/checkMark").GetComponent<Image>();
        hitStun = this.gameObject.transform.Find("Canvas/stunSheet").GetComponent<SpriteRenderer>();
        imageFeedback.enabled = false;
        hitStun.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(canMove)
        {
            _target = Camera.ScreenToWorldPoint(Input.mousePosition);
            _target.z = 0;
            transform.position = Vector3.MoveTowards(transform.position, _target, 1);
            Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
            pos.x = Mathf.Clamp01(pos.x);
            pos.y = Mathf.Clamp01(pos.y);
            transform.position = Camera.main.ViewportToWorldPoint(pos);
        }

        if (whirlwind)
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
                StartCoroutine(giveNegFeedback());
                Debug.Log("bird");
                gameLoop.misses--;
                Destroy(other.gameObject);
                break;
            case "butterfly":
                StartCoroutine(givePosFeedback());
                Debug.Log("butterfly");
                gameLoop.hits++;
                Destroy(other.gameObject);
                break;
            case "balloon":
                StartCoroutine(giveNegFeedback());
                Debug.Log("balloon");
                gameLoop.misses--;
                Destroy(other.gameObject);
                break;
        }
    }

    IEnumerator giveNegFeedback()
    {
        imageFeedback.enabled = false;
        if(canMove)
        {
            canMove = false;
            hitStun.enabled = true;
        }
        yield return new WaitForSeconds(3);
        canMove = true;
        hitStun.enabled = false;
    }
    IEnumerator givePosFeedback()
    {
        imageFeedback.enabled = true;
        yield return new WaitForSeconds(3);
        imageFeedback.enabled = false;
    }

}
