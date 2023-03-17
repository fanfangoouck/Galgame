using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private List<ElementData> firstData;

    private int elementIndex; //element index
    

    public int energy;// 精力值
    public Dictionary<string, int> favorValueDic; //好感度

    public Dictionary<int, Action<object>> eventDict;
    public GameObject hitPointGo;


    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;

        //未赋值的是默认值，string = null; int = 1;
        firstData = new List<ElementData>()
        {
            //背景
            new ElementData()
            {
                loadType = 1,spriteName = "Title",audioType = 2, musicPath = "Normal"
            },

            //MASK
              new ElementData()
            {
                  // eventID = 5, mask -- data = 1 , 隐藏mask 显示1
                  loadType = 3, eventID = 5, eventData = 0
            },

            
            new ElementData() // test进场
            {
                  // eventID = 5, mask -- data = 1 , 隐藏mask 显示1
                  //现在一定要加增加透明度这个事件，人物图片的透明度已被调到最低
                  loadType = 3, eventID = 6, eventData = 1, characterID = 0,name="Test",characterPos=2
            },
           

            //人物
             new ElementData()
            {
                loadType = 2,name = "Test",
                dialogContent = "你好，我叫Test",
                characterPos = 2,audioType = 3, musicPath = "0",
                energy = 10
            },
             //2
              new ElementData()
            {
                loadType = 2,name = "Test",
                dialogContent = "你真厉害，这么快就学会了galgame的核心思路内容",
                characterPos = 2,audioType = 3, musicPath = "1", favorValue = 20
            },
              //3
               new ElementData()
            {
                loadType = 2,name = "Test",
                dialogContent = "也学会了翻转图片呢",
                characterPos = 1,ifRotate = true
            },
               //4
              new ElementData()
            {
                loadType = 2,name = "Test",
                dialogContent = "接下来让我们认识一个新朋友吧",
                characterPos = 2
            },
              //5
               new ElementData()
            {
                audioType = 2, musicPath = "Daily"
            },

                new ElementData() // test进场
            {
                  // eventID = 5, mask -- data = 1 , 隐藏mask 显示1
                  //现在一定要加增加透明度这个事件，人物图片的透明度已被调到最低
                  loadType = 3, eventID = 6, eventData = 1, characterID = 1,name="Debug",characterPos=3
            },

               //6
                new ElementData()
            {
                loadType = 2,name = "Debug",
                dialogContent = "你好，在下是Debug",
                characterPos = 3, audioType = 3, musicPath = "0", favorValue = 20, ifRotate = true,characterID = 1
            },
                //7
                 new ElementData()
            {
                loadType = 2,name = "Test",
                dialogContent = "呦，你来啦",
                characterPos = 2
            },
                 //8
                new ElementData()
            {
                loadType = 2,name = "Debug",
                dialogContent = "想和我一起击剑吗",
                characterPos = 3,  favorValue = 20, ifRotate = true,characterID = 1
            },

            //开始事件部分
            //9
              new ElementData()
            {
                loadType = 3, eventID = 1, eventData = 3,scriptID = 4
            },
              //10
                new ElementData()
            {
                loadType = 3,name = "Debug",
                dialogContent = "我拒绝，我害怕",
                eventID = 2, eventData = 1
            },
                //11
                  new ElementData()
            {
                loadType = 3,name = "Debug",
                dialogContent = "有什么奖励吗",
                eventID = 2, eventData = 2
            },
                  //12
                  new ElementData()
            {
                loadType = 3,name = "Debug",
                dialogContent = "好的，来吧！",
                eventID = 2, eventData = 3
            },
                  //跳转到对应结果
                  //13

            
                new ElementData()
            {
                loadType = 2,name = "Debug",
                dialogContent = "来勇敢尝试一下吧", //事件一的返回结果
                characterPos = 3,  ifRotate = true,characterID = 1,
                scriptID = 1 //eventdata = 1 的转到这
            },
                  
                 //点击上一个剧本，scriptIndex++, handleData执行这个剧本
                  new ElementData()
            {
                loadType = 3,
                eventID = 2, eventData = 4,//转回选项
            },

                  new ElementData()
            {
                loadType = 2,name = "Debug",
                dialogContent = "好感度会大大增加哦",//事件二的返回结果
                characterPos = 3,  ifRotate = true,characterID = 1,
                scriptID = 2 //eventdata = 1 的转到这
            },

                  new ElementData()
            {
                loadType = 3,
                eventID = 2, eventData = 4,//转回选项
            },

                  //15
                  new ElementData()
            {
                loadType = 2,name = "Debug",
                dialogContent = "那我们现在就开始",//事件三的返回结果
                characterPos = 3,  ifRotate = true,characterID = 1,
                scriptID = 3
            },

                  //开始射击
                   new ElementData()
            {
                loadType=3,eventID=4,eventData=1
            },

                   new ElementData()//失败时需要跳转的剧情位置,往下跳一位
            {
                loadType=3,eventID=2,eventData=5
            },
                   new  ElementData()//胜利时需要跳转的剧情位置，往下跳两位
            {
                loadType=3,eventID=2,eventData=6
            },

           new ElementData()
            {
                 loadType=2,name="Debug",characterPos=1,audioType = 1, musicPath ="5",ifRotate=true,characterID=1
                ,dialogContent="阁下的剑术不是很精进，还需要多加努力",scriptID=5
            },

            new ElementData()
            {
                loadType=3,eventID=2,eventData=7
            },
                   new ElementData()
            {
                 loadType=2,name="Debug",characterPos=1,audioType = 1, musicPath ="6", ifRotate=true,characterID=1
                ,dialogContent="阁下的剑术果然厉害！",scriptID=6
            },
                   new ElementData()
            {
                loadType=3,eventID=2,eventData=7
            },
                   new ElementData()
            {
                 loadType=2,name="Debug",characterPos=1,audioType = 1, musicPath ="7",ifRotate=true,characterID=1
                ,dialogContent="那么在下告退，期待下次与阁下见面",scriptID=7
            },

             //MASK
              new ElementData()
            {
                  // eventID = 5, mask -- data = 1 ,显示0 隐藏1
                  loadType = 3, eventID = 5, eventData = 1
            },






        }; //注意这个分号，是ElementData = new List<ElementData>()； 的分号

      
        elementIndex = 0;
        //开始处理脚本
        HandleData();
        //初始化精力
        energy = 100;
        ChangeEnergyValue();

        //初始化好感值。这个格式得背一下
        favorValueDic = new Dictionary<string, int>()
        {
            {"Player",0 },
            {"Test",50 },
            {"Debug",50 }

        };


        //背一下这些格式把。。。。 (), 分号
        eventDict = new Dictionary<int, Action<object>>()
        {
            {1, StartHitPointEvent}
        };

        for (int i = 0; i < firstData.Count; i++)
        {
            firstData[i].elementIndex = i;
        }
       
    }




    private void HandleData()
    {
        if(elementIndex >= firstData.Count)
        {
            Debug.Log("game over");
            return;
        }

        PlaySound(firstData[elementIndex].audioType);

        if (firstData[elementIndex].loadType == 1)
        {
            // 背景
            //设置背景图
            SetBackground(firstData[elementIndex].spriteName);
            // PlaySound(firstData[elementIndex].audioType, firstData[elementIndex].musicPath);
            
            LoadNextScript();
        }

        //剩下情况不做处理（没有角色）
        else if(firstData[elementIndex].loadType == 2)
        {

            HandlerCharacter();

        }

        else if (firstData[elementIndex].loadType == 3)
        {

            switch (firstData[elementIndex].eventID)
            {
                case 1:
                    ShowChoiceUI(firstData[elementIndex].eventData, GetChoiceContent(firstData[elementIndex].eventData));
                    break;
                case 2:
                    SetSciptID();
                    break;
                case 4:
                    //loadType=3,eventID=4,eventData=1 射击
                    eventDict[firstData[elementIndex].eventData](null);
                    break;
                case 5:
                    ShowOrHideMask(firstData[elementIndex].eventData);
                    break;
                case 6:
                    ShowOrHideCharacter(firstData[elementIndex].eventData, firstData[elementIndex].characterID);
                    HandlerCharacter(true);
                   // LoadNextScript();
                    break;
                default:
                    break;
            }

        }
        else
        {
            LoadNextScript();
        }
    }

    //设置背景图片
    private void SetBackground(string  spriteName)
    {
        UIManager.Instance.SetBGImageSprite(spriteName);
    }

    //加载下一条剧情数据
    public void LoadNextScript(int addNum = 1)
    {
        Debug.Log("LoadNextScript的之前elemengindex " + elementIndex);
        elementIndex += addNum;
        Debug.Log("LoadNextScript的之后elemengindex " + elementIndex);

        HandleData(); 
    }

    //展示人物
    private void ShowCharacter(string name, int characterID = 0)
    {
        UIManager.Instance.ShowCharacter(name,characterID);
    }

    //展示对话框
    private void UpdateTalkLineText(string dialogContent)
    {
        UIManager.Instance.UpdateTalkLineText(dialogContent);
    }

    
   //变化位置
   //初始化很重要。防止loadtype = 2， handlecharacter时没有传入参数，报错
    private void ChangeCharacterPosition(int characterPos, int characterID = 0, bool ifRotate = false)
    {
        UIManager.Instance.ChangeCharacterPosition(characterPos, characterID, ifRotate);
    }

    // 音乐

    public void PlaySound(int soundType)
    {
        switch (soundType)
        {
            case 1:
                AudioManager.Instance.PlaySound(firstData[elementIndex].musicPath);
                break;
            case 2:
                AudioManager.Instance.PlayMusic(firstData[elementIndex].musicPath);
                break;
            case 3:
                AudioManager.Instance.PlayDialog(firstData[elementIndex].name + "/" + firstData[elementIndex].musicPath);
                break;
            default:
                break;
        }
     }


    //改变生命值
    public void ChangeEnergyValue(int value = 0)
    {
        if (value == 0) return;
        if (value > 0) AudioManager.Instance.PlaySound("Energy");

        energy = Mathf.Clamp(energy + value, 0, 100);
        Debug.Log("energy 变成了" + energy);
        UpdateEnergyValue(energy);

    }


    // 更新生命值
    public  void UpdateEnergyValue(int value = 0)
    {
        UIManager.Instance.UpdateEnergyValue(value);
    }


    //改变好感度
    public void ChangeFavorValue(int value, string name)
    {
        if (value == 0) return;
        if (value > 0) AudioManager.Instance.PlaySound("Favor");

        favorValueDic[name] = Mathf.Clamp(favorValueDic[name] + value, 0, 100);
        Debug.Log("喜爱度变成了" + favorValueDic[name]);
        UpdateFavorValue(favorValueDic[name],name); //不传name 行不行？

    }


    // 更新好感度
    public void UpdateFavorValue(int value = 0,string name = null)
    {
        UIManager.Instance.UpdateFavorValue(value,name);
    }


    public void HandlerCharacter(bool onlyCharacter = false)
    {
        //变换角色位置
        Debug.Log("ChangeCharacterPosition");
        ChangeCharacterPosition(firstData[elementIndex].characterPos, firstData[elementIndex].characterID, firstData[elementIndex].ifRotate);
       
        //角色
        Debug.Log(" ShowCharacter");
        ShowCharacter(firstData[elementIndex].name, firstData[elementIndex].characterID);
   

        if (!onlyCharacter)
        {
            //PlaySound(firstData[elementIndex].audioType, firstData[elementIndex].musicPath);
            //PlaySound(firstData[elementIndex].audioType);
            ChangeEnergyValue(firstData[elementIndex].energy); // 改变的能量值
            ChangeFavorValue(firstData[elementIndex].favorValue, firstData[elementIndex].name); // 改变的喜爱度 - 角色
            UpdateTalkLineText(firstData[elementIndex].dialogContent);//展示对话框
        }
        
    }


    /// <summary>
    /// 显示多项事件选择对话框
    /// </summary>
    /// <param name="choice"></param>
    /// <param name="choiceContent"></param>
    public void ShowChoiceUI(int choice, string[] choiceContent)
    {
        Debug.Log("ShowChoiceUI的之前elemengindex " + elementIndex);
        UIManager.Instance.ShowChoiceUI(choice, choiceContent);
        Debug.Log("ShowChoiceUI的之后elemengindex " + elementIndex);
    }

    /// <summary>
    /// 获得当前选择项上的文本
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    public string[] GetChoiceContent(int num)
    {
        string[] choiceContent = new string[num];
        Debug.Log("生成选项内容前" + elementIndex);

        for (int i = 0; i < num; i++)
        {
            choiceContent[i] = firstData[elementIndex + 1 + i].dialogContent;
        }
        Debug.Log("生成选项内容后" + elementIndex);
        return choiceContent;
    }

    /// <summary>
    /// 跳转到对应剧本
    /// </summary>
    /// <param name="eventData">剧本对应id</param>

    public void SetSciptID(int index = 0)
    {
        for (int i = 0; i < firstData.Count; i++)
        {
           if (firstData[elementIndex + index].eventData == firstData[i].scriptID)
            {
                Debug.Log("SetSciptID里此时的elementIndex" + elementIndex);
                elementIndex = firstData[i].elementIndex;
                //elementIndex = i;
                Debug.Log("SetSciptID里改变后的elementIndex" + elementIndex);
                break;
            }
        }
        //处理新的对应剧本
       // UIManager.Instance.CloseChoiceUI();
        Debug.Log("CloseChoiceUI()之后, 要进入HandleData 的elementIndex" + elementIndex);
        HandleData();
    }

    //背一下，为啥传入的是object 
    public void StartHitPointEvent(object src)
    {
        UIManager.Instance.ShowOrHideTalkLine(false);
        hitPointGo.SetActive(true);
    }


    public void ShowOrHideMask(int hideOrShow)
    {
        if(hideOrShow == 0)
        {
            Debug.Log("走到这里了UIManager.Instance.ShowOrHideMask(false);" );
            //最后没有mask
            UIManager.Instance.ShowOrHideMask(false);
        }

        else if (hideOrShow == 1)
        {
            // percent 变成 1.0f;
            //最后有mask
            UIManager.Instance.ShowOrHideMask(true);
        }
    }


    public void ShowOrHideCharacter(int hideOrShow, int characterID)
    {
        
        if (hideOrShow == 0)
        {
            UIManager.Instance.ShowOrHideCharacter(false, characterID);
        }

        else if (hideOrShow == 1)
        {
            UIManager.Instance.ShowOrHideCharacter(true , characterID);
        }
    }

    public void DoShowOrHideUITween(bool show, bool ifLoadNext, float interval, params UnityEngine.UI.Image[] images)
    {
        UIManager.Instance.DoShowOrHideUITween(show, ifLoadNext, interval, images);
    }

    /*
     
     */


    /*
    public void PlaySound(int soundType, string path)
    {
        switch (soundType)
        {
            case 1:
                AudioManager.Instance.PlaySound(path);
                break;
            case 2:
                AudioManager.Instance.PlayMusic(path);
                break;
            case 3:
                AudioManager.Instance.PlayDialog(firstData[elementIndex].name + "/" + path);
                break; 
            default:
                break;
        }
    */


}


