using UnityEngine;
using System.Collections;

public class AutoRotation : MonoBehaviour {

    public int _xSpeed;
    public int _ySpeed;
    public int _zSpeed;


    void Start()
    {
        Time.timeScale = 1;
    }

    void LateUpdate()
    {
        transform.Rotate(_xSpeed * Time.deltaTime, _ySpeed * Time.deltaTime, _zSpeed * Time.deltaTime);
    }
}
