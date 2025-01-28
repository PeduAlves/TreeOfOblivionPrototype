using UnityEngine;
using System.Collections.Generic;

public class ConeVIsion : MonoBehaviour
{
    public float visionRange = 5f;
    public float visionAngle = 60f;
    public int rayCount = 30;
    public LayerMask whatIsObstacle;

    private Mesh visionMesh;
    private MeshFilter meshFilter;

    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        visionMesh = new Mesh();
        meshFilter.mesh = visionMesh;
    }

    void Update()
    {
        GenerateVisionCone();
    }

    void GenerateVisionCone()
    {
        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();

        // Centro do cone (origem do inimigo)
        vertices.Add(Vector3.zero);

        float angleStep = visionAngle / (rayCount - 1);
        float startAngle = -visionAngle / 2;

        // Gerar vértices ao redor do cone
        for (int i = 0; i < rayCount; i++)
        {
            float angle = startAngle + angleStep * i;
            Vector3 direction = Quaternion.Euler(0, angle, 0) * transform.forward;

            if (Physics.Raycast(transform.position, direction, out RaycastHit hit, visionRange, whatIsObstacle))
            {
                // Se houver obstáculo, termina ali
                vertices.Add(transform.InverseTransformPoint(hit.point));
            }
            else
            {
                // Sem obstáculo, estende até o limite do cone
                vertices.Add(transform.InverseTransformPoint(transform.position + direction * visionRange));
            }
        }

        // Criar triângulos do mesh
        for (int i = 1; i < vertices.Count - 1; i++)
        {
            triangles.Add(0);
            triangles.Add(i);
            triangles.Add(i + 1);
        }

        // Atualizar a malha
        visionMesh.Clear();
        visionMesh.vertices = vertices.ToArray();
        visionMesh.triangles = triangles.ToArray();
        visionMesh.RecalculateNormals();
    }
}
