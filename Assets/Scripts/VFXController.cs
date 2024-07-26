using UnityEngine;

public class VFXController : MonoBehaviour
{
    [SerializeField] private string targetName = "Player";
    [SerializeField] private bool isNeedParent;

    private Transform _target;

    private void Awake()
    {
        _target = GameObject.Find(targetName).GetComponent<Transform>();

        if (!isNeedParent)
        {
            transform.parent = null;
            transform.position = Vector3.zero;
        }
        MyVFXTransformBinder myTransformBinder =  GetComponent<MyVFXTransformBinder>();
        myTransformBinder.Target = _target.transform;
    }
}
