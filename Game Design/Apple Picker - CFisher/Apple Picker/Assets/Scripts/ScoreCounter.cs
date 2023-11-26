using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // This line enables use of uGUI classes like Text.        // a

public class ScoreCounter : MonoBehaviour
{
     [Header("Dynamic")]                                                          // b
     public int score = 0;

     private Text uiText;                                                        // c

     void Start()
    {
        uiText = GetComponent<Text>();                                           // d
    }

     void Update()
    {
        uiText.text = score.ToString("#,0"); // This 0 is a zero!              // e
    }
}
