using System.Numerics;

public interface Unlockable
{
    public void Unlock();
    public BigInteger GetUnlockCost();
}