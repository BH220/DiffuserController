using DiffuserControllerNew.Views;
using IWshRuntimeLibrary;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32.SafeHandles;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Windows;

namespace DiffuserControllerNew
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        [DllImport("kernel32.dll", EntryPoint = "AllocConsole", SetLastError = true, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern Boolean AllocConsole();

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        private const int SW_MINIMIZE = 6;

        public IServiceProvider? Services { get; private set; }

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

        public App()
        {
            AppDomain.CurrentDomain.ProcessExit += OnProcessExit;
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
        }

        protected override void OnExit(ExitEventArgs e)
        {
            // ServiceProvider가 IDisposable 구현체를 자동 Dispose
            if (Services is IDisposable disposable)
                disposable.Dispose();
            base.OnExit(e);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            //관리자 권한 변경
            SelfElevatedProcess();
            //시작전 메모리에 남아 있는 프로세스가 있으면 죽이고 시작
            ProcessCleaner();

            ShowConsoleWindow();

            // 1. 기본 WPF 어플리케이션 초기화
            base.OnStartup(e);

            // DI 컨테이너 구성
            Services = DiService.ServicesRegister();
             

            // 2. 어플리케이션 종료 모드 설정
            ShutdownMode = ShutdownMode.OnExplicitShutdown;

            MakeShortCut();
              

            // MainWindow 설정 및 수동 Show
            var view = Services.GetRequiredService<MainView>();
            ShowWindow(view);

        }

        private static void ShowWindow(Window window)
        {
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            //window.Left = 
            //window.Top = 
            //window.Width = 
            //window.Height = 
            window.Closed += Window_Closed;
            window.WindowState = WindowState.Normal;
            window.Show();
        }

        private static void Window_Closed(object? sender, EventArgs e)
        {
            if (Current != null)
            {
                foreach (Window win in Current.Windows)
                {
                    if (win != sender as Window)
                        win.Close();
                }
            }
            Application.Current.Shutdown();
            Environment.Exit(0); // 혹시 모를 잔여 프로세스를 강제로 종료
        }



        private void MakeShortCut()
        {
            string appPath = Environment.ProcessPath;
            string shortcutName = "System Contoller.lnk"; // 생성할 바로가기 파일 이름
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            string shortcutPath = Path.Combine(desktopPath, shortcutName);

            try
            {
                // 1. WScript.Shell을 사용하여 기본 바로가기 생성
                WshShell shell = new WshShell();
                IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutPath);

                shortcut.TargetPath = appPath;
                shortcut.WorkingDirectory = Path.GetDirectoryName(appPath);
                shortcut.Description = "System Contoller";
                shortcut.IconLocation = appPath; // 필요하다면 아이콘 설정

                shortcut.Save();
                byte[] shortcutBytes = System.IO.File.ReadAllBytes(shortcutPath);
                shortcutBytes[0x15] = (byte)(shortcutBytes[0x15] | 0x20);
                System.IO.File.WriteAllBytes(shortcutPath, shortcutBytes);
                //RegistryHelper.AddKey(Microsoft.Win32.RegistryHive.CurrentUser, "", "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\AppCompatFlags\\Layers", appPath, "~ RUNASADMIN");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"바로가기 생성 실패: {ex.Message}");
            }
        }

        private void ProcessCleaner()
        {
            try
            {
                string currentName = Process.GetCurrentProcess().ProcessName;
                var currentId = Process.GetCurrentProcess().Id;
                var processes = Process.GetProcessesByName(currentName);

                foreach (var p in processes)
                {
                    if (p.Id != currentId)
                    {
                        Console.WriteLine($"ProcessCleaner - Found another process with the same name: {p.ProcessName} (PID: {p.Id}). Attempting to kill it.");
                        p.Kill(); // 다른 동일 이름 프로세스 종료
                    }
                }
            }
            catch { }
        }

        private void SelfElevatedProcess()
        {
            Console.WriteLine("관리자 권한으로 실행준비...");

            if (!IsRunAsAdmin())
            {
                Console.WriteLine("관리자 권한이 없으므로 관리자 권한으로 실행");

                ProcessStartInfo proc = new ProcessStartInfo
                {
                    UseShellExecute = true,
                    WorkingDirectory = Environment.CurrentDirectory,
                    FileName = Environment.ProcessPath,
                    Verb = "runas"
                };

                Console.WriteLine("관리자권한으로 실행 -- 관리자권한으로 실행할 프로세스 : " + proc.FileName);

                try
                {
                    Process.Start(proc);
                    Console.WriteLine("관리자 권한으로 실행...");

                    Application.Current.Shutdown();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("관리자 실행 실패: " + ex.Message);
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

        private void ShowConsoleWindow()
        {
            bool OpenConsole = false;
#if DEBUG
            OpenConsole = true;
#else
            if (System.IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory + "CTest.dat"))
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

        private void TaskScheduler_UnobservedTaskException(object? sender, UnobservedTaskExceptionEventArgs e)
        {
            Console.WriteLine($"[System] 처리되지 않은 Task 예외 발생: {e.Exception}");
        }

        private static void OnProcessExit(object? sender, EventArgs e)
        {
            Console.WriteLine("[System] 어플리케이션이 종료되었습니다.");
            //Application.Current.Shutdown();
        }

        private void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = e.ExceptionObject as Exception ?? new Exception("Unknown Error");
            Console.WriteLine($"[System] 도메인 전체에서 처리되지 않은 예외 발생: {ex.Message}");
        }
    }
}
