using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIController : MonoBehaviour {
	public PlayerInfo _PlayerInfo;
	public PlayerClass _PlayerClass;

	public Text _NameValue;
	public Text _ClassValue;
	public Text _LevelValue;

	public Text _HitPointsValue;
	public Text _MagicPointsValue;

	public void Update()
	{
		if (_PlayerInfo != null && _PlayerInfo.Dirty)
		{
			_NameValue.text = _PlayerInfo._CharacterName;

			_PlayerInfo.Dirty = false;
		}

		if (_PlayerClass != null && _PlayerClass.Dirty)
		{
			_ClassValue.text = _PlayerClass._ClassName;
			_LevelValue.text = _PlayerClass._Level.ToString();

			_HitPointsValue.text = _PlayerClass._CurrentHitPoints.ToString() + "/" + _PlayerClass._MaximumHitPoint.ToString();
			_MagicPointsValue.text = _PlayerClass._CurrentMagicPoints.ToString() + "/" + _PlayerClass._MaximumMagicPoints.ToString();

			_PlayerClass.Dirty = false;
		}
	}
}
