using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerControls : MonoBehaviour
{
    [Header("Player Config")]
    public float walkSpeed = 2.0f;
    public float runSpeed = 6.0f;
    public float speedOffset = 1.0f;
   
    [Tooltip("Player turn rate")]
    [Range(0.0f, 0.4f)]
    public float rotationSmoothRate = 0.1f;

    [Tooltip("Player rate of speed change")]
    public float speedChangeRate = 10.0f;

    [Header("Player Grounded")]
    [Tooltip("Check if the Player is grounded")]
    public bool grounded = true;

    [Tooltip("Rough grounded offset, useful for complicated terrains")]
    [Range(-1, 1)]
    public float groundedOffSet = 0.0f;

    [Tooltip("Radius of the above check")]
    public float groundedRadius = 0.3f;

    [Tooltip("Layers approved to be the 'grounded'")]
    public LayerMask groundLayers;

    // Animator animator;
    Animator animator;
    CharacterController cc;
    PlayerInputManager inputs;
    GameObject mainCamera;

    //player settings
    float speed;
    float rotation = 0;
    float forwardVelocity;
    float animationBlend;
    //=----------------------=

    bool hasAnimator;
    bool rotateOnMove = true;

    Vector3 spherePos;

    void Awake()
    {
        if (mainCamera == null)
            mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    void Start()
    {
        hasAnimator = TryGetComponent(out animator);
        inputs = GetComponent<PlayerInputManager>();
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        Grounded();
        MovePlayer();
    }

    void Grounded()
    {
        spherePos = new Vector3(transform.position.x, transform.position.y - groundedOffSet, transform.position.z);
        grounded = Physics.CheckSphere(spherePos, groundedRadius, groundLayers,
           QueryTriggerInteraction.Ignore);

        if (hasAnimator)
        {
            //animator.SetBool("isGrounded", grounded);
        }
    }

    void MovePlayer()
    {
        //sprint
        float targetSpeed = inputs.run ? runSpeed : walkSpeed;

        //check if player stops pressing a key
        if (inputs.move == Vector2.zero)
            targetSpeed = 0.0f;

        //grab the players current speed
        float currentHorizontalSpeed = new Vector3(cc.velocity.x, 0.0f, cc.velocity.z).magnitude;

        float inputMag = inputs.move.magnitude;

        //we adjust the target speed
        if (currentHorizontalSpeed < targetSpeed - speedOffset ||
            currentHorizontalSpeed > targetSpeed + speedOffset)
        {
            speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMag, Time.deltaTime * speedChangeRate);
            speed = Mathf.Round(speed * 1000) / 1000f; // This will keep it at 3 decimal places.
        }
        else
        {
            speed = targetSpeed;
        }

        //blend the animation
        animationBlend = Mathf.Lerp(animationBlend, targetSpeed, Time.deltaTime * speedChangeRate);

        //get the normal of the input direction
        Vector3 inputDir = new Vector3(inputs.move.x, 0.0f, inputs.move.y).normalized;

        //rotate the player
        if (inputs.move != Vector2.zero)
        {
            rotation = Mathf.Atan2(inputDir.x, inputDir.z) * Mathf.Rad2Deg 
                + mainCamera.transform.eulerAngles.y;           
        }

        Vector3 targetDir = Quaternion.Euler(0.0f, rotation, 0.0f) * Vector3.forward;

        cc.Move(targetDir.normalized * (speed * Time.deltaTime) +
            new Vector3(0.0f, forwardVelocity, 0.0f) * Time.deltaTime);

        //update animator
        if (hasAnimator)
        {
           // Vector2 move = new Vector2(forwardVelocity, currentHorizontalSpeed).normalized;
           // animator.SetFloat("Speed", animationBlend);
           // animator.SetFloat("SpeedMultiplier", inputMag);
        }


        //=============
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(spherePos, 0.5f);
    }
}
