using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    public static float bottomY = -20f;

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < bottomY)
        {
            Destroy(this.gameObject);                                 // c
        
        // Get a reference to the ApplePicker component of Main Camera
        ApplePicker apScript = Camera.main.GetComponent<ApplePicker>();           // b
        // Call the public AppleMissed() method of apScript
        apScript.AppleMissed();
        }
    }
}
