using UnityEngine;

//public class ManualTargetSystem : Singleton<ManualTargetSystem>
//{
//    [SerializeField] private ArrowView arrowView;
//    [SerializeField] private LayerMask targetLayerMask;

//    public void StartTargeting(Vector3 startPosition)
//    {
//        arrowView.gameObject.SetActive(true);
//        arrowView.SetupArrow(startPosition);
//    }
//    public EnemyView EndTargeting()
//    {
//        arrowView.gameObject.SetActive(false);

//        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

//        if (Physics.Raycast(ray, out RaycastHit hit, 100f, targetLayerMask))
//        {
//            return hit.transform.GetComponent<EnemyView>();
//        }

//        Debug.Log("Ray did not hit EnemyView layer");
//        return null;
//    }
//}

public class ManualTargetSystem : Singleton<ManualTargetSystem>
{
    [SerializeField] private ArrowView arrowView;
    [SerializeField] private LayerMask targetLayerMask;

    public void StartTargeting(Vector3 startPosition)
    {
        arrowView.gameObject.SetActive(true);
        arrowView.SetupArrow(startPosition);
    }

    public CombatantView EndTargeting()
    {
        arrowView.gameObject.SetActive(false);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 100f, targetLayerMask))
        {
            return hit.transform.GetComponentInParent<CombatantView>();
        }

        return null;
    }
}