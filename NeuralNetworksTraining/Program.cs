namespace NeuralNetworksTraining
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(Form1.getInstance());
        }
    }
}