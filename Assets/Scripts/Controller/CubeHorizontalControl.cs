using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CubeHorizontalControl : MonoBehaviour {


    public GameObject flat; // Cube xanh length = 5
    public GameObject flat_diamond; // Cube xanh + kim cương
    public GameObject cube_horizontal; // Cube trắng length = 2
    public GameObject cube_horizontal_diamond; // Cube trắng + kim cương

    public List<Vector3> _list_position;
    public List<GameObject> _list_object;

    public List<CubePrefabsControll> _list_controll;

    public int numberChild;
    
    
    private Vector3 _standarPositon;
    private int countChild;
    private int index; // index của controller
    private int i; // index child
    
	void Start () {

        _standarPositon = Vector3.zero;
        countChild = 0;
        index = 0;
        _list_position = new List<Vector3>();
        _list_object = new List<GameObject>();
        _list_controll = new List<CubePrefabsControll>();
	}
	

	void Update () {

        //Creat Map horizontal gồm numberChild child trong đó random số lượng cũng như vị trí của các cube 
        if (GameController.Instance._isGameAlive)
        {
            if ((GameController.Instance._control_Horizontal && GameController.Instance.Cube_Horizontal1.childCount == 0) ||
                (GameController.Instance.Cube_Horizontal1.childCount == 0 && GameController.Instance.Cube_Vertical1.childCount == numberChild))
            {
               // Debug.LogError($"index {index} :: childCount {transform.childCount}");
                if (transform.childCount == 0)
                {
                    NextGame(flat, _standarPositon);
                }
                else
                {
                    GenerateMap();
                }

                //set up cube child after generate
                if (i < transform.childCount)
                {
                    //Debug.LogError($"Setup child {i} :: childCount {transform.childCount}");
                    if (transform.GetChild(i).GetComponent<CubePrefabsControll>() != null)
                    {
                        transform.GetChild(i).GetComponent<CubePrefabsControll>().enabled = false;
                        _list_controll.Add(transform.GetChild(i).GetComponent<CubePrefabsControll>());
                        if (index <= 0)
                        {
                            _list_controll[index].enabled = true;
                        }
                    }

                    i++;
                }
            }

            if (GameController.Instance.Cube_Horizontal1.childCount == numberChild && GameController.Instance._control_Horizontal == false)
            {
                DestroyAllChild();
            }

            if (index < _list_controll.Count - 1 && _list_controll[index]?.enabled == false)
            {
               // Debug.LogError($"Enable child {index} :: childCount {transform.childCount}");
                index++;
                _list_controll[index].enabled = true;
            }
        }
        else
        {
            DestroyAllChild();
        }
    }
       

    private void CreatePrefabs(GameObject o, Vector3 position)
    {
        GameObject obj = Instantiate(o, Vector3.zero, Quaternion.identity) as GameObject;
        obj.transform.parent = this.gameObject.transform;
        obj.transform.localPosition = position;
        
    }

    private void CreateListPrefabs(GameObject o,Vector3 pos)
    {
        _list_object.Add(o);
        _list_position.Add(pos);
        countChild++;
    }

    /// <summary>
    /// Đoạn code này sinh ra lần lượt các cube hoặc các flat một cách ngẫu nhiên, trên cơ sở 3 prefabs đầu tiên
    /// </summary>
    #region Dieu khien viec sinh ra Map
    public void GenerateMap()
    {
        if (countChild < numberChild)
        {
            int k = Random.Range(0, 4);

            if (k == 0) // create thêm cube trắng 
            {
                int j = _list_object.Count - 1;

                if (_list_object[j].Equals(cube_horizontal) || _list_object[j].Equals(cube_horizontal_diamond))
                {
                    int r = Random.Range(0, 2);
                    if (r == 0)
                    {
                        Vector3 pos = new Vector3(_list_position[j].x + 2, _list_position[j].y, _standarPositon.z + 1);
                        CreateListPrefabs(cube_horizontal, pos);
                    }
                    else
                    {
                        Vector3 pos = new Vector3(_list_position[j].x + 2, _list_position[j].y, _standarPositon.z - 1);
                        CreateListPrefabs(cube_horizontal, pos);
                    }
                }
                else
                {
                    if (_list_object[j].Equals(flat) || _list_object[j].Equals(flat_diamond))
                    {
                        int r = Random.Range(0, 2);
                        if (r == 0)
                        {
                            Vector3 pos = new Vector3(_list_position[j].x + 5, _list_position[j].y, _standarPositon.z + 1);
                            CreateListPrefabs(cube_horizontal, pos);
                        }
                        else
                        {
                            Vector3 pos = new Vector3(_list_position[j].x + 5, _list_position[j].y, _standarPositon.z - 1);
                            CreateListPrefabs(cube_horizontal, pos);
                        }
                    }
                }
            }
            else if (k == 1) // create thêm cube xanh
            {
                int j = _list_object.Count - 1;
                if (_list_object[j].Equals(flat) || _list_object[j].Equals(flat_diamond))
                {
                    Vector3 pos = new Vector3(_list_position[j].x + 5, 0, _standarPositon.z);
                    CreateListPrefabs(flat, pos);
                }
                else
                {
                    Vector3 pos = new Vector3(_list_position[j].x + 2, 0, _standarPositon.z);
                    CreateListPrefabs(flat, pos);
                }
            }
            else if(k == 2) // create cube xanh với kim cương
            {
                int j = _list_object.Count - 1;
                if (_list_object[j].Equals(flat) || _list_object[j].Equals(flat_diamond))
                {
                    Vector3 pos = new Vector3(_list_position[j].x + 5, 0, _standarPositon.z);
                    CreateListPrefabs(flat_diamond, pos);
                }
                else
                {
                    Vector3 pos = new Vector3(_list_position[j].x + 2, 0, _standarPositon.z);
                    CreateListPrefabs(flat_diamond, pos);
                }
            }
            else // create cube trắng với kc
            {
                int j = _list_object.Count - 1;

                if (_list_object[j].Equals(cube_horizontal) || _list_object[j].Equals(cube_horizontal_diamond))
                {
                    int r = Random.Range(0, 2);
                    if (r == 0)
                    {
                        Vector3 pos = new Vector3(_list_position[j].x + 2, _list_position[j].y, _standarPositon.z + 1);
                        CreateListPrefabs(cube_horizontal_diamond, pos);
                    }
                    else
                    {
                        Vector3 pos = new Vector3(_list_position[j].x + 2, _list_position[j].y, _standarPositon.z - 1);
                        CreateListPrefabs(cube_horizontal_diamond, pos);
                    }
                }
                else
                {
                    if (_list_object[j].Equals(flat) || _list_object[j].Equals(flat_diamond))
                    {
                        int r = Random.Range(0, 2);
                        if (r == 0)
                        {
                            Vector3 pos = new Vector3(_list_position[j].x + 5, _list_position[j].y, _standarPositon.z + 1);
                            CreateListPrefabs(cube_horizontal_diamond, pos);
                        }
                        else
                        {
                            Vector3 pos = new Vector3(_list_position[j].x + 5, _list_position[j].y, _standarPositon.z - 1);
                            CreateListPrefabs(cube_horizontal_diamond, pos);
                        }
                    }
                }
            }
            CreatePrefabs(_list_object[countChild - 1], _list_position[countChild - 1]);
        }
    }
    #endregion

    /// <summary>
    /// Destroy All Child in Transform when player pass GameObject
    /// </summary>
    public void DestroyAllChild()
    {

      //  Debug.LogError($"DestroyAllChild :: transform.childCount {transform.childCount}");
        if (transform.childCount > 0)
        {
            Destroy(transform.GetChild(0).gameObject);
        }
        else
        {
            _list_controll.Clear();
            _list_object.Clear();
            _list_position.Clear();
            countChild = 0;
            index = 0;
            i = 0;
            _standarPositon = GameController.Instance._endCubeVerticalPosition;
        }
    }

    /// <summary>
    /// Sinh ra flat dau tien
    /// </summary>
    /// <param name="o"></param>
    /// <param name="pos"></param>
    public void NextGame(GameObject o, Vector3 pos)
    {
        CreateListPrefabs(o, pos);
        CreatePrefabs(o, pos);
    }
}
