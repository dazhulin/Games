/**
 *  Title:      学习ITween脚本插件
 *          
 *              
 *
 *  Description:
 *              使用MoveTo方法做自定义路径导航寻路（NPC）
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

public class MoveToPath : MonoBehaviour {
    public GameObject hero;                 // 寻路主角
    public Transform[] trsPathPosition;     // 路径点集合

	// Use this for initialization
	void Start () {
        DisplayMoveByPath();
    }
	
	void DisplayMoveByPath()
    {
        iTween.MoveTo(hero.gameObject, iTween.Hash(
            "path", trsPathPosition,
            "time",5F
            ));
    }

    public void OnDrawGizmos()
    {
        iTween.DrawLineGizmos(trsPathPosition);
    }

}
