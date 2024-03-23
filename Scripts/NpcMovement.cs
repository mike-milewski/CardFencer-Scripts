using System.Collections;
using UnityEngine;

public class NpcMovement : MonoBehaviour
{
    [SerializeField]
    private Transform[] wayPoints;

    [SerializeField]
    private Animator npcAnimator;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private float moveSpeed, rotationSpeed;

    private int wayPointIndex;

    private bool canMove;

    private Vector3 direction;

    private Quaternion lookDirection;

    private Coroutine moveRoutine;

    private void Awake()
    {
        wayPointIndex = 0;
        canMove = true;
        npcAnimator.Play("WalkNpc");
    }

    private void Update()
    {
        if(canMove)
        {
            direction = new Vector3(wayPoints[wayPointIndex].transform.position.x - transform.position.x, 0, wayPoints[wayPointIndex].transform.position.z - transform.position.z).normalized;

            lookDirection = Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.Slerp(transform.rotation, lookDirection, rotationSpeed * Time.deltaTime).normalized;

            transform.position += direction * moveSpeed * Time.deltaTime;

            float distanceToDestination = Vector3.Distance(transform.position, wayPoints[wayPointIndex].transform.position);

            if(distanceToDestination < 2)
            {
                if(canMove)
                {
                    moveRoutine = null;
                    canMove = false;
                    moveRoutine = StartCoroutine(MoveCoroutine());
                }
            }
        }
    }

    private IEnumerator MoveCoroutine()
    {
        npcAnimator.Play("IdlePose");
        yield return new WaitForSeconds(5f);
        wayPointIndex++;
        if(wayPointIndex >= wayPoints.Length)
        {
            wayPointIndex = 0;
        }
        npcAnimator.Play("WalkNpc");
        canMove = true;
    }

    public void StopMovement()
    {
        canMove = false;
        npcAnimator.Play("IdlePose");
        if(moveRoutine != null)
        {
            StopCoroutine(moveRoutine);
            moveRoutine = null;
        }
    }

    public void ResumeMovement()
    {
        float distanceToDestination = Vector3.Distance(transform.position, wayPoints[wayPointIndex].transform.position);

        if (distanceToDestination < 2)
        {
            moveRoutine = null;
            moveRoutine = StartCoroutine(MoveCoroutine());
        }
        else
        {
            npcAnimator.Play("WalkNpc");
            canMove = true;
        }
    }

    public void PlayWalkAudio(AudioClip audioClip)
    {
        if (PlayerPrefs.HasKey("SoundEffects"))
        {
            audioSource.volume = PlayerPrefs.GetFloat("SoundEffects");
        }
        else
        {
            audioSource.volume = 1.0f;
        }

        audioSource.clip = audioClip;
        audioSource.Play();
    }
}