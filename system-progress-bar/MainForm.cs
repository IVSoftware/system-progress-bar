using System;
using System.Numerics;
using System.Xml.Schema;
using System.Runtime.InteropServices;

namespace system_progress_bar
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            buttonUnzip.Click += onButtonUnzip;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            SetWindowTheme(progressBar1.Handle, string.Empty, string.Empty);
            progressBar1.ForeColor = Color.Aqua;
        }

        [DllImport("uxtheme", ExactSpelling = true, CharSet = CharSet.Unicode)]
        public extern static Int32 SetWindowTheme(IntPtr hWnd,
                      String textSubAppName, String textSubIdList);


        private async void onButtonUnzip(object? sender, EventArgs e)
        {
            progressBar1.Visible = true;
            progressBar1.Value = 0;
            Progress<int> progress = new Progress<int>(value => {
                progressBar1.Value = value; progressBar1.Update();
            });
            await Task.Run(() => FileUtils.UnzipTo("mockSource", "mockDest", true, progress));
            progressBar1.Visible = false;
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
            int complete = 0;

            int total = files.Length;
            foreach (var file in files)
            {
                UnzipOneMock(sourceDir, targetDir, file);
                progress.Report((int)(++complete / (double)total * 100));
            }
        }
    }
}