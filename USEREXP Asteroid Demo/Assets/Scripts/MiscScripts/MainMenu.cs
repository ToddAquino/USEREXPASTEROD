using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public string soloGame;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene(soloGame);
    }
    public void Settings()
    {

    }
    public void HighScores()
    {

    }
    public void Quit()
    {
        Application.Quit();
    }
}
