using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocoCarnaval : MonoBehaviour, IInteractable
{
    [TextArea]
    [SerializeField] string alert;


    [SerializeField] bool playerBloco;
    bool hasInteracted = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interaction()
    {
        if (playerBloco)
            GameManager.instance.EndGame();
        else
            StartCoroutine(Alert());
    }

    IEnumerator Alert()
    {
        if (!hasInteracted)
        {
            GameManager.instance.StartDialogue(alert);
            hasInteracted = true;

            yield return new WaitForSeconds(20f);

            Debug.Log("Ta liberado a interação");
            hasInteracted = false;
        }

        
    }
}
