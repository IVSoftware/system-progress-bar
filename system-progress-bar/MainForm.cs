using System;
using System.Xml.Schema;

namespace system_progress_bar
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            buttonUnzip.Click += onButtonUnzip;
        }


        private async void onButtonUnzip(object? sender, EventArgs e)
        {
            progressBar1.Visible = true;
            Progress<int> progress = new Progress<int>(callback);
            await Task.Run(() => FileUtils.UnzipTo("mockSource", "mockDest", true, progress));
            progressBar1.Visible = false;
        }

        private void callback(int value)
        {
            progressBar1.Value = value;
        }
    }
    class FileUtils
    {
        static string[] GetFilesMock() => new string[25];
        static void UnzipOneMock(string sourceDir, string targetDir, string file) 
            => Thread.Sleep(TimeSpan.FromMilliseconds(100));
        public static void UnzipTo(
            string sourceDir,
            string targetDir,
            bool option, 
            IProgress<int> progress)
        {
            var files = GetFilesMock();
            int completed = 1;

            double total = files.Length;
            foreach (var file in files)
            {
                UnzipOneMock(sourceDir, targetDir, file);
                var net = (int)(completed++ * 100 / total);
                progress.Report(net);
            }
        }
    }
}