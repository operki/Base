using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Properties;
using Word = Microsoft.Office.Interop.Word;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Drawing;

namespace WindowsFormsApp1
{
    public partial class TasksForm : Form
    {

        //установка шрифта
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont,
            IntPtr pdv, [System.Runtime.InteropServices.In] ref uint pcFonts);

        private readonly PrivateFontCollection _fonts = new PrivateFontCollection();

        Font _roboto;

        private string _userName;
        private SqlDataReader _sqlReader;
        private SqlCommand _sqlCommand;
        private SqlConnection _sqlConnection;
        private string _sqlConnectionString;
        private int _peopleId;
        private int _editAccess;
        private int _colorSchema = 0;
        private bool isSelected = false;
        private int _taskDoes = 0;
        private Color[] _borderColor = { Color.FromArgb(80, 80, 80), Color.FromArgb(150, 150, 150)};
        private Color[] _backColor = { Color.FromArgb(45, 45, 45), Color.FromArgb(225, 225, 225) };
        private Color[] _foreColor = { Color.FromArgb(240, 240, 240), Color.FromArgb(0, 0, 0) };
        private Color[] _mainColor = { Color.FromArgb(12, 93, 165), Color.FromArgb(64, 141, 200) };
        private Color[] _secondColor = { Color.FromArgb(0, 129, 16), Color.FromArgb(37, 148,51 ) };
        private Color[] _changerColor = { Color.FromArgb(255, 149, 0), Color.FromArgb(166, 97, 0) };
        private Color[] _mainHoverColor = { Color.FromArgb(12, 93, 165), Color.FromArgb(12, 93, 165) }; //???
        private Color[] _secondHoverColor = { Color.FromArgb(0, 154, 19), Color.FromArgb(44, 177, 61) };

        private readonly IEnumerable<string[]> _positions;
        public TasksForm(IEnumerable<string[]> positions)
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

            _positions = positions;
        }

        //защита от мерцания при Resize
        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
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
            if (control.Name == "panel1")
                t = 0;
            control.BackColor = ColorSchemaChange(control.BackColor);
            control.ForeColor = ColorSchemaChange(control.ForeColor);
            foreach (Control c in control.Controls)
                ColorSchemaSet(c);
        }

        private void ElementFocusSet(Control control)
        {
            foreach (Control c in control.Controls)
                ElementFocusSet(c);

            var box = control as TextBox;
            if (box != null)
            {
                //приводим к типу и устанавливаем обработчики событий
                box.Enter += ElementFocus_Enter;
                box.MouseEnter += ElementFocus_Enter;
                box.Leave += ElementFocus_Leave;
                box.MouseLeave += ElementFocus_Leave;
            }
            else if (control is ListBox)
            {
                ((ListBox) control).Enter += ElementFocus_Enter;
                ((ListBox) control).MouseEnter += ElementFocus_Enter;
                ((ListBox) control).Leave += ElementFocus_Leave;
                ((ListBox) control).MouseLeave += ElementFocus_Leave;
            }
        }

        private void ElementFocus_Enter(object sender, EventArgs e)
        {
            var box = sender as TextBox;
            if (box != null)
                ElementFocus(box.Name, true);
            else if (sender is ListBox)
                ElementFocus(((ListBox) sender).Name, true);
        }

        private void ElementFocus_Leave(object sender, EventArgs e)
        {
            var box = sender as TextBox;
            if (box != null)
                ElementFocus(box.Name, false);
            else if (sender is ListBox)
                ElementFocus(((ListBox) sender).Name, false);
        }

        /// <summary>
        ///     Изменение цвета связанных panel и button
        /// </summary>
        /// <param name="elementName"></param>
        /// <param name="focusSet"></param>
        private void ElementFocus(string elementName, bool focusSet)
        {
            var elementChange = Controls.Find(elementName, true);
            if (elementChange.Length <= 0) return;
            if (!focusSet && elementChange[0].Focused) return;
            string[] elementNameChanger;
            if (elementChange[0] is TextBox)
                elementNameChanger = new[] {"tb", "tbp", "tbp1", "tbp2", "b1", "b"};
            else if (elementChange[0] is ListBox)
                elementNameChanger = new[] {"lb", "lbp", "lbp1", "lbp2", "b1", "b"};
            else
                return;
            var changedColor = focusSet ? Color.FromArgb(0, 129, 16) : Color.FromArgb(12, 93, 165);
            elementChange[0].ForeColor = focusSet ? changedColor : Color.FromArgb(240, 240, 240);
            for (var i = 0; i < elementNameChanger.Length - 1; i++)
            {
                elementName = elementName.Replace(elementNameChanger[i], elementNameChanger[i + 1]);
                elementChange = Controls.Find(elementName, true);
                if (elementChange.Length > 0)
                    elementChange[0].BackColor = changedColor;
            }
        }

        /// <summary>
        ///     Переопределение элемента paint listbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbChoosePeopleFind_DrawItem(object sender, DrawItemEventArgs e)
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
                g.DrawString(itemText, e.Font, itemTextColorBrush, location.X, location.Y + Convert.ToInt32(elementChange.ItemHeight / 9));
                backgroundColorBrush.Dispose();
                itemTextColorBrush.Dispose();
            }

            e.DrawFocusRectangle();
        }

        private void TasksForm_Load(object sender, EventArgs e)
        {
            foreach (Control control in Controls)
                ElementFocusSet(control);
        }

        private void LoadTasks()
        {
            bEditBack.Text = "        Назад";
            lbpTasks.BringToFront();
            //все задачи
            lbTasks.Items.Clear();
            lbTasksPeoplesId.Items.Clear();
            lbTasksId.Items.Clear();
            _sqlReader = null;
            _sqlCommand =
                new SqlCommand(
                    "SELECT [id], [destination], [name], [peopleId] FROM [Tasks] WHERE [isWork]=@isWork ORDER BY [action]",
                    _sqlConnection);
            _sqlCommand.Parameters.AddWithValue("isWork", -1);
            _sqlReader = _sqlCommand.ExecuteReader();
            while (_sqlReader.Read())
            {
                lbTasksPeoplesId.Items.Add(_sqlReader["peopleId"].ToString());
                lbTasksId.Items.Add(_sqlReader["id"].ToString());
                lbTasks.Items.Add(_sqlReader["name"] + " " + _sqlReader["destination"]);
            }

            _sqlReader.Close();
            //данные по людям в отображение
            for (var i = 0; i < lbTasksPeoplesId.Items.Count; i++)
            {
                _sqlCommand =
                    new SqlCommand(
                        "SELECT [fio0], [fio1], [fio2] FROM [Peoples] WHERE [id]=@peopleId",
                        _sqlConnection);
                _sqlCommand.Parameters.AddWithValue("peopleId", Convert.ToInt32(lbTasksPeoplesId.Items[i]));
                _sqlReader = _sqlCommand.ExecuteReader();
                _sqlReader.Read();
                lbTasks.Items[i] = (i + 1) + ". " + _sqlReader["fio0"] + " " + _sqlReader["fio1"] +
                                   " " + _sqlReader["fio2"] + " | " + lbTasks.Items[i];
                _sqlReader.Close();
            }
        }

        public void LoadFromSQL(string sqlConnectionStringFrom, string formUserName, int editAccess, int peopleSetId, string peopleName, int colorSchema)
        {
            _editAccess = editAccess;
            _peopleId = peopleSetId;
            _userName = formUserName;
            _sqlConnectionString = sqlConnectionStringFrom;
            _sqlConnection = new SqlConnection(_sqlConnectionString);
            _sqlConnection.Open();
            lbpTasks.Left = 1;
            lbpTasks.Top = 71;
            lbpTasks.Height = Height - 1;
            lbpTasks.Width = Width - 1;
            if (editAccess == 1)
            {
                lbpTasks.BringToFront();
            }
            LoadTasks();

            _colorSchema = colorSchema == 0 ? 1 : 0;
            ColorSchemaSet(this);
            _colorSchema = colorSchema;
        }

        private void BEditBack_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void bOpenPeople_Click(object sender, EventArgs e)
        {
            if (lbTasks.SelectedIndex > -1)
            {
                //редактирование человека
                var editPeopleForm = new EditPeopleForm (_positions) {Left = Left, Top = Top, Height = Height, Width = Width};
                editPeopleForm.LoadFromSQL(_sqlConnectionString, _userName, _editAccess, Convert.ToInt32(lbTasksPeoplesId.Text), _colorSchema, false);
                Hide();
                editPeopleForm.Closed += (s, args) =>
                {
                    Left = editPeopleForm.Left;
                    Top = editPeopleForm.Top;
                    Height = editPeopleForm.Height;
                    Width = editPeopleForm.Width;
                    WindowState = editPeopleForm.WindowState;
                    Show();
                };
                editPeopleForm.Show(this);
            }
        }

        private void ButtonCheck()
        {
            if (lbTasks.SelectedIndex == -1)
            {
                bOpenPeople.Enabled = false;
                bCloseTask.Enabled = false;
                bOpenPeople.Text = "";
                bCloseTask.Text = "";
            }

            else
            {
                bOpenPeople.Enabled = true;
                bCloseTask.Enabled = true;
                bOpenPeople.Text = "         Открыть выбранного военнослужащего";
                bCloseTask.Text = "         Удалить задачу";
            }
        }

        private void LbTasks_SelectedIndexChanged(object sender, EventArgs e)
        {
            ButtonCheck();
            lbTasksId.SelectedIndex = lbTasks.SelectedIndex;
            lbTasksPeoplesId.SelectedIndex = lbTasks.SelectedIndex;
        }

        private void BCloseTask_Click(object sender, EventArgs e)
        {
            if (lbTasksId.SelectedIndex <= -1) return;
            _sqlCommand = new SqlCommand(
                "UPDATE [Tasks] SET [nameWork]=@nameWork, [dateWork]=@dateWork, [isWork]=@isWork WHERE [id]=@id",
                _sqlConnection);
            _sqlCommand.Parameters.AddWithValue("id", Convert.ToInt32(lbTasksId.Text));
            _sqlCommand.Parameters.AddWithValue("dateWork", DateTime.Now);
            _sqlCommand.Parameters.AddWithValue("nameWork", _userName);
            _sqlCommand.Parameters.AddWithValue("isWork", 1);
            _sqlCommand.ExecuteNonQuery();
            LoadTasks();
        }

        private void bMax_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Maximized)
            {
                WindowState = FormWindowState.Normal;
                bMax.BackgroundImage = Resources.fullin30;
            }
            else
            {
                WindowState = FormWindowState.Maximized;
                bMax.BackgroundImage = Resources.fulloff30;
            }
        }

        private void bExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void lbTasks_DoubleClick(object sender, EventArgs e)
        {
            if (lbTasks.SelectedIndex > -1)
            {
                //редактирование человека
                var editPeopleForm = new EditPeopleForm (_positions) {Left = Left, Top = Top, Height = Height, Width = Width};
                editPeopleForm.LoadFromSQL(_sqlConnectionString, _userName, _editAccess, Convert.ToInt32(lbTasksPeoplesId.Text), _colorSchema, false);
                Hide();
                editPeopleForm.Closed += (s, args) =>
                {
                    Left = editPeopleForm.Left;
                    Top = editPeopleForm.Top;
                    Height = editPeopleForm.Height;
                    Width = editPeopleForm.Width;
                    WindowState = editPeopleForm.WindowState;
                    Show();
                };
                editPeopleForm.Show(this);
            }
        }

        private void pAddTask_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bMax_MouseEnter(object sender, EventArgs e)
        {
            bMax.Image = WindowState == FormWindowState.Normal ? Resources.fullin30_1 : Resources.fulloff30_1;
        }

        private void bMax_MouseLeave(object sender, EventArgs e)
        {
            bMax.Image = WindowState == FormWindowState.Normal ? Resources.fullin30 : Resources.fulloff30;
        }
    }
}
