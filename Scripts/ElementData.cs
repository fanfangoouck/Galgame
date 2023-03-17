using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementData 
{
    public int loadType;//载入资源类型 1.背景 2.人物 3. 事件
    public string name;//角色名称
    public string spriteName;//图片资源路径
    public string dialogContent;//对话内容
    public int characterPos;//1.左边 2.右边 3.中间
    public bool ifRotate;//是否翻转
    public int audioType; //1. 特效音 2. 背景音 3. 对话音乐
    public string musicPath; // 音乐存放路径
    public int energy;//要改变的能量值
    public int favorValue;//要改变的喜爱值
    public int characterID;//显示哪个人物 0 test; 1 debug
    public int elementIndex; //脚本的编号

    // 处理事件
    //1. 显示选择事件  2. 跳转到指定剧本位置 3. 特殊事件
    public int eventID;

    //当 eventID = 1时，是第几个选项（ eventData=3 一共三个选择，往下数1，2，3）
    //当 eventID = 2时，是跳转到哪个剧本 scriptid
    //当 eventID = 3时，事件id（击剑啥的）
    // eventID = 5时，0 隐藏mask 1 显示mask
    public int eventData;
    public int scriptID; //  eventData - scriptID 一一对应，跳转到对应的剧本
}
