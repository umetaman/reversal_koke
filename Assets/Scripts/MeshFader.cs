using UnityEngine;
using System.Collections;

// note: fade doesnt work if the material doest support transparency..

public class MeshFader : MonoBehaviour
{
    private bool fadeOut = false;
    private Rigidbody RB;
    

    void Awake()
    {
        RB = GetComponent<Rigidbody>();

    }
        void Update()
    {
        if (fadeOut) return;

        // wait until rigibody is spleeping
        if (this.gameObject.transform.localScale.x<=-5f)
        {
            fadeOut = true;
            StartCoroutine(FadeOut());
        }

        Vector3 aF = new Vector3(Time.time*100f, 0f, 0f);
        RB.AddForce(aF, ForceMode.Force);
        if (this.gameObject.transform.localScale.x>0f)
        {
            this.gameObject.transform.localScale -= new Vector3(0.01f, 0.01f, 0.01f);
        }
    }

    IEnumerator FadeOut()
    {
        float fadeTime = 2.0f;
        var rend = GetComponent<Renderer>();

        var startColor = Color.white;
        var endColor = new Color(1, 1, 1, 0);

        for (float t = 0.0f; t < fadeTime; t += Time.deltaTime)
        {
            rend.material.color = Color.Lerp(startColor, endColor, t / fadeTime);
            yield return null;

        }
       
        Destroy(this.transform.root.gameObject);
    }
}
