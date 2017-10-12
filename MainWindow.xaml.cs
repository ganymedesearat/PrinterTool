using System.Windows;
using System.ServiceProcess;
using System;

namespace SDPrinterTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        string path = "";
        string wsid = "";

        private void btnInfo_Click(object sender, RoutedEventArgs e) //Open printer info 
        {
            path = txtPath.Text;

            if (path.Equals(""))
            {
                MessageBox.Show("You must enter a printer path.");
                return;
            }

            string cmd = "/Xg /n" + path;
            System.Diagnostics.Process.Start("printui.exe", cmd);
        }

        private void btnQueue_Click(object sender, RoutedEventArgs e) //Open printer queue
        {
            path = txtPath.Text;

            if (path.Equals(""))
            {
                MessageBox.Show("You must enter a printer path.");
                return;
            }

            string cmd = "/o /n" + path;
            System.Diagnostics.Process.Start("printui.exe", cmd);
        }

        private void btnProp_Click(object sender, RoutedEventArgs e) //Bring up printer properties
        {
            path = txtPath.Text;

            if (path.Equals(""))
            {
                MessageBox.Show("You must enter a printer path.");
                return;
            }

            string cmd = "/p /n" + path;
            System.Diagnostics.Process.Start("printui.exe", cmd);
        }

        private void btnSpool_Click(object sender, RoutedEventArgs e) //Start\stop printer spooler
        {
            wsid = txtWSID.Text;
            string cmd = @"/c sc \\" + wsid + " query spooler ";

            System.Diagnostics.Process prc = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo strtInfo = new System.Diagnostics.ProcessStartInfo();


            strtInfo.FileName = "cmd.exe";
            strtInfo.RedirectStandardOutput = true;
            strtInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            strtInfo.UseShellExecute = false;
            strtInfo.Arguments = cmd;

            if (wsid.Equals(""))
            {
                MessageBox.Show("You must enter a WSID.");
                return;
            }

            prc.StartInfo = strtInfo;
            prc.Start();
            prc.StandardOutput.ReadToEnd(); //Figure out how to gather the spooler data, then pick whether or not to start/stop.



            //ServiceController controller = new ServiceController("spooler");

            //if (wsid.Equals(""))
            //{
            //    MessageBox.Show("You must enter a WSID.");
            //    return;
            //}
            //controller.MachineName = wsid;

            //try
            //{
            //    string status = controller.Status.ToString();


            //    if (status.CompareTo("Running") == 0)
            //    {
            //        controller.Stop();
            //        MessageBox.Show("Service Stopped.");
            //    }
            //    else
            //    {
            //        MessageBox.Show("Service stopped, starting service.");
            //        controller.Start();
            //    }
            //}
            //catch (InvalidOperationException)
            //{
            //    MessageBox.Show("Invalid WSID, or you do not have the permissions to do this.");
            //}

        }

        private void btnClear_OnClick(object sender, RoutedEventArgs e)
        {
            txtPath.Text = "";
            txtWSID.Text = "";
        }

        private void btnMapPrinter_Click(object sender, RoutedEventArgs e) //Map printer to WSID
        {
            path = txtPath.Text;
            wsid = txtWSID.Text;

            if(path.Equals("") || wsid.Equals(""))
            {
                MessageBox.Show("Please check your input and try again.");
                return;
            }

            string cmd = "/ga /c" + @"\\" + wsid + " /n" + path;
            System.Diagnostics.Process.Start("printui.exe", cmd);
            MessageBox.Show("Printer mapped, please refresh.");
        }

        private void btnTest_Click(object sender, RoutedEventArgs e) //Send test print to printer
        {
            path = txtPath.Text;

            System.Diagnostics.Process prc = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();

            startInfo.FileName = "printui.exe";
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.UseShellExecute = false;

            if(path.Equals(""))
            {
                MessageBox.Show("You must enter a printer path.");
                return;
            }

            string cmd = @"/k /n" + "\"" + path + "\"";
            startInfo.Arguments = cmd;
            Console.WriteLine(cmd);

            prc.StartInfo = startInfo;
            prc.Start();
            prc.Close();

        }
    }
}
