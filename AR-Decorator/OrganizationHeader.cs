using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OrganizationHeader : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI OrganizationName;
    [SerializeField] private TextMeshProUGUI OrganizationMarkers;
    [SerializeField] private TextMeshProUGUI OrganizationAddress;

    public void ChangeFields() {
        OrganizationName.text = GlobalVariables.nameInst;
        OrganizationMarkers.text = GlobalVariables.countInst;
        OrganizationAddress.text = GlobalVariables.descInst;
    }
}
