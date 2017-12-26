using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Net.Cache;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace NPSHawaii
{
    public partial class MainWindow
    {
        //Game list connected to UI ListView
        ObservableCollection<GameItem> GameLibrary = new ObservableCollection<GameItem>();
        DBParser DatabaseParser;

        ChihiroAPI Chihiro = new ChihiroAPI();
        BackgroundWorker ChihiroThread = new BackgroundWorker();

        GameItem CurrentGameItem;

        public MainWindow()
        {
            InitializeComponent();

            DisableSearchbar();

            GameLibrary.CollectionChanged += GameLibrary_CollectionChanged;

            DatabaseParser = new DBParser();
            DatabaseParser.ParsedDB.CollectionChanged += DBParser_NewParsed;
            DatabaseParser.AsyncParser.RunWorkerCompleted += Database_AllParsed;

            ChihiroThread.DoWork += Chihiro_Fetch;
            ChihiroThread.WorkerSupportsCancellation = true;
        }

        //Observe GameLibrary list and add any new games to UI
        private void GameLibrary_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            AddUIGames(e.NewItems);
        }

        //Observe Database parser for new parsed game entries
        private void DBParser_NewParsed(object sender, NotifyCollectionChangedEventArgs e)
        {
            AddUIGames(e.NewItems);
        }

        //Add GameItem to TitleList UI
        private void AddUIGames(System.Collections.IList Games)
        {
            foreach(GameItem game in Games)
            {
                Dispatcher.Invoke(new Action(() => TitleList.Items.Add(game)));
            }
        }

        private void ClearGames()
        {
            Dispatcher.Invoke(new Action(() => TitleList.Items.Clear() ));
        }

        //Load game content from PS Store when clicking an item
        private void TitleListItem_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ListViewItem obj = (ListViewItem)sender;
            CurrentGameItem = (GameItem)obj.Content;

            string contentid = CurrentGameItem.ContentID;
            string region = CurrentGameItem.Region;

            PopulateChihiroAsync(contentid, region);
        }

        //Set PS Store side panel information with game item data
        private void SetPanelPSN(PSNItem game)
        {
            PanelTitle.Text = game.TitleName;
            PanelSize.Text = $"Install Size: {game.Size}GB";

            //Parse and insert Release Date
            if (game.ReleaseDate != null)
            {
                string month = game.ReleaseDate.ToString("MMM", CultureInfo.InvariantCulture);
                string date = $"{month} {game.ReleaseDate.Date.Day}, {game.ReleaseDate.Year}";
                PanelRelease.Text = $"Released: {date}";
            }

            //Game Description
            if(game.LongDesc != null)
            {
                String _description = game.LongDesc.Replace("<br>", Environment.NewLine);
                _description = Environment.NewLine + _description;
                PanelDescription.Text = $"{_description}";
            }

            if (game.Images != null)
            {
                PanelIconImage.Source = PanelImageSource(game.Images[0].Url);
            }

            if (game.PlayablePlatform != null)
            {
                PanelPlatform.Text = "Platform: ";
                foreach (string platform in game.PlayablePlatform)
                {
                    PanelPlatform.Text += $"{platform}, ";
                }
            }

            if(game.Metadata.Genre != null)
            {
                PanelGenre.Text = "Genre: ";
                foreach (string genre in game.Metadata.Genre.Values)
                {
                    PanelGenre.Text += $"{genre}, ";
                }
            }
            else
            {
                PanelGenre.Text = String.Empty;
            }
        }

        //Starts Chihiro API scraper and sets global values for execution
        private void PopulateChihiroAsync(string ContentID, string Region)
        {
            ChihiroThread.CancelAsync();
            if(!ChihiroThread.IsBusy)
            {
                List<object> ChihiroRequest = new List<object>() { ContentID, Region };
                ChihiroThread.RunWorkerAsync(ChihiroRequest);
            }
            
        }

        //Fetches Chihiro API and updates PS Store side panel
        private void Chihiro_Fetch(object sender, DoWorkEventArgs e)
        {
            List<object> arguments = e.Argument as List<object>;

            PSNItem CurrentPSNGame = null;
            CurrentPSNGame = Chihiro.PSNItemAPI((string)arguments[0], (string)arguments[1]);

            //If Chihiro returns content, then populate side panel
            //If not, forge a dummy item
            if(CurrentPSNGame != null)
            {
                Dispatcher.BeginInvoke(new Action(() => SetPanelPSN(CurrentPSNGame)));
            }
        }

        //Download icon/image from url and return as bitmap
        private System.Windows.Media.Imaging.BitmapImage PanelImageSource(string Url)
        {
            System.Windows.Media.Imaging.BitmapImage artSource = new System.Windows.Media.Imaging.BitmapImage();

            //Opens the filestream
            artSource.BeginInit();

            //Fixes the caching issues, where cached copy would just hang around and bother me for two days
            artSource.CacheOption = System.Windows.Media.Imaging.BitmapCacheOption.OnLoad;
            artSource.UriCachePolicy = new RequestCachePolicy(RequestCacheLevel.CacheIfAvailable);
            artSource.CreateOptions = System.Windows.Media.Imaging.BitmapCreateOptions.IgnoreImageCache;

            artSource.CacheOption = System.Windows.Media.Imaging.BitmapCacheOption.OnLoad;
            artSource.UriSource = new Uri(Url, UriKind.RelativeOrAbsolute);

            //Closes the filestream
            artSource.EndInit();

            return artSource;
        }

        //Download PKG button
        private void DownloadButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if(TitleList.SelectedIndex >= 0)
            {
                //Generate RAP license and download PKG
                RAPGenerator.SaveRap(CurrentGameItem);
                Process.Start(CurrentGameItem.PKGLink);
            }
            else
                Console.WriteLine("No item selected to download.");
        }

        //Item searchbar
        private void Searchbar_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            //Clear all games in UI
            ClearGames();

            //Use parsed database to query new items
            foreach (GameItem item in DatabaseParser.ParsedDB)
            {
                if(SearchQuery(item.TitleName, item.TitleID, Searchbar.Text))
                {
                    Dispatcher.Invoke(new Action(() => GameLibrary.Insert(0, item)));
                }
            }
        }

        private bool SearchQuery(string ItemTitle, string TitleID, string Query)
        {
            bool pass = false;

            ItemTitle = ItemTitle.ToLower();
            TitleID = TitleID.ToLower();
            Query = Query.ToLower();

            double LowestSimilarity = 60;

            //Search for TitleID
            if(Query.Contains(TitleID))
            {
                pass = true;
            }

            //Exact match
            if (ItemTitle.Contains(Query))
            {
                pass = true;
            }

            //String search algorithm based on similarity %
            string[] TitleWords = ItemTitle.Split(' ');
            if(TitleWords.Length > 2)
            {
                double progress = 0;

                foreach (string word in TitleWords)
                {
                    if (Query.Contains(word))
                    {
                        progress++;
                    }
                }

                double similarity = (progress * 100) / TitleWords.Length;

                if (similarity > LowestSimilarity)
                {
                    pass = true;
                }
            }

            return pass;
        }

        private void Database_AllParsed(object sender, RunWorkerCompletedEventArgs e)
        {
            EnableSearchbar();
        }

        private void EnableSearchbar()
        {
            double startpos = - SearchbarPanel.ActualWidth;

            Console.WriteLine($"Enabling searchbar. Width = {startpos}");

            var start = new Thickness(startpos, 0, 0, 0);
            var end = new Thickness(0, 0, 0, 0);

            ThicknessAnimation anim = new ThicknessAnimation()
            {
                From = start,
                To = end,
                Duration = TimeSpan.FromMilliseconds(600)

            };

            Dispatcher.Invoke( new Action(() => 
                SearchbarPanel.BeginAnimation(MarginProperty, anim)
            ));
            
        }

        private void DisableSearchbar()
        {
            double startpos = - SearchbarPanel.ActualWidth;
            if(startpos == 0)
            {
                startpos = -1280;
            }

            Console.WriteLine($"Hiding searchbar. Width = {startpos}");

            Dispatcher.Invoke(new Action(() =>
               SearchbarPanel.Margin = new Thickness(startpos, 0, 0, 0)
            ));
        }

    }
}
