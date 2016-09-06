using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.DataComponents.ClassData
{
	// Warrior placeholder class.
	// Real class will have data loaded from a data file.
	public class WarPH : IClass
	{
		public string ClassName
		{
			get { return "Warrior"; }
		}

		public int MaxHP(int level, int raceMod)
		{
			return level * (8 + raceMod);
		}

		public int MaxMP(int level, int raceMod)
		{
			return 2 + (level * raceMod);
		}

		public int HPIncrease(int level, int raceMod)
		{
			return 8 + raceMod;
		}

		public int MPIncrease(int level, int raceMod)
		{
			return 0 + raceMod;
		}
	}
}
