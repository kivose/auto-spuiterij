using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprayPainter : BasePickUp
{
    public LayerMask whatIsPaintable;
    public Color currentSprayColor;
    
    public List<PaintableObject> paintableObjects = new List<PaintableObject> ();

    Collider collider;
    Rigidbody rigidbody;

    private void Start()
    {
        collider = gameObject.GetComponent<Collider> ();
        rigidbody = gameObject.GetComponent<Rigidbody>();
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
}
