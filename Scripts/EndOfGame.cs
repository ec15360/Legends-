// QMUL Final Year Project 2018/19
// Programme of study: BSc(Hons) Computer Science with Business Management with Industrial Experience
// Project Title: Legends: A Quest to Uncover Female Protagonist Throughout History.
// Student Name: Laraib Azam Rajper
// Student ID: 150701938
// Supervisor: Graham White

// Class Discription: Class for final teleporter object 
// Features: Ends game, moves player to final end credits screen

using UnityEngine;

public class EndOfGame : MonoBehaviour {

    public GameMaster gameMaster;

    void OnTriggerEnter2D(Collider2D collision)
    {
        GameMaster.gm.GameFinished();

        // Play Sound 
        AudioSource source = GetComponent<AudioSource>();
        source.Play();

    }
}
