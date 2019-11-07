using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using WindowsFormsApp1.Properties;
//установка шрифта
using System.Drawing.Text;

namespace WindowsFormsApp1
{
    public partial class TerminalForm : Form
    {
        private readonly string _sqlConnectionString;
        private bool _canInputGuest = true;
        private string _exitForGuest = "";
        private int _peopleId;
        private bool _isSelected = false;
        private Color _borderColor = Color.FromArgb(80, 80, 80);
        private Color _backColor = Color.FromArgb(45, 45, 45);
        private Color _foreColor = Color.FromArgb(240, 240, 240);
        private Color _mainColor = Color.FromArgb(12, 93, 165);
        private Color _secondColor = Color.FromArgb(0, 129, 16);
        private Color _changerColor = Color.FromArgb(255, 149, 0);
        private Color _mainHoverColor = Color.FromArgb(12, 93, 165);
        private Color _secondHoverColor = Color.FromArgb(0, 154, 19);
        private readonly List<string> _lNumber = new List<string>();
        private readonly List<string> _names = new List<string>();
        private readonly List<int> _id = new List<int>();
        private SqlCommand _sqlCommand;
        private SqlConnection _sqlConnection;
        private SqlDataReader _sqlReader;
        private int _taskDoes = 0;

        //установка шрифта
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont,
            IntPtr pdv, [System.Runtime.InteropServices.In] ref uint pcFonts);

        private readonly PrivateFontCollection _fonts = new PrivateFontCollection();
        Font _roboto18;
        Font _roboto24;
        Font _roboto36;
        Font _roboto48;
        Font _roboto72;
        Font _raleway48;

        private void FontSet(Control control, Font font)
        {
            foreach (Control c in control.Controls)
                FontSet(c, font);
            control.Font = font;
        }

        private void OtherKeyboardClickSet(Control control)
        {
            foreach (Control c in control.Controls)
                OtherKeyboardClickSet(c);
            var label = control as Label;
            if (label != null)
            {
                label.Click += OtherKeyboardClick_Click;
            }
        }

        private void OtherKeyboardClick_Click(object sender, EventArgs e)
        {
            var label = sender as Label;
            if (label == null || tbKeyboard.Text.Length > 100) return;
            if (((Label)sender).Name == "lSpace")
                tbKeyboard.Text += " ";
            else
                tbKeyboard.Text += label.Text;
        }

        private void KeyboardClickSet(Control control)
        {
            foreach (Control c in control.Controls)
                KeyboardClickSet(c);
            var label = control as Label;
            if (label != null)
            {
                label.Click += KeyboardClick_Click;
            }
        }

        private void KeyboardClick_Click(object sender, EventArgs e)
        {
            if (tQuest.Enabled) return;
            var label = sender as Label;
            bool isNumber = false;
            string currentChar = "";
            if (label != null)
            {
                if (_exitForGuest == "11П12А1991В")
                {
                    Close();
                    return;
                }

                _exitForGuest += label.Text;
                currentChar = label.Text;
                isNumber = char.IsDigit(label.Text[0]);
            }

            if (isNumber)
            {
                if (tbGuestNumber.ForeColor == _borderColor)
                {
                    tbGuestNumber.Text = currentChar;
                    tbGuestNumber.ForeColor = _foreColor;
                }
                else if (tbGuestNumber.TextLength < 6)
                    tbGuestNumber.Text += currentChar;
                else return;
            }
            else
            {
                if (tbGuestNumberChar.ForeColor == _borderColor)
                {
                    tbGuestNumberChar.Text = currentChar;
                    tbGuestNumberChar.ForeColor = _foreColor;
                    tbGuestNumberLine.ForeColor = _foreColor;
                }
                else if (tbGuestNumberChar.TextLength < 2)
                    tbGuestNumberChar.Text += currentChar;
                else return;
            }

            if (tbGuestNumber.Text.Length < 6 ||
                tbGuestNumber.ForeColor == _borderColor ||
                tbGuestNumberChar.ForeColor == _borderColor) return;
            var findString = tbGuestNumberChar.Text + "-" + tbGuestNumber.Text;
            var finded = 0;
            var findNumbers = 0;
            var i = -1;
            while (findNumbers < 2 && i < _lNumber.Count - 1)
            {
                i++;
                if (_lNumber[i].IndexOf(findString, StringComparison.Ordinal) <= -1) continue;
                finded = i;
                findNumbers++;
            }

            if (findNumbers == 1)
            {
                //постановка задач гостями
                _peopleId = _id[finded];
                tbPeopleTaskName.Text = _names[finded];
                LoadTasksToGrid();
                pAddTask.BringToFront();
                tbGuestNumberChar.Text = "XX";
                tbGuestNumberLine.Text = "–";
                tbGuestNumber.Text = "123456";
                tbGuestNumberChar.ForeColor = _borderColor;
                tbGuestNumberLine.ForeColor = _borderColor;
                tbGuestNumber.ForeColor = _borderColor;
            }
            else
            {
                tQuest.Enabled = true;
                tbGuestNumberChar.Text = "";
                tbGuestNumberLine.Text = "";
                tbGuestNumber.Text = "Не найден!";
                tbGuestNumberChar.ForeColor = _borderColor;
                tbGuestNumberLine.ForeColor = _borderColor;
                tbGuestNumber.ForeColor = Color.FromArgb(255, 13, 0);
            }
        }

        private void LoadTasksToGrid()
        {
            //активные задачи на человеке
            _sqlReader?.Close();
            dgvTasksInWork.Rows.Clear();
            _sqlReader = null;
            _sqlCommand =
                new SqlCommand(
                    "SELECT TOP 7 [destination], [name], [action] FROM [Tasks] " +
                    "WHERE [peopleId]=@peopleId ORDER BY [action] DESC",
                    _sqlConnection);
            _sqlCommand.Parameters.AddWithValue("peopleId", _peopleId);
            _sqlReader = _sqlCommand.ExecuteReader();
            dgvTasksInWork.Rows.Clear();
            if (_sqlReader.HasRows)
            {
                lTasksInWork.Visible = true;
                dgvTasksInWork.Visible = true;
            }

            while (_sqlReader.Read())
            {
                object[] addRow =
                {
                    _sqlReader["action"].ToString(),
                    _sqlReader["name"].ToString(),
                    _sqlReader["destination"].ToString()
                };
                dgvTasksInWork.Rows.Add(addRow);
            }

            _sqlReader.Close();
        }

        public TerminalForm(string sqlConnectionString)
        {
            InitializeComponent();

            //установка шрифта
            var fontData = Resources.roboto;
            var fontPtr = System.Runtime.InteropServices.Marshal.AllocCoTaskMem(fontData.Length);
            System.Runtime.InteropServices.Marshal.Copy(fontData, 0, fontPtr, fontData.Length);
            uint dummy = 0;
            _fonts.AddMemoryFont(fontPtr, Resources.roboto.Length);
            AddFontMemResourceEx(fontPtr, (uint) Resources.roboto.Length, IntPtr.Zero, ref dummy);
            System.Runtime.InteropServices.Marshal.FreeCoTaskMem(fontPtr);
            _roboto18 = new Font(_fonts.Families[0], 18.0F);
            _roboto24 = new Font(_fonts.Families[0], 24.0F);
            _roboto36 = new Font(_fonts.Families[0], 36.0F);
            _roboto48 = new Font(_fonts.Families[0], 48.0F);
            _roboto72 = new Font(_fonts.Families[0], 72.0F);
            fontData = Resources.raleway;
            fontPtr = System.Runtime.InteropServices.Marshal.AllocCoTaskMem(fontData.Length);
            System.Runtime.InteropServices.Marshal.Copy(fontData, 0, fontPtr, fontData.Length);
            dummy = 0;
            _fonts.AddMemoryFont(fontPtr, Resources.raleway.Length);
            AddFontMemResourceEx(fontPtr, (uint) Resources.raleway.Length, IntPtr.Zero, ref dummy);
            System.Runtime.InteropServices.Marshal.FreeCoTaskMem(fontPtr);
            _raleway48 = new Font(_fonts.Families[0], 48.0F);
            foreach (Control control in Controls)
                FontSet(control, _roboto36);
            FontSet(pGuest, _roboto72);
            FontSet(pKeyboard, _roboto72);
            FontSet(tbKeyboard, _roboto48);
            FontSet(lEnterLNumber, _raleway48);
            FontSet(pAddTask, _roboto24);
            FontSet(bEditBack, _roboto36);
            FontSet(dgvTasksInWork, _roboto18);
            FontSet(pTaskDoes, _roboto36);
            FontSet(lTaskDoes, _roboto24);
            FontSet(lTaskFZ, _roboto24);
            KeyboardClickSet(pGuest);
            OtherKeyboardClickSet(pOKWords);
            OtherKeyboardClickSet(pOKNumbers);

            _sqlConnectionString = sqlConnectionString;
            _sqlConnection = new SqlConnection(_sqlConnectionString);
            _sqlConnection.Open();
            _sqlCommand = new SqlCommand(
                "SELECT [id], [fio0], [fio1], [fio2], [lNumber] FROM [Peoples] ORDER BY [lNumber]",
                _sqlConnection);
            _sqlReader = _sqlCommand.ExecuteReader();
            while (_sqlReader.Read())
            {
                _lNumber.Add(_sqlReader["lNumber"].ToString());
                _names.Add(_sqlReader["fio0"] + " " + _sqlReader["fio1"] + " " + _sqlReader["fio2"]);
                _id.Add(Convert.ToInt32(_sqlReader["id"]));
            }

            pGuest.Dock = DockStyle.Fill;
            pAddTask.Dock = DockStyle.Fill;
            pTaskDoes.Dock = DockStyle.Fill;
            pKeyboard.Dock = DockStyle.Fill;
            pGuest.BringToFront();
        }

        private void TerminalForm_Load(object sender, EventArgs e)
        {
            lEnterLNumber.Focus();
        }

        private void TerminalForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_sqlConnection != null && _sqlConnection.State != ConnectionState.Closed)
                _sqlConnection.Close();
            Application.Exit();
        }

        private void lbTasktype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isSelected) return;
            _isSelected = true;
            if (lbTasktype.Text == "Другое")
            {
                lbTasktype.Items[lbTasktype.Items.Count - 1] = "";
                tbKeyboard.Text = "";
                tbKeyboard.Focus();
                pKeyboard.Tag = 0;
                pKeyboard.BringToFront();
            }
            else
            {
                lbTasktype.Items[lbTasktype.Items.Count - 1] = "Другое";
            }

            lbpTaskDestination.Visible = true;
            _isSelected = false;
        }

        private void lbTaskDestination_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isSelected) return;
            _isSelected = true;
            if (lbTaskDestination.Text == "Другое")
            {
                lbTaskDestination.Items[lbTaskDestination.Items.Count - 1] = "";
                tbKeyboard.Text = "";
                tbKeyboard.Focus();
                pKeyboard.Tag = 1;
                pKeyboard.BringToFront();
            }
            else
            {
                lbTaskDestination.Items[lbTaskDestination.Items.Count - 1] = "Другое";
            }

            bAddTask.Visible = true;
            _isSelected = false;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (tbKeyboard.TextLength > 0)
                tbKeyboard.Text = tbKeyboard.Text.Substring(0, tbKeyboard.TextLength - 1);
        }

        private void bClearAll_Click(object sender, EventArgs e)
        {
            tbKeyboard.Text = "";
        }

        private void bEnter_Click(object sender, EventArgs e)
        {
            pKeyboard.SendToBack();
            _isSelected = true;
            if (Convert.ToInt32(pKeyboard.Tag) == 0)
                lbTasktype.Items[lbTasktype.Items.Count - 1] = tbKeyboard.Text;
            else
                lbTaskDestination.Items[lbTaskDestination.Items.Count - 1] = tbKeyboard.Text;
            _isSelected = false;
        }

        private void bAddTask_Click(object sender, EventArgs e)
        {
            if (lbTaskDestination.SelectedIndex == -1 || lbTasktype.SelectedIndex == -1)
            {
                MessageBox.Show(@"Укажите куда нужна справка и какая справка",
                    @"Неполные данные", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                _sqlReader = null;
                _sqlCommand = new SqlCommand(
                    "INSERT INTO [Tasks] (destination, name, peopleId," +
                    "isWork, action, actionUser) VALUES (@destination, @name, @peopleId," +
                    "@isWork, @action, @actionUser)",
                    _sqlConnection);
                _sqlCommand.Parameters.AddWithValue("destination", lbTaskDestination.Text);
                _sqlCommand.Parameters.AddWithValue("name", lbTasktype.Text);
                _sqlCommand.Parameters.AddWithValue("peopleId", _peopleId);
                _sqlCommand.Parameters.AddWithValue("isWork", -1);
                _sqlCommand.Parameters.AddWithValue("action", DateTime.Now);
                _sqlCommand.Parameters.AddWithValue("actionUser", "Terminal");
                _sqlCommand.ExecuteNonQuery();
                _sqlReader?.Close();
                /*object[] addRow =
                {
                    DateTime.Now.ToLongDateString(),
                    lbTaskDestination.Text,
                    lbTasktype.Text
                };
                dgvTasksInWork.Rows.Add(addRow);*/
                LoadTasksToGrid();
                dgvTasksInWork.Focus();
                _taskDoes = 0;
                bTaskClose.Text = "       Вернуться назад (" + (15 - _taskDoes) + ")";
                tTaskDoes.Enabled = true;
                pTaskDoes.Left = pAddTask.Left;
                pTaskDoes.Width = pAddTask.Width;
                lTaskDate.Text = DateTime.Now.ToString("f");
                lTaskType.Text = lbTasktype.Text;
                lTaskDestination.Text = lbTaskDestination.Text;
                pTaskDoes.Visible = true;
                pTaskDoes.BringToFront();
                //Close();
            }
        }

        private void tTaskDoes_Tick(object sender, EventArgs e)
        {
            _taskDoes++;
            bTaskClose.Text = "       Вернуться назад (" + (15 - _taskDoes) + ")";
            if (_taskDoes != 15) return;
            tTaskDoes.Enabled = false;
            pGuest.BringToFront();
            lbTasktype.SelectedIndex = -1;
            lbTaskDestination.SelectedIndex = -1;
            lbpTaskDestination.Visible = false;
            bAddTask.Visible = false;
        }

        private void bTaskClose_Click(object sender, EventArgs e)
        {
            tTaskDoes.Enabled = false;
            pGuest.BringToFront();
            lbTasktype.SelectedIndex = -1;
            lbTaskDestination.SelectedIndex = -1;
            lbpTaskDestination.Visible = false;
            bAddTask.Visible = false;
        }

        private void bTaskAnother_Click(object sender, EventArgs e)
        {
            tTaskDoes.Enabled = false;
            pAddTask.BringToFront();
            lbTasktype.SelectedIndex = -1;
            lbTaskDestination.SelectedIndex = -1;
            lbpTaskDestination.Visible = false;
            LoadTasksToGrid();
            bAddTask.Visible = false;
        }

        private void bClear_Click(object sender, EventArgs e)
        {
            _exitForGuest = "";
            if (tbGuestNumber.TextLength > 0 &&
                tbGuestNumber.ForeColor != _borderColor)
            {
                tbGuestNumber.Text = tbGuestNumber.Text.Substring(0, tbGuestNumber.TextLength - 1);
                if (tbGuestNumber.TextLength == 0)
                {
                    tbGuestNumber.Text = "123456";
                    tbGuestNumber.ForeColor = _borderColor;
                }
            }
            else if (tbGuestNumberChar.TextLength > 0 &&
                     tbGuestNumberChar.ForeColor != _borderColor)
            {
                tbGuestNumberChar.Text = tbGuestNumberChar.Text.Substring(0, tbGuestNumberChar.TextLength - 1);
                if (tbGuestNumberChar.TextLength == 0)
                {
                    tbGuestNumberChar.Text = "XX";
                    tbGuestNumberChar.ForeColor = _borderColor;
                    tbGuestNumberLine.ForeColor = _borderColor;
                }
            }
        }


        /// <summary>
        ///     Переопределение элемента paint listbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbTasktype_DrawItem(object sender, DrawItemEventArgs e)
        {
            var elementChange = ((ListBox) sender).Parent.Controls[((ListBox) sender).Name] as ListBox;
            if (elementChange == null) return;
            e.DrawBackground();
            var isItemSelected = (e.State & DrawItemState.Selected) == DrawItemState.Selected;
            var itemIndex = e.Index;
            if (itemIndex >= 0 && itemIndex < elementChange.Items.Count)
            {
                var g = e.Graphics;
                // Background Color
                var backgroundColorBrush =
                    new SolidBrush(isItemSelected ? Color.FromArgb(0, 129, 16) : Color.FromArgb(45, 45, 45));
                g.FillRectangle(backgroundColorBrush, e.Bounds);
                // Set text color
                var itemText = elementChange.Items[itemIndex].ToString();
                var itemTextColorBrush = new SolidBrush(Color.FromArgb(240, 240, 240));
                var location = elementChange.GetItemRectangle(itemIndex).Location;
                g.DrawString(itemText, e.Font, itemTextColorBrush, location.X,
                    location.Y + Convert.ToInt32(elementChange.ItemHeight / 9));
                backgroundColorBrush.Dispose();
                itemTextColorBrush.Dispose();
            }

            e.DrawFocusRectangle();
        }

        private void tQuest_Tick(object sender, EventArgs e)
        {
            tQuest.Enabled = false;
            tbGuestNumberChar.Text = "XX";
            tbGuestNumberLine.Text = "–";
            tbGuestNumberChar.ForeColor = _borderColor;
            tbGuestNumberLine.ForeColor = _borderColor;
            tbGuestNumber.Text = "123456";
            tbGuestNumber.ForeColor = _borderColor;
        }
    }
}
