// QMUL Final Year Project 2018/19
// Programme of study: BSc(Hons) Computer Science with Business Management with Industrial Experience
// Project Title: Legends: A Quest to Uncover Female Protagonist Throughout History.
// Student Name: Laraib Azam Rajper
// Student ID: 150701938
// Supervisor: Graham White

// Class Discription: Checks plays position, if below a certain degree, kills player
// Features: Kills player when they fall from a platform 

using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public PlayerMovement movement;  // A reference to our PlayerMovement script

    [System.Serializable]
    public class PlayerStats 
    {
        public int Health = 100;   
    }

    public PlayerStats playerStats = new PlayerStats();
    public int fallBoundary = -20;

    public void DamagePlayer(int damage)
    {
        playerStats.Health -= damage;
        if (playerStats.Health <= 0)
        {
            GameMaster.gm.KillPlayer(this);
        }
    }

    void Update()
    {
        if (transform.position.y <= fallBoundary)
            DamagePlayer(999999999);

    }

}