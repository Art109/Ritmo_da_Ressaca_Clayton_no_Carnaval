using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{

   
    Rigidbody rb;
    float moveSpeed = 2;
    [Header("GridMovement Settings")]
    [SerializeField] Transform movePoint;
    [SerializeField] LayerMask obstacleLayer;
    [SerializeField] float obstacleCheckRadius;

    [Header("Interaction Settings")]
    [SerializeField] LayerMask interactableLayer;
    [SerializeField] float interactionCheckRadius;


    int playerScore = 0;
    public int PlayerScore {  get { return PlayerScore; } }

    [SerializeField] float gliterAmount;
    float maxGliterAmount = 100;

    
    //Transform lastSavePoint;
    bool isAlive = true;
    bool canSpentGliter = true;

    // VFX
    [Header("VFX - Player")]
    public Vector3 playerWalkVFXOffset;

    private ParticleSystem _playerWalkVFX;

    void Start()
    {
        gliterAmount = maxGliterAmount;
        rb = GetComponent<Rigidbody>();

        movePoint.parent = null;

        _playerWalkVFX = VFXManager.Instance.GetPermanentVFXByType(VFXManager.VFXType.WALK,
            this.transform.position, playerWalkVFXOffset, this.transform);
    }


    void Update()
    {
        if (isAlive)
        {
            //Movement();
            Interaction();
            GridMovement();
            DeathTrigger();
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(movePoint.position, obstacleCheckRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionCheckRadius);
    }

    #region PlayerMovement
    void Movement()
    {
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;

        rb.velocity = input * moveSpeed;
    }

    void GridMovement()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputZ = Input.GetAxisRaw("Vertical");

        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        transform.LookAt(movePoint);


        if (Vector3.Distance(transform.position, movePoint.position) < 0.5f)
        {
            if (inputX != 0)
            {
                movePoint.position += new Vector3(inputX, 0, 0);
                if (canSpentGliter)
                {
                    gliterAmount -= 1;
                    var emission = _playerWalkVFX.emission;
                    emission.rateOverDistance = new ParticleSystem.MinMaxCurve(gliterAmount);
                    playerScore += 1;
                }
            }
            else if (inputZ != 0)
            {
                movePoint.position += new Vector3(0, 0, inputZ);
                if (canSpentGliter)
                {
                    gliterAmount -= 1;
                    var emission = _playerWalkVFX.emission;
                    emission.rateOverDistance = new ParticleSystem.MinMaxCurve(gliterAmount);
                    playerScore += 1;
                }
            }

            Collider[] obstacles = Physics.OverlapSphere(movePoint.position, obstacleCheckRadius, obstacleLayer);
            if (obstacles.Length > 0)
            {
                movePoint.position = transform.position;
                canSpentGliter = false;
            }
            else
            {
                canSpentGliter = true;
            }

        }

    }
    #endregion

    void TakeGliter()
    {
        gliterAmount += 50;
        if (gliterAmount > maxGliterAmount)
            gliterAmount = maxGliterAmount;
    }

    void DeathTrigger()
    {
        if (gliterAmount <= 0 && isAlive)
        {
            isAlive = false;
            StartCoroutine(Death());
        }

    }

    IEnumerator Death()
    {
        //Anima��o de morrer;
        transform.Rotate(0, 0, 90);

        yield return new WaitForSeconds(2f);

        /*
        if (lastSavePoint != null)
            GameManager.instance.RestartGame(lastSavePoint, this);
        else
        {
            GameManager.instance.EndGame();
            Destroy(gameObject);
        }
        */
        GameManager.instance.EndGame();
        Destroy(gameObject);

        yield return null;
    }

    /*
    void SavePosition(Transform position)
    {
        if(lastSavePoint ==  null || lastSavePoint != position)
            lastSavePoint = position;
        Debug.Log(lastSavePoint);
    }*/

    #region PlayerInteraction
    void Interaction()
    {
        Collider[] interactables = Physics.OverlapSphere(transform.position, interactionCheckRadius, interactableLayer);

        foreach(var interactable in interactables)
        {
            IInteractable inter = interactable.GetComponent<IInteractable>();
          
            if (interactable.CompareTag("GliterPot"))
            {
                TakeGliter();
            }

            /*
            if (interactable.CompareTag("SavePoint"))
            {
                SavePosition(interactable.transform);
            }*/

            if(inter != null)
                inter.Interaction();
        }

    }
    #endregion


    #region PLAYER_VFX
    private void EnableWalkVFX()
    {
        if (!_playerWalkVFX.isPlaying)
        {
            _playerWalkVFX.Play();
        }
    }

    private void DisableWalkVFX()
    {
        if (_playerWalkVFX.isPlaying)
        {
            _playerWalkVFX.Stop();
        }
    }
    #endregion
}
