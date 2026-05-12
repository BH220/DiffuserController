using DiffuserControllerNew.Common;
using DiffuserControllerNew.Views;
using IWshRuntimeLibrary;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32.SafeHandles;
using Hardcodet.Wpf.TaskbarNotification;  
using System.Windows.Controls; 
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Printing;
using Microsoft.Win32;

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
        public static string ApiKey { get; private set; } = "";
        
        private TaskbarIcon _trayIcon;
        private MainView _mainView;

        public App()
        {
            AppDomain.CurrentDomain.ProcessExit += OnProcessExit;
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
        }


        protected override void OnExit(ExitEventArgs e)
        {
            
            _trayIcon?.Dispose();

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
#if DEBUG
#else
            MakeShortCut();
            AttachRegistry();
            RegisterTaskScheduler();
#endif

            // API 키 로드
            KeyLoad();

            // ↓ 수정: MainView를 필드에 저장
            _mainView = Services.GetRequiredService<MainView>();
            _mainView.Closing += Window_Closing;
            InitTrayIcon();
        }

        private void RegisterTaskScheduler()
        {
            try
            {
                string appPath = Environment.ProcessPath;
                string taskName = "DiffuserController";

                using (var taskService = new Microsoft.Win32.TaskScheduler.TaskService())
                {
                    // 기존 작업 있으면 삭제
                    taskService.RootFolder.DeleteTask(taskName, false);

                    var task = taskService.NewTask();
                    task.RegistrationInfo.Description = "Diffuser Controller 자동 시작";

                    // 로그온 시 시작
                    task.Triggers.Add(new Microsoft.Win32.TaskScheduler.LogonTrigger());

                    // 관리자 권한으로 실행
                    task.Principal.RunLevel = Microsoft.Win32.TaskScheduler.TaskRunLevel.Highest;

                    task.Actions.Add(new Microsoft.Win32.TaskScheduler.ExecAction(appPath));

                    task.Settings.DisallowStartIfOnBatteries = false;
                    task.Settings.StopIfGoingOnBatteries = false;

                    taskService.RootFolder.RegisterTaskDefinition(taskName, task);
                    Console.WriteLine("작업 스케줄러 등록 완료");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"작업 스케줄러 등록 실패: {ex.Message}");
            }
        }

        private void AttachRegistry()
        {
            try
            {
                string appPath = Environment.ProcessPath;
                string startMenuPath = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.StartMenu),
                    "Programs");
                string shortcutPath = Path.Combine(startMenuPath, "디퓨저 제어기.lnk");

                // 이미 있으면 스킵
                if (System.IO.File.Exists(shortcutPath))
                    System.IO.File.Delete(shortcutPath);

                // 아이콘 임시 추출
                string icoPath = Path.Combine(Path.GetTempPath(), "main_ico.ico");
                using (var stream = Application.GetResourceStream(
                    new Uri("pack://application:,,,/Resources/main_ico.ico")).Stream)
                using (var fileStream = new FileStream(icoPath, FileMode.Create))
                {
                    stream.CopyTo(fileStream);
                }

                WshShell shell = new WshShell();
                IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutPath);
                shortcut.TargetPath = appPath;
                shortcut.WorkingDirectory = Path.GetDirectoryName(appPath);
                shortcut.Description = "Diffuser Controller";
                shortcut.IconLocation = icoPath;
                shortcut.Save();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"시작 메뉴 등록 실패: {ex.Message}");
            }
        }

        private static void KeyLoad()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "appSetting.prod.json";
            if (System.IO.File.Exists(path))
            {
                string json = System.IO.File.ReadAllText(path);
                AppSettings se = JsonSerializer.Deserialize<AppSettings>(json);
                App.ApiKey = se.api_key;
            }
        }
        
        private void InitTrayIcon()
        {
            _trayIcon = new TaskbarIcon();
            _trayIcon.Icon = new System.Drawing.Icon(Application.GetResourceStream(new Uri("pack://application:,,,/Resources/main_ico.ico")).Stream);
            _trayIcon.ToolTipText = $"디퓨저 제어기";
            _trayIcon.TrayMouseDoubleClick += (s, e) => ShowMainWindow();

            var menu = new ContextMenu();

            var openItem = new MenuItem { Header = "프로그램 열기" };
            openItem.Click += (s, e) => ShowMainWindow();
            

            var closeItem = new MenuItem { Header = "프로그램 닫기" };
            closeItem.Click += (s, e) => ExitApp();

            menu.Items.Add(openItem);
            menu.Items.Add(new Separator());
            menu.Items.Add(closeItem);
            _trayIcon.ContextMenu = menu;
        }
        
        private void ExitApp()
        {
            _trayIcon?.Dispose();
            Application.Current.Shutdown();
            Environment.Exit(0);
        }
        
        private void ShowMainWindow()
        {
            if (!_mainView.IsVisible)
            {
                var screenWidth = SystemParameters.WorkArea.Width;
                var screenHeight = SystemParameters.WorkArea.Height;
                int detailPoint = 8;
                _mainView.Left = screenWidth - _mainView.Width + detailPoint;
                _mainView.Top = screenHeight - _mainView.Height + detailPoint;
                _mainView.Show();
            }
            _mainView.WindowState = WindowState.Normal;
            _mainView.Activate();
        }
  
        private static void Window_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            if (sender is Window win)
                win.Hide();
        }



        private void MakeShortCut()
        {
            string appPath = Environment.ProcessPath;
            string shortcutName = "디퓨저 제어기.lnk"; // 생성할 바로가기 파일 이름
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            string shortcutPath = Path.Combine(desktopPath, shortcutName);

            try
            {
                // 1. WScript.Shell을 사용하여 기본 바로가기 생성
                WshShell shell = new WshShell();
                IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutPath);

                shortcut.TargetPath = appPath;
                shortcut.WorkingDirectory = Path.GetDirectoryName(appPath);
                shortcut.Description = "Diffuser Contoller";
                string icoPath = Path.Combine(Path.GetTempPath(), "main_ico.ico");
                using (var stream = Application.GetResourceStream(
                    new Uri("pack://application:,,,/Resources/main_ico.ico")).Stream)
                using (var fileStream = new FileStream(icoPath, FileMode.Create))
                {
                    stream.CopyTo(fileStream);
                }

                shortcut.IconLocation = icoPath;

                shortcut.Save();
                byte[] shortcutBytes = System.IO.File.ReadAllBytes(shortcutPath);
                shortcutBytes[0x15] = (byte)(shortcutBytes[0x15] | 0x20);
                System.IO.File.WriteAllBytes(shortcutPath, shortcutBytes);
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
