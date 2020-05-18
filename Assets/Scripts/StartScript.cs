using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScript : MonoBehaviour
{
    
	public void Quit()
	{
		Application.Quit();
	}
	public void StartGame()
	{
		Application.LoadLevel("SampleScene");
    }
    public void Instruction()
    {
        Application.LoadLevel("InstScene");
    }
}
