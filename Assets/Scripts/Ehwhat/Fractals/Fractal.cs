using UnityEngine;
using System.Collections;

public class Fractal : MonoBehaviour {

	Vector3[] directions = {
		Vector3.up,
		Vector3.down,
		Vector3.left,
		Vector3.right,
		Vector3.forward,
		Vector3.back
	};

	public Mesh mesh;
	public Material material;

	public int maxDepth;
	public float childScale;
	public int children;

	private int depth;
	private Vector3[] avaliableDirections;
	
	private MeshFilter mf;
	private MeshRenderer mr;

	// Use this for initialization
	void Start () {
		mf = gameObject.AddComponent<MeshFilter>();
		mr = gameObject.AddComponent<MeshRenderer>();
		mr.material = material;
		mf.mesh = mesh;
		if(depth < maxDepth)
			StartCoroutine("createChildren");
	}

	private void Init(Fractal parent){
		mesh = parent.mesh;
		material = parent.material;
		maxDepth = parent.maxDepth;
		depth = parent.depth+1;
		childScale = parent.childScale;
		children = parent.children;
		int direction = Random.Range(0,5);
		transform.localScale = Vector3.one*childScale;
		transform.localPosition = directions[direction]*(0.5f + 0.5f*childScale);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator createChildren(){
		for(int i = 0; i < children; i++){
			yield return new WaitForSeconds(0.5f);
			new GameObject("Fractal Child").AddComponent<Fractal>().Init(this);
		}
	}

	Vector3[] getNewDirections(int exclude){
		Vector3[] tempDirections = new Vector3[6];
		for(int i = 0; i < 6; i++){
			if(i == exclude){
				continue;
			}
			tempDirections[i] = directions[i];
		}
		return tempDirections;
	}

}
