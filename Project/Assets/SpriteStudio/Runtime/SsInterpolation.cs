/**
	SpriteStudioPlayer
	
	Interpolation functions
	
	Copyright(C) 2003-2013 Web Technology Corp. 
	
*/

using UnityEngine;
using System;
using System.IO;
using System.Text;

public interface SsInterpolatable
{
	SsInterpolatable GetInterpolated(SsCurveParams curve, float time, SsInterpolatable startValue, SsInterpolatable endValue, int startTime, int endTime);
	SsInterpolatable Interpolate(SsCurveParams curve, float time, SsInterpolatable startValue, SsInterpolatable endValue, int startTime, int endTime);
}

public class SsInterpolation
{
	static public float Linear(float cur, float start, float end)
	{
		return start + (end - start) * cur;
	}

	static public int Linear(float cur, int istart, int iend)
	{
		float start = (float)istart;
		float end = (float)iend;
		return (int)(start + (end - start) * cur);
	}

	static public float Hermite(
		float fTime,					// time to get the value
		float fStartV, float fEndV,		// value of the nearest previous and next keys away from specified time.
		float fSParamV, float fEParamV)	// start and end value at handle point
	{
		float fTempTime = fTime;
		if ( fTempTime > 1.0f )
		{
			fTempTime = 1.0f;
		}
		if ( fTempTime < 0.0f )
		{
			fTempTime = 0.0f;
		}
		
		float fTemp1 = fTime;
		float fTemp2 = fTemp1 * fTemp1;
		float fTemp3 = fTemp2 * fTemp1;
		float fRet = ( 2 * fTemp3 - 3 * fTemp2 + 1 ) * fStartV +
				( -2 * fTemp3 + 3 * fTemp2 ) * fEndV +
				( fTemp3 - 2 * fTemp2 + fTemp1 ) * (fSParamV - fStartV) +
				( fTemp3 - fTemp2 ) * (fEParamV - fEndV);
		
		return fRet;
	}
	
	static public int Hermite(
		float fTime,					// time to get the value
		int fStartV, int fEndV,			// value of the nearest previous and next keys away from specified time.
		float fSParamV, float fEParamV)	// start and end value at handle point
	{
		return (int)Hermite(fTime, (float)fStartV, (float)fEndV, fSParamV, fEParamV);
	}

	static public float Bezier(
		float fTime,					// time to get the value
		float fStartT, float fStartV,	// time and value of the nearest previous key away from specified time.
		float fEndT, float fEndV,		// time and value of the nearest next key away from specified time.
		float fSParamT, float fSParamV,	// start time and value at handle point
		float fEParamT, float fEParamV)	// end time and value at handle point
	{
		float fTempTime = fTime;
		if ( fTempTime > 1.0f )
		{
			fTempTime = 1.0f;
		}
		if ( fTempTime < 0.0f )
		{
			fTempTime = 0.0f;
		}
		
		float fCurrentPos = ( fEndT - fStartT ) * fTime + fStartT;
		float fRet = fEndV;
		float fCurrentCalc = 0.5f;
		float fCalcRange = 0.5f;

		float fTemp1;
		float fTemp2;
		float fTemp3;
		
		float fCurrentX;

		for(int iLoop = 0; iLoop < 8; iLoop++ )
		{// more count of loop, better precision increase
			fTemp1 = 1.0f - fCurrentCalc;
			fTemp2 = fTemp1 * fTemp1;
			fTemp3 = fTemp2 * fTemp1;
			fCurrentX = ( fTemp3 * fStartT ) +
						( 3 * fTemp2 * fCurrentCalc * (fSParamT + fStartT) ) +
						( 3 * fTemp1 * fCurrentCalc * fCurrentCalc * (fEParamT + fEndT) ) +
						( fCurrentCalc * fCurrentCalc * fCurrentCalc * fEndT );

			fCalcRange /= 2.0f;
			if( fCurrentX > fCurrentPos )
			{
				fCurrentCalc -= fCalcRange;
			}
			else
			{
				fCurrentCalc += fCalcRange;
			}
		}
		
		// finally calculate with current value
		fTemp1 = 1.0f - fCurrentCalc;
		fTemp2 = fTemp1 * fTemp1;
		fTemp3 = fTemp2 * fTemp1;
		fRet = ( fTemp3 * fStartV ) +
					( 3 * fTemp2 * fCurrentCalc * (fSParamV + fStartV) ) +
					( 3 * fTemp1 * fCurrentCalc * fCurrentCalc * (fEParamV + fEndV) ) +
					( fCurrentCalc * fCurrentCalc * fCurrentCalc * fEndV );
		
		return fRet;
	}
	
	// time: must be normalized(0~1)
	static public float Interpolate<T>(SsCurveParams curve, float time, T startValue, T endValue, int startTime, int endTime)
	{
#if false
		if ( !(startValue is float) )
			if ( !(startValue is int) )
				SsDebugLog.PrintLn(startValue.GetType().ToString());
#endif
		switch (curve.Type)
		{
		case SsInterpolationType.Linear:
			return Linear(time, Convert.ToSingle(startValue), Convert.ToSingle(endValue));
		case SsInterpolationType.Hermite:
			return Hermite(time, Convert.ToSingle(startValue), Convert.ToSingle(endValue), curve.StartV, curve.EndV);
		case SsInterpolationType.Bezier:
			return Bezier(time, startTime, Convert.ToSingle(startValue), endTime, Convert.ToSingle(endValue), curve.StartT, curve.StartV, curve.EndT, curve.EndV);
		default:
			return Convert.ToSingle(startValue);
		}
	}

	static public int Interpolate(SsCurveParams curve, float time, int startValue, int endValue, int startTime, int endTime)
	{
		return (int)Interpolate(curve, time, (float)startValue, (float)endValue, startTime, endTime);
	}

	// returns a derived value interpolated from "prev" to "next" at "time".
	static public object	InterpolateKeyValue(SsKeyFrameInterface prevKey, SsKeyFrameInterface nextKey, int time)
	{
		// just returns a start value if prevKey has no curve.
		if (prevKey.Curve.IsNone)
			return prevKey.ObjectValue;

		// get times at nearest keys away from specified time.
		int startTime = prevKey.Time;
		int endTime = nextKey.Time;

		float now = 0f;
		if (startTime < endTime)
			now = ((float)(time - startTime)) / (endTime - startTime);
		
		SsInterpolatable interpolatable = prevKey.ObjectValue as SsInterpolatable;
		if (interpolatable == null)
			return Interpolate(prevKey.Curve, now, prevKey.ObjectValue, nextKey.ObjectValue, startTime, endTime);
		else
			return (object)interpolatable.GetInterpolated(prevKey.Curve, now, (SsInterpolatable)prevKey.ObjectValue, (SsInterpolatable)nextKey.ObjectValue, startTime, endTime);
	}
}
