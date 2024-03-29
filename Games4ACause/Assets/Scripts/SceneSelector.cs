﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSelector : MonoBehaviour
{
    public Canvas endLevelImage;
    public static SceneSelector sceneInstance;

    private bool canvasInstantiated;

    public Canvas titleScreen;

    private GameObject character;

    public Canvas youWin;

    public Canvas youLose;

    public Canvas pauseScreen;

    public bool win;
    public bool lose;
    public bool pause;
    public bool pauseFirst;
    public int totalLevelCount;

    //public Canvas titleScreen;


    // Start is called before the first frame update
    void Start()
    {
        canvasInstantiated = false;
        win = false;
        lose = false;
        pause = false;
        pauseFirst = false;
        totalLevelCount = 16;

        character = GameObject.Find("Character");

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            titleScreen = Instantiate(titleScreen, Vector3.zero, Quaternion.identity);
        }

        if (sceneInstance == null)
        {
            sceneInstance = this;
        }
        else
        {
            Destroy(this);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (canvasInstantiated)
        {
            EndLevel();
        }

        if (SceneManager.GetActiveScene().name == "Level1")
        {
            TitleLevel();
        }

        if (win)
        {
            EndLevel();
        }
        else
        {
            RestartLevel();
            FailLevel();
            PauseScreen();
        }

    }

    public void EndLevel()
    {
        if (SceneManager.GetActiveScene().name.Substring(0, 5) == "Level" && int.Parse(SceneManager.GetActiveScene().name.Substring(5)) >= totalLevelCount)
        {

            Debug.Log("AHHAHAH");
            if (win == false)
            {
                youWin = Instantiate(youWin, new Vector3(0, 0, -9), Quaternion.identity);
                win = true;
            }


            if (UnityEngine.Physics.gravity.y > 0)
            {
                UnityEngine.Physics.gravity *= -1;
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene(0);
            }
        }
        else
        {
            if (canvasInstantiated == false)
            {
                endLevelImage = Instantiate(endLevelImage, new Vector3(0, 0, -9), Quaternion.identity);
                canvasInstantiated = true;
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                NextLevel();
            }
        }

    }

    public void NextLevel()
    {

        Debug.Log("ye");
            if (UnityEngine.Physics.gravity.y > 0)
            {
                UnityEngine.Physics.gravity *= -1;
            }

        string currentScene = SceneManager.GetActiveScene().name;
        if(currentScene.Substring(0, 5) == "Level")
        {
            int levelIndex = int.Parse(currentScene.Substring(5));
            levelIndex++;
            SceneManager.LoadScene("Level" + levelIndex);
        }
    }

    public void RestartLevel()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (UnityEngine.Physics.gravity.y > 0)
            {
                UnityEngine.Physics.gravity *= -1;
            }
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void TitleLevel()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Destroy(GameObject.Find("TitlePage(Clone)"));
        }
        else if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void FailLevel()
    {
        if ((character.transform.position.y < (Camera.main.orthographicSize * -1) && UnityEngine.Physics.gravity.y < 0) ||
            (character.transform.position.y > Camera.main.orthographicSize && UnityEngine.Physics.gravity.y > 0))
        {

            if (!lose)
            {
                youLose = Instantiate(youLose, new Vector3(0, 0, -9), Quaternion.identity);
                lose = true;
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                if (UnityEngine.Physics.gravity.y > 0)
                {
                    UnityEngine.Physics.gravity *= -1;
                }
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (UnityEngine.Physics.gravity.y > 0)
                {
                    UnityEngine.Physics.gravity *= -1;
                }

                SceneManager.LoadScene(0);
            }
        }
    }

    public void QuitGame()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }
    }

    public void PauseScreen()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (pause == false)
            {
                if (pauseFirst == false)
                {
                    pauseScreen = Instantiate(pauseScreen, new Vector3(0, 0, -9), Quaternion.identity);
                    pauseFirst = true;
                }
                else if (pauseFirst)
                {
                    pauseScreen.enabled = true;
                }
                pause = true;
            }
        }

        if (pause)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                pause = false;
                pauseScreen.enabled = false;
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                if (UnityEngine.Physics.gravity.y > 0)
                {
                    UnityEngine.Physics.gravity *= -1;
                }
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (UnityEngine.Physics.gravity.y > 0)
                {
                    UnityEngine.Physics.gravity *= -1;
                }

                SceneManager.LoadScene(0);
            }
        }
    }
}
