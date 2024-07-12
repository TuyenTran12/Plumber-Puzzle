using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject PipesHolder;
    public GameObject[] Pipes;

    [SerializeField]
    int totalPipes = 0;

    public bool isEndGame;

    public int correctPipes = 0;

    public GameObject pnlEndGame;
    public GameObject imgStartGame;
    public GameObject btnGiaigame;

    public bool isClick = false;

    public PipeScript pipiScripts;


    // Start is called before the first frame update
    void Start()
    {
        isEndGame = false;
        //Các UI
        pnlEndGame.SetActive(false);
        imgStartGame.SetActive(true); 
        btnGiaigame.SetActive(false);

        totalPipes = PipesHolder.transform.childCount;
        Pipes = new GameObject[totalPipes];

        for (int i = 0; i < Pipes.Length; i++)
        {
            Pipes[i] = PipesHolder.transform.GetChild(i).gameObject;
        }
    }

    public void correctMove()
    {
        
        correctPipes++;
        if (correctPipes == totalPipes)
        {
            EndGame();
        }

    }
    public void wrongMove()
    {
        correctPipes--;
    }
    public void EndGame()
    {
        isEndGame = true;
        Time.timeScale = 0;
        pnlEndGame.SetActive(true);
    }

    public void ReStartGame()
    {
        SceneManager.LoadScene(0);
    }
    public void StartGame()
    {
        btnGiaigame.SetActive(true);
        Time.timeScale = 1;
        imgStartGame.SetActive(false);
    }
    public void GiaiGame()
    {
        isClick = true;
        foreach (GameObject pipe in Pipes)
        {
            PipeScript pipeScript = pipe.GetComponent<PipeScript>();
            pipeScript.CheckPlacement();
            pipeScript.ApplyHillClimbing();
        }
        EndGame();
    }

}
