﻿/*!
 * @file myTransform.cs
 * @author Toru Yamaguchi
 * @date 2013/03/08
 * */
using UnityEngine;
using System.Collections;

/*!
 * @brief myTransform class
 * */
public class myTransform : MonoBehaviour {

	/*! @brief 絶対座標の取得 */
	static public Vector3 GetPosition(GameObject myobj)
	{ return myobj.transform.position; }
	/*! @brief ローカル座標の取得 */
	static public Vector3 GetLocalPosition(GameObject myobj)
	{ return myobj.transform.localPosition; }

	/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	/*! @brief 絶対座標での座標の設定 */
	static public void SetPosition(GameObject myobj, Vector3 pos)
	{ myobj.transform.position = pos; }
	/*! @brief 絶対座標でのX座標の設定 */
	static public void SetPositionX(GameObject myobj, float x)
	{
		Vector3 newPosition = myobj.transform.position;
		newPosition = new Vector3(x,newPosition.y,newPosition.z);
		myobj.transform.position = newPosition;
	}
	/*! @brief 絶対座標でのY座標の設定 */
	static public void SetPositionY(GameObject myobj, float y)
	{
		Vector3 newPosition = myobj.transform.position;
		newPosition = new Vector3(newPosition.x, y, newPosition.z);
		myobj.transform.position = newPosition;
	}
	/*! @brief 絶対座標でのZ座標の設定 */
	static public void SetPositionZ(GameObject myobj, float z)
	{
		{
			Vector3 newPosition = myobj.transform.position;
			newPosition = new Vector3(newPosition.x, newPosition.y, z);
			myobj.transform.position = newPosition;
		}
	}

	/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	/*! @brief ローカル座標での座標の設定 */
	static public void SetLocalPosition(GameObject myobj, Vector3 pos)
	{ myobj.transform.localPosition = pos; }
	/*! @brief ローカル座標での座標Xの設定 */
	static public void SetLocalPositionX(GameObject myobj, float x)
	{
		Vector3 newPosition = myobj.transform.localPosition;
		newPosition = new Vector3(x, newPosition.y, newPosition.z);
		myobj.transform.localPosition = newPosition;
	}
	/*! @brief ローカル座標での座標Yの設定 */
	static public void SetLocalPositionY(GameObject myobj, float y)
	{
		Vector3 newPosition = myobj.transform.localPosition;
		newPosition = new Vector3(newPosition.x, y, newPosition.z);
		myobj.transform.localPosition = newPosition;
	}

	/*! @brief ローカル座標での座標Zの設定 */
	static public void SetLocalPositionZ(GameObject myobj, float z)
	{
		Vector3 newPosition = myobj.transform.localPosition;
		newPosition = new Vector3(newPosition.x, newPosition.y, z);
		myobj.transform.localPosition = newPosition;
	}

	/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	/*!
	 * @brief 入力分絶対座標を動かす
	 * */
	static public void MovePosition(GameObject myobj, Vector3 pos)
	{
		{
			// myobj.transform.position += pos;
			MovePos(myobj, pos);
		}
	}
	/*!
	 * @brief 入力分ローカル座標を動かす
	 * */
	static public void MoveLocalPosition(GameObject myobj, Vector3 pos)
	{
		{
			// myobj.transform.localPosition += pos;
			MovePos(myobj, pos);
		}
	}
	static private void MovePos(GameObject myobj, Vector3 pos)
	{ myobj.transform.Translate(pos); }

	/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	/*!
	 * @brief 絶対座標上での設定位置まで動く
	 * (毎回呼び出さなくてはいけません)
	 * */
	static public void MoveTargetPosition(GameObject myobj, Vector3 target, float move)
	{ myobj.transform.position = Vector3.Lerp(myobj.transform.position, target, move * Time.deltaTime); }
	/*!
	 * @brief ローカル座標上での設定位置まで動く
	 * (毎回呼び出さなくてはいけません)
	 * */
	static public void MoveTargetLocalPosition(GameObject myobj, Vector3 target, float move)
	{ myobj.transform.localPosition = Vector3.Lerp(myobj.transform.localPosition, target, move * Time.deltaTime); }

	/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	/*! @brief スケールを変更する */
	static public void SetLocalScale(GameObject myobj, Vector3 target)
	{ myobj.transform.localScale = target; }
	/*! @brief スケール変更X. */
	static public void SetLocalScaleX(GameObject myobj, float x)
	{
		Vector3 newScale = myobj.transform.localScale;
		newScale = new Vector3(x,newScale.y, newScale.z);
		myobj.transform.localScale = newScale;
	}
	/*! @brief スケール変更Y. */
	static public void SetLocalScaleY(GameObject myobj, float y)
	{
		Vector3 newScale = myobj.transform.localScale;
		newScale = new Vector3(newScale.x, y, newScale.z);
		myobj.transform.localScale = newScale;
	}
	/*! @brief スケール変更Z. */
	static public void SetLocalScaleZ(GameObject myobj, float z)
	{
		Vector3 newScale = myobj.transform.localScale;
		newScale = new Vector3(newScale.x, newScale.y, z);
		myobj.transform.localScale = newScale;
	}

	/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	/*! @brief 画像を設定角度にする*/
	static public void SetRotate(GameObject myobj, Vector3 rotate)
	{
		myobj.transform.localRotation = new Quaternion(0, 0, 0, 0);
		myobj.transform.Rotate(rotate);
	}
	/*! @brief 回転X. */
	static public void SetRotateX(GameObject myobj, float x)
	{
		Quaternion newRotation = myobj.transform.localRotation;
		newRotation.eulerAngles = new Vector3(x, newRotation.eulerAngles.y, newRotation.eulerAngles.z);
		myobj.transform.localRotation = newRotation;
	}
	/*! @brief 回転Y. */
	static public void SetRotateY(GameObject myobj, float y)
	{
		Quaternion newRotation = myobj.transform.localRotation;
		newRotation.eulerAngles = new Vector3(newRotation.eulerAngles.x, y, newRotation.eulerAngles.z);
		myobj.transform.localRotation = newRotation;
	}
	/*! @brief 回転Z. */
	static public void SetRotateZ(GameObject myobj, float z)
	{
		Quaternion newRotation = myobj.transform.localRotation;
		newRotation.eulerAngles = new Vector3(newRotation.eulerAngles.x, newRotation.eulerAngles.y, z);
		myobj.transform.localRotation = newRotation;
	}

	/*!
	 * @brief 画像を軸中心に回す
	 * angleがそのままオブジェクトの角度になる
	 * */
	static public void SetRotate(GameObject myobj, float angle, string name)
	{
		{
			switch (name.ToLower())
			{
				// X軸を中心に回転
				case "x":
					myobj.transform.rotation = Quaternion.AngleAxis(angle, Vector3.right);
					break;
				// -X軸を中心に回転
				case "-x":
					myobj.transform.rotation = Quaternion.AngleAxis(angle, Vector3.left);
					break;
				// Y軸を中心に回転
				case "y":
					myobj.transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
					break;
				// -Y軸を中心に回転
				case "-y":
					myobj.transform.rotation = Quaternion.AngleAxis(angle, Vector3.down);
					break;
				// Z軸を中心に回転
				case "z":
					myobj.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
					break;
				// -Z軸を中心に回転
				case "-z":
					myobj.transform.rotation = Quaternion.AngleAxis(angle, Vector3.back);
					break;

				////////////////////////////////////////////////////////////////////////////////////////////////////////////

				// XY軸を中心に回転
				case "xy":
				case "yx":
					myobj.transform.rotation = Quaternion.AngleAxis(angle, Vector3.right + Vector3.up);
					break;

				// -XY軸を中心に回転
				case "-xy":
				case "y-x":
					myobj.transform.rotation = Quaternion.AngleAxis(angle, Vector3.left + Vector3.up);
					break;

				// X-Y軸を中心に回転
				case "x-y":
				case "-yx":
					myobj.transform.rotation = Quaternion.AngleAxis(angle, Vector3.right + Vector3.down);
					break;

				// -X-Y軸を中心に回転
				case "-x-y":
				case "-y-x":
					myobj.transform.rotation = Quaternion.AngleAxis(angle, Vector3.left + Vector3.down);
					break;

				////////////////////////////////////////////////////////////////////////////////////////////////////////////

				// XZ軸を中心に回転
				case "xz":
				case "zx":
					myobj.transform.rotation = Quaternion.AngleAxis(angle, Vector3.right + Vector3.forward);
					break;

				// -XZ軸を中心に回転
				case "-xz":
				case "z-x":
					myobj.transform.rotation = Quaternion.AngleAxis(angle, Vector3.left + Vector3.forward);
					break;

				// X-Z軸を中心に回転
				case "x-z":
				case "-zx":
					myobj.transform.rotation = Quaternion.AngleAxis(angle, Vector3.right + Vector3.back);
					break;

				// -X-Z軸を中心に回転
				case "-x-z":
				case "-z-x":
					myobj.transform.rotation = Quaternion.AngleAxis(angle, Vector3.left + Vector3.back);
					break;

				////////////////////////////////////////////////////////////////////////////////////////////////////////////

				// YZ軸を中心に回転
				case "yz":
				case "zy":
					myobj.transform.rotation = Quaternion.AngleAxis(angle, Vector3.up + Vector3.forward);
					break;

				// -YZ軸を中心に回転
				case "-yz":
				case "z-y":
					myobj.transform.rotation = Quaternion.AngleAxis(angle, Vector3.down + Vector3.forward);
					break;

				// Y-Z軸を中心に回転
				case "y-z":
				case "-zy":
					myobj.transform.rotation = Quaternion.AngleAxis(angle, Vector3.up + Vector3.back);
					break;

				// -Y-Z軸を中心に回転
				case "-y-z":
				case "-z-y":
					myobj.transform.rotation = Quaternion.AngleAxis(angle, Vector3.down + Vector3.back);
					break;

				////////////////////////////////////////////////////////////////////////////////////////////////////////////
				case "xyz":
				case "xzy":
				case "yxz":
				case "yzx":
				case "zxy":
				case "zyx":
					myobj.transform.rotation = Quaternion.AngleAxis(angle, Vector3.right + Vector3.up + Vector3.forward);
					break;

				case "-xyz":
				case "-xzy":
				case "y-xz":
				case "yz-x":
				case "z-xy":
				case "zy-x":
					myobj.transform.rotation = Quaternion.AngleAxis(angle, Vector3.left + Vector3.up + Vector3.forward);
					break;

				case "x-yz":
				case "xz-y":
				case "-yxz":
				case "-yzx":
				case "zx-y":
				case "z-yx":
					myobj.transform.rotation = Quaternion.AngleAxis(angle, Vector3.right + Vector3.down + Vector3.forward);
					break;


				case "xy-z":
				case "x-zy":
				case "yx-z":
				case "y-zx":
				case "-zxy":
				case "-zyx":
					myobj.transform.rotation = Quaternion.AngleAxis(angle, Vector3.right + Vector3.up + Vector3.back);
					break;

				case "-x-yz":
				case "-xz-y":
				case "-y-xz":
				case "-yz-x":
				case "z-x-y":
				case "z-y-x":
					myobj.transform.rotation = Quaternion.AngleAxis(angle, Vector3.left + Vector3.down + Vector3.forward);
					break;

				case "-xy-z":
				case "-x-zy":
				case "y-x-z":
				case "y-z-x":
				case "-z-xy":
				case "-zy-x":
					myobj.transform.rotation = Quaternion.AngleAxis(angle, Vector3.left + Vector3.up + Vector3.back);
					break;

				case "x-y-z":
				case "x-z-y":
				case "-yx-z":
				case "-y-zx":
				case "-zx-y":
				case "-z-yx":
					myobj.transform.rotation = Quaternion.AngleAxis(angle, Vector3.right + Vector3.down + Vector3.back);
					break;

				case "-x-y-z":
				case "-x-z-y":
				case "-y-x-z":
				case "-y-z-x":
				case "-z-x-y":
				case "-z-y-x":
					myobj.transform.rotation = Quaternion.AngleAxis(angle, Vector3.left + Vector3.down + Vector3.back);
					break;
				////////////////////////////////////////////////////////////////////////////////////////////////////////////


				////////////////////////////////////////////////////////////////////////////////////////////////////////////
			}
		}
	}
}