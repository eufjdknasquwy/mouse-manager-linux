#nullable disable
using System;
using System.Text;
using System.Diagnostics;

namespace Modules {
    public class GetMouse
    {
        private Dictionary<string, int> mice_dict;
        public GetMouse()
        {
            mice_dict = new Dictionary<string, int>();
        }
        public List<string> get_available_mice()
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "bash",
                    Arguments = $"-c \"xinput list --name-only | grep -i mouse\"",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    StandardOutputEncoding = Encoding.UTF8,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };
            process.Start();
            string output = process.StandardOutput.ReadToEnd().Trim();
            process.WaitForExit();

            List<string> result = new List<string>();
            foreach (string line in output.Split('\n'))
            {
                if (!string.IsNullOrEmpty(line))
                    result.Add(line.Trim());
            }
            return result;
        }
        public List<string> get_mice_ids()
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "bash",
                    Arguments = $"-c \"xinput list | grep -i mouse | grep -oP 'id=\\K[0-9]+'\"",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    StandardOutputEncoding = Encoding.UTF8,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };
            process.Start();
            //List<string> result = new List<string> {process.StandardOutput.ReadToEnd().Split('\n').Trim()};
            string output = process.StandardOutput.ReadToEnd().Trim();
            process.WaitForExit();
            List<string> result = new List<string>();
            foreach (string line in output.Split('\n'))
            {
                if (!string.IsNullOrEmpty(line))
                    result.Add(line.Trim());
            }
            return result;
        }
        public Dictionary<string, int> get_mice_dict()
        {
            List<string> mice_names = this.get_available_mice();
            List<string> mice_ids = this.get_mice_ids();
            for (int i = 0; i < mice_names.Count; i++)
            {
                this.mice_dict[mice_names[i]] = int.Parse(mice_ids[i]);
            }
            if (this.mice_dict.Count == 0)
            {
                Console.WriteLine("Мыши не обнаружены");
                Environment.Exit(1);
                return this.mice_dict;
            }
            else
            {
                return this.mice_dict;
            }
        }
    }
}