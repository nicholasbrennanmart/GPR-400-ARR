using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Example3 : MonoBehaviour
{

    //what is being set
    public ComputeShader computeShader;
    ComputeBuffer computeBuffer;

    public GameObject cube;
    public GameObject cube2;

    //struct to match what is in compute shader
    public struct Test
    {

        Vector3 pos;
        Vector4 color;
    }

    //set up struct
    public Test[] data = new Test[2];

    // Start is called before the first frame update
    void Start()
    {

        //data[0].pos = cube.transform.position;
        //data[1].pos = cube2.transform.position;
        //data[0].color = cube2.GetComponent<Color>();
        //data[1].color = cube2.GetComponent<Color>();

        //tex = new RenderTexture(256, 256, 24);
        //tex.enableRandomWrite = true;
        //tex.Create();

        //shader.SetTexture(kernelHandle, "Result", tex);
        //shader.SetFloat("Resolution", tex.width);
        //shader.Dispatch(kernelHandle, 256 / 8, 256 / 8, 1);
    }

    // Update is called once per frame
    void Update()
    {

        //decide if you want to run on cpu or gpu
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {

            RunCPU();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {

            RunGPU();
        }
    }

    void RunCPU()
    {

        //if(data[1].pos.x > data[0].pos.x)
        //{

        //    data[0].color = Color.green;
        //}
    }

    void RunGPU()
    {

        //set up compute shader
        int totalSize = sizeof(float) * 4 + sizeof(float) * 3;
        ComputeBuffer cBuffer = new ComputeBuffer(data.Length, totalSize);
        cBuffer.SetData(data);

        //send data over to compute shader
        computeShader.SetBuffer(0, "Collision", cBuffer);
        computeShader.SetFloat("resolution", totalSize);
        computeShader.SetFloat("length", 1.0f);
        computeShader.SetInt("main", 0);
        computeShader.Dispatch(0, data.Length / 8, data.Length / 8, 1);

        //data comes back from compute shader
        cBuffer.GetData(data);

        //set data that came back
        //cube.transform.position = data[0].pos;
        //cube.GetComponent<Color>() = data[0].color;

        //close compute buffer
        cBuffer.Dispose();
    }
}
