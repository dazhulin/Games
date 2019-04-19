/**
 *  Title:      快乐消消乐
 *          
 *              列
 *
 *  Description:
 *              布局和管理其中一列棋子
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
using System.Collections.Generic;   // 泛型集合命名空间
using UnityEngine;

public class Columns : MonoBehaviour {
    public int IntCurrentColumnNumber = -999;    // 当前列编号  
    internal List<Chess> liChessArray = new List<Chess>(); // 当前列包含的棋子集合
    internal int intNeedAddingChessNumber = 0;   // 需要补充增加棋子的数量（消除之后）
    
    // Use this for initialization
    void Start () {
        // 初始化当前列UI界面
        // row表示行
        for (int row = 0; row < GameManager.Instance.IntRowNumber; row++) 
        {
            // 得到预设
            GameObject prefabsObj = GameManager.Instance.PrefabsArray[Random.Range(0,6)];
            // 克隆预设
            // 传入列边距参数GameManager.Instance.FloColumnSpace
            GameObject cloneObj = Instantiate(prefabsObj,new Vector3(IntCurrentColumnNumber*GameManager.Instance.FloColumnSpace,-row, prefabsObj.transform.position.z),Quaternion.identity);
            // 父子对象关系
            cloneObj.transform.parent = this.transform;
            // 确定棋子大小
            cloneObj.transform.localScale = new Vector3(GameManager.Instance.FloChessScale, GameManager.Instance.FloChessScale, GameManager.Instance.FloChessScale);
            // 把克隆的棋子存入集合
            liChessArray.Add(cloneObj.GetComponent<Chess>());
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // 分配邻居
    internal void AssignNeighbor()
    {
        for (int row = 0; row < liChessArray.Count; row++)
        {
            // 左
            if(IntCurrentColumnNumber == 0)
            {
                // 左邻居为空
                liChessArray[row].chessNeighbor[0] = null;
            }
            else
            {
                liChessArray[row].chessNeighbor[0] = 
                    ColumnsManager.Instance.colArray[IntCurrentColumnNumber - 1].liChessArray[row];
            }
            // 右
            if (IntCurrentColumnNumber == ColumnsManager.Instance.colArray.Length-1)
            {
                // 右邻居为空
                liChessArray[row].chessNeighbor[1] = null;
            }
            else
            {
                liChessArray[row].chessNeighbor[1] =
                    ColumnsManager.Instance.colArray[IntCurrentColumnNumber + 1].liChessArray[row];
            }
            // 上
            if (row == 0)
            {
                // 上邻居为空
                liChessArray[row].chessNeighbor[2] = null;
            }
            else
            {
                liChessArray[row].chessNeighbor[2] = liChessArray[row-1];
            }
            // 下
            if (row == liChessArray.Count-1)
            {
                // 下邻居为空
                liChessArray[row].chessNeighbor[3] = null;
            }
            else
            {
                liChessArray[row].chessNeighbor[3] = liChessArray[row+1];
            }
        }
    }

    // 增加新的棋子（棋盘顶部），每一列
    internal void AddNewChessByCurrentColumns()
    {
        // i表示增加的棋子的数量
        for (int i = 1; i <= intNeedAddingChessNumber; i++)
        {
            // 得到预设，先全部定义0下标，为了测试
            GameObject prefabsObj = GameManager.Instance.PrefabsArray[Random.Range(0, 6)
]; // Random.Range(0, 6)
            // 克隆预设
            // 传入列边距参数GameManager.Instance.FloColumnSpace
            GameObject cloneObj = Instantiate(prefabsObj, new Vector3(IntCurrentColumnNumber * GameManager.Instance.FloColumnSpace, i, prefabsObj.transform.position.z), Quaternion.identity);
            // 父子对象关系
            cloneObj.transform.parent = this.transform;
            // 确定棋子大小
            cloneObj.transform.localScale = new Vector3(GameManager.Instance.FloChessScale, GameManager.Instance.FloChessScale, GameManager.Instance.FloChessScale);
            // 把克隆的棋子存入集合
            //liChessArray.Add(cloneObj.GetComponent<Chess>());
            // 在集合的最顶端加入棋子
            liChessArray.Insert(0, cloneObj.GetComponent<Chess>());
        }
    }

    // 新的棋子下落动画处理
    internal void PlayNewChessDropDown()
    {
        for (int i = 0; i < GameManager.Instance.IntRowNumber; i++)
        {
            Chess chessObj = liChessArray[i];
            iTween.MoveTo(chessObj.gameObject
                ,new Vector3(IntCurrentColumnNumber * GameManager.Instance.FloColumnSpace, -i, GameManager.Instance.PrefabsArray[0].transform.position.z)
                ,1F);
        }
        // 需要补充增加棋子的数量置0
        intNeedAddingChessNumber = 0;
    }
}
