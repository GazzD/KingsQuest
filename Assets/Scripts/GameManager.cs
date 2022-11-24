using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [SerializeField] float backgroundSpeed = 0.015f;
    [SerializeField] Renderer background;
    [SerializeField] GameObject floorCol;
    [SerializeField] GameObject rock1;
    [SerializeField] GameObject rock2;
    [SerializeField] GameObject titleScreen;
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] float mapSpeed = 2;
    public bool gameOver = false;
    public bool gameStarted = false;

    public List<GameObject> floorCols;
    public List<GameObject> obstacles;

    public static GameManager Instance;

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        // Create floor
        for(int i=0; i<21; i++)
        {
            floorCols.Add(Instantiate(floorCol, new Vector2(-13 + i, -4), Quaternion.identity));
        }

        // Create obstacles
        obstacles.Add(Instantiate(rock1, new Vector2(15, -2), Quaternion.identity));
        obstacles.Add(Instantiate(rock2, new Vector2(-18, -2), Quaternion.identity));
    }

    void Update()
    {
        titleScreen.SetActive(!gameStarted);
        gameOverScreen.SetActive(gameOver);

        if (gameStarted == false && Input.GetKeyDown(KeyCode.X))
        {
            gameStarted = true;
            
        }

        if (gameOver == true && Input.GetKeyDown(KeyCode.X))
        {
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // reload scene
        }

        if (gameStarted && !gameOver)
        {
            // Move background
            background.material.mainTextureOffset += new Vector2(backgroundSpeed, 0) * Time.deltaTime;

            // Move map (to simulate player movement)
            for (int i = 0; i < floorCols.Count; i++)
            {
                // Reset passed floor colums to the front
                if (floorCols[i].transform.position.x < -14)
                {
                    floorCols[i].transform.position = new Vector3(5, -4);
                }
                // floor movement
                floorCols[i].transform.position += new Vector3(-1, 0) * Time.deltaTime * mapSpeed;
            }

            // Move obstacles
            for (int i = 0; i < obstacles.Count; i++)
            {
                if (obstacles[i].transform.position.x < -14)
                {

                    float randomPosition = Random.Range(7, 12);
                    obstacles[i].transform.position = new Vector3(randomPosition, -2);
                }
                // Move obstacles with the floor
                obstacles[i].transform.position += new Vector3(-1, 0) * Time.deltaTime * mapSpeed;
            }
        }

        

    }
}
