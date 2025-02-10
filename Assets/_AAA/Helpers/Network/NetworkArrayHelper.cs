using Netick.Unity;

public static class NetworkArrayHelper
{
    public static bool Contains<T>(this NetworkArray<T> networkArray, T value) where T : unmanaged
    {
        foreach (var t in networkArray)
        {
            if (t.Equals(value))
                return true;
        }

        return false;
    }
    
    public static void Clear<T>(this NetworkArray<T> networkArray, int size) where T : unmanaged
    {
        for (var i = 0; i < size; i++)
        {
            networkArray[i] = default;
        }
    }
    
    public static void Remove<T>(this NetworkArray<T> networkArray, T value) where T : unmanaged
    {
        for (var i = 0; i < networkArray.Length; i++)
        {
            if (!networkArray[i].Equals(value)) 
                continue;
            networkArray[i] = default;
            return;
        }
    }
}