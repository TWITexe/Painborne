using Unity.VisualScripting;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    [SerializeField] private LampController[] lamps;


    [SerializeField]
    private int[][] leverLampIndices =
    {
        new int[] {1, 3, 4, 5, 7},   
        new int[] {2, 3, 5, 8},      
        new int[] {0, 2, 4, 6, 8},               
        new int[] {0, 3, 5, 6}
    };

    public void ActivateLever(int leverIndex)
    {
        foreach (int i in leverLampIndices[leverIndex])
        {
            lamps[i].Toggle();
        }

        CheckWinCondition();
    }

    private void CheckWinCondition()
    {
        foreach (var lamp in lamps)
        {
            if (!lamp.IsOn)
                return;
        }

        Debug.Log("✅ Все лампочки включены! Победа!");
    }
}
