using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

[DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
public class AppleTree : MonoBehaviour
{
    [Header("Inscribed")]                                                  // a
    // Prefab for instantiating apples
    public GameObject applePrefab;

    // Speed at which the AppleTree moves
    public float speed = 1f;

    // Distance where AppleTree turns around
    public float leftAndRightEdge = 10f;

    // Chance that the AppleTree will change directions
    public float changeDirChance = 0.1f;

    // Seconds between Apples instantiations
    public float appleDropDelay = 1f;

    void Start()
    {
        // Start dropping apples                                          
        Invoke("DropApple", 2f);                                        // a
    }

     void DropApple()
    {                                                    // b
        GameObject apple = Instantiate<GameObject>(applePrefab);        // c
        apple.transform.position = transform.position;                    // d
        Invoke("DropApple", appleDropDelay);                            // e
    }


    void Update()
    {
        // Basic Movement                                                  
        Vector3 pos = transform.position;                        // b
        pos.x += speed * Time.deltaTime;                         // c
        transform.position = pos;                                // d

        // Changing Direction
        if (pos.x < -leftAndRightEdge)
        {                                        // a
            speed = Mathf.Abs(speed);   // Move right                           // b
        }
        else if (pos.x > leftAndRightEdge)
        {                                  // c
            speed = -Mathf.Abs(speed);  // Move left                            // c
        }
        //else if (Random.value < changeDirChance)
        //{                         // a
        //    speed *= -1;  // Change direction 
        //}
    }

    void FixedUpdate()
    {
        // Random direction changes are now time-based due to FixedUpdate()
        if (Random.value >= changeDirChance)
        {
            return;
        }
        speed *= -1; // Change direction 
    }


    private string GetDebuggerDisplay()
    {
        return ToString();
    }
}