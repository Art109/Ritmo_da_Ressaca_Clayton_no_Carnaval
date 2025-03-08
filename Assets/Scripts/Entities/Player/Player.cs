using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player instance;

    [Header("GridMovement Settings")]
    [SerializeField] Transform movePoint;
    [SerializeField] LayerMask obstacleLayer;
    [SerializeField] float obstacleCheckRadius;
    [SerializeField] float moveSpeed = 2;

    [Header("Interaction Settings")]
    [SerializeField] LayerMask interactableLayer;
    [SerializeField] float interactionCheckRadius;

    //Animation
    [SerializeField] Animator animator;
    bool isWalking = false;

    [Header("SFX Controlles")]
    [SerializeField] FootstepsController footstepsController;


    int playerScore = 0;
    public int PlayerScore { get { return playerScore; } set { playerScore = value; } }
    bool foundObjective;
    public bool FoundObjective { get { return foundObjective; } set { foundObjective = value; } }

    public bool CanControl { get => canControl; set => canControl = value; }
    public Animator Animator { get => animator; set => animator = value; }
    public float GliterAmount { get => gliterAmount; set => gliterAmount = value; }

    [SerializeField] float gliterAmount;
    float maxGliterAmount = 100;


    //Transform lastSavePoint;
    bool isAlive = true;
    bool canSpentGliter = true;

    // VFX
    [Header("VFX - Player")]
    public Vector3 playerWalkVFXOffset;

    private ParticleSystem _playerWalkVFX;
    private Image _glitterImg;

    // Control
    private bool canControl = true;


    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else
            instance = this;
    }

    void Start()
    {
        GliterAmount = maxGliterAmount;

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
            if (canControl)
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

    /*
    void Movement()
    {
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;

        rb.velocity = input * moveSpeed;
    }*/

    void GridMovement()
    {
        if (Vector3.Distance(transform.position, movePoint.position) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
            footstepsController.PlayTrigger();
            return;
        }

        float inputX = Input.GetAxisRaw("Horizontal");
        float inputZ = Input.GetAxisRaw("Vertical");

        if (inputX != 0)
        {
            inputZ = 0;
        }

        if (inputX != 0 || inputZ != 0)
        {
            Vector3 nextPosition = movePoint.position + new Vector3(inputX, 0, inputZ);

            if (Physics.OverlapSphere(nextPosition, obstacleCheckRadius, obstacleLayer).Length == 0)
            {
                movePoint.position = nextPosition;
                isWalking = true;

                // Rotação correta
                Vector3 direction = movePoint.position - transform.position;
                direction.y = 0;

                if (direction.sqrMagnitude > 0.01f)
                {
                    transform.rotation = Quaternion.LookRotation(direction);
                }

                if (canSpentGliter)
                {
                    GliterAmount -= 0.8f;
                    var emission = _playerWalkVFX.emission;
                    emission.rateOverDistance = new ParticleSystem.MinMaxCurve(GliterAmount * 0.5f);
                    UIImageFillManager.Instance.UpdateGlitterImage(GliterAmount / 100);
                    playerScore += 1;
                }
            }
            else
            {
                isWalking = false;
            }
        }
        else
        {
            isWalking = false;
        }

        Animator.SetBool("isWalking", isWalking);
    }


    #endregion

    public void TakeGliter()
    {
        UIImageFillManager.Instance.UpdateGlitterImage(GliterAmount / 100);
        GliterAmount += 50;
        if (GliterAmount > maxGliterAmount)
            GliterAmount = maxGliterAmount;
    }

    void DeathTrigger()
    {
        if (GliterAmount <= 0 && isAlive)
        {
            isAlive = false;
            StartCoroutine(Death());
        }

    }

    IEnumerator Death()
    {
        //Anima��o de morrer;
        Animator.SetTrigger("Death");

        yield return new WaitForSeconds(5f);

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

        foreach (var interactable in interactables)
        {
            IInteractable inter = interactable.GetComponent<IInteractable>();

            /*
            if (interactable.CompareTag("SavePoint"))
            {
                SavePosition(interactable.transform);
            }*/

            if (inter != null)
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
