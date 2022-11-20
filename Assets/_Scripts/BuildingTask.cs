using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingTask : SaiBehaviour
{
    public BuildingCtrl buildingCtrl;
    [SerializeField] protected float taskTimer = 0;
    [SerializeField] protected float taskDelay = 5f;

    [SerializeField] protected float treeTimer = 20f;
   

   

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBuildingCtrl();
        
    }

   

    protected virtual void LoadBuildingCtrl()
    {
        if (this.buildingCtrl != null) return;
        this.buildingCtrl = GetComponent<BuildingCtrl>();
    }
   


    protected virtual bool IsTime2Work()
    {
        this.taskTimer += Time.fixedDeltaTime;
        if (this.taskTimer < this.taskDelay) return false;
        this.taskTimer = 0;
        return true;
    }
    public virtual void DoingTask(WorkerCtrl workerCtrl)
    {
        //Debug.Log("DoingTaskkkkkkkkkkk");
    }
    
}
