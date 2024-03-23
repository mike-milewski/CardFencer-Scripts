using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleBackground : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed, timeToTransition, timeToResetPosition;

    [SerializeField]
    private Vector3 direction;

    [SerializeField]
    private MeshRenderer meshRenderer;

    [SerializeField]
    private List<Material> zoneMaterials = new List<Material>();

    private Material tempGO;

    private int zoneMaterialIndex;

    private float defaultTimeToTransition, defaultTimeToResetPosition, defaultPositionX;

    private bool canMove, isFading, movingRight;

    private IEnumerator Start()
    {
        defaultPositionX = transform.position.x;

        defaultTimeToTransition = timeToTransition;

        defaultTimeToResetPosition = timeToResetPosition;

        meshRenderer.materials = zoneMaterials.ToArray();

        zoneMaterialIndex = meshRenderer.materials.Length - 1;

        movingRight = true;

        RearrangeZoneMaterials();

        yield return new WaitForSeconds(0.5f);
        canMove = true;
    }

    private void Update()
    {
        if(canMove)
        {
            transform.position += direction * moveSpeed * Time.deltaTime;

            timeToTransition -= Time.deltaTime;
            if(timeToTransition <= 0)
            {
                if(!isFading)
                {
                    isFading = true;
                    StartCoroutine("FadeTexture");
                }
            }

            timeToResetPosition -= Time.deltaTime;
            if(timeToResetPosition <= 0)
            {
                if(movingRight)
                {
                    direction = new Vector3(1, 0, 0);
                    movingRight = false;
                }
                else
                {
                    direction = new Vector3(-1, 0, 0);
                    movingRight = true;
                }

                timeToResetPosition = defaultTimeToResetPosition;
            }
        }
    }

    private void RearrangeZoneMaterials()
    {
        for (int i = 0; i < zoneMaterials.Count; i++)
        {
            int rnd = Random.Range(0, zoneMaterials.Count);
            tempGO = zoneMaterials[rnd];
            zoneMaterials[rnd] = zoneMaterials[i];
            zoneMaterials[i] = tempGO;

            meshRenderer.materials = zoneMaterials.ToArray();
        }
    }

    private IEnumerator FadeTexture()
    {
        float zoneAlpha = meshRenderer.materials[zoneMaterialIndex].color.a;

        while (zoneAlpha > 0)
        {
            zoneAlpha -= Time.deltaTime / 4;
            meshRenderer.materials[zoneMaterialIndex].color = new Color(meshRenderer.materials[zoneMaterialIndex].color.a, meshRenderer.materials[zoneMaterialIndex].color.g, meshRenderer.materials[zoneMaterialIndex].color.r, zoneAlpha);

            yield return new WaitForFixedUpdate();
        }
        ReorderMaterials();

        timeToTransition = defaultTimeToTransition;

        isFading = false;

        if (zoneMaterialIndex < 0)
        {
            zoneMaterialIndex = zoneMaterials.Count - 1;
        }
    }

    private void ReorderMaterials()
    {
        zoneMaterials.Insert(0, zoneMaterials[zoneMaterialIndex]);

        zoneMaterials.RemoveAt(zoneMaterialIndex + 1);

        meshRenderer.materials = zoneMaterials.ToArray();
    }
}