using UnityEngine;
using System.Collections;
using Assets.Scripts.DataComponents.ClassData;

public class PlayerClass : MonoBehaviour {
	public string _ClassName;
	public int _Level;
	public int _MaximumHitPoints;
	public int _CurrentHitPoints;
	public int _MaximumMagicPoints;
	public int _CurrentMagicPoints;

	public IClass _Class;

	public bool Dirty = true;

	public void Awake()
	{
		_Class = new WarPH();
	}

	public void Update()
	{
		if (Dirty && _Class != null)
		{
			_ClassName = _Class.ClassName;

			if (_MaximumHitPoints != _Class.MaxHP(_Level))
			{
				_MaximumHitPoints = _Class.MaxHP(_Level);
				_CurrentHitPoints += _Class.HPIncrease(_Level);
			}

			if (_MaximumMagicPoints != _Class.MaxMP(_Level))
			{
				_MaximumMagicPoints = _Class.MaxMP(_Level);
				_CurrentMagicPoints += _Class.MPIncrease(_Level);
			}
		}
	}
}
