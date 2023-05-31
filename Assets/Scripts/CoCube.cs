using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoCube : MonoBehaviour
{
    Renderer renderer;
    Color baseColor;
    Color finalColor;
    float transitionTime = 1.0f;
    float currentTransition = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
        baseColor = renderer.material.color;
        finalColor = Color.green;
    }
    //say we wanted to update the colour of our cube slowly over time
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("f"))
        {
            StopAllCoroutines(); //we make sure to stop all coroutines
            StartCoroutine(Fade());
        }
        //we will want to slowly transition this color from one color to another
    }

    IEnumerator Fade()
    {
        Color c = renderer.material.color; //this is our base color
        Color f = new Color(Random.value, Random.value, Random.value);
        for(float alpha = 1f; alpha >= 0; alpha -= 0.1f)
        {
            renderer.material.color = c * alpha + (1.0f - alpha) * f;
            yield return new WaitForSeconds(0.5f); //here we're going to wait until .2f seconds has passed before this can be called again
        }

    }
}
