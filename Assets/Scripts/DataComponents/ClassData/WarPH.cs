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

		public int MaxHP(int level)
		{
			return level * 8;
		}

		public int MaxMP(int level)
		{
			return 2;
		}

		public int HPIncrease(int level)
		{
			return 8;
		}

		public int MPIncrease(int level)
		{
			return 0;
		}
	}
}
