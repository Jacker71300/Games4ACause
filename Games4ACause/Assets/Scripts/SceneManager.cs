using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public GameObject endLevelImage;

    // Start is called before the first frame update
    void Start()
    {
        endLevelImage = Instantiate(endLevelImage, new Vector3(0, 0, -9), Quaternion.identity);
        endLevelImage.SetActive(false);
       
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

        if (Input.GetKeyDown(KeyCode.KeypadEnter) && endLevelImage.activeInHierarchy)
        {
            NextLevel();
        }
    }

    public void NextLevel()
    {
    }

    public void RestartLevel()
    {
    }

    public void Menu()
    {
    }
}
