using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI coinText;
    public Camera mainCamera;

    private int _coinCount;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Mouse0))
            return;

        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (!Physics.Raycast(ray, out hit))
            return;

        if (hit.transform.gameObject.tag.Equals("Question"))
            HitQuestion();
        else if (hit.transform.gameObject.tag.Equals("Brick"))
            Destroy(hit.transform.gameObject);
    }

    private void HitQuestion()
    {
        _coinCount++;
        coinText.text = _coinCount > 9 ? $"C x {_coinCount}": coinText.text = $"C x 0{_coinCount}";;
    }

    private void Restart()
    {
        _coinCount = 0;
    }
}
