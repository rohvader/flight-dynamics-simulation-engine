using UnityEngine;
// ensure this gameobject always has a MeshFilter, MeshRenderer, and MeshCollider attached
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]
public class TerrainGenerator : MonoBehaviour
{
    [Header("Terrain Settings")]    // groups these variables under "Terrain Settings" in the inspector
    //these public variables allow me to refine terrain properties from the Unity editor during testing
    public int width = 500;                // width of terrain grid in x
    public int depth = 500;               //depth of terrain grid in z 
    public float noiseScale = 20f;         // SF for perlin noise - larger values create smoother variations (modifiable based on testing)
    public float heightMultiplier = 10f;   // multiplier to affect range of heights produced by the perlin noise

    [Header("Flat Regions")]           //groups variables which are defining flat areas 
    public Vector2 runwayCentre = new Vector2(100, 50);  // centre of runway (x,z coordinates)
    public float runwayWidth = 50f;        // width of  runway in x
    public float runwayLength = 50f;       // length of runway in z

    public Vector2 spawnAreaCentre = new Vector2(50, 50);  //centre of spawn area
    public float spawnAreaSize = 10f;      // size of the spawn area (chosen as square for simplification)

    /*[Header("River Settings")]         // grouping variables for potential river feature
    public Vector2 riverStart = new Vector2(0, 100);   //starting point of the river (x,z coordinates)
    public Vector2 riverEnd = new Vector2(200, 100);     // ending point of the river
    public float riverWidth = 10f;         // width of the river channel 
    */

    // private references to the mesh components
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;
    private MeshCollider meshCollider;

    // this method is called when the game starts
    void Awake()
    {
        // fetch the MeshFilter, MeshRenderer, and MeshCollider components attached to this gameobject
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();
        meshCollider = GetComponent<MeshCollider>();

        // generate the terrain mesh immediately on start
        GenerateTerrainMesh();
    }

    //special function called when values are edited in the inspector (for better tracking of changes)
    void OnValidate()
    {
        // keeps components up to date in edit mode as well
        if (meshFilter == null || meshCollider == null)
        {
            meshFilter = GetComponent<MeshFilter>();
            meshCollider = GetComponent<MeshCollider>();
        }
        GenerateTerrainMesh();
    }

    // method to create the terrain mesh using perlin noise and adjustments for flat regions (runway, spawn area, river)
    void GenerateTerrainMesh()
    {
        // create a new mesh object that will store terrain data (vertices, triangles, etc.)
        Mesh mesh = new Mesh();
        mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;

        // calculate the number of vertices in a grid; +1 to include both ends of the grid
        Vector3[] vertices = new Vector3[(width + 1) * (depth + 1)];
        // each vertex gets a uv coordinate for texturing; array size matches the vertices array size
        Vector2[] uv = new Vector2[vertices.Length];
        // every grid square produces two triangles; each triangle uses 3 vertices, hence width * depth * 6 total indexes
        int[] triangles = new int[width * depth * 6];

        // initialise indices for vertices and triangles
        int vertIndex = 0;
        int triIndex = 0;

        // loop through each row (z-axis) and column (x-axis) to generate the grid of vertices
        for (int z = 0; z <= depth; z++)
        {
            for (int x = 0; x <= width; x++)
            {
                // generate a height value for each vertex through usual perlin noise function; dividing by noiseScale makes the noise smoother
                float y = Mathf.PerlinNoise((float)x / noiseScale, (float)z / noiseScale) * heightMultiplier;

                //space for specific adjustments for features e.g runway, spawn

                // Flatten runway area - corrected to center the runway about runwayCentre
                if (x >= runwayCentre.x - runwayWidth / 2f && x <= runwayCentre.x + runwayWidth / 2f &&
                    z >= runwayCentre.y - runwayLength / 2f && z <= runwayCentre.y + runwayLength / 2f)
                {
                    y = 0f; // Make this region flat (height = 0)
                }

                // sets the vertex position based on the calculated x, y, and z values
                vertices[vertIndex] = new Vector3(x, y, z);
                // assigns normalised uv coordinates (for applying textures)
                uv[vertIndex] = new Vector2((float)x / width, (float)z / depth);

                // if statement to check for boundary conditions (vertex being on far right or top edge of the terrain square/grid)
                if (x < width && z < depth)
                {
                    //first triangle of the quad
                    triangles[triIndex + 0] = vertIndex;
                    triangles[triIndex + 1] = vertIndex + width + 1;
                    triangles[triIndex + 2] = vertIndex + 1;

                    //second triangle of the quad
                    triangles[triIndex + 3] = vertIndex + 1;
                    triangles[triIndex + 4] = vertIndex + width + 1;
                    triangles[triIndex + 5] = vertIndex + width + 2;

                    //moving the triangle index ahead by 6 to prepare for the next quad
                    triIndex += 6;
                }

                // increment to next vertex in the array
                vertIndex++;
            }
        }

        // assign the calculated vertices, uv and triangles to the mesh
        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
        //recalculate normals for proper lighting and shading effects
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        //updates the MeshFilter and MeshCollider components with the generated mesh
        meshFilter.mesh = mesh;
        meshCollider.sharedMesh = null;
        meshCollider.sharedMesh = mesh;
    }
}




    /* // method to calculate the distance from a given point to a line segment defined by lineStart and lineEnd
    float DistanceFromPointToLine(Vector2 point, Vector2 lineStart, Vector2 lineEnd)
    {
        // calculate the difference in x and y from the point to the start of the line
        float A = point.x - lineStart.x;
        float B = point.y - lineStart.y;
        // calculate the difference in x and y for the line segment
        float C = lineEnd.x - lineStart.x;
        float D = lineEnd.y - lineStart.y;

        // compute the dot product between the vector from lineStart to point and the line's direction vector
        float dot = A * C + B * D;
        // compute the squared length of the line segment (avoids a square root for performance)
        float lenSq = C * C + D * D;
        // determine the position along the line segment (0 = start, 1 = end) where the perpendicular from the point lands
        float param = dot / lenSq;

        // ensure the parameter is clamped between 0 and 1, so the result lies within the segment
        if (param < 0f) {
            param = 0f;
        }
        else {
            if (param > 1f) {
                param = 1f;
            }
        }

        // calculate the exact x and y coordinates on the line segment corresponding to the parameter value
        float xx = lineStart.x + param * C;
        float yy = lineStart.y + param * D;

        // compute the difference between the point and this closest point on the line
        float dx = point.x - xx;
        float dy = point.y - yy;
        // return the euclidean distance between the point and the closest point on the line
        return Mathf.Sqrt(dx * dx + dy * dy);
    } 
*/

