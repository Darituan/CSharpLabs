namespace Lab05.Tools.Managers
{
    internal static class ProcessManager
    {
        private static ProcessesInfo _processesInfo;

        public static ProcessesInfo ProcessesInfo => _processesInfo;

        internal static void Initialize()
        {
            _processesInfo = new ProcessesInfo();
        }
    }
}