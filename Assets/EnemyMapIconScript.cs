using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMapIconScript : MonoBehaviour
{
    [SerializeField]private Canvas thisMapCanvas;
    [SerializeField]private Image thisMapIcon;

    // Start is called before the first frame update
    void Start()
    {
        thisMapCanvas = gameObject.GetComponentInChildren<Canvas>();
        thisMapIcon = thisMapCanvas.GetComponentInChildren<Image>();
        thisMapCanvas.gameObject.layer = LayerMask.NameToLayer("Map");
        thisMapIcon.gameObject.layer = LayerMask.NameToLayer("Map");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
