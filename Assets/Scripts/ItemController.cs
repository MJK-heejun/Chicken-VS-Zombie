using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public float fallingSpeed = 0.1f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate() {
        float newY = transform.position.y - fallingSpeed;
        this.transform.position = new Vector3(transform.position.x, newY, 0);
    }
}
