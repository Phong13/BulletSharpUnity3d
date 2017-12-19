namespace DemoFramework
{
    public interface IDemoConfiguration
    {
        ISimulation CreateSimulation(Demo demo);
    }
}
