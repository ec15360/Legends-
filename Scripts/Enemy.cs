// QMUL Final Year Project 2018/19
// Programme of study: BSc(Hons) Computer Science with Business Management with Industrial Experience
// Project Title: Legends: A Quest to Uncover Female Protagonist Throughout History.
// Student Name: Laraib Azam Rajper
// Student ID: 150701938
// Supervisor: Graham White

// Class Discription: Placed on enemy objects, recycled through the game, ensures efficeny
// Features: Checks in enemy collides with player, if yes, kills player, calls gm to take away a life, enemy's killing animation starts
// Source:

using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

    public Animator animator;           // Ref to enemy animator for changing states 
    public GameMaster gameMaster;       // Ref to game Master
    public Player player;               // Ref to player obj
    public float speed;                 // how fast enemy will move
    float distance = 1;
    private bool movingRight = true;    // tells character where to move
    public Transform groundDetction;
    private bool searchingForPlayer = false;

    void Start()
    {
        if (player == null)
        {
            if (!searchingForPlayer)
                {
                    searchingForPlayer = true;
                    StartCoroutine(SearchForPlayer());
                }
        }
    }

    // Patrolling Behaviour - using invisible beams (rays) 
    void Update () {

        // Character moving forward using pysics 
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        // character moving left using pysics 
        // origin, direction, lenghth of the invisable ray 
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetction.position, Vector2.down, distance);

        // checking if ray has collided with anything 

        if (groundInfo.collider == false)
        {
            if (movingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }

    }

    // if player is not attacking, kill player
    void OnTriggerEnter2D(Collider2D collision)
    {
        animator.SetBool("IsAttacking", true);
        if (player == null)
        {
            if (!searchingForPlayer)
            {
                searchingForPlayer = true;
                StartCoroutine(SearchForPlayer());
            }
        }
        else
        {
            animator.SetBool("IsAttacking", true);
            gameMaster.KillPlayer(player);
        }

    }

    // looking for player
    // returns game object, we need transform 
    IEnumerator SearchForPlayer()
    {
        GameObject sResult = GameObject.FindWithTag("Player");
        if(sResult == null)
        {
           
            yield return new WaitForSeconds(0.5f); 
            StartCoroutine(SearchForPlayer());
        }
        else
        {
            //if player is found - kill
            player = sResult.GetComponent<Player>(); 
            gameMaster.KillPlayer(player);
            searchingForPlayer = false;
            yield return false; 
        }
    }

}
