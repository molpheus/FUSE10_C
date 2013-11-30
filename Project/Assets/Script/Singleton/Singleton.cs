﻿/*!
 * @file Singleton.cs
 * @author Toru Yamaguchi
 * @date 2013/06/03
 * */
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

/*!
 * @brief Singleton class
 * */
public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T> {	
	
	[HideInInspector]
	static public T Instance = null;	//!< シングルトンインスタンス


	// 初期化関数.
	/*!
	 * @brief 開始時にランダム順番で呼ばれる.
	 * */
	protected virtual void Awake()
	{
		if(Instance == null)
		{
			Instance = this as T;
			SingletonCounter.SingletonTotalCt++;
		}
		else
		{
			Destroy(gameObject);
		}
	}

	/*!
	 * @brief Awake()がすべて呼ばれた後に呼ばれる.
	 * */
	protected virtual void Start()
	{

	}

	/*!
	 * @brief インスタンス破棄時に呼び出し.
	 * */
	protected virtual void OnDestroy()
	{
		if(Instance == this as T)
		{
			Instance = null;
			SingletonCounter.SingletonTotalCt--;
		}
	}

	int hoge = 0;
	/*!
	 * @brief 何か関数を作っておかなくてはエラーが吐かれるため.
	 * */
	void Hoge()
	{
		hoge++;
		Hoge();
	}
}

/**********************************************//*
 * 
 * 
 * @brief シングルトン処理フローカウンタクラス.
 * 
 * 
*//*********************************************/
static class SingletonCounter
{
	static private int _SingletonTotalCt = 0;	//!< シングルトンの総数.
	static public int SingletonTotalCt
	{
		get
		{
			return _SingletonTotalCt;
		}
		set
		{
			_SingletonTotalCt = value;
		}
	}
}