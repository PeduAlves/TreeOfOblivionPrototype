using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public void StartGame(){

        SceneManager.LoadScene("PrototypeLevel");
        Time.timeScale = 1f;
    }
}
