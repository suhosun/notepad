using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace notepad
{
    public partial class frmDotNetNote : Form
    {
        #region Private Members
        
            private bool _IsTextChanged;
        
        #endregion

        public frmDotNetNote()
        {
            InitializeComponent();
        }

        // 새로 만들기 (N)
        private void menuNew_Click(object sender, EventArgs e)
        {
            if (_IsTextChanged)
            {               
                SaveOrClearOrCreate("New");             
            }
            else
            {
                CrearText();
            }

          

            //this.Text = "제목 없음 - 메모장";
        }

        private void SaveText()
        {            
            if (this.Text == "제목 없음")
            {
                SaveFileDialog sfd = new SaveFileDialog();
                String filter = "Text Files|*.txt|All Files|*.*";
                sfd.Filter = filter;

                DialogResult objDr = sfd.ShowDialog();

                if (objDr != DialogResult.Cancel)
                {
                    string strFileName = sfd.FileName;
                    //String filter = "Text Files|*.txt|All Files|*.*";

                    //sfd.Filter = filter;
                    SaveFile(strFileName);
                }
                else
                {
                    string strFileName = this.Text;
                    SaveFile(strFileName);
                }


                //SaveFileDialog sfd = new SaveFileDialog();
                //String filter = "Text Files|*.txt|All Files|*.*";

                //sfd.Filter = filter;

                //if (sfd.ShowDialog() == DialogResult.OK)
                //{
                //    int search = sfd.FileName.LastIndexOf("\\");
                //    this.Text = sfd.ToString().Substring(search + 1);
                //    StreamWriter sw = new StreamWriter(sfd.FileName, false, Encoding.Default);

                //    sw.WriteLine(txtMain.Text);
                //    sw.Close();
                //}
            }
        }

        private void SaveFile(string strFileName)
        {
            StreamWriter objSw = new StreamWriter(strFileName);

            objSw.WriteLine(txtMain.Text);
            objSw.Flush();
            objSw.Close();

            _IsTextChanged = false;

            //DialogResult objDr = MessageBox.Show(
            //   this.Text + " 파일의 내용이 변경되었습니다." + Environment.NewLine + Environment.NewLine +
            //   "변경된 내용을 저장하시겠습니까?"
            //   , "메모장"
            //   , MessageBoxButtons.YesNoCancel
            //   , MessageBoxIcon.Exclamation);

            //switch (objDr)
            //{
            //    case DialogResult.Yes:
            //        //menuFileSave_Click(sender, e);
            //        SaveText();
            //        CrearText();
            //        break;
            //    case DialogResult.No:
            //        CrearText();
            //        this.Text = "제목 없음 - 메모장";
            //        break;
            //}
        }
        
        private void CrearText()
        {
            this.txtMain.ResetText(); //TextBox ReSet
            this.Text = "제목 없음 - 메모장";
            _IsTextChanged = false;
        }

        //파일
        private void menuOpen_Click(object sender, EventArgs e)
        {
            if (_IsTextChanged)
            {
                SaveOrClearOrCreate("Open");

            }
            else
            {
                OpenText();
            }

        }

        //저장 또는 텍스트박스 클리어, 또는 취소 기능 메서드
        private void SaveOrClearOrCreate(string strFlag)
        {
            DialogResult objDr = MessageBox.Show(
                   this.Text + " 파일의 내용이 변경되었습니다." + Environment.NewLine + Environment.NewLine +
                   "변경된 내용을 저장하시겠습니까?"
                   , "메모장"
                   , MessageBoxButtons.YesNoCancel
                   , MessageBoxIcon.Exclamation);


            switch (objDr)
            {
                case DialogResult.Yes:
                    //menuFileSave_Click(sender, e);
                    SaveText();

                    if (strFlag == "New")
                        CrearText();
                    else
                        OpenText();
                    break;
                case DialogResult.No:
                    if (strFlag == "New")
                        CrearText();
                    else
                        OpenText();

                    this.Text = "제목 없음 - 메모장";
                    break;
            }
        }

        //열기 기능 
        private void OpenText()
        {
            DialogResult objDr = openFileDialog1.ShowDialog();

            if (objDr != DialogResult.Cancel)
            {
                string strFileName = openFileDialog1.FileName;
                int search = openFileDialog1.FileName.LastIndexOf("\\");

                this.Text = strFileName.Substring(search + 1);

                txtMain.Text = null;
                StreamReader objSr = new StreamReader(strFileName, Encoding.Default);

                txtMain.Text = objSr.ReadToEnd();
                objSr.Close();

                _IsTextChanged = false;
                this.Text = strFileName; //제목에 파일명 출력
            }

            //if (objDr == DialogResult.OK)
            //{
            //    string openSrc = openFileDialog1.FileName;
            //    int search = openFileDialog1.FileName.LastIndexOf("\\");

            //    this.Text = openSrc.Substring(search + 1);

            //    txtMain.Text = null;
            //    StreamReader sr = new StreamReader(openSrc, Encoding.Default);

            //    txtMain.Text = sr.ReadToEnd();
            //    sr.Close();

            //}
        }

        private void menuFileSave_Click(object sender, EventArgs e)
        {
            SaveText(); //저장 메서드 호출
            return;

            SaveFileDialog sfd = new SaveFileDialog();
            String filter = "Text Files|*.txt|All Files|*.*";
            
            sfd.Filter = filter;

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                int search = sfd.FileName.LastIndexOf("\\");
                this.Text = sfd.ToString().Substring(search + 1);
                StreamWriter sw = new StreamWriter(sfd.FileName, false, Encoding.Default);

                sw.WriteLine(txtMain.Text);
                sw.Close();
            }

            #region ddd
            //SaveFileDialog sfd = new SaveFileDialog();
            //DialogResult dr = MessageBox.Show("저장 하시겠습니까?", "save", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            //if(dr.Equals(DialogResult.Yes))
            //{
            //    string filename = sfd.FileName;
            //    String filter = "Text Files|*.txt|All Files|*.*";
            //    sfd.Filter = filter;

            //    sfd.Title = "Save";

            //    if(sfd.ShowDialog(this) == DialogResult.OK)
            //    {
            //        System.IO.File.WriteAllText(filename, txtMain.Text);
            //    }
            //    else{
            //        return;
            //    }
            //}
            //else
            //{
            //    txtMain.Clear();
            //}
            #endregion

        }

        private void menuFileSaveAs_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                int search = saveFileDialog1.FileName.LastIndexOf("\\");
                this.Text = saveFileDialog1.ToString().Substring(search + 1);
                StreamWriter sw = new StreamWriter(saveFileDialog1.FileName, false, Encoding.Default);

                sw.WriteLine(txtMain.Text);
                sw.Close();
            }
        }

       
        //텍스트가 변경됨 감지
        private void txtMain_TextChanged(object sender, EventArgs e)
        {
            _IsTextChanged = true;
        }       
     
    }

      
}
