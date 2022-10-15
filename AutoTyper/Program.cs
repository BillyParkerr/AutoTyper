using System.Diagnostics;
using System.Runtime.InteropServices;

namespace AutoTyper;

internal class Program
{
    [DllImport("user32.dll")]
    public static extern int SetForegroundWindow(IntPtr hWnd);

    [STAThread]
    public static void Main()
    {
        while (true)
        {
            try
            {
                {
                    Process process = GetProcess();
                    string textToType = GetText();
                    TypeToProcess(process, textToType);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n" + ex + "\n");
            }
        }
    }

    private static void TypeToProcess(Process process, string text)
    {
        SetForegroundWindow(process.MainWindowHandle);
        SendKeys.SendWait(text);
        Console.WriteLine("The text has been sent!\n");
    }

    private static Process GetProcess()
    {
        while (true)
        {
            Console.WriteLine("Enter the process of the program you want typing too:");
            string? processName = Console.ReadLine();
            if (!string.IsNullOrEmpty(processName))
            {
                var processes = Process.GetProcessesByName(processName);
                if (!processes.Any())
                {
                    Console.WriteLine($"No processes found with the name: {processName}");
                }
                else
                {
                    return processes.First();
                }
            }
        }
    }

    private static string GetText()
    {
        while (true)
        {
            Console.WriteLine("Please enter the text you wish to be typed:");
            string? text = Console.ReadLine();
            if (!string.IsNullOrEmpty(text))
            {
                {
                    return text;
                }
            }
        }
    }
}