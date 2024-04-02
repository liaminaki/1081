using UnityEngine;

[CreateAssetMenu(fileName = "CoinCollectionData", menuName = "Coin Collection Data")]
public class CoinCollectionData : ScriptableObject
{
    public bool[] collectedCoins;

    public void Initialize(int numCoins)
    {
        collectedCoins = new bool[numCoins];
    }

    public void CollectCoin(int coinID)
    {
        if (coinID >= 0 && coinID < collectedCoins.Length)
        {
            collectedCoins[coinID] = true;
        }
    }

    public bool IsCoinCollected(int coinID)
    {
        if (coinID >= 0 && coinID < collectedCoins.Length)
        {
            return collectedCoins[coinID];
        }
        return false;
    }

    public void ResetCollectedCoins()
    {
        for (int i = 0; i < collectedCoins.Length; i++)
        {
            collectedCoins[i] = false;
        }
    }
}
