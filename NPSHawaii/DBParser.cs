using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace NPSHawaii
{
    public class DBParser
    {
        public BackgroundWorker AsyncParser = new BackgroundWorker();

        private string pkgDBs = $"{AppDomain.CurrentDomain.BaseDirectory}pkg_repos\\"; //database directory

        List<string> DBList = new List<string>(); //List of raw database files found
        public ObservableCollection<GameItem> ParsedDB = new ObservableCollection<GameItem>(); //database with parsed games

        public DBParser()
        {
            Console.WriteLine("Starting DBParser");
            InitializeDatabaseList();

            AsyncParser.DoWork += AsyncParse_DoWork;
            AsyncParser.RunWorkerAsync();
        }

        //Enumerate all database files
        private void InitializeDatabaseList()
        {
            foreach(string dir in Directory.EnumerateDirectories(pkgDBs))
            {
                foreach (var dbfile in Directory.EnumerateFiles(dir))
                {
                    Console.WriteLine(dbfile);
                    DBList.Add(dbfile);
                }
            }

            Console.WriteLine("All database files enumerated!");
        }

        private void AsyncParse_DoWork(object sender, DoWorkEventArgs e)
        {
            Console.WriteLine("Database parsing started!");

            Stopwatch timer = new Stopwatch();
            timer.Start();

            //go through each database
            foreach(string db in DBList)
            {
                //load current database in memory
                List<string> CurrentDB = File.ReadAllLines(db).ToList();

                //parse each line that contains a link and add to parsed database
                foreach (string line in CurrentDB)
                {
                    if(line.Contains("http"))
                    {
                        ParseNPSLine(line);
                        ParsePSNDLLine(line);
                    }
                    
                }
            }

            timer.Stop();
            Console.WriteLine($"Database built in {timer.ElapsedMilliseconds}ms. Total Items: {ParsedDB.Count}");
            
        }

        //Parse NPS database entry
        //nps splits data with '	' character
        private bool ParseNPSLine(string line)
        {
            char splitter = '	';

            if (line.Contains(splitter))
            {
                string[] data = line.Split(splitter);

                GameItem item = new GameItem(data[0], data[1], data[2], data[3])
                {
                    RAP = data[4],
                    ContentID = data[5],
                    LastModified = data[6],
                    FileSize = data[7],
                    DBSource = "NoPayStation"
                };

                ParsedDB.Insert(0, item);
                return true;
            }
            else
            {
                return false;
            }
        }

        //Parse PSNDLv3 database entry
        //psndl splits data with ';'
        private bool ParsePSNDLLine(string line)
        {
            char splitter = ';';

            if (line.Contains(splitter))
            {
                string[] data = line.Split(splitter);

                GameItem item = new GameItem(data[0], data[3], data[1], data[4])
                {
                    RAP = data[6],
                    ContentID = data[5].Replace(".rap", String.Empty), //remove .rap extension
                    DBSource = "PSNDLv3"
                };

                ParsedDB.Insert(0, item);

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
