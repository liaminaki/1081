// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class FieldofViewMesh : MonoBehaviour
// {
//     FieldOfView fow;
//     Mesh mesh;
//     RaycastHit2D hit;
//     [HideInInspector] public Vector3[] vertex;
//     [HideInInspector] public int[] triangle;
//     [HideInInspector] public int stepCount;

//     void Start(){
//         Mesh = GetComponent<MeshFilter>();
//         fow = GetComponent<FielOfView>();
//     }

//     void LateUpdate(){
//         MakeMesh();
//     }

//     void MakeMesh(){
//         stepCount = Mathf.RoundToInt(fow.viewAngle * meshRes);
//         float stepAngle = fow.viewAngle / stepCount;

//         List <Vector3> viewVertex = new List<Vector3>();

//         hit = new RaycastHit2D();

//         for (int i = 0; i <= stepCount; i++){
//             float angle = fow.transform.eulerAngles.y - fow.viewAngle / 2 + stepAngle * i;
//             Vector3 dir = fow.DirFromAngle(angle, false);
//             hit = Physics2D.Raycast(fow.transform.position, dir, fow.viewRadius, fow.obstacleMask);

//             if (hit.collider == null){
//                 viewVertex.Add(transform.position + dir.normalized * fow.viewRadius);
//             }
//             else{
//                 viewVertex.Add(transform.position + dir.normalized * hit.distance);
//             }
//         }

//         int vertexCount = viewVertex.Count + 1;
//         vertices = new Vector3[vertexCount];
//         triangles = new int [(vertexCount - 2) * 3];

//         vertices[0] = Vector3.zero;
//         for (int i = 0; i < vertexCount - 1; i++){
//             vertices[i + 1] = transform.InverseTransformPoint();
//         }
//     }
// }
