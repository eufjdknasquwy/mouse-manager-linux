using System;
using System.Text;
using System.Diagnostics;

namespace Modules {
    public class Requirements
    {
        public Requirements()
        {
            this.check_requirements();
        }
        public void check_requirements()
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "bash",
                    Arguments = $"-c \"command -v xinput\"",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    StandardOutputEncoding = Encoding.UTF8,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };
            process.Start();
            process.WaitForExit();
            if (process.ExitCode != 0)
            {
                Console.WriteLine("Ошибка: программа xinput не обнаружена, установите ее используя пакетный менеджер своего дистрибутива, например Arch: 'sudo pacman -S xorg-xinput'");
                Environment.Exit(1);
            }
            else Console.WriteLine("Проверка пакетов пройдена");
        }
    }
}