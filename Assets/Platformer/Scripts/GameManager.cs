using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI scoreText;
    public Camera mainCamera;
    public GameObject character;

    private Transform _camTransform;
    private static int _coinCount = 0;
    private static int _score = 0;

    // Start is called before the first frame update
    void Start()
    {
        _camTransform = mainCamera.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        coinText.text = _coinCount > 9 ? $"C x {_coinCount}": coinText.text = $"C x 0{_coinCount}";;
        scoreText.text =$"MARIO\n{_score}";
        
        _camTransform.position = new Vector3(character.transform.position.x, _camTransform.position.y, _camTransform.position.z);
        
        // if (!Input.GetKeyDown(KeyCode.Mouse0))
        //     return;
        //
        // RaycastHit hit;
        // Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        //
        // if (!Physics.Raycast(ray, out hit))
        //     return;
        //
        // if (hit.transform.gameObject.tag.Equals("Question"))
        //     HitQuestion();
        // else if (hit.transform.gameObject.tag.Equals("Brick"))
        //     Destroy(hit.transform.gameObject);
    }

    public static void CoinCollected()
    {
        _coinCount++;
        _score += 100;
    }

    public static void BrickDestroyed()
    {
        _score += 100;
    }
    
    public static void Restart()
    {
        _coinCount = 0;
        _score = 0;
    }
}
