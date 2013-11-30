/*==================================================*/
/*!
 * @file SoundUtil.cs
 * @author Toru Yamaguchi
 * @date 2013/10/09
 * */
/*==================================================*/
/*--------------------------------------------------*/
/*
 * @brief using.
 * */
/*--------------------------------------------------*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*==================================================*/
/*!
 * @brief サウンドマネージャクラス.
 * @note
 * サウンドの機能として、2Dと3D両方について対応してみた.
 * 3Dの方に関してはまだ怪しい部分があるので.
 * 基本的に使用する場合は、2D側を使用する.
 * 
 * 
 * サウンドのファイル名について.
 * すべてのファイル名は別の名前を持っていると仮定して作成しているので.
 * 名前がかぶらないように細心の注意を払うように.
 * 
 * 
 * */
/*==================================================*/
public class SoundUtil : MonoBehaviour {

	/*++++++++++++++++++++++++++++++++++++++++++++++++++
	 * シングルトンインスタンス.
	 ++++++++++++++++++++++++++++++++++++++++++++++++++*/
	static private SoundUtil _instance = null;
	static public SoundUtil instance{ get{return _instance;} }

	/*++++++++++++++++++++++++++++++++++++++++++++++++++
	 * 変数.
	 ++++++++++++++++++++++++++++++++++++++++++++++++++*/
	
	//! 2DAudioClip 一覧.
	public AudioClip[] clips2D;

	//! 2DAudioClipが０以上かどうか.
	private bool _inClips2D = false;

	//! 2DAudioClipの数から、作成したクラスを保持するリスト.
	private List<SoundUtilData2D> _datas2D = new List<SoundUtilData2D>();

	//! 3DAudioClip.
	public AudioClip[] clips3D;

	//! 3DAudioClipが０以上かどうか.
	private bool _inClips3D = false;

	//! 3DAudioClipの数から、作成したクラスを保持するリスト.
	private List<SoundUtilData3D> _datas3D = new List<SoundUtilData3D>();

	//! ２Dのサウンドを置く場所(AudioListenerがある場所が望ましい？).
	public GameObject listenerObj;

	/*++++++++++++++++++++++++++++++++++++++++++++++++++
	 * 初期化関数.
	 ++++++++++++++++++++++++++++++++++++++++++++++++++*/
	/*--------------------------------------------------*/
	/*!
	 * @brief 開始時にランダム順番で呼ばれる.
	 * */
	/*--------------------------------------------------*/
	void Awake ()
	{
		if(_instance == null)
		{
			if(listenerObj == null)
			{
				listenerObj = new GameObject("__AudioSouceObject");
			}

			if (clips2D.Length != 0)
			{
				for (int i = 0; i < clips2D.Length; i++)
				{
					SoundUtilData2D data = new SoundUtilData2D();
					data.SetData(clips2D[i], listenerObj);
					_datas2D.Add(data);
				}
			}
			else
			{ _inClips2D = true; }

			if (clips3D.Length != 0)
			{
				for (int i = 0; i < clips3D.Length; i++)
				{
					SoundUtilData3D data = new SoundUtilData3D();
					data.SetData(clips3D[i], listenerObj);
					_datas3D.Add(data);
				}
			}
			else
			{ _inClips3D = true; }

			_instance = this;
		}
		else
		{
			Object.Destroy(this);
		}
	}

	/*--------------------------------------------------*/
	/*!
	 * @brief Awake()がすべて呼ばれた後に呼ばれる.
	 * */
	/*--------------------------------------------------*/
	void Start ()
	{
		// AudioClipのサイズの確認.

		
	}
	
	/*++++++++++++++++++++++++++++++++++++++++++++++++++
	 * 更新関数.
	 ++++++++++++++++++++++++++++++++++++++++++++++++++*/
	/*--------------------------------------------------*/
	/*!
	 * @brief 毎フレーム必ず呼ばれる.
	 * */
	/*--------------------------------------------------*/
	void Update ()
	{
		if (!_inClips2D)
		{
			foreach (SoundUtilData2D data in _datas2D)
			{
				data.Check();
			}
		}
		if (!_inClips3D)
		{
			foreach (SoundUtilData3D data in _datas3D)
			{
				data.Check();
			}
		}
	}

	/*--------------------------------------------------*/
	/*!
	 * @brief オブジェクトに音をつける.
	 * */
	/*--------------------------------------------------*/
	public void SetSound(GameObject obj, int number)
	{
		if (_inClips3D) { return; }
		_datas3D[number].SetObject(obj);
	}
	public void SetSound(GameObject obj, string str)
	{
		foreach(SoundUtilData3D data in _datas3D)
		{
			if(data.CheckName(str))
			{
				data.SetObject(obj);
			}
		}
	}

	void Play2D(SoundUtilData2D data, float voloume, float pitch, float pan, bool flg = false)
	{
		bool inVol = false;
		bool inPit = false;
		bool inPan = false;

		if (voloume != -1) { inVol = true; }
		if (pitch != -1) { inPit = true; }
		if (pan != -1) { inPan = true; }

		if (inVol)
		{
			if (inPit)
			{
				if (inPan)
				{
					if(flg)
					{ data.Play(voloume, pitch, pan); }
					else
					{ data.PlayOnShot(voloume, pitch, pan); }
				}
				else
				{
					if(flg)
					{data.Playvolpit(voloume, pitch);}
					else
					{data.PlayOnShotvolpit(voloume, pitch);}
				}
			}
			if (inPan)
			{
				if(flg)
				{data.Playvolpan(voloume, pan);}
				else
				{data.PlayOnShotvolpan(voloume, pan);}
			}
			else
			{
				if(flg)
				{data.Playvol(voloume);}
				else
				{data.PlayOnShotvol(voloume);}
			}
		}
		else if (inPit)
		{
			if (inPan)
			{
				if(flg)
				{data.Playpitpan(pitch, pan);}
				else
				{data.PlayOnShotpitpan(pitch, pan);}
			}
			else
			{
				if(flg)
				{data.Playpit(pitch);}
				else
				{data.PlayOnShotpit(pitch);}
			}
		}
		else if (inPan)
		{
			if(flg)
			{data.Playpan(pan);}
			else
			{data.PlayOnShotpan(pan);}
		}
		else
		{
			if(flg)
			{data.Play();}
			else
			{data.PlayOnShot();}
		}
	}

	void Play3D(SoundUtilData3D data, float voloume, float pitch, float pan, bool flg = false)
	{
		bool inVol = false;
		bool inPit = false;
		bool inPan = false;

		if (voloume != -1) { inVol = true; }
		if (pitch != -1) { inPit = true; }
		if (pan != -1) { inPan = true; }

		if (inVol)
		{
			if (inPit)
			{
				if (inPan)
				{
					if (flg)
					{ data.Play(voloume, pitch, pan); }
					else
					{ data.PlayOnShot(voloume, pitch, pan); }
				}
				else
				{
					if (flg)
					{ data.Playvolpit(voloume, pitch); }
					else
					{ data.PlayOnShotvolpit(voloume, pitch); }
				}
			}
			if (inPan)
			{
				if (flg)
				{ data.Playvolpan(voloume, pan); }
				else
				{ data.PlayOnShotvolpan(voloume, pan); }
			}
			else
			{
				if (flg)
				{ data.Playvol(voloume); }
				else
				{ data.PlayOnShotvol(voloume); }
			}
		}
		else if (inPit)
		{
			if (inPan)
			{
				if (flg)
				{ data.Playpitpan(pitch, pan); }
				else
				{ data.PlayOnShotpitpan(pitch, pan); }
			}
			else
			{
				if (flg)
				{ data.Playpit(pitch); }
				else
				{ data.PlayOnShotpit(pitch); }
			}
		}
		else if (inPan)
		{
			if (flg)
			{ data.Playpan(pan); }
			else
			{ data.PlayOnShotpan(pan); }
		}
		else
		{
			if (flg)
			{ data.Play(); }
			else
			{ data.PlayOnShot(); }
		}
	}

	/*--------------------------------------------------*/
	/*!
	 * @brief サウンドの再生を行う.
	 * @param name		:	鳴らしたいサウンド名.
	 * @param vloume	:	設定したいボリューム.
	 * @param pitch		:	設定したいピッチ.
	 * @param pan		:	設定したいパン.
	 * */
	/*--------------------------------------------------*/
	public void PlayOnShot2D(int number, float voloume = -1, float pitch = -1, float pan = -1)
	{
		Play2D(_datas2D[number], voloume, pitch, pan);
	}

	/*--------------------------------------------------*/
	/*!
	 * @brief サウンドの再生を行う.
	 * @param name		:	鳴らしたいサウンド名.
	 * @param vloume	:	設定したいボリューム.
	 * @param pitch		:	設定したいピッチ.
	 * @param pan		:	設定したいパン.
	 * */
	/*--------------------------------------------------*/
	public void PlayOnShot2D(string name, float voloume = -1, float pitch = -1, float pan = -1)
	{
		foreach (SoundUtilData2D data in _datas2D)
		{
			if (data.CheckName(name))
			{
				Play2D(data, voloume, pitch, pan);
				break;
			}
		}
	}

	/*--------------------------------------------------*/
	/*!
	 * @brief サウンドの再生を行う.
	 * @param name		:	鳴らしたいサウンド名.
	 * @param vloume	:	設定したいボリューム.
	 * @param pitch		:	設定したいピッチ.
	 * @param pan		:	設定したいパン.
	 * */
	/*--------------------------------------------------*/
	public void PlayOnShot3D(int number, float voloume = -1, float pitch = -1, float pan = -1)
	{
		Play3D(_datas3D[number], voloume, pitch, pan);
	}

	/*--------------------------------------------------*/
	/*!
	 * @brief サウンドの再生を行う.
	 * @param name		:	鳴らしたいサウンド名.
	 * @param vloume	:	設定したいボリューム.
	 * @param pitch		:	設定したいピッチ.
	 * @param pan		:	設定したいパン.
	 * */
	/*--------------------------------------------------*/
	public void PlayOnShot3D(string name, float voloume = -1, float pitch = -1, float pan = -1)
	{
		foreach (SoundUtilData3D data in _datas3D)
		{
			if (data.CheckName(name))
			{
				Play3D(data, voloume, pitch, pan);
				break;
			}
		}
	}

	/*--------------------------------------------------*/
	/*!
	 * @brief サウンドの再生を行う.
	 * @param name		:	鳴らしたいサウンド名.
	 * @param vloume	:	設定したいボリューム.
	 * @param pitch		:	設定したいピッチ.
	 * @param pan		:	設定したいパン.
	 * */
	/*--------------------------------------------------*/
	public void PlayOnShot(string name, float voloume = -1, float pitch = -1, float pan = -1)
	{
		foreach (SoundUtilData2D data in _datas2D)
		{
			if (data.CheckName(name))
			{
				Play2D(data, voloume, pitch, pan);
				break;
			}
		}

		foreach (SoundUtilData3D data in _datas3D)
		{
			if (data.CheckName(name))
			{
				Play3D(data, voloume, pitch, pan);
				break;
			}
		}
	}

	/*--------------------------------------------------*/
	/*!
	 * @brief サウンドの再生を行う.
	 * @param name		:	鳴らしたいサウンド名.
	 * @param vloume	:	設定したいボリューム.
	 * @param pitch		:	設定したいピッチ.
	 * @param pan		:	設定したいパン.
	 * */
	/*--------------------------------------------------*/
	public void Play2D(int number, float voloume = -1, float pitch = -1, float pan = -1)
	{
		Play2D(_datas2D[number], voloume, pitch, pan, true);
	}

	/*--------------------------------------------------*/
	/*!
	 * @brief サウンドの再生を行う.
	 * @param name		:	鳴らしたいサウンド名.
	 * @param vloume	:	設定したいボリューム.
	 * @param pitch		:	設定したいピッチ.
	 * @param pan		:	設定したいパン.
	 * */
	/*--------------------------------------------------*/
	public void Play2D(string name, float voloume = -1, float pitch = -1, float pan = -1)
	{
		foreach (SoundUtilData2D data in _datas2D)
		{
			if (data.CheckName(name))
			{
				Play2D(data, voloume, pitch, pan,true);
				break;
			}
		}
	}

	/*--------------------------------------------------*/
	/*!
	 * @brief サウンドの再生を行う.
	 * @param name		:	鳴らしたいサウンド名.
	 * @param vloume	:	設定したいボリューム.
	 * @param pitch		:	設定したいピッチ.
	 * @param pan		:	設定したいパン.
	 * */
	/*--------------------------------------------------*/
	public void Play3D(int number, float voloume = -1, float pitch = -1, float pan = -1)
	{
		Play3D(_datas3D[number], voloume, pitch, pan,true);
	}

	/*--------------------------------------------------*/
	/*!
	 * @brief サウンドの再生を行う.
	 * @param name		:	鳴らしたいサウンド名.
	 * @param vloume	:	設定したいボリューム.
	 * @param pitch		:	設定したいピッチ.
	 * @param pan		:	設定したいパン.
	 * */
	/*--------------------------------------------------*/
	public void Play3D(string name, float voloume = -1, float pitch = -1, float pan = -1)
	{
		foreach (SoundUtilData3D data in _datas3D)
		{
			if (data.CheckName(name))
			{
				Play3D(data, voloume, pitch, pan,true);
				break;
			}
		}
	}

	/*--------------------------------------------------*/
	/*!
	 * @brief サウンドの再生を行う.
	 * @param name		:	鳴らしたいサウンド名.
	 * @param vloume	:	設定したいボリューム.
	 * @param pitch		:	設定したいピッチ.
	 * @param pan		:	設定したいパン.
	 * */
	/*--------------------------------------------------*/
	public void Play(string name, float voloume = -1, float pitch = -1, float pan = -1)
	{
		foreach (SoundUtilData2D data in _datas2D)
		{
			if (data.CheckName(name))
			{
				Play2D(data, voloume, pitch, pan,true);
				break;
			}
		}

		foreach (SoundUtilData3D data in _datas3D)
		{
			if (data.CheckName(name))
			{
				Play3D(data, voloume, pitch, pan,true);
				break;
			}
		}
	}

	public void PlayChangeSound(string _fastSoundName, string _secondSoundName,
								float _fastvoloume = -1, float _fastpitch = -1, float _fastpan = -1,
								float _secondvoloume = -1, float _secondpitch = -1, float _secondpan = -1)
	{
		foreach (SoundUtilData2D data in _datas2D)
		{
			if (data.CheckName(_fastSoundName))
			{
				StartCoroutine(ChangeSound2D(data, _secondSoundName, _fastvoloume, _fastpitch, _fastpan,
																	 _secondvoloume, _secondpitch, _secondpan));
				return ;
			}
		}

		foreach (SoundUtilData3D data in _datas3D)
		{
			if (data.CheckName(_fastSoundName))
			{
				StartCoroutine(ChangeSound3D(data, _secondSoundName, _fastvoloume, _fastpitch, _fastpan,
																	 _secondvoloume, _secondpitch, _secondpan)); 
				return;
			}
		}
	}

	IEnumerator ChangeSound2D(SoundUtilData2D fast, string second,
								float _fastvoloume = -1, float _fastpitch = -1, float _fastpan = -1,
								float _secondvoloume = -1, float _secondpitch = -1, float _secondpan = -1)
	{
		Play2D(fast, _fastvoloume, _fastpitch, _fastpan);
		yield return null;
		while(fast.CheckSound())
		{
			yield return null;
		}

		foreach (SoundUtilData2D data in _datas2D)
		{
			if (data.CheckName(second))
			{
				Play2D(data, _secondvoloume, _secondpitch, _secondpan);
				break;
			}
		}
	}

	IEnumerator ChangeSound3D(SoundUtilData3D fast, string second,
								float _fastvoloume = -1, float _fastpitch = -1, float _fastpan = -1,
								float _secondvoloume = -1, float _secondpitch = -1, float _secondpan = -1)
	{
		Play3D(fast, _fastvoloume, _fastpitch, _fastpan);
		yield return null;
		while (fast.CheckSound())
		{
			yield return null;
		}

		foreach (SoundUtilData3D data in _datas3D)
		{
			if (data.CheckName(second))
			{
				Play3D(data, _secondvoloume, _secondpitch, _secondpan);
				break;
			}
		}
	}

	/*--------------------------------------------------*/
	/*!
	 * @brief サウンドをループさせるかどうか.
	 * @param number	:	サウンドの番号.
	 * @param flg		:	TRUE or FALSE.
	 * */
	/*--------------------------------------------------*/
	public void SetLoop2D(int number, bool flg)
	{
		if(_datas2D.Count == 0){return;}
		_datas2D[number].SetLoop(flg);

	}
	/*--------------------------------------------------*/
	/*!
	 * @brief サウンドをループさせるかどうか.
	 * @param number	:	サウンドの番号.
	 * @param flg		:	TRUE or FALSE.
	 * */
	/*--------------------------------------------------*/
	public void SetLoop3D(int number, bool flg)
	{
        if (_datas3D.Count == 0) { return; }
		_datas3D[number].SetLoop(flg);
	}
	/*--------------------------------------------------*/
	/*!
	 * @brief サウンドをループさせるかどうか.
	 * @param name		:	サウンド名.
	 * @param flg		:	TRUE or FALSE.
	 * */
	/*--------------------------------------------------*/
	public void SetLoop(string name, bool flg)
	{
		foreach (SoundUtilData2D data in _datas2D)
		{
			if (data.CheckName(name))
			{
				data.SetLoop(flg);
			}
		}
		foreach (SoundUtilData3D data in _datas3D)
		{
			if (data.CheckName(name))
			{
				data.SetLoop(flg);
			}
		}
	}

	/*--------------------------------------------------*/
	/*!
	 * @brief サウンドの始点の設定.
	 * @param number	:	サウンドの番号.
	 * @param seek		:	設定したい秒数.
	 * */
	/*--------------------------------------------------*/
	public void SetLStartTime2D(int number, float seek)
	{
        if (_datas2D.Count == 0) { return; }
		_datas2D[number].SetStartSeek(seek);

	}
	/*--------------------------------------------------*/
	/*!
	 * @brief サウンドの始点の設定.
	 * @param number	:	サウンドの番号.
	 * @param seek		:	設定したい秒数.
	 * */
	/*--------------------------------------------------*/
	public void SetLStartTime3D(int number, float seek)
	{
        if (_datas3D.Count == 0) { return; }
		_datas3D[number].SetStartSeek(seek);
	}
	/*--------------------------------------------------*/
	/*!
	 * @brief サウンドの始点の設定.
	 * @param name		:	サウンド名.
	 * @param seek		:	設定したい秒数.
	 * */
	/*--------------------------------------------------*/
	public void SetLStartTime(string name, float seek)
	{
		foreach (SoundUtilData2D data in _datas2D)
		{
			if (data.CheckName(name))
			{
				data.SetStartSeek(seek);
			}
		}
		foreach (SoundUtilData3D data in _datas3D)
		{
			if (data.CheckName(name))
			{
				data.SetStartSeek(seek);
			}
		}
	}

	/*--------------------------------------------------*/
	/*!
	 * @brief サウンドの終点の設定.
	 * @param number	:	サウンドの番号.
	 * @param seek		:	設定したい秒数.
	 * */
	/*--------------------------------------------------*/
	public void SetLEndTime2D(int number, float seek)
	{
        if (_datas2D.Count == 0) { return; }
		_datas2D[number].SetEndSeek(seek);

	}
	/*--------------------------------------------------*/
	/*!
	 * @brief サウンドの終点の設定.
	 * @param number	:	サウンドの番号.
	 * @param seek		:	設定したい秒数.
	 * */
	/*--------------------------------------------------*/
	public void SetLEndTime3D(int number, float seek)
	{
        if (_datas3D.Count == 0) { return; }
		_datas3D[number].SetEndSeek(seek);
	}
	/*--------------------------------------------------*/
	/*!
	 * @brief サウンドの終点の設定.
	 * @param name		:	サウンド名.
	 * @param seek		:	設定したい秒数.
	 * */
	/*--------------------------------------------------*/
	public void SetLEndTime(string name, float seek)
	{
		foreach (SoundUtilData2D data in _datas2D)
		{
			if (data.CheckName(name))
			{
				data.SetEndSeek(seek);
			}
		}
		foreach (SoundUtilData3D data in _datas3D)
		{
			if (data.CheckName(name))
			{
				data.SetEndSeek(seek);
			}
		}
	}

	/*--------------------------------------------------*/
	/*!
	 * @brief サウンドの一時停止.
	 * @param number	:	サウンドの番号.
	 * */
	/*--------------------------------------------------*/
	public void SetPause2D(int number)
	{
        if (_datas2D.Count == 0) { return; }
		_datas2D[number].Pause();

	}
	/*--------------------------------------------------*/
	/*!
	 * @brief サウンドの一時停止.
	 * @param number	:	サウンドの番号.
	 * */
	/*--------------------------------------------------*/
	public void SetPause3D(int number)
	{
        if (_datas3D.Count == 0) { return; }
		_datas3D[number].Pause();
	}
	/*--------------------------------------------------*/
	/*!
	 * @brief サウンドの一時停止.
	 * @param name		:	サウンド名.
	 * */
	/*--------------------------------------------------*/
	public void SetPause(string name)
	{
		foreach (SoundUtilData2D data in _datas2D)
		{
			if (data.CheckName(name))
			{
				data.Pause();
			}
		}
		foreach (SoundUtilData3D data in _datas3D)
		{
			if (data.CheckName(name))
			{
				data.Pause();
			}
		}
	}
	/*--------------------------------------------------*/
	/*!
	 * @brief すべてのサウンドの一時停止.
	 * @param name		:	サウンド名.
	 * */
	/*--------------------------------------------------*/
	public void SetPauseAll(string name)
	{
		foreach (SoundUtilData2D data in _datas2D)
		{
			data.Pause();
		}
		foreach (SoundUtilData3D data in _datas3D)
		{
			data.Pause();
		}
	}

	/*--------------------------------------------------*/
	/*!
	 * @brief サウンドの停止.
	 * @param number	:	サウンドの番号.
	 * */
	/*--------------------------------------------------*/
	public void SetStop2D(int number)
	{
        if (_datas2D.Count == 0) { return; }
		_datas2D[number].Stop();

	}
	/*--------------------------------------------------*/
	/*!
	 * @brief サウンドの停止.
	 * @param number	:	サウンドの番号.
	 * */
	/*--------------------------------------------------*/
	public void SetStop3D(int number)
	{
        if (_datas3D.Count == 0) { return; }
		_datas3D[number].Stop();
	}
	/*--------------------------------------------------*/
	/*!
	 * @brief サウンドの停止.
	 * @param name		:	サウンド名.
	 * */
	/*--------------------------------------------------*/
	public void SetStop(string name)
	{
		foreach (SoundUtilData2D data in _datas2D)
		{
			if (data.CheckName(name))
			{
				data.Stop();
			}
		}
		foreach (SoundUtilData3D data in _datas3D)
		{
			if (data.CheckName(name))
			{
				data.Stop();
			}
		}
	}
	/*--------------------------------------------------*/
	/*!
	 * @brief すべてのサウンドの停止.
	 * @param name		:	サウンド名.
	 * */
	/*--------------------------------------------------*/
	public void SetStopAll(string name = "")
	{
		foreach (SoundUtilData2D data in _datas2D)
		{
			data.Stop();
		}
		foreach (SoundUtilData3D data in _datas3D)
		{
			data.Stop();
		}
	}

	/*--------------------------------------------------*/
	/*!
	 * @brief フェードアウト処理.
	 * @param name		:	サウンド名.
	 * @param time		:	フェードアウトの時間.
	 * */
	/*--------------------------------------------------*/
	public void SetFadeOut(string name, float time = 0)
	{
		foreach (SoundUtilData2D data in _datas2D)
		{
			if (data.CheckName(name))
			{
				StartCoroutine(FadeOut(data, time));
			}
		}
		foreach (SoundUtilData3D data in _datas3D)
		{
			if (data.CheckName(name))
			{
				StartCoroutine(FadeOut(data, time));
			}
		}
	}
	IEnumerator FadeOut(SoundUtilData data, float time)
	{
		float startTime = Time.time;
		float endTime = startTime + time;
		float fadeT = 0;
		float startVol = data.__volume;

		if( time != 0)
		{
			while( fadeT <= 1)
			{
				fadeT = Mathf.Clamp01( (Time.time - startTime) / (endTime - startTime) );

				float vol = startVol * ( 1 - fadeT) + 0 * fadeT;
				data.__volume = vol;
				yield return null;
			}
		}

		data.Stop();
		yield return null;
		// 音量を元に戻す.
		data.__volume = startVol;
		yield return null;
	}
}

/*==================================================*/
/*!
 * @brief サウンドデータクラス.
 * @note
 *
 * */
/*==================================================*/
public class SoundUtilData2D : SoundUtilData
{
	protected override void SetData()
	{
		_source.panLevel = 0;
	}
}

/*==================================================*/
/*!
 * @brief サウンドデータクラス.
 * @note
 *
 * */
/*==================================================*/
public class SoundUtilData3D : SoundUtilData
{
	protected override void SetData()
	{
		base.SetData();
		_source.panLevel = 1;
	}

	public void SetObject(GameObject obj)
	{
		AudioSource source = obj.AddComponent<AudioSource>();
		source.clip = _clip;
		source.Play();
	}
}


/*==================================================*/
/*!
 * @brief サウンドデータクラスのベース.
 * */
/*==================================================*/
public class SoundUtilData
{
	/*++++++++++++++++++++++++++++++++++++++++++++++++++
	 * 変数宣言.
	 ++++++++++++++++++++++++++++++++++++++++++++++++++*/
	protected AudioClip _clip;		//!< AudioClip.
	protected AudioSource _source;	//!< AudioSource.
	protected string _clipName;		//!< AudioClipの名前.
	protected GameObject _obj;			//!< AudioSouceがついているオブジェクト.
	protected float _startSeek;		//!< サウンドが鳴り始めの位置.
	protected float _endSeek;			//!< サウンドの鳴り終わりの位置.
	protected bool _inPause;			//!< ポーズ中かどうか.

	/*++++++++++++++++++++++++++++++++++++++++++++++++++
	 * オーバーライド関数群.
	 ++++++++++++++++++++++++++++++++++++++++++++++++++*/
	protected virtual void SetData() { }	//!< サウンドデータの設定をしたときに呼ばれる関数.

	/*--------------------------------------------------*/
	/*
	 * @brief コンストラクタ.
	 * */
	/*--------------------------------------------------*/
	public SoundUtilData()
	{
		_clip = null;
		_clipName = string.Empty;
		_source = null;
		_startSeek = 0;
		_endSeek = 0;
		_inPause = false;
	}

	/*--------------------------------------------------*/
	/*
	 * @brief データのコピー.
	 * */
	/*--------------------------------------------------*/
	public void Copy(SoundUtilData data)
	{
		_clip = data._clip;
		_clipName = data._clipName;
		_source = data._source;
		_startSeek = data._startSeek;
		_endSeek = data._endSeek;
		_inPause = data._inPause;
	}

	/*--------------------------------------------------*/
	/*!
	 * @brief サウンドデータの設定を行う.
	 * @param clip		:	音のAudioClip.
	 * @param parent	:	AudioSouceをつけるGameObject.
	 * @return true 終了したら帰ってくる.
	 * */
	/*--------------------------------------------------*/
	public bool SetData(AudioClip clip, GameObject parent)
	{
		Debug.Log("clip :" + clip.name);
		_clip = clip;
		_endSeek = clip.length;
		_obj = parent;
		_source = parent.AddComponent<AudioSource>();
		_source.clip = clip;
		_clipName = clip.name;
		
		SetData();

		_source.volume = 0.5f;

		return true;
	}

	/*--------------------------------------------------*/
	/*!
	 * @brief サウンドの再生を行う.
	 * */
	/*--------------------------------------------------*/
	public void PlayOnShot()
	{
		_source.time = _startSeek;
		if(!_inPause)
		{
			Stop();
		}
		_source.PlayOneShot(_clip);
	}
	/*--------------------------------------------------*/
	/*!
	 * @brief サウンドの再生を行う.
	 * @param vloume	:	設定したいボリューム.
	 * */
	/*--------------------------------------------------*/
	public void PlayOnShotvol(float vloume)
	{
		if(vloume > 1){vloume = 1;}
		if(vloume < 0){vloume = 0;}

		_source.volume = vloume;

		PlayOnShot();
	}
	/*--------------------------------------------------*/
	/*!
	 * @brief サウンドの再生を行う.
	 * @param pitch		:	設定したいピッチ.
	 * */
	/*--------------------------------------------------*/
	public void PlayOnShotpit(float pitch)
	{
		if (pitch > 3) { pitch = 3; }
		if (pitch < -3) { pitch = -3; }

		_source.pitch = pitch;

		PlayOnShot();
	}
	/*--------------------------------------------------*/
	/*!
	 * @brief サウンドの再生を行う.
	 * @param pan		:	設定したいパン.
	 * */
	/*--------------------------------------------------*/
	public void PlayOnShotpan(float pan)
	{
		if (pan > 1) { pan = 1; }
		if (pan < -1) { pan = -1; }

		_source.pan = pan;

		PlayOnShot();
	}
	/*--------------------------------------------------*/
	/*!
	 * @brief サウンドの再生を行う.
	 * @param vloume	:	設定したいボリューム.
	 * @param pitch		:	設定したいピッチ.
	 * */
	/*--------------------------------------------------*/
	public void PlayOnShotvolpit(float vloume, float pitch)
	{
		if(vloume > 1){vloume = 1;}
		if(vloume < 0){vloume = 0;}
		if(pitch > 3){pitch = 3;}
		if(pitch < -3){pitch = -3;}

		_source.volume = vloume;
		_source.pitch = pitch;

		PlayOnShot();
	}
	/*--------------------------------------------------*/
	/*!
	 * @brief サウンドの再生を行う.
	 * @param pitch		:	設定したいピッチ.
	 * @param pan		:	設定したいパン.
	 * */
	/*--------------------------------------------------*/
	public void PlayOnShotpitpan(float pitch, float pan)
	{
		if (pitch > 3) { pitch = 3; }
		if (pitch < -3) { pitch = -3; }
		if (pan > 1) { pan = 1; }
		if (pan < -1) { pan = -1; }

		_source.pitch = pitch;
		_source.pan = pan;

		PlayOnShot();
	}
	/*--------------------------------------------------*/
	/*!
	 * @brief サウンドの再生を行う.
	 * @param vloume	:	設定したいボリューム.
	 * @param pan		:	設定したいパン.
	 * */
	/*--------------------------------------------------*/
	public void PlayOnShotvolpan(float vloume, float pan)
	{
		if (vloume > 1) { vloume = 1; }
		if (vloume < 0) { vloume = 0; }
		if (pan > 1) { pan = 1; }
		if (pan < -1) { pan = -1; }

		_source.volume = vloume;
		_source.pan = pan;

		PlayOnShot();
	}
	/*--------------------------------------------------*/
	/*!
	 * @brief サウンドの再生を行う.
	 * @param vloume	:	設定したいボリューム.
	 * @param pitch		:	設定したいピッチ.
	 * @param pan		:	設定したいパン.
	 * */
	/*--------------------------------------------------*/
	public void PlayOnShot(float vloume, float pitch, float pan)
	{
		if(vloume > 1){vloume = 1;}
		if(vloume < 0){vloume = 0;}
		if(pitch > 3){pitch = 3;}
		if(pitch < -3){pitch = -3;}
		if(pan > 1){pan = 1;}
		if(pan < -1){pan = -1;}

		_source.volume = vloume;
		_source.pitch = pitch;
		_source.pan = pan;

		PlayOnShot();
	}

	/*--------------------------------------------------*/
	/*!
	 * @brief サウンドの再生を行う.
	 * */
	/*--------------------------------------------------*/
	public void Play()
	{
		_source.time = _startSeek;
		if (!_inPause)
		{
			Stop();
		}
		_source.Play();
	}
	/*--------------------------------------------------*/
	/*!
	 * @brief サウンドの再生を行う.
	 * @param vloume	:	設定したいボリューム.
	 * */
	/*--------------------------------------------------*/
	public void Playvol(float vloume)
	{
		if (vloume > 1) { vloume = 1; }
		if (vloume < 0) { vloume = 0; }

		_source.volume = vloume;

		Play();
	}
	/*--------------------------------------------------*/
	/*!
	 * @brief サウンドの再生を行う.
	 * @param pitch		:	設定したいピッチ.
	 * */
	/*--------------------------------------------------*/
	public void Playpit(float pitch)
	{
		if (pitch > 3) { pitch = 3; }
		if (pitch < -3) { pitch = -3; }

		_source.pitch = pitch;

		Play();
	}
	/*--------------------------------------------------*/
	/*!
	 * @brief サウンドの再生を行う.
	 * @param pan		:	設定したいパン.
	 * */
	/*--------------------------------------------------*/
	public void Playpan(float pan)
	{
		if (pan > 1) { pan = 1; }
		if (pan < -1) { pan = -1; }

		_source.pan = pan;

		Play();
	}
	/*--------------------------------------------------*/
	/*!
	 * @brief サウンドの再生を行う.
	 * @param vloume	:	設定したいボリューム.
	 * @param pitch		:	設定したいピッチ.
	 * */
	/*--------------------------------------------------*/
	public void Playvolpit(float vloume, float pitch)
	{
		if (vloume > 1) { vloume = 1; }
		if (vloume < 0) { vloume = 0; }
		if (pitch > 3) { pitch = 3; }
		if (pitch < -3) { pitch = -3; }

		_source.volume = vloume;
		_source.pitch = pitch;

		Play();
	}
	/*--------------------------------------------------*/
	/*!
	 * @brief サウンドの再生を行う.
	 * @param pitch		:	設定したいピッチ.
	 * @param pan		:	設定したいパン.
	 * */
	/*--------------------------------------------------*/
	public void Playpitpan(float pitch, float pan)
	{
		if (pitch > 3) { pitch = 3; }
		if (pitch < -3) { pitch = -3; }
		if (pan > 1) { pan = 1; }
		if (pan < -1) { pan = -1; }

		_source.pitch = pitch;
		_source.pan = pan;

		Play();
	}
	/*--------------------------------------------------*/
	/*!
	 * @brief サウンドの再生を行う.
	 * @param vloume	:	設定したいボリューム.
	 * @param pan		:	設定したいパン.
	 * */
	/*--------------------------------------------------*/
	public void Playvolpan(float vloume, float pan)
	{
		if (vloume > 1) { vloume = 1; }
		if (vloume < 0) { vloume = 0; }
		if (pan > 1) { pan = 1; }
		if (pan < -1) { pan = -1; }

		_source.volume = vloume;
		_source.pan = pan;

		Play();
	}
	/*--------------------------------------------------*/
	/*!
	 * @brief サウンドの再生を行う.
	 * @param vloume	:	設定したいボリューム.
	 * @param pitch		:	設定したいピッチ.
	 * @param pan		:	設定したいパン.
	 * */
	/*--------------------------------------------------*/
	public void Play(float vloume, float pitch, float pan)
	{
		if (vloume > 1) { vloume = 1; }
		if (vloume < 0) { vloume = 0; }
		if (pitch > 3) { pitch = 3; }
		if (pitch < -3) { pitch = -3; }
		if (pan > 1) { pan = 1; }
		if (pan < -1) { pan = -1; }

		_source.volume = vloume;
		_source.pitch = pitch;
		_source.pan = pan;

		Play();
	}

	/*--------------------------------------------------*/
	/*!
	 * @brief サウンドをループさせるかどうか.
	 * @param flg	:	TRUE or FALSE.
	 * */
	/*--------------------------------------------------*/
	public void SetLoop(bool flg)
	{
		_source.loop = flg;
	}

	/*--------------------------------------------------*/
	/*!
	 * @brief サウンドをミュートさせるかどうか.
	 * @param flg	:	TRUE or FALSE.
	 * */
	/*--------------------------------------------------*/
	public void SetMute(bool flg)
	{
		_source.mute = flg;
	}

	/*--------------------------------------------------*/
	/*!
	 * @brief サウンドをスタートさせる位置.
	 * @param seek	:	スタートさせる位置(秒単位).
	 * */
	/*--------------------------------------------------*/
	public void SetStartSeek(float seek)
	{
		_startSeek = seek;
	}

	/*--------------------------------------------------*/
	/*!
	 * @brief サウンドを終了させる位置.
	 * */
	/*--------------------------------------------------*/
	public void SetEndSeek(float seek)
	{
		_endSeek = seek;
	}

	/*--------------------------------------------------*/
	/*!
	 * @brief サウンドを停止する.
	 * */
	/*--------------------------------------------------*/
	public void Stop()
	{
		_inPause = false;
		_source.Stop();
	}

	/*--------------------------------------------------*/
	/*!
	 * @brief サウンドを一時停止する.
	 * */
	/*--------------------------------------------------*/
	public void Pause()
	{
		_inPause = true;
		_source.Pause();
	}

	/*--------------------------------------------------*/
	/*!
	 * @brief 現在のサウンドの再生秒から、設定されたシークまで秒数が言っている場合の設定.
	 * */
	/*--------------------------------------------------*/
	public void Check()
	{
		if(_source.isPlaying)
		{
			if (_source.time > _endSeek)
			{
				if(_source.loop){ Play(); }
				else { Stop(); }
			}
		}
        else
        {
            if (_source.loop) { Play(); }
            else { Stop(); }
        }
	}

	/*--------------------------------------------------*/
	/*!
	 * @brief サウンド名が同じかどうか.
	 * @return TRUE or FALSE
	 * */
	/*--------------------------------------------------*/
	public bool CheckName(string name)
	{
		if(_clipName == name){return true;}
		return false;
	}

	public float __volume
	{
		get{ return _source.volume; }
		set{ _source.volume = value; }
	}
	public float __pitch
	{
		get { return _source.pitch; }
		set { _source.pitch = value; }
	}
	public float __pan
	{
		get { return _source.pan; }
		set { _source.pan = value; }
	}

	public bool CheckSound()
	{
		return _source.isPlaying;
	}
}

/*==================================================*/
/*
 * @brief EOF.
 * */
/*==================================================*/