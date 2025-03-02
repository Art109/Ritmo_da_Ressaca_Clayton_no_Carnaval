using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    // Feature futura
    //float tempo



    public static GameManager instance;
    [SerializeField] GameObject playerPrefab;

    // Cinemachine
    [Header("Cinemachine")]
    [SerializeField] CinemachineVirtualCamera virtualCamera;

    void Start()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else
            instance = this;
    }


    public void EndGame()
    {
        //Logica de Finalizar o Game (CutScene)
    }

    public void RestartGame(Transform savePointPosition, Player player)
    {
        Destroy(player.gameObject);

        GameObject newPlayer = Instantiate(playerPrefab, savePointPosition.position, Quaternion.identity);

        virtualCamera.LookAt = newPlayer.transform;
        virtualCamera.Follow = newPlayer.transform;
    }

}
