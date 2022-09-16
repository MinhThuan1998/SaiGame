using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerTask : SaiBehaviour
{
    public WorkerCtrl workerCtrl;
    
    
   
    [SerializeField] protected bool inHouse = false;
    [SerializeField] protected float buildingDistance = 0;
    [SerializeField] protected float buildDisLimit = 0.7f;

    [SerializeField] private Transform doorTarget;

    
    protected override void FixedUpdate()
    {
       
        base.FixedUpdate();

        if (this.GetBuilding()) this.GettingReadyForWork();
        else this.FindBuilding();

        if (workerCtrl.workerTasks.readyForTask) {
            //this.Working();
            
            
        } 
        

    }
    
    protected override void OnDisable()
    {
        base.OnDisable();
        this.GoOutBuilding();
       

    }
    protected virtual void GettingReadyForWork()
    {
        if (this.workerCtrl.workerTasks.readyForTask) return;

        if (!this.IsAtBuilding())
        {
            this.GotoBuilding();
            return;
        }
       
        this.workerCtrl.workerTasks.readyForTask = true;
        
        this.GoIntoBuilding();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadWorkerCtrl();
       
       
        
    }
  
    protected virtual void LoadWorkerCtrl()
    {
        if (this.workerCtrl != null) return;
        this.workerCtrl = GetComponent<WorkerCtrl>();
        
        Debug.Log(transform.name + ": LoadWorkerCtrl", gameObject);

    }

    protected virtual void WorkPlanning()
    {
        if (this.IsAtBuilding()) this.GoIntoBuilding();
        else this.GotoBuilding();

        if (this.inHouse) {
            //this.Working();
        } 
    }
    protected virtual void FindBuilding()
    {
        BuildingCtrl buildingCtrl = BuildingManager.instance.FindBuilding(transform, this.GetBuildingType());
        if (buildingCtrl == null) return;
        this.AssignBuilding(buildingCtrl);
        
    }

    protected virtual void Working()
    {
        this.GetBuilding().buildingTask.DoingTask(this.workerCtrl);
    }
    protected virtual BuildingCtrl GetBuilding()
    {
      
        return null;
    }

    protected virtual void AssignBuilding(BuildingCtrl buildingCtrl)
    {
        //For overide
    }

    protected virtual BuildingType GetBuildingType()
    {
        return BuildingType.workstation;
    }
    
    
    protected virtual void GoIntoBuilding()
    {
        if (this.workerCtrl.workerTasks.inHouse) return;

        this.workerCtrl.workerMovement.SetTarget(null);
        this.workerCtrl.workerTasks.inHouse = true;
        this.workerCtrl.workerModel.gameObject.SetActive(false);


    }
    public virtual bool IsAtBuilding()
    {
        return this.buildingDistance < this.buildDisLimit;
    }
    public virtual void GotoBuilding()
    {
        /*BuildingCtrl buildingCtrl = this.GetBuilding();
        this.workerCtrl.workerMovement.SetTarget(buildingCtrl.door);*/
        this.workerCtrl.workerMovement.SetTarget(doorTarget);





    }

    public virtual void GoOutBuilding()
    {
        this.inHouse = false;
        this.workerCtrl.workerModel.gameObject.SetActive(true);
    }
    
}
