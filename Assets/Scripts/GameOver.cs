using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
   public void ExitGame()
   {
        Application.Quit();
   }

   public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}
