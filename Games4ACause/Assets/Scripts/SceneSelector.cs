using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSelector : MonoBehaviour
{
    public Canvas endLevelImage;
    public static SceneSelector sceneInstance;

    private bool canvasInstantiated;

    public Canvas titleScreen;

    //public Canvas titleScreen;


    // Start is called before the first frame update
    void Start()
    {
        canvasInstantiated = false;

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

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            TitleLevel();
        }

        RestartLevel();
    }

    public void EndLevel()
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

    public void NextLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex + 1 > SceneManager.GetActiveScene().buildIndex + 1)
        {

        }
        else
        {
            Debug.Log("YES");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void RestartLevel()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void TitleLevel()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Destroy(GameObject.Find("TitlePage(Clone)"));
        }
    }
}
