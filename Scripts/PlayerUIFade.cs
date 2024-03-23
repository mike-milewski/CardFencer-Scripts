using System.Collections;
using UnityEngine;

public class PlayerUIFade : MonoBehaviour
{
    [SerializeField]
    private SkinnedMeshRenderer skinnedMeshRendererHead, skinnedMeshRendererNoviceBody, skinnedMeshRendererVeteranBody, skinnedMeshRendererEliteBody;

    [SerializeField]
    private Material opaqueHead, opaqueNoviceBody, opaqueVeteranBody, opaqueEliteBody, alphaHead, alphaNoviceBody, alphaVeteranBody, alphaEliteBody;

    private Coroutine fadeOutRoutine, fadeInRoutine;

    public void StartFadeOutCoroutine()
    {
        if (fadeInRoutine != null)
        {
            StopCoroutine("FadeInPlayer");
        }

        fadeOutRoutine = StartCoroutine("FadeOutPlayer");
    }

    public void StartFadeInCoroutine()
    {
        if (fadeOutRoutine != null)
        {
            StopCoroutine("FadeOutPlayer");
        }

        fadeInRoutine = StartCoroutine("FadeInPlayer");
    }

    private IEnumerator FadeOutPlayer()
    {
        skinnedMeshRendererHead.material = alphaHead;
        skinnedMeshRendererNoviceBody.material = alphaNoviceBody;
        skinnedMeshRendererVeteranBody.material = alphaVeteranBody;
        skinnedMeshRendererEliteBody.material = alphaEliteBody;

        float headAlpha = alphaHead.color.a;
        float bodyNoviceAlpha = alphaNoviceBody.color.a;
        float bodyVeteranAlpha = alphaVeteranBody.color.a;
        float bodyEliteAlpha = alphaEliteBody.color.a;

        while (headAlpha > 0 && bodyNoviceAlpha > 0 && bodyVeteranAlpha > 0 && bodyEliteAlpha > 0)
        {
            headAlpha -= 8 * Time.unscaledDeltaTime;
            alphaHead.color = new Color(1, 1, 1, headAlpha);

            bodyNoviceAlpha -= 8 * Time.unscaledDeltaTime;
            alphaNoviceBody.color = new Color(1, 1, 1, bodyNoviceAlpha);

            bodyVeteranAlpha -= 8 * Time.unscaledDeltaTime;
            alphaVeteranBody.color = new Color(1, 1, 1, bodyVeteranAlpha);

            yield return new WaitForSecondsRealtime(Time.unscaledDeltaTime);
        }
        headAlpha = 0;
        bodyNoviceAlpha = 0;
        bodyVeteranAlpha = 0;
        bodyEliteAlpha = 0;

        skinnedMeshRendererHead.material = opaqueHead;
        skinnedMeshRendererNoviceBody.material = opaqueNoviceBody;
        skinnedMeshRendererVeteranBody.material = opaqueVeteranBody;
        skinnedMeshRendererEliteBody.material = opaqueEliteBody;

        gameObject.SetActive(false);
    }

    private IEnumerator FadeInPlayer()
    {
        skinnedMeshRendererHead.material = alphaHead;
        skinnedMeshRendererNoviceBody.material = alphaNoviceBody;
        skinnedMeshRendererVeteranBody.material = alphaVeteranBody;
        skinnedMeshRendererEliteBody.material = alphaEliteBody;

        float headAlpha = 0;
        float noviceBodyAlpha = 0;
        float veteranBodyAlpha = 0;
        float eliteBodyAlpha = 0;

        while (headAlpha < 1 && noviceBodyAlpha < 1 && veteranBodyAlpha < 1 && eliteBodyAlpha < 1)
        {
            headAlpha += 8 * Time.unscaledDeltaTime;
            alphaHead.color = new Color(1, 1, 1, headAlpha);

            noviceBodyAlpha += 8 * Time.unscaledDeltaTime;
            alphaNoviceBody.color = new Color(1, 1, 1, noviceBodyAlpha);

            veteranBodyAlpha += 8 * Time.unscaledDeltaTime;
            alphaVeteranBody.color = new Color(1, 1, 1, veteranBodyAlpha);

            eliteBodyAlpha += 8 * Time.unscaledDeltaTime;
            alphaEliteBody.color = new Color(1, 1, 1, eliteBodyAlpha);

            yield return new WaitForSecondsRealtime(Time.unscaledDeltaTime);
        }
        headAlpha = 1;
        noviceBodyAlpha = 1;
        veteranBodyAlpha = 1;
        eliteBodyAlpha = 1;

        skinnedMeshRendererHead.material = opaqueHead;
        skinnedMeshRendererNoviceBody.material = opaqueNoviceBody;
        skinnedMeshRendererVeteranBody.material = opaqueVeteranBody;
        skinnedMeshRendererEliteBody.material = opaqueEliteBody;
    }
}