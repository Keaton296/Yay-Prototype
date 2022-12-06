using UnityEngine;
using System.Collections;
using DG.Tweening;
public class TowerCommander : MonoBehaviour
{
    Camera myCamera;
    [SerializeField] YayTower heldTower;
    [SerializeField] bool isAvailable = true;
    private void Awake()
    {
        myCamera = Camera.main;
    }
    private void Update()
    {
        if (isAvailable)
        {
            RaycastHit hitInfo;
            YayTower pointingTower = null;
            if (Physics.Raycast(myCamera.ScreenPointToRay(Input.mousePosition), out hitInfo))
            {
                pointingTower = hitInfo.collider.GetComponent<YayTower>();
            }
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (pointingTower != null)
                {
                    heldTower = pointingTower;
                }
            }
            else if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                if (heldTower != null && pointingTower != null && heldTower != pointingTower)
                {
                    //we can start a coroutine using DOTween for each yay for its animation
                    StartCoroutine(MeshAnimationCoroutine(heldTower,pointingTower));
                    
                }
            }
        }
    }
    IEnumerator MeshAnimationCoroutine(YayTower held,YayTower target)
    {
        isAvailable = false;
        Yay _yay = held.GetLastYay();
        //Debug.Log(_yay.height);
        if (_yay != null)
        {

            for (int i = 0; i < _yay.height; i++)
            {
                heldTower.yayMeshManager.ThrowLastElement(target);
                yield return new WaitForSeconds(YayMeshManager.animationIntervalSeconds);
            }
            held.TakeFromTop();
            target.PutOnTop(_yay);
        }
        heldTower = null;
        isAvailable = true;
    }
}
