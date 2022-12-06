using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
[RequireComponent(typeof(YayTower))]
public class YayMeshManager : MonoBehaviour
{
    [SerializeField] YayTower m_yayTower;
    [SerializeField] GameObject yayPrefab;
    [SerializeField] Transform yayParentTransform;
    public static float animationHeightLimit = 3f;
    [SerializeField] List<Transform> yayTransforms;
    public static float animationIntervalSeconds = .15f;
    public static float animationDuration = .075f; //should be half of the value animationIntervalSeconds
    public static float YAYPIECEHEIGHT = 0.2f;
    private void Awake()
    {
        m_yayTower.OnInitialize += Initialize;
    }
    void Initialize()
    {
        yayTransforms = new List<Transform>();
        InstantiateYayMeshes();
    }

    void CreateYayPieceMesh(YayColor color,int heightIndex)
    {
        Transform instance = Instantiate(yayPrefab , yayParentTransform.position + (heightIndex) * new Vector3(0,YAYPIECEHEIGHT,0),Quaternion.Euler(90,0,0),yayParentTransform).transform;
        yayTransforms.Add(instance);
        instance.GetComponent<MeshRenderer>().material.color = Yay.GetColorFromEnum(color);
    }

    public void ThrowLastElement(YayTower target)
    {
        
        int index = target.yayMeshManager.GetTransformCount();
        Transform thrownObject = yayTransforms[yayTransforms.Count -1];
        Vector3 rotationAxis = target.transform.position - transform.position;
        rotationAxis = new Vector3(rotationAxis.x,0,rotationAxis.z).normalized;
        rotationAxis = Quaternion.AngleAxis(90,Vector3.up) * rotationAxis;
        thrownObject.parent = target.yayMeshManager.yayParentTransform;
        yayTransforms.Remove(thrownObject);
        target.yayMeshManager.yayTransforms.Add(thrownObject);  
        thrownObject.DOMove((target.transform.position + transform.position)/2 + new Vector3(0,animationHeightLimit,0),animationDuration).SetEase(Ease.OutCubic).onComplete += () => {
            thrownObject.DOMove(target.yayMeshManager.yayParentTransform.position + index * Vector3.up * YAYPIECEHEIGHT, animationDuration).SetEase(Ease.InCubic);
            };
        thrownObject.DORotate((transform.rotation * Quaternion.AngleAxis(180, rotationAxis)).eulerAngles, animationDuration, RotateMode.WorldAxisAdd).SetEase(Ease.Linear);
        //Debug.Log(Quaternion.AngleAxis(90, rotationAxis).eulerAngles);
        
    }

    void InstantiateYayMeshes()
    {
        int counter = 0;
        foreach (Yay item in m_yayTower.Yays)
        {
            for (int i = 0; i < item.height; i++)
            {
                CreateYayPieceMesh(item.color, counter);
                counter++;
            }
        }
    }
    public int GetTransformCount()
    {
        return yayTransforms.Count;
    }
}
