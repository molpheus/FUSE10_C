using UnityEngine;
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
    static public int score = 0;
    // 最高スコア.
    static public int maxScore = 0;

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
