using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class GroundChange : MonoBehaviour {

    public Color colorStart;
    public Color colorEnd;
    public Material cube_material;
    public Color backupColor;

    private float rate = 1;
    private float i = 0;

    void Awake()
    {
        colorStart = backupColor;
        colorEnd = backupColor;
    }
    
    void Update()
    {
        if (GameController.Instance._isGamePlaying)
        {
            i += Time.deltaTime;
            if (i >= 10)
            {
                i = 0;
                colorStart = cube_material.color;
                colorEnd = new Color(Random.value, Random.value, Random.value);
            }
            else
            {
                cube_material.color = Color.Lerp(colorStart, colorEnd, i * rate);
            }
        }
    }

    void OnDisable()
    {
        cube_material.color = backupColor;
    }

    public void RestartColor()
    {
        colorStart = backupColor;
        colorEnd = backupColor;
        cube_material.color = backupColor;
        i = 0;
    }

}
