using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public enum InteractableSymbol { NoviceSword, VeteranSword, EliteSword, NoviceShield, VeteranShield, EliteShield };

public class InteractableObject : MonoBehaviour
{
    [SerializeField]
    private InteractableSymbol interactableSymbol;

    [SerializeField]
    private PlayerMenuInfo playerMenuInfo;

    [SerializeField]
    PlayerFieldSwordAndShield playerField;

    [SerializeField]
    private Rigidbody rigidBody;

    [SerializeField]
    private BoxCollider boxCollider;

    [SerializeField]
    private MeshRenderer ringFrontMesh, ringBackMesh;

    [SerializeField]
    private Sprite noviceSwordSprite, veteranSwordSprite, eliteSwordSprite, noviceShieldSprite, veteranShieldSprite, eliteShieldSprite;

    [SerializeField]
    private Image frontInteractableSymbolImage, backInteractableSymbolImage;

    [SerializeField]
    private Material interactableRing, unInteractableRing;

    [SerializeField]
    private GameObject symbolHolder;

    [SerializeField]
    private ParticleSystem movedObjectParticle, destroyObjectParticle;

    [SerializeField]
    private int interactableIndex;

    [SerializeField]
    private float boxColliderShrinkSizeX, boxColliderShrinkSizeZ;

    [SerializeField]
    private bool isAPuzzlePiece, isADungeonLever, shouldntShrink;

    private bool canMove, hasSword, isBreakable;

    private float defaultBoxColliderSizeX, defaultBoxColliderSizeZ, defaultBoxColliderCenterZ;

    private Vector3 defaultPosition;

    public Vector3 DefaultPosition => defaultPosition;

    private void Start()
    {
        SetUpInteractableSymbol();

        defaultPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        SetDefaultBoxColliderSize();
        SetDefaultBoxColliderCenter();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!isAPuzzlePiece)
        {
            if (!isBreakable)
            {
                if (canMove)
                {
                    if (other.tag == "Shield")
                    {
                        rigidBody.isKinematic = false;

                        playerField.MovedObject = this;

                        if(!shouldntShrink)
                           ShrinkColliderSize();
                    }
                }
            }
            else
            {
                if (other.tag == "Sword")
                {
                    Instantiate(destroyObjectParticle, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), Quaternion.identity);
                    AudioManager.instance.PlaySoundEffect(AudioManager.instance.BasicSwordAudio);

                    if (hasSword)
                        Destroy(gameObject);
                }
            }
        }
        else
        {
            if (other.tag == "Sword")
            {
                Instantiate(destroyObjectParticle, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), Quaternion.identity);
                AudioManager.instance.PlaySoundEffect(AudioManager.instance.BasicSwordAudio);

                DisableSymbols();

                if(gameObject.GetComponent<LeverPuzzle>())
                {
                    gameObject.GetComponent<LeverPuzzle>().TriggerLever();
                }
                else if(gameObject.GetComponent<MultiLeverPuzzle>())
                {
                    gameObject.GetComponent<MultiLeverPuzzle>().PlayAnimation();
                }
                else if(gameObject.GetComponent<GatePuzzle>())
                {
                    gameObject.GetComponent<GatePuzzle>().OpenGate();

                    if(isADungeonLever)
                    {
                        GetComponent<Animator>().Play("DungeonLever");
                    }
                }
                else if(gameObject.GetComponent<MultiWispPuzzle>())
                {
                    gameObject.GetComponent<MultiWispPuzzle>().TriggerPuzzle();
                }

                gameObject.GetComponent<BoxCollider>().enabled = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(!isBreakable)
        {
            if(canMove)
            {
                if (other.tag == "Shield")
                {
                    rigidBody.isKinematic = true;

                    playerField.MovedObject = null;

                    ResetColliderSize();
                }
            }
        }
    }

    private void SetDefaultBoxColliderSize()
    {
        defaultBoxColliderSizeX = boxCollider.size.x;
        defaultBoxColliderSizeZ = boxCollider.size.z;
    }

    private void SetDefaultBoxColliderCenter()
    {
        defaultBoxColliderCenterZ = boxCollider.center.z;
    }

    private void ShrinkColliderSize()
    {
        boxCollider.size = new Vector3(boxColliderShrinkSizeX, boxCollider.size.y, boxCollider.size.z);
    }

    private void ResetColliderSize()
    {
        boxCollider.size = new Vector3(defaultBoxColliderSizeX, boxCollider.size.y, defaultBoxColliderSizeZ);
        boxCollider.center = new Vector3(boxCollider.center.x, boxCollider.center.y, defaultBoxColliderCenterZ);
    }

    private void SetUpInteractableSymbol()
    {
        switch(interactableSymbol)
        {
            case InteractableSymbol.NoviceSword:
                frontInteractableSymbolImage.sprite = noviceSwordSprite;
                backInteractableSymbolImage.sprite = noviceSwordSprite;
                isBreakable = true;
                break;
            case InteractableSymbol.VeteranSword:
                frontInteractableSymbolImage.sprite = veteranSwordSprite;
                backInteractableSymbolImage.sprite = veteranSwordSprite;
                isBreakable = true;
                break;
            case InteractableSymbol.EliteSword:
                frontInteractableSymbolImage.sprite = eliteSwordSprite;
                backInteractableSymbolImage.sprite = eliteSwordSprite;
                isBreakable = true;
                break;
            case InteractableSymbol.NoviceShield:
                frontInteractableSymbolImage.sprite = noviceShieldSprite;
                backInteractableSymbolImage.sprite = noviceShieldSprite;
                break;
            case InteractableSymbol.VeteranShield:
                frontInteractableSymbolImage.sprite = veteranShieldSprite;
                backInteractableSymbolImage.sprite = veteranShieldSprite;
                break;
            case InteractableSymbol.EliteShield:
                frontInteractableSymbolImage.sprite = eliteShieldSprite;
                backInteractableSymbolImage.sprite = eliteShieldSprite;
                break;
        }

        if(isBreakable)
        {
            if(playerMenuInfo.weaponIndex >= interactableIndex)
            {
                hasSword = true;

                ringFrontMesh.material = interactableRing;
                ringBackMesh.material = interactableRing;
            }
            else
            {
                ringFrontMesh.material = unInteractableRing;
                ringBackMesh.material = unInteractableRing;
            }
        }
        else
        {
            if (playerMenuInfo.shieldIndex >= interactableIndex)
            {
                canMove = true;

                ringFrontMesh.material = interactableRing;
                ringBackMesh.material = interactableRing;
            }
            else
            {
                ringFrontMesh.material = unInteractableRing;
                ringBackMesh.material = unInteractableRing;
            }
        }
    }

    public void EnableKinematic()
    {
        rigidBody.isKinematic = true;

        playerField.MovedObject = null;
    }

    public void DisableRigidbody()
    {
        canMove = false;

        EnableKinematic();

        if(!shouldntShrink)
        {
            ShrinkColliderSize();

            boxCollider.size = new Vector3(boxColliderShrinkSizeX, boxCollider.size.y, boxColliderShrinkSizeZ);
            boxCollider.center = new Vector3(boxCollider.center.x, boxCollider.center.y, 0);
        }

        movedObjectParticle.gameObject.SetActive(true);
        if (PlayerPrefs.HasKey("SoundEffects"))
        {
            movedObjectParticle.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("SoundEffects");
            movedObjectParticle.GetComponent<AudioSource>().Play();
        }
        else
        {
            movedObjectParticle.GetComponent<AudioSource>().Play();
        }

        if(GetComponent<GatePuzzle>())
        {
            GetComponent<GatePuzzle>().OpenGate();
        }

        DisableSymbols();

        StartCoroutine("WaitToDisableParticle");
    }

    public void DisableSymbols()
    {
        symbolHolder.SetActive(false);
    }

    private IEnumerator WaitToDisableParticle()
    {
        yield return new WaitForSeconds(1.2f);
        movedObjectParticle.gameObject.SetActive(false);
    }
}