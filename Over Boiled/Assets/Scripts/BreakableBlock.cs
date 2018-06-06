using UnityEngine;
using System.Collections;

public class BreakableBlock : MonoBehaviour {

    public Stat_Manager sm;
    public BlockManager bm;
    public PickUps range;
    public PickUps limit;
    public PickUps speed;
    public int row;
    public int col;

    protected float rand;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (bm.CheckEmpty(row, col))
            OnExplosion();
	}

    public void OnExplosion()
    {
        Vector3 pickupPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        rand = Random.Range(0.0f, 60.0f);
        if (rand <= 10.0f)
        {
            PickUps pickup = Instantiate(range, pickupPos, Quaternion.identity) as PickUps;
            if (pickup != null)
            {
				pickup.row = this.row;
				pickup.col = this.col;
                pickup.sm = this.sm;
				pickup.bm = this.bm;
                pickup.pickupRange = true;
                pickup.pickupLimit = false;
                pickup.pickupSpeed = false;
            }
        }
        if (rand <= 20.0f && rand > 10.0f)
        {
            PickUps pickup = Instantiate(limit, pickupPos, Quaternion.identity) as PickUps;
            if (pickup != null)
            {
				pickup.row = this.row;
				pickup.col = this.col;
                pickup.sm = this.sm;
				pickup.bm = this.bm;
                pickup.pickupRange = false;
                pickup.pickupLimit = true;
                pickup.pickupSpeed = false;
            }
        }
        if (rand <= 30.0f && rand > 20.0f)
        {
            PickUps pickup = Instantiate(speed, pickupPos, Quaternion.identity) as PickUps;
            if (pickup != null)
            {
				pickup.row = this.row;
				pickup.col = this.col;
                pickup.sm = this.sm;
				pickup.bm = this.bm;
                pickup.pickupRange = false;
                pickup.pickupLimit = false;
                pickup.pickupSpeed = true;
            }
        }


        DestroyObject(gameObject);
    }
}

