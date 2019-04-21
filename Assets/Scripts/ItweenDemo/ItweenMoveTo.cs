/**
 *  Title:      学习Itween脚本插件的基本使用
 *          
 *              
 *
 *  Description:
 *              
 *  Date:2019/4
 *
 *  Author:     何进
 *
 *  Version:    1.0
 *
 *  Modify Recorder:
 *
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItweenMoveTo : MonoBehaviour {
    public GameObject goHero;               // 移动的对象
    public Transform trTargetPosition;      // 移动到的目标方位

	// Use this for initialization
	void Start () {
        DisplayMoveToMethod();
    }
	
	// Update is called once per frame
	void Update () {

    }

    void DisplayMoveToMethod()
    {
        // 第一种写法
        //iTween.MoveTo(goHero.gameObject,trTargetPosition.position,2F);
        // 第二种写法
        iTween.MoveTo(goHero, iTween.Hash(
            "position", trTargetPosition.position,  // 位置
            "time", 3F,                             // 移动时间
            "speed", 5F,                            // 速度
            "delay", 0.5F,                            // 延迟
            "easeType", iTween.EaseType.bounce,     // 运行的模式
            "looptype", iTween.LoopType.none,       // 是否循环，循环方式
            "oncomplete", "TestMethod",             // 完成时调用方法
            "oncompletetarget",this.gameObject,
            "onstart", "TestMethod_Start",          // 一开始就调用方法
            "onstarttarget",this.gameObject,
            "onupdate", "TestMethod_Update",        // 不断调用方法
            "onupdatetarget",this.gameObject
            ));
    }

    void TestMethod()
    {
        print("我是运行的方法1(演示的方法是oncomplete)");
    }

    void TestMethod_Start()
    {
        print("我是运行的方法2(一开始就运行，演示的方法是onstart)");
    }

    void TestMethod_Update()
    {
        print("我是运行的方法3(我是每一帧都执行，演示的方法是onupdate)");
    }

}
