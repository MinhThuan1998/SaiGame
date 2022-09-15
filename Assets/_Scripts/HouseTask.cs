using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseTask : BuildingTask
{
    public override void DoingTask(WorkerCtrl workerCtrl)
    {
        if (this.IsTime2Work()) return;
        string message = workerCtrl.name + " " + transform.name;

    }


}
