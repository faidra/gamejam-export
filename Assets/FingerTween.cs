using UnityEngine;
using System.Collections;

public class FingerTween : MonoBehaviour {
    [SerializeField]
    float Diff;
    [SerializeField]
    float Speed;

    // Use this for initialization
    void Start () {
	
	}

    // Update is called once per frame
    void Update()
    {
        // tekitou
        transform.localPosition = new Vector3(Diff * Mathf.Sin(Speed * Time.time), 0, 0);
    }
}
