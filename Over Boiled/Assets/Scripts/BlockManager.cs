using UnityEngine;
using System.Collections;

public enum BlockType { offGrid, empty, unbreakable, breakable, player, player2, playerPrvPos};

public class BlockManager : MonoBehaviour
{

    public Transform UnBreakable;

    public BreakableBlock Breakable;

    public BombExplosion bombPrefab;

    public Player_Controller playerPrefab;

    public Stat_Manager sm;

    public PickUps range;

    public PickUps limit;

    public PickUps speed;

    public int rows;

    public int columns;

    public int seed = 0;

    public bool testing = false;

    protected Vector3 blockSize = new Vector3(-1.0f, 0.0f, -1.0f);

    protected Vector3 startPos = new Vector3(12.0f, 0.62f, -12.0f);

    protected float rand;

    protected BlockType[,] levelGrid;

    protected int totalSpaces;

   

    

    // Use this for initialization
    void Start()
    {

        totalSpaces = rows * columns;

        if (testing)
            Random.InitState(seed);

        levelGrid = new BlockType[rows, columns];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                rand = Random.Range(0.0f, 20.0f);
                if (rand <= 9.0f)
                {
                    levelGrid[i, j] = BlockType.breakable;
                }
                if (rand <= 13.5f && rand > 9.0f)
                {
                    levelGrid[i, j] = BlockType.unbreakable;
                }
                if (rand <= 20 && rand > 13.5f)
                {
                    levelGrid[i, j] = BlockType.empty;
                }
            }
        }



        levelGrid[0, columns - 2] = BlockType.empty;
        levelGrid[0, columns - 1] = BlockType.player;
        levelGrid[1, columns - 1] = BlockType.empty;

        levelGrid[rows - 2, 0] = BlockType.empty;
		levelGrid[rows - 1, 0] = BlockType.player2;
        levelGrid[rows - 1, 1] = BlockType.empty;



     
           for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (levelGrid[i, j] == BlockType.breakable)
                    {
                        Vector3 breakPos = new Vector3(startPos.x + blockSize.x * i, startPos.y + blockSize.y, startPos.z - blockSize.z * j);
                        BreakableBlock block = Instantiate(Breakable, breakPos, Quaternion.identity) as BreakableBlock;
                        if (block != null)
                       {
                        block.bm = this;
                        block.sm = this.sm;
                        block.range = this.range;
                        block.limit = this.limit;
                        block.speed = this.speed;
                        block.row = i;
                        block.col = j;
                       }
                    }

                    if (levelGrid[i, j] == BlockType.unbreakable)
                    {
                        Vector3 unBreakPos = new Vector3(startPos.x + blockSize.x * i, startPos.y + blockSize.y, startPos.z - blockSize.z * j);
                        Instantiate(UnBreakable, unBreakPos , Quaternion.identity);
                    }

                    if (levelGrid[i, j] == BlockType.player)
                    {           
                        Vector3 playerPos = new Vector3(startPos.x + blockSize.x * i, startPos.y + blockSize.y, startPos.z - blockSize.z * j);
                        Player_Controller player = Instantiate(playerPrefab, playerPos, Quaternion.identity) as Player_Controller;
                        if (player != null)
                        {
							player.name = "Player1";
                            player.bm = this;
                            player.sm = this.sm;
                            player.bombPrefab = this.bombPrefab;
						player.amIPlayer1 = true;
                        }
                    }
				if (levelGrid [i, j] == BlockType.player2) {
					Vector3 playerPos = new Vector3(startPos.x + blockSize.x * i, startPos.y + blockSize.y, startPos.z - blockSize.z * j);
					Player_Controller player = Instantiate(playerPrefab, playerPos, Quaternion.identity) as Player_Controller;
					if (player != null)
					{
						player.name = "Player2";
						player.bm = this;
						player.sm = this.sm;
						player.bombPrefab = this.bombPrefab;
						player.amIPlayer1 = false;
					}
				}
					
                }
            }
          
       

    }

    // Update is called once per frame
    void Update()
    {
		
    }

    public float GetGridSize()
    {
        return blockSize.x;
    }

    public bool BlockIsValid(int x, int y) { return x >= 0 && x < rows && y >= 0 && y < columns; }

    public BlockType CheckType(int x, int y)
    {
        if (BlockIsValid(x, y))
            return levelGrid[x, y];
        else
            return BlockType.offGrid;
    }

    public bool CheckEmpty(int x, int y)
    {
        return CheckType(x, y) == BlockType.empty;
    }

    public BlockType CheckLeft(int x, int y)
    {
        return CheckType(x - 1, y);
    }

    public BlockType CheckRight(int x, int y)
    {
        return CheckType(x + 1, y);
    }

    public BlockType CheckUp(int x, int y)
    {
        return CheckType(x, y-1);
    }

    public BlockType CheckDown(int x, int y)
    {
        return CheckType(x, y + 1);
    }

	public int GetPlayerRowPos(BlockType player)
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                if (levelGrid[i, j] == player)
                    return i;                
            }
        }
        return -1;
    }



	public int GetPlayerColPos(BlockType player)
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                if (levelGrid[i, j] == player)
                    return j;
            }
        }
        return -1;
    }

	public void UpdatePlayerPos(int prvX, int prvY, int newX, int newY, BlockType player)
    {
        levelGrid[prvX, prvY] = BlockType.playerPrvPos;
        levelGrid[newX, newY] = player;
    }

    public void UpdateBlock(int x, int y)
    {
        levelGrid[x, y] = BlockType.empty;
    }

    public bool CheckBreak(int x, int y)
    {
        if (BlockIsValid(x, y))
        {
            if (levelGrid[x, y] == BlockType.breakable)
                return true;
            else
                return false;
        }
        else
            return false;
    }

    public bool CheckUnbreak(int x, int y)
    {
        if (BlockIsValid(x,y))
        {
            if (levelGrid[x, y] == BlockType.unbreakable)
                return true;
            else
                return false;
        }
        else
            return false;
    }

    public bool CheckPlayer(int x, int y)
    {
        if (BlockIsValid(x, y))
        {
            if (levelGrid[x, y] == BlockType.player)
                return true;
            else
                return false;
        }
        else
            return false;
    }

	public bool CheckPlayer2(int x, int y)
	{
		if (BlockIsValid(x, y))
		{
			if (levelGrid[x, y] == BlockType.player2)
				return true;
			else
				return false;
		}
		else
			return false;
	}
}



