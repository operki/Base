using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WindowsFormsApp1.Properties;
//склонение фио
using CaseDecline.CS;
//установка шрифта
using System.Drawing.Text;

namespace WindowsFormsApp1
{
    public partial class EditPeopleForm : Form
    {
        private readonly List<float> _dataVariety = new List<float>();

        private readonly int[][] _dateRange =
        {
            new[] {0, 0, 0},
            new[] {0, 0, 0},
            new[] {0, 0, 0},
            new[] {0, 0, 0}
        };

        private int _dataLast;
        private List<DateTime[]> _dataStartEnd = new List<DateTime[]>();
        private List<string[]> _positionsDictionary = new List<string[]>();
        private List<string[]> _fails = new List<string[]>();
        private bool _dateOpen;
        private bool _helpVisible;
        private int _nokPositionId;
        private int _nshPositionId;
        private int _peopleId;
        private bool _primaryOpen;
        private SqlCommand _sqlCommand;
        private SqlConnection _sqlConnection;
        private string _sqlConnectionString;
        private SqlDataReader _sqlReader;
        private string _userName;
        private int _colorSchema = 0;
        public bool _menuSchema = true;
        private bool _onlyActiveFails = false;
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
        Font _raleway;

        public EditPeopleForm(IEnumerable<string[]> positions)
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
            fontData = Resources.raleway;
            fontPtr = System.Runtime.InteropServices.Marshal.AllocCoTaskMem(fontData.Length);
            System.Runtime.InteropServices.Marshal.Copy(fontData, 0, fontPtr, fontData.Length);
            dummy = 0;
            _fonts.AddMemoryFont(fontPtr, Resources.raleway.Length);
            AddFontMemResourceEx(fontPtr, (uint)Resources.raleway.Length, IntPtr.Zero, ref dummy);
            System.Runtime.InteropServices.Marshal.FreeCoTaskMem(fontPtr);
            _raleway = new Font(_fonts.Families[0], 14.0F);
            foreach (Control control in Controls)
                FontSet(control, _roboto);
            foreach (Control control in pNavigationMenu.Controls)
                FontSet(control, _raleway);

            lbCurrentPosition.Items.Clear();
            foreach (var t in positions)
                lbCurrentPosition.Items.Add(t[1] + " | " + t[2] + t[3] +
                                            (t[10] == "" ? "" : " - " + t[10]));
            lbCurrentPositionScroll.Maximum = lbCurrentPosition.Items.Count - 1;
            if (lbCurrentPositionScroll.Height < 5) lbCurrentPositionScroll.Height = 5;
            var step = lbCurrentPositionScroll.Maximum * 18 / lbCurrentPositionScroll.Height;
            if (step < 2) step = 2;
            step = Convert.ToInt32(lbCurrentPositionScroll.Height / step);
            if (step < 5) step = 5;
            lbCurrentPositionScroll.ThumbSize = step;
            lbCurrentPositionScroll.Visible = lbCurrentPositionScroll.Maximum * 18 > lbCurrentPositionScroll.Height;
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
        private void ChangeMenuSchema()
        {
            var pWidth = _menuSchema ? -160 : 160;
            bMenu.Image = _menuSchema ? Resources.sortmore : Resources.sortleft;
            pNavigationMenu.Width += pWidth;
            bMainData.Text = _menuSchema ? "" : "        Основные";
            bOtherData.Text = _menuSchema ? "" : "        Дополнительные";
            bPositionsData.Text = _menuSchema ? "" : "        Прохожд. службы";
            bMemoryData.Text = _menuSchema ? "" : "        Выслуга лет";
            bTasksData.Text = _menuSchema ? "" : "        Печать справок";
            bFails.Text = _menuSchema ? "" : "        Взыскания";
            pEditPeople.Left += pWidth;
            pEditPeople.Width -= pWidth;
            _menuSchema = !_menuSchema;
        }

        private void EditPeople_Load(object sender, EventArgs e)
        {
            _dataStartEnd = new List<DateTime[]>();
            tcEditPeople.Top -= 31;
            pEditPeople.Top = 30;
            pEditPeople.Left = 231;
            pEditPeople.Height = Height - 31;
            pEditPeople.Width = Width - 237;
            tcEditPeople.SelectedIndex = 0;
            dtpStart.MaxDate = DateTime.Today;
            dtpStartThis.MaxDate = DateTime.Today;
            dtpDateBirthday.MaxDate = DateTime.Today;
            dtpPrimaryOrderDate.MaxDate = DateTime.Today;
            dtpPrimaryDate.MaxDate = DateTime.Today;
            dtpTaskDate.MinDate = DateTime.Today;
            dtpTaskDate.Value = DateTime.Today;
            if (lbCurrentPosition.Items.Count > 0 &&
                lbCurrentPosition.SelectedIndex > 0)
            {
                lbCurrentPosition.SelectedIndex--;
                lbCurrentPosition.SelectedIndex++;
            }

            if (bPeopleDelete.Visible)
            {
                bPeopleDelete.Top = 41;
                lFio0.Top = 100;
            }

            foreach (Control control in Controls) ElementFocusSet(control);
            EducationCalcToText();
            FamilyCalcToText();
            BattlefieldsCalcToText();
            MedalsCalcToText();
            LoadDictionary(Convert.ToInt32(bDecline.Tag));
            if (!_menuSchema)
            {
                _menuSchema = !_menuSchema;
                ChangeMenuSchema();
            }
            
            /*_primaryOpen = true;
            cbNshName.SelectedIndex = Convert.ToInt32(cbNshName.Tag);
            cbNokName.SelectedIndex = Convert.ToInt32(cbNokName.Tag);
            _primaryOpen = false;*/

            //версия release
            //pDecline.Visible = false;
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
            else if (control is RichTextBox)
            {
                ((RichTextBox) control).Enter += ElementFocus_Enter;
                ((RichTextBox) control).MouseEnter += ElementFocus_Enter;
                ((RichTextBox) control).Leave += ElementFocus_Leave;
                ((RichTextBox) control).MouseLeave += ElementFocus_Leave;
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
            else if (sender is RichTextBox)
                ElementFocus(((RichTextBox) sender).Name, true);
        }

        private void ElementFocus_Leave(object sender, EventArgs e)
        {
            if (sender is TextBox)
                ElementFocus(((TextBox) sender).Name, false);
            else if (sender is ListBox)
                ElementFocus(((ListBox) sender).Name, false);
            else if (sender is DataGridView)
                ElementFocus(((DataGridView) sender).Name, false);
            else if (sender is RichTextBox)
                ElementFocus(((RichTextBox) sender).Name, false);
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
            else if (elementChange[0] is RichTextBox)
                elementNameChanger = new[] {"rtb", "rtbp"};
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

        private void NavigationFocus(int idItem, bool setFocus)
        {
            switch (idItem)
            {
                case 0:
                    bpMainData.BackColor = setFocus
                        ? bMainData.BackColor != _secondColor[_colorSchema]
                            ? _secondColor[_colorSchema]
                            : _secondHoverColor[_colorSchema]
                        : bMainData.BackColor != _secondColor[_colorSchema]
                            ? _mainColor[_colorSchema]
                            : _secondColor[_colorSchema];
                    break;
                case 1:
                    bpOtherData.BackColor = setFocus
                        ? bOtherData.BackColor != _secondColor[_colorSchema]
                            ? _secondColor[_colorSchema]
                            : _secondHoverColor[_colorSchema]
                        : bOtherData.BackColor != _secondColor[_colorSchema]
                            ? _mainColor[_colorSchema]
                            : _secondColor[_colorSchema];
                    break;
                case 2:
                    bpPositionsData.BackColor = setFocus
                        ? bPositionsData.BackColor != _secondColor[_colorSchema]
                            ? _secondColor[_colorSchema]
                            : _secondHoverColor[_colorSchema]
                        : bPositionsData.BackColor != _secondColor[_colorSchema]
                            ? _mainColor[_colorSchema]
                            : _secondColor[_colorSchema];
                    break;
                case 3:
                    bpMemoryData.BackColor = setFocus
                        ? bMemoryData.BackColor != _secondColor[_colorSchema]
                            ? _secondColor[_colorSchema]
                            : _secondHoverColor[_colorSchema]
                        : bMemoryData.BackColor != _secondColor[_colorSchema]
                            ? _mainColor[_colorSchema]
                            : _secondColor[_colorSchema];
                    break;
                case 4:
                    bpTasksData.BackColor = setFocus
                        ? bTasksData.BackColor != _secondColor[_colorSchema]
                            ? _secondColor[_colorSchema]
                            : _secondHoverColor[_colorSchema]
                        : bTasksData.BackColor != _secondColor[_colorSchema]
                            ? _mainColor[_colorSchema]
                            : _secondColor[_colorSchema];
                    break;
                case 5:
                    bpFails.BackColor = setFocus
                        ? bFails.BackColor != _secondColor[_colorSchema]
                            ? _secondColor[_colorSchema]
                            : _secondHoverColor[_colorSchema]
                        : bFails.BackColor != _secondColor[_colorSchema]
                            ? _mainColor[_colorSchema]
                            : _secondColor[_colorSchema];
                    break;
            }
        }

        private void GoPanel(int typePanel, bool direction)
        {
            for (var i = 0; i < 61; i++)
            {
                if (typePanel == 0) bpMainData.Top += direction ? 1 : -1;
                else if (typePanel == 1) bpOtherData.Top += direction ? 1 : -1;
                else if (typePanel == 2) bpPositionsData.Top += direction ? 1 : -1;
                else if (typePanel == 3) bpMemoryData.Top += direction ? 1 : -1;
                else if (typePanel == 4) bpTasksData.Top += direction ? 1 : -1;
                else if (typePanel == 5) bpFails.Top += direction ? 1 : -1;
                Application.DoEvents();
            }
        }

        private void SelectPanel(int idPanel)
        {
            if (idPanel == 0 && bpMainData.Top == 0 ||
                idPanel == 1 && bpOtherData.Top == 0 ||
                idPanel == 2 && bpPositionsData.Top == 0 ||
                idPanel == 3 && bpMemoryData.Top == 0 ||
                idPanel == 4 && bpTasksData.Top == 0 ||
                idPanel == 5 && bpFails.Top == 0) return;
            var numbersGo = 0;
            if (idPanel == 0 && bpMainData.Top == 61 ||
                idPanel == 1 && bpOtherData.Top == 61 ||
                idPanel == 2 && bpPositionsData.Top == 61 ||
                idPanel == 3 && bpMemoryData.Top == 61 ||
                idPanel == 4 && bpTasksData.Top == 61 ||
                idPanel == 5 && bpFails.Top == 61)
                numbersGo = 1;
            else if (idPanel == 0 && bpMainData.Top == 122 ||
                     idPanel == 1 && bpOtherData.Top == 122 ||
                     idPanel == 2 && bpPositionsData.Top == 122 ||
                     idPanel == 3 && bpMemoryData.Top == 122 ||
                     idPanel == 4 && bpTasksData.Top == 122 ||
                     idPanel == 5 && bpFails.Top == 122)
                numbersGo = 2;
            else if (idPanel == 0 && bpMainData.Top == 183 ||
                     idPanel == 1 && bpOtherData.Top == 183 ||
                     idPanel == 2 && bpPositionsData.Top == 183 ||
                     idPanel == 3 && bpMemoryData.Top == 183 ||
                     idPanel == 4 && bpTasksData.Top == 183 ||
                     idPanel == 5 && bpFails.Top == 183)
                numbersGo = 3;
            else if (idPanel == 0 && bpMainData.Top == 244 ||
                     idPanel == 1 && bpOtherData.Top == 244 ||
                     idPanel == 2 && bpPositionsData.Top == 244 ||
                     idPanel == 3 && bpMemoryData.Top == 244 ||
                     idPanel == 4 && bpTasksData.Top == 244 ||
                     idPanel == 5 && bpFails.Top == 244)
                numbersGo = 4;
            else if (idPanel == 0 && bpMainData.Top == 305 ||
                     idPanel == 1 && bpOtherData.Top == 305 ||
                     idPanel == 2 && bpPositionsData.Top == 305 ||
                     idPanel == 3 && bpMemoryData.Top == 305 ||
                     idPanel == 4 && bpTasksData.Top == 305 ||
                     idPanel == 5 && bpFails.Top == 305)
                numbersGo = 5;
            for (var i = 0; i < numbersGo; i++)
                GoPanel(idPanel, false);
            if (numbersGo > 4)
            {
                if (bpMainData.Top == 244)
                    GoPanel(0, true);
                else if (bpOtherData.Top == 244)
                    GoPanel(1, true);
                else if (bpPositionsData.Top == 244)
                    GoPanel(2, true);
                else if (bpMemoryData.Top == 244)
                    GoPanel(3, true);
                else if (bpTasksData.Top == 244)
                    GoPanel(4, true);
                else if (bpFails.Top == 244)
                    GoPanel(5, true);
            }

            if (numbersGo > 3)
            {
                if (bpMainData.Top == 183)
                    GoPanel(0, true);
                else if (bpOtherData.Top == 183)
                    GoPanel(1, true);
                else if (bpPositionsData.Top == 183)
                    GoPanel(2, true);
                else if (bpMemoryData.Top == 183)
                    GoPanel(3, true);
                else if (bpTasksData.Top == 183)
                    GoPanel(4, true);
                else if (bpFails.Top == 183)
                    GoPanel(5, true);
            }

            if (numbersGo > 2)
            {
                if (bpMainData.Top == 122)
                    GoPanel(0, true);
                else if (bpOtherData.Top == 122)
                    GoPanel(1, true);
                else if (bpPositionsData.Top == 122)
                    GoPanel(2, true);
                else if (bpMemoryData.Top == 122)
                    GoPanel(3, true);
                else if (bpTasksData.Top == 122)
                    GoPanel(4, true);
                else if (bpFails.Top == 122)
                    GoPanel(5, true);
            }

            if (numbersGo > 1)
            {
                if (bpMainData.Top == 61)
                    GoPanel(0, true);
                else if (bpOtherData.Top == 61)
                    GoPanel(1, true);
                else if (bpPositionsData.Top == 61)
                    GoPanel(2, true);
                else if (bpMemoryData.Top == 61)
                    GoPanel(3, true);
                else if (bpTasksData.Top == 61)
                    GoPanel(4, true);
                else if (bpFails.Top == 61)
                    GoPanel(5, true);
            }

            if (idPanel != 0 && bpMainData.Top == 0)
                GoPanel(0, true);
            else if (idPanel != 1 && bpOtherData.Top == 0)
                GoPanel(1, true);
            else if (idPanel != 2 && bpPositionsData.Top == 0)
                GoPanel(2, true);
            else if (idPanel != 3 && bpMemoryData.Top == 0)
                GoPanel(3, true);
            else if (idPanel != 4 && bpTasksData.Top == 0)
                GoPanel(4, true);
            else if (idPanel != 5 && bpFails.Top == 0)
                GoPanel(5, true);

            switch (idPanel)
            {
                case 0:
                    bMainData.BackColor = _secondColor[_colorSchema];
                    bOtherData.BackColor = _backColor[_colorSchema];
                    bPositionsData.BackColor = _backColor[_colorSchema];
                    bMemoryData.BackColor = _backColor[_colorSchema];
                    bTasksData.BackColor = _backColor[_colorSchema];
                    bFails.BackColor = _backColor[_colorSchema];
                    bpOtherData.BackColor = _mainColor[_colorSchema];
                    bpPositionsData.BackColor = _mainColor[_colorSchema];
                    bpMemoryData.BackColor = _mainColor[_colorSchema];
                    bpTasksData.BackColor = _mainColor[_colorSchema];
                    bpFails.BackColor = _mainColor[_colorSchema];
                    tbFio0.Select();
                    break;
                case 1:
                    bMainData.BackColor = _backColor[_colorSchema];
                    bOtherData.BackColor = _secondColor[_colorSchema];
                    bPositionsData.BackColor = _backColor[_colorSchema];
                    bMemoryData.BackColor = _backColor[_colorSchema];
                    bTasksData.BackColor = _backColor[_colorSchema];
                    bFails.BackColor = _backColor[_colorSchema];
                    bpMainData.BackColor = _mainColor[_colorSchema];
                    bpPositionsData.BackColor = _mainColor[_colorSchema];
                    bpMemoryData.BackColor = _mainColor[_colorSchema];
                    bpTasksData.BackColor = _mainColor[_colorSchema];
                    bpFails.BackColor = _mainColor[_colorSchema];
                    tbPrimaryDate.Select();
                    break;
                case 2:
                    bMainData.BackColor = _backColor[_colorSchema];
                    bOtherData.BackColor = _backColor[_colorSchema];
                    bPositionsData.BackColor = _secondColor[_colorSchema];
                    bMemoryData.BackColor = _backColor[_colorSchema];
                    bTasksData.BackColor = _backColor[_colorSchema];
                    bFails.BackColor = _backColor[_colorSchema];
                    bpMainData.BackColor = _mainColor[_colorSchema];
                    bpOtherData.BackColor = _mainColor[_colorSchema];
                    bpMemoryData.BackColor = _mainColor[_colorSchema];
                    bpTasksData.BackColor = _mainColor[_colorSchema];
                    bpFails.BackColor = _mainColor[_colorSchema];
                    dgvEditHistory.Select();
                    break;
                case 3:
                    bMainData.BackColor = _backColor[_colorSchema];
                    bOtherData.BackColor = _backColor[_colorSchema];
                    bPositionsData.BackColor = _backColor[_colorSchema];
                    bMemoryData.BackColor = _secondColor[_colorSchema];
                    bTasksData.BackColor = _backColor[_colorSchema];
                    bFails.BackColor = _backColor[_colorSchema];
                    bpMainData.BackColor = _mainColor[_colorSchema];
                    bpOtherData.BackColor = _mainColor[_colorSchema];
                    bpPositionsData.BackColor = _mainColor[_colorSchema];
                    bpTasksData.BackColor = _mainColor[_colorSchema];
                    bpFails.BackColor = _mainColor[_colorSchema];
                    dgvMemoryCalend.Select();
                    break;
                case 4:
                    bMainData.BackColor = _backColor[_colorSchema];
                    bOtherData.BackColor = _backColor[_colorSchema];
                    bPositionsData.BackColor = _backColor[_colorSchema];
                    bMemoryData.BackColor = _backColor[_colorSchema];
                    bTasksData.BackColor = _secondColor[_colorSchema];
                    bFails.BackColor = _backColor[_colorSchema];
                    bpMainData.BackColor = _mainColor[_colorSchema];
                    bpOtherData.BackColor = _mainColor[_colorSchema];
                    bpPositionsData.BackColor = _mainColor[_colorSchema];
                    bpMemoryData.BackColor = _mainColor[_colorSchema];
                    bpFails.BackColor = _mainColor[_colorSchema];
                    bNshName.Select();
                    break;
                case 5:
                    bMainData.BackColor = _backColor[_colorSchema];
                    bOtherData.BackColor = _backColor[_colorSchema];
                    bPositionsData.BackColor = _backColor[_colorSchema];
                    bMemoryData.BackColor = _backColor[_colorSchema];
                    bTasksData.BackColor = _backColor[_colorSchema];
                    bFails.BackColor = _secondColor[_colorSchema];
                    bpMainData.BackColor = _mainColor[_colorSchema];
                    bpOtherData.BackColor = _mainColor[_colorSchema];
                    bpPositionsData.BackColor = _mainColor[_colorSchema];
                    bpMemoryData.BackColor = _mainColor[_colorSchema];
                    bpTasksData.BackColor = _mainColor[_colorSchema];
                    bNshName.Select();
                    break;
            }

            tcEditPeople.SelectedIndex = idPanel;
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

        private void bMin_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
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

        private void lbCurrentPosition_DrawItem(object sender, DrawItemEventArgs e)
        {
            //переопределение элемента paint listbox
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
                    new SolidBrush(isItemSelected ? _secondColor[_colorSchema] : _backColor[_colorSchema]);
                g.FillRectangle(backgroundColorBrush, e.Bounds);
                // Set text color
                var itemText = elementChange.Items[itemIndex].ToString();
                var itemTextColorBrush = new SolidBrush(_foreColor[_colorSchema]);
                g.DrawString(itemText, e.Font, itemTextColorBrush, elementChange.GetItemRectangle(itemIndex).Location);
                // Clean up
                backgroundColorBrush.Dispose();
                itemTextColorBrush.Dispose();
            }

            e.DrawFocusRectangle();
        }

        private void LoadDictionary(int declineType)
        {
            _positionsDictionary.Clear();
            _sqlCommand = new SqlCommand(
                    "SELECT [name], [decline1], [decline2] FROM [Dictionary]",
                    _sqlConnection);
            _sqlReader = _sqlCommand.ExecuteReader();
            if (declineType == 2)
            {
                while (_sqlReader.Read())
                {
                    _positionsDictionary.Add(new[] { _sqlReader["name"].ToString(),
                        _sqlReader["decline2"].ToString() });
                }
            }
            else
            {
                while (_sqlReader.Read())
                {
                    _positionsDictionary.Add(new[] { _sqlReader["name"].ToString(),
                        _sqlReader["decline1"].ToString() });
                }
            }
            _sqlReader.Close();
            GoDecline();
        }

        private string PositionDecline(int positionId)
        {
            if (positionId == 2360) return "";
            _sqlReader?.Close();
            _sqlCommand = new SqlCommand("SELECT [fullName] FROM [Positions] WHERE [id]=@id ", _sqlConnection);
            _sqlCommand.Parameters.AddWithValue("id", positionId);
            _sqlReader = _sqlCommand.ExecuteReader();
            _sqlReader.Read();
            var primaryName = _sqlReader["fullName"].ToString().ToLower();
            _sqlReader.Close();
            foreach (var t in _positionsDictionary)
                primaryName = primaryName.Replace(t[0], t[1]);

            primaryName += "+++";
            primaryName = primaryName.Replace(" бригады+++", " войсковой части 71289");
            primaryName = primaryName.Replace("+++", " войсковой части 71289");
            return primaryName;
        }

        private string PrimaryDecline(string primaryName, int declineType)
        {
            if (declineType == 2)
            {
                switch (primaryName)
                {
                    case "рядовой":
                        return "рядового";
                    case "старшина":
                        return "старшины";
                    default:
                        primaryName += "а";
                        primaryName = primaryName.Replace("ший", "шего");
                        return primaryName;
                }
            }
            switch (primaryName)
            {
                case "рядовой":
                    return "рядовому";
                case "старшина":
                    return "старшие";
                default:
                    primaryName += "у";
                    primaryName = primaryName.Replace("ший", "шему");
                    return primaryName;
            }
        }

        private void GoDecline()
        {
            var decline = Convert.ToInt32(bDecline.Tag);
            tbDecline.Text = PrimaryDecline(tbPrimary.Text, decline);
            var fioNames = new Decliner().Decline(tbFio0.Text, tbFio1.Text, tbFio2.Text, decline);
            tbDecline.Text += " " + fioNames[0] + " " + fioNames[1] + " " + fioNames[2];
            tbDecline.Text += ", " + tbDateBirthday.Text + (Char)160 + "г.р., " + tbLNumber.Text + ", ";
            var positionId = 2360;
            if (lbCurrentPositionId.SelectedIndex > -1 && lbCurrentPositionId.Items.Count > lbCurrentPosition.SelectedIndex + 2)
            {
                lbCurrentPositionId.SelectedIndex = lbCurrentPosition.SelectedIndex;
                positionId = Convert.ToInt32(lbCurrentPositionId.Text);
            }
            tbDecline.Text += PositionDecline(positionId);
        }

        private int FindOrderId(string orderName, string orderNumber, DateTime orderDate)
        {
            int orderId;
            _sqlCommand = new SqlCommand(
                "SELECT [id] FROM [Orders] WHERE [name]=@name AND [number]=@number AND [date]=@date",
                _sqlConnection);
            _sqlCommand.Parameters.AddWithValue("name", orderName);
            _sqlCommand.Parameters.AddWithValue("number", orderNumber);
            _sqlCommand.Parameters.AddWithValue("date", orderDate);
            _sqlReader = _sqlCommand.ExecuteReader();
            if (_sqlReader.HasRows)
            {
                _sqlReader.Read();
                orderId = Convert.ToInt32(_sqlReader["id"]);
                _sqlReader.Close();
            }
            else
            {
                _sqlReader.Close();
                _sqlCommand =
                    new SqlCommand(
                        "INSERT INTO [Orders] (name, number, date, action," +
                        " actionUser) VALUES (@name, @number, @date, @action, @actionUser)",
                        _sqlConnection);
                _sqlCommand.Parameters.AddWithValue("name", orderName);
                _sqlCommand.Parameters.AddWithValue("number", orderNumber);
                _sqlCommand.Parameters.AddWithValue("date", orderDate);
                _sqlCommand.Parameters.AddWithValue("action", DateTime.Now);
                _sqlCommand.Parameters.AddWithValue("actionUser", _userName);
                _sqlCommand.ExecuteNonQuery();
                _sqlReader.Close();
                _sqlCommand = new SqlCommand(
                    "SELECT [id] FROM [Orders] WHERE [name]=@name AND [number]=@number AND [date]=@date",
                    _sqlConnection);
                _sqlCommand.Parameters.AddWithValue("name", orderName);
                _sqlCommand.Parameters.AddWithValue("number", orderNumber);
                _sqlCommand.Parameters.AddWithValue("date", orderDate);
                _sqlReader = _sqlCommand.ExecuteReader();
                _sqlReader.Read();
                orderId = Convert.ToInt32(_sqlReader["id"]);
                _sqlReader.Close();
            }

            return orderId;
        }

        public void LoadFromSQL(string sqlConnectionString, string formUserName, int userAccess, int peopleId, int colorSchema, bool menuSchema)
        {
            _menuSchema = menuSchema;
            _peopleId = peopleId;
            _userName = formUserName;
            switch (userAccess)
            {
                case 0:
                    foreach (var textBox in tabPage1.Controls.OfType<TextBox>())
                        textBox.ReadOnly = true;
                    foreach (var textBox in tabPage2.Controls.OfType<TextBox>())
                        textBox.ReadOnly = true;
                    foreach (var textBox in tabPage3.Controls.OfType<TextBox>())
                        textBox.ReadOnly = true;
                    foreach (var textBox in tabPage4.Controls.OfType<TextBox>())
                        textBox.ReadOnly = true;
                    foreach (var dateTimePicker in tabPage1.Controls.OfType<DateTimePicker>())
                        dateTimePicker.Enabled = false;
                    foreach (var dateTimePicker in tabPage2.Controls.OfType<DateTimePicker>())
                        dateTimePicker.Enabled = false;
                    foreach (var dateTimePicker in tabPage3.Controls.OfType<DateTimePicker>())
                        dateTimePicker.Enabled = false;
                    foreach (var dateTimePicker in tabPage4.Controls.OfType<DateTimePicker>())
                        dateTimePicker.Enabled = false;
                    foreach (var dateGridView in tabPage1.Controls.OfType<DataGridView>())
                        dateGridView.ReadOnly = true;
                    foreach (var dateGridView in tabPage2.Controls.OfType<DataGridView>())
                        dateGridView.ReadOnly = true;
                    foreach (var dateGridView in tabPage3.Controls.OfType<DataGridView>())
                        dateGridView.ReadOnly = true;
                    foreach (var dateGridView in tabPage4.Controls.OfType<DataGridView>())
                        dateGridView.ReadOnly = true;
                    dgvEditEducations.ReadOnly = true;
                    dgvEditFamily.ReadOnly = true;
                    dgvEditBattlefields.ReadOnly = true;
                    dgvEditMedals.ReadOnly = true;
                    dgvMemoryCalend.ReadOnly = true;
                    dgvMemoryJump.ReadOnly = true;
                    dgvMemoryFar.ReadOnly = true;
                    dgvMemoryCivilian.ReadOnly = true;
                    tbCurrentPosition.Height = +26;
                    break;
                case 2:
                    bCurrentPosition.Visible = true;
                    b1CurrentPosition.Visible = true;
                    if (_userName == "Admin")
                        bPeopleDelete.Visible = true;
                    break;
            }

            if (userAccess != 2)
                tbCurrentPosition.Height += 35;
            _sqlConnectionString = sqlConnectionString;
            _sqlConnection = new SqlConnection(sqlConnectionString);
            _sqlConnection.Open();
            //загрузка воинских званий
            _sqlCommand = new SqlCommand("SELECT [name] FROM [Primary]", _sqlConnection);
            _sqlReader = _sqlCommand.ExecuteReader();
            cbPrimary.Items.Clear();
            while (_sqlReader.Read())
                cbPrimary.Items.Add(_sqlReader["name"].ToString());
            _sqlReader.Close();
            //загрузка должностей
            _sqlCommand = new SqlCommand("SELECT [id], [parent1], [parent2], [parent3], " +
                                         "[parent4], [name] FROM [Positions] ORDER BY [Position]", _sqlConnection);
            _sqlReader = _sqlCommand.ExecuteReader();
            lbCurrentPositionId.Items.Clear();
            while (_sqlReader.Read()) lbCurrentPositionId.Items.Add(_sqlReader["id"].ToString());

            _sqlReader.Close();
            //редактирование человека
            if (peopleId > -1)
            {
                //если нужна печать справок
                _sqlCommand = new SqlCommand(
                    "SELECT [name], [destination] FROM [Tasks] WHERE [peopleId]=@peopleId AND [isWork]=@isWork",
                    _sqlConnection);
                _sqlCommand.Parameters.AddWithValue("peopleId", _peopleId);
                _sqlCommand.Parameters.AddWithValue("isWork", -1);
                _sqlReader = _sqlCommand.ExecuteReader();
                if (_sqlReader.HasRows)
                {
                    tcEditPeople.SelectedIndex = 4;
                    _sqlReader.Read();
                    //назначение и какая справка
                    tbTaskDestination.Text = _sqlReader["destination"].ToString();
                }

                _sqlReader.Close();
                lbTaskName.SelectedIndex = 0;
                //загрузка людей

                    _sqlCommand = new SqlCommand("SELECT [id] FROM [Positions] WHERE [fullName]=@fullName",
                        _sqlConnection);
                    _sqlCommand.Parameters.AddWithValue("fullName", "Начальник штаба – заместитель командира бригады");
                    _sqlReader = _sqlCommand.ExecuteReader();
                    _sqlReader.Read();
                    _nshPositionId = Convert.ToInt32(_sqlReader["id"]);
                
                    _sqlReader.Close();
                    _sqlCommand = new SqlCommand("SELECT [id] FROM [Peoples] WHERE [positionId]=@positionId",
                        _sqlConnection);
                    _sqlCommand.Parameters.AddWithValue("positionId", _nshPositionId);
                    _sqlReader = _sqlCommand.ExecuteReader();
                    if (_sqlReader.HasRows)
                    {
                        _sqlReader.Read();
                        cbNshName.Tag = Convert.ToInt32(_sqlReader["id"]);
                    }
                    _sqlReader.Close();

                    cbNshName.Tag = _nshPositionId;
                    _sqlReader.Close();
                    _sqlCommand = new SqlCommand("SELECT [id] FROM [Positions] WHERE [fullName]=@fullName",
                        _sqlConnection);
                    _sqlCommand.Parameters.AddWithValue("fullName", "Начальник отделения кадров");
                    _sqlReader = _sqlCommand.ExecuteReader();
                    _sqlReader.Read();
                    _nokPositionId = Convert.ToInt32(_sqlReader["id"]);
                    cbNokName.Tag = _nokPositionId;
                    _sqlReader.Close();
                
                    _sqlReader.Close();
                    _sqlCommand = new SqlCommand("SELECT [id] FROM [Peoples] WHERE [positionId]=@positionId",
                        _sqlConnection);
                    _sqlCommand.Parameters.AddWithValue("positionId", _nokPositionId);
                    _sqlReader = _sqlCommand.ExecuteReader();
                    if (_sqlReader.HasRows)
                    {
                        _sqlReader.Read();
                        cbNokName.Tag = Convert.ToInt32(_sqlReader["id"]);
                    }
                    _sqlReader.Close();

                _sqlCommand = new SqlCommand("SELECT TOP 1 [nshId], [nokId] " +
                                             "FROM [Settings] ORDER BY [action] DESC", _sqlConnection);
                _sqlReader = _sqlCommand.ExecuteReader();
                if (_sqlReader.HasRows)
                {
                    _sqlReader.Read();
                    _nshPositionId = Convert.ToInt32(_sqlReader["nshId"]);
                    _nokPositionId = Convert.ToInt32(_sqlReader["nokId"]);
                }

                _sqlReader.Close();

                _sqlCommand =
                    new SqlCommand(
                        "SELECT [id], [fio0], [fio1], [fio2], [positionId] FROM [Peoples] " +
                        "WHERE [primaryId]>@primaryId ORDER BY [fio0], [fio1], [fio2]",
                        _sqlConnection);
                _sqlCommand.Parameters.AddWithValue("primaryId", 6);
                _sqlReader = _sqlCommand.ExecuteReader();
                cbNshName.Items.Clear();
                cbNokName.Items.Clear();
                lbPeoplesId.Items.Clear();
                _primaryOpen = true;
                while (_sqlReader.Read())
                {
                    //принадлежат к другой воинской части
                    if (Convert.ToInt32(_sqlReader["positionId"]) == 2360) continue;
                    //иначе
                    cbNshName.Items.Add(_sqlReader["fio0"] + " " + _sqlReader["fio1"] + " " + _sqlReader["fio2"]);
                    cbNokName.Items.Add(_sqlReader["fio0"] + " " + _sqlReader["fio1"] + " " + _sqlReader["fio2"]);
                    lbPeoplesId.Items.Add(_sqlReader["id"]);
                    if (_nshPositionId == Convert.ToInt32(_sqlReader["positionId"]))
                        cbNshName.SelectedIndex = cbNshName.Items.Count - 1;
                    if (_nokPositionId == Convert.ToInt32(_sqlReader["positionId"]))
                        cbNokName.SelectedIndex = cbNokName.Items.Count - 1;
                }
                _primaryOpen = false;

                _sqlReader.Close();

                bEditBack.Text = "          Назад";
                bEditNext.Text = "          Обновить данные";
                bEditBack2.Text = bEditBack.Text;
                bEditBack3.Text = bEditBack.Text;
                bEditBack4.Text = bEditBack.Text;
                bEditNext2.Text = bEditNext.Text;
                bEditNext3.Text = bEditNext.Text;
                bEditNext4.Text = bEditNext.Text;
                bEditNext.Enabled = true;
                bEditNext2.Enabled = true;
                bEditNext3.Enabled = true;
                bEditNext4.Enabled = true;
                //чтение данных человека
                _sqlReader = null;
                _sqlCommand = new SqlCommand("SELECT * FROM [Peoples] WHERE [id]=@id", _sqlConnection);
                _sqlCommand.Parameters.AddWithValue("id", _peopleId);
                try
                {
                    _sqlReader = _sqlCommand.ExecuteReader();
                    _sqlReader.Read();
                    tbFio0.Text = _sqlReader["fio0"].ToString();
                    tbFio1.Text = _sqlReader["fio1"].ToString();
                    tbFio2.Text = _sqlReader["fio2"].ToString();
                    Text = tbFio0.Text + " " + tbFio1.Text + " " + tbFio2.Text;
                    ChangeGender(Convert.ToInt32(_sqlReader["gender"]));
                    tbPhoneNumber.Text = _sqlReader["phoneNumber"].ToString();
                    tbTableNumber.Text = _sqlReader["tableNumber"].ToString();
                    tbLNumber.Text = _sqlReader["lNumber"].ToString();
                    dtpDateBirthday.Value = Convert.ToDateTime(_sqlReader["dateBirthday"]);
                    dtpPrimaryDate.Value = Convert.ToDateTime(_sqlReader["primaryDate"]);
                    tbPlaceBirthday.Text = _sqlReader["placeBirthday"].ToString();
                    mtbNIS.Text = _sqlReader["numberNIS"].ToString();
                    tbDamages.Text = _sqlReader["damages"].ToString();
                    dtpStart.Value = Convert.ToDateTime(_sqlReader["start"]);
                    dtpStartThis.Value = Convert.ToDateTime(_sqlReader["startThis"]);
                    var primaryId = Convert.ToInt32(_sqlReader["primaryId"]);
                    var primaryOrderId = Convert.ToInt32(_sqlReader["primaryOrderId"]);
                    var positionId = Convert.ToInt32(_sqlReader["positionId"]);
                    var positionOrderId = Convert.ToInt32(_sqlReader["positionOrderId"]);
                    _sqlReader.Close();
                    //чтение primaryId
                    _sqlCommand = new SqlCommand("SELECT [name] FROM [Primary] " +
                                                 "WHERE [id]=@id", _sqlConnection);
                    _sqlCommand.Parameters.AddWithValue("id", primaryId);
                    _sqlReader = _sqlCommand.ExecuteReader();
                    _sqlReader.Read();
                    cbPrimary.Text = _sqlReader["name"].ToString();
                    _sqlReader.Close();
                    //чтение primaryOrderId
                    _sqlCommand = new SqlCommand("SELECT [name], [number], [date] FROM [Orders] " +
                                                 "WHERE [id]=@id", _sqlConnection);
                    _sqlCommand.Parameters.AddWithValue("id", primaryOrderId);
                    _sqlReader = _sqlCommand.ExecuteReader();
                    _sqlReader.Read();
                    tbPrimaryOrderName.Text = _sqlReader["name"].ToString();
                    tbPrimaryOrderNumber.Text = _sqlReader["number"].ToString();
                    dtpPrimaryOrderDate.Text = _sqlReader["date"].ToString();
                    _sqlReader.Close();
                    //чтение positionId
                    lbCurrentPositionId.SelectedIndex = lbCurrentPositionId.FindString(positionId.ToString());
                    if (lbCurrentPosition.Items.Count > lbCurrentPositionId.SelectedIndex)
                        lbCurrentPosition.SelectedIndex = lbCurrentPositionId.SelectedIndex;

                    _sqlCommand = new SqlCommand("SELECT [parent1], [parent2], [parent3], [parent4], [name] " +
                                                 "FROM [Positions] WHERE [id]=@id", _sqlConnection);
                    _sqlCommand.Parameters.AddWithValue("id", positionId);
                    _sqlReader = _sqlCommand.ExecuteReader();
                    _sqlReader.Read();
                    var t = _sqlReader["parent1"].ToString().Trim();
                    tbCurrentPosition.Text = (_sqlReader["parent1"].ToString().Trim() == ""
                                                 ? ""
                                                 : _sqlReader["parent1"].ToString().Trim() + " | ") +
                                             (_sqlReader["parent2"].ToString().Trim() == ""
                                                 ? ""
                                                 : _sqlReader["parent2"].ToString().Trim() + " | ") +
                                             (_sqlReader["parent3"].ToString().Trim() == ""
                                                 ? ""
                                                 : _sqlReader["parent3"].ToString().Trim() + " | ") +
                                             (_sqlReader["parent4"].ToString().Trim() == ""
                                                 ? ""
                                                 : _sqlReader["parent4"].ToString().Trim() + " | ") +
                                             _sqlReader["name"];
                    _sqlReader.Close();

                    //чтение positionOrderId
                    _sqlCommand = new SqlCommand("SELECT [name], [number], [date] FROM [Orders] " +
                                                 "WHERE [id]=@id", _sqlConnection);
                    _sqlCommand.Parameters.AddWithValue("id", positionOrderId);
                    _sqlReader = _sqlCommand.ExecuteReader();
                    _sqlReader.Read();
                    tbPositionOrderName.Text = _sqlReader["name"].ToString();
                    tbPositionOrderNumber.Text = _sqlReader["number"].ToString();
                    dtpPositionOrderDate.Text = _sqlReader["date"].ToString();
                    _sqlReader.Close();

                    //чтение контракта человека
                    _sqlCommand =
                        new SqlCommand(
                            "SELECT [slaveStart], [slaveEnd], [orderId] FROM [Slaves]" +
                            " WHERE [peopleId]=@peopleId ORDER BY [slaveStart] DESC", _sqlConnection);
                    _sqlCommand.Parameters.AddWithValue("peopleId", _peopleId);
                    _sqlReader = _sqlCommand.ExecuteReader();
                    if (_sqlReader.HasRows)
                    {
                        _sqlReader.Read();
                        dtpSlaveStart.Text = _sqlReader["slaveStart"].ToString();
                        dtpSlaveEnd.Text = _sqlReader["slaveEnd"].ToString();
                        var slaveOrderId = _sqlReader["orderId"].ToString();
                        _sqlReader.Close();
                        //чтение slaveOrderId
                        _sqlCommand = new SqlCommand("SELECT [name], [number], [date] FROM [Orders] " +
                                                     "WHERE [id]=@id", _sqlConnection);
                        _sqlCommand.Parameters.AddWithValue("id", slaveOrderId);
                        _sqlReader = _sqlCommand.ExecuteReader();
                        _sqlReader.Read();
                        tbSlaveOrderName.Text = _sqlReader["name"].ToString();
                        tbSlaveOrderNumber.Text = _sqlReader["number"].ToString();
                        dtpSlaveOrderDate.Text = _sqlReader["date"].ToString();
                    }

                    _sqlReader.Close();

                    //чтение образования человека
                    dgvEditEducations.Rows.Clear();
                    _sqlReader = null;
                    _sqlCommand =
                        new SqlCommand(
                            "SELECT [name], [year], [special] FROM [Educations] WHERE [peopleId]=@peopleId",
                            _sqlConnection);
                    _sqlCommand.Parameters.AddWithValue("peopleId", _peopleId);
                    _sqlReader = _sqlCommand.ExecuteReader();
                    while (_sqlReader.Read())
                        dgvEditEducations.Rows.Add(
                            _sqlReader["name"].ToString(),
                            _sqlReader["year"].ToString(),
                            _sqlReader["special"].ToString());

                    _sqlReader.Close();

                    //чтение состава семьи человека
                    dgvEditFamily.Rows.Clear();
                    _sqlReader = null;
                    _sqlCommand =
                        new SqlCommand(
                            "SELECT [position], [name], [dateBirthday] FROM [Family] WHERE [peopleId]=@peopleId",
                            _sqlConnection);
                    _sqlCommand.Parameters.AddWithValue("peopleId", _peopleId);
                    _sqlReader = _sqlCommand.ExecuteReader();
                    while (_sqlReader.Read())
                        dgvEditFamily.Rows.Add(
                            _sqlReader["position"].ToString(),
                            _sqlReader["name"].ToString(),
                            _sqlReader["dateBirthday"].ToString());

                    _sqlReader.Close();

                    //чтение послужного списка человека
                    dgvEditHistory.Rows.Clear();
                    _sqlReader = null;
                    _sqlCommand =
                        new SqlCommand(
                            "SELECT [name], [orderId] FROM [History] WHERE [peopleId]=@peopleId",
                            _sqlConnection);
                    _sqlCommand.Parameters.AddWithValue("peopleId", _peopleId);
                    _sqlReader = _sqlCommand.ExecuteReader();
                    while (_sqlReader.Read())
                        dgvEditHistory.Rows.Add(
                            _sqlReader["name"].ToString(),
                            _sqlReader["orderId"].ToString());

                    _sqlReader.Close();

                    for (var i = 0; i < dgvEditHistory.RowCount - 1; i++)
                    {
                        _sqlReader = null;
                        _sqlCommand =
                            new SqlCommand(
                                "SELECT [name], [number], [date] FROM [Orders] WHERE [id]=@id",
                                _sqlConnection);
                        _sqlCommand.Parameters.AddWithValue("id", Convert.ToInt32(dgvEditHistory[1, i].Value));
                        _sqlReader = _sqlCommand.ExecuteReader();
                        _sqlReader.Read();
                        dgvEditHistory[1, i].Value = _sqlReader["name"].ToString();
                        dgvEditHistory[2, i].Value = _sqlReader["number"].ToString();
                        dgvEditHistory[3, i].Value = Convert.ToDateTime(_sqlReader["date"]).ToString("dd.MM.yyyy");
                        _sqlReader.Close();
                    }

                    //чтение боевых действий человека
                    dgvEditBattlefields.Rows.Clear();
                    _sqlReader = null;
                    _sqlCommand =
                        new SqlCommand(
                            "SELECT [name], [dateText] FROM [Battlefields] WHERE [peopleId]=@peopleId",
                            _sqlConnection);
                    _sqlCommand.Parameters.AddWithValue("peopleId", _peopleId);
                    _sqlReader = _sqlCommand.ExecuteReader();
                    while (_sqlReader.Read())
                        dgvEditBattlefields.Rows.Add(
                            _sqlReader["name"].ToString(),
                            _sqlReader["dateText"].ToString());

                    _sqlReader.Close();

                    //чтение медалей человека
                    dgvEditMedals.Rows.Clear();
                    _sqlReader = null;
                    _sqlCommand =
                        new SqlCommand(
                            "SELECT [name], [orderId] FROM [Medals] WHERE [peopleId]=@peopleId",
                            _sqlConnection);
                    _sqlCommand.Parameters.AddWithValue("peopleId", _peopleId);
                    _sqlReader = _sqlCommand.ExecuteReader();
                    while (_sqlReader.Read())
                        dgvEditMedals.Rows.Add(
                            _sqlReader["name"].ToString(),
                            _sqlReader["orderId"].ToString());

                    _sqlReader.Close();

                    //чтение медальных приказов
                    for (var i = 0; i < dgvEditMedals.RowCount - 1; i++)
                    {
                        _sqlReader = null;
                        _sqlCommand =
                            new SqlCommand(
                                "SELECT [name], [number], [date] FROM [Orders] WHERE [id]=@id",
                                _sqlConnection);
                        _sqlCommand.Parameters.AddWithValue("id", Convert.ToInt32(dgvEditMedals[1, i].Value));
                        _sqlReader = _sqlCommand.ExecuteReader();
                        _sqlReader.Read();
                        dgvEditMedals[1, i].Value = _sqlReader["name"].ToString();
                        dgvEditMedals[2, i].Value = _sqlReader["number"].ToString();
                        dgvEditMedals[3, i].Value = Convert.ToDateTime(_sqlReader["date"]).ToString("dd.MM.yyyy");
                        _sqlReader.Close();
                    }

                    //чтение выслуги
                    dgvMemoryCalend.Rows.Clear();
                    dgvMemoryJump.Rows.Clear();
                    dgvMemoryFar.Rows.Clear();
                    dgvMemoryCivilian.Rows.Clear();
                    _sqlReader = null;
                    _sqlCommand =
                        new SqlCommand(
                            "SELECT [type], [dateStart], [dateEnd], [isLast], [variety], [text] " +
                            "FROM [Memory] WHERE [peopleId]=@peopleId ORDER BY [dateStart]",
                            _sqlConnection);
                    _sqlCommand.Parameters.AddWithValue("peopleId", _peopleId);
                    _sqlReader = _sqlCommand.ExecuteReader();
                    while (_sqlReader.Read())
                        switch (Convert.ToInt32(_sqlReader["type"]))
                        {
                            case 0:
                                dgvMemoryCalend.Rows.Add(
                                    Convert.ToDateTime(_sqlReader["dateStart"]).ToString("dd.MM.yyyy"),
                                    Convert.ToDateTime(Convert.ToBoolean(_sqlReader["isLast"])
                                        ? DateTime.Now
                                        : _sqlReader["dateEnd"]).ToString("dd.MM.yyyy"));
                                break;
                            case 1:
                                dgvMemoryJump.Rows.Add(
                                    Convert.ToDateTime(_sqlReader["dateStart"]).ToString("dd.MM.yyyy"),
                                    Convert.ToDateTime(Convert.ToBoolean(_sqlReader["isLast"])
                                        ? DateTime.Now
                                        : _sqlReader["dateEnd"]).ToString("dd.MM.yyyy"));
                                break;
                            case 2:
                                dgvMemoryFar.Rows.Add(
                                    Convert.ToDateTime(_sqlReader["dateStart"]).ToString("dd.MM.yyyy"),
                                    Convert.ToDateTime(Convert.ToBoolean(_sqlReader["isLast"])
                                        ? DateTime.Now
                                        : _sqlReader["dateEnd"]).ToString("dd.MM.yyyy"),
                                    Convert.ToSingle(_sqlReader["variety"]));
                                break;
                            case 3:
                                dgvMemoryCivilian.Rows.Add(
                                    Convert.ToDateTime(_sqlReader["dateStart"]).ToString("dd.MM.yyyy"),
                                    Convert.ToDateTime(Convert.ToBoolean(_sqlReader["isLast"])
                                        ? DateTime.Now
                                        : _sqlReader["dateEnd"]).ToString("dd.MM.yyyy"),
                                    Convert.ToSingle(_sqlReader["variety"]),
                                    _sqlReader["text"].ToString());
                                break;
                        }

                    _sqlReader.Close();
                    MemoryChange("tbMemoryCalend");
                    MemoryChange("tbMemoryJump");
                    MemoryChange("tbMemoryFar");
                    MemoryChange("tbMemoryCivilian");

                    //загрузка взысканий
                    _sqlCommand =
                        new SqlCommand(
                            "SELECT [peopleId], [type], [text], [orderId], [resultOrderId] " +
                            "FROM [Fails] WHERE [peopleId]=@peopleId ORDER BY [orderId]",
                            _sqlConnection);
                    _sqlCommand.Parameters.AddWithValue("peopleId", _peopleId);
                    _sqlReader = _sqlCommand.ExecuteReader();
                    while (_sqlReader.Read())
                    {
                        _fails.Add(new[]
                        {
                            _sqlReader["type"].ToString(),
                            _sqlReader["text"].ToString(),
                            _sqlReader["orderId"].ToString(), "", "",
                            _sqlReader["resultId"].ToString(), "", ""
                        });
                    }

                    _sqlReader.Close();

                    foreach (var t1 in _fails)
                    {
                        _sqlCommand =
                            new SqlCommand(
                                "SELECT [name], [number], [date] FROM [Orders] WHERE [id]=@id",
                                _sqlConnection);
                        _sqlCommand.Parameters.AddWithValue("id", Convert.ToInt32(t1[2]));
                        _sqlReader = _sqlCommand.ExecuteReader();
                        _sqlReader.Read();
                        t1[2] = _sqlReader["name"].ToString();
                        t1[3] = _sqlReader["number"].ToString();
                        t1[4] = Convert.ToDateTime(_sqlReader["date"]).ToString("dd.MM.yyyy");
                        _sqlReader.Close();

                        if (Convert.ToInt32(t1[5]) == 2442)
                        {
                            t1[5] = "";
                            t1[6] = "";
                            t1[7] = "";
                        }
                        else
                        {
                            _sqlCommand =
                                new SqlCommand(
                                    "SELECT [name], [number], [date] FROM [Orders] WHERE [id]=@id",
                                    _sqlConnection);
                            _sqlCommand.Parameters.AddWithValue("id", Convert.ToInt32(t1[5]));
                            _sqlReader = _sqlCommand.ExecuteReader();
                            _sqlReader.Read();
                            t1[5] = _sqlReader["name"].ToString();
                            t1[6] = _sqlReader["number"].ToString();
                            t1[7] = Convert.ToDateTime(_sqlReader["date"]).ToString("dd.MM.yyyy");
                            _sqlReader.Close();
                        }
                    }

                    dgvFails.Rows.Clear();
                    foreach (var t1 in _fails)
                        dgvFails.Rows.Add(t1[0], t1[1], t1[2],
                            t1[3], t1[4], t1[5], t1[6], t1[7]);
                }
                finally
                {
                    _sqlReader?.Close();
                }
            }
            //добавление человека
            else
            {
                //tcEditPeople.TabPages[4].Enabled = false;
                bEditBack.Text = "       Отмена";
                bEditNext.Text = "       Добавить человека";
                bEditBack2.Text = bEditBack.Text;
                bEditBack3.Text = bEditBack.Text;
                bEditBack4.Text = bEditBack.Text;
                bEditNext2.Text = bEditNext.Text;
                bEditNext3.Text = bEditNext.Text;
                bEditNext4.Text = bEditNext.Text;
                Text = "Добавление нового человека";
                bEditNext.Enabled = true;
                bEditNext2.Enabled = true;
                bEditNext3.Enabled = true;
                bEditNext4.Enabled = true;
                tbFio0.Text = "";
                tbFio1.Text = "";
                tbFio2.Text = "";
                ChangeGender(0);
                tbPhoneNumber.Text = "79";
                tbLNumber.Text = "";
                cbPrimary.SelectedIndex = 0;
                tbPrimaryOrderName.Text = "ком.83 одшбр";
                tbPrimaryOrderNumber.Text = "1";
                tbPositionOrderName.Text = "ком.83 одшбр";
                tbPositionOrderNumber.Text = "1";
                lbCurrentPosition.SelectedIndex = -1;
                tbPlaceBirthday.Text = "";
                mtbNIS.Text = "          ";
                tbDamages.Text = "";
                dtpSlaveEnd.Value = DateTime.Today;
                tbSlaveOrderName.Text = "ком.83 одшбр";
                tbSlaveOrderNumber.Text = "1";
                dgvEditEducations.Rows.Clear();
                dgvEditFamily.Rows.Clear();
                dgvEditBattlefields.Rows.Clear();
                dgvEditMedals.Rows.Clear();
                dgvMemoryCalend.Rows.Clear();
                dgvMemoryJump.Rows.Clear();
                dgvMemoryFar.Rows.Clear();
                dgvMemoryCivilian.Rows.Clear();
                dgvFails.Rows.Clear();
            }

            _colorSchema = colorSchema == 0 ? 1 : 0;
            bFlash.Image = _colorSchema == 0 ? Resources.unsun : Resources.sun;
            ColorSchemaSet(this);
            _colorSchema = colorSchema;
        }

        private void LbCurrentPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbCurrentPositionId.Items.Count > lbCurrentPosition.SelectedIndex)
                lbCurrentPositionId.SelectedIndex = lbCurrentPosition.SelectedIndex;
            tbCurrentPosition.Text = lbCurrentPosition.SelectedIndex > -1 ? lbCurrentPosition.Text : "";
            pCurrentVus.Visible = _userName == "Admin";
            if (pCurrentVus.Visible && tbCurrentPosition.Text != "")
            {
                _sqlCommand =
                    new SqlCommand(
                        "SELECT [fullName], [vus], [primaryId], [tarif] FROM [Positions] WHERE [id]=@id",
                        _sqlConnection);
                _sqlCommand.Parameters.AddWithValue("id", Convert.ToInt32(lbCurrentPositionId.Text));
                _sqlReader = _sqlCommand.ExecuteReader();
                _sqlReader.Read();
                tbCurrentFullName.Text = _sqlReader["fullName"].ToString();
                tbCurrentVus.Text = _sqlReader["vus"].ToString();
                tbCurrentPrimaryId.Text = _sqlReader["primaryId"].ToString();
                tbCurrentTarif.Text = _sqlReader["tarif"].ToString();
                _sqlReader.Close();
                _sqlCommand =
                    new SqlCommand(
                        "SELECT [name] FROM [Primary] WHERE [id]=@id", _sqlConnection);
                _sqlCommand.Parameters.AddWithValue("id", Convert.ToInt32(tbCurrentPrimaryId.Text));
                _sqlReader = _sqlCommand.ExecuteReader();
                _sqlReader.Read();
                tbCurrentPrimaryId.Text = _sqlReader["name"].ToString();
                _sqlReader.Close();
            }
            else
            {
                tbCurrentFullName.Text = "";
                tbCurrentVus.Text = "";
                tbCurrentPrimaryId.Text = "";
                tbCurrentTarif.Text = "";
            }
        }

        private void EditDo()
        {
            if (tEdited.Enabled) return;
            if (tbFio0.Text.Length > 0 && tbFio1.Text.Length > 0 &&
                tbLNumber.Text.Length > 6 && tbLNumber.Text.Length < 10)
            {
                int primaryId;
                int primaryOrderId;
                int positionId;
                int positionOrderId;
                int slaveOrderId;

                //узнаем primaryId выбранного звания
                if (cbPrimary.SelectedIndex > -1)
                {
                    _sqlCommand = new SqlCommand("SELECT [id] FROM [Primary] WHERE [name]=@name",
                        _sqlConnection);
                    _sqlCommand.Parameters.AddWithValue("name", cbPrimary.Items[cbPrimary.SelectedIndex]);
                    _sqlReader = _sqlCommand.ExecuteReader();
                    _sqlReader.Read();
                    primaryId = Convert.ToInt32(_sqlReader["id"]);
                }
                else
                {
                    primaryId = -1;
                }

                _sqlReader?.Close();
                //узнаем primaryOrderId (вставка order если надо)
                primaryOrderId = FindOrderId(tbPrimaryOrderName.Text, tbPrimaryOrderNumber.Text,
                    dtpPrimaryOrderDate.Value);

                var t = lbCurrentPositionId.SelectedIndex;
                //узнаем positionId выбранной должности
                if (lbCurrentPositionId.SelectedIndex > -1 && lbCurrentPositionId.Items.Count > lbCurrentPosition.SelectedIndex + 2)
                {
                    lbCurrentPositionId.SelectedIndex = lbCurrentPosition.SelectedIndex;
                    positionId = Convert.ToInt32(lbCurrentPositionId.Text);
                }
                else
                {
                    positionId = 2360;
                }

                //узнаем positionOrderId (вставка order если надо)
                positionOrderId = FindOrderId(tbPositionOrderName.Text, tbPositionOrderNumber.Text,
                    dtpPositionOrderDate.Value);

                //узнаем slaveOrderId (вставка order если надо)
                slaveOrderId = FindOrderId(tbSlaveOrderName.Text, tbSlaveOrderNumber.Text, dtpSlaveOrderDate.Value);

                //проверка всех остальных значений и найденных ранее
                if (primaryId == -1 || tbFio0.Text == "" || tbFio1.Text == "" || tbLNumber.Text == "" ||
                    dtpDateBirthday.Value == dtpDateBirthday.MinDate)
                {
                    MessageBox.Show("Некорректные данные, проверьте правильность ввода", "Ошибка",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
                //успех
                else
                {
                    //запрет клика на пару секунд
                    tEdited.Enabled = true;
                    bEditBack.Text = "          Назад";
                    bEditBack2.Text = bEditBack.Text;
                    bEditBack3.Text = bEditBack.Text;
                    bEditBack4.Text = bEditBack.Text;
                    bEditNext2.Text = bEditNext.Text;
                    bEditNext3.Text = bEditNext.Text;
                    bEditNext4.Text = bEditNext.Text;
                    bEditNext.Enabled = false;
                    bEditNext2.Enabled = false;
                    bEditNext3.Enabled = false;
                    bEditNext4.Enabled = false;
                    //очернение всех полей
                    foreach (var textBox in tabPage1.Controls.OfType<TextBox>())
                        textBox.ForeColor = Color.FromArgb(240,240,240);
                    if (bEditNext.Text == "          Обновить данные")
                    {
                        //обновление данных человека
                        bEditNext.Text = "          Данные обновлены...";
                        bEditNext2.Text = bEditNext.Text;
                        bEditNext3.Text = bEditNext.Text;
                        bEditNext4.Text = bEditNext.Text;
                        _sqlCommand =
                            new SqlCommand(
                                "UPDATE [Peoples] SET [fio0]=@fio0, [fio1]=@fio1, [fio2]=@fio2, [gender]=@gender, " +
                                "[phoneNumber]=@phoneNumber, [lNumber]=@lNumber, " +
                                "[dateBirthday]=@dateBirthday, [placeBirthday]=@placeBirthday, [primaryId]=@primaryId," +
                                "[primaryOrderId]=@primaryOrderId, [positionId]=@positionId, [positionOrderId]=@positionOrderId," +
                                "[start]=@start, [startThis]=@startThis, [action]=@action, [actionUser]=@actionUser " +
                                "WHERE [id]=@id", _sqlConnection);
                        _sqlCommand.Parameters.AddWithValue("id", _peopleId);
                        _sqlCommand.Parameters.AddWithValue("fio0", tbFio0.Text);
                        _sqlCommand.Parameters.AddWithValue("fio1", tbFio1.Text);
                        _sqlCommand.Parameters.AddWithValue("fio2", tbFio2.Text);
                        _sqlCommand.Parameters.AddWithValue("gender", cbGender.SelectedIndex);
                        _sqlCommand.Parameters.AddWithValue("phoneNumber", tbPhoneNumber.Text);
                        _sqlCommand.Parameters.AddWithValue("tableNumber", tbTableNumber.Text);
                        _sqlCommand.Parameters.AddWithValue("lNumber", tbLNumber.Text);
                        _sqlCommand.Parameters.AddWithValue("dateBirthday", dtpDateBirthday.Value);
                        _sqlCommand.Parameters.AddWithValue("primaryDate", dtpPrimaryDate.Value);
                        _sqlCommand.Parameters.AddWithValue("placeBirthday", tbPlaceBirthday.Text);
                        _sqlCommand.Parameters.AddWithValue("damages", tbDamages.Text);
                        _sqlCommand.Parameters.AddWithValue("numberNIS", mtbNIS.Text);
                        _sqlCommand.Parameters.AddWithValue("primaryId", primaryId);
                        _sqlCommand.Parameters.AddWithValue("primaryOrderId", primaryOrderId);
                        _sqlCommand.Parameters.AddWithValue("positionId", positionId);
                        _sqlCommand.Parameters.AddWithValue("positionOrderId", positionOrderId);
                        _sqlCommand.Parameters.AddWithValue("start", dtpStart.Value);
                        _sqlCommand.Parameters.AddWithValue("startThis", dtpStartThis.Value);
                        _sqlCommand.Parameters.AddWithValue("action", DateTime.Now);
                        _sqlCommand.Parameters.AddWithValue("actionUser", _userName);
                        _sqlCommand.ExecuteNonQuery();

                        //удаление контракта человека
                        _sqlCommand =
                            new SqlCommand(
                                "DELETE FROM [Slaves] WHERE [peopleId]=@peopleId",
                                _sqlConnection);
                        _sqlCommand.Parameters.AddWithValue("peopleId", _peopleId);
                        _sqlCommand.ExecuteNonQuery();

                        //удаление образования человека
                        _sqlCommand =
                            new SqlCommand(
                                "DELETE FROM [Educations] WHERE [peopleId]=@peopleId",
                                _sqlConnection);
                        _sqlCommand.Parameters.AddWithValue("peopleId", _peopleId);
                        _sqlCommand.ExecuteNonQuery();

                        //удаление состава семьи человека
                        _sqlCommand =
                            new SqlCommand(
                                "DELETE FROM [Family] WHERE [peopleId]=@peopleId",
                                _sqlConnection);
                        _sqlCommand.Parameters.AddWithValue("peopleId", _peopleId);
                        _sqlCommand.ExecuteNonQuery();

                        //удаление послужного списка
                        _sqlCommand =
                            new SqlCommand(
                                "DELETE FROM [History] WHERE [peopleId]=@peopleId",
                                _sqlConnection);
                        _sqlCommand.Parameters.AddWithValue("peopleId", _peopleId);
                        _sqlCommand.ExecuteNonQuery();

                        //удаление боевых действий человека
                        _sqlCommand =
                            new SqlCommand(
                                "DELETE FROM [Battlefields] WHERE [peopleId]=@peopleId",
                                _sqlConnection);
                        _sqlCommand.Parameters.AddWithValue("peopleId", _peopleId);
                        _sqlCommand.ExecuteNonQuery();

                        //удаление медалей человека
                        _sqlCommand =
                            new SqlCommand(
                                "DELETE FROM [Medals] WHERE [peopleId]=@peopleId",
                                _sqlConnection);
                        _sqlCommand.Parameters.AddWithValue("peopleId", _peopleId);
                        _sqlCommand.ExecuteNonQuery();

                        //удаление выслуги человека
                        _sqlCommand =
                            new SqlCommand(
                                "DELETE FROM [Memory] WHERE [peopleId]=@peopleId",
                                _sqlConnection);
                        _sqlCommand.Parameters.AddWithValue("peopleId", _peopleId);
                        _sqlCommand.ExecuteNonQuery();

                        //удаление взысканий человека
                        if (dgvFails.Enabled)
                        {
                            _sqlCommand =
                                new SqlCommand(
                                    "DELETE FROM [Fails] WHERE [peopleId]=@peopleId",
                                    _sqlConnection);
                            _sqlCommand.Parameters.AddWithValue("peopleId", _peopleId);
                            _sqlCommand.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        //добавление человека
                        bEditNext.Text = "         Человек добавлен...";
                        bEditNext2.Text = bEditNext.Text;
                        bEditNext3.Text = bEditNext.Text;
                        bEditNext4.Text = bEditNext.Text;
                        _sqlCommand =
                            new SqlCommand(
                                "INSERT INTO [Peoples] (fio0, fio1, fio2, gender, phoneNumber, lNumber, dateBirthday, " +
                                "placeBirthday, primaryId, primaryOrderId, positionId, positionOrderId, start, startThis, " +
                                "action, actionUser) VALUES (@fio0, @fio1, @fio2, @gender, @phoneNumber, @lNumber, @dateBirthday," +
                                "@placeBirthday, @primaryId, @primaryOrderId, @positionId, @positionOrderId, @start, @startThis, " +
                                "@action, @actionUser)",
                                _sqlConnection);
                        _sqlCommand.Parameters.AddWithValue("fio0", tbFio0.Text);
                        _sqlCommand.Parameters.AddWithValue("fio1", tbFio1.Text);
                        _sqlCommand.Parameters.AddWithValue("fio2", tbFio2.Text);
                        _sqlCommand.Parameters.AddWithValue("gender", cbGender.SelectedIndex);
                        _sqlCommand.Parameters.AddWithValue("phoneNumber", tbPhoneNumber.Text);
                        _sqlCommand.Parameters.AddWithValue("tableNumber", tbTableNumber.Text);
                        _sqlCommand.Parameters.AddWithValue("lNumber", tbLNumber.Text);
                        _sqlCommand.Parameters.AddWithValue("dateBirthday", dtpDateBirthday.Value);
                        _sqlCommand.Parameters.AddWithValue("primaryDate", dtpPrimaryDate.Value);
                        _sqlCommand.Parameters.AddWithValue("placeBirthday", tbPlaceBirthday.Text);
                        _sqlCommand.Parameters.AddWithValue("damages", tbDamages.Text);
                        _sqlCommand.Parameters.AddWithValue("numberNIS", mtbNIS.Text);
                        _sqlCommand.Parameters.AddWithValue("primaryId", primaryId);
                        _sqlCommand.Parameters.AddWithValue("primaryOrderId", primaryOrderId);
                        _sqlCommand.Parameters.AddWithValue("positionId", positionId);
                        _sqlCommand.Parameters.AddWithValue("positionOrderId", positionOrderId);
                        _sqlCommand.Parameters.AddWithValue("start", dtpStart.Value);
                        _sqlCommand.Parameters.AddWithValue("startThis", dtpStartThis.Value);
                        _sqlCommand.Parameters.AddWithValue("action", DateTime.Now);
                        _sqlCommand.Parameters.AddWithValue("actionUser", _userName);
                        _sqlCommand.ExecuteNonQuery();
                        _sqlReader.Close();

                        //чтение peopleId только что добавленного
                        _sqlCommand = new SqlCommand(
                            "SELECT [id] FROM [Peoples] WHERE [fio0]=@fio0 AND " +
                            "[fio1]=@fio1 AND [fio2]=@fio2 AND [lNumber]=@lNumber",
                            _sqlConnection);
                        _sqlCommand.Parameters.AddWithValue("fio0", tbFio0.Text);
                        _sqlCommand.Parameters.AddWithValue("fio1", tbFio1.Text);
                        _sqlCommand.Parameters.AddWithValue("fio2", tbFio2.Text);
                        _sqlCommand.Parameters.AddWithValue("lNumber", tbLNumber.Text);
                        _sqlReader = _sqlCommand.ExecuteReader();
                        _sqlReader.Read();
                        _peopleId = Convert.ToInt32(_sqlReader["id"]);
                        _sqlReader.Close();
                    }

                    //добавление контракта
                    _sqlCommand =
                        new SqlCommand(
                            "INSERT INTO [Slaves] (peopleId, slaveStart, slaveEnd, orderId, " +
                            "action, actionUser) VALUES (@peopleId, @slaveStart, @slaveEnd, @orderId, " +
                            "@action, @actionUser)",
                            _sqlConnection);
                    _sqlCommand.Parameters.AddWithValue("peopleId", _peopleId);
                    _sqlCommand.Parameters.AddWithValue("slaveStart", dtpSlaveStart.Value);
                    _sqlCommand.Parameters.AddWithValue("slaveEnd", dtpSlaveEnd.Value);
                    _sqlCommand.Parameters.AddWithValue("orderId", slaveOrderId);
                    _sqlCommand.Parameters.AddWithValue("action", DateTime.Now);
                    _sqlCommand.Parameters.AddWithValue("actionUser", _userName);
                    _sqlCommand.ExecuteNonQuery();

                    //добавляем education
                    for (var i = 0; i < dgvEditEducations.RowCount - 1; i++)
                    {
                        //если строка неправильная, то пропускаем ее
                        if (dgvEditEducations.Rows[i].Cells[0].Value == null ||
                            dgvEditEducations.Rows[i].Cells[1].Value == null ||
                            dgvEditEducations.Rows[i].Cells[0].Value.ToString() == "" ||
                            dgvEditEducations.Rows[i].Cells[1].Value.ToString() == "") continue;
                        _sqlCommand =
                            new SqlCommand(
                                "INSERT INTO [Educations] (peopleId, name, year, special, " +
                                "action, actionUser) VALUES (@peopleId, @name, @year, @special, " +
                                "@action, @actionUser)",
                                _sqlConnection);
                        _sqlCommand.Parameters.AddWithValue("peopleId", _peopleId);
                        _sqlCommand.Parameters.AddWithValue("name",
                            dgvEditEducations.Rows[i].Cells[0].Value);
                        _sqlCommand.Parameters.AddWithValue("year",
                            dgvEditEducations.Rows[i].Cells[1].Value);
                        _sqlCommand.Parameters.AddWithValue("special",
                            dgvEditEducations.Rows[i].Cells[2].Value);
                        _sqlCommand.Parameters.AddWithValue("action", DateTime.Now);
                        _sqlCommand.Parameters.AddWithValue("actionUser", _userName);
                        _sqlCommand.ExecuteNonQuery();
                    }

                    //добавление состава семьи из таблицы
                    for (var i = 0; i < dgvEditFamily.RowCount - 1; i++)
                    {
                        //если строка неправильная, то пропускаем ее
                        if (dgvEditFamily.Rows[i].Cells[0].Value == null ||
                            dgvEditFamily.Rows[i].Cells[1].Value == null ||
                            dgvEditFamily.Rows[i].Cells[2].Value == null ||
                            dgvEditFamily.Rows[i].Cells[0].Value.ToString() == "" ||
                            dgvEditFamily.Rows[i].Cells[1].Value.ToString() == "" ||
                            dgvEditFamily.Rows[i].Cells[2].Value.ToString() == "") continue;
                        _sqlCommand =
                            new SqlCommand(
                                "INSERT INTO [Family] (peopleId, position, name, " +
                                "dateBirthday, action, actionUser) VALUES (@peopleId, @position," +
                                " @name, @dateBirthday, @action, @actionUser)",
                                _sqlConnection);
                        _sqlCommand.Parameters.AddWithValue("peopleId", _peopleId);
                        _sqlCommand.Parameters.AddWithValue("position",
                            dgvEditFamily.Rows[i].Cells[0].Value);
                        _sqlCommand.Parameters.AddWithValue("name",
                            dgvEditFamily.Rows[i].Cells[1].Value);
                        _sqlCommand.Parameters.AddWithValue("dateBirthday",
                            Convert.ToDateTime(dgvEditFamily.Rows[i].Cells[2].Value));
                        _sqlCommand.Parameters.AddWithValue("action", DateTime.Now);
                        _sqlCommand.Parameters.AddWithValue("actionUser", _userName);
                        _sqlCommand.ExecuteNonQuery();
                    }

                    //добавление послужного списка из таблицы
                    for (var i = 0; i < dgvEditHistory.RowCount - 1; i++)
                    {
                        //если строка неправильная, то пропускаем ее
                        if (dgvEditHistory.Rows[i].Cells[0].Value == null ||
                            dgvEditHistory.Rows[i].Cells[1].Value == null ||
                            dgvEditHistory.Rows[i].Cells[2].Value == null ||
                            dgvEditHistory.Rows[i].Cells[3].Value == null ||
                            dgvEditHistory.Rows[i].Cells[0].Value.ToString() == "" ||
                            dgvEditHistory.Rows[i].Cells[1].Value.ToString() == "" ||
                            dgvEditHistory.Rows[i].Cells[2].Value.ToString() == "" ||
                            dgvEditHistory.Rows[i].Cells[3].Value.ToString() == "") continue;
                        //узнаем orderId (вставка order если надо)
                        var orderId = FindOrderId(dgvEditHistory.Rows[i].Cells[1].Value.ToString(),
                            dgvEditHistory.Rows[i].Cells[2].Value.ToString(),
                            Convert.ToDateTime(dgvEditHistory.Rows[i].Cells[3].Value));
                        _sqlCommand =
                            new SqlCommand(
                                "INSERT INTO [History] (peopleId, name, " +
                                "orderId, action, actionUser) VALUES (@peopleId, @name, " +
                                "@orderId, @action, @actionUser)",
                                _sqlConnection);
                        _sqlCommand.Parameters.AddWithValue("peopleId", _peopleId);
                        _sqlCommand.Parameters.AddWithValue("name",
                            dgvEditHistory.Rows[i].Cells[0].Value);
                        _sqlCommand.Parameters.AddWithValue("orderId", orderId);
                        _sqlCommand.Parameters.AddWithValue("action", DateTime.Now);
                        _sqlCommand.Parameters.AddWithValue("actionUser", _userName);
                        _sqlCommand.ExecuteNonQuery();
                    }

                    //добавляем battlefields
                    for (var i = 0; i < dgvEditBattlefields.RowCount - 1; i++)
                    {
                        //если строка неправильная, то пропускаем ее
                        if (dgvEditBattlefields.Rows[i].Cells[0].Value == null ||
                            dgvEditBattlefields.Rows[i].Cells[1].Value == null ||
                            dgvEditBattlefields.Rows[i].Cells[0].Value.ToString() == "" ||
                            dgvEditBattlefields.Rows[i].Cells[1].Value.ToString() == "") continue;
                        _sqlCommand =
                            new SqlCommand(
                                "INSERT INTO [Battlefields] (peopleId, name, dateText, " +
                                "action, actionUser) VALUES (@peopleId, @name, @dateText, " +
                                "@action, @actionUser)",
                                _sqlConnection);
                        _sqlCommand.Parameters.AddWithValue("peopleId", _peopleId);
                        _sqlCommand.Parameters.AddWithValue("name",
                            dgvEditBattlefields.Rows[i].Cells[0].Value);
                        _sqlCommand.Parameters.AddWithValue("dateText",
                            dgvEditBattlefields.Rows[i].Cells[1].Value);
                        _sqlCommand.Parameters.AddWithValue("action", DateTime.Now);
                        _sqlCommand.Parameters.AddWithValue("actionUser", _userName);
                        _sqlCommand.ExecuteNonQuery();
                    }

                    //добавляем medals
                    for (var i = 0; i < dgvEditMedals.RowCount - 1; i++)
                    {
                        //если строка неправильная, то пропускаем ее
                        if (dgvEditMedals.Rows[i].Cells[0].Value == null ||
                            dgvEditMedals.Rows[i].Cells[1].Value == null ||
                            dgvEditMedals.Rows[i].Cells[2].Value == null ||
                            dgvEditMedals.Rows[i].Cells[3].Value == null ||
                            dgvEditMedals.Rows[i].Cells[0].Value.ToString() == "" ||
                            dgvEditMedals.Rows[i].Cells[1].Value.ToString() == "" ||
                            dgvEditMedals.Rows[i].Cells[2].Value.ToString() == "" ||
                            dgvEditMedals.Rows[i].Cells[3].Value.ToString() == "") continue;
                        //узнаем orderId (вставка order если надо)
                        var orderId = FindOrderId(dgvEditMedals.Rows[i].Cells[1].Value.ToString(),
                            dgvEditMedals.Rows[i].Cells[2].Value.ToString(),
                            Convert.ToDateTime(dgvEditMedals.Rows[i].Cells[3].Value));
                        _sqlCommand =
                            new SqlCommand(
                                "INSERT INTO [Medals] (peopleId, name, orderId, " +
                                "action, actionUser) VALUES (@peopleId, @name, @orderId, " +
                                "@action, @actionUser)",
                                _sqlConnection);
                        _sqlCommand.Parameters.AddWithValue("peopleId", _peopleId);
                        _sqlCommand.Parameters.AddWithValue("name",
                            dgvEditMedals.Rows[i].Cells[0].Value);
                        _sqlCommand.Parameters.AddWithValue("orderId", orderId);
                        _sqlCommand.Parameters.AddWithValue("action", DateTime.Now);
                        _sqlCommand.Parameters.AddWithValue("actionUser", _userName);
                        _sqlCommand.ExecuteNonQuery();
                    }

                    //добавляем выслугу календари
                    for (var i = 0; i < dgvMemoryCalend.RowCount - 1; i++)
                    {
                        if (dgvMemoryCalend.Rows[i].Cells[0].Value == null ||
                            dgvMemoryCalend.Rows[i].Cells[0].Value.ToString() == "") continue;
                        _sqlCommand =
                            new SqlCommand(
                                "INSERT INTO [Memory] (peopleId, type, dateStart, dateEnd, isLast, variety, text, " +
                                "action, actionUser) VALUES (@peopleId, @type, @dateStart, @dateEnd, @isLast, @variety, @text, " +
                                "@action, @actionUser)",
                                _sqlConnection);
                        _sqlCommand.Parameters.AddWithValue("peopleId", _peopleId);
                        _sqlCommand.Parameters.AddWithValue("type", 0);
                        _sqlCommand.Parameters.AddWithValue("dateStart",
                            dgvMemoryCalend.Rows[i].Cells[0].Value);
                        if (dgvMemoryCalend.Rows[i].Cells[1].Value == null ||
                            dgvMemoryCalend.Rows[i].Cells[1].Value.ToString() == "")
                        {
                            _sqlCommand.Parameters.AddWithValue("dateEnd",
                                DateTime.Now);
                            _sqlCommand.Parameters.AddWithValue("isLast", 1);
                        }
                        else
                        {
                            _sqlCommand.Parameters.AddWithValue("dateEnd",
                                dgvMemoryCalend.Rows[i].Cells[1].Value);
                            _sqlCommand.Parameters.AddWithValue("isLast", 0);
                        }

                        _sqlCommand.Parameters.AddWithValue("variety", 1.0);
                        _sqlCommand.Parameters.AddWithValue("text", "");
                        _sqlCommand.Parameters.AddWithValue("action", DateTime.Now);
                        _sqlCommand.Parameters.AddWithValue("actionUser", _userName);
                        _sqlCommand.ExecuteNonQuery();
                    }

                    //добавляем выслугу прыжковая
                    for (var i = 0; i < dgvMemoryJump.RowCount - 1; i++)
                    {
                        //если строка неправильная, то пропускаем ее
                        if (dgvMemoryJump.Rows[i].Cells[0].Value == null ||
                            dgvMemoryJump.Rows[i].Cells[0].Value.ToString() == "") continue;
                        _sqlCommand =
                            new SqlCommand(
                                "INSERT INTO [Memory] (peopleId, type, dateStart, dateEnd, isLast, variety, text, " +
                                "action, actionUser) VALUES (@peopleId, @type, @dateStart, @dateEnd, @isLast, @variety, @text, " +
                                "@action, @actionUser)",
                                _sqlConnection);
                        _sqlCommand.Parameters.AddWithValue("peopleId", _peopleId);
                        _sqlCommand.Parameters.AddWithValue("type", 1);
                        _sqlCommand.Parameters.AddWithValue("dateStart",
                            dgvMemoryJump.Rows[i].Cells[0].Value);
                        if (dgvMemoryJump.Rows[i].Cells[1].Value == null ||
                            dgvMemoryJump.Rows[i].Cells[1].Value.ToString() == "")
                        {
                            _sqlCommand.Parameters.AddWithValue("dateEnd",
                                DateTime.Now);
                            _sqlCommand.Parameters.AddWithValue("isLast", 1);
                        }
                        else
                        {
                            _sqlCommand.Parameters.AddWithValue("dateEnd",
                                dgvMemoryJump.Rows[i].Cells[1].Value);
                            _sqlCommand.Parameters.AddWithValue("isLast", 0);
                        }

                        _sqlCommand.Parameters.AddWithValue("variety", 1.5);
                        _sqlCommand.Parameters.AddWithValue("text", "");
                        _sqlCommand.Parameters.AddWithValue("action", DateTime.Now);
                        _sqlCommand.Parameters.AddWithValue("actionUser", _userName);
                        _sqlCommand.ExecuteNonQuery();
                    }

                    //добавляем выслугу отдаленка
                    for (var i = 0; i < dgvMemoryFar.RowCount - 1; i++)
                    {
                        //если строка неправильная, то пропускаем ее
                        if (dgvMemoryFar.Rows[i].Cells[0].Value == null ||
                            dgvMemoryFar.Rows[i].Cells[2].Value == null ||
                            dgvMemoryFar.Rows[i].Cells[0].Value.ToString() == "" ||
                            dgvMemoryFar.Rows[i].Cells[2].Value.ToString() == "") continue;
                        _sqlCommand =
                            new SqlCommand(
                                "INSERT INTO [Memory] (peopleId, type, dateStart, dateEnd, isLast, variety, text, " +
                                "action, actionUser) VALUES (@peopleId, @type, @dateStart, @dateEnd, @isLast, @variety, @text, " +
                                "@action, @actionUser)",
                                _sqlConnection);
                        _sqlCommand.Parameters.AddWithValue("peopleId", _peopleId);
                        _sqlCommand.Parameters.AddWithValue("type", 2);
                        _sqlCommand.Parameters.AddWithValue("dateStart",
                            dgvMemoryFar.Rows[i].Cells[0].Value);
                        if (dgvMemoryFar.Rows[i].Cells[1].Value == null ||
                            dgvMemoryFar.Rows[i].Cells[1].Value.ToString() == "")
                        {
                            _sqlCommand.Parameters.AddWithValue("dateEnd",
                                DateTime.Now);
                            _sqlCommand.Parameters.AddWithValue("isLast", 1);
                        }
                        else
                        {
                            _sqlCommand.Parameters.AddWithValue("dateEnd",
                                dgvMemoryFar.Rows[i].Cells[1].Value);
                            _sqlCommand.Parameters.AddWithValue("isLast", 0);
                        }

                        _sqlCommand.Parameters.AddWithValue("variety",
                            Convert.ToDouble(dgvMemoryFar.Rows[i].Cells[2].Value));
                        _sqlCommand.Parameters.AddWithValue("text", "");
                        _sqlCommand.Parameters.AddWithValue("action", DateTime.Now);
                        _sqlCommand.Parameters.AddWithValue("actionUser", _userName);
                        _sqlCommand.ExecuteNonQuery();
                    }

                    //добавляем выслугу гражданская
                    for (var i = 0; i < dgvMemoryCivilian.RowCount - 1; i++)
                    {
                        //если строка неправильная, то пропускаем ее
                        if (dgvMemoryCivilian.Rows[i].Cells[0].Value == null ||
                            dgvMemoryCivilian.Rows[i].Cells[2].Value == null ||
                            dgvMemoryCivilian.Rows[i].Cells[3].Value == null ||
                            dgvMemoryCivilian.Rows[i].Cells[0].Value.ToString() == "" ||
                            dgvMemoryCivilian.Rows[i].Cells[2].Value.ToString() == "" ||
                            dgvMemoryCivilian.Rows[i].Cells[3].Value.ToString() == "") continue;
                        _sqlCommand =
                            new SqlCommand(
                                "INSERT INTO [Memory] (peopleId, type, dateStart, dateEnd, isLast, variety, text, " +
                                "action, actionUser) VALUES (@peopleId, @type, @dateStart, @dateEnd, @isLast, @variety, @text, " +
                                "@action, @actionUser)",
                                _sqlConnection);
                        _sqlCommand.Parameters.AddWithValue("peopleId", _peopleId);
                        _sqlCommand.Parameters.AddWithValue("type", 3);
                        _sqlCommand.Parameters.AddWithValue("dateStart",
                            dgvMemoryCivilian.Rows[i].Cells[0].Value);
                        if (dgvMemoryCivilian.Rows[i].Cells[1].Value == null ||
                            dgvMemoryCivilian.Rows[i].Cells[1].Value.ToString() == "")
                        {
                            _sqlCommand.Parameters.AddWithValue("dateEnd",
                                DateTime.Now);
                            _sqlCommand.Parameters.AddWithValue("isLast", 1);
                        }
                        else
                        {
                            _sqlCommand.Parameters.AddWithValue("dateEnd",
                                dgvMemoryCivilian.Rows[i].Cells[1].Value);
                            _sqlCommand.Parameters.AddWithValue("isLast", 0);
                        }

                        _sqlCommand.Parameters.AddWithValue("variety",
                            Convert.ToDouble(dgvMemoryCivilian.Rows[i].Cells[2].Value));
                        _sqlCommand.Parameters.AddWithValue("text",
                            dgvMemoryCivilian.Rows[i].Cells[3].Value);
                        _sqlCommand.Parameters.AddWithValue("action", DateTime.Now);
                        _sqlCommand.Parameters.AddWithValue("actionUser", _userName);
                        _sqlCommand.ExecuteNonQuery();
                    }

                    //добавляем взыскания
                    if (dgvFails.Enabled)
                    {
                        for (var i = 0; i < dgvFails.RowCount - 1; i++)
                        {
                            //если строка неправильная, то пропускаем ее
                            if (dgvFails.Rows[i].Cells[0].Value == null ||
                                dgvFails.Rows[i].Cells[1].Value == null ||
                                dgvFails.Rows[i].Cells[2].Value == null ||
                                dgvFails.Rows[i].Cells[3].Value == null ||
                                dgvFails.Rows[i].Cells[4].Value == null ||
                                dgvFails.Rows[i].Cells[0].Value.ToString() == "" ||
                                dgvFails.Rows[i].Cells[1].Value.ToString() == "" ||
                                dgvFails.Rows[i].Cells[2].Value.ToString() == "" ||
                                dgvFails.Rows[i].Cells[3].Value.ToString() == "" ||
                                dgvFails.Rows[i].Cells[4].Value.ToString() == "") continue;
                            //узнаем orderId (вставка order если надо)
                            var orderId = FindOrderId(dgvFails.Rows[i].Cells[2].Value.ToString(),
                                dgvFails.Rows[i].Cells[3].Value.ToString(),
                                Convert.ToDateTime(dgvFails.Rows[i].Cells[4].Value));
                            //узнаем resultOrderId (вставка order если надо)
                            int resultOrderId;
                            if (dgvFails.Rows[i].Cells[5].Value == null ||
                                dgvFails.Rows[i].Cells[6].Value == null ||
                                dgvFails.Rows[i].Cells[7].Value == null ||
                                dgvFails.Rows[i].Cells[5].Value.ToString() == "" ||
                                dgvFails.Rows[i].Cells[6].Value.ToString() == "" ||
                                dgvFails.Rows[i].Cells[7].Value.ToString() == "")
                                resultOrderId = 2442;
                            else
                                resultOrderId = FindOrderId(dgvFails.Rows[i].Cells[2].Value.ToString(),
                                    dgvFails.Rows[i].Cells[3].Value.ToString(),
                                    Convert.ToDateTime(dgvFails.Rows[i].Cells[4].Value));
                            _sqlCommand =
                                new SqlCommand(
                                    "INSERT INTO [Fails] (peopleId, type, text, orderId, resultOrderId, " +
                                    "action, actionUser) VALUES (@peopleId, @type, @text, @orderId, @resultOrderId, " +
                                    "@action, @actionUser)", _sqlConnection);
                            _sqlCommand.Parameters.AddWithValue("peopleId", _peopleId);
                            _sqlCommand.Parameters.AddWithValue("type",
                                dgvEditMedals.Rows[i].Cells[0].Value);
                            _sqlCommand.Parameters.AddWithValue("text",
                                dgvEditMedals.Rows[i].Cells[1].Value);
                            _sqlCommand.Parameters.AddWithValue("orderId", orderId);
                            _sqlCommand.Parameters.AddWithValue("resultOrderId", resultOrderId);
                            _sqlCommand.Parameters.AddWithValue("action", DateTime.Now);
                            _sqlCommand.Parameters.AddWithValue("actionUser", _userName);
                            _sqlCommand.ExecuteNonQuery();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Некорректные данные", "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void BEditNext_Click(object sender, EventArgs e)
        {
            EditDo();
        }

        private void BCurrentPositionChange_Click(object sender, EventArgs e)
        {
            pCurrentPosition.Top = tbpCurrentPosition.Top;
            pCurrentPosition.Height = Height - tbpCurrentPosition.Top;
            pCurrentPosition.Visible = true;
            lbCurrentPosition.Select();
        }

        private void ChoosePositionHide()
        {
            pCurrentPosition.Visible = false;
        }

        private void BChoosePositionHide_Click(object sender, EventArgs e)
        {
            ChoosePositionHide();
        }

        private void BEditBack_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void TaskDo(bool print)
        {
            //врио может быть
            bool[] nok = {false, false};
            var t9 = Convert.ToInt32(lbPeoplesId.Items[cbNshName.SelectedIndex]);
            if (Convert.ToInt32(cbNshName.Tag) != Convert.ToInt32(lbPeoplesId.Items[cbNshName.SelectedIndex]))
                nok[0] = true;
            if (Convert.ToInt32(cbNokName.Tag) != Convert.ToInt32(lbPeoplesId.Items[cbNokName.SelectedIndex]))//cbNokName.SelectedIndex)
                nok[1] = true;
            var linkToFile = "";
            switch (lbTaskName.Text)
            {
                case "Справка о прохождении службы":
                    linkToFile = @"C:\temp\Прохождение службы.docx";
                    new GeneratedClassCurrent().CreatePackage(linkToFile,
                        _sqlConnectionString, _peopleId, nok,
                        Convert.ToInt32(lbPeoplesId.Items[cbNshName.SelectedIndex]),
                        Convert.ToInt32(lbPeoplesId.Items[cbNokName.SelectedIndex]), tbTaskDestination.Text);
                    break;
                case "Справка о составе семьи":
                    linkToFile = @"C:\temp\Состав семьи.docx";
                    new GeneratedClassAge().CreatePackage(linkToFile,
                        _sqlConnectionString, _peopleId, nok,
                        Convert.ToInt32(lbPeoplesId.Items[cbNshName.SelectedIndex]),
                        Convert.ToInt32(lbPeoplesId.Items[cbNokName.SelectedIndex]), tbTaskDestination.Text);
                    break;
                case "Послужной список":
                    linkToFile = @"C:\temp\Послужной список.docx";
                    new GeneratedClassHistory().CreatePackage(linkToFile,
                        _sqlConnectionString, _peopleId, nok,
                        Convert.ToInt32(lbPeoplesId.Items[cbNokName.SelectedIndex]));
                    break;
                case "Справка о выслуге лет":
                    linkToFile = @"C:\temp\Выслуга лет.docx";
                    new GeneratedClassMemory().CreatePackage(linkToFile,
                        _sqlConnectionString, _peopleId, nok,
                        Convert.ToInt32(lbPeoplesId.Items[cbNshName.SelectedIndex]),
                        Convert.ToInt32(lbPeoplesId.Items[cbNokName.SelectedIndex]), tbTaskDestination.Text,
                        dtpTaskDate.Value, tbMemoryCalend.Text, tbMemoryAll.Text);
                    break;
                case "Справка-объективка":
                    linkToFile = @"C:\temp\Справка-объективка.docx";
                    new GeneratedClassAll().CreatePackage(linkToFile,
                        _sqlConnectionString, _peopleId);
                    break;
            }

            if (print)
            {
                //печать
                new Process {StartInfo = {Verb = "Print", FileName = linkToFile}}.Start();
            }
            else
                    Process.Start(linkToFile);
        }

        private void BTaskSimple_Click(object sender, EventArgs e)
        {
            TaskDo(true);
        }

        private void TbFio0_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (sender is TextBox &&
                    (((TextBox) sender).Name == "tbFio0" ||
                     ((TextBox) sender).Name == "tbFio1" ||
                     ((TextBox) sender).Name == "tbFio2"))
                    lFioPeople.Text = tbFio0.Text + @" " + tbFio1.Text + @" " + tbFio2.Text;
            }
            catch (Exception)
            {
                // ignored
            }
            finally
            {
                GoDecline();
            }

            //покраснение связанного label
            //var textBoxName = "";
            //textBoxName = ((TextBox) sender).Name;
            //textBoxName = textBoxName.Replace("tb", "l");
            //textBoxName = textBoxName.Replace("dtp", "l");
            //var textBoxControl = Controls.Find(textBoxName, true);
            //if (textBoxControl.Length > 0)
            //textBoxControl[0].ForeColor = Color.Red;
        }

        /// <summary>
        /// Перевод массива времени в строку для отображения
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private static string DateRangeToString(IList<int> date)
        {
            var yearsOut = date[0] < 5 ? " г. " : " л. ";
            return date[0] + yearsOut + date[1] + " м. " + date[2] + " д.";
        }

        /// <summary>
        /// Сложение массивов времени
        /// </summary>
        /// <param name="dateFirst"></param>
        /// <param name="dateSecond"></param>
        /// <returns></returns>
        private int[] DateRangePlus(int[] dateFirst, int[] dateSecond)
        {
            int[] dateRange1 = {dateFirst[0], dateFirst[1], dateFirst[2]};
            for (var i = 0; i < dateRange1.Length; i++)
                dateRange1[i] += dateSecond[i];
            if (dateRange1[2] > 30)
            {
                dateRange1[2] = dateRange1[2] - 30;
                dateRange1[1]++;
            }

            if (dateRange1[1] > 11)
            {
                dateRange1[1] = dateRange1[1] - 12;
                dateRange1[0]++;
            }

            return dateRange1;
        }

        /// <summary>
        /// Вычитание массивов времени
        /// </summary>
        /// <param name="dateFirst"></param>
        /// <param name="dateSecond"></param>
        /// <returns></returns>
        private int[] DateRangeMinus(int[] dateFirst, int[] dateSecond)
        {
            int[] dateRange1 = {dateFirst[0], dateFirst[1], dateFirst[2]};
            for (var i = 0; i < dateRange1.Length; i++)
                dateRange1[i] -= dateSecond[i];
            if (dateRange1[2] < 0)
            {
                dateRange1[2] = dateRange1[2] + 30;
                dateRange1[1]--;
            }

            if (dateRange1[1] < 0)
            {
                dateRange1[1] = dateRange1[1] + 12;
                dateRange1[0]--;
            }

            return dateRange1;
        }

        /// <summary>
        /// Умножение массива времени на коэффициент
        /// </summary>
        /// <param name="date"></param>
        /// <param name="variety"></param>
        /// <param name="moreThenHalf"></param>
        /// <returns></returns>
        private int[] DateRangeVariety(int[] date, float variety, bool moreThenHalf)
        {
            int[] dateRange1 = {date[0], date[1], date[2]};
            int[] intAdd = {0, 0};
            if (Convert.ToInt32(dateRange1[0] * variety * 10) != Convert.ToInt32(dateRange1[0] * variety) * 10)
            {
                intAdd[0] = 6;
                var floatAdd = moreThenHalf ? (float) 0.5 : (float) -0.5;
                dateRange1[0] = Convert.ToInt32(dateRange1[0] * variety - floatAdd);
            }
            else
            {
                dateRange1[0] = Convert.ToInt32(dateRange1[0] * variety);
            }

            if (Convert.ToInt32(dateRange1[1] * variety * 10) != Convert.ToInt32(dateRange1[1] * variety) * 10)
            {
                intAdd[1] = 15;
                var floatAdd = moreThenHalf ? (float) 0.5 : (float) -0.5;
                dateRange1[1] = Convert.ToInt32(dateRange1[1] * variety - floatAdd) + intAdd[0];
            }
            else
            {
                dateRange1[1] = Convert.ToInt32(dateRange1[1] * variety) + intAdd[0];
            }

            dateRange1[2] = Convert.ToInt32(dateRange1[2] * variety) + intAdd[1];
            if (dateRange1[2] > 30)
            {
                dateRange1[2] = dateRange1[2] - 30;
                dateRange1[1]++;
            }

            if (dateRange1[1] > 11)
            {
                dateRange1[1] = dateRange1[1] - 12;
                dateRange1[0]++;
            }

            return dateRange1;
        }

        //перевод двух дат в массив времени
        private int[] DateStartEndToRange(DateTime dateStart, DateTime dateEnd)
        {
            var years = dateEnd.Year - dateStart.Year;
            var months = dateEnd.Month - dateStart.Month;
            var days = dateEnd.Day - dateStart.Day;
            if (days < 0)
            {
                days += 30;
                months--;
            }

            if (months < 0)
            {
                months += 12;
                years--;
            }

            int[] returnInt = {years, months, days};
            return returnInt;
        }

        private void TbFio0_TextChanged(object sender, DataGridViewCellEventArgs e)
        {
            var textBoxName = ((DataGridView) sender).Name;
            textBoxName = textBoxName.Replace("dgv", "l");
            var textBoxControl = Controls.Find(textBoxName, true);
            if (textBoxControl.Length > 0)
                textBoxControl[0].ForeColor = Color.Red;
            //подсчет выслуги лет
            textBoxName = textBoxName.Replace("lMemory", "tbMemory");
            textBoxControl = Controls.Find(textBoxName, true);
            if (textBoxControl.Length <= 0) return;
            MemoryChange(textBoxName);
            if (textBoxName != "tbMemoryCivilian")
                MemoryAllChange();
        }

        private void MemoryChange(string memoryName)
        {
            var textBoxControl = Controls.Find(memoryName, true);
            switch (memoryName)
            {
                case "tbMemoryCalend":
                    try
                    {
                        _dateRange[0][0] = 0;
                        _dateRange[0][1] = 0;
                        _dateRange[0][2] = 0;
                        for (var i = 0; i < dgvMemoryCalend.RowCount - 1; i++)
                        {
                            if (dgvMemoryCalend[0, i].Value.ToString() == "") continue;
                            var dateStart = Convert.ToDateTime(dgvMemoryCalend[0, i].Value);
                            DateTime dateEnd;
                            try
                            {
                                dateEnd = dgvMemoryCalend[1, i].Value.ToString() == ""
                                    ? DateTime.Now
                                    : Convert.ToDateTime(dgvMemoryCalend[1, i].Value);
                            }
                            catch
                            {
                                dateEnd = DateTime.Now;
                            }

                            _dateRange[0] = DateRangePlus(_dateRange[0], DateStartEndToRange(dateStart, dateEnd));
                        }

                        textBoxControl[0].Text = DateRangeToString(_dateRange[0]);
                    }
                    catch
                    {
                        textBoxControl[0].Text = "";
                    }

                    break;
                case "tbMemoryJump":
                    try
                    {
                        _dateRange[1][0] = 0;
                        _dateRange[1][1] = 0;
                        _dateRange[1][2] = 0;
                        for (var i = 0; i < dgvMemoryJump.RowCount - 1; i++)
                        {
                            if (dgvMemoryJump[0, i].Value.ToString() == "") continue;
                            var dateStart = Convert.ToDateTime(dgvMemoryJump[0, i].Value);
                            DateTime dateEnd;
                            try
                            {
                                dateEnd = dgvMemoryJump[1, i].Value.ToString() == ""
                                    ? DateTime.Now
                                    : Convert.ToDateTime(dgvMemoryJump[1, i].Value);
                            }
                            catch
                            {
                                dateEnd = DateTime.Now;
                            }

                            _dateRange[1] = DateRangePlus(_dateRange[1], DateStartEndToRange(dateStart, dateEnd));
                        }

                        textBoxControl[0].Text = DateRangeToString(_dateRange[1]);
                    }
                    catch
                    {
                        textBoxControl[0].Text = "";
                    }

                    break;
                case "tbMemoryFar":
                    try
                    {
                        _dateRange[2][0] = 0;
                        _dateRange[2][1] = 0;
                        _dateRange[2][2] = 0;
                        for (var i = 0; i < dgvMemoryFar.RowCount - 1; i++)
                        {
                            if (dgvMemoryFar[0, i].Value.ToString() == "") continue;
                            var dateStart = Convert.ToDateTime(dgvMemoryFar[0, i].Value);
                            DateTime dateEnd;
                            try
                            {
                                dateEnd = dgvMemoryFar[1, i].Value.ToString() == ""
                                    ? DateTime.Now
                                    : Convert.ToDateTime(dgvMemoryFar[1, i].Value);
                            }
                            catch
                            {
                                dateEnd = DateTime.Now;
                            }

                            float variety;
                            try
                            {
                                variety = Convert.ToSingle(dgvMemoryFar[2, i].Value);
                            }
                            catch
                            {
                                variety = 1;
                            }

                            _dateRange[2] = DateRangePlus(_dateRange[2],
                                DateRangeVariety(
                                    DateStartEndToRange(dateStart, dateEnd), variety, true));
                        }

                        textBoxControl[0].Text = DateRangeToString(_dateRange[2]);
                    }
                    catch
                    {
                        textBoxControl[0].Text = "";
                    }

                    break;
                case "tbMemoryCivilian":
                    try
                    {
                        _dateRange[3][0] = 0;
                        _dateRange[3][1] = 0;
                        _dateRange[3][2] = 0;
                        for (var i = 0; i < dgvMemoryCivilian.RowCount - 1; i++)
                        {
                            if (dgvMemoryCivilian[0, i].Value.ToString() == "") continue;
                            var dateStart = Convert.ToDateTime(dgvMemoryCivilian[0, i].Value);
                            DateTime dateEnd;
                            try
                            {
                                dateEnd = dgvMemoryCivilian[1, i].Value.ToString() == ""
                                    ? DateTime.Now
                                    : Convert.ToDateTime(dgvMemoryCivilian[1, i].Value);
                            }
                            catch
                            {
                                dateEnd = DateTime.Now;
                            }

                            float variety;
                            try
                            {
                                variety = Convert.ToSingle(dgvMemoryCivilian[2, i].Value);
                            }
                            catch
                            {
                                variety = 1;
                            }

                            _dateRange[3] = DateRangePlus(_dateRange[3],
                                DateRangeVariety(
                                    DateStartEndToRange(dateStart, dateEnd), variety, true));
                        }

                        textBoxControl[0].Text = DateRangeToString(_dateRange[3]);

                        MemoryAllChange();
                    }
                    catch
                    {
                        textBoxControl[0].Text = "";
                    }

                    break;
            }
        }

        private void MemoryAllChange()
        {
            _dataStartEnd.Clear();
            _dataVariety.Clear();
            _dataLast = 0;
            for (var i = 0; i < dgvMemoryJump.RowCount - 1; i++)
            {
                if (dgvMemoryJump[0, i].Value.ToString() == "") continue;
                var dateStart = Convert.ToDateTime(dgvMemoryJump[0, i].Value);
                DateTime dateEnd;
                try
                {
                    dateEnd = dgvMemoryJump[1, i].Value.ToString() == ""
                        ? DateTime.Now
                        : Convert.ToDateTime(dgvMemoryJump[1, i].Value);
                }
                catch
                {
                    dateEnd = DateTime.Now;
                }

                _dataStartEnd.Add(new[] {dateStart, dateEnd});
                _dataVariety.Add((float) 1.5);
                _dataLast++;
            }

            for (var i = 0; i < dgvMemoryFar.RowCount - 1; i++)
            {
                if (dgvMemoryFar[0, i].Value.ToString() == "") continue;
                var dateStart = Convert.ToDateTime(dgvMemoryFar[0, i].Value);
                DateTime dateEnd;
                try
                {
                    dateEnd = dgvMemoryFar[1, i].Value.ToString() == ""
                        ? DateTime.Now
                        : Convert.ToDateTime(dgvMemoryFar[1, i].Value);
                }
                catch
                {
                    dateEnd = DateTime.Now;
                }

                float variety;
                try
                {
                    variety = Convert.ToSingle(dgvMemoryFar[2, i].Value);
                }
                catch
                {
                    variety = 1;
                }

                _dataStartEnd.Add(new[] {dateStart, dateEnd});
                _dataVariety.Add(variety);
                _dataLast++;
            }

            for (var i = 0; i < dgvMemoryCivilian.RowCount - 1; i++)
            {
                if (dgvMemoryCivilian[0, i].Value.ToString() == "" &&
                    dgvMemoryCivilian[3, i].Value.ToString() != "боевые") continue;
                var dateStart = Convert.ToDateTime(dgvMemoryCivilian[0, i].Value);
                DateTime dateEnd;
                try
                {
                    dateEnd = dgvMemoryCivilian[1, i].Value.ToString() == ""
                        ? DateTime.Now
                        : Convert.ToDateTime(dgvMemoryCivilian[1, i].Value);
                }
                catch
                {
                    dateEnd = DateTime.Now;
                }

                float variety;
                try
                {
                    variety = Convert.ToSingle(dgvMemoryCivilian[2, i].Value);
                }
                catch
                {
                    variety = 1;
                }

                _dataStartEnd.Add(new[] {dateStart, dateEnd});
                _dataVariety.Add(variety);
                _dataLast++;
            }

            for (var i = 0; i < _dataLast; i++)
            for (var j = 0; j < _dataLast; j++)
                DataDoubleCorrect(i, j);

            int[] dataRange = {0, 0, 0};
            int[] dataRangeOne = {0, 0, 0};

            for (var i = 0; i < _dataLast; i++)
            {
                dataRange = DateRangePlus(
                    dataRange, DateRangeVariety(
                        DateStartEndToRange(
                            _dataStartEnd[i][0], _dataStartEnd[i][1]), _dataVariety[i], true));
                dataRangeOne = DateRangePlus(
                    dataRangeOne, DateStartEndToRange(
                        _dataStartEnd[i][0], _dataStartEnd[i][1]));
            }

            dataRange = DateRangeMinus(dataRange, dataRangeOne);
            dataRange = DateRangePlus(dataRange, _dateRange[0]);
            tbMemoryAll.Text = DateRangeToString(dataRange);
        }

        private void MemoryMoneyChange()
        {
            //ополовинивание прыжковой выслуги
            int[] dataRange;
            if (tbMemoryJump.Text != "")
            {
                dataRange = DateRangeVariety(_dateRange[1], (float) 1.5, true);
                dataRange = DateRangeMinus(dataRange, _dateRange[1]);
            }
            else
            {
                dataRange = new[]
                {
                    0, 0, 0
                };
            }

            //сложение с обычной
            tbMemoryMoney.Text = DateRangeToString(DateRangePlus(dataRange, _dateRange[0]));
        }

        private void DataDoubleCorrect(int first, int second)
        {
            if (first == second || first > _dataLast || second > _dataLast ||
                _dataLast > _dataStartEnd.Count) return;
            if (_dataStartEnd[first][0] < _dataStartEnd[second][0])
            {
                if (_dataStartEnd[first][1] < _dataStartEnd[second][1])
                {
                    if (_dataStartEnd[first][1] > _dataStartEnd[second][0])
                    {
                        if (_dataVariety[first] > _dataVariety[second])
                            //1a
                            _dataStartEnd[second][0] = _dataStartEnd[first][1];
                        else
                            //1b
                            _dataStartEnd[first][1] = _dataStartEnd[second][0];
                    }
                }
                else
                {
                    if (_dataVariety[first] >= _dataVariety[second])
                    {
                        //3a
                        _dataStartEnd.RemoveAt(second);
                        _dataVariety.RemoveAt(second);
                        _dataLast--;
                    }
                    else
                    {
                        //3b
                        _dataStartEnd[first][1] = _dataStartEnd[second][0];
                        _dataStartEnd.Add(new[] {_dataStartEnd[second][1], _dataStartEnd[first][1]});
                        _dataVariety.Add(_dataVariety[first]);
                        _dataLast++;
                    }
                }
            }
            else
            {
                if (_dataStartEnd[first][1] < _dataStartEnd[second][1])
                {
                    if (_dataVariety[first] > _dataVariety[second])
                    {
                        //4a
                        _dataStartEnd[second][1] = _dataStartEnd[first][0];
                        _dataStartEnd.Add(new[] {_dataStartEnd[first][1], _dataStartEnd[second][1]});
                        _dataVariety.Add(_dataVariety[second]);
                        _dataLast++;
                    }
                    else
                    {
                        //4b
                        _dataStartEnd.RemoveAt(first);
                        _dataVariety.RemoveAt(first);
                        _dataLast--;
                    }
                }
                else
                {
                    if (_dataStartEnd[second][1] > _dataStartEnd[first][0])
                    {
                        if (_dataVariety[first] > _dataVariety[second])
                            //2a
                            _dataStartEnd[second][1] = _dataStartEnd[first][0];
                        else
                            //2b
                            _dataStartEnd[first][0] = _dataStartEnd[second][1];
                    }
                }
            }

            if (_dataStartEnd.Count > first && _dataStartEnd[first][0] == _dataStartEnd[first][1])
            {
                _dataStartEnd.RemoveAt(first);
                _dataVariety.RemoveAt(first);
                _dataLast--;
            }

            if (_dataStartEnd.Count > second && _dataStartEnd[second][0] == _dataStartEnd[second][1])
            {
                _dataStartEnd.RemoveAt(second);
                _dataVariety.RemoveAt(second);
                _dataLast--;
            }
        }

        private void TEdited_Tick(object sender, EventArgs e)
        {
            bEditNext.Text = "          Обновить данные";
            bEditNext2.Text = bEditNext.Text;
            bEditNext3.Text = bEditNext.Text;
            bEditNext4.Text = bEditNext.Text;
            bEditNext.Enabled = true;
            bEditNext2.Enabled = true;
            bEditNext3.Enabled = true;
            bEditNext4.Enabled = true;
            tEdited.Enabled = false;
        }

        private void TbMemoryJump_TextChanged(object sender, EventArgs e)
        {
            //ополовинивание прыжковой выслуги
            var dataRange = _dateRange[1];
            dataRange = DateRangeVariety(dataRange, (float) 1.5, true);
            tbMemoryJumpIn.Text = tbMemoryJump.Text != ""
                ? DateRangeToString(DateRangeMinus(dataRange, _dateRange[1]))
                : "";
        }

        private void TbMemoryFar_TextChanged(object sender, EventArgs e)
        {
            if (tbMemoryFar.Text == "" || tbMemoryFar.Text == "0 г. 0 м. 0 д.")
                tbMemoryFarIn.Text = "";
            else
                //чистая прибавка к выслуге
                try
                {
                    int[] dateRange = {0, 0, 0};
                    for (var i = 0; i < dgvMemoryFar.RowCount - 1; i++)
                    {
                        if (dgvMemoryFar[0, i].Value.ToString() == "") continue;
                        var dateStart = Convert.ToDateTime(dgvMemoryFar[0, i].Value);
                        DateTime dateEnd;
                        try
                        {
                            dateEnd = dgvMemoryFar[1, i].Value.ToString() == ""
                                ? DateTime.Now
                                : Convert.ToDateTime(dgvMemoryFar[1, i].Value);
                        }
                        catch
                        {
                            dateEnd = DateTime.Now;
                        }

                        dateRange = DateRangePlus(dateRange,
                            DateStartEndToRange(dateStart, dateEnd));
                    }

                    tbMemoryFarIn.Text = DateRangeToString(DateRangeMinus(_dateRange[2], dateRange));
                }
                catch
                {
                    tbMemoryFarIn.Text = "";
                }
        }

        private void TbMemoryCivilian_TextChanged(object sender, EventArgs e)
        {
            if (tbMemoryCivilian.Text == "" || tbMemoryCivilian.Text == "0 г. 0 м. 0 д.")
            {
                tbMemoryCivilianIn.Text = "";
                tbMemoryCivilianIn2.Text = "";
            }
            else
            {
                //подсчет боевой выслуги
                try
                {
                    int[] dateRange = {0, 0, 0};
                    for (var i = 0; i < dgvMemoryCivilian.RowCount - 1; i++)
                    {
                        if (dgvMemoryCivilian[0, i].Value.ToString() == "" ||
                            dgvMemoryCivilian[2, i].Value.ToString() != "боевые") continue;
                        var dateStart = Convert.ToDateTime(dgvMemoryCivilian[0, i].Value);
                        DateTime dateEnd;
                        try
                        {
                            dateEnd = dgvMemoryCivilian[1, i].Value.ToString() == ""
                                ? DateTime.Now
                                : Convert.ToDateTime(dgvMemoryCivilian[1, i].Value);
                        }
                        catch
                        {
                            dateEnd = DateTime.Now;
                        }

                        float variety;
                        try
                        {
                            variety = Convert.ToSingle(dgvMemoryCivilian[2, i].Value);
                        }
                        catch
                        {
                            variety = 3;
                        }

                        dateRange = DateRangePlus(dateRange,
                            DateRangeVariety(
                                DateStartEndToRange(dateStart, dateEnd), variety, true));
                    }

                    tbMemoryCivilianIn.Text = DateRangeToString(dateRange);
                }
                catch
                {
                    tbMemoryCivilianIn.Text = "";
                }

                //подсчет гражданской выслуги
                try
                {
                    int[] dateRange = {0, 0, 0};
                    for (var i = 0; i < dgvMemoryCivilian.RowCount - 1; i++)
                    {
                        if (dgvMemoryCivilian[0, i].Value.ToString() == "" ||
                            dgvMemoryCivilian[2, i].Value.ToString() != "гражд") continue;
                        var dateStart = Convert.ToDateTime(dgvMemoryCivilian[0, i].Value);
                        DateTime dateEnd;
                        try
                        {
                            dateEnd = dgvMemoryCivilian[1, i].Value.ToString() == ""
                                ? DateTime.Now
                                : Convert.ToDateTime(dgvMemoryCivilian[1, i].Value);
                        }
                        catch
                        {
                            dateEnd = DateTime.Now;
                        }

                        float variety;
                        try
                        {
                            variety = Convert.ToSingle(dgvMemoryCivilian[2, i].Value);
                        }
                        catch
                        {
                            variety = 3;
                        }

                        dateRange = DateRangePlus(dateRange,
                            DateRangeVariety(
                                DateStartEndToRange(dateStart, dateEnd), variety, true));
                    }

                    tbMemoryCivilianIn2.Text = DateRangeToString(dateRange);
                }
                catch
                {
                    tbMemoryCivilianIn2.Text = "";
                }
            }
        }

        private void BCurrentPositionFree_Click(object sender, EventArgs e)
        {
            lbCurrentPosition.SelectedIndex = -1;
            lbCurrentPositionId.SelectedIndex = -1;
        }

        private void TbMemoryAll_TextChanged(object sender, EventArgs e)
        {
            MemoryMoneyChange();
        }

        private void cbNshName_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbNshName.Text = cbNshName.Text;
        }

        private void tcEditPeople_KeyPress(object sender, KeyPressEventArgs e)
        {
            //ESC
            if (e.KeyChar == 27)
                Close();
            //Enter
            if (e.KeyChar == '\r')
                EditDo();
        }

        private void lbTaskName_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Enter
            if (e.KeyChar == '\r')
                TaskDo(true);
        }

        private void lbCurrentPosition_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Enter
            if (e.KeyChar == '\r')
                ChoosePositionHide();
        }

        private void bPeopleDelete_Click(object sender, EventArgs e)
        {
            //удаление контракта человека
            _sqlCommand =
                new SqlCommand(
                    "DELETE FROM [Slaves] WHERE [peopleId]=@peopleId",
                    _sqlConnection);
            _sqlCommand.Parameters.AddWithValue("peopleId", _peopleId);
            _sqlCommand.ExecuteNonQuery();

            //удаление образования человека
            _sqlCommand =
                new SqlCommand(
                    "DELETE FROM [Educations] WHERE [peopleId]=@peopleId",
                    _sqlConnection);
            _sqlCommand.Parameters.AddWithValue("peopleId", _peopleId);
            _sqlCommand.ExecuteNonQuery();

            //удаление состава семьи человека
            _sqlCommand =
                new SqlCommand(
                    "DELETE FROM [Family] WHERE [peopleId]=@peopleId",
                    _sqlConnection);
            _sqlCommand.Parameters.AddWithValue("peopleId", _peopleId);
            _sqlCommand.ExecuteNonQuery();

            //удаление послужного списка
            _sqlCommand =
                new SqlCommand(
                    "DELETE FROM [History] WHERE [peopleId]=@peopleId",
                    _sqlConnection);
            _sqlCommand.Parameters.AddWithValue("peopleId", _peopleId);
            _sqlCommand.ExecuteNonQuery();

            //удаление боевых действий человека
            _sqlCommand =
                new SqlCommand(
                    "DELETE FROM [Battlefields] WHERE [peopleId]=@peopleId",
                    _sqlConnection);
            _sqlCommand.Parameters.AddWithValue("peopleId", _peopleId);
            _sqlCommand.ExecuteNonQuery();

            //удаление медалей человека
            _sqlCommand =
                new SqlCommand(
                    "DELETE FROM [Medals] WHERE [peopleId]=@peopleId",
                    _sqlConnection);
            _sqlCommand.Parameters.AddWithValue("peopleId", _peopleId);
            _sqlCommand.ExecuteNonQuery();

            //удаление выслуги человека
            _sqlCommand =
                new SqlCommand(
                    "DELETE FROM [Memory] WHERE [peopleId]=@peopleId",
                    _sqlConnection);
            _sqlCommand.Parameters.AddWithValue("peopleId", _peopleId);
            _sqlCommand.ExecuteNonQuery();

            _sqlCommand =
                new SqlCommand(
                    "DELETE FROM [Peoples] WHERE [id]=@id",
                    _sqlConnection);
            _sqlCommand.Parameters.AddWithValue("id", _peopleId);
            _sqlCommand.ExecuteNonQuery();
            Close();
        }

        private void bCurrentChange_Click(object sender, EventArgs e)
        {
            _sqlCommand = new SqlCommand(
                "SELECT [id] FROM [Primary] WHERE [name]=@name",
                _sqlConnection);
            _sqlCommand.Parameters.AddWithValue("name", tbCurrentPrimaryId.Text);
            _sqlReader = _sqlCommand.ExecuteReader();
            _sqlReader.Read();
            var primaryId = Convert.ToInt32(_sqlReader["id"]);
            _sqlReader.Close();

            _sqlCommand =
                new SqlCommand(
                    "UPDATE [Positions] SET [fullName]=@fullName, [vus]=@vus, [primaryId]=@primaryId, " +
                    "[tarif]=@tarif, [action]=@action, [actionUser]=@actionUser WHERE [id]=@id", _sqlConnection);
            _sqlCommand.Parameters.AddWithValue("id", Convert.ToInt32(lbCurrentPositionId.Text));
            _sqlCommand.Parameters.AddWithValue("fullName", tbCurrentFullName.Text);
            _sqlCommand.Parameters.AddWithValue("vus", tbCurrentVus.Text);
            _sqlCommand.Parameters.AddWithValue("primaryId", Convert.ToInt32(primaryId));
            _sqlCommand.Parameters.AddWithValue("tarif", tbCurrentTarif.Text);
            _sqlCommand.Parameters.AddWithValue("action", DateTime.Now);
            _sqlCommand.Parameters.AddWithValue("actionUser", _userName);
            _sqlCommand.ExecuteNonQuery();
        }

        private void BCurrentPositionFree_MouseDown(object sender, MouseEventArgs e)
        {
            b1CurrentPosition.Image = Resources.delete1;
        }

        private void BCurrentPositionFree_MouseUp(object sender, MouseEventArgs e)
        {
            b1CurrentPosition.Image = Resources.delete;
        }

        private void ChangeGender(int gender)
        {
            if (gender == -1)
                gender = cbGender.SelectedIndex == 1 ? 0 : 1;
            if (gender == 1)
            {
                if (cbGender.SelectedIndex == 1) return;
                cbGender.SelectedIndex = 1;
                bGenderChange.Image = Resources.switch0;
                pGenderColored.BackColor = Color.FromArgb(255, 122, 115);
                bGenderFemale.BackColor = Color.FromArgb(255, 122, 115);
                bGenderMale.BackColor = _backColor[_colorSchema];
            }
            else
            {
                if (cbGender.SelectedIndex == 0) return;
                cbGender.SelectedIndex = 0;
                bGenderChange.Image = Resources.switch1;
                pGenderColored.BackColor = _mainColor[_colorSchema];
                bGenderFemale.BackColor = _backColor[_colorSchema];
                bGenderMale.BackColor = _mainColor[_colorSchema];
            }
        }

        private void bGenderFemale_Click(object sender, EventArgs e)
        {
            ChangeGender(1);
        }

        private void bGenderMale_Click(object sender, EventArgs e)
        {
            ChangeGender(0);
        }

        private void bGenderChange_Click(object sender, EventArgs e)
        {
            ChangeGender(-1);
        }

        private void PrimaryEdit(bool isOpen)
        {
            if (!isOpen)
            {
                var olHeight = tbpPrimary.Height;
                lbPrimary.Top = 25;
                tbpPrimary.Height = Convert.ToInt32(tbpPrimary.Tag.ToString());
                tbpPrimary.Tag = olHeight.ToString();
                lbPrimary.Items.Clear();
                foreach (var t in cbPrimary.Items)
                    lbPrimary.Items.Add(t);
                _primaryOpen = true;
                if (cbPrimary.SelectedIndex > -1 && lbPrimary.Items.Count > 0)
                    lbPrimary.SelectedIndex = cbPrimary.SelectedIndex;
                _primaryOpen = false;
            }
            else
            {
                if (lbPrimary.SelectedIndex > -1 && cbPrimary.Items.Count > 0)
                    cbPrimary.SelectedIndex = lbPrimary.SelectedIndex;
                if (_primaryOpen) return;
                var olHeight = tbpPrimary.Height;
                lbPrimary.Top = 26;
                tbpPrimary.Height = Convert.ToInt32(tbpPrimary.Tag.ToString());
                tbpPrimary.Tag = olHeight.ToString();
            }
        }

        private void tbPrimary_Enter(object sender, EventArgs e)
        {
            PrimaryEdit(false);
        }

        private void cbPrimary_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbPrimary.Text = cbPrimary.Text;
        }

        private void lbPrimary_SelectedIndexChanged(object sender, EventArgs e)
        {
            PrimaryEdit(true);
        }

        private static bool CheckDate(string inputStringDate, bool checkEnd)
        {
            try
            {
                if (inputStringDate.Length < 10)
                    return false;
                var dateTime = Convert.ToDateTime(inputStringDate);
                return checkEnd ? dateTime <= DateTime.Today : true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void DateToText(string elementSurname)
        {
            var elementChange = Controls.Find("dtp" + elementSurname, true);
            if (elementChange.Length <= 0 || _dateOpen) return;
            _dateOpen = true;
            var textDate = elementChange[0].Text;
            elementChange = Controls.Find("tb" + elementSurname, true);
            if (elementChange.Length <= 0)
            {
                _dateOpen = false;
                return;
            }
            elementChange[0].Text = textDate;
            _dateOpen = false;
        }

        private void TextToDate(string elementSurname)
        {
            try
            {
                var elementChange = Controls.Find("tb" + elementSurname, true);
                if (elementChange.Length <= 0 || _dateOpen) return;
                _dateOpen = true;
                bool checkEnd = !(elementSurname == "SlaveEnd" || elementSurname == "SlaveStart");
                if (CheckDate(elementChange[0].Text, checkEnd))
                {
                    var date = Convert.ToDateTime(elementChange[0].Text);
                    elementChange[0].ForeColor = _foreColor[_colorSchema];
                    elementChange = Controls.Find("dtp" + elementSurname, true);
                    if (elementChange.Length <= 0) return;
                    elementChange[0].Text = date.ToString("dd.MM.yyyy");
                }
                else
                {
                    elementChange[0].ForeColor = elementChange[0].Name == "tbSlaveEnd"
                                                 ? _foreColor[_colorSchema]
                    : Color.FromArgb(166, 8, 0);
                }
                _dateOpen = false;
            }
            catch (Exception)
            {
                _dateOpen = false;
            }
        }

        private void dtpDateBirthday_ValueChanged(object sender, EventArgs e)
        {
            DateToText("DateBirthday");
            tbDateBirthday.Text = dtpDateBirthday.Value.ToString("dd.MM.yyyy");
        }

        private void tbDateBirthday_TextChanged(object sender, EventArgs e)
        {
            TextToDate("DateBirthday");
        }

        private void bChoosePrimary_Click(object sender, EventArgs e)
        {
        }

        private void tbFio0_Enter(object sender, EventArgs e)
        {
        }

        private void tbLNumber_TextChanged(object sender, EventArgs e)
        {
            var thisString = tbLNumber.Text;
            if (thisString.Length < 8 ||
                thisString.Length > 9 ||
                !thisString.Contains("-"))
                tbLNumber.ForeColor = Color.FromArgb(166, 8, 0);
            else
                tbLNumber.ForeColor = _foreColor[_colorSchema];
            GoDecline();
        }

        private void EditPeopleForm_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, ClientRectangle, _borderColor[_colorSchema], ButtonBorderStyle.Solid);
        }

        private void bMainData_Click(object sender, EventArgs e)
        {
            SelectPanel(0);
        }

        private void bOtherData_Click(object sender, EventArgs e)
        {
            SelectPanel(1);
        }

        private void bPositionsData_Click(object sender, EventArgs e)
        {
            SelectPanel(2);
        }

        private void bMemoryData_Click(object sender, EventArgs e)
        {
            SelectPanel(3);
        }

        private void bTasksData_Click(object sender, EventArgs e)
        {
            SelectPanel(4);
        }

        private void bMainData_Enter(object sender, EventArgs e)
        {
            NavigationFocus(0, true);
        }

        private void bMainData_Leave(object sender, EventArgs e)
        {
            NavigationFocus(0, false);
        }

        private void bOtherData_Enter(object sender, EventArgs e)
        {
            NavigationFocus(1, true);
        }

        private void bOtherData_Leave(object sender, EventArgs e)
        {
            NavigationFocus(1, false);
        }

        private void bPositionsData_Enter(object sender, EventArgs e)
        {
            NavigationFocus(2, true);
        }

        private void bPositionsData_Leave(object sender, EventArgs e)
        {
            NavigationFocus(2, false);
        }

        private void bMemoryData_Enter(object sender, EventArgs e)
        {
            NavigationFocus(3, true);
        }

        private void bMemoryData_Leave(object sender, EventArgs e)
        {
            NavigationFocus(3, false);
        }

        private void bTasksData_Enter(object sender, EventArgs e)
        {
            NavigationFocus(4, true);
        }

        private void bTasksData_Leave(object sender, EventArgs e)
        {
            NavigationFocus(4, false);
        }

        private void dtpPrimaryDate_ValueChanged(object sender, EventArgs e)
        {
            DateToText("PrimaryDate");
            tbPrimaryDate.Text = (dtpPrimaryDate.Value == dtpStart.MinDate
                                  || dtpPrimaryDate.Value == DateTime.Parse("1.1.1990"))
                ? ""
                : dtpPrimaryDate.Value.ToString("dd.MM.yyyy");
        }

        private void tbPrimaryDate_TextChanged(object sender, EventArgs e)
        {
            TextToDate("PrimaryDate");
        }

        private void dtpPrimaryOrderDate_ValueChanged(object sender, EventArgs e)
        {
            DateToText("PrimaryOrderDate");
            tbPrimaryOrderDate.Text = dtpPrimaryOrderDate.Value.ToString("dd.MM.yyyy");
            if (tbPrimaryDate.Text == "")
                tbPrimaryDate.Text = tbPrimaryOrderDate.Text;
        }

        private void tbPrimaryOrderDate_TextChanged(object sender, EventArgs e)
        {
            TextToDate("PrimaryOrderDate");
        }

        private void dtpPositionOrderDate_ValueChanged(object sender, EventArgs e)
        {
            DateToText("PositionOrderDate");
            tbPositionOrderDate.Text = dtpPositionOrderDate.Value.ToString("dd.MM.yyyy");
        }

        private void tbPositionOrderDate_TextChanged(object sender, EventArgs e)
        {
            TextToDate("PositionOrderDate");
        }

        private void dtpSlaveStart_ValueChanged(object sender, EventArgs e)
        {
            DateToText("SlaveStart");
            tbSlaveStart.Text = dtpSlaveStart.Value.ToString("dd.MM.yyyy");
        }

        private void tbSlaveStart_TextChanged(object sender, EventArgs e)
        {
            TextToDate("SlaveStart");
        }

        private void dtpSlaveEnd_ValueChanged(object sender, EventArgs e)
        {
            DateToText("SlaveEnd");
            tbSlaveEnd.Text = dtpSlaveEnd.Value.ToString("dd.MM.yyyy");
        }

        private void tbSlaveEnd_TextChanged(object sender, EventArgs e)
        {
            TextToDate("SlaveEnd");
        }

        private void dtpSlaveOrderDate_ValueChanged(object sender, EventArgs e)
        {
            DateToText("SlaveOrderDate");
            tbSlaveOrderDate.Text = dtpSlaveOrderDate.Value.ToString("dd.MM.yyyy");
        }

        private void tbSlaveOrderDate_TextChanged(object sender, EventArgs e)
        {
            TextToDate("SlaveOrderDate");
        }

        private void dtpStart_ValueChanged(object sender, EventArgs e)
        {
            DateToText("Start");
            tbStart.Text = dtpStart.Value.ToString("dd.MM.yyyy");
        }

        private void tbStart_TextChanged(object sender, EventArgs e)
        {
            TextToDate("Start");
        }

        private void bEducation_Click(object sender, EventArgs e)
        {
            dgvpEditEducations.Top = tbpEducation.Top;
            dgvpEditEducations.Height = tabPage4.Height - tbpEducation.Top - 2;
            dgvpEditEducations.Visible = true;
            dgvpEditEducations.BringToFront();
        }

        private void dgvCloseEditEducations_Click(object sender, EventArgs e)
        {
            dgvpEditEducations.Visible = false;
        }

        private void bFamily_Click(object sender, EventArgs e)
        {
            dgvpEditFamily.Top = tbpFamily.Top;
            dgvpEditFamily.Height = Height - tbpFamily.Top - 2;
            dgvpEditFamily.Visible = true;
            dgvpEditFamily.BringToFront();
        }

        private void dgvCloseEditFamily_Click(object sender, EventArgs e)
        {
            dgvpEditFamily.Visible = false;
        }

        private void bBattlefields_Click(object sender, EventArgs e)
        {
            dgvpEditBattlefields.Top = tbpBattlefields.Top;
            dgvpEditBattlefields.Height = Height - tbpBattlefields.Top - 2;
            dgvpEditBattlefields.Visible = true;
            dgvpEditBattlefields.BringToFront();
        }

        private void dgvCloseBattlefields_Click(object sender, EventArgs e)
        {
            dgvpEditBattlefields.Visible = false;
        }

        private void bMedals_Click(object sender, EventArgs e)
        {
            dgvpEditMedals.Top = tbpMedals.Top;
            dgvpEditMedals.Height = Height - tbpMedals.Top - 2;
            dgvpEditMedals.Visible = true;
            dgvpEditMedals.BringToFront();
        }

        private void dgvCloseMedals_Click(object sender, EventArgs e)
        {
            dgvpEditMedals.Visible = false;
        }

        private void EducationCalcToText()
        {
            tbEducation.Text = "";
            for (var i = dgvEditEducations.RowCount - 1; i > -1; i--)
            {
                if (dgvEditEducations.Rows[i].Cells[0].Value == null ||
                    dgvEditEducations.Rows[i].Cells[0].ToString() == "" ||
                    dgvEditEducations.Rows[i].Cells[1].Value == null ||
                    dgvEditEducations.Rows[i].Cells[1].ToString() == "") continue;
                if (tbEducation.Text != "")
                    tbEducation.Text += ", ";
                tbEducation.Text += dgvEditEducations.Rows[i].Cells[0].Value +
                                    " в " + dgvEditEducations.Rows[i].Cells[1].Value + " г.";
            }
        }

        private void dgvEditEducations_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            EducationCalcToText();
        }

        private void FamilyCalcToText()
        {
            var isMarried = -1;
            var numberChilds = 0;
            for (var i = 0; i < dgvEditFamily.RowCount; i++)
            {
                if (dgvEditFamily.Rows[i].Cells[0].Value == null ||
                    dgvEditFamily.Rows[i].Cells[0].ToString() == "" ||
                    dgvEditFamily.Rows[i].Cells[1].Value == null ||
                    dgvEditFamily.Rows[i].Cells[1].ToString() == "" ||
                    dgvEditFamily.Rows[i].Cells[2].Value == null ||
                    dgvEditFamily.Rows[i].Cells[2].ToString() == "") continue;
                if (dgvEditFamily.Rows[i].Cells[0].Value.ToString() == "жена")
                    isMarried = 0;
                else if (dgvEditFamily.Rows[i].Cells[0].Value.ToString() == "муж")
                    isMarried = 1;
                else if (dgvEditFamily.Rows[i].Cells[0].Value.ToString() == "дочь" ||
                         dgvEditFamily.Rows[i].Cells[0].Value.ToString() == "сын")
                    numberChilds++;
            }

            switch (isMarried)
            {
                case 0:
                    tbFamily.Text = "женат";
                    break;
                case 1:
                    tbFamily.Text = "замужем";
                    break;
                default:
                    tbFamily.Text = "холост";
                    break;
            }

            if (numberChilds <= 0) return;
            tbFamily.Text += ", ";
            if (numberChilds == 1)
                tbFamily.Text += numberChilds + " ребенок";
            else
                tbFamily.Text += numberChilds + " детей";
        }

        private void dgvEditFamily_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            FamilyCalcToText();
        }

        private void BattlefieldsCalcToText()
        {
            var numberBattlefields = 0;
            for (var i = 0; i < dgvEditBattlefields.RowCount; i++)
            {
                if (dgvEditBattlefields.Rows[i].Cells[0].Value == null ||
                    dgvEditBattlefields.Rows[i].Cells[0].ToString() == "" ||
                    dgvEditBattlefields.Rows[i].Cells[1].Value == null ||
                    dgvEditBattlefields.Rows[i].Cells[1].ToString() == "") continue;
                numberBattlefields++;
            }

            tbBattlefields.Text = numberBattlefields > 0
                ? "участвовал в боевых действиях"
                : "не участвовал";
        }

        private void dgvBattlefields_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            BattlefieldsCalcToText();
        }

        private void MedalsCalcToText()
        {
            var numberMedalsGoverment = 0;
            var numberMedals = 0;
            for (var i = 0; i < dgvEditMedals.RowCount; i++)
            {
                if (dgvEditMedals.Rows[i].Cells[0].Value == null ||
                    dgvEditMedals.Rows[i].Cells[0].ToString() == "" ||
                    dgvEditMedals.Rows[i].Cells[1].Value == null ||
                    dgvEditMedals.Rows[i].Cells[1].ToString() == "") continue;
                if (dgvEditMedals.Rows[i].Cells[1].ToString().Contains("Президент") ||
                    dgvEditMedals.Rows[i].Cells[1].ToString().Contains("президент") ||
                    dgvEditMedals.Rows[i].Cells[1].ToString().Contains("Указ") ||
                    dgvEditMedals.Rows[i].Cells[1].ToString().Contains("указ"))
                    numberMedalsGoverment++;
                else
                    numberMedals++;
            }

            tbMedals.Text = "";
            if (numberMedalsGoverment > 0)
                tbMedals.Text += "Гос.награды – " + numberMedalsGoverment;
            if (numberMedals > 0)
            {
                if (numberMedalsGoverment > 0)
                    tbMedals.Text += ", медалей – ";
                else
                    tbMedals.Text += "Медалей – ";
                tbMedals.Text += numberMedals;
            }
            else
            {
                tbMedals.Text = "не имеет";
            }
        }

        private void dgvMedals_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            MedalsCalcToText();
        }

        private void lbCurrentPositionScroll_Scroll(object sender, ScrollEventArgs e)
        {
            lbCurrentPosition.SelectedIndex = lbCurrentPositionScroll.Value;
        }

        private void NshNokEdit(int nshNok)
        {
            if (nshNok == -1 && cbNokName.Items.Count > 0 && lbNshNokName.SelectedIndex > -1)
            {
                if (pNshNokName.Top == pNshName.Top)
                {
                    cbNshName.SelectedIndex = lbNshNokName.SelectedIndex;
                }
                else
                {
                    if (pNshNokName.Top == pNokName.Top)
                        cbNokName.SelectedIndex = lbNshNokName.SelectedIndex;
                    else
                        return;
                }

                pNshNokName.Visible = false;
            }
            else
            {
                lbNshNokName.Items.Clear();
                foreach (var t in cbNshName.Items)
                    lbNshNokName.Items.Add(t);
                lbNshNokNameScroll.Maximum = lbNshNokName.Items.Count - 1;
                if (nshNok == 0)
                {
                    pNshNokName.Top = pNshName.Top;
                    if (cbNshName.SelectedIndex > -1 && lbNshNokName.Items.Count > 0)
                        lbNshNokName.SelectedIndex = cbNshName.SelectedIndex;
                }
                else if (nshNok == 1)
                {
                    pNshNokName.Top = pNokName.Top;
                    if (cbNokName.SelectedIndex > -1 && lbNshNokName.Items.Count > 0)
                        lbNshNokName.SelectedIndex = cbNokName.SelectedIndex;
                }

                pNshNokName.Height = tcEditPeople.Height - pNshNokName.Top - 1;
                pNshNokName.BringToFront();
                pNshNokName.Visible = true;
                if (lbNshNokNameScroll.Height < 5) lbNshNokNameScroll.Height = 5;
                var step = lbNshNokNameScroll.Maximum * 18 / lbNshNokNameScroll.Height;
                if (step < 2) step = 2;
                step = Convert.ToInt32(lbNshNokNameScroll.Height / step);
                if (step < 5) step = 5;
                lbNshNokNameScroll.ThumbSize = step;
                lbNshNokNameScroll.Visible = lbNshNokNameScroll.Maximum * 18 > lbNshNokNameScroll.Height;
            }
        }

        private void lbNshNameScroll_Scroll(object sender, ScrollEventArgs e)
        {
            lbNshNokName.SelectedIndex = lbNshNokNameScroll.Value;
        }

        private void lbNshName_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbNshNokNameScroll.Value = lbNshNokName.SelectedIndex;
        }

        private void lbbNshNokName_Click(object sender, EventArgs e)
        {
            NshNokEdit(-1);
        }

        private void bNshName_Click(object sender, EventArgs e)
        {
            NshNokEdit(0);
        }

        private void bNokName_Click(object sender, EventArgs e)
        {
            NshNokEdit(1);
        }

        private void cbNokName_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbNokName.Text = cbNokName.Text;
        }

        private void bHelp_Click_2(object sender, EventArgs e)
        {
            _helpVisible = !_helpVisible;
            bHelp.Image = _helpVisible ? Resources.help1 : Resources.help;
            lMainHelp.Visible = _helpVisible;
            pSecondHelp.Visible = _helpVisible;
            pPositionsHelp.Visible = _helpVisible;
            pHelp1.Visible = _helpVisible;
            pHelp2.Visible = _helpVisible;
            pHelp3.Visible = _helpVisible;
            pHelp4.Visible = _helpVisible;
            lTasksHelp1.Visible = _helpVisible;
            lTasksHelp2.Visible = _helpVisible;
        }

        private void bDecline_Click(object sender, EventArgs e)
        {
            bDecline.Tag = Convert.ToInt32(bDecline.Tag) == 2 ? 3 : 2;
            LoadDictionary(Convert.ToInt32(bDecline.Tag));
        }
        
        private void bFlash_Click(object sender, EventArgs e)
        {
            ColorSchemaSet(this);
            bFlash.Image = _colorSchema == 0 ? Resources.unsun : Resources.sun;
            _colorSchema = _colorSchema == 0 ? 1 : 0;
        }

        private void BEditBack_Click(object sender, KeyPressEventArgs e)
        {

        }

        private void lbNshNokName_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            NshNokEdit(-1);
        }

        private void tbChooseExcelPrimary0_Enter(object sender, EventArgs e)
        {
            bActiveFails.ForeColor = _changerColor[_colorSchema];
            bActiveFails.Image = Resources.check2;
        }

        private void bActiveFails_Leave(object sender, EventArgs e)
        {
            if (_onlyActiveFails)
            {
                bActiveFails.ForeColor = _secondColor[_colorSchema];
                bActiveFails.Image = Resources.check1;
            }
            else
            {
                bActiveFails.ForeColor = _foreColor[_colorSchema];
                bActiveFails.Image = Resources.check;
            }
        }

        private void bActiveFails_Click(object sender, EventArgs e)
        {
            _onlyActiveFails = !_onlyActiveFails;
            if (_onlyActiveFails)
            {
                bActiveFails.ForeColor = _secondColor[_colorSchema];
                bActiveFails.Image = Resources.check1;
            }
            else
            {
                bActiveFails.ForeColor = _foreColor[_colorSchema];
                bActiveFails.Image = Resources.check;
            }
            dgvFails.Rows.Clear();
            foreach (var t1 in _fails)
            {
                if (_onlyActiveFails && t1[5] != "") continue;
                dgvFails.Rows.Add(t1[0], t1[1], t1[2],
                    t1[3], t1[4], t1[5], t1[6], t1[7]);
            }

            bSaveFails.Visible = !_onlyActiveFails;
            dgvFails.Enabled = !_onlyActiveFails;
        }

        private void bFails_Click(object sender, EventArgs e)
        {
            SelectPanel(5);
        }

        private void bFails_Enter(object sender, EventArgs e)
        {
            NavigationFocus(5, true);
        }

        private void bFails_Leave(object sender, EventArgs e)
        {
            NavigationFocus(5, false);
        }

        private void bMax_MouseEnter(object sender, EventArgs e)
        {
            bMax.Image = WindowState == FormWindowState.Normal ? Resources.fullin30_1 : Resources.fulloff30_1;
        }

        private void bMax_MouseLeave(object sender, EventArgs e)
        {
            bMax.Image = WindowState == FormWindowState.Normal ? Resources.fullin30 : Resources.fulloff30;
        }

        private void bMin_Enter(object sender, EventArgs e)
        {
            bMin.Image = Resources.minimum1;
        }

        private void bMin_MouseLeave(object sender, EventArgs e)
        {
            bMin.Image = Resources.minimum;
        }

        private void bFlash_MouseEnter(object sender, EventArgs e)
        {
            bFlash.Image = _colorSchema == 0 ? Resources.sun1 : Resources.unsun1;
        }

        private void bFlash_MouseLeave(object sender, EventArgs e)
        {
            bFlash.Image = _colorSchema == 0 ? Resources.sun : Resources.unsun;
        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void bTaskWord_Click(object sender, EventArgs e)
        {
            TaskDo(false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbTaskName_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (lbTaskName.Text)
            {
                case "Послужной список":
                    rtbPreview.Text = "Недоступно";
                    break;
                case "Справка-объективка":
                    rtbPreview.Text = "Недоступно";
                    break;
                case "Справка о выслуге лет":
                    rtbPreview.Text = "По состоянию на " + dtpTaskDate.Value.ToString("dd.MM.yyyy") + " г.:\n" +
                                      "Выслуга лет в календарном исчислении: " + tbMemoryCalend.Text + "\n" +
                                      "В льготном исчислении: " + tbMemoryAll.Text;
                    break;
                case "Справка о прохождении службы":
                    rtbPreview.Text = "Выдана в том, что он (она) проходит военную службу " +
                                     "по контракту в войсковой части 71289 (г. Уссурийск " +
                                     "Приморского края).";
                    break;
                case "Справка о составе семьи":
                    _sqlCommand = new SqlCommand("SELECT [position], [name], [dateBirthday] FROM [Family] WHERE [peopleId]=@peopleId", _sqlConnection);
                    _sqlCommand.Parameters.AddWithValue("peopleId", _peopleId);
                    _sqlReader = _sqlCommand.ExecuteReader();
                    var _family = "";
                    while (_sqlReader.Read())
                    {
                        if (_family.Length > 0)
                            _family += ", ";
                        _family += _sqlReader["position"] + " ";
                        if (_sqlReader["position"].ToString() == "жена" ||
                            _sqlReader["position"].ToString() == "муж")
                            _family += "– ";
                        _family += _sqlReader["name"] + ", " + Convert.ToDateTime(_sqlReader["dateBirthday"]).ToString("dd.MM.yyy") + " г.р.";
                    }
                    _sqlReader?.Close();

                    if (_family == "")
                        _family = "холост.";
                    rtbPreview.Text = "В его (ее) личном деле записаны: " + _family;
                    break;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            char[] rowSplitter = { '\r', '\n' };
            //get the text from clipboard
            var dataInClipboard = Clipboard.GetDataObject();
            var stringInClipboard = (string)dataInClipboard.GetData(DataFormats.Text);
            //split it into lines
            var rowsInClipboard = stringInClipboard.Split(rowSplitter, StringSplitOptions.RemoveEmptyEntries);
            listBox2.Items.Clear();
            for (var i = 0; i < rowsInClipboard.Length; i++)
                listBox2.Items.Add(PrimaryDecline(rowsInClipboard[i], 1));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            char[] rowSplitter = { '\r', '\n' };
            char[] columnSplitter = { ' ' };
            //get the text from clipboard
            var dataInClipboard = Clipboard.GetDataObject();
            var stringInClipboard = (string)dataInClipboard.GetData(DataFormats.Text);
            //split it into lines
            var rowsInClipboard = stringInClipboard.Split(rowSplitter, StringSplitOptions.RemoveEmptyEntries);
            listBox1.Items.Clear();
            for (var i = 0; i < rowsInClipboard.Length; i++)
            {
                //clmns
                var valuesInRow = rowsInClipboard[i].Split(columnSplitter);
                var fioNames = new Decliner().Decline(valuesInRow[0], valuesInRow[1], valuesInRow[2], 3);
                listBox2.Items[i] += " " + fioNames[0];
                listBox1.Items.Add(fioNames[1] + " " + fioNames[2]);
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            for (var i = 0; i < listBox1.Items.Count; i++)
            {
                var linkToFile = button4.Text;
                string[] st = {listBox2.Items[i].ToString(), listBox1.Items[i].ToString(), t1.Text, t2.Text, t3.Text, t4.Text, t5.Text, t6.Text, t7.Text};
                new GeneratedClassTemp().CreatePackage(linkToFile + "\\" + i + "_" + listBox2.Items[i] + " " + listBox1.Items[i] + ".docx", st);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            panel2.Visible = !panel2.Visible;
        }

        private void dtpStartThis_ValueChanged(object sender, EventArgs e)
        {
            DateToText("StartThis");
            tbStartThis.Text = dtpStartThis.Value.ToString("dd.MM.yyyy");
        }

        private void tbStartThis_TextChanged(object sender, EventArgs e)
        {
            TextToDate("StartThis");
        }

        private void bMenu_Click(object sender, EventArgs e)
        {
            ChangeMenuSchema();
        }

        private void tbNshName_TextChanged(object sender, EventArgs e)
        {
            if (_primaryOpen) return;
            _primaryOpen = true;
            _sqlCommand =
                new SqlCommand(
                    "SELECT [nshId], [nokId] FROM [Settings] WHERE [nshId]=@nshId AND [nokId]=@nokId",
                    _sqlConnection);
            _sqlCommand.Parameters.AddWithValue("nshId", Convert.ToInt32(lbPeoplesId.Items[cbNshName.SelectedIndex]));
            _sqlCommand.Parameters.AddWithValue("nokId", Convert.ToInt32(lbPeoplesId.Items[cbNokName.SelectedIndex]));
            _sqlReader = _sqlCommand.ExecuteReader();
            if (_sqlReader.HasRows)
            {
                var settingsId = Convert.ToInt32(_sqlReader["name"]);
                _sqlReader.Close();
                _sqlCommand =
                    new SqlCommand(
                        "UPDATE [Settings] SET [action]=@action, [actionUser]=@actionUser " +
                        "WHERE [id]=@id", _sqlConnection);
                _sqlCommand.Parameters.AddWithValue("id", settingsId);
                _sqlCommand.Parameters.AddWithValue("action", DateTime.Now);
                _sqlCommand.Parameters.AddWithValue("actionUser", _userName);
                _sqlCommand.ExecuteNonQuery();
            }
            else
            {
                _sqlReader.Close();
                _sqlCommand =
                    new SqlCommand(
                        "SELECT [positionId] FROM [Peoples] WHERE [id]=@id",
                        _sqlConnection);
                _sqlCommand.Parameters.AddWithValue("id", Convert.ToInt32(lbPeoplesId.Items[cbNshName.SelectedIndex]));
                _sqlReader = _sqlCommand.ExecuteReader();
                _sqlReader.Read();
                var nshId = Convert.ToInt32(_sqlReader["positionId"]);
                _sqlReader.Close();
                _sqlCommand =
                    new SqlCommand(
                        "SELECT [positionId] FROM [Peoples] WHERE [id]=@id",
                        _sqlConnection);
                _sqlCommand.Parameters.AddWithValue("id", Convert.ToInt32(lbPeoplesId.Items[cbNokName.SelectedIndex]));
                _sqlReader = _sqlCommand.ExecuteReader();
                _sqlReader.Read();
                var nokId = Convert.ToInt32(_sqlReader["positionId"]);
                _sqlReader.Close();

                _sqlCommand =
                    new SqlCommand(
                        "INSERT INTO [Settings] (nshId, nokId, action," +
                        " actionUser) VALUES (@nshId, @nokId, @action, @actionUser)",
                        _sqlConnection);
                var t0 = Convert.ToInt32(lbPeoplesId.Items[cbNshName.SelectedIndex]);
                _sqlCommand.Parameters.AddWithValue("nshId", nshId);
                _sqlCommand.Parameters.AddWithValue("nokId", nokId);
                _sqlCommand.Parameters.AddWithValue("action", DateTime.Now);
                _sqlCommand.Parameters.AddWithValue("actionUser", _userName);
                _sqlCommand.ExecuteNonQuery();
            }

            _sqlReader.Close();
            _primaryOpen = false;
        }

        private void tbCurrentFullName_TextChanged(object sender, EventArgs e)
        {
            //t
        }

        private void bEducationCheck_Click(object sender, EventArgs e)
        {
            var linkToFile = @"C:\temp\Запрос " + tbFio0.Text + " " + tbFio1.Text[0] + tbFio2.Text[0] + ".docx";
            var fioNames = new Decliner().Decline(tbFio0.Text, tbFio1.Text, tbFio2.Text, 3);
            _sqlCommand =
                new SqlCommand(
                    "SELECT TOP 1 * FROM [Educations] WHERE [peopleId]=@peopleId ORDER BY [action] DESC",
                    _sqlConnection);
            _sqlCommand.Parameters.AddWithValue("peopleId", _peopleId);
            _sqlReader = _sqlCommand.ExecuteReader();
            _sqlReader.Read();
            string[] education = { _sqlReader["name"].ToString(), _sqlReader["year"].ToString(), _sqlReader["special"].ToString()};
            _sqlReader.Close();
            new WordEducationCheckClass().CreatePackage(linkToFile, fioNames[0] + " " + fioNames[1] + " " + fioNames[2], 
                Convert.ToInt32(Convert.ToDateTime(tbDateBirthday.Text).Year).ToString(), 
                education[0], education[1], education[2]);
            new DialogForm
            {
                lText = {Text = "Запрос сохранен в папке " + linkToFile},
                bCancel = {Visible = false},
                bOk = {Width = 200}
            }.ShowDialog();
        }
    }
}