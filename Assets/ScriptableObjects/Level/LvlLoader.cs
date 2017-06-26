using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Completed;

public abstract class LvlLoader : ScriptableObject
{
	public virtual void Init(BoardManager board, int lvl) { }
	public abstract void Load(BoardManager board);
}
