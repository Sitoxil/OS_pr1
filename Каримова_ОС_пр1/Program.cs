using System;
using System.IO;
using System.IO.Compression;
using System.Text.Json;
using System.Xml.Linq;

namespace Каримова_ОС_пр1
{
  class Person
  {
    public string Name { get; set; }
    public int Age { get; set; }
  }
  class Program
  {
    static void disc()
    {
      DriveInfo[] drives = DriveInfo.GetDrives();
      foreach (DriveInfo drive in drives)
      {
        Console.WriteLine($"Название: {drive.Name}");
        Console.WriteLine($"Тип: {drive.DriveType}");
        if (drive.IsReady)
        {
          Console.WriteLine($"Объем диска: {drive.TotalSize}");
          Console.WriteLine($"Свободное пространство: {drive.TotalFreeSpace}");
          Console.WriteLine($"Метка: {drive.VolumeLabel}");
        }
        Console.WriteLine();
      }
    }

    static void file()
    {
      string path = @"D:\Documents\Каримова Ангелина БББО-05-19\ОС_Практика1";
      DirectoryInfo dirInfo = new DirectoryInfo(path);
      if (!dirInfo.Exists)
      {
        dirInfo.Create();
      }

      Console.WriteLine("Введите строку, которую хотите записать в файл: ");
      string str;
      str = Console.ReadLine();

      string pathfile = @"D:\Documents\Каримова Ангелина БББО-05-19\ОС_Практика1\note.txt";
      using (FileStream fstream = new FileStream(pathfile, FileMode.OpenOrCreate))
      {
        byte[] array = System.Text.Encoding.Default.GetBytes(str);
        fstream.Write(array, 0, array.Length);
        Console.WriteLine("Текст записан в файл");
      }

      using (FileStream fstream = File.OpenRead(pathfile))
      {
        byte[] array = new byte[fstream.Length];
        fstream.Read(array, 0, array.Length);
        string textFromFile = System.Text.Encoding.Default.GetString(array);
        Console.WriteLine($"Текст из файла: {textFromFile}");
        fstream.Close();

        Console.WriteLine("Вы хотите удалить файл? Нажмите 1 - да, 0 - нет");
        if (Console.ReadLine() == "1")
        {
          File.Delete(pathfile);
          Console.WriteLine("Файл удалён");
        }
        else
        {
          Console.WriteLine("Файл не удалён!");
        }
      }
    }

    static void Json()
    {
      string pathjson = @"D:\Documents\Каримова Ангелина БББО-05-19\ОС_Практика1\file.json";
      // Сериализация объекта класса Person (Tom, 35)
      using (FileStream fs = new FileStream(pathjson, FileMode.OpenOrCreate))
      {
        Person tom = new Person() { Name = "Tom", Age = 35 };
         JsonSerializer.SerializeAsync<Person>(fs, tom);
      }
      // Десериализация
      string jsonString = File.ReadAllText(pathjson);
      Person restoredPerson = JsonSerializer.Deserialize<Person>(jsonString);
      // Вывод данных из файла
      Console.WriteLine("Информация из файла json: ");
      Console.WriteLine($"Имя: {restoredPerson.Name}  Возраст: {restoredPerson.Age}");
      Console.WriteLine("Вы хотите удалить файл json? Нажмите 1 - да, 0 - нет");
      if (Console.ReadLine() == "1")
      {
        File.Delete(pathjson); // Удаление файла json
        Console.WriteLine("Файл удалён");
      }
      else { Console.WriteLine("Файл не удалён"); }
    }
    static void xml()
    {
      // Добавление данных в xml файл
      XDocument xdoc;
      xdoc = new XDocument();
      XElement iphone6 = new XElement("phone");
      Console.WriteLine("Введите название первого телефона");
      string str1 = Console.ReadLine();
      XAttribute iphoneNameAttr = new XAttribute("name", str1);
      Console.WriteLine("Введите название компании");
      string str11 = Console.ReadLine();
      XElement iphoneCompanyElem = new XElement("company", str11);
      Console.WriteLine("Введите стоимость");
      string str111 = Console.ReadLine();
      XElement iphonePriceElem = new XElement("price", str111);
      iphone6.Add(iphoneNameAttr);
      iphone6.Add(iphoneCompanyElem);
      iphone6.Add(iphonePriceElem);

      XElement galaxys5 = new XElement("phone");
      Console.WriteLine("Введите название второго телефона");
      string str2 = Console.ReadLine();
      XAttribute galaxysNameAttr = new XAttribute("name", str2);
      Console.WriteLine("Введите название компании");
      string str22 = Console.ReadLine();
      XElement galaxysCompanyElem = new XElement("company", str22);
      Console.WriteLine("Введите стоимость");
      string str222 = Console.ReadLine();
      XElement galaxysPriceElem = new XElement("price", str222);
      galaxys5.Add(galaxysNameAttr);
      galaxys5.Add(galaxysCompanyElem);
      galaxys5.Add(galaxysPriceElem);
      XElement phones = new XElement("phones"); 
      phones.Add(iphone6);
      phones.Add(galaxys5);
      xdoc.Add(phones);
      string pathxml = @"D:\Documents\Каримова Ангелина БББО-05-19\ОС_Практика1\phones.xml";
      xdoc.Save(pathxml); // Создание xml файла


      Console.WriteLine("Содержимое файла xml: "); // Вывод содержимого файла
      foreach (XElement phoneElement in xdoc.Element("phones").Elements("phone"))
      {
        XAttribute nameAttribute = phoneElement.Attribute("name");
        XElement companyElement = phoneElement.Element("company");
        XElement priceElement = phoneElement.Element("price");

        if (nameAttribute != null && companyElement != null && priceElement != null)
        {
          Console.WriteLine($"Смартфон: {nameAttribute.Value}");
          Console.WriteLine($"Компания: {companyElement.Value}");
          Console.WriteLine($"Цена: {priceElement.Value}");
        }
        Console.WriteLine();
      }
      Console.WriteLine("Вы хотите удалить файл xml? Нажмите 1 - да, 0 - нет"); // Удаление файла
      if(Console.ReadLine()=="1")
      {
        File.Delete(pathxml);
        Console.WriteLine("Файл xml удален");
      }
    }

    static void zip()
    {
      string arch = @"D:\Documents\Каримова Ангелина БББО-05-19\ОС_Практика1\arch.zip";
      string file = @"D:\Documents\Каримова Ангелина БББО-05-19\ОС_Практика1\doc.doc";
      string target = @"D:\Documents\Каримова Ангелина БББО-05-19\ОС_Практика1\doc_new.doc";

      Console.WriteLine("Введите строку, которую хотите записать в файл: ");
      string str;
      str = Console.ReadLine();

      using (FileStream fstream = new FileStream(file, FileMode.OpenOrCreate)) // Побайтовая запись информации в файл
      {
        byte[] array = System.Text.Encoding.Default.GetBytes(str);
        fstream.Write(array, 0, array.Length);
        Console.WriteLine("Текст записан в файл");
      }

      using (FileStream sourceStream = new FileStream(file, FileMode.OpenOrCreate))
      {
        // поток для записи сжатого файла
        using (FileStream targetStream = File.Create(arch))
        {
          // поток архивации
          using (GZipStream compressionStream = new GZipStream(targetStream, CompressionMode.Compress))
          {
            sourceStream.CopyTo(compressionStream); // копируем байты из одного потока в другой
            Console.WriteLine("Сжатие файла {0} завершено. Исходный размер: {1}  Сжатый размер: {2}.", file, sourceStream.Length.ToString(), targetStream.Length.ToString());
          }
        }
      }

      using (FileStream sourceStream = new FileStream(arch, FileMode.OpenOrCreate))
      {
        // поток для записи восстановленного файла
        using (FileStream targetStream = File.Create(target))
        {
          // поток разархивации
          using (GZipStream decompressionStream = new GZipStream(sourceStream, CompressionMode.Decompress))
          {
            decompressionStream.CopyTo(targetStream);
            Console.WriteLine("Восстановлен файл: {0}, размер файла: {1}", target, targetStream.Length.ToString());
          }
        }
      }

      Console.WriteLine("Хотите удалить файлы и архив? (1 - да, 0 - нет)");
      if (Console.ReadLine() == "1")
      {
        // Удаление файлов
        File.Delete(arch);
        File.Delete(file);
        File.Delete(target);
        Console.WriteLine("Файлы удалены");
      }
      else
      {
        Console.WriteLine("Файлы не удалены");
      }
    }
  static void  Main(string[] args)
    {
      bool i = true;
      while (i)
      {
        Console.WriteLine("\tЧто вы хотите сделать?");
        Console.WriteLine("\t1 - Информация о логических дисках, именах, метке тома, размере типе файловой системы (нажмите 1)\n\t2 - Работа с файлами (нажмите 2)\n\t3 - Работа с форматом JSON(нажмите 3) \n\t4 - Работа с форматом XML (нажмите 4) \n\t5 - Работа с zip архивом (нажмите 5) \n\t6 - Выход (нажмите 6)");
        switch (Console.ReadLine())
        {
          case "1": disc(); break;
          case "2": file(); break;
          case "3": Json(); break;
          case "4": xml(); break;
          case "5": zip(); break;
          case "6": i = false;  break;
          default: Console.WriteLine("Вы нажали неверную клавишу!"); break;
        }
      }
    }
  }
}
