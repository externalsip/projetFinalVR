using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startMenu : MonoBehaviour
{

    public GameObject mainMenu;
    public GameObject helpMenu1;
    public GameObject helpMenu2;

    public GameObject helpTitle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startGame()
    {
        SceneManager.LoadScene("mainGame");
    }

    public void quitGame()
    {
        Application.Quit();
    }


}
