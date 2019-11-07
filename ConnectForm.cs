using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
//установка шрифта
using System.Drawing.Text;
using WindowsFormsApp1.Properties;

namespace WindowsFormsApp1
{
    public partial class ConnectForm : Form
    {

        private bool _connectCan = true;
        private SqlConnection _sqlConnection;
        private SqlDataReader _sqlReader;
        private SqlCommand _sqlCommand;
        private string _sqlConnectionString;

        //редактирование BD (0 нет, 1 да)
        //выгрузка в excel (0 нет, 1 да)
        //люди (0 только заказ справок, 1 чтение данные и печать справок)
        //добавление людей (0 нет, 1 да)
        //люди (0 только чтение, 1 изменение почти всего, 2 + изменение должностей)

        private string _userName;
        private int[] _userRights = { 0, 0, 0, 0, 0 };

        //установка шрифта
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont,
            IntPtr pdv, [System.Runtime.InteropServices.In] ref uint pcFonts);
        private readonly PrivateFontCollection _fonts = new PrivateFontCollection();
        Font _roboto;
        public ConnectForm()
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
            _roboto = new Font(_fonts.Families[0], 14.0F);
            foreach (Control control in Controls)
                FontSet(control, _roboto);
        }
        private void FontSet(Control control, Font font)
        {
            foreach (Control c in control.Controls)
                FontSet(c, font);
            control.Font = font;
        }

        //загрузка прав пользователя и загрузка основной таблицы
        private void SetUser()
        {
            _sqlCommand = new SqlCommand("SELECT [rights] FROM [Users] WHERE [name]=@name", _sqlConnection);
            _sqlCommand.Parameters.AddWithValue("name", _userName);
            _sqlReader = _sqlCommand.ExecuteReader();
            _sqlReader.Read();
            var sqlRights = _sqlReader["rights"].ToString();
            _sqlReader.Close();
            for (var i = 0; i < _userRights.Length; i++)
                _userRights[i] = Convert.ToInt32(sqlRights[i]) - 48;
        }

        private void ConnectForm_Load(object sender, EventArgs e)
        {
            //начальное оформление
            try
            {
                _sqlConnectionString = "Data Source=" + tbConnectBD.Text +
                                       @";Initial Catalog=DatabasePeopleMain;User ID=sa;Password=operki1991";
                _sqlConnection = new SqlConnection(_sqlConnectionString);
                _sqlConnection.Open();
                lbUserNames.Items.Clear();
                lbUserPasswords.Items.Clear();
                _sqlCommand = new SqlCommand("SELECT [name], [password] FROM [Users] ORDER BY [name]", _sqlConnection);
                _sqlReader = _sqlCommand.ExecuteReader();
                while (_sqlReader.Read())
                {
                    lbUserNames.Items.Add(_sqlReader["name"].ToString());
                    lbUserPasswords.Items.Add(_sqlReader["password"].ToString());
                }
                //версия release
                //lbUserNames.SelectedIndex = 0;
                //tbUserPass.Text = "admin";
                lbUserNames.SelectedIndex = 8;
                tbUserPass.Text = "1";
            }
            catch
            {
                _sqlReader?.Close();
                lbUserNames.Items.Clear();
                lbUserPasswords.Items.Clear();
                lbUserNames.Items.Add("Admin");
                lbUserPasswords.Items.Add("admin");
                throw;
            }
            finally
            {
                _sqlReader?.Close();
                lbUserNamesScroll.Maximum = lbUserNames.Items.Count - 1;
                lbUserNamesScroll.Height = lbUserNames.Height;
                lbUserNamesScroll.Visible = lbUserNamesScroll.Maximum * 30 > lbUserNames.Height;
                if (lbUserNamesScroll.Height < 5) lbUserNamesScroll.Height = 5;
                var step = lbUserNamesScroll.Maximum * 30 / lbUserNamesScroll.Height;
                if (step < 2) step = 2;
                step = Convert.ToInt32(lbUserNamesScroll.Height / step);
                if (step < 5) step = 5;
                lbUserNamesScroll.ThumbSize = step;
                lbUserNames.Select();
            }

        }
        private void ConnectToBD(bool isUser)
        {
            try
            {
                if (!_connectCan) return;
                //соединение с базой данных
                if (_sqlConnection == null)
                {
                    _sqlConnectionString = "Data Source=" + tbConnectBD.Text +
                                           ";Initial Catalog=DatabasePeopleMain;User ID=sa;Password=operki1991";
                    _sqlConnection = new SqlConnection(_sqlConnectionString);
                    _sqlConnection.Open();
                }
                if (!isUser)
                {
                    //_userName = "Гость";
                    //_userRights = new[] {0, 0, 0, 0, 0};
                    var terminalForm = new TerminalForm(_sqlConnectionString);
                    Hide();
                    terminalForm.Show(this);
                }
                else
                {
                    var userIndex = lbUserNames.SelectedIndex;
                    if (lbUserPasswords.Items[lbUserNames.SelectedIndex].ToString() != tbUserPass.Text)
                        userIndex = -1;
                    if (userIndex < 0)
                    {
                        bConnectBD.Text = "        Неверный пароль";
                        bConnectBD.BackColor = Color.FromArgb(255, 13, 0);
                        tPass.Enabled = true;
                        _connectCan = false;
                        return;
                    }
                    _userName = lbUserNames.Text;
                    SetUser();
                    //экран добавления человека
                    var startForm = new StartForm(_sqlConnectionString, _userName, _userRights);
                    startForm.LoadFromConnect();
                    Hide();
                    startForm.Show(this);
                }
            }
            finally
            {
                _sqlReader?.Close();
            }
        }

        private void lbUserNames_DoubleClick(object sender, EventArgs e)
        {
            ConnectToBD(true);
        }

        private void ConnectForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Enter
            if (e.KeyChar == '\r')
                ConnectToBD(true);
        }

        private void pbPassLook_MouseLeave(object sender, EventArgs e)
        {
            tbUserPass.PasswordChar = '*';
            pbPassLook.Image = Resources.pass;
        }

        private void bConnectBD_Click(object sender, EventArgs e)
        {
            tbUserPass.Focus();
            ConnectToBD(true);
        }

        private void lbUserNames_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();

            bool isItemSelected = ((e.State & DrawItemState.Selected) == DrawItemState.Selected);
            int itemIndex = e.Index;
            if (itemIndex >= 0 && itemIndex < lbUserNames.Items.Count)
            {
                Graphics g = e.Graphics;

                // Background Color
                SolidBrush backgroundColorBrush = new SolidBrush((isItemSelected) ? Color.FromArgb(12, 93, 165) : Color.FromArgb(45, 45, 45));
                g.FillRectangle(backgroundColorBrush, e.Bounds);

                // Set text color
                string itemText = lbUserNames.Items[itemIndex].ToString();

                SolidBrush itemTextColorBrush = new SolidBrush(Color.FromArgb(240, 240, 240));
                var location = lbUserNames.GetItemRectangle(itemIndex).Location;
                g.DrawString(itemText, e.Font, itemTextColorBrush, location.X, location.Y + 4);

                // Clean up
                backgroundColorBrush.Dispose();
                itemTextColorBrush.Dispose();
            }

            e.DrawFocusRectangle();
        }

        private void ConnectForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //закрытие соединения с базой
            if (_sqlConnection != null && _sqlConnection.State != ConnectionState.Closed)
                _sqlConnection.Close();
        }

        private void bExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void bMin_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void pWindowState_MouseDown(object sender, MouseEventArgs e)
        {
            pWindowState.Capture = false;
            Message m = Message.Create(base.Handle, 0xa1, new IntPtr(2), IntPtr.Zero);
            this.WndProc(ref m);
        }

        private void tbConnectBD_Enter(object sender, EventArgs e)
        {
            tbConnectBD.ForeColor = Color.FromArgb(12, 93, 165);
            tbpConnectBD.BackColor = Color.FromArgb(12, 93, 165);
            tbpbConnectBD.Image = Resources.link1;
        }

        private void tbConnectBD_Leave(object sender, EventArgs e)
        {
            tbConnectBD.ForeColor = Color.FromArgb(240, 240, 240);
            tbpConnectBD.BackColor = Color.FromArgb(240, 240, 240);
            tbpbConnectBD.Image = Resources.link;
        }

        private void tbUserPass_Enter(object sender, EventArgs e)
        {
            tbUserPass.ForeColor = Color.FromArgb(12, 93, 165);
            tbpUserPass.BackColor = Color.FromArgb(12, 93, 165);
            tbpbUserPass.Image = Resources.key1;
        }

        private void tbUserPass_Leave(object sender, EventArgs e)
        {
            tbUserPass.ForeColor = Color.FromArgb(240, 240, 240);
            tbpUserPass.BackColor = Color.FromArgb(240, 240, 240);
            tbpbUserPass.Image = Resources.key;
        }

        private void tPass_Tick(object sender, EventArgs e)
        {
            tPass.Enabled = false;
            bConnectBD.Text = "        Подключиться";
            bConnectBD.BackColor = Color.FromArgb(12, 93, 165);
            _connectCan = true;
        }

        private void ConnectForm_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, ClientRectangle, Color.FromArgb(80, 80, 80), ButtonBorderStyle.Solid);
        }

        private void lbUserNames_Enter(object sender, EventArgs e)
        {
            lbpbUserNames.Image = Resources.user1;
        }

        private void lbUserNames_LocationChanged(object sender, EventArgs e)
        {

        }

        private void lbUserNames_Leave(object sender, EventArgs e)
        {
            lbpbUserNames.Image = Resources.user;
        }

        private void pbPassLook_MouseHover_1(object sender, EventArgs e)
        {
            pbPassLook.Image = Resources.pass1;
            tbUserPass.PasswordChar = '\0';
        }

        private void lbUserNamesScroll_Scroll(object sender, ScrollEventArgs e)
        {
            lbUserNames.SelectedIndex = lbUserNamesScroll.Value;
        }

        private void lbUserNames_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void lbpbUserNames_Click(object sender, EventArgs e)
        {

        }

        private void bTerminal_Click(object sender, EventArgs e)
        {
            ConnectToBD(false);
        }

        private void bMin_MouseEnter(object sender, EventArgs e)
        {
            bMin.Image = Resources.minimum1;
        }

        private void bMin_MouseLeave(object sender, EventArgs e)
        {
            bMin.Image = Resources.minimum;
        }
    }
}
