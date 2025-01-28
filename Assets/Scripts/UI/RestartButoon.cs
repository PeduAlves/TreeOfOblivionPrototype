using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButoon : MonoBehaviour
{
   public void RestartButton(){

    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    Time.timeScale = 1;
   }
}
