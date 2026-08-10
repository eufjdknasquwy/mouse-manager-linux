# 🖱️ Mouse Manager

A simple mouse control program for Linux (C# + GTK)

## Who is this for?

For people who switched to a tiling window manager (bspwm, i3, awesome, etc.) and don't want to bother with writing their own mouse control program.

Just install it — and it works.

---

## Features

- 🖱️ Select a mouse device
- 📊 Adjust mouse speed
- 🚀 Toggle mouse acceleration

---

## Dependencies

- `bash`
- `xorg-xinput`

Make sure `xinput` is installed. If not:

```bash
# Arch
sudo pacman -S xorg-xinput

# Debian based (Ubuntu, Mint)
sudo apt install xinput

# Fedora based
sudo dnf install xinput
```

## Installation

```bash
# Build from source
1. git clone https://github.com/eufjdknasquwy/mouse-manager-linux.git
2. cd mouse-manager-linux
3. dotnet publish -c Release -r linux-x64 --self-contained true -p:PublishSingleFile=true
4. ./bin/Release/net8.0/linux-x64/publish/mouse-manager
```




# 🖱️ Mouse Manager

Простая программа для управления мышью в Linux (C# + GTK)

## Для кого этот проект?

Для людей, которые перешли на тайлинговый менеджер окон (bspwm, i3, awesome и т.д.) и не хотят заморачиваться с написанием своей программы для управления мышью.

Просто установил — и работает.

---

## Возможности

- 🖱️ Выбор устройства мыши
- 📊 Регулировка скорости
- 🚀 Включение/выключение ускорения

---

## Зависимости

- `bash`
- `xorg-xinput`

Убедитесь, что `xinput` установлен. Если нет:

```bash
# Arch
sudo pacman -S xorg-xinput

# Debian based (Ubuntu, Mint)
sudo apt install xinput

# Fedora based
sudo dnf install xinput
```

## Установка

```bash
# Собрать из исходников
1. git clone https://github.com/eufjdknasquwy/mouse-manager-linux.git
2. cd mouse-manager-linux
3. dotnet publish -c Release -r linux-x64 --self-contained true -p:PublishSingleFile=true
4. ./bin/Release/net8.0/linux-x64/publish/mouse-manager
```