using UnityEngine;
using System.Collections;

public class Stat_Manager : MonoBehaviour
{

    public int p1bombLimit = 1;

    public int p1bombsPlaced = 0; 

	public int p2bombLimit = 1;

	public int p2bombsPlaced = 0; 

    public int maxBombs = 5;

    public int playerSpeed = 2;

	public int player2Speed = 2;

    public int maxSpeed = 8;

    public int p1bombRange = 1;

	public int p2bombRange = 1;

    public int maxRange = 14;

	public int GetBombLimit(BlockType player)
    {
		if (player == BlockType.player)
			return p1bombLimit;
		else
			return p2bombLimit;
    }

	public int GetPlayerSpeed(BlockType player)
    {
		if (player == BlockType.player)
			return playerSpeed;
		else
			return player2Speed;
    }

	public int GetBombRange(BlockType player)
    {
		if (player == BlockType.player)
			return p1bombRange;
		else
			return p2bombRange;
    }


	public void IncreaseBLimit(BlockType player)
    {
		if (player == BlockType.player) {
			if (p1bombLimit != maxBombs) {
				p1bombLimit++;
			}
		} else {
			if (p2bombLimit != maxBombs) {
				p2bombLimit++;
			}
		}
    }

	public void IncreasePSpeed(BlockType player)
    {
		if (player == BlockType.player) {
			if (playerSpeed != maxSpeed) {
				playerSpeed++;
			}
		} else {
			if (player2Speed != maxSpeed) {
				player2Speed++;
			}
		}
    }

	public void IncreaseBRange(BlockType player)
    {
		if (player == BlockType.player) {
			if (p1bombRange != maxRange) {
				p1bombRange++;
			}
		} else {
			if (p2bombRange != maxRange) {
				p2bombRange++;
			}
		}
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


 


}
