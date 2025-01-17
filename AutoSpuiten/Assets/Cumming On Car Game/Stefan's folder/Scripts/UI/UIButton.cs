using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

[ExecuteInEditMode]
public class UIButton : 
    MonoBehaviour, IPointerClickHandler, ISelectHandler,
    IDeselectHandler, IPointerUpHandler, IPointerDownHandler,
    IPointerEnterHandler, IPointerExitHandler {

    public static UIButton selectedButton;

    [System.Serializable]
    struct DebugData
    {
        public bool hovered;
        public bool selected;

        public bool clicked;
        public float clickedTimer;

        public Color hexagonTargetColor1, hexagonTargetColor2;
        public Vector3 targetScale;
        public Vector3 defaultScale;
    }

    struct HexagonData
    {
        public Color color;
        public float currentTimer;
        public float maxTimer;
    }

    const int k_SpaceSize = 8;
    public bool Selected
    {
        get => debug.selected;
        set => debug.selected = value; 
    }

    public bool Hovered
    {
        get => debug.hovered; 
        set => debug.hovered = value; 
    }
    public bool Clicked
    {
        get => debug.clicked;
        set => debug.clicked = value;
    }


    [SerializeField]
    DebugData debug;

    [Space(k_SpaceSize)]
    [Header("References")]

    public Image[] hexagons;
    HexagonData[] hexData;

    public TextMeshProUGUI mainText, backgroundText;

    public Image boxImage, cornersImage;


    [Space(k_SpaceSize)]
    [Header("Default")]

    [TextArea(5,15)]
    public string text;

    public Color defaultHexagonColor1;
    public Color defaultHexagonColor2;

    public float colorLerpSpeed;

    public float scalesmoothTime;
    Vector3 scaleVelocity;

    [Space(k_SpaceSize)]
    [Header("Hovered")]
    public Color hoveredHexagonColor1;
    public Color hoveredHexagonColor2;
    public Vector3 hoveredScale = Vector3.one;

    [Space(k_SpaceSize)]
    [Header("Selected")]
    public Color selectedHexagonColor1;
    public Color selectedHexagonColor2;

    [Space(k_SpaceSize)]
    [Header("Clicked")]
    public Color clickedHexagonColor1;
    public Color clickedHexagonColor2;

    public float clickedRegistrationTime = 0;

    public Button.ButtonClickedEvent onClick;

    void Start()
    {
        hexData = new HexagonData[hexagons.Length];
        debug.defaultScale = transform.localScale;
    }

    void Update()
    {
        if (Application.isPlaying)
        {
            //Determine if the button is selected;
            Selected = selectedButton == this;

            //Determine the graphics of the button

            Color targetColor1 = defaultHexagonColor1;
            Color targetColor2 = defaultHexagonColor2;

            Vector3 targetScale = debug.defaultScale;

            //selected

            if (Selected)
            {
                targetColor1 = selectedHexagonColor1;
                targetColor2 = selectedHexagonColor2;
            }


            //hovered
            if (Hovered)
            {
                targetColor1 = hoveredHexagonColor1;
                targetColor2 = hoveredHexagonColor2;

                targetScale = hoveredScale;
            }
            //clicked
            if (Clicked)
            {
                targetColor1 = clickedHexagonColor1;
                targetColor2 = clickedHexagonColor2;

                onClick.Invoke();
            }

            debug.hexagonTargetColor1 = targetColor1;
            debug.hexagonTargetColor2 = targetColor2;
            debug.targetScale = targetScale;

            

            transform.localScale = Vector3.SmoothDamp(transform.localScale, debug.targetScale, ref scaleVelocity, scalesmoothTime);
        }

        backgroundText.text = text;
        mainText.text = text;
        transform.name = text;

    }

    void FixedUpdate()
    {
        //update the buttons graphics
        UpdateHexagonsColor();
    }

    void OnDisable()
    {
        Hovered = false;
        Selected = false;
        Clicked = false;
    }
    public void UpdateHexagonsColor()
    {
        for (int i = 0; i < hexagons.Length; i++)
        {
            hexagons[i].color = Color.Lerp(hexagons[i].color, hexData[i].color, Time.fixedDeltaTime / hexData[i].currentTimer);
            
            hexData[i].currentTimer -= Time.fixedDeltaTime;
            if(hexData[i].currentTimer <= 0)
            {
                hexData[i].maxTimer = colorLerpSpeed + Random.Range(-50f, 50f) / 500f;
                hexData[i].currentTimer = hexData[i].maxTimer;
                hexData[i].color = Color.Lerp(debug.hexagonTargetColor1, debug.hexagonTargetColor2, Random.Range(1f, 100f) / 100);
            }
        }

        mainText.color = debug.hexagonTargetColor1;
        backgroundText.color = debug.hexagonTargetColor2;

        boxImage.color = debug.hexagonTargetColor2;
        cornersImage.color = debug.hexagonTargetColor1;
    }

    #region Input Registration

    /// <summary>
    /// Called when the user clicks on this gameObject
    /// </summary>
    /// <param name="data"></param>
    public void OnPointerClick(PointerEventData data)
    {
        Debug.Log("Click");
        selectedButton = this;

        debug.clicked = true;
        debug.clickedTimer = clickedRegistrationTime;
        
    }

    /// <summary>
    /// Called when the user selects this gameObject
    /// </summary>
    /// <param name="data"></param>
    public void OnSelect(BaseEventData data)
    {
        Debug.Log("Select");
    }

    /// <summary>
    /// Called when the user deselects this gameObject
    /// </summary>
    /// <param name="data"></param>
    public void OnDeselect(BaseEventData data)
    {
        Debug.Log("Select");
    }

    /// <summary>
    /// Called when the user cancels clicking on this gameObject
    /// </summary>
    /// <param name="data"></param>
    public void OnPointerUp(PointerEventData data)
    {
        Debug.Log("Pointer Up");
        Clicked = false;
    }

    /// <summary>
    /// Called when the user starts clicking on this gameObject
    /// </summary>
    /// <param name="data"></param>
    public void OnPointerDown(PointerEventData data)
    {
        Debug.Log("Pointer down");
        Clicked = true;
    }

    /// <summary>
    /// Called when the user hovers the pointer on this gameObject
    /// </summary>
    /// <param name="data"></param>
    public void OnPointerEnter(PointerEventData data)
    {
        Debug.Log("Pointer enter");
        debug.hovered = true;
    }

    /// <summary>
    /// Called when the user unhovers the pointer on this gameObject
    /// </summary>
    /// <param name="data"></param>
    public void OnPointerExit(PointerEventData data)
    {
        Debug.Log("Pointer exit");
        debug.hovered = false;
    }

    #endregion
}
