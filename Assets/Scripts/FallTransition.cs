using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FallTransition : MonoBehaviour
{
    [SerializeField]
    public Material fallMaterial;
    public int transitionTime = 1;

    void Awake()
    {
        fallMaterial.SetFloat("_Alpha", 0);
    }

    public IEnumerator FadeOut(Material material, float time)
    {
        // Debug.Log("FallTransition FadeOut has started");
        GetComponent<Image>().material = material;
        float current = 0;
        while (current < time)
        {
            material.SetFloat("_Alpha", current / time);
            yield return new WaitForEndOfFrame();
            current += Time.deltaTime;
        }
        transform.rotation = Quaternion.Euler(0, 0, 0);
        material.SetFloat("_Alpha", 1);
    }

    public IEnumerator FadeIn(Material material, float time)
    {
        // Debug.Log("FallTransition FadeIn has started");
        float current = 0;
        while (current < time)
        {
            material.SetFloat("_Alpha", 1 - (current / time));
            yield return new WaitForEndOfFrame();
            current += Time.deltaTime;
        }
        transform.rotation = Quaternion.Euler(180, 0, 0);
        material.SetFloat("_Alpha", 0);
    }
}
