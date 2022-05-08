using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;

public class MenuButtonEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public void OnPointerEnter(PointerEventData eventData)
    {
        TextMeshProUGUI tmp = GetComponent<TextMeshProUGUI>();

        float multiplier = 1 / 255f;
        float dark1 = 70 * multiplier, dark2 = 170 * multiplier;

        Color tl = new Color(dark1, dark1, dark1);
        Color br = new Color(dark1, dark1, dark1);
        Color tr = new Color(dark2, dark2, dark2);
        Color bl = new Color(dark2, dark2, dark2);

        tmp.colorGradient = new TMPro.VertexGradient(tl, tr, bl, br);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TextMeshProUGUI tmp = GetComponent<TextMeshProUGUI>();

        float multiplier = 1 / 255f;
        float light1 = 136 * multiplier, light2 = 255 * multiplier;

        Color tl = new Color(light1, light1, light1);
        Color br = new Color(light1, light1, light1);
        Color tr = new Color(light2, light2, light2);
        Color bl = new Color(light2, light2, light2);

        tmp.colorGradient = new TMPro.VertexGradient(tl, tr, bl, br);
    }

    public void PlayGame()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(1);
    }
}
