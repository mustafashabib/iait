using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using iTunesLib;
using AlbumArtTagger.Amazon;
using AlbumArtTagger;


namespace AlbumArtTagger
{
    public partial class Form1 : Form
    {
        private int _iNumTracksTagged = 0;
        private int _iTotalTracks = 0;
        private int _iCurrTrack = 0;
        private bool _bCheckFirst = true;
        private string _currTrackInfo = "";
        public static bool ConfirmingArt = false;
        public static bool UseArt = true;
        public static System.Drawing.Bitmap CurrImage = null;
        private System.Collections.Hashtable _htPlaylists = new System.Collections.Hashtable();
        private IITPlaylist _selectedPlaylist = null;
        

        public Form1()
        {
            InitializeComponent();
        }

        private enum ProgMode
        {
            Start,
            Working
        }

        private void BindPlaylists()
        {
            iTunesLib.iTunesAppClass myItunes = new iTunesAppClass();
            _htPlaylists.Clear();
            if (selPlaylist.Items != null)
            {
                selPlaylist.Items.Clear();
            }
           // _htPlaylists.Add("Entire Library", myItunes.LibraryPlaylist);

            foreach (IITPlaylist currPlaylist in myItunes.LibrarySource.Playlists)
            {
                if(!currPlaylist.Name.Equals("Music Videos") &&
                    !currPlaylist.Name.Equals("Movies") &&
                    !currPlaylist.Name.Equals("Podcasts") &&
                    !currPlaylist.Name.Equals("TV Shows"))
                {
                _htPlaylists.Add(currPlaylist.Name, currPlaylist);
                }
            }
            selPlaylist.DisplayMember = "key";
            selPlaylist.ValueMember = "value";
            foreach (System.Collections.DictionaryEntry currEntry in _htPlaylists)
            {
                selPlaylist.Items.Add(currEntry);
                
            }

            foreach (object currItem in selPlaylist.Items)
            {
                System.Collections.DictionaryEntry currEntry = (System.Collections.DictionaryEntry)currItem;
                if (currEntry.Key.ToString() == "Library")
                {
                    selPlaylist.SelectedItem = currItem;
                    selPlaylist.SelectedText = "Library";
                    selPlaylist.SelectedValue = (IITPlaylist)_htPlaylists["Library"];
                }
                selPlaylist.Text = "Library";
            }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                if (btnCommand.Text == "Start")
                {
                    // read digits from numeric updown control
                    // ask worker to do its job
                    SetFormMode(ProgMode.Working);
                    bg1.RunWorkerAsync();

                }
                else if (btnCommand.Text == "Cancel")
                {
                    // ask worker to cancel
                    if (MessageBox.Show("Are you sure?", "Confirm Cancellation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        bg1.CancelAsync();
                        SetFormMode(ProgMode.Start);
                    }

                }
                else
                {
                    // Only two enabled states for 
                    // this button: start and Cancel
                    System.Diagnostics.Debug.Assert(false);
                }
               

            }
            catch (Exception ex)
            {
                Log.LogLine(String.Format("EXCEPTION ENCOUNTERED: {0}", ex.Message));
                System.Diagnostics.Debug.WriteLine(String.Format("EXCEPTION ENCOUNTERED: {0}", ex.Message));
            }
        }

        private void SetFormMode(ProgMode m)
        {
            _iNumTracksTagged = 0;
            _iTotalTracks = 0;
            _iCurrTrack = 0;
            _currTrackInfo = "";

            pgbProgress.Value = 0;
            lblPercentage.Text = "";
            lblSongInfo.Text = "";
            btnCommand.Enabled = true;

            if(m == ProgMode.Start)
            {
                panel1.Visible = false;
                btnCommand.Text = "Start";
                chkConfirmImage.Enabled = true;
                selPlaylist.Enabled = true;
            }
            else
            {
                panel1.Visible = true;
                btnCommand.Text = "Cancel";
                chkConfirmImage.Enabled = false;
                selPlaylist.Enabled = false;
            }
        }

        private void FindAndTagArt()
        {
            System.Collections.Hashtable htArtworkAlreadyFound = new System.Collections.Hashtable();
            System.Collections.ArrayList alArtworkWeCannotFind = new System.Collections.ArrayList();
            iTunesLib.iTunesAppClass myItunes = new iTunesAppClass();
            iTunesLib.IITPlaylist currPlaylist = _selectedPlaylist;

            if (currPlaylist == null)
            {
                currPlaylist = myItunes.LibraryPlaylist;
            }


            _iTotalTracks = currPlaylist.Tracks.Count;

            _iNumTracksTagged = 0;
            _iCurrTrack = 0;

            foreach (iTunesLib.IITTrack currTrack in currPlaylist.Tracks)
            {

                if (bg1.CancellationPending)
                {
                    _iCurrTrack = 0;
                    _iTotalTracks = 0;
                    _iCurrTrack = 0;
                    break;
                }


                _iCurrTrack++;
                _currTrackInfo = currTrack.Artist + "\n" + currTrack.Album + "\n" + currTrack.Name;
                bg1.ReportProgress(_iCurrTrack);
                if (currTrack.Artwork.Count == 0)
                {
                    string currAlbum = currTrack.Album == null ? "" : currTrack.Album;
                    
                    bool bFoundArt = false;

                    //if we never tried locating art for this file
                    //and we dont yet have it, try to find it
                    if (currAlbum != "" && //we know the album name
                        !alArtworkWeCannotFind.Contains(currAlbum) &&
                        !htArtworkAlreadyFound.ContainsKey(currAlbum)) //we never tried looking before
                    {
                        string artFilename = "";
                        System.Collections.ArrayList artURLs = new System.Collections.ArrayList();
                        //find artwork
                        string currArtist = currTrack.Artist == null ? "" : currTrack.Artist;
                        bFoundArt = AmazonAlbumArtFinder.FindArtOnAmazon(currAlbum, currArtist, ref artURLs);

                        if (bFoundArt)
                        {
                            UseArt = true;
                            if (_bCheckFirst)
                            {
                                AlbumArtTagger.frmConfirm tagThisImageConfirmation = new AlbumArtTagger.frmConfirm(artURLs, currArtist, currAlbum, currTrack.Name);
                                //tagThisImageConfirmation.Visible = false;
                             //   this.Enabled = false;
                                tagThisImageConfirmation.ShowDialog();

                                while (ConfirmingArt)
                                {
                                    //wait
                                }
                              //  this.Enabled = true ;
                                
                                
                            }
                            else
                            {
                                //use first image
                                if (artURLs.Count > 0)
                                {
                                    CurrImage = new System.Drawing.Bitmap(new System.Drawing.Bitmap(new System.IO.MemoryStream(new System.Net.WebClient().DownloadData(artURLs[0].ToString()))));
                                }
                
                            }

                            if (UseArt)
                            {
                                //save this image
                                //                                artFilename = "c:\\itunes_art\\" + FilenameFromString(currAlbum.Trim()) + "_" + FilenameFromString(currArtist.Trim()) + ".png";

                                string strDirectoryToSaveInto = Environment.CurrentDirectory;
                                if (currTrack.Kind == ITTrackKind.ITTrackKindFile)
                                {
                                    iTunesLib.IITFileOrCDTrack file = (iTunesLib.IITFileOrCDTrack)currTrack;
                                    if (file.Location != null)
                                    {
                                        System.IO.FileInfo fi = new System.IO.FileInfo(file.Location);
                                       if (fi.Exists)
                                        {
                                            strDirectoryToSaveInto = fi.DirectoryName;
                                            //strDirectoryToSaveInto = file.Location.Substring(0, ;
                                        }
                                    }
                                }
                                artFilename = strDirectoryToSaveInto + "\\" + FilenameFromString(currAlbum.Trim()) + "_" + FilenameFromString(currArtist.Trim()) + ".png";
                                CurrImage.Save(artFilename, System.Drawing.Imaging.ImageFormat.Png);
                                Log.LogLine(String.Format("Saving a new album cover image for {0} at {1}", currArtist + " " + currAlbum, artFilename));
                                //add artwork to ht
                                htArtworkAlreadyFound.Add(currAlbum, artFilename);
                            }
                            else
                            {
                                //user doesn't wanna use image for this album
                                Log.LogLine(String.Format("Found art for {0} but user didn't like it.", currArtist + " " + currAlbum));                                
                                alArtworkWeCannotFind.Add(currAlbum);
                            }
                        }
                        else
                        {
                            Log.LogLine(String.Format("Couldn't locate art for {0}", currArtist + " " + currAlbum));                                
                            alArtworkWeCannotFind.Add(currAlbum);
                        }


                    }


                    //if we already have artwork for this album,
                    //it means the user likes it so be sure to use it
                    if(htArtworkAlreadyFound.ContainsKey(currAlbum))
                    {
                        bFoundArt = true;
                        UseArt = true;
                    }

                    
                    //now, if we found the art, we can tag it
                    if (bFoundArt && UseArt)
                    {
                        //tag artwork

                        
                        try
                        {
                            currTrack.AddArtworkFromFile(htArtworkAlreadyFound[currTrack.Album].ToString());
                            Log.LogLine(String.Format("{0} was tagged. This is track #{1} that has been tagged.", _currTrackInfo.Replace('\n', '\t'), _iNumTracksTagged));
                            _iNumTracksTagged++;
                        }
                        catch (Exception ex)
                        {
                            Log.LogLine(String.Format("EXCEPTION ENCOUNTERED: {0}", ex.Message));
                            System.Diagnostics.Debug.WriteLine(ex.Message);
                        }
                    }
                }
            }
        }
        

        private void Form1_Load(object sender, EventArgs e)
        {
            System.Diagnostics.Process[] currITunes = System.Diagnostics.Process.GetProcessesByName("itunes");
           /*if (currITunes.Length == 0)
            {
                if (MessageBox.Show("Please make sure ITunes is running and press Retry. Press Cancel to exit.", "Cannot find ITunes", MessageBoxButtons.RetryCancel, MessageBoxIcon.Exclamation) == DialogResult.Retry)
                {
                    Form1_Load(sender, e); //try again
                }
                else
                {
                    this.Dispose(); //exit
                }
            }*/
                BindPlaylists();
            
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                FindAndTagArt();
            }
            catch (Exception ex)
            {
                Log.LogLine(String.Format("EXCEPTION ENCOUNTERED: {0}", ex.Message));
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            double dPercentageComplete = 0;
            if (_iTotalTracks != 0)
            {
                dPercentageComplete = (double)_iCurrTrack / _iTotalTracks;
                dPercentageComplete = Math.Floor(dPercentageComplete*100);
                pgbProgress.Maximum = _iTotalTracks;
                pgbProgress.Value++;
                lblPercentage.Text = String.Format("{0}/{1} Tracks checked. {2}% Complete", _iCurrTrack, _iTotalTracks, dPercentageComplete);
                lblSongInfo.Text = _currTrackInfo;
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
           // MessageBox.Show(String.Format("Added art to {0} tracks in your library. See log.txt for details.", _iNumTracksTagged));
            MessageBox.Show(String.Format("Added art to {0} tracks in your library. Check the file Log.txt for more info.", _iNumTracksTagged));
            System.IO.File.WriteAllText(Log.Filename, Log.Info);
            
            SetFormMode(ProgMode.Start);
        }

        private void chkConfirmImage_CheckedChanged(object sender, EventArgs e)
        {
            _bCheckFirst = !_bCheckFirst;
        }

        public static string FilenameFromString(string name)
        {
            // first trim the raw string
            string safe = name.Trim();

            // replace spaces with hyphens
            safe = safe.Replace(" ", "-").ToLower();

            // replace any 'double spaces' with singles
            while (safe.IndexOf("--") >= 0)
            {
                safe = safe.Replace("--", "-");
            }

            // trim out illegal characters
            safe = System.Text.RegularExpressions.Regex.Replace(safe, "[^a-z0-9\\-]", "");

            // trim the length.  you should use a finite limit although the length
            // is entirely up to you.
            if (safe.Length > 50)
            {
                safe = safe.Substring(0, 49);
            }

            // clean the beginning and end of the filename
            char[] replace = { '-', '.' };
            safe = safe.TrimStart(replace);
            safe = safe.TrimEnd(replace);

            return safe;
        }

        private void selPlaylist_SelectedIndexChanged(object sender, EventArgs e)
        {
            _selectedPlaylist = ( (iTunesLib.IITPlaylist)((System.Collections.DictionaryEntry)selPlaylist.SelectedItem).Value);
        }
    }
}