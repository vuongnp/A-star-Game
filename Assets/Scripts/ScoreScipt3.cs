﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScipt3 : MonoBehaviour
{
	public static int scoreValue3 = 0;
	Text score3;
	
    void Start()
    {
      score3 = GetComponent<Text>();
    }

    void Update()
    {
        score3.text = "Player  " +scoreValue3 ;
    }
}
