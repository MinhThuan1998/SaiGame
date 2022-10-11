using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerTasks : SaiBehaviour
{
    public WorkerCtrl workerCtrl;
    public bool isNightTime = false;//TODO: it should not be here
    public bool inHouse = false;
    public bool readyForTask = false;
    public WorkerTask taskWorking;
    public WorkerTask taskGoHome;
    public Transform taskTarget;
    [SerializeField] protected List<TaskType> tasks;
    //public TaskWorking taskWorking;
    //public Transform taskTarget;
    //public BuildingCtrl taskBuildingCtrl;


    protected override void Awake()
    {
        base.Awake();
        
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

       
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

    protected virtual void LoadTasks()
    {
        if (this.taskWorking != null) return;
        Transform tasksObj = transform.Find("Tasks");
        this.taskWorking = tasksObj.GetComponentInChildren<TaskWorking>();
        this.taskGoHome = tasksObj.GetComponentInChildren<TaskGoHome>();
        Debug.Log(transform.name + ": LoadTasks", gameObject);
    }

    public virtual void TaskAdd(TaskType taskType)
    {
        TaskType currentTask = this.TaskCurrent();
        if (taskType == currentTask) return;
        this.tasks.Add(taskType);
    }

    public virtual void TaskCurrentDone()
    {
        this.tasks.RemoveAt(this.tasks.Count - 1);
    }
    public virtual TaskType TaskCurrent()
    {
        if (this.tasks.Count <= 0) return TaskType.none;
        return this.tasks[this.tasks.Count - 1];
    }




}
