using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprayPainter : BasePickUp
{
    public LayerMask whatIsPaintable;
    public Color currentSprayColor;

    public Transform origin;
    public List<PaintableObject> paintableObjects = new List<PaintableObject> ();

    Collider collider;
    Rigidbody rigidbody;

    public float progressSpeed = 1f;
    public float paintDstTreshold;
    private void Start()
    {
        collider = gameObject.GetComponent<Collider> ();
        rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    public void FixedUpdate()
    {
        if (pickedUp == false) return;
        #region Old Method
        /*PaintableObject.ProgressSpeed = progressSpeed;
       

        if (true) //input detection
        {
            if(Physics.Raycast(origin.position, origin.forward, out RaycastHit hit, 100000f, whatIsPaintable))
            {
                print(hit.transform);
                var material = hit.transform.GetComponent<MeshRenderer>()?.sharedMaterial;

                if(material != null)
                {
                    if (ContainsMaterial(material, out int paintableIndex))
                    {
                        paintableObjects[paintableIndex].PaintObject(Time.deltaTime,currentSprayColor);
                    }
                    else
                    {
                        PaintableObject newObject = new PaintableObject
                        {
                            objectMaterial = material,
                            gameObject = hit.transform.gameObject,
                            startColor = material.color,
                            
                        };

                        paintableObjects.Add(newObject);
                    }
                }
            }

            
        } */
        #endregion

        #region New Method

        if (true)//input detection komt nog
        {
            // ==> Raycast vanuit de spray painter
            if (Physics.Raycast(origin.position, origin.forward, out RaycastHit hit, 100000f))
            {
                // ==> Overlap sphere gebruiken, als we op een hoek van een mesh sprayen, worden ook andere paintable meshes gepaint
                var colliders = Physics.OverlapSphere(hit.point, paintDstTreshold * 2, whatIsPaintable);
                
                foreach (MeshCollider collider in colliders)
                {
                    KinematicObject kin = collider.GetComponent<KinematicObject>();

                    if (kin)
                    {
                        if (kin.isSuccesfullyPainted) continue;
                    }

                    Mesh mesh = collider.GetComponent<MeshFilter>().sharedMesh;

                    if (mesh) // ==> kijk of er een mesh is
                    {
                        var colors = mesh.colors;

                        // ==> checken of de meshColors array dezelfde length heeft als de vertices, zodat ze juist gepaint kunnen worden
                        if (colors.Length != mesh.vertices.Length)
                        {
                            colors = new Color[mesh.vertices.Length];

                            // ==> Niet dezelfde length? De color array wordt gemaakt met een basiskleur
                            for (int i = 0; i < colors.Length; i++)
                            {
                                colors[i] = Color.white;
                            }
                        }

                        var transform = collider.transform;

                        var vertices = mesh.vertices;

                        // ==> Voor elke vertex kijken of die in de spray zone ligt van de spray gun
                        for (int i = 0; i < vertices.Length; i++)
                        {
                            // ==> Convert vertex positie naar world pos
                            Vector3 vertexWorldPos = transform.TransformPoint(vertices[i]);

                            float dst = Vector3.Distance(vertexWorldPos, hit.point);

                            if (dst <= paintDstTreshold)
                            {
                                // ==> de intensiteit van het sprayen wordt bepaalt op basis van de spray zone
                                float factor = Mathf.InverseLerp(0, paintDstTreshold, dst);
                                colors[i] = Color.Lerp(colors[i], currentSprayColor, progressSpeed * factor * Time.fixedDeltaTime); // ==> lerpen gebruiken om het smooth te painter
                            }
                        }

                        mesh.colors = colors;
                    }
                }
                
            }
        }
        #endregion
    }

    public bool ContainsMaterial(Material material, out int paintableIndex)
    {
        for (int i = 0; i < paintableObjects.Count; i++)
        {
            if (paintableObjects[i].objectMaterial == material) 
            {
                paintableIndex = i;
                return true;
            }
        }
        paintableIndex = -1;
        return false;
    }
    public override void OnPickUp()
    {
        base.OnPickUp();
        collider.enabled = false;
        rigidbody.isKinematic = true;
    }

    public override void OnDrop()
    {
        base.OnDrop();
        collider.enabled = true;
        rigidbody.isKinematic = false;
    }
    
    [System.Serializable]
    public struct PaintableObject
    {
        public static float ProgressSpeed = 1;

        //The color when we started painting this object
        public Color startColor;

        //The color we want to have on this object
        private Color m_targetColor;
        
        public Color TargetColor
        {
            get
            {
                return m_targetColor;
            }
            set
            {
                if (value == m_targetColor) return;

                m_targetColor = value;
                Progress = 0;
            }
        }

        //The color the object now has
        public Color CurrentColor
        {
            get
            {
                return objectMaterial.color;
            }
            set
            {
                objectMaterial.color = value;
            }
        }

        private float m_progress;

        public float Progress
        {
            get
            {
                return m_progress;
            }
            set
            {
                m_progress = Mathf.Clamp01(value);
            }
        }

        public Material objectMaterial;

        public GameObject gameObject;

        public void PaintObject(float deltaTime, Color color)
        {
            TargetColor = color;

            Progress += deltaTime * ProgressSpeed;
            
            CurrentColor = Color.Lerp(CurrentColor, TargetColor, Progress);
        }
    }

    #region Mesh Instantiation
    //Dictionary<Material, Material> dict = new Dictionary<Material, Material>();

    // ==> Cloned een mesh op start, waneer de game stopt in editor, wordt de mesh gereset
    Mesh CloneMesh(Mesh aMat)
    {
        return Instantiate(aMat);
    }

    void Awake()
    {
        MeshFilter[] filters = FindObjectsOfType<MeshFilter>();
        for (int i = 0; i < filters.Length; i++)
        {
            if (filters[i].gameObject.layer != LayerMask.NameToLayer("Paintable")) continue; // ==> Verander paintable naar de naam van je paint layer

            var mesh = filters[i].sharedMesh;

            mesh = CloneMesh(mesh);
            mesh.name = mesh.name + filters[i].transform.name;
            
            filters[i].sharedMesh = mesh;
        }
    }
    #endregion
}
