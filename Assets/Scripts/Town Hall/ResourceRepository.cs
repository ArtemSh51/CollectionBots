using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceRepository : MonoBehaviour
{
    [SerializeField] private float _timeBetweenWritingFoldableResources;
    private List<Resource> _foldableResources = new List<Resource>();

    private bool _canShowFoldableResources = true;

    public int Count => _foldableResources.Count;

    private void Awake()
    {
        StartCoroutine(WriteCountOfFoldableResources());
    }

    private IEnumerator WriteCountOfFoldableResources()
    {
        WaitForSeconds wait = new WaitForSeconds(_timeBetweenWritingFoldableResources);

        while (_canShowFoldableResources)
        {
            Debug.Log(_foldableResources.Count);

            yield return wait;
        }
    }

    public void AddResource(Resource resource)
    {
        _foldableResources.Add(resource);
    }

    public void RemoveResource(int index)
    {
        _foldableResources.RemoveAt(index);
    }
}
