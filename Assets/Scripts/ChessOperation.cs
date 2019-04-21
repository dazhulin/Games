/**
 *  Title:      快乐消消乐
 *          
 *              棋子操作管理器
 *
 *  Description:
 *              动态管理棋子
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

public class ChessOperation : MonoBehaviour {
    public static ChessOperation Instance;        // 静态实例
    internal bool IfExistBurstItem = false;       // 当前棋盘是否可以存在消除项
    internal Chess chessItem1;                    // 用户选择的第1个棋子 
    internal Chess chessItem2;                    // 用户选择的第2个棋子

    public void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start () {
        // 检测当前棋盘是否进行消除
        StartCoroutine("CheckIfCanBurst");
        // 测试
        //Invoke("TestAssignNeighbor", 1F);  // 1秒延迟调用
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator CheckIfCanBurst()
    {
        yield return new WaitForSeconds(0.2F);    // 0.5秒延迟调用
        // 棋盘分配邻居
        AssignNeighbor();

        // 检测当前棋盘是否可以存在消除项
        CheckIfExistBurstItems();
        yield return new WaitForSeconds(0.2F);
        if (IfExistBurstItem)
        {
            // 删除当前棋盘可以消除的棋子
            DestoryChessIfCanBurst();
            yield return new WaitForSeconds(0.2F);
            // 增加新的棋子（棋盘顶部）
            AddNewChessByTop();
            yield return new WaitForSeconds(0.5F);
            // 新的棋子下落动画处理
            PlayNewChessDropDown();
            // 迭代循环检测
            StartCoroutine("CheckIfCanBurst");
        }
        else
        {
            print("当前棋盘没有消除选项了");
        }
    }

    // 增加新的棋子（棋盘顶部）
    private void AddNewChessByTop()
    {
        // 计算每一列需要增加棋子的数量
        for (int col = 0; col < ColumnsManager.Instance.colArray.Length; col++)  // 列
        {
            for (int row = 0; row < ColumnsManager.Instance.colArray[col].liChessArray.Count; row++)  // 行
            {
                if (ColumnsManager.Instance.colArray[col].liChessArray[row].CanBurstCurrentChess)
                {
                    // 统计每一列需要增加棋子的数量
                    ++ColumnsManager.Instance.colArray[col].intNeedAddingChessNumber;
                }
            }
        }
        // 每一列中棋子集合需要Remove掉已经删除了的棋子对象（脚本）
        for (int col = 0; col < ColumnsManager.Instance.colArray.Length; col++)  // 列
        {
            // 此语句必须改
            //for (int row = 0; row < ColumnsManager.Instance.colArray[col].liChessArray.Count; row++)  // 行
            for(int row = ColumnsManager.Instance.colArray[col].liChessArray.Count-1; row >= 0 ; row--)
            {
                if (ColumnsManager.Instance.colArray[col].liChessArray[row].CanBurstCurrentChess)
                {
                    // 已经被销毁的游戏对象所属的脚本对象，需要同步删除
                    ColumnsManager.Instance.colArray[col].liChessArray.RemoveAt(row);
                }
            }
        }
        
        for (int col = 0; col < ColumnsManager.Instance.colArray.Length; col++)
        {
            // 增加新的棋子
            ColumnsManager.Instance.colArray[col].AddNewChessByCurrentColumns();
        }
    }

    // 新的棋子下落动画处理
    private void PlayNewChessDropDown()
    {
        for (int col = 0; col < ColumnsManager.Instance.colArray.Length; col++)
        {
            // 每一列播放运动动画效果
            ColumnsManager.Instance.colArray[col].PlayNewChessDropDown();
        }
    }

    // 检测当前棋盘且设置当前棋盘是否可以消除
    private void CheckIfExistBurstItems()
    {
        for (int col = 0; col < ColumnsManager.Instance.colArray.Length; col++)  // 列
        {
            for (int row = 0; row < ColumnsManager.Instance.colArray[col].liChessArray.Count; row++)  // 行
            {
                // 设置每个棋子是否可以消除标志位
                ColumnsManager.Instance.colArray[col].liChessArray[row].MakeFlagIfCanBurst();
                if (ColumnsManager.Instance.colArray[col].liChessArray[row].CanBurstCurrentChess)
                {
                    IfExistBurstItem = true;
                }
            }
        }
    }

    // 删除当前棋盘可以消除的棋子
    private  void DestoryChessIfCanBurst()
    {
        for (int col = 0; col < ColumnsManager.Instance.colArray.Length; col++)  // 列
        {
            for (int row = 0; row < ColumnsManager.Instance.colArray[col].liChessArray.Count; row++)  // 行
            {
                ColumnsManager.Instance.colArray[col].liChessArray[row].DestroyChess();
            }
        }
    }

    // 棋盘分配邻居
    internal void AssignNeighbor()
    {
        // 列管理器分配邻居
        ColumnsManager.Instance.AssignNeighbor();
        AssignNeighborEveryChess();
    }

    // 对每一个棋子，由定义的数组转换成8个邻居字符串
    private void AssignNeighborEveryChess()
    {
        for (int col = 0; col < ColumnsManager.Instance.colArray.Length; col++)
        {
            for (int row = 0; row < ColumnsManager.Instance.colArray[col].liChessArray.Count; row++)
            {
                ColumnsManager.Instance.colArray[col].liChessArray[row].AssignNeighbor();
            }
        }
    }

    // 测试分配邻居
    public void TestAssignNeighbor()
    {
        for (int col = 0; col < ColumnsManager.Instance.colArray.Length; col++)
        {
            print(string.Format("col={0}",col));
            for (int row = 0; row < ColumnsManager.Instance.colArray[col].liChessArray.Count; row++)
            {
                print(string.Format("row={0}", row));
                // 测试分配邻居
                ColumnsManager.Instance.colArray[col].liChessArray[row].TestAssignNeighbor();
            }
        }
    }

}
