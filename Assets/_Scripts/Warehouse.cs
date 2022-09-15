using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warehouse : SaiBehaviour
{
    [Header("Warehouse")]
    //public BuildingType buildingType = BuildingType.workStation;
    public BuildingType buildingType;
    [SerializeField] protected bool isFull = false;
    [SerializeField] protected List<ResHolder> resHolders;

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        
    }

    protected override void LoadComponents()
    {
        this.LoadHolders();
    }

    protected virtual void LoadHolders()
    {
        if (this.resHolders.Count > 0) return;

        Transform res = transform.Find("Res");
        foreach (Transform resTran in res)
        {
            Debug.Log(resTran.name);
            ResHolder resHolder = resTran.GetComponent<ResHolder>();
            if (resHolder == null) continue;
            this.resHolders.Add(resHolder);
        }

        Debug.Log(transform.name + ": LoadHolders", gameObject);
    }

    public virtual ResHolder GetResource(ResourceName name)
    {
        return this.resHolders.Find((holder) => holder.Name() == name);
    }

    

    

   

   

    

    public virtual ResHolder ResNeed2Move()
    {
        return null;
    }

    public virtual List<Resource> NeedResoures()
    {
        return new List<Resource>();//Do not return null
    }
}
