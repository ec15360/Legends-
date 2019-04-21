// QMUL Final Year Project 2018/19
// Programme of study: BSc(Hons) Computer Science with Business Management with Industrial Experience
// Project Title: Legends: A Quest to Uncover Female Protagonist Throughout History.
// Student Name: Laraib Azam Rajper
// Student ID: 150701938
// Supervisor: Graham White

// Class Discription: Class interactes with game master to load next level
// Features: Placed on teleportes object, transports player to next level on collision

using UnityEngine;

public class Teleporter : MonoBehaviour {

    public GameMaster gameMaster; 

    void OnTriggerEnter2D(Collider2D col)
    {
        GameMaster.gm.CompleteLevel();

        // Play Sound 
        AudioSource source = GetComponent<AudioSource>();
        source.Play();


    }
}
