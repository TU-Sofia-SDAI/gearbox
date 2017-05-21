namespace GearboxContracts
{
    public interface IGearboxComputerListener
    {
        void Receive(int currentGear, int nextGear);
    }
}
