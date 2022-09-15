using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingCtrl :SaiBehaviour
{
    public Workers workers;
    public Transform door;
    public BuildingTask buildingTask;
    public Warehouse warehouse;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadWorkers();
        this.LoadDoor();
        this.LoadBuldingTask();
        this.LoadWarehouse();
    }

    protected virtual void LoadWorkers()
    {
        if (this.workers != null) return;
        this.workers = GetComponent<Workers>();
       
    }
    protected virtual void LoadWarehouse()
    {
        if (this.warehouse != null) return;
        this.warehouse = GetComponent<Warehouse>();
       
    }

    protected virtual void LoadDoor()
    {
        if (this.door != null) return;
        this.door = transform.Find("Door");
        
    }
    protected virtual void LoadBuldingTask()
    {
        if (this.buildingTask != null) return;
        this.buildingTask = GetComponent<BuildingTask>();
       
    }

    public virtual void DoingTask()
    {
        
    }
}
