using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class HiddenDoor : MonoBehaviour
{
    public GameObject ParentHolder;
    private Vector3[] DoorCompPos;
    private Vector3[] ScaleValues;

    private int size;
    public ComputeShader Dothings;
    private ComputeBuffer Data;
    private ComputeBuffer Data1;
    private int GlobPosID;
    private int ObjPosID;
    private int ScaleID;
    private int SensID;
    [FormerlySerializedAs("se")] [SerializeField]
    private float Sensetivity;

    private Transform[] a;

    private void OnEnable()
    {
        a = ParentHolder.GetComponentsInChildren<Transform>();
        size = a.Length-1;
        Data = new ComputeBuffer(size, sizeof(float)*3 );
        Data1 = new ComputeBuffer(size, sizeof(float)*3 );
    }

    private void OnDisable()
    {
        Data.Release();
        Data = null;
    }

    // Start is called before the first frame update
    void Start()
    {
     
        GlobPosID = Shader.PropertyToID("globpos");
        ObjPosID = Shader.PropertyToID("ObjectPos");
        ScaleID = Shader.PropertyToID("Scales");
        SensID = Shader.PropertyToID("sensitivity");
      
        
    
        
        DoorCompPos = new Vector3[size];
        ScaleValues = new Vector3[size];

        
        for (int i = 0; i < size; i++)
        {
            DoorCompPos[i] = a[i+1].position;
        }
       Data.SetData(DoorCompPos);
       Dothings.SetBuffer(0,ObjPosID, Data);
       Dothings.SetBuffer(0, ScaleID, Data1);
       //change scales data type
    }

    // Update is called once per frame
    void Update()
    {
        

        Dothings.SetVector(GlobPosID, transform.position);
        Dothings.SetFloat(SensID, Sensetivity);
       
        Dothings.Dispatch(0, size,1,1);
        Data1.GetData(ScaleValues);
        for (int i = 0; i < size; i++)
        {
            a[i+1].transform.localScale = ScaleValues[i] ;
        }
        
        



    }
}
