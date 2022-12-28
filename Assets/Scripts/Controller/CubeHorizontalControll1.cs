using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CubeHorizontalControll1 : MonoBehaviour {

    public GameObject flat; // cube xanh
    public GameObject flat_diamond; // cube xanh + kc
    public GameObject cube_horizontal; // cube trắng
    public GameObject cube_horizontal_diamond; // cube trắng + kc

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
            _standarPositon = GameController.Instance._endCubeVerticalPosition;
            if (GameController.Instance._control_Horizontal == false && GameController.Instance.Cube_Vertical.childCount == numberChild)
            {
                if (transform.childCount == 0)
                {
                    NextGame(flat, _standarPositon);
                }

                GenerateMap();

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

            if (GameController.Instance.Cube_Vertical1.childCount == numberChild && GameController.Instance._control_Horizontal == false)
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
        

    private void CreatePrefabs(GameObject o, Vector3 position)
    {
        GameObject obj = Instantiate(o, Vector3.zero, Quaternion.identity) as GameObject;
        obj.transform.parent = this.gameObject.transform;
        obj.transform.localPosition = position;

    }

    private void CreateListPrefabs(GameObject o, Vector3 pos)
    {
        _list_object.Add(o);
        _list_position.Add(pos);
        countChild++;
    }

    /// <summary>
    /// Đoạn code này sinh ra lần lượt các cube hoặc các flat một cách ngẫu nhiên, trên cơ sở 3 prefabs đầu tiên
    /// </summary>
    /// 
    #region Dieu khien viec sinh ra Map

    public void GenerateMap()
    {
        if (countChild < numberChild)
        {
            int k = Random.Range(0, 4);

            if (k == 0)
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
            else if (k == 1)
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
            else if(k == 2)
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
            else
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

    public void DestroyAllChild()
    {

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

    public void NextGame(GameObject o, Vector3 pos)
    {
        CreateListPrefabs(o, pos);
        CreatePrefabs(o, pos);
    }
}
