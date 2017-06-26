using UnityEngine;
using System.Collections;

namespace Completed
{
	//Enemy inherits from MovingObject, our base class for objects that can move, Player also inherits from this.
	public class Enemy : MovingObject
	{
		public int playerDamage; 							//The amount of food points to subtract from the player when attacking.
		public AudioClip attackSound1;						//First of two audio clips to play when attacking the player.
		public AudioClip attackSound2;						//Second of two audio clips to play when attacking the player.
		public EnemyLoader eLoader;
		[HideInInspector]
		public Animator animator;							//Variable of type Animator to store a reference to the enemy's Animator component.

		private Enemy _instance = null;
		
		//Start overrides the virtual Start function of the base class.
		protected override void Start ()
		{
			if (_instance == null) {
				_instance = this;
			}
			GameManager.instance.AddEnemyToList (this);
			animator = GetComponent<Animator> ();
			eLoader.Init (_instance);
			base.Start ();
		}
		
		//MoveEnemy is called by the GameManger each turn to tell each Enemy to try to move towards the player.
		public void MoveEnemy ()
		{
			Vector2 pos = eLoader.Move (_instance);
			base.AttemptMove <Player> ((int) pos.x, (int) pos.y);
		}
		
		
		//OnCantMove is called if Enemy attempts to move into a space occupied by a Player, it overrides the OnCantMove function of MovingObject 
		//and takes a generic parameter T which we use to pass in the component we expect to encounter, in this case Player
		protected override void OnCantMove <T> (T component)
		{
			eLoader.CantMove (_instance, component, SoundManager.instance);
		}
	}
}
