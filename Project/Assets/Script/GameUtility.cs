﻿using UnityEngine;
using System.Collections;

public class GameUtility
{
    // 体力.
    static public int hp = 0;
    // 最大体力.
    static public int maxhp = 0;
    
    // スキルゲージ.
    static public int skillGage = 0;
    // 距離.
	static public int length = 0;

    // スコア.
	static public int _score = 0;
    static public int score
	{
		get{return _score;}
		set
		{
			int num = _score + value;
			if (num >= MAX_SCORE) { num = MAX_SCORE; }
			_score = num;
		}
	}
	static readonly int MAX_SCORE = 9999999;
    // 最高スコア.
    static public int maxScore = 0;

	// スピードコントロール.
	static public float speed = 1f;

	/*
	 * データの初期化.
	 * */
    static public void InitData()
    {
        hp = maxhp = 100;
        skillGage = 0;
		length = 0;
		score = 0;
		
		int num = Save.GetInt("SOCRE");

		maxScore = (num == Save.DEF_NUM) ? 0 : num;
    }
}
