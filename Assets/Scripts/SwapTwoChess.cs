/**
 *  Title:      快乐消消乐
 *          
 *              交换棋子
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
#if UNITY_EDITOR
using UnityEditor.SceneManagement;
#endif

public class SwapTwoChess : MonoBehaviour {
    public static SwapTwoChess Instance;       // 本类的静态实例

    void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // 交换棋子算法
    internal void SwapTwoChessObj(Chess chess1, Chess chess2)
    {
        // 参数检查
        if (chess1 == null || chess2 == null)
        {
            Debug.LogError("[SwapTwoChess.cs/SwapTwoChessObj]参数缺失!");
        }

        // 交换动画
        iTween.MoveTo(chess1.gameObject, chess2.gameObject.transform.position, 1F);
        iTween.MoveTo(chess2.gameObject, chess1.gameObject.transform.position, 1F);

        // 核心交换算法
        SwapTwoChessItem(chess1, chess2);
    }

    // 核心棋子交换算法
    internal void SwapTwoChessItem(Chess chess1, Chess chess2)
    {
        Columns columnfromChess1;                       // 棋子1所属的列
        Columns columnfromChess2;                       // 棋子2所属的列
        int intChess1_IndexNumFromColumns = -999;           // 棋子1所在列中的序号
        int intChess2_IndexNumFromColumns = -999;           // 棋子2所在列中的序号

        columnfromChess1 = chess1.fromColumns;
        columnfromChess2 = chess2.fromColumns;
        if (columnfromChess1 == null || columnfromChess2 == null)
        {
            Debug.Log("[SwapTwoChess.cs/SwapTwoChessItem]参数不对");
            return;
        }
        // 得到棋子1在所属列中的序号
        for (int i = 0; i < columnfromChess1.liChessArray.Count; i++)
        {
            if (chess1.GetInstanceID() == columnfromChess1.liChessArray[i].GetInstanceID())
            {
                intChess1_IndexNumFromColumns = i;
            }
        }
        // 得到棋子2在所属列中的序号
        for (int j = 0; j < columnfromChess2.liChessArray.Count; j++)
        {
            if (chess2.GetInstanceID() == columnfromChess2.liChessArray[j].GetInstanceID())
            {
                intChess2_IndexNumFromColumns = j;
            }
        }
        if (intChess1_IndexNumFromColumns == -999 || intChess2_IndexNumFromColumns == -999)
        {
            // 由于参数错误，重新调用本关卡
            // 已过时
            //Application.LoadLevel(Application.loadedLevel);
            #if UNITY_EDITOR
            EditorSceneManager.LoadScene(EditorSceneManager.loadedSceneCount);
            #endif
        }

        // 更新棋子所属列父子关系
        chess1.transform.parent = columnfromChess2.transform.parent;
        chess2.transform.parent = columnfromChess1.transform.parent;

        // 更新集合：删除原来的棋子，增加交换的棋子
        columnfromChess1.liChessArray.RemoveAt(intChess1_IndexNumFromColumns);
        columnfromChess1.liChessArray.Insert(intChess1_IndexNumFromColumns, chess2);
        columnfromChess2.liChessArray.RemoveAt(intChess2_IndexNumFromColumns);
        columnfromChess2.liChessArray.Insert(intChess2_IndexNumFromColumns, chess1);

        // 参数重置(棋子)
        ChessOperation.Instance.chessItem1.UnSelectMe();            //发暗
        ChessOperation.Instance.chessItem2.UnSelectMe();
        ChessOperation.Instance.chessItem1 = null;
        ChessOperation.Instance.chessItem2 = null;
        // 循环消除检查
        //ChessOperation.Instance.StartCoroutine("CheckIfCanBurst");
    }

}
