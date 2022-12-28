using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CubeVerticalControll1 : MonoBehaviour {

    public GameObject flat;
    public GameObject flat_diamond;
    public GameObject cube_vertical;
    public GameObject cube_vertical_diamond;


    public List<Vector3> _list_position;
    public List<GameObject> _list_object;
    public List<CubePrefabsControll> _list_controll;

    public int numberChild;


    private Vector3 _standarPositon;
    private int countChild;
    private int index;
    private int i;

    void Start()
    {
        countChild = 0;
        index = 0;
        _standarPositon = Vector3.zero;
        _list_position = new List<Vector3>();
        _list_object = new List<GameObject>();
        _list_controll = new List<CubePrefabsControll>();
    }

    void Update()
    {
        if (GameController.Instance._isGameAlive)
        {
            _standarPositon = GameController.Instance._endCubeHorizontalPosition;

            if (GameController.Instance._control_Horizontal && GameController.Instance.Cube_Horizontal1.childCount == numberChild)
            {
                if (transform.childCount == 0)
                {
                    NextGame(flat, _standarPositon);
                }
                else
                {
                    GenerateMap();
                }


                if (i < transform.childCount)
                {
                    if (transform.GetChild(i).GetComponent<CubePrefabsControll>() != null)
                    {
                        transform.GetChild(i).GetComponent<CubePrefabsControll>().enabled = false;
                        _list_controll.Add(transform.GetChild(i).GetComponent<CubePrefabsControll>());
                        if (index <= 0)
                            _list_controll[index].enabled = true;
                    }

                    i++;
                }
            }

            if (GameController.Instance.Cube_Horizontal.childCount == numberChild && GameController.Instance._control_Horizontal)
            {
                DestroyAllChild();
            }

            if (index < _list_controll.Count - 1 && _list_controll[index]?.enabled == false)
            {
                index++;
                _list_controll[index].enabled = true;
            }

        }
        else
        {
            DestroyAllChild();
        }
        
    }

    public void CreatePrefabs(GameObject o, Vector3 position)
    {
        GameObject obj = Instantiate(o, Vector3.zero, Quaternion.identity) as GameObject;
        obj.transform.parent = this.gameObject.transform;
        obj.transform.localPosition = position;

    }

    public void CreateListPrefabs(GameObject o, Vector3 pos)
    {
        _list_object.Add(o);
        _list_position.Add(pos);
        countChild++;
    }

    /// <summary>
    /// Đoạn code này sinh ra tự động các cube_vertical và các flat trong Object Cube_vertical theo tọa độ
    /// của các cube_horizotal trước đó
    /// </summary>
    /// 

    #region Doan code nay sinh map tu dong
    public void GenerateMap()
    {
        if (countChild < numberChild)
        {
            int k = Random.Range(0, 4);

            if (k == 0)
            {
                int j = _list_object.Count - 1;

                if (_list_object.Count > 0 && (_list_object[j].Equals(cube_vertical) || _list_object[j].Equals(cube_vertical_diamond)))
                {
                    int r = Random.Range(0, 2);

                    if (r == 0)
                    {
                        Vector3 pos = new Vector3(_standarPositon.x + 1, _list_position[j].y, _list_position[j].z - 2);
                        CreateListPrefabs(cube_vertical, pos);
                    }
                    else
                    {
                        Vector3 pos = new Vector3(_standarPositon.x - 1, _list_position[j].y, _list_position[j].z - 2);
                        CreateListPrefabs(cube_vertical, pos);
                    }
                }
                else
                {
                    int r = Random.Range(0, 2);
                    if (r == 0)
                    {
                        Vector3 pos = new Vector3(_standarPositon.x + 1, _list_position[j].y, _list_position[j].z - 5);
                        CreateListPrefabs(cube_vertical, pos);
                    }
                    else
                    {
                        Vector3 pos = new Vector3(_standarPositon.x - 1, _list_position[j].y, _list_position[j].z - 5);
                        CreateListPrefabs(cube_vertical, pos);
                    }
                }
            }
            else if (k == 1)
            {
                int j = _list_object.Count - 1;

                if (_list_object.Count > 0 && (_list_object[j].Equals(flat)) || _list_object[j].Equals(flat_diamond))
                {
                    Vector3 pos = new Vector3(_standarPositon.x, _standarPositon.y, _list_position[j].z - 5);
                    CreateListPrefabs(flat, pos);
                }
                else
                {
                    Vector3 pos = new Vector3(_standarPositon.x, _standarPositon.y, _list_position[j].z - 2);
                    CreateListPrefabs(flat, pos);
                }
            }
            else if (k == 2)
            {
                int j = _list_object.Count - 1;

                if (_list_object.Count > 0 && (_list_object[j].Equals(flat)) || _list_object[j].Equals(flat_diamond))
                {
                    Vector3 pos = new Vector3(_standarPositon.x, _standarPositon.y, _list_position[j].z - 5);
                    CreateListPrefabs(flat_diamond, pos);
                }
                else
                {
                    Vector3 pos = new Vector3(_standarPositon.x, _standarPositon.y, _list_position[j].z - 2);
                    CreateListPrefabs(flat_diamond, pos);
                }
            }
            else if (k == 3)
            {
                int j = _list_object.Count - 1;

                if (_list_object.Count > 0 && (_list_object[j].Equals(cube_vertical) || _list_object[j].Equals(cube_vertical_diamond)))
                {
                    int r = Random.Range(0, 2);

                    if (r == 0)
                    {
                        Vector3 pos = new Vector3(_standarPositon.x + 1, _list_position[j].y, _list_position[j].z - 2);
                        CreateListPrefabs(cube_vertical_diamond, pos);
                    }
                    else
                    {
                        Vector3 pos = new Vector3(_standarPositon.x - 1, _list_position[j].y, _list_position[j].z - 2);
                        CreateListPrefabs(cube_vertical_diamond, pos);
                    }
                }
                else
                {
                    int r = Random.Range(0, 2);
                    if (r == 0)
                    {
                        Vector3 pos = new Vector3(_standarPositon.x + 1, _list_position[j].y, _list_position[j].z - 5);
                        CreateListPrefabs(cube_vertical_diamond, pos);
                    }
                    else
                    {
                        Vector3 pos = new Vector3(_standarPositon.x - 1, _list_position[j].y, _list_position[j].z - 5);
                        CreateListPrefabs(cube_vertical_diamond, pos);
                    }
                }
            }
            CreatePrefabs(_list_object[countChild - 1], _list_position[countChild - 1]);
        }
    }
    #endregion


    public void NextGame(GameObject o, Vector3 pos)
    {
        CreateListPrefabs(o, pos);
        CreatePrefabs(o, pos);
    }

    public void DestroyAllChild()
    {
        _list_controll.Clear();
        _list_object.Clear();
        _list_position.Clear();

        countChild = 0;
        index = 0;
        i = 0;
        _standarPositon = GameController.Instance._endCubeHorizontalPosition;

        if (transform.childCount > 0)
        {
            Destroy(transform.GetChild(0).gameObject);
        }

    }
}
