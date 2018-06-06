using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BombExplosion : MonoBehaviour {

    public Stat_Manager sm;
    public BlockManager bm;
    public int range;
    public int delay;
	public bool setByPlayer1;
    public Player_Controller player1;
   	public Player_Controller player2;

    protected int bmbRow;
    protected int bmbCol;

	// Use this for initialization
	void Start () {
		if (setByPlayer1) {
			bmbCol = bm.GetPlayerColPos (BlockType.player);
			bmbRow = bm.GetPlayerRowPos (BlockType.player);
			range = sm.GetBombRange(BlockType.player);
		} else {
			bmbCol = bm.GetPlayerColPos (BlockType.player2);
			bmbRow = bm.GetPlayerRowPos (BlockType.player2);
			range = sm.GetBombRange(BlockType.player2);
		}
		player1 = GameObject.Find("Player1").GetComponent<Player_Controller>();
		player2 = GameObject.Find("Player2").GetComponent<Player_Controller>();

        StartCoroutine(Delay());
    }
	
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(delay);

        Explosion();
        DestroyObject(gameObject);
    }

   public void Explosion()
   {
		if (setByPlayer1)
			sm.p1bombsPlaced -= 1;
		else
			sm.p2bombsPlaced -= 1;

        //up
        for (int w = 0; w <= range; w++)
        {
            BlockType type = bm.CheckType(bmbRow, bmbCol - w);

            if (type == BlockType.unbreakable) break;

            if (type == BlockType.breakable)
            {
                bm.UpdateBlock(bmbRow, bmbCol - w);
                break;
            }

            if (type == BlockType.player)
            {
				if(bmbCol - w == player1.playerY && bmbRow == player1.playerX){
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }


            }

			if (type == BlockType.player2)
			{
				if(bmbCol - w == player2.playerY && bmbRow == player2.playerX){
					SceneManager.LoadScene(SceneManager.GetActiveScene().name);
				}


			}

            //if (bm.CheckBreak(bmbRow, bmbCol - w))
            //{
            //    bm.UpdateBlock(bmbRow, bmbCol - w);
            //    break;
            //}

            //if (bm.CheckUnbreak(bmbRow, bmbCol - w))
            //{
            //    break;
            //}

            //if (bm.CheckPlayer(bmbRow, bmbCol - w))
            //{
            //    bm.UpdateBlock(bmbRow, bmbCol - w);
            //    break;
            //}
        }

        //down
        for (int s = 0; s <= range; s++)
        {
            BlockType type = bm.CheckType(bmbRow, bmbCol + s);

            if (type == BlockType.unbreakable) break;

            if (type == BlockType.breakable)
            {
                bm.UpdateBlock(bmbRow, bmbCol + s);
                break;
            }

            if (type == BlockType.player)
            {
                if (bmbCol + s == player1.playerY && bmbRow == player1.playerX)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
            }

			if (type == BlockType.player2)
			{
				if (bmbCol + s == player2.playerY && bmbRow == player2.playerX)
				{
					SceneManager.LoadScene(SceneManager.GetActiveScene().name);
				}
			}

            //if (bm.CheckBreak(bmbRow, bmbCol + s))
            //{
            //    bm.UpdateBlock(bmbRow, bmbCol + s);
            //    break;
            //}

            //if (bm.CheckUnbreak(bmbRow, bmbCol + s))
            //{
            //    break;
            //}

            //if (bm.CheckPlayer(bmbRow, bmbCol + s))
            //{
            //    bm.UpdateBlock(bmbRow, bmbCol + s);
            //    break;
            //}
        }

        // left
        for (int a = 0; a <= range; a++)
        {
            BlockType type = bm.CheckType(bmbRow - a, bmbCol);

            if (type == BlockType.unbreakable) break;

            if (type == BlockType.breakable)
            {
                bm.UpdateBlock(bmbRow - a, bmbCol);
                break;
            }

            if (type == BlockType.player)
            {
                if (bmbCol == player1.playerY && bmbRow - a == player1.playerX)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
            }

			if (type == BlockType.player2)
			{
				if (bmbCol == player2.playerY && bmbRow - a == player2.playerX)
				{
					SceneManager.LoadScene(SceneManager.GetActiveScene().name);
				}
			}

            //if (bm.CheckBreak(bmbRow - a, bmbCol))
            //{
            //    bm.UpdateBlock(bmbRow - a, bmbCol);
            //}

            //if (bm.CheckUnbreak(bmbRow - a, bmbCol ))
            //{
            //    break;
            //}

            //if (bm.CheckPlayer(bmbRow - a, bmbCol))
            //{
            //    bm.UpdateBlock(bmbRow - a, bmbCol);
            //}
        }

        //right
        for (int d = 0; d <= range; d++)
        {
            BlockType type = bm.CheckType(bmbRow + d, bmbCol);

            if (type == BlockType.unbreakable) break;

            if (type == BlockType.breakable)
            {
                bm.UpdateBlock(bmbRow + d, bmbCol);
                break;
            }

            if (type == BlockType.player)
            {
                if (bmbCol == player1.playerY && bmbRow + d == player1.playerX)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
            }

			if (type == BlockType.player2)
			{
				if (bmbCol == player2.playerY && bmbRow + d == player2.playerX)
				{
					SceneManager.LoadScene(SceneManager.GetActiveScene().name);
				}
			}

            //if (bm.CheckBreak(bmbRow + d, bmbCol))
            //{
            //    bm.UpdateBlock(bmbRow + d, bmbCol);
            //    break;
            //}

            //if (bm.CheckUnbreak(bmbRow + d, bmbCol))
            //{
            //    break;
            //}


            //if (bm.CheckPlayer(bmbRow + d, bmbCol))
            //{
            //    bm.UpdateBlock(bmbRow + d, bmbCol);
            //    break;
            //}
        }
    }
}

