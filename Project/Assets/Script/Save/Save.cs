﻿/*!
 * @file Save.cs
 * @author Toru Yamaguchi
 * @date 2013/02/13
 * */
using UnityEngine;
using System.Collections;

/*!
 * @brief Save class
 * @note
 * 保持できるデータ量は最大で１M
 * */
public class Save : MonoBehaviour
{
	/*! @brief デフォルトの値. */
	public const int DEF_NUM = -123456;
	public const string DEF_STRING = "DEF_STRING"; 

	/*!
	 * @brief キー識別子が存在しているのかを確認する.
	 * @return キー識別子が存在していた場合 TRUE.
	 * @return してなかった場合 FALSE.
	 * */
	public static bool SearchKey(string key)
	{
		return PlayerPrefs.HasKey(key);
	}

	/*!
	 * @brief キー識別子と中身を削除する
	 * @param key   ->  キー識別子
	 * @return 削除した場合は TRUE
	 * @return 削除しなかった(もともと存在していない)場合は FALSE
	 * */
	public static bool DeleteKey(string key)
	{
		if (SearchKey(key))
		{
			PlayerPrefs.DeleteKey(key);
			return true;
		}
		return false;
	}

	/*!
	 * @brief キー識別子および中身をすべて削除する
	 * */
	public static void DeleteAll()
	{
		PlayerPrefs.DeleteAll();
	}

	/*!
	 * @brief int型のデータを保存する.
	 * @param key	   ->  キー識別子.
	 * @param num	   ->  セーブ内容.
	 * */
	public static void SetInt(string key, int num)
	{
		// キーによって識別された値をセットする.
		PlayerPrefs.SetInt(key, num);
	}

	/*!
	 * @brief int型のデータを保存する.
	 * @param key	   ->  キー識別子.
	 * @param num	   ->  セーブ内容.
	 * */
	public static void SetIntArray(string key, int[] num)
	{
		// キーによって識別された値をセットする.
		PlayerPrefsX.SetIntArray(key, num);
	}

	/*!
	 * @brief int型のデータを取得する.
	 * @param key   ->  キー識別子.
	 * @return キーが存在していた場合は　中身が帰ってくる.
	 * @return 存在してなかった場合は　-123456が帰ってくる.
	 * @note
	 * DEF_NUMに存在してなかった場合の値が入っている.
	 * */
	public static int GetInt(string key)
	{
		int num = DEF_NUM;
		if (SearchKey(key))
		{
			num = PlayerPrefs.GetInt(key);
		}
		return num;
	}

	/*!
	 * @brief string型のデータを保存する.
	 * @param key	   ->  キー識別子.
	 * @param str	   ->  セーブ内容.
	 * */
	public static void SetString(string key, string str)
	{
		PlayerPrefs.SetString(key, str);
	}

	/*!
	 * @brief string型のデータを取得する.
	 * @param key   ->  キー識別子.
	 * @return キーが存在していた場合は　中身が帰ってくる.
	 * @return 存在してなかった場合は　NOT_STRINGが帰ってくる.
	 * @note
	 * DEF_NUMに存在してなかった場合の値が入っている.
	 * */
	public static string GetString(string key)
	{
		string str = DEF_STRING;
		if (SearchKey(key))
		{
			str = PlayerPrefs.GetString(key);
		}
		return str;
	}

	/*!
	 * @brief float型のデータを保存する.
	 * @param key	   ->  キー識別子.
	 * @param num	   ->  セーブ内容.
	 * */
	public static void SetFloat(string key, float num)
	{
		PlayerPrefs.SetFloat(key, num);
	}

	/*!
	 * @brief float型のデータを取得する.
	 * @param key   ->  キー識別子.
	 * @return キーが存在していた場合は　中身が帰ってくる.
	 * @return 存在してなかった場合は　-123456が帰ってくる.
	 * @note
	 * DEF_NUMに存在してなかった場合の値が入っている.
	 * */
	public static float GetFloat(string key)
	{
		float num = DEF_NUM;
		if (SearchKey(key))
		{
			num = PlayerPrefs.GetFloat(key);
		}
		return num;
	}
}