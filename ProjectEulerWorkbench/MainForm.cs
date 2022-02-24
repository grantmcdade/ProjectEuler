using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ProjectEulerWorkbench.Problems;
using Microsoft.Win32;
using Microsoft.Practices.Unity;
using ProjectEuler.Library;

namespace ProjectEulerWorkbench
{
    public partial class MainForm : Form
    {
        private const string ApplicationKeyName = "HKEY_CURRENT_USER\\Software\\Rulkosh\\ProjectEulerWorkbench";
        private const string LastProjectValueName = "LastProject";
        private string _answer;
        private string _timeTaken;
        private IProblem _currentProblem;
        private UnityContainer _container = new UnityContainer();

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            var lastProject = Registry.GetValue(ApplicationKeyName, LastProjectValueName, "1");
            if (lastProject != null)
                ProblemNumber.Value = Convert.ToDecimal(lastProject);

            // Configure the DI container
            _container.RegisterType<IPathProvider, PathProvider>();
            _container.RegisterTypes(
                AllClasses.FromLoadedAssemblies(),
                WithMappings.None,
                WithName.Default);
        }

        private void Go_Click(object sender, EventArgs e)
        {
            try
            {
                Registry.SetValue(ApplicationKeyName, LastProjectValueName, ProblemNumber.Value);

                Type problemType = Type.GetType($"ProjectEulerWorkbench.Problems.Problem{Convert.ToInt32(ProblemNumber.Value):000}, ProjectEulerWorkbench");
                if (problemType != null)
                {
                    _currentProblem = _container.Resolve(problemType) as IProblem;
                    Description.Text = _currentProblem.Description;
                    Answer.Text = "Calculating...";
                    Go.Enabled = false;
                    ProblemNumber.ReadOnly = true;
                    worker.RunWorkerAsync();
                }
                else
                {
                    MessageBox.Show(this, "Problem has no solution yet!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.ToString());
            }
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            Stopwatch timer = new Stopwatch();
            try
            {
                timer.Start();
                _answer = _currentProblem.Solve();
                timer.Stop();
                _timeTaken = $"{timer.ElapsedMilliseconds} ms";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _currentProblem = null;
            Answer.Text = _answer;
            TimeTaken.Text = _timeTaken;
            Go.Enabled = true;
            ProblemNumber.ReadOnly = false;
        }
    }
}
