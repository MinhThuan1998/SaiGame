using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResGenerator : Warehouse
{
    
    [SerializeField] protected List<Resource> resCreate;
    [SerializeField] protected List<Resource> resRequire;
    [SerializeField] protected float createTimer = 0f;
    [SerializeField] protected float createDelay = 2f;
    [SerializeField] protected int number = 1;

    public ResourceName resourceName;

    private void Start()
    {
        this.Generating();
    }
    protected override void FixedUpdate()
    {
        
        this.Creating();
       
    }

    protected virtual void Generating()
    {

        ResourceManager.instance.AddResource(ResourceName.gold, this.number);
    }
    protected override void LoadComponents()
    {
        this.LoadHolders();
    }
   

    protected virtual void Creating()
    {
        this.createTimer += Time.fixedDeltaTime;
        if (this.createTimer < this.createDelay) return;
        this.createTimer = 0;

        foreach(Resource res in this.resCreate)
        {
            ResHolder resHolder = this.resHolders.Find((holder) => holder.Name() == res.name);
            resHolder.Add(res.number);
        }
        

    }

    protected virtual bool IsRequireEnough()
    {
        if (this.resRequire.Count < 1) return true;

        return false;
    }

    public virtual ResHolder GetHolder(ResourceName name)
    {
        return this.resHolders.Find((holder) => holder.Name() == name);
    }

    public virtual float GetCreateDelay()
    {
        return this.GetCreateDelay();
    }

}
