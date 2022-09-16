using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class WorkerMovement : SaiBehaviour
{
    public WorkerCtrl workerCtrl;
  
    [SerializeField] protected Transform target;
    [SerializeField] protected NavMeshAgent navMeshAgent;
    [SerializeField] protected Animator animator;

    [SerializeField] protected bool isWalking = false;
    [SerializeField] protected bool isWorking = false;

    [SerializeField] protected float walkLimit = 0.7f;
    [SerializeField] protected float targetDistance = 0f;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadAgent();
        this.LoadAnimator();
        this.LoadWorkerCtrl();
        
    }
    protected override void FixedUpdate()
    {
        this.Moving();
        this.Animating();
        
    }

    protected virtual void LoadAgent()
    {
        if (this.navMeshAgent != null) return;
        this.navMeshAgent = GetComponent<NavMeshAgent>();

    }

    protected virtual void LoadAnimator ()
    {
        if (this.animator != null) return;
        this.animator = GetComponentInChildren<Animator>();


    }
    
    public virtual void SetTarget(Transform trans)
    {
       
        this.target = trans;
        


        /* if (this.target == null)
         {
             this.workerCtrl.navMeshAgent.enabled = false;
         }
         else
         {
             this.workerCtrl.navMeshAgent.enabled = true;
             this.IsClose2Target();
         }*/



    }
    protected virtual void LoadWorkerCtrl()
    {
        if (this.workerCtrl != null) return;
        this.workerCtrl = GetComponent<WorkerCtrl>();
       
    }
      public virtual Transform GetTarget()
    {
        return this.target;
       
    }
    protected virtual void Moving()
    {
        if (this.target == null  || this.IsClose2Target())
        {
            this.navMeshAgent.isStopped = true;
            this.isWalking = false;
            return;

        }
       
        this.isWalking = true;
        this.navMeshAgent.isStopped = false;
        this.navMeshAgent.SetDestination(this.target.position);
       

    }

    protected virtual void Animating()
    {
        this.animator.SetBool("isWalking", this.isWalking);
        this.animator.SetBool("isWorking", this.isWorking);

    }

    public virtual bool IsClose2Target()
    {
        if (this.target == null) return false;

        Vector3 targetPos = this.target.position;
        targetPos.y = transform.position.y;

        this.targetDistance = Vector3.Distance(transform.position, targetPos);
        return this.targetDistance < this.walkLimit;
    }





}
