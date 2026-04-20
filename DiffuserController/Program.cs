using Microsoft.Extensions.Configuration;
using Microsoft.Win32.SafeHandles;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Text.Json;

namespace DiffuserController
{
    public class AppSettings
    {
        public string api_key { get; set; }
    }
    internal static class Program
    {
        [DllImport("kernel32.dll", EntryPoint = "AllocConsole", SetLastError = true, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern Boolean AllocConsole();

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr CreateFile(
    string lpFileName,
    uint dwDesiredAccess,
    uint dwShareMode,
    uint lpSecurityAttributes,
    uint dwCreationDisposition,
    uint dwFlagsAndAttributes,
    uint hTemplateFile);

        private const int MY_CODE_PAGE = 949;
        private const uint GENERIC_WRITE = 0x40000000;
        private const uint FILE_SHARE_WRITE = 0x2;
        private const uint OPEN_EXISTING = 0x3;

        public static string ApiKey { get; private set; } = "";
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            ShowConsoleWindow();
            SelfElevatedProcess(args);
            ReadApiKey();
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new frmMain());
        }

        private static void ReadApiKey()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "appSetting.prod.json";
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                AppSettings se = JsonSerializer.Deserialize<AppSettings>(json);
                Program.ApiKey = se.api_key;
            }
        }

        private static void SelfElevatedProcess(string[] args)
        {
            //vista 미만일때는 패스
            if (Environment.OSVersion.Version.Major < 6)
            {
                Console.WriteLine("관리자 권한으로 실행안함");
                return;
            }
            Console.WriteLine("관리자 권한으로 실행준비...");

            if (!IsRunAsAdmin())
            {
                Console.WriteLine("관리자 권한이 없으므로 관리자 권한으로 실행");
                // Launch itself as administrator
                ProcessStartInfo proc = new ProcessStartInfo();
                proc.UseShellExecute = true;
                proc.WorkingDirectory = Environment.CurrentDirectory;
                proc.FileName = Application.ExecutablePath;

                Console.WriteLine("관리자권한으로 실행 -- 관리자권한으로 실행할 프로세스 : " + proc.FileName);

                string argument = string.Empty;
                if (args != null)
                {
                    for (int i = 0; i < args.Length; i++)
                    {
                        if (argument == string.Empty)
                        {
                            argument = args[i];
                        }
                        else
                        {
                            argument += " " + args[i];
                        }
                    }
                }
                proc.Arguments = argument;
                proc.Verb = "runas";

                try
                {
                    Process.Start(proc);
                    Console.WriteLine("관리자 권한으로 실행...");
                    Application.Exit(); // Quit itself

#if !DEBUG
                    Environment.Exit(0);
#endif
                }
                catch (Exception ex)
                {
                    // The user refused to allow privileges elevation.
                    // Do nothing and return directly ...
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private static bool IsRunAsAdmin()
        {
            bool isAdmin = false;
            try
            {
                WindowsIdentity id = WindowsIdentity.GetCurrent();
                WindowsPrincipal principal = new WindowsPrincipal(id);
                isAdmin = principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
            catch
            {
            }
            return isAdmin;
        }
        private static void ShowConsoleWindow()
        {
            bool OpenConsole = false;
#if DEBUG
            OpenConsole = true;
#else
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "CTest.dat"))
                OpenConsole = true;
#endif
            if (OpenConsole)
            {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                if (!AllocConsole())
                    MessageBox.Show("Console Window Load Failed");
                else
                {
                    IntPtr stdHandle = CreateFile("CONOUT$", GENERIC_WRITE, FILE_SHARE_WRITE, 0, OPEN_EXISTING, 0, 0);
                    SafeFileHandle safeFileHandle = new SafeFileHandle(stdHandle, true);
                    FileStream fileStream = new FileStream(safeFileHandle, FileAccess.Write);
                    Encoding encoding = System.Text.Encoding.GetEncoding(MY_CODE_PAGE);
                    StreamWriter standardOutput = new StreamWriter(fileStream, encoding);
                    standardOutput.AutoFlush = true;
                    Console.SetOut(standardOutput);
                    Console.WriteLine("This will show up in the Console window.");
                }
            }
        }
    }
}