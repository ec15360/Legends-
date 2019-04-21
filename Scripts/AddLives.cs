// QMUL Final Year Project 2018/19
// Programme of study: BSc(Hons) Computer Science with Business Management with Industrial Experience
// Project Title: Legends: A Quest to Uncover Female Protagonist Throughout History.
// Student Name: Laraib Azam Rajper
// Student ID: 150701938
// Supervisor: Graham White

// Class Discription: Class interactes with game master to add lives to players current lives
// Features: Placed on life object, add a life to player obj on collision

using UnityEngine;

public class AddLives : MonoBehaviour {

    public int lives; 

	void OnTriggerEnter2D (Collider2D col)
    {
        GameMaster.gm.AddLives(lives, gameObject);

        // Play Sound 
        AudioSource source = GetComponent<AudioSource>();
        source.Play();
    }


}
