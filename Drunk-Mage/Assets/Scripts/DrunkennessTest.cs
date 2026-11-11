using UnityEngine;

public class DrunkennessTest : MonoBehaviour
{
    [ContextMenu("Test Add Drunkenness")]
    private void TestAddDrunkenness()
    {
        if (!Application.isPlaying)
        {
            Debug.LogWarning("Test must run in Play Mode!");
            return;
        }

        if (ActionSystem.Instance == null)
        {
            Debug.LogError("ActionSystem.Instance is null!");
            return;
        }

        ActionSystem.Instance.Perform(
            new AddDrunkennessGA(3)
        );
    }
}