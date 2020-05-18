using UnityEngine;
using System;
using System.Collections.Generic;         
using Random = UnityEngine.Random;         

namespace Completed

{
    public class BoardManager : MonoBehaviour
    {
        [Serializable]
        public class Count
        {
            public int minimum;             
            public int maximum;            


            public Count(int min, int max)
            {
                minimum = min;
                maximum = max;
            }
        }


        public int columns = 15;                                      
        public int rows = 15;                                          
        public Count weaponCount = new Count(3, 3);                      
        public GameObject player1;          // Player1
        public GameObject player2;          // Player2
        public GameObject player3;          // Player3
        public GameObject player4;          // Player4                            
        public GameObject floorTiles;       // Floor                           
        public GameObject wallTiles;        // Wall                     
        public GameObject[] weaponTiles;    // Weapon                                                                 
        public GameObject outerWallTiles;   // OuterWall                            
        private Transform boardHolder;      // Vị trí của đối tượng Board    
        private List<Vector3> gridPositions = new List<Vector3>();  // Danh sách lưu các vị trí trên bản đồ

    public void InitialiseList()
        {
            gridPositions.Clear();
            for (int x = 2; x < columns - 2; x++)
            {
                for (int y = 2; y < rows - 2; y++)
                {
                    gridPositions.Add(new Vector3(x, y, 0f));
                }
            }
        }


        // Khởi tạo OuterWall và Floor
        void BoardSetup()
        {
            
            boardHolder = new GameObject("Board").transform;

            //OuterWall bắt đầu từ vị trí bên ngoài map
            for (int x = -1; x < columns + 1; x++)
            {
                for (int y = -1; y < rows + 1; y++)
                {
                    //tạo đối tượng Floor
                    GameObject toInstantiate = floorTiles;

                    //Nếu vị trí đang xét nằm bên ngoài hoặc viền của map thì tạo đối tượng OuterWall
                    if (x == -1 || x == 0 || x == columns || x == columns-1 || y == -1 || y == 0 || y == rows || y == rows-1)
                    {
                        toInstantiate = outerWallTiles;
                        GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;
                        // đặt cha của nó là boardHolder để tránh phân cấp lộn xộn
                        instance.transform.SetParent(boardHolder);
                    }
                     //Nếu không phải outerWall   
                    else if(x>0&&x<columns-1&&y>0&&y<rows-1)
                    {
                        toInstantiate = floorTiles;
                        GameObject instance =
                       Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;                     
                        instance.transform.SetParent(boardHolder);
                    }                                        
                }
            }
        }


        //Trả về một vị trí bất kỳ trên bản đồ chưa có đối tượng nào ngoài Floor
         public Vector3 RandomPosition()
        {           
            int randomIndex = Random.Range(0, gridPositions.Count);
            Vector3 randomPosition = gridPositions[randomIndex];
            gridPositions.RemoveAt(randomIndex);
            return randomPosition;
        }

        //Khởi tạo Wall
        public void LayoutObjectAtRandom(GameObject tileChoice, int objectCount)
        {
            for (int i = 0; i < objectCount; i++)
            {
                //Vị trí đối tượng
                Vector3 randomPosition = RandomPosition();
               
                Instantiate(tileChoice, randomPosition, Quaternion.identity);
            }

        }

        public void SetupScene()
        {
            InitialiseList();
            //Khởi tạo các Player
            Instantiate(player2, new Vector3(columns - 2, 1f, 0f), Quaternion.identity);
            Instantiate(player1, new Vector3(1f, 1f, 0f), Quaternion.identity);           
            Instantiate(player3, new Vector3(1f, rows - 2, 0f), Quaternion.identity);
            Instantiate(player4, new Vector3(columns - 2, rows - 2, 0f), Quaternion.identity);
            LayoutObjectAtRandom(wallTiles, 35);
            BoardSetup();
        }
       
    }
}