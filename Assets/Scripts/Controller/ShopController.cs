using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine.SceneManagement;

public class ShopController : MonoBehaviour
{
    public Transform Item;

    public GameObject btn_Key;
    public Text txt_Key;
    public Text txt_Coin;

    public Sprite keyNotActive;
    public Sprite keyActive;


    public int _coinChespin;
    public int _coinChicken;
    public int _coinCow;
    public int _coinPatrick;
    public int _coinPenguin;
    public int _coinStrongeBob;

    private Vector3 _currentPosition;

    private string _chooseItems;
    private int _currentItem;
    private int _countItems;

    void Start()
    {
        _currentItem = 0;
        _countItems = Item.childCount - 1;
        SoundController.Inst.PlayGameBackGround();
        SwipeDetector.OnSwipeLeftCallback += OnSwipeLeft;
        SwipeDetector.OnSwipeRightCallback += OnSwipeRight;
        UpdateState();
    }

    private void OnDestroy()
    {
        SwipeDetector.OnSwipeLeftCallback -= OnSwipeLeft;
        SwipeDetector.OnSwipeRightCallback -= OnSwipeRight;
    }


    private void OnSwipeRight()
    {
        if (_currentItem == -_countItems)
        {
            return;
        }

        _currentPosition = new Vector3(_currentItem * 5, 0, 0);
        Item.DOMoveX(_currentPosition.x - 5, 0.3f);
        _currentItem--;
        UpdateState();
    }

    private void UpdateState()
    {
        txt_Coin.text = PlayerPrefs.GetInt(Config.Coin).ToString();
        switch (_currentItem)
        {
            case -1:
                {
                    if (PlayerPrefs.GetString(ItemActive.Chespin) == State.Active)
                    {
                        txt_Key.text = "";
                        btn_Key.SetActive(false);
                    }
                    else
                    {
                        txt_Key.text = _coinChespin.ToString();
                        btn_Key.SetActive(true);
                        CompareCoin(_coinChespin);
                    }
                }
                break;
            case -2:
                {
                    if (PlayerPrefs.GetString(ItemActive.Chicken) == State.Active)
                    {
                        txt_Key.text = "";
                        btn_Key.SetActive(false);
                    }
                    else
                    {
                        txt_Key.text = _coinChicken.ToString();
                        btn_Key.SetActive(true);
                        CompareCoin(_coinChicken);
                    }
                }
                break;

            case -3:
                {
                    if (PlayerPrefs.GetString(ItemActive.Cow) == State.Active)
                    {
                        txt_Key.text = "";
                        btn_Key.SetActive(false);
                    }
                    else
                    {
                        txt_Key.text = _coinCow.ToString();
                        btn_Key.SetActive(true);
                        CompareCoin(_coinCow);
                    }
                }
                break;
            case -4:
                {
                    if (PlayerPrefs.GetString(ItemActive.Patrick) == State.Active)
                    {
                        txt_Key.text = "";
                        btn_Key.SetActive(false);
                    }
                    else
                    {
                        txt_Key.text = _coinPatrick.ToString();
                        btn_Key.SetActive(true);
                        CompareCoin(_coinPatrick);
                    }
                }
                break;
            case -5:
                {
                    if (PlayerPrefs.GetString(ItemActive.Penguin) == State.Active)
                    {
                        txt_Key.text = "";
                        btn_Key.SetActive(false);
                    }
                    else
                    {
                        txt_Key.text = _coinPenguin.ToString();
                        btn_Key.SetActive(true);
                        CompareCoin(_coinPenguin);
                    }
                }
                break;
            case -6:
                {
                    if (PlayerPrefs.GetString(ItemActive.SpongeBob) == State.Active)
                    {
                        txt_Key.text = "";
                        btn_Key.SetActive(false);
                    }
                    else
                    {
                        txt_Key.text = _coinStrongeBob.ToString();
                        btn_Key.SetActive(true);
                        CompareCoin(_coinStrongeBob);
                    }
                }
                break;
            default:
                {
                    txt_Key.text = null;
                    btn_Key.SetActive(false);
                }
                break;
        }
    }
    private void OnSwipeLeft()
    {
        if (_currentItem == 0)
        {
            return;
        }

        _currentPosition = new Vector3(_currentItem * 5, 0, 0);
        Item.DOMoveX(_currentPosition.x + 5, 0.3f);
        _currentItem++;
        UpdateState();
    }

    public void SelectItem()
    {
        switch (_currentItem)
        {
            case 0:
                {
                    ChoosePlayer();
                    break;
                }
            case -1:
                {
                    if (CompareCoin(_coinChespin) && PlayerPrefs.GetString(ItemActive.Chespin) != State.Active)
                    {
                        ActiveItem();
                    }

                    break;
                }
            case -2:
                {
                    if (CompareCoin(_coinChicken) && PlayerPrefs.GetString(ItemActive.Chicken) != State.Active)
                    {
                        ActiveItem();
                    }

                    break;
                }
            case -3:
                {
                    if (CompareCoin(_coinCow) && PlayerPrefs.GetString(ItemActive.Cow) != State.Active)
                    {
                        ActiveItem();
                    }

                    break;
                }
            case -4:
                {
                    if (CompareCoin(_coinPatrick) && PlayerPrefs.GetString(ItemActive.Patrick) != State.Active)
                    {
                        ActiveItem();
                    }

                    break;
                }
            case -5:
                {
                    if (CompareCoin(_coinPenguin) && PlayerPrefs.GetString(ItemActive.Penguin) != State.Active)
                    {
                        ActiveItem();
                    }

                    break;
                }
            case -6:
                {
                    if (CompareCoin(_coinStrongeBob) && PlayerPrefs.GetString(ItemActive.SpongeBob) != State.Active)
                    {
                        ActiveItem();
                    }

                    break;
                }
        }
        UpdateState();
    }


    private bool CompareCoin(int t)
    {
        if (PlayerPrefs.GetInt(Config.Coin) >= t)
        {
            btn_Key.GetComponent<Button>().image.overrideSprite = keyActive;
            btn_Key.GetComponent<Button>().enabled = true;
            return true;
        }
        else
        {
            btn_Key.GetComponent<Button>().image.overrideSprite = keyNotActive;
            btn_Key.GetComponent<Button>().enabled = false;
            return false;
        }
    }

    public void ChoosePlayer()
    {
        switch (_currentItem)
        {
            case 0:
                {
                    _chooseItems = PlayerNames.BabyDuck;
                }
                break;
            case -1:
                {
                    if (PlayerPrefs.GetString(ItemActive.Chespin) == State.Active)
                        _chooseItems = PlayerNames.Chespin;
                }
                break;
            case -2:
                {
                    if (PlayerPrefs.GetString(ItemActive.Chicken) == State.Active)
                        _chooseItems = PlayerNames.Chicken;
                }
                break;
            case -3:
                {
                    if (PlayerPrefs.GetString(ItemActive.Cow) == State.Active)
                        _chooseItems = PlayerNames.Cow;
                }
                break;
            case -4:
                {
                    if (PlayerPrefs.GetString(ItemActive.Patrick) == State.Active)
                        _chooseItems = PlayerNames.Patrick;
                }
                break;
            case -5:
                {
                    if (PlayerPrefs.GetString(ItemActive.Penguin) == State.Active)
                        _chooseItems = PlayerNames.Penguin;
                }
                break;
            case -6:
                {
                    if (PlayerPrefs.GetString(ItemActive.SpongeBob) == State.Active)
                        _chooseItems = PlayerNames.SpongeBob;
                }
                break;
            default:
                _chooseItems = PlayerNames.BabyDuck;
                break;
        }

        PlayerPrefs.SetString(Config.Player, _chooseItems);
        //Debug.Log(_chooseItems);
    }

    public void ActiveItem()
    {
        switch (_currentItem)
        {
            case -1:
                {
                    PlayerPrefs.SetString(ItemActive.Chespin, State.Active);
                    int t = PlayerPrefs.GetInt(Config.Coin);
                    t = t - _coinChespin;
                    PlayerPrefs.SetInt(Config.Coin, t);
                }
                break;
            case -2:
                {
                    PlayerPrefs.SetString(ItemActive.Chicken, State.Active);
                    int t = PlayerPrefs.GetInt(Config.Coin);
                    t = t - _coinChicken;
                    PlayerPrefs.SetInt(Config.Coin, t);
                }
                break;
            case -3:
                {
                    PlayerPrefs.SetString(ItemActive.Cow, State.Active);
                    int t = PlayerPrefs.GetInt(Config.Coin);
                    t = t - _coinCow;
                    PlayerPrefs.SetInt(Config.Coin, t);
                }
                break;
            case -4:
                {
                    PlayerPrefs.SetString(ItemActive.Patrick, State.Active);
                    int t = PlayerPrefs.GetInt(Config.Coin);
                    t = t - _coinPatrick;
                    PlayerPrefs.SetInt(Config.Coin, t);
                }
                break;
            case -5:
                {
                    PlayerPrefs.SetString(ItemActive.Penguin, State.Active);
                    int t = PlayerPrefs.GetInt(Config.Coin);
                    t = t - _coinPenguin;
                    PlayerPrefs.SetInt(Config.Coin, t);
                }
                break;
            case -6:
                {
                    PlayerPrefs.SetString(ItemActive.SpongeBob, State.Active);
                    int t = PlayerPrefs.GetInt(Config.Coin);
                    t = t - _coinStrongeBob;
                    PlayerPrefs.SetInt(Config.Coin, t);
                }
                break;
        }
    }


    public void LoadPlayScene()
    {
        ChoosePlayer();
        SceneManager.LoadScene(SceneName.Level1);
    }

    public void LoadIntroScene()
    {
        SceneManager.LoadScene(SceneName.Intro);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}