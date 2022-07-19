using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerControls : MonoBehaviour
{
    [Header("Player Config")]

    public GameObject playerModel;

    public float walkSpeed = 4.0f;
    public float runSpeed = 8.0f;
    public float speedOffset = 1.0f;
   
    [Tooltip("Player turn rate")]
    [Range(0.0f, 0.4f)]
    public float rotationSmoothRate = 0.1f;

    [Tooltip("Player rate of speed change")]
    public float speedChangeRate = 10.0f;

    public bool shouldRespawn;
    public bool headMode;

    // Animator animator;
    Animator animator;
    CharacterController cc;
    PlayerInputManager inputs;
    GameObject mainCamera;

    //player settings
    float speed;
    float camRotation = 0;
    float ySpeed;
    bool isRunning;

    float ccHeight;
    float ccRadius;

    public GameObject spawn;

    void Awake()
    {
        if (mainCamera == null)
            mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        inputs = GetComponent<PlayerInputManager>();
        cc = GetComponent<CharacterController>();

        ccHeight = cc.height;
        ccRadius = cc.radius;
    }

    void Update()
    {
        MovePlayer();
        RespawnPlayer();
    }

    void RespawnPlayer()
    {
        if (shouldRespawn)      
            gameObject.transform.position = spawn.transform.position;
    }

    void PlayerAnimation(bool isGrabbing, float playerSpeed)
    {
        animator.SetBool("grab", isGrabbing);

        animator.SetFloat("movement", playerSpeed);

        animator.SetBool("isRunning", playerSpeed == runSpeed);

    }

    void MovePlayer()
    {
        //check if the user is sprinting
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
            speed = targetSpeed;

        //blend the animation
       //animationBlend = Mathf.Lerp(animationBlend, targetSpeed, Time.deltaTime * speedChangeRate);

        //get the normal of the input direction
        Vector3 inputDir = new Vector3(inputs.move.x, 0.0f, inputs.move.y).normalized;

        camRotation = Mathf.Atan2(inputDir.x, inputDir.z) * Mathf.Rad2Deg + mainCamera.transform.eulerAngles.y;
        Vector3 targetDir = Quaternion.Euler(0.0f, camRotation, 0.0f) * Vector3.forward;
        Vector3 playerRotation = Quaternion.Euler(0.0f, camRotation, 0.0f) * Vector3.forward;

        //rotate the player
        if (inputs.move != Vector2.zero)               
            playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, 
                Quaternion.LookRotation(playerRotation), 0.15f);
        
        //apply gravity
        if (cc.isGrounded)      
            ySpeed = 0;
        else
            ySpeed = Physics.gravity.y;

        cc.Move(targetDir.normalized * (speed * Time.deltaTime) +
            new Vector3(0.0f, ySpeed, 0.0f) * Time.deltaTime);

        PlayerAnimation(inputs.interact, targetSpeed);
    }
    
    public void Eaten()
    {
        headMode = true;
        cc.height = 0.01f;
        cc.radius = 0.5f;
    }

    public void Heal()
    {
        headMode = false;
        cc.height = ccHeight;
        cc.radius = ccRadius;
    }
}
