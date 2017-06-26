using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Completed;

[CreateAssetMenu(menuName="Enemy/Basic")]
public class BasicEnemy : EnemyLoader {

	private Transform target;							//Transform to attempt to move toward each turn.
	private bool skipMove;								//Boolean to determine whether or not enemy should skip a turn or move this turn.

	public override void Init(Enemy enemy) { 
		target = GameObject.FindGameObjectWithTag ("Player").transform;
	}

	public override Vector2 Move(Enemy enemy) { 
		skipMove = !skipMove;
		if (!skipMove) {
			int xDir = 0;
			int yDir = 0;

			if (Mathf.Abs (target.position.x - enemy.transform.position.x) < float.Epsilon) {
				yDir = target.position.y > enemy.transform.position.y ? 1 : -1;
			} else {
				xDir = target.position.x > enemy.transform.position.x ? 1 : -1;
			}
			return new Vector2 (xDir, yDir);
		} 
		return enemy.transform.position;
	}

	public override void CantMove<T> (Enemy enemy, T component, SoundManager sManager){
		Player hitPlayer = component as Player;
		hitPlayer.LoseFood (enemy.playerDamage);
		enemy.animator.SetTrigger ("enemyAttack");
		sManager.RandomizeSfx (enemy.attackSound1, enemy.attackSound2);
	}
}
