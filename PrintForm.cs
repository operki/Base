using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//установка шрифта
using System.Drawing.Text;
using WindowsFormsApp1.Properties;
using Microsoft.Office.Interop.Word;

namespace WindowsFormsApp1
{
    public partial class PrintForm : Form
    {
        private bool auto = true;
        private string _userName;
        private SqlDataReader _sqlReader;
        private SqlCommand _sqlCommand;
        private SqlConnection _sqlConnection;
        private string _sqlConnectionString;
        private Color _borderColor = Color.FromArgb(80, 80, 80);
        private Color _backColor = Color.FromArgb(45, 45, 45);
        private Color _foreColor = Color.FromArgb(240, 240, 240);
        private Color _mainColor = Color.FromArgb(12, 93, 165);
        private Color _secondColor = Color.FromArgb(0, 129, 16);
        private Color _changerColor = Color.FromArgb(255, 149, 0);
        private Color _mainHoverColor = Color.FromArgb(12, 93, 165);
        private Color _secondHoverColor = Color.FromArgb(0, 154, 19);
        private List<string[]> _tasks = new List<string[]>();
        private int currentTask = 0;
        
        //установка шрифта
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont,
            IntPtr pdv, [System.Runtime.InteropServices.In] ref uint pcFonts);

        private readonly PrivateFontCollection _fonts = new PrivateFontCollection();

        Font _roboto;

        public PrintForm()
        {
            InitializeComponent();
            //установка шрифта
            var fontData = Resources.roboto;
            var fontPtr = System.Runtime.InteropServices.Marshal.AllocCoTaskMem(fontData.Length);
            System.Runtime.InteropServices.Marshal.Copy(fontData, 0, fontPtr, fontData.Length);
            uint dummy = 0;
            _fonts.AddMemoryFont(fontPtr, Resources.roboto.Length);
            AddFontMemResourceEx(fontPtr, (uint)Resources.roboto.Length, IntPtr.Zero, ref dummy);
            System.Runtime.InteropServices.Marshal.FreeCoTaskMem(fontPtr);
            _roboto = new Font(_fonts.Families[0], 11.0F);
            foreach (Control control in Controls)
                FontSet(control, _roboto);
        }

        private static void FontSet(Control control, Font font)
        {
            foreach (Control c in control.Controls)
                FontSet(c, font);
            control.Font = font;
        }

        public void LoadFromSQL(string sqlConnectionString, string userName)
        {
            _userName = userName;
            _sqlConnectionString = sqlConnectionString;
            _sqlConnection = new SqlConnection(_sqlConnectionString);
            _sqlConnection.Open();
        }

        private void PrintForm_Load(object sender, EventArgs e)
        {
            Capture = false;
            var m = Message.Create(Handle, 0xa1, new IntPtr(2), IntPtr.Zero);
            WndProc(ref m);
            _tasks.Clear();
            _sqlCommand = new SqlCommand("SELECT * FROM [Tasks] WHERE [isWork]=@isWork ", _sqlConnection);
            _sqlCommand.Parameters.AddWithValue("isWork", -1);
            _sqlReader = _sqlCommand.ExecuteReader();
            while (_sqlReader.Read())
            {
                _tasks.Add(new[]
                {
                    Convert.ToDateTime(_sqlReader["action"]).ToString("dd.MM.yyyy – HH:mm:ss"),
                    _sqlReader["peopleId"].ToString(),
                    "",
                    _sqlReader["name"].ToString(),
                    _sqlReader["destination"].ToString()
                });
            }
            _sqlReader.Close();
            foreach (var t in _tasks)
            {
                _sqlCommand = new SqlCommand("SELECT [fio0], [fio1], [fio2] FROM [Peoples] " +
                                             "WHERE [id]=@peopleId ", _sqlConnection);
                _sqlCommand.Parameters.AddWithValue("peopleId", t[1]);
                _sqlReader = _sqlCommand.ExecuteReader();
                _sqlReader.Read();
                t[2] = _sqlReader["fio0"] + " " + _sqlReader["fio1"].ToString()[0] + "." 
                       + _sqlReader["fio2"].ToString()[0] + ".";
                _sqlReader.Close();
            }

            taskSet(0);
        }

        private void taskSet(int index)
        {
            if (index >= _tasks.Count) return;
            currentTask = index;
            tbDate.Text = _tasks[currentTask][0];
            tbName.Text = _tasks[currentTask][2];
            tbType.Text = _tasks[currentTask][3];
            tbDestination.Text = _tasks[currentTask][4];
        }

        private void bExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void PrintForm_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, ClientRectangle, _borderColor, ButtonBorderStyle.Solid);
        }

        private void tRefresh_Tick(object sender, EventArgs e)
        {

        }

        private void bPrev_Click(object sender, EventArgs e)
        {
            taskSet(currentTask - 1);
        }

        private void bNext_Click(object sender, EventArgs e)
        {
            taskSet(currentTask + 1);
        }

        private void doTask(bool print)
        {
            int nokPositionId, nshPositionId;
            int nokId, nshId;
            //врио может быть
            //загрузка людей

            _sqlCommand = new SqlCommand("SELECT [id] FROM [Positions] WHERE [fullName]=@fullName",
                _sqlConnection);
            _sqlCommand.Parameters.AddWithValue("fullName", "Начальник штаба – заместитель командира бригады");
            _sqlReader = _sqlCommand.ExecuteReader();
            _sqlReader.Read();
            nshPositionId = Convert.ToInt32(_sqlReader["id"]);

            _sqlReader.Close();
            _sqlCommand = new SqlCommand("SELECT [id] FROM [Peoples] WHERE [positionId]=@positionId",
                _sqlConnection);
            _sqlCommand.Parameters.AddWithValue("positionId", nshPositionId);
            _sqlReader = _sqlCommand.ExecuteReader();
            nshId = 0;
            if (_sqlReader.HasRows)
            {
                _sqlReader.Read();
                nshId = Convert.ToInt32(_sqlReader["id"]);
            }

            _sqlReader.Close();

            _sqlReader.Close();
            _sqlCommand = new SqlCommand("SELECT [id] FROM [Positions] WHERE [fullName]=@fullName",
                _sqlConnection);
            _sqlCommand.Parameters.AddWithValue("fullName", "Начальник отделения кадров");
            _sqlReader = _sqlCommand.ExecuteReader();
            _sqlReader.Read();
            nokPositionId = Convert.ToInt32(_sqlReader["id"]);
            nokId = 0;
            _sqlReader.Close();

            _sqlReader.Close();
            _sqlCommand = new SqlCommand("SELECT [id] FROM [Peoples] WHERE [positionId]=@positionId",
                _sqlConnection);
            _sqlCommand.Parameters.AddWithValue("positionId", nokPositionId);
            _sqlReader = _sqlCommand.ExecuteReader();
            if (_sqlReader.HasRows)
            {
                _sqlReader.Read();
                nokId = Convert.ToInt32(_sqlReader["id"]);
            }

            _sqlReader.Close();

            _sqlCommand = new SqlCommand("SELECT TOP 1 [nshId], [nokId] " +
                                         "FROM [Settings] ORDER BY [action] DESC", _sqlConnection);
            _sqlReader = _sqlCommand.ExecuteReader();
            bool[] nok = {false, false};
            if (_sqlReader.HasRows)
            {
                _sqlReader.Read();
                if (nshPositionId != Convert.ToInt32(_sqlReader["nshId"]))
                {
                    nok[0] = true;
                    nshPositionId = Convert.ToInt32(_sqlReader["nshId"]);
                }
                if (nokPositionId != Convert.ToInt32(_sqlReader["nokId"]))
                {
                    nok[1] = true;
                    nokPositionId = Convert.ToInt32(_sqlReader["nokId"]);
                }
            }

            _sqlReader.Close();
            var linkToFile = "";
            //выслугу лет подсчитать
            switch (tbType.Text)
            {
                case "Справка о прохождении службы":
                    linkToFile = @"C:\temp\Прохождение службы.docx";
                    new GeneratedClassCurrent().CreatePackage(linkToFile,
                        _sqlConnectionString, Convert.ToInt32(_tasks[currentTask][1]), nok,
                        nshId, nokId, tbDestination.Text);
                    break;
                case "Справка о составе семьи":
                    linkToFile = @"C:\temp\Состав семьи.docx";
                    new GeneratedClassAge().CreatePackage(linkToFile,
                        _sqlConnectionString, Convert.ToInt32(_tasks[currentTask][1]), nok,
                        nshId, nokId, tbDestination.Text);
                    break;
                case "Послужной список":
                    linkToFile = @"C:\temp\Послужной список.docx";
                    new GeneratedClassHistory().CreatePackage(linkToFile,
                        _sqlConnectionString, Convert.ToInt32(_tasks[currentTask][1]), nok,
                        nokId);
                    break;
                        /*case "Справка о выслуге лет":
                    linkToFile = @"C:\temp\Выслуга лет.docx";
                    new GeneratedClassMemory().CreatePackage(linkToFile,
                        _sqlConnectionString, Convert.ToInt32(_tasks[currentTask][1]), nok,
                        nshId, nokId, tbDestination.Text,
                        Convert.ToDateTime(tbDate.Text), tbMemoryCalend.Text, tbMemoryAll.Text);
                    break;*/
                case "Справка-объективка":
                    linkToFile = @"C:\temp\Справка-объективка.docx";
                    new GeneratedClassAll().CreatePackage(linkToFile,
                        _sqlConnectionString, Convert.ToInt32(_tasks[currentTask][1]));
                    break;
            }

            if (print)
            {
                //печать напрямую
                new Process {StartInfo = {Verb = "Print", FileName = linkToFile}}.Start();
            }
            else
                Process.Start(linkToFile);
        }

        private void bPrint_Click(object sender, EventArgs e)
        {
            doTask(false);
        }
    }
}
