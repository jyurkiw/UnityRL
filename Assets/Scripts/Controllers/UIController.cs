using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIController : MonoBehaviour {
    // Info UI
	public PlayerInfo _PlayerInfo;
	public PlayerClass _PlayerClass;

	public Text _NameValue;
	public Text _ClassValue;
	public Text _RaceValue;
	public Text _LevelValue;

	public Text _HitPointsValue;
	public Text _MagicPointsValue;

    // Floor UI
    public Text _FloorValue;

    private LevelGenerator _level;
    private LevelGenerator level
    {
        get
        {
            if (_level == null) _level = FindObjectOfType<LevelGenerator>();
            return _level;
        }
    }

	public void Update()
	{
		if (_PlayerInfo != null && _PlayerInfo.Dirty)
		{
			_NameValue.text = _PlayerInfo._CharacterName;

			if (_PlayerInfo._Race != null)
				_RaceValue.text = _PlayerInfo._Race.RaceName;

			_PlayerInfo.Dirty = false;
		}

		if (_PlayerClass != null && _PlayerClass.Dirty)
		{
			_PlayerClass.UpdatePlayerData();

			_ClassValue.text = _PlayerClass._ClassName;
			_LevelValue.text = _PlayerClass._Level.ToString();

			_HitPointsValue.text = _PlayerClass._CurrentHitPoints.ToString() + "/" + _PlayerClass._MaximumHitPoints.ToString();
			_MagicPointsValue.text = _PlayerClass._CurrentMagicPoints.ToString() + "/" + _PlayerClass._MaximumMagicPoints.ToString();

			_PlayerClass.Dirty = false;
		}

        if (level.Dirty && _FloorValue != null)
        {
            _FloorValue.text = level.Floor.ToString();
        }

	}
}
