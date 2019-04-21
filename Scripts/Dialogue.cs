// QMUL Final Year Project 2018/19
// Programme of study: BSc(Hons) Computer Science with Business Management with Industrial Experience
// Project Title: Legends: A Quest to Uncover Female Protagonist Throughout History.
// Student Name: Laraib Azam Rajper
// Student ID: 150701938
// Supervisor: Graham White

// Class Discription: Type Dialogue
// Features: Object attributes for Dialogue
// Source: Brackeys (2016)
// Source: Tutorial Used: https://www.youtube.com/watch?v=_nRzoTzeyxU&t=448s 

using UnityEngine;

[System.Serializable]
public class Dialogue {

    public string name;
    [TextArea(3,10 )]
    public string[] sentances;

}
