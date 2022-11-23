using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeCtrl : SaiBehaviour
{
    public TreeLevel treeLevel;
    public WorkerCtrl choper;
    public LogwoodGenerator logwoodGenerator;

    protected virtual void LoadTreeLevel()
    {
        if (this.treeLevel != null) return;
        this.treeLevel = GetComponent<TreeLevel>();
       
    }
    protected virtual void LoadLogwoodGenerator()
    {
        if (this.logwoodGenerator != null) return;
        this.logwoodGenerator = GetComponent<LogwoodGenerator>();
        
    }
}
