/**
 *  Title:      快乐消消乐
 *          
 *              列管理器
 *
 *  Description:
 *              通过操作列和棋子，最终起到管理整个棋盘的目的。
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

public class ColumnsManager : MonoBehaviour {
    public static ColumnsManager Instance;          // 静态实例
    internal Columns[] colArray = new Columns[8];   // 列数组

    public void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start () {
        // 初始化棋盘界面
        for (int i = 0; i < colArray.Length; i++)
        {
            GameObject newObj = new GameObject();
            colArray[i] = newObj.AddComponent<Columns>();
            // 确立父子关系
            newObj.transform.parent = this.transform;
            // 给列起名字
            newObj.name = "Columns_" + i;
            // 给列编号
            colArray[i].IntCurrentColumnNumber = i;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // 分配邻居
    internal void AssignNeighbor()
    {
        for (int col = 0; col < colArray.Length; col++)
        {
            // 每一列分配邻居
            colArray[col].AssignNeighbor();
        }
    }
}
