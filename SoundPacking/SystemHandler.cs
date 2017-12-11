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
            createFolders(folderlist, "WorstFitDecreasingHEAP");
            audiolist = getAudioFiles();
            folderlist = Methods.worstFitLS(audiolist, maxcap);
            createFolders(folderlist, "WorstFitLS");
            audiolist = getAudioFiles();
            folderlist = Methods.worstFitDecreasingLS(audiolist, maxcap);
            createFolders(folderlist, "WorstFitDecreasingLS");
            audiolist = getAudioFiles();
            folderlist = Methods.worstFitHEAP(audiolist, maxcap);
            createFolders(folderlist, "WorstFitHeap");

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
                string foldername = "F" + i;
                string newfolderpath = Path.Combine(outputpath, foldername);
                System.IO.Directory.CreateDirectory(newfolderpath);

                for (int j = 0; j < myFolders[i].files.Count; j++)
                {
                    //copy files here
                    string sourceFileName = SystemHandler.folderPath + "\\Audios\\"
                        + myFolders[i].files[j].FileName;
                    string destinationFileName = newfolderpath + "\\" +
                        myFolders[i].files[j].FileName;
                    File.Copy(sourceFileName, destinationFileName);

                }

            }

        }

    }
}
