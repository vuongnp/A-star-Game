using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScipt4 : MonoBehaviour
{
	public static int scoreValue4 = 0;
	Text score4;	
    void Start()
    {
      score4 = GetComponent<Text>();
    }
    void Update()
    {
        score4.text = "Bot 3    " +scoreValue4 ;
    }
}
