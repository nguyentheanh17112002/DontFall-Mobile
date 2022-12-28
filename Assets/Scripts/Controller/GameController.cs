using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance;


    public PlayerController Player;
    public Transform Camera;

    public Transform Cube_Horizontal;
    public Transform Cube_Horizontal1;
    public Transform Cube_Vertical;
    public Transform Cube_Vertical1;

    public Image panelHome;
    public Image panelPause;
    public Image panelRestart;

    public Text txt_HighScore;
    public Text txt_Score;
    public Text txt_ScoreGameOver;
    public Text txt_YourScore;

    public Button btn_Pause;

    public bool _isGamePlaying      = false;
    public bool _control_Horizontal = true;
    public bool _isGameAlive        = false;
    public bool _movePlayer         = false;

    public Vector3 _endCubeHorizontalPosition;
    public Vector3 _endCubeVerticalPosition;


    public float _speedMove;

    private int _score = 0;

    private Vector3 startCamera;
    private Vector3 rotationCamera;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        startCamera = Camera.position;
        rotationCamera = Camera.eulerAngles;
        _isGamePlaying = false;
        _isGameAlive = true;
        _control_Horizontal = true;
        _movePlayer = false;
        _endCubeHorizontalPosition = Vector3.zero;
        _endCubeVerticalPosition = Vector3.zero;
        Player.transform.position = new Vector3(1, 0, 0);
        Camera.position = startCamera;
        Camera.eulerAngles = rotationCamera;
        Player.transform.eulerAngles = Vector3.zero;
        Player.GetComponent<Rigidbody>().useGravity = false;
        Camera.GetComponent<CameraLookAt>().enabled = true;
        _score = 0;
        _speedMove = 5;
        panelRestart.gameObject.SetActive(false);
        panelHome.gameObject.SetActive(true);
        btn_Pause.gameObject.SetActive(false);
        txt_Score.gameObject.SetActive(false);
        Player.gameObject.SetActive(true);
        SoundController.Inst.PlayGameBackGround();
    }

    void Update()
    {
        txt_Score.text = _score.ToString();

        if (Player.transform.position.y < -5)
        {
            OnGameOver();
        }

        if (Player.transform.position.y < -1)
        {
            if (Camera.GetComponent<CameraLookAt>().enabled)
            {
                SoundController.Inst.PlayGameOver();
                CheckHighScore();
                AddCoin();
                Camera.GetComponent<CameraLookAt>().enabled = false;
            }
        }

        CheckEndPoint();

        if (_movePlayer)
        {
            _speedMove += Time.deltaTime * 0.1f;
        }
    }

    private void CheckEndPoint()
    {
        if (Cube_Horizontal.childCount > 0)
        {
            Vector3 _standar = Cube_Horizontal.GetChild(0).localPosition;
            Vector3 pos = Cube_Horizontal.GetChild(Cube_Horizontal.childCount - 1).localPosition;
            if (pos.z == _standar.z)// nếu cube cuối cùng là ground
            {
                _endCubeHorizontalPosition = new Vector3(pos.x + 5.5f, pos.y, pos.z + 0.5f);
            }
            else if (pos.z > _standar.z)// nếu cube cuối cùng là obstacle
            {
                _endCubeHorizontalPosition = new Vector3(pos.x + 2.5f, pos.y, pos.z - 0.5f);
            }
            else if (pos.z < _standar.z)// nếu cube cuối cùng là obstacle
            {
                _endCubeHorizontalPosition = new Vector3(pos.x + 2.5f, pos.y, pos.z + 1.5f);
            }
        }

        if (Cube_Horizontal1.childCount > 0)
        {
            Vector3 _standar = Cube_Horizontal1.GetChild(0).localPosition;
            Vector3 pos = Cube_Horizontal1.GetChild(Cube_Horizontal1.childCount - 1).localPosition;
            if (pos.z == _standar.z)
            {
                _endCubeHorizontalPosition = new Vector3(pos.x + 5.5f, pos.y, pos.z + 0.5f);
            }
            else if (pos.z > _standar.z)
            {
                _endCubeHorizontalPosition = new Vector3(pos.x + 2.5f, pos.y, pos.z - 0.5f);
            }
            else if (pos.z < _standar.z)
            {
                _endCubeHorizontalPosition = new Vector3(pos.x + 2.5f, pos.y, pos.z + 1.5f);
            }
        }


        if (Cube_Vertical.childCount > 0)
        {
            Vector3 _standar = Cube_Vertical.GetChild(0).localPosition;
            Vector3 pos = Cube_Vertical.GetChild(Cube_Vertical.childCount - 1).localPosition;
            if (pos.x == _standar.x)
            {
                _endCubeVerticalPosition = new Vector3(pos.x - 0.5f, pos.y, pos.z - 5.5f);
            }
            else if (pos.x < _standar.x)
            {
                _endCubeVerticalPosition = new Vector3(pos.x + 0.5f, pos.y, pos.z - 2.5f);
            }
            else if (pos.x > _standar.x)
            {
                _endCubeVerticalPosition = new Vector3(pos.x - 1.5f, pos.y, pos.z - 2.5f);
            }
        }

        if (Cube_Vertical1.childCount > 0)
        {
            Vector3 _standar = Cube_Vertical1.GetChild(0).localPosition;
            Vector3 pos = Cube_Vertical1.GetChild(Cube_Vertical1.childCount - 1).localPosition;
            if (pos.x == _standar.x)
            {
                _endCubeVerticalPosition = new Vector3(pos.x - 0.5f, pos.y, pos.z - 5.5f);
            }
            else if (pos.x < _standar.x)
            {
                _endCubeVerticalPosition = new Vector3(pos.x + 0.5f, pos.y, pos.z - 2.5f);
            }
            else if (pos.x > _standar.x)
            {
                _endCubeVerticalPosition = new Vector3(pos.x - 1.5f, pos.y, pos.z - 2.5f);
            }
        }
    }

    public void OnRestartGame()
    {
        _isGamePlaying = false;
        _movePlayer = false;
        _isGameAlive = true;
        _control_Horizontal = true;
        _endCubeHorizontalPosition = Vector3.zero;
        _endCubeVerticalPosition = Vector3.zero;
        Player.transform.position = new Vector3(1, 0, 0);
        Camera.position = startCamera;
        Camera.eulerAngles = rotationCamera;
        Player.transform.eulerAngles = Vector3.zero;
        Player.GetComponent<Rigidbody>().useGravity = false;
        Camera.GetComponent<CameraLookAt>().enabled = true;
        _score = 0;
        _speedMove = 5;
        panelRestart.gameObject.SetActive(false);
        panelHome.gameObject.SetActive(true);
        btn_Pause.gameObject.SetActive(false);
        txt_Score.gameObject.SetActive(false);
        Player.gameObject.SetActive(true);
        Player.OnRestartGame();
        SoundController.Inst.PlayGameBackGround();
    }


    public void OnGameOver()
    {
        _isGamePlaying = false;
        _isGameAlive = false;
        _endCubeHorizontalPosition = Vector3.zero;
        _endCubeVerticalPosition = Vector3.zero;
        Player.GetComponent<Rigidbody>().useGravity = false;
        Camera.GetComponent<CameraLookAt>().enabled = false;
        txt_ScoreGameOver.text = _score.ToString();
        panelRestart.gameObject.SetActive(true);
        txt_Score.gameObject.SetActive(false);
        btn_Pause.gameObject.SetActive(false);
    }

    public void OnPauseGame()
    {
        if (_movePlayer)
        {
            _movePlayer = false;
            Time.timeScale = 0;
            panelPause.gameObject.SetActive(true);
            btn_Pause.gameObject.SetActive(false);
        }
    }

    public void OnPlayGame()
    {
        Time.timeScale = 1;
        _isGamePlaying = true;
        _isGameAlive = true;
        _movePlayer = true;
        Player.GetComponent<Rigidbody>().useGravity = true;
        Camera.GetComponent<CameraLookAt>().enabled = true;
        panelHome.gameObject.SetActive(false);
        panelPause.gameObject.SetActive(false);
        panelRestart.gameObject.SetActive(false);
        btn_Pause.gameObject.SetActive(true);
        txt_Score.gameObject.SetActive(true);
        SoundController.Inst.StopGameBackGround();
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void LoadIntroScene()
    {
        SceneManager.LoadScene(SceneName.Intro);
    }


    public void AddScore()
    {
        _score++;
    }

    public void GoShop()
    {
        SceneManager.LoadScene(SceneName.Shop);
    }

    private void CheckHighScore()
    {
        if (PlayerPrefs.GetInt(Config.HighScore) <= _score && _score != 0)
        {
            PlayerPrefs.SetInt(Config.HighScore, _score);
            txt_HighScore.text = "";
            txt_YourScore.text = "New High Score !!!";
        }
        else
        {
            txt_HighScore.text = "Best " + PlayerPrefs.GetInt(Config.HighScore).ToString();
            txt_YourScore.text = "Your Score";
        }
    }

    public void AddCoin()
    {
        int coin = _score + PlayerPrefs.GetInt(Config.Coin);
        PlayerPrefs.SetInt(Config.Coin, coin);
    }
}