using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5.0f; 
    public float rotationSpeed = 200.0f; 
    private float _horizontal;
    private float _vertical;

    [Header("VFX - Player")]
    public Vector3 playerWalkVFXOffset;

    private ParticleSystem _playerWalkVFX;
    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true; 

        _playerWalkVFX = VFXManager.Instance.GetPermanentVFXByType(VFXManager.VFXType.WALK,
            this.transform.position, playerWalkVFXOffset, this.transform);
    }

    void Update()
    {
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");

        if (!Mathf.Approximately(_horizontal, 0) && Mathf.Approximately(_vertical, 0))
        {
            DisableWalkVFX();
        }
        else if (!Mathf.Approximately(_vertical, 0))
        {
            EnableWalkVFX();
        }


        transform.Rotate(Vector3.up * _horizontal * rotationSpeed * Time.deltaTime);

        Vector3 moveDirection = transform.forward * _vertical * speed;
        _rb.velocity = new Vector3(moveDirection.x, _rb.velocity.y, moveDirection.z);
    }

    private void FixedUpdate()
    {

    }

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
