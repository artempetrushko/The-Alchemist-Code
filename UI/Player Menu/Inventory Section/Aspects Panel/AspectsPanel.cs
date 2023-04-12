using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AspectsPanel : MonoBehaviour
{
    [SerializeField]
    private GameObject aspectIconPrefab;

    public void UpdateAspectsValues(List<AspectState> aspects)
    {
        ClearAspectValues();
        for (var i = 0; i < aspects.Count; i++)
        {
            var aspectIcon = Instantiate(aspectIconPrefab, gameObject.transform);
            aspectIcon.transform.GetChild(1).GetComponent<Image>().sprite = aspects[i].Icon;
            aspectIcon.transform.GetChild(1).GetComponent<Image>().fillAmount = aspects[i].ProgressionState / aspects[i].MaxProgression;
        }
    }

    public void ClearAspectValues()
    {
        for (var i = gameObject.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(gameObject.transform.GetChild(i).gameObject);
        }
    }
}
