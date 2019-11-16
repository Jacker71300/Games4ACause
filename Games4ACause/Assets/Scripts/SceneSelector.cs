using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSelector : MonoBehaviour
{
    public GameObject endLevelImage;
    public static SceneSelector sceneInstance;


    // Start is called before the first frame update
    void Start()
    {
        endLevelImage = Instantiate(endLevelImage, new Vector3(0, 0, -9), Quaternion.identity);
        endLevelImage.SetActive(false);


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
        if (endLevelImage.activeInHierarchy)
        {
            EndLevel();
        }
    }

    public void EndLevel()
    {
        endLevelImage.SetActive(true);

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

    public void Menu()
    {
    }
}
