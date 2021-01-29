using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TitleAnimator : MonoBehaviour
{
    private TextMeshProUGUI textMesh;
    private bool upDilate;

    private float maxDilate = 0.1f;
    private float minDilate = -0.2f;

    private float timeToDilate = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        textMesh.fontMaterial.SetFloat(ShaderUtilities.ID_FaceDilate, -.7f);
        upDilate = true;
    }

    // Update is called once per frame
    void Update()
    {
        float currentDilate = textMesh.fontMaterial.GetFloat(ShaderUtilities.ID_FaceDilate);
        float deltaTime = Time.deltaTime;
        float newDilate;

        if (currentDilate > maxDilate)
        {
            upDilate = false;
        } else if (currentDilate < minDilate)
        {
            upDilate = true;
        }

        if (upDilate)
        {
            newDilate = currentDilate + deltaTime / timeToDilate * (maxDilate - minDilate);
            textMesh.fontMaterial.SetFloat(ShaderUtilities.ID_FaceDilate, newDilate);
        } else
        {
            newDilate = currentDilate - deltaTime / timeToDilate * (maxDilate - minDilate);
            textMesh.fontMaterial.SetFloat(ShaderUtilities.ID_FaceDilate, newDilate);
        }

    }
}
