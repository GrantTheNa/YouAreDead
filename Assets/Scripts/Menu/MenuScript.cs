using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public GameObject menuScreen;
    // Start is called before the first frame update
    void Start()
    {
        menuScreen.SetActive(true);
    }


    public void Quit()
    {
        Application.Quit();
        Debug.Log("Application has Quit");
    }

    public void Resume()
    {
        menuScreen.SetActive(false);
        //unpause the player

    }

    public void Restart(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
        Debug.Log("Loaded Scene Index " + sceneNumber);
    }

}
