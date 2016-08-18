using UnityEngine;
using System.Collections;

// The IClass interface is a contract for class data.
// All class data is either looked up from a data file,
// or calculated somehow.
public interface IClass {
	string ClassName { get; }

	int MaxHP(int level);
	int MaxMP(int level);

	int HPIncrease(int level);
	int MPIncrease(int level);
}
