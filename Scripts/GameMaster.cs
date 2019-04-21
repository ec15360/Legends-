// QMUL Final Year Project 2018/19
// Programme of study: BSc(Hons) Computer Science with Business Management with Industrial Experience
// Project Title: Legends: A Quest to Uncover Female Protagonist Throughout History.
// Student Name: Laraib Azam Rajper
// Student ID: 150701938
// Supervisor: Graham White

// Class Discription: Singleton Class, Only ever 1 instance of this class is available
// Features : Controls all states in the game, player states, game movement through scenes
// Source: Brackeys (2016)
// Source: Tutorial Used: https://www.youtube.com/watch?v=UbPiCgCkHTE&t=1085s

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameMaster : MonoBehaviour {

 
    public static GameMaster gm;
    private LevelComplete levelComplete; 
    public static int health;

    public Transform playerPrefab;
    public Transform spawnPoint;
    public int spawnDelay = 2;
    public Transform spawnPrefab;

    public GameObject scoreTextObject;
    static public int score = 0;
    public Text scoreText;
    string scoreWriting = "SCORE | ";

    public GameObject highScoreTextObject;
    static public int highScore = 0;
    public Text highScoreText;
    string highScoreWriting = "HIGHSCORE | ";

    public GameObject livesTextObject;
   
    static public int lives = 3;
    public Text livesText;
    string livesWriting = "LIVES | ";

    public GameObject gameStatsUI;
    public GameObject gameOverUI;
    public GameObject levelCompletedUI;


    public GameObject newHighScoreTextObject;
    public Text newHighScoreText;
    string newHighScoreWriting = "NEW HIGHSCORE | ";
    string finalScoreWriting = "FINAL SCORE ";


    public GameObject livesTextObjects_LC;
    public Text livesText_LC;
    string updatelivesWriting = "LIVES SO FAR | ";

    // This method is used for initialization of variables, called at the start of the scene on which the obj exists
    // Updating Player lives
    // Updating high score
    void Start()
    {
        if (gm == null)
        {
            gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        }


        scoreText = scoreTextObject.GetComponent<Text>();
        scoreText.text = scoreWriting + score;

        highScoreText = highScoreTextObject.GetComponent<Text>();
        highScoreText.text = highScoreWriting + highScore;
       
        lives = 3;
        livesText = livesTextObject.GetComponent<Text>();
        livesText.text = livesWriting + lives;

    }

    // CONTROLLING PLAYER STATES

    public IEnumerator RespawnPlayer()
    {
        yield return new WaitForSeconds(spawnDelay);
        Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
        GameObject clone = Instantiate(spawnPrefab, spawnPoint.position, spawnPoint.rotation).gameObject;
        Destroy(clone, 3f);

        // Play Sound 
        AudioSource source = GetComponent<AudioSource>();
        source.Play();
    }

    // This method wil kill the player object
    // Updating Player lives
    // Updating high score
    public void KillPlayer(Player player)
    {
        Destroy(player.gameObject);
        gm.StartCoroutine(gm.RespawnPlayer());

        if (score > highScore) {
           
            highScore = score;      //make current score, new high score
            HighScore(highScore);   //high score passed
            score = 0;              //resetting score to 0
            scoreText.text = scoreWriting + 0;
        }
        else if ( highScore > score)
        {
            score = 0;              //resetting score to 0
            scoreText.text = scoreWriting + 0;
        }

        lives = lives - 1;
        livesText.text = livesWriting + lives;

        if(lives == 0)
        {
            EndGame();
        }
    }

    public void KillEnemy(Enemy enemy)
    {
        Destroy(enemy.gameObject,1f);

    }


    // GAME ADMIN, CONTROLING OVERALL GAME STATES
    public void KillDialogue(DialogueTrigger diaTrig)
    {
        Destroy(diaTrig.gameObject);

    }

    public void EndGame()
    {
        gameOverUI.SetActive(true);

        // Play Sound 
        AudioSource source = GetComponent<AudioSource>();
        source.Play();
    }
  
    // Updating high score
    public void CompleteLevel()
    {
        Time.timeScale = 0;            // pausing game, prevents player dying further
        gm.gameStatsUI.SetActive(false);
        levelCompletedUI.SetActive(true);

        if (score > highScore)
        {
            highScore = score;         //make current score, new high score
            HighScore(highScore);      //high score passed
            score = 0;                 //resetting score to 0
            scoreText.text = scoreWriting + 0;

            newHighScoreTextObject.SetActive(true);
            newHighScoreText = newHighScoreTextObject.GetComponent<Text>();
            newHighScoreText.text = newHighScoreWriting + highScore;


        }
        else if (highScore > score)
        {
            score = 0;                  //resetting score to 0
            scoreText.text = scoreWriting + 0;
        }

        // update lives 
        livesText.text = livesWriting + lives;
        livesTextObjects_LC.SetActive(true);
        livesText_LC = livesTextObjects_LC.GetComponent<Text>();
        livesText_LC.text = updatelivesWriting + lives; 

    }

    // Updating high score
    public void GameFinished()
    {
        Time.timeScale = 0;             // pausing game, prevents player dying further
        gm.gameStatsUI.SetActive(false);
        levelCompletedUI.SetActive(true);

        if (score > highScore)
        {
            highScore = score;         //make current score, new high score
            HighScore(highScore);      //high score passed
            score = 0;                 //resetting score to 0
            scoreText.text = scoreWriting + 0;

            newHighScoreTextObject.SetActive(true);
            newHighScoreText = newHighScoreTextObject.GetComponent<Text>();
            newHighScoreText.text = finalScoreWriting + highScore;

        }
        else if (highScore > score)
        {
            score = 0;                 //resetting score to 0
            scoreText.text = scoreWriting + 0;
        }
    }


    // CONTROLLING STATES OF COLLECTABLE OBJECTS
    // Destroy collectable 
    // Makes the object disappear after player collision
    // Obj is destroyed after 0.27 secs, to allow sound to play 
    //update score value, then score UI
    public void Collect(int passedValue, GameObject passedObj)
    {
        passedObj.GetComponent<Renderer>().enabled = false;
        passedObj.GetComponent<Collider2D>().enabled = false;
        Destroy(passedObj,0.27f);      

        score = score + passedValue;
        scoreText.text = scoreWriting + score;
    }

    //current score displayed as high score
    public void HighScore(int currentScore)
    {
       highScoreText = highScoreTextObject.GetComponent<Text>();
       highScoreText.text = highScoreWriting + currentScore; 
 
    }

    //destroy collectable 
    //makes the object disappear after player collision
    public void AddLives(int numOfLives, GameObject passedObj)
    {
        passedObj.GetComponent<Renderer>().enabled = false;
        passedObj.GetComponent<Collider2D>().enabled = false;
        Destroy(passedObj, 1f); 

        livesText = livesTextObject.GetComponent<Text>();
        lives = lives + numOfLives;
        livesText.text = livesWriting + lives;

    }

}
