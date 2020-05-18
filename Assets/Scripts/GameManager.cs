using UnityEngine;
using System.Collections;

using System.Collections.Generic;        
using Completed;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;                
    private BoardManager boardScript;                       
    public GameObject axe;
    public float time = 0.5f;
    private bool flag;
    public bool getFlag()
    {
        return this.flag;
    }
    public void setFlag(bool flag)
    {
        this.flag = flag;
    }
    public GameManager(bool flag)
    {
        this.flag = flag;
    }
	

    void Awake()
    {
        //kiểm tra đối tượng đã sẵn sàng chưa
        if (instance == null)

            //nếu chưa
            instance = this;

        //nếu đối tượng đã sẵn sàng
        else if (instance != this)
           
            Destroy(gameObject);

        boardScript = GetComponent<BoardManager>();


        InitGame();
    }

    void InitGame()
    {
        //Khởi tạo Game
        boardScript.SetupScene();
    }

    void Update()
    {
       
    }
	void Restart()
	{
		Application.LoadLevel(Application.loadedLevel);
	}
}