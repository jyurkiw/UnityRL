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
	public PlayerInfo playerInfo;

	public bool Dirty { get; set; }

	public void Awake()
	{
		_Class = null;
		playerInfo = GetComponent<PlayerInfo>();
		Dirty = false;
	}

	public void SetPlayerClass(string className)
	{
		switch (className)
		{
			case "Warrior":
				_Class = new WarPH();
				break;
			default:
				break;
		}
	}

	public void UpdatePlayerData()
	{
		if (_Class != null)
		{
			_ClassName = _Class.ClassName;
			IRace race = playerInfo._Race;

			if (_MaximumHitPoints != _Class.MaxHP(_Level, race.HPPerLevel))
			{
				_MaximumHitPoints = _Class.MaxHP(_Level, race.HPPerLevel);
				_CurrentHitPoints += _Class.HPIncrease(_Level, race.HPPerLevel);
			}

			if (_MaximumMagicPoints != _Class.MaxMP(_Level, race.MPPerLevel))
			{
				_MaximumMagicPoints = _Class.MaxMP(_Level, race.MPPerLevel);
				_CurrentMagicPoints += _Class.MPIncrease(_Level, race.MPPerLevel);
			}
		}
	}

	public void PlayButtonAction()
	{
		if (!Dirty && !playerInfo.Dirty && _Class != null && playerInfo._Race != null)
		{
			GameObject.Find("CharacterCreationPanel").SetActive(false);
			PlayerController player = GetComponent<PlayerController>();
			player.State = PlayerState.WAIT_FOR_INPUT;
		}
	}
}
