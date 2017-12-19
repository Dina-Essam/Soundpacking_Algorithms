using System;
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
        public static void start()
        {
            List<AudioFile> audiolist = getAudioFiles();
            
            List<Folder> folderlist = Methods.worstFitDecreasingHEAP(audiolist, maxcap);
            createFolders(folderlist, "[2]WorstFitDecreasingHEAP");
            audiolist = getAudioFiles();
            folderlist = Methods.worstFitLS(audiolist, maxcap);
            createFolders(folderlist, "[1]WorstFitLS");
            audiolist = getAudioFiles();
            folderlist = Methods.worstFitDecreasingLS(audiolist, maxcap);
            createFolders(folderlist, "[2]WorstFitDecreasingLS");
            audiolist = getAudioFiles();
            folderlist = Methods.worstFitHEAP(audiolist, maxcap);
            createFolders(folderlist, "[1]WorstFitHeap");
            folderlist = Methods.firstFitDecreasingLS(audiolist, maxcap);
            createFolders(folderlist, "[3]FirstFitDecreasing");
            folderlist = Methods.bestFitLS(audiolist, maxcap);
            createFolders(folderlist, "[4]BestFitLS");
            audiolist = getAudioFiles();
            folderlist = Methods.firstFitLS(audiolist, maxcap);
            createFolders(folderlist, "[6]FirstFit");
            audiolist = getAudioFiles();
            folderlist = Methods.bestFitDecreasingLS(audiolist, maxcap);
            createFolders(folderlist, "[5]BestFitDecreasingLS");
            audiolist = getAudioFiles();
            folderlist = Methods.NextFitDecreasingLS(audiolist, maxcap);
            createFolders(folderlist, "[8]NextFitDecreasing");
            audiolist = getAudioFiles();
            folderlist = Methods.NextFitLS(audiolist, maxcap);
            createFolders(folderlist, "[7]NextFitLS");
            audiolist = getAudioFiles();
            folderlist = Methods.folderFilling(audiolist, maxcap);
            createFolders(folderlist, "[9]FolderFilling");


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
                string newtextfile= Path.Combine(outputpath, foldername +"_METADATA.txt");
                System.IO.Directory.CreateDirectory(newfolderpath);

                for (int j = 0; j < myFolders[i].files.Count; j++)
                {
                    //copy files here
                    string sourceFileName = SystemHandler.folderPath + "\\Audios\\"
                        + myFolders[i].files[j].FileName;
                    string destinationFileName = newfolderpath + "\\" +
                        myFolders[i].files[j].FileName;
                   // File.Copy(sourceFileName, destinationFileName,true);

                }
                FileStream fs = new FileStream(newtextfile, FileMode.Append);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine(myFolders[i].files.Count.ToString());
                TimeSpan Duration=new TimeSpan();
                for (int j = 0; j < myFolders[i].files.Count; j++)
                {
                    Duration = Duration + myFolders[i].files[j].Duration;
                    sw.WriteLine(myFolders[i].files[j].FileName.ToString()+" "+ myFolders[i].files[j].Duration.ToString());
                }
                sw.WriteLine(Duration.ToString());
                sw.Close();
            }


        }

    }
}
