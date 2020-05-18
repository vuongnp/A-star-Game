using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;       
using Completed;
using System.Collections.Generic;

public class Player3 : MovingObject
{
    public float speed = 5f;
    private Animator animator;                    
    private bool flag = true;                                                                  
    private GameObject[] wall;
    private GameObject[] fire;
    public GameObject item;
    public GameObject itemFire;
    private GameObject player1;
    private GameObject player2;
    private GameObject player4;
    private GameObject destroy = null;
	private GameObject gameOver;
    private List<Vector3> gridPositions = new List<Vector3>();
    private Rigidbody2D r2;
    private int failScore=5;
    public void initMap()
    {
        gridPositions.Clear();

        for (int x = 2; x < 13; x++)
        {
            for (int y = 2; y < 13; y++)
            {
                gridPositions.Add(new Vector3(x, y, 0f));
            }
        }
        // loại bỏ các vị trí Wall
        int len = wall.Length;
        for (int i = 0; i < len; i++)
        {
            Vector3 tmp = new Vector3(wall[i].transform.position.x, wall[i].transform.position.y, 0f);
            gridPositions.Remove(tmp);
        }
        int len2 = fire.Length;
        // loại bỏ các vị trí Fire nếu có
        if (len2 > 0)
        {
            for (int i = 0; i < len2; i++)
            {
                Vector3 tmp = new Vector3(fire[i].transform.position.x, fire[i].transform.position.y, 0f);
                gridPositions.Remove(tmp);
            }
        }

    }

    public Vector3 RandomPosition()
    {
        int randomIndex = Random.Range(0, gridPositions.Count);
        Vector3 randomPosition = gridPositions[randomIndex];
        gridPositions.RemoveAt(randomIndex);
        return randomPosition;
    }
    protected override void Start()
    {
        animator = GetComponent<Animator>();
        r2 = GetComponent<Rigidbody2D>();
        r2.gravityScale = 0;
        r2.rotation = 0;
		ScoreScipt3.scoreValue3=0;		
        wall = GameObject.FindGameObjectsWithTag("Wall");
        fire = GameObject.FindGameObjectsWithTag("Fire");
        initMap();
        player1 = GameObject.FindGameObjectWithTag("Player1");
        player2 = GameObject.FindGameObjectWithTag("Player2");
        player4 = GameObject.FindGameObjectWithTag("Player4");
		destroy = player1;
    }

    int i = 0;
    private void Tick()
    {
        time += Time.deltaTime;
        Vector2 movement = new Vector2();
        int horizontal = 0;      // di chuyển ngang
        int vertical = 0;       // di chuyển dọc

        horizontal = (int)(Input.GetAxisRaw("Horizontal")); // nếu nhấn nút sang phải(trái)
        vertical = (int)(Input.GetAxisRaw("Vertical")); // nếu nhấn nút lên trên ( xuống dưới)
        if (horizontal != 0)
        {
            vertical = 0;
        }
        movement = new Vector2(horizontal, vertical);
        movement = movement.normalized * 1;

        transform.Translate(movement);
    }
    //Kiểm tra Bot nào sẽ bị phá hủy
    public void CheckDestroy(GameObject player1, GameObject player2, GameObject player4)
    {
        int tmp1 = ScoreScipt1.scoreValue1;
        int tmp2 = ScoreScipt2.scoreValue2;
        int tmp4 = ScoreScipt4.scoreValue4;


        if (player1 == null) tmp1 = 500;
        if (player2 == null) tmp2 = 500;
        if (player4 == null) tmp4 = 500;

        if (tmp1 <= tmp2 && tmp1 <= tmp4) destroy = player1;
        else if (tmp2 <= tmp1 && tmp2 <= tmp4) destroy = player2;
        else if (tmp4 <= tmp2 && tmp4 <= tmp1) destroy = player4;
		else if (tmp1==500&&tmp2==500&&tmp4==500) destroy = null;

    }
    private float deltaT = 0.3f;
    private float time = 0;
    private void Update()
    {
        r2.gravityScale = 0;
        r2.rotation = 0;
        // nếu một trong các Bot đạt 200 điểm thì Game Over
        if (ScoreScipt1.scoreValue1 >= 200 || ScoreScipt2.scoreValue2 >= 200 || ScoreScipt4.scoreValue4 >= 200)
        {
			MonoBehaviour.print("Game Over");
			SceneManager.LoadScene("LoseScene");
		}
		CheckDestroy(player1, player2, player4);
        //Nếu tất cả các Bot đều bị phá hủy thì Win
		if (destroy==null)
		{
			MonoBehaviour.print("You Win");
			SceneManager.LoadScene("WinScene");
		}
        //Nếu Player đạt 200 điểm thì Bot thấp điểm nhất sẽ bị phá hủy, mỗi lần có 1 Bot chết điểm phạt khi va phải lửa của Player tăng lên
		else 
		if (ScoreScipt3.scoreValue3 >= 200)
        {          
                Destroy(destroy);
                ScoreScipt3.scoreValue3 -= 200;
                //failScore += 5;
        }

        transform.Translate(new Vector3(Input.GetAxis("Horizontal") * speed * Time.deltaTime, Input.GetAxis("Vertical") * speed * Time.deltaTime, 0f));
        //Khi nhặt được Weapon sẽ sinh Fire và Weapon mới
        if (flag == false)
        {
            fire = GameObject.FindGameObjectsWithTag("Fire");
            initMap();
            Instantiate(itemFire, RandomPosition(), Quaternion.identity);
            fire = GameObject.FindGameObjectsWithTag("Fire");
            initMap();
            Instantiate(item, RandomPosition(), Quaternion.identity);
            flag = true;
        }

    }
    // Hàm kiểm tra va chạm
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Axe")
        {
            ScoreScipt3.scoreValue3 += 20;
            other.gameObject.SetActive(false);
            animator.SetTrigger("player3Ready");
            flag = false;
        }

        else if (other.tag == "Fire")
        {

            if (ScoreScipt3.scoreValue3 > failScore)
                ScoreScipt3.scoreValue3 -= failScore;
            else ScoreScipt3.scoreValue3 = 0;
        }

    }
    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.name == "Wall")
        {
            Debug.Log("Collision");
            animator.SetTrigger("player3Ready");

        }
    }

}
