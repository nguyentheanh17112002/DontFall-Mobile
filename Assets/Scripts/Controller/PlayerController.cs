using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class PlayerController : MonoBehaviour {


    public GameObject _badyduck;
    public GameObject _chespin;
    public GameObject _chicken;
    public GameObject _cow;
    public GameObject _patrick;
    public GameObject _penguin;
    public GameObject _spongeBob;

    public float _speedMove = 5;
    public float _rayDistance = 1;

    private Transform player;
    private Ray ray;
    private RaycastHit hit;
	void Start () 
    {
        string _playerObject = PlayerPrefs.GetString(Config.Player);
        switch (_playerObject)
        {
            case PlayerNames.BabyDuck:
                GeneratorObject(_badyduck);
                break;
            case PlayerNames.Chespin:
                GeneratorObject(_chespin);
                break;
            case PlayerNames.Chicken:
                GeneratorObject(_chicken);
                break;
            case PlayerNames.Cow:
                GeneratorObject(_cow);
                break;
            case PlayerNames.Patrick:
                GeneratorObject(_patrick);
                break;
            case PlayerNames.Penguin:
                GeneratorObject(_penguin);
                break;
            case PlayerNames.SpongeBob:
                GeneratorObject(_spongeBob);
                break;
            default:
                GeneratorObject(_badyduck);
                break;
        }
	}
	
	void Update () 
    {

        _speedMove = GameController.Instance._speedMove;

        ray = new Ray(transform.position + Vector3.up, Vector3.down * _rayDistance);
        Debug.DrawRay(ray.origin, Vector3.down * _rayDistance, Color.red);
        if (Physics.Raycast(ray, out hit, _rayDistance))
        {
            if (!GameController.Instance._control_Horizontal && hit.collider.tag == Tags.Horizontal )
            {
                transform.DOMoveZ(hit.collider.transform.position.z, 0.1f);
                player.DORotate(Vector3.zero, 0.1f);
                GameController.Instance._control_Horizontal = true;
            }
            else if (GameController.Instance._control_Horizontal && hit.collider.tag == Tags.Vertical)
            {
                transform.DOMoveX(hit.collider.transform.position.x, 0.1f);
                player.DORotate(new Vector3(0, 90, 0), 0.1f);
                GameController.Instance._control_Horizontal = false;
            }

        }
        else
        {
            GameController.Instance._isGamePlaying = false;
        }

        if (GameController.Instance._movePlayer)
        {
            if (GameController.Instance._control_Horizontal)
            {
                transform.Translate(Vector3.right * Time.deltaTime * _speedMove);
            }
            else
            {
                transform.Translate(Vector3.back * Time.deltaTime * _speedMove);
            }
        }
	}

    public void GeneratorObject(GameObject o)
    {
        GameObject obj = Instantiate(o, Vector3.zero, Quaternion.identity) as GameObject;
        obj.transform.parent = transform;
        obj.transform.localPosition = new Vector3(0, 0.5f, 0);
        if (obj.GetComponent<AutoRotation>() != null)
        {
            obj.GetComponent<AutoRotation>().enabled = false;
        }
        player = transform.GetChild(0).transform;
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == Tags.Diamond)
        {
            SoundController.Inst.PlayCoin();
            GameController.Instance.AddScore();
            other.gameObject.SetActive(false);
        }
    }

    public void OnRestartGame()
    {
        if(player!=null)
        player.transform.localEulerAngles =Vector3.zero;
    }

}
