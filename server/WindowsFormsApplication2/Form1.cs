using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Timers;
using System.Threading;

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            DirectoryInfo di1 = new DirectoryInfo(@"D:\FTPServer\workspace\workspace");
            DirectoryInfo di2 = new DirectoryInfo(@"D:\FTPServer\workspace\workspace1");
            DirectoryInfo di3 = new DirectoryInfo(@"C:\Users\Server\Dropbox");
            CopyAll(di1,di2);
            CopyAll(di2, di3);
            izpisiFajle();
            timer1.Start();
        }

       
        public void izpisiFajle()
        {
            
                string path;
                path= "D:\\FTPServer\\workspace\\workspace";

                string[] fileEntries = Directory.GetFiles(path, "*", SearchOption.AllDirectories);
                String[] a = new String[fileEntries.Length * 2];
                int stevec = 0;
                foreach (string fileName in fileEntries)  //grem po fajlih
                {
                    if (fileName.ToString().Substring(fileName.Length - 5) != "w.exe")
                    {
                        a[stevec] = fileName.ToString().Substring(path.Length);
                        stevec++;
                        FileInfo fi = new FileInfo(fileName); // dobim čas spremembe
                        a[stevec] = fi.LastAccessTimeUtc.ToString();
                        //MessageBox.Show(fi.LastAccessTimeUtc.ToString());
                        stevec++;
                    }
                }
                try
                {
                    System.IO.File.WriteAllLines(@"D:\FTPServer\workspace\eclipselist", a);
                }
                catch (Exception af) { Console.Write(af.ToString()); }
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            izpisiFajle();
        }
         public static void CopyAll(DirectoryInfo source, DirectoryInfo target)
        {
        // Check if the target directory exists, if not, create it.
        if (Directory.Exists(target.FullName) == false)
        {
            Directory.CreateDirectory(target.FullName);
        }

        // Copy each file into it’s new directory.
        foreach (FileInfo fi in source.GetFiles())
        {
            fi.CopyTo(Path.Combine(target.ToString(), fi.Name), true);
        }

        // Copy each subdirectory using recursion.
        foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
        {
            DirectoryInfo nextTargetSubDir =
                target.CreateSubdirectory(diSourceSubDir.Name);
            CopyAll(diSourceSubDir, nextTargetSubDir);
        }
    }


    }
}
