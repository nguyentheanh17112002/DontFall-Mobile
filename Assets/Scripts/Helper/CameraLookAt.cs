using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class CameraLookAt : MonoBehaviour {

    public Transform Target;
    public Vector3 offset;

    public Color _colorStart;
    public Color _colorEnd;

    public List<Color> _listColor;

    Camera camera;

    private float rate = 1;
    private float i = 0;
    public int index = 1;

	void Start ()
    {
        offset = transform.position - Target.position;
        camera = GetComponent<Camera>();
        _colorEnd = _listColor[index];
	}


    void Update()
    {
        i += Time.deltaTime;
        if (index >= _listColor.Count)
        {
            index = 0;
        }
        if (i >= 5)
        {
            _colorStart = camera.backgroundColor;
            _colorEnd = _listColor[index];
            index++;
            i = 0;
        }
        else
        {
            camera.backgroundColor = Color.Lerp(_colorStart, _colorEnd, i * rate);
        }
    }


	void LateUpdate () 
    {
        transform.position = Target.position + offset;
	}
}
