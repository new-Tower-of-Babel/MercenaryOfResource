using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeOutHit : MonoBehaviour
{
    [SerializeField] Image image;
    public float fadeDuration = 1.5f;

    private void OnEnable()
    {
        StartCoroutine(FadeOut(image));
    }

    private void ResetImageAlpha()
    {
        Color color = image.color;
        color.a = 1f;
        image.color = color;
    }

    private IEnumerator FadeOut(Image image)
    {
        float startAlpha = image.color.a;  // 이미지의 초기 알파 값
        float time = 0f;

        while (time < fadeDuration)
        {
            float alpha = Mathf.Lerp(startAlpha, 0f, time / fadeDuration);
            Color color = image.color;
            color.a = alpha;
            image.color = color;

            time += Time.deltaTime;
            yield return null;
        }

        Color finalColor = image.color;
        finalColor.a = 0f;
        image.color = finalColor;

        this.gameObject.SetActive(false);
    }

    void OnDisable()
    {
        ResetImageAlpha();
    }
}
