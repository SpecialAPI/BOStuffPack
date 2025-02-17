using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialController : MonoBehaviour
{
    public Renderer render;

    public Color Color
    {
        get => render.material.color;
        set => render.material.color = value;
    }
}
