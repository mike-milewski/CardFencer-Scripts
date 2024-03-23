using Steamworks;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private MainCharacterStats mainCharacterStats;

    [SerializeField]
    private Camera cam, mapCam;

    [SerializeField]
    private PlayerFieldSwordAndShield playerField;

    [SerializeField]
    private Transform rightFootDirtPosition, leftFootDirtPosition;

    [SerializeField]
    private Animator playerAnimator;

    [SerializeField]
    private Rigidbody rigidBody;

    [SerializeField]
    private ParticleSystem rightFootDirt, leftFootDirt;

    private Vector3 direction = Vector3.zero;

    private Vector3 movement;

    private string[] controllerNames;

    [SerializeField]
    private float moveSpeed, lookRotation;

    [SerializeField]
    private bool followCameraDirection, isSprinting, canToggleMap;

    private bool hasControllerInput, isMapOn;

    public Animator PlayerAnimator => playerAnimator;

    public bool HasControllerInput => hasControllerInput;

    public bool CanToggleMap
    {
        get => canToggleMap;
        set => canToggleMap = value;
    }

    private void Start()
    {
        moveSpeed = mainCharacterStats.moveSpeed;

        controllerNames = Input.GetJoystickNames();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Update()
    {
        if(canToggleMap)
        {
            if (hasControllerInput)
            {
                if (InputManager.instance.ControllerPluggedIn)
                {
                    if (InputManager.instance.ControllerName == "xbox")
                    {
                        if (Input.GetButtonDown("XboxMap"))
                        {
                            ToggleMap();
                        }
                    }
                    else
                    {
                        if (Input.GetButtonDown("PlayStationMap"))
                        {
                            ToggleMap();
                        }
                    }
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.M))
                {
                    ToggleMap();
                }
            }
        }
    }

    public void ToggleMap()
    {
        if(mapCam != null)
        {
            if (!isMapOn)
            {
                mapCam.rect = new Rect(mapCam.rect.x, mapCam.rect.y, 0.15f, 0.2f);
                isMapOn = true;
            }
            else
            {
                mapCam.rect = new Rect(mapCam.rect.x, mapCam.rect.y, 0, 0);
                isMapOn = false;
            }
        }
    }

    private void Movement()
    {
        controllerNames = Input.GetJoystickNames();

        if(controllerNames.Length > 0)
        {
            for (int i = 0; i < controllerNames.Length; i++)
            {
                if (!string.IsNullOrEmpty(controllerNames[i]))
                {
                    if (InputManager.instance.ControllerName == "xbox")
                    {
                        direction.x = Input.GetAxis("XboxHorizontal");
                        direction.z = Input.GetAxis("XboxVertical");
                    }
                    else
                    {
                        direction.x = Input.GetAxis("Ps4Horizontal");
                        direction.z = Input.GetAxis("Ps4Vertical");
                    }

                    hasControllerInput = true;
                }
                else
                {
                    direction.x = Input.GetAxis("KeyBoardHorizontal");
                    direction.z = Input.GetAxis("KeyBoardVertical");

                    hasControllerInput = false;
                }
            }
        }
        else
        {
            direction.x = Input.GetAxis("KeyBoardHorizontal");
            direction.z = Input.GetAxis("KeyBoardVertical");

            hasControllerInput = false;
        }

        movement = new Vector3(direction.x, 0, direction.z);
        movement = cam.transform.TransformDirection(movement);
        movement.y = 0.0f;

        if(movement != Vector3.zero)
        {
            Quaternion Look = Quaternion.LookRotation(movement);
            Quaternion LookDir = Look;

            Quaternion characterRotation = Quaternion.Slerp(this.transform.rotation, LookDir, lookRotation * Time.deltaTime);
            rightFootDirt.transform.rotation = Quaternion.Slerp(rightFootDirt.transform.rotation, LookDir, lookRotation * Time.deltaTime);
            leftFootDirt.transform.rotation = Quaternion.Slerp(leftFootDirt.transform.rotation, LookDir, lookRotation * Time.deltaTime);

            transform.rotation = characterRotation;
        }

        if (movement.sqrMagnitude > 1)
            movement.Normalize();

        rigidBody.velocity = movement * moveSpeed;

        if (direction.x > 0 || direction.z > 0 || direction.x < 0 || direction.z < 0)
        {
            if(!playerField.IsUsingShield)
            {
                if(hasControllerInput)
                {
                    if(InputManager.instance.ControllerName == "xbox")
                    {
                        if(SteamUtils.IsSteamRunningOnSteamDeck())
                        {
                            if (Input.GetAxisRaw("XboxOneSprint") == 1)
                            {
                                isSprinting = true;
                                playerAnimator.Play("Sprint");
                                moveSpeed = 20;
                            }
                            else
                            {
                                isSprinting = false;
                                playerAnimator.Play("Run");
                                moveSpeed = mainCharacterStats.moveSpeed;
                            }
                        }
                        else
                        {
                            if (Input.GetAxisRaw("XboxSprint") == 1 || Input.GetAxisRaw("XboxOneSprint") == 1)
                            {
                                isSprinting = true;
                                playerAnimator.Play("Sprint");
                                moveSpeed = 20;
                            }
                            else
                            {
                                isSprinting = false;
                                playerAnimator.Play("Run");
                                moveSpeed = mainCharacterStats.moveSpeed;
                            }
                        }
                    }
                    else
                    {
                        if (Input.GetButton("Ps4Sprint"))
                        {
                            isSprinting = true;
                            playerAnimator.Play("Sprint");
                            moveSpeed = 20;
                        }
                        else
                        {
                            isSprinting = false;
                            playerAnimator.Play("Run");
                            moveSpeed = mainCharacterStats.moveSpeed;
                        }
                    }
                }
                else
                {
                    if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                    {
                        isSprinting = true;
                        playerAnimator.Play("Sprint");
                        moveSpeed = 20;
                    }
                    else
                    {
                        isSprinting = false;
                        playerAnimator.Play("Run");
                        moveSpeed = mainCharacterStats.moveSpeed;
                    }
                }
                
            }
            else
            {
                playerAnimator.Play("GuardWalk");
                moveSpeed = 10;
            }
        }
        else
        {
            if(!playerField.IsUsingShield)
            {
                playerAnimator.Play("IdlePose");
            }
            else
            {
                playerAnimator.Play("Guard");
            }
            direction = Vector3.zero;
        }
    }

    public void PlayRightFootDirt()
    {
        if(isSprinting)
        {
            rightFootDirt.transform.position = new Vector3(rightFootDirtPosition.position.x, rightFootDirtPosition.position.y, rightFootDirtPosition.position.z);
            rightFootDirt.Play();
        } 
    }

    public void PlayLeftFootDirt()
    {
        if (isSprinting)
        {
            leftFootDirt.transform.position = new Vector3(leftFootDirtPosition.position.x, leftFootDirtPosition.position.y, leftFootDirtPosition.position.z);
            leftFootDirt.Play();
        }
    }
}