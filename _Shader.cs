using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Shader : MonoBehaviour
{
    // Start is called before the first frame update
    private int id;
    private int id2;
    [SerializeField] private int sensitivity;
    void Start()
    {
        id = Shader.PropertyToID("_globopos");
        id2= Shader.PropertyToID("_diver");
    }

    // Update is called once per frame
    void Update()
    {
        Shader.SetGlobalVector(id, transform.position);
        Shader.SetGlobalInt(id2, sensitivity);
    }
}
