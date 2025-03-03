using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    // Feature futura
    //float tempo

    [Header("Dialogue Box Settings")]
    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] GameObject dialoguePanel;
    [SerializeField] float textSpeed;
    bool isTyping = false;


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

    private void Update()
    {
        if (!isTyping)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartDialogue("Aaahhh...Tá legal...");
            }
        }
        
    }

    public void EndGame()
    {
        //Logica de Finalizar o Game (CutScene)
    }

    public void RestartGame(Transform savePointPosition, Player player)
    {
        Destroy(player.gameObject);

        GameObject newPlayer = Instantiate(playerPrefab, savePointPosition.position, Quaternion.identity);

        virtualCamera.Follow = newPlayer.transform;
    }

    public void StartDialogue(string lines)
    {
        
        if (!dialoguePanel.activeInHierarchy)
        {
            dialoguePanel.SetActive(true);
        }
        dialogueText.text = string.Empty;

        
        StartCoroutine(TypeText(lines));


    }

    IEnumerator TypeText(string text)
    {
        
        isTyping = true;

        foreach(char c in text.ToCharArray())
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }

        Debug.Log("Terminei de digitar");

        isTyping = false;

        yield return new WaitForSeconds(1f);

        if(!isTyping)
            dialoguePanel.SetActive(false);

    }

}
