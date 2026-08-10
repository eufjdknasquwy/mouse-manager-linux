using Gtk;
using System;
using Gui;
using Modules;
using System.Text;
using System.Diagnostics;

namespace Modules {
    public class XInputManager
    {
        private Dictionary<string, int> mice_list;
        private MainWindow main_window;
        private string mouse_name = null!;
        private int mouse_id;
        private string mouse_speed = null!;
        public XInputManager(MainWindow main_window)
        {
            var getmouse = new GetMouse();
            this.mice_list = getmouse.get_mice_dict();
            this.main_window = main_window;
        }
        public void update_checkbox_state(Gtk.CheckButton check)
        {
            this.check_mid();
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "bash",
                    Arguments = $"-c \"xinput list-props {this.mouse_id} | grep 'libinput Accel Profile Enabled' | grep -oE '[0-9]+' | head -2 | grep -v 300\"",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    StandardOutputEncoding = Encoding.UTF8,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };
            process.Start();
            string status = process.StandardOutput.ReadToEnd().Trim();
            process.WaitForExit();
            check.Active = status == "1";
        }
        public void toggle_acceleration(object sender, EventArgs e)
        {
            var check = sender as CheckButton;
            if (check == null) return;

            this.check_mid();
            if (check.Active)
            {
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "bash",
                        Arguments = $"-c \"xinput set-prop {this.mouse_id} 'libinput Accel Profile Enabled' 1, 0, 0\"",
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        StandardOutputEncoding = Encoding.UTF8,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    }
                };
                process.Start();
                process.WaitForExit();
                Console.WriteLine("Ускорение включено");
            }
            else
            {
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "bash",
                        Arguments = $"-c \"xinput set-prop {this.mouse_id} 'libinput Accel Profile Enabled' 0, 1, 0\"",
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        StandardOutputEncoding = Encoding.UTF8,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    }
                };
                process.Start();
                process.WaitForExit();
                Console.WriteLine("Ускорение выключено");
            }
        }
        public void on_mouse_change(object sender, EventArgs e)
        {
            var combobox = sender as ComboBoxText;
            if (combobox == null) return;

            this.mouse_name = combobox.ActiveText;
            this.mouse_id = mice_list[$"{this.mouse_name}"];
            this.check_mid();
            this.mouse_speed = this.get_mouse_speed();
            this.update_scale(this.mouse_speed);
            this.update_checkbox_state(this.main_window.check);
        }
        public void on_speed_change(object sender, EventArgs e)
        {
            var scale = sender as Scale;
            if (scale == null) return;

            this.check_mid();
            string speed = scale.Value.ToString().Replace(',', '.');
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "bash",
                    Arguments = $"-c \"xinput set-prop {this.mouse_id} 'libinput Accel Speed' {speed}\"",
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
                Console.WriteLine("Ошибка установки скорости");
                Environment.Exit(1);
            }
            else Console.WriteLine($"Текущая скорость: {speed}");
        }
        public string get_mouse_speed()
        {
            this.check_mid();
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "bash",
                    Arguments = $"-c \"xinput list-props {this.mouse_id} | grep 'libinput Accel Speed' | awk '{{print $5}}' | grep -v '('\"",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    StandardOutputEncoding = Encoding.UTF8,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };
            process.Start();
            string result = process.StandardOutput.ReadToEnd().Trim();
            process.WaitForExit();
            return result;
        }
        public void update_scale(string speed)
        {
            float speedFloat = float.Parse(speed.Replace('.', ','));
            this.main_window.scale.Value = speedFloat;
        }
        public bool check_mid()
        {
            if (this.mouse_id == 0)
            {
                Console.WriteLine("Мышь не найдена");
                return false;
            }
            return true;
        }
    }
}