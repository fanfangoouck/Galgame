using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public Image imgBG;
    public Image imgCharacter;
    public Image imgCharacter1; // 第二个人物
    // public Vector2[] characterPosition;
    public Transform[] characterPosTrans;

    public Text textName;
    public Text textLine;
    public GameObject talkLineGo; //  对话框父对象游戏物体
    public GameObject empChoiceUIGo; //选择对话框父对象游戏物体
    public GameObject[] choiceUIGos; // 所有事件对话框
    public Text[] textChoiceUIs; //事件选项，要展示的text内容
    public Text favorText;// 好感度
    public Text energyText;//能量值
    private bool toNextScene;//是否开始遮罩动画
    private bool showMask;//显示或者隐藏遮罩
    public Image mask; //遮照
    private List<UIInfo> imageTweenList;//目前需要做UI动画渐隐渐现的图片对象


    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        imageTweenList = new List<UIInfo>();
    }

    // Update is called once per frame


    public void SetBGImageSprite(string spriteName)
    {
        //从根目录找到这个图片，设为sprite, 然后展示
        imgBG.sprite = Resources.Load<Sprite>("Sprites/BG/" + spriteName);
    }

    public void ShowCharacter(string name, int characterID)
    {
        //对话框父对象游戏物体
        CloseChoiceUI();
        //ShowOrHideTalkLine(false);
        textName.text = name;

        Debug.Log("走到这里，看看id" + characterID);

        if (characterID == 0)
        {
            Child_ShowCharacter(name, imgCharacter);
        }

        else
        {
            Debug.Log("走到了Child_ShowCharacter这一步，是debug");
            Child_ShowCharacter(name, imgCharacter1);
        }
    }

    public void Child_ShowCharacter(string name, Image character)
    {
        //加载人物图片
        character.sprite = Resources.Load<Sprite>("Sprites/Characters/" + name);
        //设为图片原始大小
        character.SetNativeSize();
        Debug.Log("走到了character.gameObject.SetActive(true)这一步");
        character.gameObject.SetActive(true);

        // imgCharacter.rectTransform.transform.Translate(1, 1, 1);
    }


    public void UpdateTalkLineText(string dialogContent)
    {
        ShowOrHideTalkLine();
        textLine.text = dialogContent;
    }

    public void ChangeCharacterPosition(int posId, int characterID, bool ifRotate)
    {

        if (characterID == 0)
        {
            Child_ChangeCharacterPosition(posId, imgCharacter, ifRotate);
        }

        else
        {
            Child_ChangeCharacterPosition(posId, imgCharacter1, ifRotate);
        }

    }

    public void Child_ChangeCharacterPosition(int posId, Image character, bool ifRotate = false)
    {
        Debug.Log("先反转");
        if (ifRotate)
        {
            character.transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            character.transform.eulerAngles = Vector3.zero;
        }
        Debug.Log("再设位置");
         character.transform.localPosition = characterPosTrans[posId].localPosition;
    }

    //更改能量

    public void UpdateEnergyValue(int value = 0)
    {

        energyText.text = value.ToString();
    }

    public void UpdateFavorValue(int value = 0, string name = null)
    {
        favorText.text = value.ToString();
    }



    //显示选择对话框
    /// <param name="choice">选择数量</param>
    /// <param name="choiceContent">每一项内容</param>
    public void ShowChoiceUI(int choice, string[] choiceContent)
    {
        //这里会改spriteIndex
        Debug.Log("走到了empChoiceUIGo.SetActive(true)");
        empChoiceUIGo.SetActive(true);


        ShowOrHideTalkLine(false);


        //先把所有选项框都隐藏
        for (int i = 0; i < choiceUIGos.Length; i++)
        {
            choiceUIGos[i].SetActive(false);

        }

        //选择要显示的对话框
        for (int i = 0; i < choice; i++)
        {
            choiceUIGos[i].SetActive(true);
            textChoiceUIs[i].text = choiceContent[i];
        }


    }

    /// <summary>
    /// 关闭事件选择
    /// </summary>
    public void CloseChoiceUI()
    {
        empChoiceUIGo.SetActive(false);
    }

    /// <summary>
    /// 默认显示对话框
    /// </summary>
    /// <param name="show"></param>

    public void ShowOrHideTalkLine(bool show = true)
    {

        talkLineGo.SetActive(show);
    }

    public void ShowOrHideMask(bool show = true)
    {
        //toNextScene = true;
       // showMask = show;
       
        DoShowOrHideUITween(show, true, 1, mask);

    }

    public void ShowOrHideCharacter(bool show = true, int characterID = 0)
    {
        if(characterID == 0)
        {
            DoShowOrHideUITween(show, true, 1, imgCharacter);
        }
        else
        {
            DoShowOrHideUITween(show, true, 1, imgCharacter1);
        }
        
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="show">隐藏还是展示mask</param>
    /// <param name="ifLoadNext">是否加载下一个剧本</param>
    /// <param name="interval">多少秒显示完</param>
    /// <param name="images">哪个图片</param>
    public void DoShowOrHideUITween(bool show, bool ifLoadNext, float interval, params Image[] images)
    {
        toNextScene = true; // 这个变量不在UIInfo类里，单独设置

        // 这个变量也不在UIInfo类里，单独设置.代表 。
        //info.percent += info.lerpSpeed * Time.deltaTime; 是最终目标值
        //show? percent = 1.0f : percent = 0; 怎么改成这样啊
        float percent;
        if (show)
        {
            //初始值是0，从0变成1 showmask
            percent = 0;
        }
        else
        {
            percent = 1;
        }

        //生成每一个图片的属性。如果是mask 或者character， 就是1个图片
        for(int i = 0; i < images.Length; i++)
        {
            imageTweenList.Add(new UIInfo()
                { show = show, imageTween = images[i], ifLoadNext = ifLoadNext, lerpSpeed = 1 / interval, percent = percent });
        }
   
    }

    private void ShowMask(UIInfo info)
    {
        //透明度按percent 升值
        Debug.Log("ShowMask函数内");
        info.percent += info.lerpSpeed * Time.deltaTime;
        info.imageTween.color = new Color(info.imageTween.color.r, info.imageTween.color.g, info.imageTween.color.b,
                info.percent);
        if (info.imageTween.color.a >= 0.9f)
        {
            //保证透明度变成1
            Debug.Log("保证透明度变成1");
            info.imageTween.color = new Color(mask.color.r, mask.color.g, mask.color.b, 1);


            Debug.Log("ifLoadNext之前");
            if (info.ifLoadNext)
            {
                Debug.Log("进入LoadNext判断");
                GameManager.Instance.LoadNextScript();
            }

            //移除当前info
            //当列表里没有info对象了，再停止动画
            imageTweenList.Remove(info);
            if (imageTweenList.Count <= 0)
            {
                toNextScene = false;
            }
        }
    }

    public void HideMask(UIInfo info)
    {
        //透明度按percent 降
        info.percent -= info.lerpSpeed * Time.deltaTime;
        info.imageTween.color = new Color(info.imageTween.color.r, info.imageTween.color.g, info.imageTween.color.b,
                info.percent);
        if (info.imageTween.color.a <= 0.1f)
        {
            //保证透明度变成1
            info.imageTween.color = new Color(mask.color.r, mask.color.g, mask.color.b, 0);

            if (info.ifLoadNext)
            {
                GameManager.Instance.LoadNextScript();
            }

            //移除当前info
            //当列表里没有info对象了，再停止动画
            imageTweenList.Remove(info);
            if (imageTweenList.Count <= 0)
            {
                toNextScene = false;
            }
        }
    }

    public void Update()
    {
        if (toNextScene)
        {

            for (int i = 0; i < imageTweenList.Count; i++)
            {
                if (imageTweenList[i].show)
                {
                    Debug.Log("进入到ShowMask()");
                    ShowMask(imageTweenList[i]);
                }
                else
                {
                    HideMask(imageTweenList[i]);
                }
            }
        }
    }
  
}

public class UIInfo
{
    public bool show;   //展示动画还是隐藏动画 0 隐藏，1展示
    public Image imageTween; // 单个图片
    public bool ifLoadNext; //是否加载下一个剧本
    public float percent; 
    public float lerpSpeed;
}
