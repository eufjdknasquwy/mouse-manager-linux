using Gtk;
using System;
using MainWindow = Gui.MainWindow;
using Requirements = Modules.Requirements;

Application.Init();

var requirements = new Requirements();

var main_window = new MainWindow();
main_window.show();
Application.Run();
Environment.Exit(0);