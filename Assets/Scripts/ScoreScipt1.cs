using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScipt1 : MonoBehaviour
{
	public static int scoreValue1 = 0;
	Text score1;
	
    void Start()
    {
      score1 = GetComponent<Text>();
    }
    void Update()
    {
        score1.text = "Bot 1    " +scoreValue1 ;
    }
}
