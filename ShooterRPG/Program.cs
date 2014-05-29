namespace ShooterRPG
{
    using System;
    
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            ChaosGame.Instance.Run();
        }
    }
}