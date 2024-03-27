using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    Color lerpedColor;
    Renderer render;
    public Color color1;
    public Color color2;

    // Start is called before the first frame update
    void Start()
    {
        render = gameObject.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        lerpedColor = Color.Lerp(color1, color2, Mathf.PingPong(Time.time, 1));
        render.material.color = lerpedColor;
    }
}
