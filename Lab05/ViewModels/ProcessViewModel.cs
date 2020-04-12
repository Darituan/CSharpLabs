using System;
using System.Diagnostics;
using Lab05.Tools;


namespace Lab05.ViewModels
{
    public class ProcessViewModel: BaseViewModel
    {
        private readonly Process _process;

        private PerformanceCounter _performanceCounter;
        
        private static readonly long TotalMemory = PerformanceInfo.GetTotalMemory();

        private string _startUserName;
        
        private string _startFileName;

        private double _cpuUsagePercentage;
        
        private double _memoryUsagePercentage;
        
        private double _memoryUsage;
        
        internal Process Process => _process;

        public string Name => _process.ProcessName;
        
        public int Id => _process.Id;

        public bool Responding => _process.Responding;

        public double CpuUsagePercentage => _cpuUsagePercentage;
        
        public double MemoryUsagePercentage => _memoryUsagePercentage;
        
        public double MemoryUsage => _memoryUsage;

        public int ThreadCount => _process.Threads.Count;

        public string StartUserName => _startUserName;
        
        public string StartFileName => _startFileName;
        
        public DateTime StartTime => _process.StartTime;

        private string GetStartUserName()
        {
            return ProcessTools.GetUsernameBySessionId(_process.SessionId, true);
        }

        private string GetStartFileName()
        {
            return _process.GetMainModuleFileName();
        }

        private double GetCpuUsagePercentage()
        {
            return Math.Round((double) _performanceCounter.NextValue() / Environment.ProcessorCount, 2);
        }

        private double GetMemoryUsagePercentage()
        {
            return Math.Round(100 * (double) _process.WorkingSet64 / TotalMemory, 2);
        }
        
        private double GetMemoryUsage()
        {
            return Math.Round((((double)_process.WorkingSet64) / 1024) / 1024, 2);
        }

        private void Update()
        {
            _cpuUsagePercentage = GetCpuUsagePercentage();
            _memoryUsagePercentage = GetMemoryUsagePercentage();
            _memoryUsage = GetMemoryUsage();
            _startFileName = GetStartFileName();
            _startUserName = GetStartUserName();
        }

        internal void Refresh()
        {
            _process.Refresh();
            Update();
        }
        
        internal void RefreshAndNotify()
        {
            _process.Refresh();
            Update();
            OnPropertyChanged();
        }

        internal ProcessViewModel(Process process)
        {
            _process = process;
            _performanceCounter = new PerformanceCounter("Process", "% Processor Time", _process.ProcessName, true);
            Update();
        }
    }
}