using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class PlaneGeneration : MonoBehaviour {

    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;


	// Use this for initialization
	void Start () {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        createShape();
        refreshMesh();
	}
	
    void createShape()
    {
        vertices = new Vector3[]
        {
            new Vector3(0, 0, 0),
            new Vector3(0, 0, 1),
            new Vector3(1, 0, 1),
            new Vector3(1, 0, 0),

        };

        triangles = new int[]
        {
            1, 3, 0,
            2, 3, 1
        };
    }

    void refreshMesh()
    {
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }

    // Update is called once per frame
    void Update () {
		
	}
}
