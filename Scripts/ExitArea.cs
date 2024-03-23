using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class ExitArea : MonoBehaviour
{
    public static ExitArea instance = null;

    [SerializeField]
    private ExitAreaPanel exitAreaPanel;

    [SerializeField]
    private CameraFollow cameraFollow;

    [SerializeField]
    private PlayerFieldSwordAndShield playerField;

    [SerializeField]
    private Transform player;

    [SerializeField]
    private GameObject verticalLayoutObject;

    [SerializeField]
    private Animator exitAreaAnimator, exitYesAnimator, exitNoAnimator;

    [SerializeField]
    private SceneNameToLoad sceneNameToLoad;

    [SerializeField]
    private TextMeshProUGUI exitAreaText, warningText;

    [SerializeField]
    private Transform targetDestinationToMoveTowards;

    [SerializeField]
    private bool completesStage, unlocksSecretStage;

    [SerializeField]
    private bool shouldMovePlayer;

    private float distanceBetweenPlayerAndColldider;

    private Scene scene;

    public Animator ExitAreaAnimator => exitAreaAnimator;

    public Animator ExitNoAnimator => exitNoAnimator;

    public GameObject VerticlLayoutObject => verticalLayoutObject;

    public TextMeshProUGUI WarningText
    {
        get
        {
            return warningText;
        }
        set
        {
            warningText = value;
        }
    }

    public bool ShouldMovePlayer
    {
        get
        {
            return shouldMovePlayer;
        }
        set
        {
            shouldMovePlayer = value;
        }
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        scene = SceneManager.GetActiveScene();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerMovement>())
        {
            ExitAreaPanel();

            if (!completesStage)
            {
                warningText.text = "All progress made here will be lost!";
            }
            else
            {
                warningText.text = "";
            }

            shouldMovePlayer = true;

            playerField.enabled = false;

            AudioManager.instance.PlaySoundEffect(AudioManager.instance.OpenMenuAudio);

            other.GetComponent<PlayerFieldSwordAndShield>().ResetEquipment();

            other.GetComponent<PlayerMovement>().PlayerAnimator.Play("IdlePose");

            other.GetComponent<PlayerMovement>().enabled = false;

            exitAreaPanel._ExitArea = this;
        }
    }

    public void ExitAreaPanel()
    {
        Scene scene = SceneManager.GetActiveScene();

        verticalLayoutObject.SetActive(true);

        exitAreaPanel.ExitAreaPanelOpen = true;

        if(scene.name.Contains("Town"))
        {
            exitAreaText.text = "Exit Town?";

            warningText.gameObject.SetActive(false);
        }
        else
        {
            exitAreaText.text = "Exit this Area?";

            warningText.gameObject.SetActive(true);
        }

        if(scene.name.Contains("Forest"))
        {
            sceneNameToLoad.SceneToLoad = "ForestField";
        }
        else if(scene.name.Contains("Wood"))
        {
            sceneNameToLoad.SceneToLoad = "ForestField";
        }
        else if(scene.name.Contains("Desert"))
        {
            sceneNameToLoad.SceneToLoad = "DesertField";
        }
        else if(scene.name.Contains("Arctic"))
        {
            sceneNameToLoad.SceneToLoad = "ArcticField";
        }
        else if(scene.name.Contains("Graveyard"))
        {
            sceneNameToLoad.SceneToLoad = "GraveyardField";
        }
        else if(scene.name.Contains("Castle"))
        {
            sceneNameToLoad.SceneToLoad = "CastleField";
        }

        InputManager.instance.ForceCursorOn = true;

        MenuController.instance.CanToggleMenu = false;

        InputManager.instance.FirstSelectedObject = exitNoAnimator.gameObject;
        InputManager.instance.SetSelectedObject(null);

        cameraFollow.enabled = false;

        exitAreaAnimator.Play("Open");

        exitYesAnimator.Play("Normal");
        exitNoAnimator.Play("Normal");
    }

    public void CheckIfExitCompletesStage()
    {
        if(completesStage)
        {
            NodeManager.instance.CompletedStage = true;
        }
        if(unlocksSecretStage)
        {
            NodeManager.instance.UnlockedSecretStage = true;
        }
    }

    public void MovePlayerAwayFromCollider()
    {
        StartCoroutine("MoveAway");

        if(MenuController.instance.OpenedMenu)
        {
            InputManager.instance.SetSelectedObject(MenuController.instance.FirstSelectedObject);

            MenuController.instance.CanToggleMenu = true;
        }
        else
        {
            InputManager.instance.SetSelectedObject(null);
        }
    }

    private IEnumerator MoveAway()
    {
        if (shouldMovePlayer)
        {
            InputManager.instance.FirstSelectedObject = null;

            exitAreaPanel._ExitArea = null;

            sceneNameToLoad.SceneToLoad = "ForestField";

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            cameraFollow.enabled = true;

            Vector3 distance = new Vector3(targetDestinationToMoveTowards.position.x - player.position.x, 0, targetDestinationToMoveTowards.position.z - player.position.z).normalized;

            Animator playerAnimator = player.GetComponent<Animator>();

            distanceBetweenPlayerAndColldider = Vector3.Distance(player.transform.position, targetDestinationToMoveTowards.position);

            while (distanceBetweenPlayerAndColldider > 1)
            {
                playerAnimator.Play("Run");

                distanceBetweenPlayerAndColldider = Vector3.Distance(player.transform.position, targetDestinationToMoveTowards.position);

                player.transform.rotation = Quaternion.Slerp(player.transform.rotation, targetDestinationToMoveTowards.rotation, 10 * Time.deltaTime);

                player.transform.position += distance * 4 * Time.deltaTime;

                yield return new WaitForFixedUpdate();
            }
            player.GetComponent<PlayerMovement>().enabled = true;
            
            if(!scene.name.Contains("Town"))
               playerField.enabled = true;

            playerAnimator.Play("IdlePose");

            verticalLayoutObject.SetActive(false);
        }
    }
}