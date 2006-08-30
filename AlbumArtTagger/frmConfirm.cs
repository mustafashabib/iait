using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AlbumArtTagger
{
    public partial class frmConfirm : Form
    {
        private System.Collections.ArrayList _aURLS;
        protected int _iCurrImage = 1;
        protected bool _bSelected = false;
        protected string _strArtist = "";
        protected string _strAlbum = "";

        /*public frmConfirm()
        {
            InitializeComponent();
        }*/

        public frmConfirm(System.Collections.ArrayList albumArts, string strArtist, string strAlbum, string strTitle)
        {
            //frmSearching = frmParent;
            Form1.ConfirmingArt = true;
            _aURLS = albumArts;
            _strArtist = strArtist;
            _strAlbum = strAlbum;
            
            InitializeComponent();
            lblInfo.Text = _strArtist + "\n" + _strAlbum + "\n" + strTitle;
            ShowImage();
            btnPrevImg.Enabled = false;
            if (_aURLS.Count == 1)
            {
                btnNextImg.Enabled = false;
            }
        }

        private void ShowImage()
        {
            try{
                //New System.Drawing.Bitmap(New IO.MemoryStream(New System.Net.WebClient().DownloadData(TextBox1.Text)))
                Form1.CurrImage = new System.Drawing.Bitmap(new System.Drawing.Bitmap(new System.IO.MemoryStream(new System.Net.WebClient().DownloadData(this._aURLS[_iCurrImage - 1].ToString()))));
                pictureBox1.Image = Form1.CurrImage;

            }
            catch(Exception e){
                System.Diagnostics.Debug.WriteLine(String.Format("EXCEPTION ENCOUNTERED: {0}", e.Message));
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _bSelected = true;
            Form1.UseArt = true;
            Form1.ConfirmingArt = false;
            
            this.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _bSelected = true;
            Form1.UseArt = false;
            Form1.ConfirmingArt = false;

            this.Dispose();
        }

        private void frmConfirm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!_bSelected)
            {
                Form1.UseArt = false; //if the user just closed this window, then don't assume 
                //they meant yes.
            }
            Form1.ConfirmingArt = false;

        }

        private void btnPrevImg_Click(object sender, EventArgs e)
        {
            _iCurrImage--;
            if (_iCurrImage != _aURLS.Count)
            {
                btnNextImg.Enabled = true;
            }
            if (_iCurrImage == 1)
            {
                btnPrevImg.Enabled = false;
            }
            ShowImage();
        }

        private void btnNextImg_Click(object sender, EventArgs e)
        {
            _iCurrImage++;
            btnPrevImg.Enabled = true;

            if (_iCurrImage == _aURLS.Count)
            {
                btnNextImg.Enabled = false;
            }
            ShowImage();
        }
    }
}