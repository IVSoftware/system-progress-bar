using System;

namespace system_progress_bar
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            buttonUnzip.Click += onButtonUnzip;
        }


        private void onButtonUnzip(object? sender, EventArgs e)
        {
            BeginInvoke(new Action(() =>
            {
                progressBar1.Minimum = 0;
                progressBar1.Maximum = FileUtils.GetFileCountMock();
                Progress<int> progress = new Progress<int>(callback);
                FileUtils.UnzipTo("mockSource", "mockDest", true, progress);
            }));
        }

        private void callback(int obj)
        {
            progressBar1.Value = obj;
        }
    }
    class FileUtils
    {
        public static int GetFileCountMock() => 25;
        public static async void UnzipTo(
            string targetDir,
            string sourceDir,
            bool option, 
            IProgress<int> progress)
        {
            for (int complete = 0; complete < GetFileCountMock(); complete++)
            {
                // Unzip the file
                // Thread.Sleep(TimeSpan.FromSeconds(1));
                await Task.Delay(500);
                progress.Report(complete);
            }
        }
    }
}