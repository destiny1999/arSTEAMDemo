using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorPicker : MonoBehaviour
{
	[SerializeField]
	Image _color;
	[SerializeField]
	Slider _hSilder;
	[SerializeField]
	Slider _sSilder;
	[SerializeField]
	Slider _vSilder;

	[SerializeField]
	Image _sColor;
	[SerializeField]
	Image _sMaskColor;
	[SerializeField]
	Image _vColor;

	float _h;
	float _s;
	float _v;
	public static ColorPicker Instance;
	private void Awake()
	{
		Instance = this;
		_hSilder.onValueChanged.AddListener(H_ValueChanged);
		_sSilder.onValueChanged.AddListener(S_ValueChanged);
		_vSilder.onValueChanged.AddListener(V_ValueChanged);

		_sSilder.onValueChanged.Invoke(_sSilder.value);
		_vSilder.onValueChanged.Invoke(_vSilder.value);
	}

	private void V_ValueChanged(float value)
	{
		_v = value;
		ToColor();
	}

	private void S_ValueChanged(float value)
	{
		_s = value;
		ToColor();
	}

	private void H_ValueChanged(float value)
	{
		_h = value;
		ToColor();
	}

	void ToColor()
	{
		Color rgb = Color.HSVToRGB(_h, _s, _v);
		_color.color = rgb;

		_sColor.color = Color.HSVToRGB(_h, 1, _v);
		_sMaskColor.color = new Color(_v, _v, _v, 1);
		_vColor.color = Color.HSVToRGB(_h, 1, 1);
	}
	public Color GetSelectedColor()
    {
		return _color.color;

	}
}