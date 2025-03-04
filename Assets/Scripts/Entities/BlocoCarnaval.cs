using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocoCarnaval : MonoBehaviour, IInteractable
{
    [TextArea]
    [SerializeField] string alert;


    [SerializeField] bool playerBloco;
    bool hasInteracted = false;

    public List<GameObject> cameras;

    public void Interaction()
    {
        if (playerBloco)
        {
            StartCoroutine(Win());
        }
        else
            StartCoroutine(Alert());
    }

    void Update()
    {
        if (cameras[0].activeSelf) 
        {
            Vector3 direction = cameras[0].transform.position - Player.instance.transform.position;
            direction.y = 0;

            if (direction.sqrMagnitude > 0.01f)
            {
                Player.instance.transform.rotation = Quaternion.LookRotation(direction);
            }
        }
    }


    IEnumerator Alert()
    {
        if (!hasInteracted)
        {
            GameManager.instance.StartDialogue(alert);
            hasInteracted = true;

            yield return new WaitForSeconds(20f);

            //Debug.Log("Ta liberado a interação");
            hasInteracted = false;
        }
    }

    IEnumerator Win()
    {
        if (!hasInteracted)
        {
            hasInteracted = true;
            Player.instance.Animator.SetTrigger("Win");
            TradeToWinCam();
            Player.instance.FoundObjective = true;
            yield return new WaitForSeconds(6f);
            GameManager.instance.EndGame();
        }
    }

    private void TradeToWinCam()
    {
        cameras[0].SetActive(true);
        Player.instance.transform.LookAt(cameras[0].transform.position);
    }
}
