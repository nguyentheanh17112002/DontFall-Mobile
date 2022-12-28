using UnityEngine;




public class CubePrefabsControll : MonoBehaviour
{
    public Material cube_Material;
    public float _rayDistance;
    public bool _isHorizontal;

    private Material current_material;
    private Ray ray;
    private RaycastHit hit;

    private bool gamePlaying;
    public GameObject cubePrefab;


    void Awake()
    {
        current_material = transform.GetComponentInChildren<MeshRenderer>().material;
    }

    private void OnEnable()
    {
        SwipeDetector.OnSwipeRightCallback -= OnSwipeRight;
        SwipeDetector.OnSwipeLeftCallback -= OnSwipeLeft;
        SwipeDetector.OnSwipeRightCallback += OnSwipeRight;
        SwipeDetector.OnSwipeLeftCallback += OnSwipeLeft;
    }

    private void OnDisable()
    {
        SwipeDetector.OnSwipeRightCallback -= OnSwipeRight;
        SwipeDetector.OnSwipeLeftCallback -= OnSwipeLeft;
    }

    private void OnSwipeLeft()
    {
        gamePlaying = GameController.Instance._isGamePlaying;
        if (gamePlaying)
        {
            if (GameController.Instance._control_Horizontal && _isHorizontal && Time.timeScale == 1)
            {
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y,
                    transform.localPosition.z + 1);
                ray = new Ray(transform.localPosition - Vector3.left, Vector3.left * _rayDistance);
                Debug.DrawRay(ray.origin, ray.direction * _rayDistance, Color.red);

                if (Physics.Raycast(ray, out hit, _rayDistance))
                {
                    if (hit.collider.CompareTag(Tags.Horizontal))
                    {
                        SoundController.Inst.PlayMoveDone();
                        transform.GetComponentInChildren<MeshRenderer>().material = cube_Material;
                        transform.gameObject.tag = Tags.Horizontal;
                        gameObject.GetComponent<CubePrefabsControll>().enabled = false;
                    }
                }
            }
            else if (GameController.Instance._control_Horizontal == false && _isHorizontal == false &&
                 Time.timeScale == 1)
            {
                transform.localPosition = new Vector3(transform.localPosition.x + 1, transform.localPosition.y,
                    transform.localPosition.z);
                ray = new Ray(transform.localPosition - Vector3.forward, Vector3.forward * 2);
                Debug.DrawRay(ray.origin, ray.direction * 2, Color.red);
                if (Physics.Raycast(ray, out hit, _rayDistance))
                {
                    if (hit.collider.CompareTag(Tags.Vertical))
                    {
                        SoundController.Inst.PlayMoveDone();
                        transform.GetComponentInChildren<MeshRenderer>().material = cube_Material;
                        transform.gameObject.tag = Tags.Vertical;
                        gameObject.GetComponent<CubePrefabsControll>().enabled = false;
                    }
                }
            }

        }
    }
    private void OnSwipeRight()
    {
        gamePlaying = GameController.Instance._isGamePlaying;
        if (gamePlaying)
        {
            if (GameController.Instance._control_Horizontal && _isHorizontal && Time.timeScale == 1)
            {
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y,
                    transform.localPosition.z - 1);
                ray = new Ray(transform.localPosition - Vector3.left, Vector3.left * _rayDistance);
                Debug.DrawRay(ray.origin, ray.direction * _rayDistance, Color.red);

                if (Physics.Raycast(ray, out hit, _rayDistance))
                {
                    if (hit.collider.CompareTag(Tags.Horizontal))
                    {
                        SoundController.Inst.PlayMoveDone();
                        transform.GetComponentInChildren<MeshRenderer>().material = cube_Material;
                        transform.gameObject.tag = Tags.Horizontal;
                        gameObject.GetComponent<CubePrefabsControll>().enabled = false;
                    }
                }
            }
            else if (GameController.Instance._control_Horizontal == false && _isHorizontal == false &&
                     Time.timeScale == 1)
            {
                transform.localPosition = new Vector3(transform.localPosition.x - 1, transform.localPosition.y,
                        transform.localPosition.z);
                ray = new Ray(transform.localPosition - Vector3.forward, Vector3.forward * 2);
                Debug.DrawRay(ray.origin, ray.direction * 2, Color.red);
                if (Physics.Raycast(ray, out hit, _rayDistance))
                {
                    if (hit.collider.CompareTag(Tags.Vertical))
                    {
                        SoundController.Inst.PlayMoveDone();
                        transform.GetComponentInChildren<MeshRenderer>().material = cube_Material;
                        transform.gameObject.tag = Tags.Vertical;
                        gameObject.GetComponent<CubePrefabsControll>().enabled = false;
                    }
                }
            }
            
        }

    }

    /*

    void Update()
    {

        //gamePlaying = GameController.Instance._isGamePlaying;
        /* 
        if (gamePlaying)
        {
            if (GameController.Instance._control_Horizontal && _isHorizontal && Time.timeScale == 1)
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y,
                        transform.localPosition.z + 1);
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y,
                        transform.localPosition.z - 1);
                }

                ray = new Ray(transform.localPosition - Vector3.left, Vector3.left * _rayDistance);
                Debug.DrawRay(ray.origin, ray.direction * _rayDistance, Color.red);

                if (Physics.Raycast(ray, out hit, _rayDistance))
                {
                    if (hit.collider.CompareTag(Tags.Horizontal))
                    {
                        SoundController.Inst.PlayMoveDone();
                        transform.GetComponentInChildren<MeshRenderer>().material = cube_Material;
                        transform.gameObject.tag = Tags.Horizontal;
                        gameObject.GetComponent<CubePrefabsControll>().enabled = false;
                    }
                }
            }
            else if (GameController.Instance._control_Horizontal == false && _isHorizontal == false &&
                     Time.timeScale == 1)
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    transform.localPosition = new Vector3(transform.localPosition.x - 1, transform.localPosition.y,
                        transform.localPosition.z);
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    transform.localPosition = new Vector3(transform.localPosition.x + 1, transform.localPosition.y,
                        transform.localPosition.z);
                }

                ray = new Ray(transform.localPosition - Vector3.forward, Vector3.forward * 2);
                Debug.DrawRay(ray.origin, ray.direction * 2, Color.red);
                if (Physics.Raycast(ray, out hit, _rayDistance))
                {
                    if (hit.collider.CompareTag(Tags.Vertical))
                    {
                        SoundController.Inst.PlayMoveDone();
                        transform.GetComponentInChildren<MeshRenderer>().material = cube_Material;
                        transform.gameObject.tag = Tags.Vertical;
                        gameObject.GetComponent<CubePrefabsControll>().enabled = false;
                    }
                }
            }
        }
       
    }

    */

    public void Reset()
    {
        transform.gameObject.tag = Tags.Untagged;
        transform.GetComponentInChildren<MeshRenderer>().material = current_material;
    }
}