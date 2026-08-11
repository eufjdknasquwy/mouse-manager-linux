#nullable disable
using Gtk;
using GLib;
using System;
using XInputManager = Modules.XInputManager;
using Modules;

namespace Gui {
    public class MainWindow
    {
        private Dictionary<string, int> margin_config = new Dictionary<string, int>
        {
            {"margin_top", 5},
            {"margin_bottom", 5},
            {"margin_start", 5},
            {"margin_end", 5}
        };
        private Dictionary<string, string> win_config = new Dictionary<string, string>
        {
            {"name", "Управление мышью"},
            {"icon", "input-mouse"}
        };
        private Dictionary<string, int> win_size_config = new Dictionary<string, int>
        {
            {"width", 300},
            {"height", 280}
        };
        private Dictionary<string, int> mice_list;
        private XInputManager xinput_manager;
        private Gtk.Dialog dialog;
        private Gtk.Box content;
        private Gtk.Label label;
        private Gtk.Label label2;
        private Gtk.ComboBoxText combobox;
        public Gtk.Scale scale;
        public Gtk.CheckButton check;
        private Gtk.Notebook notebook;
        private Gtk.Box page1;
        private Gtk.Box page2;
        public MainWindow()
        {
            var getmouse = new GetMouse();
            this.mice_list = getmouse.get_mice_dict();
            this.xinput_manager = new XInputManager(this);
            this.dialog = new Gtk.Dialog(win_config["name"], null, 0);
            this.dialog.SetPosition(Gtk.WindowPosition.Center);
            this.dialog.SetDefaultSize(win_size_config["width"], win_size_config["height"]);
            this.content = this.dialog.ContentArea;
            this.dialog.AddButton("Закрыть", Gtk.ResponseType.Ok);
            this.dialog.Response += this.quit;

            this.create_pages();
            this.create_label();
            this.create_combobox();
            this.create_second_label();
            this.create_scale();
            this.create_checkbox();
            this.xinput_manager.on_mouse_change(this.combobox, EventArgs.Empty);

            GLib.Timeout.Add(500, refresh);
        }
        public void quit(object sender, ResponseArgs args)
        {
            if (args.ResponseId == ResponseType.Ok) Environment.Exit(0);
        }
        public bool refresh()
        {
            this.xinput_manager.on_mouse_change(this.combobox, EventArgs.Empty);
            return true;
        }
        public void set_margin_all(Gtk.Widget widget)
        {
            widget.MarginTop = margin_config["margin_top"];
            widget.MarginBottom = margin_config["margin_bottom"];
            widget.MarginStart = margin_config["margin_start"];
            widget.MarginEnd = margin_config["margin_end"];
        }
        public void create_label()
        {
            this.label = new Gtk.Label(win_config["name"]);
            this.label.Halign = Align.Start;
            this.set_margin_all(this.label);
            this.page1.PackStart(this.label, false, false, 0);
        }
        public void create_second_label()
        {
            this.label2 = new Gtk.Label("Настройки скорости");
            this.label2.Halign = Align.Start;
            this.set_margin_all(this.label2);
            this.page1.PackStart(this.label2, false, false, 0);
        }
        public void create_combobox()
        {
            this.combobox = new Gtk.ComboBoxText();
            foreach (var mouse in mice_list.Keys) this.combobox.AppendText(mouse);
            this.combobox.Active = 0;
            this.set_margin_all(this.combobox);
            this.page1.PackStart(this.combobox, false, false, 0);
            this.combobox.Changed += this.xinput_manager.on_mouse_change;
        }
        public void create_scale()
        {
            this.scale = new Scale(Orientation.Horizontal, null);
            this.scale.SetRange(-1, 1);
            this.scale.SetIncrements(0.1, 0.1);
            this.scale.ValueChanged += this.xinput_manager.on_speed_change;
            this.set_margin_all(this.scale);
            this.page1.PackStart(this.scale, false, false, 0);
        }
        public void create_checkbox()
        {
            this.check = new Gtk.CheckButton("Ускорение мыши");
            this.set_margin_all(this.check);
            this.page1.PackStart(this.check, false, false, 0);
            this.check.Toggled += this.xinput_manager.toggle_acceleration;
        }
        public void create_pages()
        {
            this.notebook = new Gtk.Notebook();
            this.content.Add(this.notebook);
            this.page1 = new Gtk.Box(Gtk.Orientation.Vertical, 10);
            this.set_margin_all(this.page1);
            this.notebook.AppendPage(this.page1, new Gtk.Label("Скорость мыши"));
            this.page2 = new Gtk.Box(Gtk.Orientation.Vertical, 10);
            this.set_margin_all(this.page2);
            this.notebook.AppendPage(this.page2, new Gtk.Label("Переназначение клавиш"));
        }
        public void show()
        {
            this.dialog.ShowAll();
        }
    }
}