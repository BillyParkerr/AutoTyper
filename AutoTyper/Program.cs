using System.Diagnostics;
using System.Runtime.InteropServices;

namespace AutoTyper;

internal class Program
{
    /// <summary>
    /// This is used to select a process window in which we can type into.
    /// </summary>
    /// <param name="hWnd"></param>
    /// <returns></returns>
    [DllImport("user32.dll")]
    public static extern int SetForegroundWindow(IntPtr hWnd);

    /// <summary>
    /// Entry point of the program.
    /// </summary>
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

    /// <summary>
    /// This method takes the process and text information gathered and uses it to send the text to the given process.
    /// </summary>
    /// <param name="process"></param>
    /// <param name="text"></param>
    private static void TypeToProcess(Process process, string text)
    {
        SetForegroundWindow(process.MainWindowHandle);
        SendKeys.SendWait(text);
        Console.WriteLine("The text has been sent!\n");
    }

    /// <summary>
    /// Method which attempts to get the name of the process the user is looking for.
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// Method which attempts to get the text which the user will enter.
    /// </summary>
    /// <returns></returns>
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
            else
            {
                Console.WriteLine("Please enter some text, we cannot enter nothing you know!");
            }
        }
    }
}