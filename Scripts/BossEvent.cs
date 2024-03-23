using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BossEvent : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    [SerializeField]
    private BoxCollider boxCollider;

    [SerializeField]
    private Transform playerPosition;

    [SerializeField]
    private PlayerMovement playerMovement;

    [SerializeField]
    private EnemyFieldAI enemyFieldAi;

    [SerializeField]
    private PlayerUserInterfaceController playerUserInterfaceController;

    [SerializeField]
    private CameraFollow cameraFollow;

    [SerializeField]
    private Image screenFadeImage;

    [SerializeField]
    private Animator bossAnimator;

    [SerializeField]
    private PlayerFieldSwordAndShield playerFieldSwordAndShield;

    [SerializeField]
    private RuntimeAnimatorController withEquipmentController;

    [SerializeField]
    private Vector3 playerEventPosition, cameraEventPosition;

    [SerializeField]
    private bool endsDemo;

    private Coroutine fadeScreenCoroutine;

    private void FadeOutEvent()
    {
        fadeScreenCoroutine = null;

        if (fadeScreenCoroutine == null)
        {
            fadeScreenCoroutine = StartCoroutine(FadeOut());
        }
    }

    private void FadeInEvent()
    {
        fadeScreenCoroutine = null;

        if (fadeScreenCoroutine == null)
        {
            fadeScreenCoroutine = StartCoroutine(FadeIn());
        }
    }

    private void SetBossEvent()
    {
        playerMovement.PlayerAnimator.runtimeAnimatorController = withEquipmentController;

        playerPosition.position = playerEventPosition;

        playerPosition.rotation = Quaternion.Euler(0, 0, 0);

        cam.transform.position = cameraEventPosition;

        cam.transform.rotation = Quaternion.Euler(25.4f, -36.89f, 0);
    }

    private IEnumerator FadeOut()
    {
        float t = 0;

        float alpha = 0;

        screenFadeImage.gameObject.SetActive(true);

        while (t < 1.5f)
        {
            t += Time.deltaTime;
            alpha += Time.deltaTime;

            screenFadeImage.color = new Color(0, 0, 0, alpha);

            yield return null;
        }
        screenFadeImage.color = new Color(0, 0, 0, 1);

        SetBossEvent();

        yield return new WaitForSeconds(1);

        FadeInEvent();
    }

    private IEnumerator FadeIn()
    {
        float t = 0;

        float alpha = screenFadeImage.color.a;

        while (t < 1.5f)
        {
            t += Time.deltaTime;
            alpha -= Time.deltaTime;

            screenFadeImage.color = new Color(0, 0, 0, alpha);

            yield return null;
        }
        screenFadeImage.color = new Color(0, 0, 0, 0);

        screenFadeImage.gameObject.SetActive(false);

        yield return new WaitForSeconds(0.5f);

        bossAnimator.Play("BattleCry");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerMovement>())
        {
            playerMovement.CanToggleMap = false;
            playerMovement.enabled = false;
            playerFieldSwordAndShield.ResetEquipmentForBoss();
            playerFieldSwordAndShield.enabled = false;
            cameraFollow.enabled = false;
            playerUserInterfaceController.IdlePlayerInformation();
            playerUserInterfaceController.enabled = false;
            playerMovement.PlayerAnimator.Play("IdlePose");
            MenuController.instance.CanToggleMenu = false;
            GameManager.instance.EnemyObject = enemyFieldAi;

            if(endsDemo)
            {
                GameManager.instance.EndsGameDemo = true;
            }

            FadeOutEvent();
            boxCollider.enabled = false;
        }
    }
}