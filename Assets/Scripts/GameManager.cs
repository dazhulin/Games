/**
 *  Title:      快乐消消乐
 *          
 *              游戏管理器
 *
 *  Description:
 *              作用：存储系统“预设”和全局参数
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

public class GameManager : MonoBehaviour {
    public static GameManager Instance;         // 静态实例

    public GameObject[] PrefabsArray;             // 预设数组
    public int IntRowNumber = 5;                // 当前场景行数
    public float FloColumnSpace = 1F;           // 棋盘列间距
    public float FloChessScale = 1F;            // 棋子尺寸

    public void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
