using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerControls : MonoBehaviour
{
    [Header("Player Parts")]
    public GameObject playerModel;
    public GameObject playerHead2;
    public GameObject playerArm;

    [Space(10)]

    public float heightDeath = -5.0f;
    public float walkSpeed = 4.0f;
    public float runSpeed = 8.0f;
    public float speedOffset = 2.0f;

    [Tooltip("Player rate of speed change")]
    public float speedChangeRate = 10.0f;

    public bool shouldRespawn;
    public bool headMode;
    public bool canPlayerMove = true;
    

    // Animator animator;
    Animator animator;
    CharacterController cc;
    PlayerInputManager inputs;
    GameObject mainCamera;

    //player settings
    float speed;
    float camRotation = 0;
    float ySpeed;

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

        //set the player model to have the correct items turned on
        if (playerArm != null)
            playerArm.SetActive(true);

        if (playerHead2 != null)
            playerHead2.SetActive(false);

        if (playerModel != null)
            playerModel.SetActive(true);

        //HeadRollTest();
    }

    void Update()
    {
        if (canPlayerMove)
        {
            if (!headMode)          
                MovePlayer();
            else           
                MoveHead();                            
        }           

        //respawn player and height check
        if (shouldRespawn || gameObject.transform.position.y < heightDeath)
            RespawnPlayer();
    }

    void HeadRollTest()
    {
        headMode = true;

        if (playerArm != null)
            playerArm.SetActive(true);

        if (playerHead2 != null)
            playerHead2.SetActive(true);

        if (playerModel != null)
            playerModel.SetActive(false);      
    }

    void RespawnPlayer()
    {
        //add a cut scene?
        //fade the screen black?
        //=================
        gameObject.transform.position = spawn.transform.position;
    }

    void PlayerAnimation(bool isGrabbing, float playerSpeed)
    {
        animator.SetBool("grab", isGrabbing);
        animator.SetFloat("movement", playerSpeed);
        animator.SetBool("isRunning", playerSpeed == runSpeed);
    }

    void MoveHead()
    {
        // get the normal of the input direction
        Vector3 inputDir = new Vector3(inputs.move.x, 0.0f, inputs.move.y).normalized;
        camRotation = Mathf.Atan2(inputDir.x, inputDir.z) * Mathf.Rad2Deg + mainCamera.transform.eulerAngles.y;

        //check if the user is sprinting
        speed = inputs.run ? runSpeed : walkSpeed;
        //check if player stops pressing a key
        if (inputs.move == Vector2.zero)
            speed = 0.0f;

        Vector3 targetDir = Quaternion.Euler(0.0f, camRotation, 0.0f) * Vector3.forward;
        Vector3 playerRotation = Quaternion.Euler(0.0f, camRotation, 0.0f) * Vector3.forward;

        //rotate the player when key pressed
        if (inputs.move != Vector2.zero)      
            playerHead2.transform.rotation = Quaternion.Slerp(playerHead2.transform.rotation,
               Quaternion.LookRotation(playerRotation), 0.15f);
            
        //apply gravity
        if (cc.isGrounded)
            ySpeed = 0;
        else
            ySpeed = Physics.gravity.y;

        cc.Move(targetDir.normalized * (speed * Time.deltaTime) +
            new Vector3(0.0f, ySpeed, 0.0f) * Time.deltaTime);
    }

    void MovePlayer()
    {
        //check if the user is sprinting
        float targetSpeed = inputs.run ? runSpeed : walkSpeed;

        //check if player stops pressing a key
        if (inputs.move == Vector2.zero)
            targetSpeed = 0.0f;

        //grab the players current speed
        float currentVel = new Vector3(cc.velocity.x, 0.0f, cc.velocity.z).magnitude;

        //we adjust the target speed
        if (currentVel < targetSpeed - speedOffset || currentVel > targetSpeed + speedOffset)
        {
            speed = Mathf.Lerp(currentVel, targetSpeed * inputs.move.magnitude, Time.deltaTime * speedChangeRate + 0.1f);
            speed = Mathf.Round(speed * 1000) / 1000f; // This will keep it at 3 decimal places.
        }
        else
        {
            speed = targetSpeed;
        }

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

    public void BodyEaten()
    {
        headMode = true;
        cc.height = 0.01f;
        cc.radius = 0.1f;

        if (playerArm != null)
            playerArm.SetActive(false);

        if (playerHead2 != null)
            playerHead2.SetActive(true);

        if (playerModel != null)
            playerModel.SetActive(false);
    }

    public void ArmEaten()
    {
        if (playerArm != null)
            playerArm.SetActive(false);
    }

    public void Heal()
    {
        headMode = false;
        cc.height = ccHeight;
        cc.radius = ccRadius;

        if (playerArm != null)
            playerArm.SetActive(true);

        if (playerHead2 != null)
            playerHead2.SetActive(false);

        if (playerModel != null)
            playerModel.SetActive(false);
    }
}
