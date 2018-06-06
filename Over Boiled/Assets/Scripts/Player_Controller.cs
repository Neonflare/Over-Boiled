using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Player_Controller : MonoBehaviour
{


    protected Vector3 targetPos;
    protected int lastPosX;
    protected int lastPosY;

    public int playerX;
    public int playerY;
    public float speed;
	public bool amIPlayer1;
    public BlockManager bm;
    public Stat_Manager sm;
    public BombExplosion bombPrefab;



    // Use this for initialization
    void Start()
    {
        targetPos = transform.position;
		if (amIPlayer1)
			speed = sm.GetPlayerSpeed (BlockType.player);
		else
			speed = sm.GetPlayerSpeed (BlockType.player2);

    }

    // Update is called once per frame
    void Update()
    {
		if (amIPlayer1) {
			playerX = bm.GetPlayerRowPos (BlockType.player);
			playerY = bm.GetPlayerColPos (BlockType.player);
		} else {
			playerX = bm.GetPlayerRowPos (BlockType.player2);
			playerY = bm.GetPlayerColPos (BlockType.player2);
		}
        if (transform.position == targetPos)
        {
			if (amIPlayer1) {
				if (Input.GetKeyDown ("w")) {                
					MoveUp (playerX, playerY);               
				}

				if (Input.GetKeyDown ("s")) {
					MoveDown (playerX, playerY);                
				}

				if (Input.GetKeyDown ("a")) {                
					MoveLeft (playerX, playerY);
				}

				if (Input.GetKeyDown ("d")) {                
					MoveRight (playerX, playerY);                
				}

				if (Input.GetKeyDown ("space")) {
					if (sm.p1bombsPlaced != sm.p1bombLimit) {
						sm.p1bombsPlaced += 1;
						Vector3 bombPos = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
						BombExplosion bomb = Instantiate (bombPrefab, bombPos, Quaternion.identity) as BombExplosion;
						if (bomb != null) {
							bomb.bm = this.bm;
							bomb.sm = this.sm;
							bomb.setByPlayer1 = true;
						}
					}
				}
			}

			if (!amIPlayer1) {
				if (Input.GetKeyDown ("up")) {                
					MoveUp (playerX, playerY);               
				}

				if (Input.GetKeyDown ("down")) {
					MoveDown (playerX, playerY);                
				}

				if (Input.GetKeyDown ("left")) {                
					MoveLeft (playerX, playerY);
				}

				if (Input.GetKeyDown ("right")) {                
					MoveRight (playerX, playerY);                
				}

				if (Input.GetKeyDown ("enter")) {
					if (sm.p2bombsPlaced != sm.p2bombLimit) {
						sm.p2bombsPlaced += 1;
						Vector3 bombPos = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
						BombExplosion bomb = Instantiate (bombPrefab, bombPos, Quaternion.identity) as BombExplosion;
						if (bomb != null) {
							bomb.bm = this.bm;
							bomb.sm = this.sm;
							bomb.setByPlayer1 = false;
						}
					}
				}
			}


            if (Input.GetKeyDown("escape"))
            {
                Application.Quit();
            }

			if (Input.GetKeyDown("r")){
				SceneManager.LoadScene(SceneManager.GetActiveScene().name);
			}
        }
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position , targetPos, step);
        bm.UpdateBlock(lastPosX, lastPosY);

    }
    public void MoveUp(int x, int y)
    {
        if (bm.CheckEmpty(x, y - 1))
        {
            lastPosX = x;
            lastPosY = y;
            targetPos.z = transform.position.z + bm.GetGridSize();
			if (y != 0) {
				if (amIPlayer1)
					bm.UpdatePlayerPos (x, y, x, y - 1, BlockType.player);
				else {
					bm.UpdatePlayerPos (x, y, x, y - 1, BlockType.player2);
				}
			}
        }

    }

    public void MoveDown(int x, int y)
    {
        if (bm.CheckEmpty(x, y + 1))
        {
            lastPosX = x;
            lastPosY = y;
            targetPos.z = transform.position.z - bm.GetGridSize();
			if (y != 14) {
				if (amIPlayer1)
					bm.UpdatePlayerPos (x, y, x, y + 1, BlockType.player);
				else {
					bm.UpdatePlayerPos (x, y, x, y + 1, BlockType.player2);
				}
			}
        }
    }

    public void MoveLeft(int x, int y)
    {
        if (bm.CheckEmpty(x - 1, y))
        {
            lastPosX = x;
            lastPosY = y;
            targetPos.x = transform.position.x - bm.GetGridSize();
			if (x != 0) {
				if (amIPlayer1)
					bm.UpdatePlayerPos (x, y, x - 1, y, BlockType.player);
				else {
					bm.UpdatePlayerPos (x, y, x - 1, y, BlockType.player2);
				}
			}
        }
    }

    public void MoveRight(int x, int y)
    {
        if (bm.CheckEmpty(x + 1, y))
        {
            lastPosX = x;
            lastPosY = y;
            targetPos.x = transform.position.x + bm.GetGridSize();
			if (x != 14) {
				if (amIPlayer1)
					bm.UpdatePlayerPos (x, y, x + 1, y, BlockType.player);
				else {
					bm.UpdatePlayerPos (x, y, x + 1, y, BlockType.player2);
				}
			}
        }
    }
}

