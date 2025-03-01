using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody rb;
    float moveSpeed = 2;
    [SerializeField]Transform movePoint;
    [SerializeField]LayerMask obstacleLayer;
    [SerializeField]float obstacleCheckRadius; 
    
    
    [SerializeField]float gliterAmount;
    float maxGliterAmount = 100;
    
    
    [SerializeField]Transform lastSavePoint;
    bool isAlive = true;
    
    
    void Start()
    {
        gliterAmount = maxGliterAmount;
        rb = GetComponent<Rigidbody>();

        movePoint.parent = null;
    }

    
    void Update()
    {
        if (isAlive)
        {
            //Movement();
            //GridMovement();
            //DeathTrigger();
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(movePoint.position, obstacleCheckRadius);
    }

    void Movement()
    {
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        rb.velocity = input * moveSpeed;
    }

    void GridMovement()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputZ = Input.GetAxisRaw("Vertical");

        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);


        if (Vector3.Distance(transform.position, movePoint.position ) < 0.5f)
        {
            if (inputX != 0)
            {
                movePoint.position += new Vector3(inputX, 0, 0);
                gliterAmount -= 1;
            }
            if (inputZ != 0)
            {
                movePoint.position += new Vector3(0, 0, inputZ);
                gliterAmount -= 1;
            }

            Collider[] obstacles = Physics.OverlapSphere(movePoint.position, obstacleCheckRadius, obstacleLayer);
            if (obstacles.Length > 0)
            {
                movePoint.position = transform.position;
            }
           
        }
        
    }

    void TakeGliter()
    {
        gliterAmount += 50;
        if (gliterAmount > maxGliterAmount)
            gliterAmount = maxGliterAmount;
    }

    void DeathTrigger()
    {
        if(gliterAmount <= 0 && isAlive)
        {
            isAlive = false;
            StartCoroutine(Death());
        }
            
    }

    IEnumerator Death()
    {
        //Animação de morrer;
        transform.Rotate(0, 0, 90);

        yield return new WaitForSeconds(2f);

        if (lastSavePoint != null)
            GameManager.instance.RestartGame(lastSavePoint, this);
        else
            Destroy(gameObject);

        yield return null;
    }

    void SavePosition(Transform position)
    {
        lastSavePoint = position;
        Debug.Log(lastSavePoint);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("GliterPot"))
        {
            TakeGliter();
            Destroy(collider.gameObject);
            
        }

        if (collider.CompareTag("SavePoint"))
        {
            SavePosition(collider.transform);
            //SavePoint fazer alguma alteração e bloquea-lo
            
        }
    }
}
