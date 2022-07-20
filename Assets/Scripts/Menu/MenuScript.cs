using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public GameObject menuScreen;
    public PlayerControls player;
    
    // Start is called before the first frame update
    void Start()
    {
        menuScreen.gameObject.SetActive(true);
        player.pausedState = true;
    }

    void Update()
    {
        //if menu is active
        if (menuScreen.gameObject.activeSelf == true)
        {
            player.PauseGame();
        }
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Application has Quit");
    }

    public void Resume()
    {
            player.UnPause();
            Debug.Log("Resumed");
    }

    public void Restart(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
        Debug.Log("Loaded Scene Index " + sceneNumber);
    }

}
