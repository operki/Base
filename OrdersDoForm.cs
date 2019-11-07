using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//склонение фио
using CaseDecline.CS;
//установка шрифта
using System.Drawing.Text;
using WindowsFormsApp1.Properties;

namespace WindowsFormsApp1
{
    public partial class OrdersDoForm : Form
    {
        private SqlCommand _sqlCommand;
        private SqlConnection _sqlConnection;
        private string _sqlConnectionString;
        private SqlDataReader _sqlReader;
        private string _userName;
        private int _colorSchema = 0;
        private List<string[]> _fails = new List<string[]>();
        private Color[] _borderColor = { Color.FromArgb(80, 80, 80), Color.FromArgb(150, 150, 150)};
        private Color[] _backColor = { Color.FromArgb(45, 45, 45), Color.FromArgb(225, 225, 225) };
        private Color[] _foreColor = { Color.FromArgb(240, 240, 240), Color.FromArgb(0, 0, 0) };
        private Color[] _mainColor = { Color.FromArgb(12, 93, 165), Color.FromArgb(64, 141, 200) };
        private Color[] _secondColor = { Color.FromArgb(0, 129, 16), Color.FromArgb(37, 148,51 ) };
        private Color[] _changerColor = { Color.FromArgb(255, 149, 0), Color.FromArgb(166, 97, 0) };
        private Color[] _mainHoverColor = { Color.FromArgb(12, 93, 165), Color.FromArgb(12, 93, 165) }; //???
        private Color[] _secondHoverColor = { Color.FromArgb(0, 154, 19), Color.FromArgb(44, 177, 61) };
        
        //установка шрифта
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont,
            IntPtr pdv, [System.Runtime.InteropServices.In] ref uint pcFonts);
        private readonly PrivateFontCollection _fonts = new PrivateFontCollection();
        Font _roboto;

        public OrdersDoForm()
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
        }

        private void FontSet(Control control, Font font)
        {
            foreach (Control c in control.Controls)
                FontSet(c, font);
            control.Font = font;
        }

        private Color ColorSchemaChange(Color input)
        {
            if (input == _backColor[_colorSchema])
                return _backColor[_colorSchema == 0 ? 1 : 0];
            if (input == _borderColor[_colorSchema])
                return _borderColor[_colorSchema == 0 ? 1 : 0];
            if (input == _foreColor[_colorSchema])
                return _foreColor[_colorSchema == 0 ? 1 : 0];
            if (input == _mainColor[_colorSchema])
                return _mainColor[_colorSchema == 0 ? 1 : 0];
            if (input == _secondColor[_colorSchema])
                return _secondColor[_colorSchema == 0 ? 1 : 0];
            return input;
        }

        private void ColorSchemaSet(Control control)
        {
            int t;
            if (control.Name == "bExit")
                t = 0;
            control.BackColor = ColorSchemaChange(control.BackColor);
            control.ForeColor = ColorSchemaChange(control.ForeColor);
            foreach (Control c in control.Controls)
                ColorSchemaSet(c);
        }

        //защита от мерцания при Resize
        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }

        //изменение размера
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x84)
            {
                // Trap WM_NCHITTEST
                var pos = new Point(m.LParam.ToInt32());
                pos = PointToClient(pos);
                if (pos.Y < 32)
                {
                    m.Result = (IntPtr) 2; // HTCAPTION
                    return;
                }

                if (pos.X >= ClientSize.Width - 16 && pos.Y >= ClientSize.Height - 16)
                {
                    m.Result = (IntPtr) 17; // HTBOTTOMRIGHT
                    return;
                }
            }

            base.WndProc(ref m);
        }

        private void ElementFocusSet(Control control)
        {
            foreach (Control c in control.Controls)
                ElementFocusSet(c);

            if (control is TextBox)
            {
                //приводим к типу и устанавливаем обработчики событий
                ((TextBox) control).Enter += ElementFocus_Enter;
                ((TextBox) control).MouseEnter += ElementFocus_Enter;
                ((TextBox) control).Leave += ElementFocus_Leave;
                ((TextBox) control).MouseLeave += ElementFocus_Leave;
            }
            else if (control is ListBox)
            {
                ((ListBox) control).Enter += ElementFocus_Enter;
                ((ListBox) control).MouseEnter += ElementFocus_Enter;
                ((ListBox) control).Leave += ElementFocus_Leave;
                ((ListBox) control).MouseLeave += ElementFocus_Leave;
            }
            else if (control is DataGridView)
            {
                ((DataGridView) control).Enter += ElementFocus_Enter;
                ((DataGridView) control).MouseEnter += ElementFocus_Enter;
                ((DataGridView) control).Leave += ElementFocus_Leave;
                ((DataGridView) control).MouseLeave += ElementFocus_Leave;
            }
        }

        private void ElementFocus_Enter(object sender, EventArgs e)
        {
            if (sender is TextBox)
                ElementFocus(((TextBox) sender).Name, true);
            else if (sender is ListBox)
                ElementFocus(((ListBox) sender).Name, true);
            else if (sender is DataGridView)
                ElementFocus(((DataGridView) sender).Name, true);
        }

        private void ElementFocus_Leave(object sender, EventArgs e)
        {
            if (sender is TextBox)
                ElementFocus(((TextBox) sender).Name, false);
            else if (sender is ListBox)
                ElementFocus(((ListBox) sender).Name, false);
            else if (sender is DataGridView)
                ElementFocus(((DataGridView) sender).Name, false);
        }

        private void ElementFocus(string elementName, bool focusSet)
        {
            //изменение цвета связанных panel и button
            //textBoxName = ((TextBox) sender).Name;
            //if (sender is TextBox) var t = true;
            var elementChange = Controls.Find(elementName, true);
            if (elementChange.Length <= 0) return;
            if (!focusSet && elementChange[0].Focused) return;
            string[] elementNameChanger;
            if (elementChange[0] is TextBox)
                elementNameChanger = new[] {"tb", "tbp", "tbp1", "tbp2", "b1", "b"};
            else if (elementChange[0] is ListBox)
                elementNameChanger = new[] {"lb", "lbp", "lbp1", "lbp2", "b1", "b"};
            else if (elementChange[0] is DataGridView)
                elementNameChanger = new[] {"dgv", "dgvp"};
            else
                return;
            var changedColor = focusSet ? _secondColor[_colorSchema] : _mainColor[_colorSchema];
            elementChange[0].ForeColor = focusSet ? changedColor : _foreColor[_colorSchema];
            for (var i = 0; i < elementNameChanger.Length - 1; i++)
            {
                elementName = elementName.Replace(elementNameChanger[i], elementNameChanger[i + 1]);
                elementChange = Controls.Find(elementName, true);
                if (elementChange.Length > 0)
                    elementChange[0].BackColor = changedColor;
            }
        }

        private void OrdersDoForm_Load(object sender, EventArgs e)
        {
            foreach (Control control in Controls) ElementFocusSet(control);
        }

        private void pWindowState_MouseDown(object sender, MouseEventArgs e)
        {
            pWindowState.Capture = false;
            var m = Message.Create(Handle, 0xa1, new IntPtr(2), IntPtr.Zero);
            WndProc(ref m);
        }

        private void bExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void bMax_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Maximized)
            {
                WindowState = FormWindowState.Normal;
                bMax.Image = Resources.fullin30;
            }
            else
            {
                WindowState = FormWindowState.Maximized;
                bMax.Image = Resources.fulloff30;
            }
        }

        private void bMin_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void bFlash_Click(object sender, EventArgs e)
        {
            ColorSchemaSet(this);
            bFlash.Image = _colorSchema == 0 ? Resources.sun1 : Resources.sun;
            _colorSchema = _colorSchema == 0 ? 1 : 0;
        }
        

        public void LoadFromSQL(string sqlConnectionString, string formUserName, int colorSchema)
        {
            _userName = formUserName;
            _sqlConnectionString = sqlConnectionString;
            _sqlConnection = new SqlConnection(sqlConnectionString);
            _sqlConnection.Open();

            _colorSchema = colorSchema == 0 ? 1 : 0;
            bFlash.Image = _colorSchema == 0 ? Resources.sun1 : Resources.sun;
            ColorSchemaSet(this);
            _colorSchema = colorSchema;
        }

        private void bLoadFpFails_Click(object sender, EventArgs e)
        {
            if (bLoadFpFails.Text == "       Загрузить список с двойками по ФП")
            {
                pFails.Visible = true;
                bLoadFpFails.Text = "       Таблицу на премию";
                //загрузка взысканий
                _sqlCommand = new SqlCommand("SELECT FROM [Fails] " +
                                             "WHERE [secondOrderId]=@secondOrderId ORDER BY [peopleId]",
                    _sqlConnection);
                _sqlCommand.Parameters.AddWithValue("secondOrderId", 2442);
                _sqlReader = _sqlCommand.ExecuteReader();
                while (_sqlReader.Read())
                {
                    _fails.Add(new[]
                    {
                        "",
                        _sqlReader["peopleId"].ToString(), "", "",
                        _sqlReader["type"].ToString(),
                        _sqlReader["text"].ToString(),
                        _sqlReader["orderId"].ToString(), "", "",
                        ""
                    });
                }

                _sqlReader.Close();

                foreach (var t1 in _fails)
                {
                    _sqlCommand =
                        new SqlCommand(
                            "SELECT [name] FROM [Primary] WHERE [id]=@id",
                            _sqlConnection);
                    _sqlCommand.Parameters.AddWithValue("id", t1[0]);
                    _sqlReader = _sqlCommand.ExecuteReader();
                    _sqlReader.Read();
                    t1[0] = _sqlReader["name"].ToString();
                    _sqlReader.Close();
                    _sqlCommand =
                        new SqlCommand(
                            "SELECT [fio0], [fio1], [fio2] FROM [Peoples] WHERE [peopleId]=@peopleId",
                            _sqlConnection);
                    _sqlCommand.Parameters.AddWithValue("peopleId", t1[1]);
                    _sqlReader = _sqlCommand.ExecuteReader();
                    _sqlReader.Read();
                    t1[1] = _sqlReader["fio0"].ToString();
                    t1[1] = _sqlReader["fio1"].ToString();
                    t1[3] = _sqlReader["fio2"].ToString();
                    _sqlReader.Close();
                    _sqlCommand =
                        new SqlCommand(
                            "SELECT [name], [number], [date] FROM [Orders] WHERE [id]=@id",
                            _sqlConnection);
                    _sqlCommand.Parameters.AddWithValue("id", Convert.ToInt32(t1[6]));
                    _sqlReader = _sqlCommand.ExecuteReader();
                    _sqlReader.Read();
                    t1[6] = _sqlReader["name"].ToString();
                    t1[7] = _sqlReader["number"].ToString();
                    t1[8] = Convert.ToDateTime(_sqlReader["date"]).ToString("dd.MM.yyyy");
                    _sqlReader.Close();
                    if (t1[4] == "ПНСС" || t1[4] == "ФП")
                        t1[9] = "1";
                }
            }
            else
            {
                //таблица
            }
        }
    }
}
