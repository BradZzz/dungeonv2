using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Completed;

public abstract class EnemyLoader : ScriptableObject {
	public virtual void Init(Enemy enemy) { }
	public abstract Vector2 Move (Enemy enemy);
	public virtual void CantMove<T> (Enemy enemy, T component, SoundManager sManager) { }
}
