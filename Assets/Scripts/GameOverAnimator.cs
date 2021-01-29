using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverAnimator : MonoBehaviour
{
    private TextMeshProUGUI textMesh;

    private float maxDilate = 0f;
    private float minDilate = -.8f;

    private float timeToDilate = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        textMesh.fontMaterial.SetFloat(ShaderUtilities.ID_FaceDilate, -.7f);

    }

    // Update is called once per frame
    void Update()
    {
        float currentDilate = textMesh.fontMaterial.GetFloat(ShaderUtilities.ID_FaceDilate);
        float deltaTime = Time.deltaTime;
        float newDilate;

        if (currentDilate < maxDilate)
        {
            newDilate = currentDilate + deltaTime / timeToDilate * (maxDilate - minDilate);
            textMesh.fontMaterial.SetFloat(ShaderUtilities.ID_FaceDilate, newDilate);
        }
    }
}
