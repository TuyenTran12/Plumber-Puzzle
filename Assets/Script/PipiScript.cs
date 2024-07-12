using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeScript : MonoBehaviour
{
    float[] rotation = { 0, 90, 180, 270 };

    public float[] correctRotation;
    [SerializeField]
    bool isPlaced = false;

    AudioSource audioSource;

    int possibleRots = 1;

    public GameController gameController;

    private void Awake()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    private void Start()
    {
        Time.timeScale = 0;

        audioSource = GetComponent<AudioSource>();

        possibleRots = correctRotation.Length;

        int rand = Random.Range(0, rotation.Length);
        transform.eulerAngles = new Vector3(0, 0, rotation[rand]);


        //Khi bắt đầu nếu các pipe có toạ độ z trùng với toạ độ đc gắn thì trả về true
        if (possibleRots > 1)
        {
            if (Mathf.Round(transform.eulerAngles.z) == correctRotation[0] || transform.eulerAngles.z == correctRotation[1])
            {
                isPlaced = true;
                gameController.correctMove();
            }
        }
        else
        {
            if (Mathf.Round(transform.eulerAngles.z) == correctRotation[0])
            {
                isPlaced = true;
                gameController.correctMove();
            }
        }

    }

    private void OnMouseDown()
    {

        if (gameController.GetComponent<GameController>().isEndGame == false)
        {
            audioSource.PlayOneShot(audioSource.clip);
            //Khi click vào pipe thì sẽ xoay 90 độ
            transform.Rotate(new Vector3(0, 0, 90));

            //Nếu các pipe có toạ độ z trùng với toạ độ đc gắn thì trả về true ngược lại false

            if (possibleRots > 1)
            {
                if ((Mathf.Round(transform.eulerAngles.z) == correctRotation[0] || transform.eulerAngles.z == correctRotation[1]) && isPlaced == false)
                {
                    isPlaced = true;
                    gameController.correctMove();
                }
                else if (isPlaced == true)
                {
                    isPlaced = false;
                    gameController.wrongMove();
                }
            }
            else
            {
                if (Mathf.Round(transform.eulerAngles.z) == correctRotation[0] && isPlaced == false)
                {
                    isPlaced = true;
                    gameController.correctMove();
                }
                else if (isPlaced == true)
                {
                    isPlaced = false;
                    gameController.wrongMove();
                }
            }
        }
    }

    private int EvaluateRotation(int rotationIndex)
    {
        int score = 0;

        for (int i = 0; i < correctRotation.Length; i++)
        {
            if (Mathf.Round(rotation[rotationIndex]) == correctRotation[i])
            {
                score++;
            }
        }

        return score;
    }



    public void CheckPlacement()
    {
        int currentScore = EvaluateRotation(GetRotationIndex(transform.eulerAngles.z));

        if (currentScore > 0 && !isPlaced)
        {
            isPlaced = true;
            gameController.correctMove();
        }
        else if (currentScore == 0 && isPlaced)
        {
            isPlaced = false;
            gameController.wrongMove();
        }
    }

    private int GetRotationIndex(float rotationZ)
    {
        for (int i = 0; i < rotation.Length; i++)
        {
            if (Mathf.Round(rotationZ) == rotation[i])
            {
                return i;
            }
        }

        return -1;
    }



    public void ApplyHillClimbing()
    {
        int currentRotationIndex = GetRotationIndex(transform.eulerAngles.z);
        int bestRotationIndex = currentRotationIndex;
        int bestScore = EvaluateRotation(currentRotationIndex);

        while (bestScore < possibleRots)
        {
            int nextRotationIndex = (currentRotationIndex + 1) % rotation.Length;
            int nextScore = EvaluateRotation(nextRotationIndex);

            if (nextScore >= bestScore)
            {
                bestRotationIndex = nextRotationIndex;
                bestScore = nextScore;
            }
            else
            {
                break;
            }

            currentRotationIndex = nextRotationIndex;
        }

        transform.eulerAngles = new Vector3(0, 0, rotation[bestRotationIndex]);
    }
}