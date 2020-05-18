using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScipt2 : MonoBehaviour
{
	public static int scoreValue2 = 0;
	Text score2;
	
    void Start()
    {
      score2 = GetComponent<Text>();
    }

    void Update()
    {
        score2.text = "Bot 2    " +scoreValue2 ;
    }
}
