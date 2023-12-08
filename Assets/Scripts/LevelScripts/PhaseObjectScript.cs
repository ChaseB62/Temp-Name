using System.Collections;
using UnityEngine;

//made by Aiden
public class PhaseObjectScript : MonoBehaviour
{
    public float fadeTime = 3f; // Adjust the fade duration as needed
    public float finalAlpha = 0.3f; // Adjust the final alpha value as needed
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();

        // Start the fading process after a delay (e.g., 2 seconds)
        StartCoroutine(FadeAndDisableCoroutine(2f));
    }

    IEnumerator FadeAndDisableCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);

        float elapsedTime = 0f;
        Color startColor = spriteRenderer.color;
        float initialAlpha = startColor.a;

        while (elapsedTime < fadeTime)
        {
            // Gradually decrease alpha over time
            float newAlpha = Mathf.Lerp(initialAlpha, finalAlpha, elapsedTime / fadeTime);
            spriteRenderer.color = new Color(startColor.r, startColor.g, startColor.b, newAlpha);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the final alpha is set to the specified value
        spriteRenderer.color = new Color(startColor.r, startColor.g, startColor.b, finalAlpha);

        // Disable the box collider after fading
        boxCollider.enabled = false;
    }
}
