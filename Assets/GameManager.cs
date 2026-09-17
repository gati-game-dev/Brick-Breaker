using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int score = 0;
    public bool gameOver = false;
    private bool isWaiting;
    public bool IsWaiting => isWaiting;  // encapsulation (only game manager can modify this value, others can only see it)
    public int life = 3;
    public int bricksRemaining;
    private BallMovement ball;
    private BallMovement currentBall;
    private LevelUI levelUI;
    public GameObject ballPrefab;
    public int activeBalls = 1;
    public PaddleMovement paddle;
 

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    void Start()
    {
        SetupLevel();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && gameOver)
        {
            RestartGame();
        }
        else if (Input.anyKeyDown && isWaiting && !gameOver)
        {
            ball.SetRandomVelocity();

            isWaiting = false;
            levelUI.HideLifeLost();
            levelUI.HidePressKey();
        }
    }


    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SetupLevel();
    }

    void SetupLevel()
    {
        ball = FindAnyObjectByType<BallMovement>();
        paddle = FindAnyObjectByType<PaddleMovement>();
        levelUI = FindAnyObjectByType<LevelUI>();

        bricksRemaining = FindObjectsByType<Brick>().Length;

        activeBalls = 1;
        isWaiting = true;

        levelUI.UpdateScore(score);
        levelUI.UpdateHeartsUI(life);
    }

    public void UpdateScore()
    {
        score += 1;
        levelUI.UpdateScore(score);
    }


    public void LoseLife()
    {
        isWaiting = true;
        life -= 1;
        DestroyAllPowerUps();
        levelUI.UpdateHeartsUI(life);
        if (life <= 0)
        {
            GameOver();           
        }
        else
        {
            activeBalls = 1;
            CreateNewBall();
            levelUI.ShowLifeLost();
        }   
    }
    public void RestartGame()
    {
        score = 0;
        life = 3;
        gameOver = false;

        Time.timeScale = 1;

        SceneManager.LoadScene(0);
    }

    public void GameOver()
    {
        gameOver = true;
        Time.timeScale = 0;
        levelUI.ShowGameOver();
    }

    public void WinGame()
    {
        Time.timeScale = 0;
        levelUI.ShowWin();
    }


    public void CountBricks()
    {
        bricksRemaining -= 1;

        if (bricksRemaining <= 0)
        {
            int currentLevel = SceneManager.GetActiveScene().buildIndex;
            if (currentLevel < 2)
            {
                SceneManager.LoadScene(currentLevel + 1);
            }
            else
            {
                WinGame();
            }
        }
    }
    public void ActivateThreeBallPowerUp()
    {
        if (activeBalls != 1)
        {
            return;
        }
        
        BallMovement currentBall = FindAnyObjectByType<BallMovement>();

        // No ball exists, so we can't create the extra balls
        if (currentBall == null)
        {
            return;
        }


        GameObject newBall1 = Instantiate(
            ballPrefab,
            currentBall.transform.position,
            Quaternion.identity                     
        );                            // create a new ball at the position of the current ball using the ball prefab 

        BallMovement newBallMovement1 = newBall1.GetComponent<BallMovement>();  // get the BallMovement component of the new ball 
        newBallMovement1.SetRandomVelocity();          // set a random velocity for the new ball
 

        GameObject newBall2 = Instantiate(
            ballPrefab,
            currentBall.transform.position,
            Quaternion.identity
        );

        BallMovement newBallMovement2 = newBall2.GetComponent<BallMovement>();
        newBallMovement2.SetRandomVelocity();

        activeBalls = 3;
    }

    public void BallLost(GameObject lostBall)
    {
        activeBalls -= 1;
        Destroy(lostBall);

        if (activeBalls <= 0)
        {
            LoseLife();
        }
    }  
    public void CreateNewBall()
    {
        GameObject newBall = Instantiate(
            ballPrefab,
            new Vector2(paddle.transform.position.x, -3.45f),
            Quaternion.identity
        );                       // create a new ball at the position of the paddle using the ball prefab  

        ball = newBall.GetComponent<BallMovement>();    // get the BallMovement component of the new ball

        isWaiting = true;
    }

    void DestroyAllPowerUps()
    {
        GameObject[] powerUps = GameObject.FindGameObjectsWithTag("PowerUp");

        foreach (GameObject powerUp in powerUps)
        {
            Destroy(powerUp);
        }
    }
}
