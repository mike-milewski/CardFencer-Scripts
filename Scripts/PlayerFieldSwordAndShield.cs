using UnityEngine;

public class PlayerFieldSwordAndShield : MonoBehaviour
{
    private InteractableObject movedObject = null;

    [SerializeField]
    private PlayerMenuInfo playerMenuInfo;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private PlayerMovement playerMovement;

    [SerializeField]
    private PlayerCollision playerCollision;

    [SerializeField]
    private RuntimeAnimatorController withEquipment, withoutEquipment;

    [SerializeField]
    private GameObject swordObject, shieldObject;

    [SerializeField]
    private BoxCollider noviceSwordCollider, veteranSwordCollider, eliteSwordCollider;

    [SerializeField]
    private ParticleSystem attackParticle;

    private bool isUsingSword, isUsingShield, isControllerPluggedOut;

    public InteractableObject MovedObject
    {
        get
        {
            return movedObject;
        }
        set
        {
            movedObject = value;
        }
    }

    public GameObject SwordObject
    {
        get
        {
            return swordObject;
        }
        set
        {
            swordObject = value;
        }
    }

    public GameObject ShieldObject
    {
        get
        {
            return shieldObject;
        }
        set
        {
            shieldObject = value;
        }
    }

    public bool IsUsingSword => isUsingSword;

    public bool IsUsingShield => isUsingShield;

    private void Update()
    {
        if(Time.timeScale > 0)
        {
            if(playerMovement.HasControllerInput)
            {
                if(InputManager.instance.ControllerName == "xbox")
                {
                    if (Input.GetButtonDown("XboxAttack") && !MenuController.instance.OpenedMenu)
                    {
                        if (!isUsingSword && !isUsingShield)
                            Sword();
                    }
                    else if (Input.GetButton("XboxGuard") && !MenuController.instance.OpenedMenu)
                    {
                        if (!isUsingShield && !isUsingSword)
                            Shield();
                    }

                    if (isUsingShield)
                    {
                        if (Input.GetButtonUp("XboxGuard"))
                        {
                            ResetEquipment();
                            if (movedObject != null)
                            {
                                movedObject.EnableKinematic();
                            }
                        }
                    }
                }
                else
                {
                    if (Input.GetButtonDown("Ps4Attack") && !MenuController.instance.OpenedMenu)
                    {
                        if (!isUsingSword && !isUsingShield)
                            Sword();
                    }
                    else if (Input.GetButton("Ps4Guard") && !MenuController.instance.OpenedMenu)
                    {
                        if (!isUsingShield && !isUsingSword)
                            Shield();
                    }

                    if (isUsingShield)
                    {
                        if (Input.GetButtonUp("Ps4Guard"))
                        {
                            ResetEquipment();
                            if (movedObject != null)
                            {
                                movedObject.EnableKinematic();
                            }
                        }
                    }
                }

                if(isControllerPluggedOut)
                {
                    ResetEquipment();
                    isControllerPluggedOut = false;
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.X) || Input.GetMouseButtonDown(0))
                {
                    if (!isUsingSword && !isUsingShield)
                        Sword();
                }
                else if (Input.GetKey(KeyCode.Z) || Input.GetMouseButton(1))
                {
                    if (!isUsingShield && !isUsingSword)
                        Shield();
                }

                if (isUsingShield)
                {
                    if (Input.GetKeyUp(KeyCode.Z) || Input.GetMouseButtonUp(1))
                    {
                        ResetEquipment();
                        if (movedObject != null)
                        {
                            movedObject.EnableKinematic();
                        }
                    }
                }

                if (!isControllerPluggedOut)
                {
                    ResetEquipment();
                    isControllerPluggedOut = true;
                } 
            }
        }
        else
        {
            if(!GameManager.instance.IsEnteringBattle)
            {
                ResetEquipment();
            }
            
            if (movedObject != null)
            {
                movedObject.EnableKinematic();
            }
        }
    }

    private void Sword()
    {
        isUsingSword = true;

        animator.runtimeAnimatorController = withEquipment;

        swordObject.SetActive(true);

        animator.Play("Attack");

        playerCollision.enabled = false;
        playerMovement.enabled = false;
    }

    private void Shield()
    {
        isUsingShield = true;

        animator.runtimeAnimatorController = withEquipment;

        shieldObject.SetActive(true);

        animator.Play("Guard");

        playerMovement.enabled = true;
    }

    public void ResetEquipment()
    {
        isUsingSword = false;
        isUsingShield = false;

        swordObject.SetActive(false);
        shieldObject.SetActive(false);

        animator.runtimeAnimatorController = withoutEquipment;

        attackParticle.gameObject.SetActive(false);

        playerCollision.enabled = true;

        if(!GameManager.instance.IsBossFight)
            playerMovement.enabled = true;

        noviceSwordCollider.enabled = false;
        veteranSwordCollider.enabled = false;
        eliteSwordCollider.enabled = false;

        animator.Play("IdlePose");
    }

    public void ResetEquipmentForBoss()
    {
        isUsingSword = false;
        isUsingShield = false;

        swordObject.SetActive(false);
        shieldObject.SetActive(false);

        attackParticle.gameObject.SetActive(false);

        animator.runtimeAnimatorController = withoutEquipment;
    }

    public bool ToggleSword()
    {
        BoxCollider weaponCollider = null;

        if(noviceSwordCollider.enabled)
        {
            noviceSwordCollider.enabled = false;

            weaponCollider = noviceSwordCollider;
        }
        else
        {
            noviceSwordCollider.enabled = true;

            weaponCollider = noviceSwordCollider;
        }

        if (veteranSwordCollider.enabled)
        {
            veteranSwordCollider.enabled = false;

            weaponCollider = veteranSwordCollider;
        }
        else
        {
            veteranSwordCollider.enabled = true;

            weaponCollider = veteranSwordCollider;
        }

        if (eliteSwordCollider.enabled)
        {
            eliteSwordCollider.enabled = false;

            weaponCollider = eliteSwordCollider;
        }
        else
        {
            eliteSwordCollider.enabled = true;

            weaponCollider = eliteSwordCollider;
        }

        return weaponCollider.enabled;
    }

    public void PlayAttackParticle()
    {
        attackParticle.gameObject.SetActive(true);

        attackParticle.Play();
    }
}