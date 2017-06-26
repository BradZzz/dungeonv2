using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Completed;

[CreateAssetMenu(menuName="Levels/Basic")]
public class BasicLvlSetup : LvlLoader {

	public override void Init(BoardManager board, int lvl){
		initboard (board);
		initList (board);
		LayoutObjectAtRandom (board.wallTiles, board.wallCount.minimum, board.wallCount.maximum, board.gridPositions);
		LayoutObjectAtRandom (board.foodTiles, board.foodCount.minimum, board.foodCount.maximum, board.gridPositions);
		int enemyCount = (int) Mathf.Log(lvl, 2f);
		LayoutObjectAtRandom (board.enemyTiles, enemyCount, enemyCount, board.gridPositions);
		Instantiate (board.exit, new Vector3 (board.columns - 1, board.rows - 1, 0f), Quaternion.identity);
	}

	public override void Load(BoardManager board){

	}

	private void initboard(BoardManager board) {
		for(int x = -1; x < board.columns + 1; x++)
		{
			for(int y = -1; y < board.rows + 1; y++)
			{
				GameObject toInstantiate = board.floorTiles[Random.Range (0,board.floorTiles.Length)];
				if (x == -1 || x == board.columns || y == -1 || y == board.rows) {
					toInstantiate = board.outerWallTiles [Random.Range (0, board.outerWallTiles.Length)];
				}
				GameObject instance = Instantiate (toInstantiate, new Vector3 (x, y, 0f), Quaternion.identity) as GameObject;
				instance.transform.SetParent (board.boardHolder);
			}
		}
	}

	private void initList(BoardManager board)
	{
		board.gridPositions.Clear();
		for(int x = 1; x < board.columns-1; x++)
		{
			for(int y = 1; y < board.rows-1; y++)
			{
				board.gridPositions.Add (new Vector3(x, y, 0f));
			}
		}
	}

	Vector3 RandomPosition (List <Vector3> gridPositions)
	{
		int randomIndex = Random.Range (0, gridPositions.Count);
		Vector3 randomPosition = gridPositions[randomIndex];
		gridPositions.RemoveAt (randomIndex);
		return randomPosition;
	}

	private void LayoutObjectAtRandom (GameObject[] tileArray, int minimum, int maximum, List <Vector3> gridPositions)
	{
		int objectCount = Random.Range (minimum, maximum+1);
		for(int i = 0; i < objectCount; i++)
		{
			Vector3 randomPosition = RandomPosition(gridPositions);
			GameObject tileChoice = tileArray[Random.Range (0, tileArray.Length)];
			Instantiate(tileChoice, randomPosition, Quaternion.identity);
		}
	}
}
