/**
 *  Title:      快乐消消乐
 *          
 *              监听棋子(用户操作)
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

public class ChessTouch : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    // 当用户在 GUIElement 或碰撞器上按鼠标按钮时调用 OnMouseDown
    private void OnMouseDown()
    {
        //print("鼠标按下");
    }

    // 当用户在 GUIElement 或碰撞器上单击鼠标并保持按住鼠标时调用 OnMouseDrag
    private void OnMouseDrag()
    {
        //print("鼠标拖拽");
    }

    // 当用户松开鼠标按钮时调用 OnMouseUp
    private void OnMouseUp()
    {
        //print("鼠标抬起");
        SelectChess();
    }

    // 选择棋子
    internal void SelectChess()
    {
        // 用户点击的第1个棋子
        if (ChessOperation.Instance.chessItem1 == null)
        {
            ChessOperation.Instance.chessItem1 = this.gameObject.GetComponent<Chess>();
            // 发亮显示
            ChessOperation.Instance.chessItem1.SelectMe();
        }
        // 用户点击的第2个棋子
        else if (ChessOperation.Instance.chessItem2 == null)
        {
            ChessOperation.Instance.chessItem2 = this.gameObject.GetComponent<Chess>();
            // 发亮显示
            ChessOperation.Instance.chessItem2.SelectMe();
            // 调用棋子交换算法
            SwapTwoChess.Instance.SwapTwoChessObj(ChessOperation.Instance.chessItem1, ChessOperation.Instance.chessItem2);
            
        }
        // 棋子重置
        else
        {
            ChessOperation.Instance.chessItem1.UnSelectMe();
            ChessOperation.Instance.chessItem2.UnSelectMe();
            ChessOperation.Instance.chessItem1 = null;
            ChessOperation.Instance.chessItem2 = null;
        }
    }

}
