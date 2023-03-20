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
    private void Start()
    {
        collider = gameObject.GetComponent<Collider> ();
        rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    public void Update()
    {
        PaintableObject.ProgressSpeed = progressSpeed;
        if (pickedUp == false) return;

        if (true) //input detection
        {
            if(Physics.Raycast(origin.position, origin.forward, out RaycastHit hit, 100000f))
            {
                print(hit.transform);
                var material = hit.transform.GetComponent<MeshRenderer>().sharedMaterial;

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

            
        }
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

    #region Material Instantiation
    //Dictionary<Material, Material> dict = new Dictionary<Material, Material>();

    Material GetMat(Material aMat)
    {
        Material mat;
        //if (!dict.TryGetValue(aMat, out mat))
            /*dict.Add(aMat,*/ mat = (Material)Instantiate(aMat)/*)*/;
        return mat;
    }

    void Awake()
    {
        Renderer[] renderers = FindObjectsOfType<Renderer>();
        for (int i = 0; i < renderers.Length; i++)
        {
            var materials = renderers[i].sharedMaterials;
            for (int j = 0; j < materials.Length; j++)
            {
                if (materials[j].name.Contains("Outline")) continue;

                materials[j] = GetMat(materials[j]);
                materials[j].name = materials[j].name + renderers[i].transform.name;
            }
            renderers[i].sharedMaterials = materials;
        }
    }
    #endregion
}
