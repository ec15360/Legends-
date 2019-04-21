// QMUL Final Year Project 2018/19
// Programme of study: BSc(Hons) Computer Science with Business Management with Industrial Experience
// Project Title: Legends: A Quest to Uncover Female Protagonist Throughout History.
// Student Name: Laraib Azam Rajper
// Student ID: 150701938
// Supervisor: Graham White

// Class Discription: Class allows developer to enter lines for printing out on screen, recycled for every scene.
// Features: prints diagloge to the screen, with typing effect, called when scene starts
// Source: Brackeys (2016)


using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour {

    public string[] sentances;
    public float typingSpeed;
    private int index;
    public bool pause; 

    public GameObject continueButton;
    public GameObject dialogueWindow;
    public TextMeshProUGUI dialogueText;
    [TextArea(3, 10)]

    void Start()
    {
        dialogueWindow.SetActive(true);
        Time.timeScale = 0;
        StartCoroutine(Type());

        // Play Sound 
        AudioSource source = GetComponent<AudioSource>();
        source.Play();
    }

    public IEnumerator Type() 
    {
        foreach (char letter in sentances[index].ToCharArray())
        {
            dialogueText.text += letter; 
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    void Update()
    {
        if (dialogueText.text == sentances[index])
        {
            continueButton.SetActive(true);
        }
    }

    public void DisplayNextSentance()
    {
        pause = true;
        continueButton.SetActive(false);

        if (index < sentances.Length - 1)
        {
            index++;
            dialogueText.text = " ";
            StartCoroutine(Type());
        }
        else
        {
            pause = false;
            dialogueWindow.SetActive(false);
        }

        if (pause == true)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1; //unpausing
        }


        dialogueText.text = " ";
        continueButton.SetActive(false);

    }

}
