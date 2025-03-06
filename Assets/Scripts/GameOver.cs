using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
   public void ExitGame()
   {
        SceneManager.LoadScene("Main Menu");
   }

   public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}
