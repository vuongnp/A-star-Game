using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Instruction : MonoBehaviour
{

    public void Back()
    {
        Application.LoadLevel("StartScene");
    }
    public void StartGame()
    {
        Application.LoadLevel("SampleScene");
    }
}
