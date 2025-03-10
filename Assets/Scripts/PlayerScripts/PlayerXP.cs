using UnityEngine;

public class PlayerXP : MonoBehaviour
{
    public int level = 1;
    public float currentXP = 0;
    public float xpToNextLevel = 100f;

    public delegate void OnLevelUp(int newLevel);
    public event OnLevelUp LevelUpEvent;


    public void GainXP(float xpAmount)
    {
        currentXP += xpAmount;
        if (currentXP >= xpToNextLevel) LevelUp();
    }

    void LevelUp()
    {
        level++;
        currentXP -= xpToNextLevel;
        xpToNextLevel *= 1.2f;

        Debug.Log($"seviye atlandı. yeni seviye: {level}");
        LevelUpEvent?.Invoke(level);
    }
}
