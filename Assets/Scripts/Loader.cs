using UnityEngine;
using System.Collections;


public class Loader : MonoBehaviour
{
    public GameObject gameManager;            //GameManager prefab 
    public GameObject soundManager;            //SoundManager prefab 

	void Start()
	{
		
	}
    void Awake()
    {
	     
        if (GameManager.instance == null)
		{
			Instantiate(gameManager);
		}
    }
}