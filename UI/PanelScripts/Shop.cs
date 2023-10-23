using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

[System.Serializable]
public class SpriteToTower
{
    [SerializeField] public Sprite mySprite;
    [SerializeField] public CS_TowerData myTowerData;
}

public class Shop : MonoBehaviour, IPointerDownHandler
{
    private static Shop instance = null;
    public static Shop Instance { get { return instance; } }
    [SerializeField] public Button[] towerbuttons;
    [SerializeField] public Button itemshop;
    [SerializeField] public Button reBtn;
    [SerializeField] public Sprite[] sprites;
    [SerializeField] public Sprite[] itemsprites;
    [SerializeField] public Sprite[] updatedsprites;
    [SerializeField] public Sprite tempsprite;
    [SerializeField] public Sprite ptempsprite;
    [SerializeField] public Button[] prepareBtns;
    [SerializeField] public GameObject[] prepareBgs;
    [SerializeField] public Button[] itemBtns;
    [SerializeField] public Button clickBtn;
    [SerializeField] public int[] itemamout = { 1, 1, 1, 1 };
    [SerializeField] Button sellBtn;
    [SerializeField] public bool[] preparestates = { false, false, false, false, false };
    //这三个是记录点击的物体编号用的
    [SerializeField] public int P_mouseclickindex;
    [SerializeField] public int S_mouseclickindex;
    [SerializeField] public int I_mouseclickindex;
    [SerializeField] public SpriteToTower[] sToT;
    [SerializeField] public CS_TowerData itemData0, itemData1;
    string prebg = "UI/button_white";
    bool pres = false;
    //normal道具
    int aBottledfire = 0;
    int aBottledlightning = 0;
    int aBottledstorm = 0;
    int astone = 0;
    float[] tower_cost = { 1000, 2000, 3000, 3000, 4500 };
    [SerializeField] public TMP_Text displayCost;
    [SerializeField] public TMP_Text[] itemamounts;
    float curCost;
    int curitem;//当前售卖的item编号
    int count = 0;
    [SerializeField] public GameObject[] itemBgs;
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }
    public bool isClicked = false; // 添加一个bool类型的变量
    private bool hasClicked = false;
    public void OnPointerDown(PointerEventData eventData)
    {
        if (Input.GetMouseButtonDown(0) && !hasClicked)
        {
            isClicked = true;
            hasClicked = true;
        }
    }
    void Update()
    {
        disCost();
        OnPointerDown(new PointerEventData(EventSystem.current));
        if (hasClicked)
        {
            prepareBgs[0].SetActive(false); prepareBgs[1].SetActive(false); prepareBgs[2].SetActive(false); prepareBgs[3].SetActive(false); prepareBgs[4].SetActive(false);
            itemBgs[0].SetActive(false); itemBgs[1].SetActive(false); itemBgs[2].SetActive(false); itemBgs[3].SetActive(false);
            // P_mouseclickindex = -1;
            I_mouseclickindex = -1;
            hasClicked = false;
            CS_BuildingManager.Instance.selectedTowerData = null;
        }
    }
    void Start()
    {
        // 为每个按钮添加点击事件的监听器
        for (int i = 0; i < towerbuttons.Length; i++)
        {
            int index = i; // 创建一个局部变量保存当前循环变量的值
            towerbuttons[i].onClick.AddListener(() => BuyButtonClick(towerbuttons[index]));
        }
        for (int i = 0; i < prepareBtns.Length; i++)
        {
            int index = i; // 创建一个局部变量保存当前循环变量的值
            prepareBtns[i].onClick.AddListener(() => prepareBtnClick(prepareBtns[index]));
        }
        for (int i = 0; i < itemBtns.Length; i++)
        {
            int index = i; // 创建一个局部变量保存当前循环变量的值
            itemBtns[i].onClick.AddListener(() => itemslotclick(itemBtns[index]));
        }
        if (itemshop != null)
            itemshop.onClick.AddListener(Buyitem);
        if (sellBtn != null)
            sellBtn.onClick.AddListener(sell);
        if (reBtn != null)
            reBtn.onClick.AddListener(ReButtonClick);
        CS_GameManager.Instance.getCost(2500f); disCost();
        ReButtonClick();
    }
    void Buyitem()
    {
        isClicked = false;
        if (itemshop.image.sprite != Resources.Load<Sprite>(prebg))
        {
            itemshop.image.sprite = Resources.Load<Sprite>(prebg);
            itemamout[curitem] += 1;
            itemamounts[curitem].text = "x" + itemamout[curitem];
            curitem = 0;
        }
    }
    void BuyButtonClick(Button button)
    {
        isClicked = false;
        prestate();
        S_mouseclickindex = System.Array.IndexOf(towerbuttons, button);
        if (pres) return;
        else if (towerbuttons[S_mouseclickindex].image.sprite != Resources.Load<Sprite>(prebg))
        {
            if (curCost > tower_cost[S_mouseclickindex])
            {
                CS_GameManager.Instance.getCost(-tower_cost[S_mouseclickindex]); disCost();
                tempsprite = button.image.sprite;
                towerbuttons[S_mouseclickindex].image.sprite = Resources.Load<Sprite>(prebg);
                shopToPrepare();
                towerUpdate();
            }
        }
    }
    void ReButtonClick()
    {
        isClicked = false;
        if (CS_GameManager.Instance.myCost() >= 2500f)
        {
            CS_GameManager.Instance.getCost(-2500f);
            disCost();
            foreach (Button button in towerbuttons)
            {
                // 随机选择要显示的Sprite
                int randomIndex = Random.Range(0, sprites.Length);
                Sprite randomSprite = sprites[randomIndex];

                // 将Sprite赋值给Button的Image组件
                if (randomSprite != null)
                {
                    button.image.sprite = randomSprite;
                }
            }
            if (itemsprites.Length == 0) return;
            int itemIndex = Random.Range(0, itemsprites.Length);
            Sprite itemsprite = itemsprites[itemIndex];
            if (itemsprite != null)
            {
                itemshop.image.sprite = itemsprite;
                curitem = itemIndex;
            }
        }
    }
    void shopToPrepare()
    {
        isClicked = false;
        prestate();
        if (pres) return;
        else
        {
            int index = 0;
            for (int v = index; v < 5; v++)
            {
                if (!preparestates[v])
                {
                    prepareBtns[v].image.sprite = tempsprite;
                    preparestates[v] = true;
                    break;
                }
            }
        }
        prestate();
    }
    void prepareBtnClick(Button button)
    {
        isClicked = false;
        P_mouseclickindex = System.Array.IndexOf(prepareBtns, button);
        ptempsprite = button.image.sprite;
        prepareBgs[0].SetActive(false); prepareBgs[1].SetActive(false); prepareBgs[2].SetActive(false); prepareBgs[3].SetActive(false); prepareBgs[4].SetActive(false);
        prepareBgs[P_mouseclickindex].SetActive(true);
        prepareToBuilding();
    }
    void sell()
    {
        isClicked = false;
        prepareBtns[P_mouseclickindex].image.sprite = Resources.Load<Sprite>(prebg);
        ptempsprite = null;
        preparestates[P_mouseclickindex] = false;
        prestate();
        CS_GameManager.Instance.getCost(1000);
    }
    public void useTower()
    {
        isClicked = false;
        prepareBtns[P_mouseclickindex].image.sprite = Resources.Load<Sprite>(prebg);
        ptempsprite = null;
        preparestates[P_mouseclickindex] = false;
        prestate();
    }
    //检测pre是否已满
    void prestate()
    {
        if (preparestates[0] && preparestates[1] && preparestates[2] && preparestates[3] && preparestates[4])
        {
            pres = true;
        }
        else
        {
            pres = false;
        }
    }

    void disCost()
    {
        curCost = CS_GameManager.Instance.myCost();
        if (displayCost != null)
            displayCost.text = "" + curCost;
    }
    //使用道具
    public void useitem()
    {
        if (I_mouseclickindex == 0 || I_mouseclickindex == 1 || I_mouseclickindex == 2 || I_mouseclickindex == 3)
        {
            if (itemamout[I_mouseclickindex] > 0)
            {
                itemamout[I_mouseclickindex] -= 1;
                itemamounts[I_mouseclickindex].text = "x" + itemamout[I_mouseclickindex];
            }
        }
        itemBgs[I_mouseclickindex].SetActive(false);//移除背景
    }
    void itemslotclick(Button button)
    {
        I_mouseclickindex = System.Array.IndexOf(itemBtns, button);
        itemBgs[0].SetActive(false); itemBgs[1].SetActive(false); itemBgs[2].SetActive(false); itemBgs[3].SetActive(false);
        if (itemamout[I_mouseclickindex] == 0) return;
        itemBgs[I_mouseclickindex].SetActive(true);
        if (I_mouseclickindex == 0)
        {
            CS_BuildingManager.Instance.selectedTowerData = itemData0;
        }
        else if (I_mouseclickindex == 1)
        {
            CS_BuildingManager.Instance.selectedTowerData = itemData1;
        }
        else if (I_mouseclickindex == 2)
        {
            CS_BuildingManager.Instance.OnDelete();
        }
        else if (I_mouseclickindex == 3)
        {
            CS_BuildingManager.Instance.onStone();
        }
    }
    //放塔
    void prepareToBuilding()
    {
        if (sToT.Length != 0)
        {
            for (int i = 0; i < sToT.Length; i++)
            {
                if (sToT[i].mySprite == ptempsprite)
                {
                    CS_BuildingManager.Instance.selectedTowerData = sToT[i].myTowerData;
                }
            }
        }
    }
    void deleteBuilding()
    {
        CS_BuildingManager.Instance.selectedTowerData = null;
    }
    void towerUpdate()
    {
        int count = 0;
        int updatehead = 0;
        for (int i = 0; i < 5; i++)
        {
            if (prepareBtns[i].image.sprite == tempsprite)
            {
                count++;
                if (count == 1)
                {
                    updatehead = i; // 记录第一个相同图片按钮的位置
                }
                if (count == 3)
                {
                    for (int c = 0; c < 5; c++)
                    {
                        if (prepareBtns[c].image.sprite == tempsprite)
                        {
                            prepareBtns[c].image.sprite = Resources.Load<Sprite>(prebg);
                            preparestates[c] = false;
                            for (int index = 0; index < 8; index++) if (sprites[index] == tempsprite)
                                    prepareBtns[updatehead].image.sprite = updatedsprites[index];
                            preparestates[updatehead] = true;
                        }
                    }
                }
            }
        }
    }
}

