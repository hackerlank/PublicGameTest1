using UnityEngine;
using System.Collections;

public class ProceduralPlane : MonoBehaviour {

	// Size of our Plane!
	public int xSize, ySize;
	private Mesh mesh;

	// Storing the verticies of the plane
	private Vector3[] vertices;

	private void Awake() {
		// Generate the mesh!
		Generate();
	}

	private void Generate(){

		GetComponent<MeshFilter>().mesh = mesh = new Mesh();
		mesh.name = "Procedural Plane";

		// Generate one more vertex in each dimension, as each quad can share it's adjacent's verticies
		vertices = new Vector3[(xSize + 1) * (ySize + 1)];
		// Generate UV coordinates for each vertex
		Vector2[] uv = new Vector2[vertices.Length];
		// Generate tangents for each vertex, tangents work kinda like normals, but these complete the third
		// direction for the shader, allowing it to use bumpmapping
		// Don't worry about it's scary "Vector4"
		Vector4[] tangents = new Vector4[vertices.Length];
		// As our entire mesh is a flat surface, we can just set it to the same thing
		Vector4 tang = new Vector4(1f,0f,0f,-1f);
		for (int i = 0, y = 0; y <= ySize; y++) {
			for (int x = 0; x <= xSize; x++, i++) {
				vertices[i] = new Vector3(x-xSize/2, y-ySize/2);
				uv[i] = new Vector2((float)x / xSize, (float)y / ySize);
				tangents[i] = tang;
			}
		}
		mesh.vertices = vertices;
		mesh.uv = uv;
		mesh.tangents = tangents;

		int[] triangles = new int[xSize * ySize * 6];
		for (int ti = 0, vi = 0, y = 0; y < ySize; y++, vi++) {
			for (int x = 0; x < xSize; x++, ti += 6, vi++) {
				triangles[ti] = vi;
				triangles[ti + 3] = triangles[ti + 2] = vi + 1;
				triangles[ti + 4] = triangles[ti + 1] = vi + xSize + 1;
				triangles[ti + 5] = vi + xSize + 2;
			}
		}
		mesh.triangles = triangles;
		mesh.RecalculateNormals();
	}

	private void OnDrawGizmos(){
		if(vertices == null){
			return; //Get out if no verts
		}
		Gizmos.color = Color.blue;
		for(int i = 0; i < vertices.Length; i++){
			Gizmos.DrawCube(transform.TransformPoint(vertices[i]), Vector3.one*0.1f);
		}

	}

}
