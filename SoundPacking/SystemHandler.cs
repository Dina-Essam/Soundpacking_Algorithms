using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SoundPacking
{
    class SystemHandler
    {
        private static string folderPath;
        private static int maxcap;

        public static void setMaxCap(int max)
        {
            maxcap = max;
        }
        public static void setFolderPath(string name)
        {
            folderPath = name;
        }

        //where we'll call all the methods
        public static void start(ref ListView timee)
        {
            
            List<AudioFile> audiolist = getAudioFiles();

            var watch = System.Diagnostics.Stopwatch.StartNew();
            List<Folder> folderlist = Methods.worstFitDecreasingHEAP(audiolist, maxcap);
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            timee.Items.Add( "worstFitDecreasingHEAP Time = "+elapsedMs + " ms");
            //createFolders(folderlist, "[2]WorstFitDecreasingHEAP");
            createMetaData(folderlist, "[2]WorstFitDecreasingHEAP");

            audiolist = getAudioFiles();
            watch = System.Diagnostics.Stopwatch.StartNew();
            folderlist = Methods.worstFitLS(audiolist, maxcap);
            watch.Stop();
            elapsedMs = watch.ElapsedMilliseconds;
            timee.Items.Add("worstFitLS Time = " + elapsedMs + " ms");
            //createFolders(folderlist, "[1]WorstFitLS");
            createMetaData(folderlist, "[1]WorstFitLS");

            audiolist = getAudioFiles();
            watch = System.Diagnostics.Stopwatch.StartNew();
            folderlist = Methods.worstFitDecreasingLS(audiolist, maxcap);
            watch.Stop();
            elapsedMs = watch.ElapsedMilliseconds;
            timee.Items.Add("worstFitDecreasingLS Time = " + elapsedMs + " ms");
            //createFolders(folderlist, "[2]WorstFitDecreasingLS");
            createMetaData(folderlist, "[2]WorstFitDecreasingLS");

            audiolist = getAudioFiles();
            watch = System.Diagnostics.Stopwatch.StartNew();
            folderlist = Methods.worstFitHEAP(audiolist, maxcap);
            watch.Stop();
            elapsedMs = watch.ElapsedMilliseconds;
            timee.Items.Add("worstFitHEAP Time = " + elapsedMs + " ms");
            //createFolders(folderlist, "[1]WorstFitHeap");
            createMetaData(folderlist, "[1]WorstFitHeap");

            watch = System.Diagnostics.Stopwatch.StartNew();
            folderlist = Methods.firstFitDecreasingLS(audiolist, maxcap);
            watch.Stop();
            elapsedMs = watch.ElapsedMilliseconds;
            timee.Items.Add("firstFitDecreasingLS Time = " + elapsedMs + " ms");
            //createFolders(folderlist, "[3]FirstFitDecreasing");
            createMetaData(folderlist, "[3]FirstFitDecreasing");

            watch = System.Diagnostics.Stopwatch.StartNew();
            folderlist = Methods.bestFitLS(audiolist, maxcap);
            watch.Stop();
            elapsedMs = watch.ElapsedMilliseconds;
            timee.Items.Add("bestFitLS Time = " + elapsedMs + " ms");
            // createFolders(folderlist, "[4]BestFitLS");
            createMetaData(folderlist, "[4]BestFitLS");

            audiolist = getAudioFiles();
            watch = System.Diagnostics.Stopwatch.StartNew();
            folderlist = Methods.firstFitLS(audiolist, maxcap);
            watch.Stop();
            elapsedMs = watch.ElapsedMilliseconds;
            timee.Items.Add("firstFitLS Time = " + elapsedMs + " ms");
            // createFolders(folderlist, "[6]FirstFit");
            createMetaData(folderlist, "[6]FirstFit");

            audiolist = getAudioFiles();
            watch = System.Diagnostics.Stopwatch.StartNew();
            folderlist = Methods.bestFitDecreasingLS(audiolist, maxcap);
            watch.Stop();
            elapsedMs = watch.ElapsedMilliseconds;
            timee.Items.Add("bestFitDecreasingLS Time = " + elapsedMs + " ms");
            // createFolders(folderlist, "[5]BestFitDecreasingLS");
            createMetaData(folderlist, "[5]BestFitDecreasingLS");

            audiolist = getAudioFiles();
            watch = System.Diagnostics.Stopwatch.StartNew();
            folderlist = Methods.NextFitDecreasingLS(audiolist, maxcap);
            watch.Stop();
            elapsedMs = watch.ElapsedMilliseconds;
            timee.Items.Add("NextFitDecreasingLS Time = " + elapsedMs + " ms");
            // createFolders(folderlist, "[8]NextFitDecreasing");
            createMetaData(folderlist, "[8]NextFitDecreasing");

            audiolist = getAudioFiles();
            watch = System.Diagnostics.Stopwatch.StartNew();
            folderlist = Methods.NextFitLS(audiolist, maxcap);
            watch.Stop();
            elapsedMs = watch.ElapsedMilliseconds;
            timee.Items.Add("NextFitLS Time = " + elapsedMs+" ms");
            // createFolders(folderlist, "[7]NextFitLS");
            createMetaData(folderlist, "[7]NextFitLS");

            audiolist = getAudioFiles();
            watch = System.Diagnostics.Stopwatch.StartNew();
            folderlist = Methods.folderFilling2(audiolist, maxcap);
            watch.Stop();
            elapsedMs = watch.ElapsedMilliseconds;
            timee.Items.Add("FolderFilling Time = " + elapsedMs+" ms");
            // createFolders(folderlist, "[9]FolderFilling");
            createMetaData(folderlist, "[9]FolderFilling");
        }

        public static List<AudioFile> getAudioFiles()
        {
            string[] Lines = System.IO.File.ReadAllLines(folderPath + @"\AudiosInfo.txt");
            int NumberOfFiles = Int32.Parse(Lines[0]);
            List<AudioFile> inputlist = new List<AudioFile>();

            for (int i = 1; i <= NumberOfFiles; i++)
            {
                string[] FileInfo = Lines[i].Split(' ');

                AudioFile audiofile = new AudioFile();
                audiofile.SetFileName(FileInfo[0]);
                audiofile.SetDuration(TimeSpan.Parse(FileInfo[1]));
                inputlist.Add(audiofile);

            }
            return inputlist;
        }

        public static void createFolders(List<Folder> myFolders, string filealgo)
        {
            List<string> path = SystemHandler.folderPath.Split('\\').ToList<string>();
            path.RemoveAt(path.Count - 1);
            path.Add("OUTPUT");
            path.Add(filealgo);

            string outputpath = string.Join("\\", path);
            DirectoryInfo di = Directory.CreateDirectory(outputpath);

            for (int i = 0; i < myFolders.Count; i++)
            {
                string foldername = "F" + (int)(i+1);
                string newfolderpath = Path.Combine(outputpath, foldername);
                System.IO.Directory.CreateDirectory(newfolderpath);

                for (int j = 0; j < myFolders[i].files.Count; j++)
                {
                    //copy files here
                    string sourceFileName = SystemHandler.folderPath + "\\Audios\\"
                        + myFolders[i].files[j].FileName;
                    string destinationFileName = newfolderpath + "\\" +
                        myFolders[i].files[j].FileName;
                    File.Copy(sourceFileName, destinationFileName,true);

                }
               
            }
        }
        public static void createMetaData(List<Folder> myFolders, string filealgo)
        {
            List<string> path = SystemHandler.folderPath.Split('\\').ToList<string>();
            path.RemoveAt(path.Count - 1);
            path.Add("OUTPUT");
            path.Add(filealgo);
            string outputpath = string.Join("\\", path);
            DirectoryInfo di = Directory.CreateDirectory(outputpath);

            for (int i = 0; i < myFolders.Count; i++)
            {
                string foldername = "F" + (int)(i + 1);
                string newfolderpath = Path.Combine(outputpath, foldername);
                string newtextfile = Path.Combine(outputpath, foldername + "_METADATA.txt");
                FileStream fs = new FileStream(newtextfile, FileMode.Append);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine(myFolders[i].files.Count.ToString());
                TimeSpan Duration = new TimeSpan();
                for (int j = 0; j < myFolders[i].files.Count; j++)
                {
                    Duration = Duration + myFolders[i].files[j].Duration;
                    sw.WriteLine(myFolders[i].files[j].FileName.ToString() + " " + myFolders[i].files[j].Duration.ToString());
                }
                sw.WriteLine(Duration.ToString());
                sw.Close();

            }


        }



    }
}
