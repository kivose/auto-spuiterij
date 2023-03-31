using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Oven : MonoBehaviour
{
    public enum OvenStatus
    {
        Unused,
        Loaded,
        Cooking,
        Finished,
    }

    public float maxTimer = 120;

    public float ovenSpeed;

    public float CurrentTimer;
    public bool IsOn;

    public bool IsOvenDoorOpen
    {
        get
        {
            return ovenAnimator.GetBool("Open");
        }
        set
        {
            ovenAnimator.SetBool("Open",value);
        }
    }

    public Transform pivot;

    public Transform currentObject;

    public Animator ovenAnimator;

    public TextMeshProUGUI time, ovenStatusText;

    public OvenStatusData[] ovenStatuses;

    [SerializeField]
    private OvenStatus m_ovenStatus;

    public OvenStatus CurrentOvenStatus
    {
        get
        {
            return m_ovenStatus;
        }
        set
        {
            if (value == m_ovenStatus) return;
            m_ovenStatus = value;

            OnOvenStatusChanged();
        }
    }

    Vector3 previousItemPosition;
    [System.Serializable]
    public struct OvenStatusData
    {
        public OvenStatus ovenStatus;
        public string text;
        public Color color;
    }
    // Start is called before the first frame update
    void Start()
    {
        OnOvenStatusChanged();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentObject)
        {
            var position = currentObject.position;

            if(position != previousItemPosition)
            {
                OnItemPickedUp();
            }

            previousItemPosition = position;
        }
        if (IsOn)
        {
            CurrentTimer -= Time.deltaTime * ovenSpeed;

            time.text = ToTime(CurrentTimer);

            CurrentOvenStatus = OvenStatus.Cooking;

            if (CurrentTimer <= 0)
            {
                EndOven();
                IsOn = false;
            }

        }
    }

    public void OnOvenDoorInteract()
    {
        if (IsOn) return;

        IsOvenDoorOpen = !IsOvenDoorOpen;
    }
    void OnOvenStatusChanged()
    {
        for (int i = 0; i < ovenStatuses.Length; i++)
        {
            if(CurrentOvenStatus == ovenStatuses[i].ovenStatus)
            {
                ovenStatusText.text = ovenStatuses[i].text;
                ovenStatusText.color = ovenStatuses[i].color;
                return;
            }
        }
    }

    public void OnDropItemOnOven()
    {
        var coll = Physics.OverlapSphere(pivot.position,3);

        float closestDst = float.MaxValue;
        KinematicObject closest = null;

        for (int i = 0; i < coll.Length; i++)
        {
            var kinematic = coll[i].GetComponent<KinematicObject>();

            if (kinematic)
            {
                float dst = Vector3.Distance(pivot.position,kinematic.transform.position);
                if(dst < closestDst && dst < 1)
                {
                    closestDst = dst;
                    closest = kinematic;
                }
            }
        }

        if (closest)
        {
            currentObject = closest.transform;
            CurrentOvenStatus = OvenStatus.Loaded;
            previousItemPosition = currentObject.position;
        }
    }

    public void OnItemPickedUp()
    {
        currentObject = null;
        CurrentOvenStatus = OvenStatus.Finished;
    }

    public void TryStartOven()
    {
        if(CurrentOvenStatus == OvenStatus.Loaded && IsOvenDoorOpen == false )
        {
            StartOven();
        }
    }
    public void StartOven()
    {
        CurrentTimer = maxTimer;
        IsOn = true;

        CurrentOvenStatus = OvenStatus.Cooking;
        // ==> oven kan niet meer open

    }

    void EndOven()
    {
        CurrentOvenStatus = OvenStatus.Finished;
        // ==> remove drip particles
        var particle = currentObject.GetComponentInChildren<DripParticle>();

        if (particle)
        {
            Destroy(particle);
        }
    }

    public static string ToTime(float seconds)
    {
        int minutes = Mathf.FloorToInt(seconds / 60f);
        seconds -= 60 * minutes;
        int remainingSeconds = Mathf.FloorToInt(seconds);

        return string.Format("{0:0}:{1:00}", minutes, remainingSeconds);
    }
}
