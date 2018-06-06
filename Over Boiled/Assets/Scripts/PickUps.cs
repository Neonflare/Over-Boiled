using UnityEngine;
using System.Collections;

public class PickUps : MonoBehaviour {

    public Stat_Manager sm;

	public BlockManager bm;

	public int row;

	public int col;

    public bool pickupRange;

    public bool pickupLimit;

    public bool pickupSpeed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void RangeEffect(){
		if (bm.CheckType (row, col) == BlockType.player) {
			sm.IncreaseBRange (BlockType.player);
		} else
			sm.IncreaseBRange (BlockType.player2);
	}

	void LimitEffect(){
		if (bm.CheckType (row, col) == BlockType.player) {
			sm.IncreaseBLimit (BlockType.player);
		} else
			sm.IncreaseBLimit (BlockType.player2);
	}

	void SpeedEffect(){
		if (bm.CheckType (row, col) == BlockType.player) {
			sm.IncreasePSpeed (BlockType.player);
		} else
			sm.IncreasePSpeed (BlockType.player2);
	}

    void OnTriggerEnter(Collider player)
    {
	       if (pickupRange)
	        {
			RangeEffect ();
	        }
	        if (pickupLimit)
	        {
			LimitEffect ();
	        }
	        if (pickupSpeed)
	        {
			SpeedEffect ();
	        }

        Destroy(gameObject);
    }
}
