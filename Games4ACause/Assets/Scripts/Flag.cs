using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    Collider myCollider;
    Collider playerCollider;
    SceneManager sceneManager;
    // Start is called before the first frame update
    void Start()
    {
        myCollider = gameObject.GetComponent<Collider>();
        playerCollider = GameObject.Find("Character").GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        if(myCollider.bounds.Intersects(playerCollider.bounds))
        {
            sceneManager.EndLevel();
        }
    }
}
