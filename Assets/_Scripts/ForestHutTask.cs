using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestHutTask : BuildingTask
{
    [Header("Forest Hut")]
    [SerializeField] protected GameObject plantTreeObj;
    [SerializeField] protected List<GameObject> treePrefabs;
    [SerializeField] protected float treeRange = 27f;
    [SerializeField] protected float treeDistance = 7f;
    [SerializeField] protected List<GameObject> trees;
    [SerializeField] protected int treeMax = 7;
    [SerializeField] protected float workingSpeed = 2;
    

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadObjects();
        this.LoadTrees();
    }

    protected virtual void LoadObjects()
    {
        if (this.plantTreeObj != null) return;
        this.plantTreeObj = Resources.Load<GameObject>("Building/MarkPosition");
        Debug.Log(transform.name + " LoadObjects", gameObject);

    }
    protected virtual void LoadTrees()
    {
        if (this.treePrefabs.Count > 0) return;
        GameObject tree1 = Resources.Load<GameObject>("Building/Tree1");
        GameObject tree2 = Resources.Load<GameObject>("Building/Tree1");
        GameObject tree3 = Resources.Load<GameObject>("Building/Tree1");
        this.treePrefabs.Add(tree1);
        this.treePrefabs.Add(tree2);
        this.treePrefabs.Add(tree3);
        Debug.Log(transform.name + " LoadObjects", gameObject);
    }
    public override void DoingTask(WorkerCtrl workerCtrl)
    {
        switch (workerCtrl.workerTasks.TaskCurrent())
        {
            case TaskType.plantTree:
                this.PlantTree(workerCtrl);
                break;
            case TaskType.chopTree:
                //this.ChopTree(workerCtrl);
                break;
            case TaskType.goToWorkStation:
                //this.BackToWorkStation(workerCtrl);
                break;
            default:
                if (this.IsTime2Work()) this.Planning(workerCtrl);
                break;
        }
    }

    protected virtual void PlantTree(WorkerCtrl workerCtrl)
    {
        Transform target = workerCtrl.workerMovement.GetTarget();

        if (target == null) target = this.GetPlantPlace();
        if (target == null) return;

        workerCtrl.workerTasks.taskWorking.GoOutBuilding();
        workerCtrl.workerMovement.SetTarget(target);

        if (workerCtrl.workerMovement.IsClose2Target())
        {
            workerCtrl.workerMovement.SetTarget(null);
            Destroy(target.gameObject);//TODO: not done yet
            this.Planting(workerCtrl.transform);

            if (!this.NeedMoreTree())
            {
                workerCtrl.workerTasks.TaskCurrentDone();
                workerCtrl.workerTasks.TaskAdd(TaskType.goToWorkStation);
            }
        }
    }

    protected virtual void ChopTree(WorkerCtrl workerCtrl)
    {
        if (workerCtrl.workerMovement.isWorking) return;
        StartCoroutine(Chopping(workerCtrl, workerCtrl.workerTasks.taskTarget));


    }
    private IEnumerator Chopping(WorkerCtrl workerCtrl, Transform tree)
    {
        workerCtrl.workerMovement.isWorking = true;
        yield return new WaitForSeconds(this.workingSpeed);
        TreeCtrl treeCtrl = tree.GetComponent<TreeCtrl>();
        //treeCtrl.treeLevel.ShowLastBuild();
        
        

    }
    protected virtual void Planning(WorkerCtrl workerCtrl)
    {
        if (this.NeedMoreTree()) workerCtrl.workerTasks.TaskAdd(TaskType.plantTree);
    }
    protected virtual bool NeedMoreTree()
    {
        return this.treeMax >= this.trees.Count;
    }
    protected virtual void Planting(Transform trans)
    {
        GameObject treePrefab = this.GetTreePrefab();
        GameObject treeObj = Instantiate<GameObject>(treePrefab);
        treeObj.transform.position = trans.position;
        treeObj.transform.rotation = trans.rotation;
        this.trees.Add(treeObj);
        TreeManager.instance.TreeAdd(treeObj);
    }
    protected virtual Transform GetPlantPlace()
    {
        Vector3 newTreePos = this.RandomPlaceForTree(); ;
        float dis = Vector3.Distance(transform.position, newTreePos);
        if (dis < this.treeDistance)
        {
            Debug.Log("GetPlantPlace Destroy GameObject");
            return null;
        }

        GameObject treePlace = Instantiate(this.plantTreeObj);
        treePlace.transform.position = newTreePos;

        return treePlace.transform;
    }
    protected virtual GameObject GetTreePrefab()
    {
        int rand = Random.Range(0, this.treePrefabs.Count);
        return this.treePrefabs[rand];
    }
    protected virtual Vector3 RandomPlaceForTree()
    {
        Vector3 position = transform.position;
        position.x += Random.Range(this.treeRange * -1, this.treeRange);
        position.y = 0;
        position.z += Random.Range(this.treeRange * -1, this.treeRange);

        return position;
    }

}
