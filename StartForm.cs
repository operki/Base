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
//склонение фио
using CaseDecline.CS;

namespace WindowsFormsApp1
{
    public partial class StartForm : Form
    {
        //офицеры, прапорщики, сержанты и солдаты (для выгрузки в excel)
        private readonly bool[] _categoryPeoples = {true, false, false, false};
        //склонения (для выгрузки в excel)
        private List<string[]> _positionsDictionary0 = new List<string[]>();
        private List<string[]> _positionsDictionary1 = new List<string[]>();
        //id первого человека "на уровне" при поиске (прокрутке) по штату
        private readonly int[] _idChoose = {-1, -1, -1, -1};
        //текущий "уровень" при поиске (прокрутке) по штату
        private int _currentChoose;
        private readonly List<int> _idChooseName = new List<int>();
        private readonly string _userName;
        //редактирование BD (0 нет, 1 да)
        //выгрузка в excel (0 нет, 1 да)
        //люди (0 только заказ справок, 1 чтение данные и печать справок)
        //добавление людей (0 нет, 1 да)
        //люди (0 только чтение, 1 изменение почти всего, 2 + изменение должностей)
        private readonly int[] _userRights = {0, 0, 0, 0, 0};
        private bool _canToExcelDo;
        //меню раскрыто или свернуто
        private bool _menuSchema = true;
        //цветовая схема белая или черная
        private int _colorSchema = 0;
        private Color[] _borderColor = { Color.FromArgb(80, 80, 80), Color.FromArgb(150, 150, 150)};
        private Color[] _backColor = { Color.FromArgb(45, 45, 45), Color.FromArgb(225, 225, 225) };
        private Color[] _foreColor = { Color.FromArgb(240, 240, 240), Color.FromArgb(0, 0, 0) };
        private Color[] _mainColor = { Color.FromArgb(12, 93, 165), Color.FromArgb(64, 141, 200) };
        private Color[] _secondColor = { Color.FromArgb(0, 129, 16), Color.FromArgb(37, 148,51 ) };
        private Color[] _changerColor = { Color.FromArgb(255, 149, 0), Color.FromArgb(166, 97, 0) };
        private Color[] _mainHoverColor = { Color.FromArgb(12, 93, 165), Color.FromArgb(12, 93, 165) }; //???
        private Color[] _secondHoverColor = { Color.FromArgb(0, 154, 19), Color.FromArgb(44, 177, 61) };
        //1 - posId, 2 - parentMain, 3 - parentOthers, 4 - name, 5 - primId
        //6 - peopleId, 7 - fio, 8 - lnumber, 9 - primId
        private readonly List<string[]> _positions = new List<string[]>();
        //sql-реализация
        private readonly string _sqlConnectionString;
        private SqlCommand _sqlCommand;
        private SqlConnection _sqlConnection;
        private SqlDataReader _sqlReader;

        //установка шрифта
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont,
            IntPtr pdv, [System.Runtime.InteropServices.In] ref uint pcFonts);
        private readonly PrivateFontCollection _fonts = new PrivateFontCollection();
        Font _roboto;
        Font _roboto14;
        Font _raleway;

        /// <summary>
        /// Загрузка словаря по склонениям из таблицы Dictionary основной базы
        /// </summary>
        private void LoadDictionary()
        {
            _positionsDictionary0.Clear();
            _positionsDictionary1.Clear();
            _sqlCommand = new SqlCommand(
                    "SELECT [name], [decline1], [decline2] FROM [Dictionary]",
                    _sqlConnection);
            _sqlReader = _sqlCommand.ExecuteReader();
                while (_sqlReader.Read())
                {
                    _positionsDictionary0.Add(new[] { _sqlReader["name"].ToString(),
                        _sqlReader["decline2"].ToString() });
                    _positionsDictionary1.Add(new[] { _sqlReader["name"].ToString(),
                        _sqlReader["decline1"].ToString() });
                }
            _sqlReader.Close();
        }

        /// <summary>
        /// Склонение должности
        /// </summary>
        /// <param name="position">Полное название должности</param>
        /// <param name="declineType">Тип склонения, 2 - в родительный, остальное - в дательный</param>
        /// <returns>Должность просклоненная</returns>
        private string PositionDecline(string position, int declineType)
        {
            position = position.ToLower();
            if (declineType == 2)
            {
                foreach (var t in _positionsDictionary0)
                    position = position.Replace(t[0], t[1]);
            }
            else {
                foreach (var t in _positionsDictionary1)
                    position = position.Replace(t[0], t[1]);
            }
            return position;
        }

        /// <summary>
        /// Склонение звания
        /// </summary>
        /// <param name="primaryName">Полное название звания</param>
        /// <param name="declineType">Тип склонения, 2 - в родительный, остальное - в дательный</param>
        /// <returns>Звание просклоненное</returns>
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

        /// <summary>
        /// Загрузка формы
        /// </summary>
        /// <param name="sqlConnectionString">Строка подключения к sql</param>
        /// <param name="userName">Имя пользователя</param>
        /// <param name="userRights">редактирование BD (0 нет, 1 да), выгрузка в excel (0-1),
        /// основной тип пользователя (0 только заказ справок, 1 чтение данные и печать справок), добавление людей (0-1),
        /// дополнительный тип юзера (0 только чтение, 1 изменение почти всего, 2 + изменение должностей)</param>
        public StartForm(string sqlConnectionString, string userName, int[] userRights)
        {
            InitializeComponent();
            _sqlConnectionString = sqlConnectionString;
            _userName = userName;
            _userRights = userRights;
            //установка шрифта
            var fontData = Resources.roboto;
            var fontPtr = System.Runtime.InteropServices.Marshal.AllocCoTaskMem(fontData.Length);
            System.Runtime.InteropServices.Marshal.Copy(fontData, 0, fontPtr, fontData.Length);
            uint dummy = 0;
            _fonts.AddMemoryFont(fontPtr, Resources.roboto.Length);
            AddFontMemResourceEx(fontPtr, (uint)Resources.roboto.Length, IntPtr.Zero, ref dummy);
            System.Runtime.InteropServices.Marshal.FreeCoTaskMem(fontPtr);
            //запись шрифта в переменную
            _roboto = new Font(_fonts.Families[0], 11.0F);
            _roboto14 = new Font(_fonts.Families[0], 14.0F);
            //установка другого шрифта
            fontData = Resources.raleway;
            fontPtr = System.Runtime.InteropServices.Marshal.AllocCoTaskMem(fontData.Length);
            System.Runtime.InteropServices.Marshal.Copy(fontData, 0, fontPtr, fontData.Length);
            dummy = 0;
            _fonts.AddMemoryFont(fontPtr, Resources.raleway.Length);
            AddFontMemResourceEx(fontPtr, (uint)Resources.raleway.Length, IntPtr.Zero, ref dummy);
            System.Runtime.InteropServices.Marshal.FreeCoTaskMem(fontPtr);
            //запись шрифта в переменную
            _raleway = new Font(_fonts.Families[0], 14.0F);
            //установка шрифта для элементов формы
            foreach (Control control in Controls)
                FontSet(control, _roboto);
            FontSet(tbPeopleChoose, _roboto14);
            foreach (Control control in bpFindLNumber.Controls)
                FontSet(control, _raleway);
            foreach (Control control in bpFindSchema.Controls)
                FontSet(control, _raleway);
            foreach (Control control in bpOrderToData.Controls)
                FontSet(control, _raleway);
            foreach (Control control in bpStatistic.Controls)
                FontSet(control, _raleway);
            foreach (Control control in bpToExcel.Controls)
                FontSet(control, _raleway);
        }

        /// <summary>
        /// Установка шрифта для элемента
        /// </summary>
        /// <param name="control">Элемент</param>
        /// <param name="font">Шрифт</param>
        private void FontSet(Control control, Font font)
        {
            foreach (Control c in control.Controls)
                FontSet(c, font);
            control.Font = font;
        }

        /// <summary>
        /// Инверсия цвета
        /// </summary>
        /// <param name="input">Цвет на вход</param>
        /// <returns>Измененный цвет (или тот же если изменение не нужно)</returns>
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

        /// <summary>
        /// Изменение цвета элемента и дочерних элементов
        /// </summary>
        /// <param name="control">Элемент которому нужно изменить цвет</param>
        private void ColorSchemaSet(Control control)
        {
            control.BackColor = ColorSchemaChange(control.BackColor);
            control.ForeColor = ColorSchemaChange(control.ForeColor);
            foreach (Control c in control.Controls)
                ColorSchemaSet(c);
        }

        /// <summary>
        /// Защита от мерцания при Resize
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }

        /// <summary>
        /// Установление события "фокус на элементе" для элемента и дочерних элементов
        /// (для изменения цвета связанных элементов)
        /// </summary>
        /// <param name="control">Элемент, получивший фокус</param>
        private void ElementFocusSet(Control control)
        {
            foreach (Control c in control.Controls)
                ElementFocusSet(c);
            //если фокус поймал textbox
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

        /// <summary>
        /// Событие "фокус на элементе"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ElementFocus_Enter(object sender, EventArgs e)
        {
            var box = sender as TextBox;
            if (box != null)
                ElementFocus(box.Name, true);
            else if (sender is ListBox)
                ElementFocus(((ListBox) sender).Name, true);
        }
        
        /// <summary>
        /// Событие "фокус ушел с элемента"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ElementFocus_Leave(object sender, EventArgs e)
        {
            var box = sender as TextBox;
            if (box != null)
                ElementFocus(box.Name, false);
            else if (sender is ListBox)
                ElementFocus(((ListBox) sender).Name, false);
        }

        /// <summary>
        /// Изменение цвета связанных panel и button с этим элементом
        /// </summary>
        /// <param name="elementName">Элемент</param>
        /// <param name="focusSet">Фокус пойман или фокус уходит</param>
        private void ElementFocus(string elementName, bool focusSet)
        {
            var elementChange = Controls.Find(elementName, true);
            //если элемент не найден то возврат
            if (elementChange.Length <= 0) return;
            //если элемент в фокусе и фокус уходит то возврат
            if (elementChange[0].Focused && !focusSet) return;
            //префиксы связанных элементов в зависимости от типа элемента,
            //поймавшего фокус
            string[] elementNameChanger;
            if (elementChange[0] is TextBox)
                elementNameChanger = new[] {"tb", "tbp", "tbp1", "tbp2", "b1", "b"};
            else if (elementChange[0] is ListBox)
                elementNameChanger = new[] {"lb", "lbp", "lbp1", "lbp2", "b1", "b"};
            else
                return;
            var changedColor = focusSet ? _secondColor[_colorSchema] : _mainColor[_colorSchema];
            elementChange[0].ForeColor = focusSet ? changedColor : _foreColor[_colorSchema];
            //проход по связанным элементам, изменение их фонового цвета
            for (var i = 0; i < elementNameChanger.Length - 1; i++)
            {
                elementName = elementName.Replace(elementNameChanger[i], elementNameChanger[i + 1]);
                elementChange = Controls.Find(elementName, true);
                if (elementChange.Length > 0)
                    elementChange[0].BackColor = changedColor;
            }
        }

        /// <summary>
        /// Переопределение элемента paint listbox
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
                    new SolidBrush(isItemSelected ? _secondColor[_colorSchema] : _backColor[_colorSchema]);
                g.FillRectangle(backgroundColorBrush, e.Bounds);
                // Set text color
                var itemText = elementChange.Items[itemIndex].ToString();
                var itemTextColorBrush = new SolidBrush(_foreColor[_colorSchema]);
                var location = elementChange.GetItemRectangle(itemIndex).Location;
                g.DrawString(itemText, e.Font, itemTextColorBrush, location.X, location.Y + 4);
                backgroundColorBrush.Dispose();
                itemTextColorBrush.Dispose();
            }

            e.DrawFocusRectangle();
        }

        /// <summary>
        /// Загрузка прав пользователя и загрузка основной таблицы
        /// </summary>
        private void SetUser()
        {
            bEditBD.Visible = _userRights[0] != 0;
            if (_userRights[1] == 0)
            {
                bpOrderToData.Visible = false;
                bpToExcel.Visible = false;
                pMenuButtons.Top -= 120;
            }

            bDoTasks.Visible = _userRights[2] != 0;
            pFindNumber.Top = 31;
            pFindNumber.Height = Height - 32;
            pFindNumber.Left = 231;
            pFindNumber.Width = Width - 232;

            bEditBack.Visible = _userRights[3] != 0;
            SetPositions();
            //загрузка должностей на начальный экран
            RefreshPositions();
            tbPeopleChoose.Select();
        }

        /// <summary>
        /// Загрузка должностей
        /// </summary>
        private void SetPositions()
        {
            _positions.Clear();
            _sqlCommand = new SqlCommand("SELECT [id], [parent1], [parent2], [parent3], " +
                                         "[parent4], [primaryId], [name] FROM [Positions] ORDER BY [Position]",
                _sqlConnection);
            _sqlReader = _sqlCommand.ExecuteReader();
            var positionsId = new List<string>();
            while (_sqlReader.Read())
            {
                positionsId.Add(_sqlReader["id"].ToString());
                var parentMain = "";
                var parentOthers = "";
                if (_sqlReader["parent1"].ToString().Trim() != "")
                {
                    parentMain = _sqlReader["parent1"].ToString().Trim();
                    parentOthers = (_sqlReader["parent2"].ToString().Trim() == ""
                                       ? ""
                                       : _sqlReader["parent2"].ToString().Trim() + " | ") +
                                   (_sqlReader["parent3"].ToString().Trim() == ""
                                       ? ""
                                       : _sqlReader["parent3"].ToString().Trim() + " | ") +
                                   (_sqlReader["parent4"].ToString().Trim() == ""
                                       ? ""
                                       : _sqlReader["parent4"].ToString().Trim() + " | ");
                }
                else if (_sqlReader["parent2"].ToString().Trim() != "")
                {
                    parentMain = _sqlReader["parent2"].ToString().Trim();
                    parentOthers = (_sqlReader["parent3"].ToString().Trim() == ""
                                       ? ""
                                       : _sqlReader["parent3"].ToString().Trim() + " | ") +
                                   (_sqlReader["parent4"].ToString().Trim() == ""
                                       ? ""
                                       : _sqlReader["parent4"].ToString().Trim() + " | ");
                }
                else if (_sqlReader["parent3"].ToString().Trim() != "")
                {
                    parentMain = _sqlReader["parent3"].ToString().Trim();
                    parentOthers = _sqlReader["parent4"].ToString().Trim() == ""
                        ? ""
                        : _sqlReader["parent4"].ToString().Trim() + " | ";
                }
                else if (_sqlReader["parent4"].ToString().Trim() != "")
                {
                    parentMain = _sqlReader["parent4"].ToString().Trim();
                    parentOthers = "";
                }
                else
                {
                    parentMain = "Другое";
                    parentOthers = "";
                }

                if (_sqlReader["id"].ToString() != "2360")
                    _positions.Add(new[]
                    {
                        _sqlReader["id"].ToString(), parentMain, parentOthers, _sqlReader["name"].ToString(),
                        _sqlReader["parent1"].ToString(),
                        _sqlReader["parent2"].ToString(),
                        _sqlReader["parent3"].ToString(),
                        _sqlReader["parent4"].ToString(),
                        _sqlReader["primaryId"].ToString(),
                        "-1", "", "", "", ""
                    });
            }

            _sqlReader.Close();
            //загрузка фамилий к должностям
            _sqlCommand =
                new SqlCommand("SELECT [id], [fio0], [fio1], [fio2], [lNumber], [primaryId], [positionId], " +
                               "[gender] FROM [Peoples] ORDER BY [positionId]", _sqlConnection);
            _sqlReader = _sqlCommand.ExecuteReader();
            while (_sqlReader.Read())
            {
                var i = 0;
                var addThis = false;
                while (!addThis && i < _positions.Count)
                {
                    if (_sqlReader["positionId"].ToString() == "2360")
                    {
                        //люди без должностей
                        _positions.Add(new[]
                        {
                            "-1", "Другое", "", "",
                            "", "", "", "", "-1",
                            _sqlReader["id"].ToString(),
                            _sqlReader["fio0"] + " " + _sqlReader["fio1"] + " " + _sqlReader["fio2"],
                            _sqlReader["lNumber"].ToString(),
                            Convert.ToInt32(_sqlReader["gender"]).ToString(),
                            _sqlReader["primaryId"].ToString()
                        });
                        addThis = true;
                    }
                    else if (_positions[i][0] == _sqlReader["positionId"].ToString())
                    {
                        _positions[i][9] = _sqlReader["id"].ToString();
                        _positions[i][10] =
                            _sqlReader["fio0"] + " " + _sqlReader["fio1"] + " " + _sqlReader["fio2"];
                        _positions[i][11] = _sqlReader["lNumber"].ToString();
                        _positions[i][12] = Convert.ToInt32(_sqlReader["gender"]).ToString();
                        _positions[i][13] = _sqlReader["primaryId"].ToString();
                        addThis = true;
                    }

                    i++;
                }
            }

            _sqlReader.Close();
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            ChangeMenuSchema();
            foreach (Control control in Controls) ElementFocusSet(control);
        }

        //начальная загрузка
        private void RefreshPositions()
        { 
            //загрузка должностей в общий список и р1
            lbChooseParent.Items.Clear();
            lbChooseName.Items.Clear();
            var prevAddString = "";
            for (var i = 0; i < _positions.Count; i++)
            {
                if (_positions[i][1] == prevAddString ||
                    _positions[i][1] == "Другое" ||
                    _positions[i][1] == "В распоряжении") continue;
                lbChooseParent.Items.Add(_positions[i][1]);
                prevAddString = _positions[i][1];
            }

            lbChooseParent.SelectedIndex = -1;
            lbpChooseParent.Height = 5 + lbChooseParent.Items.Count * (30 + 1);
        }

        //выбор "по штату" наверх подняться
        private void ChooseParentUp()
        {
            if (_currentChoose < 0) return;
            _currentChoose--;
            switch (_currentChoose)
            {
                case 0:
                    RefreshPositions();
                    break;
                case 1:
                    if (_idChoose[1] == -1 || _positions[_idChoose[1]][4] == "")
                    {
                        ChooseParentUp();
                        break;
                    }

                    _currentChoose = 0;
                    lbChooseParent.Items.Clear();
                    lbChooseParent.Items.Add(_positions[_idChoose[0]][4]);
                    lbChooseParent.SelectedIndex = 0;
                    break;
                case 2:
                    if (_idChoose[2] == -1 || _positions[_idChoose[2]][5] == "")
                    {
                        ChooseParentUp();
                        break;
                    }

                    _currentChoose = 1;
                    lbChooseParent.Items.Clear();
                    lbChooseParent.Items.Add(_positions[_idChoose[1]][5]);
                    lbChooseParent.SelectedIndex = 0;
                    break;
                case 3:
                    if (_idChoose[3] == -1 || _positions[_idChoose[3]][6] == "")
                    {
                        ChooseParentUp();
                        break;
                    }

                    _currentChoose = 2;
                    lbChooseParent.Items.Clear();
                    lbChooseParent.Items.Add(_positions[_idChoose[2]][6]);
                    lbChooseParent.SelectedIndex = 0;
                    break;
            }
        }

        private void ChooseParent(bool chooseName, bool chooseFromLNumbers)
        {
            //загрузка следующего списка после выбранного
            if (chooseName)
            {
                if (!chooseFromLNumbers) return;
                if (lbChoosePeopleFind.SelectedIndex < 0 ||
                    lbChoosePeopleFindId.Items.Count < 1) return;
                //выбор человека из списка при поиске
                lbChoosePeopleFindId.SelectedIndex = lbChoosePeopleFind.SelectedIndex;
            }
            else
            {
                switch (_currentChoose)
                {
                    case 0:
                        _currentChoose = 1;
                        var endThis = false;
                        var tempStartInt = 0;
                        var i = 0;
                        while (!endThis && i < _positions.Count)
                        {
                            if (_positions[i][4] == (lbChooseParent.Text == "Отдельные" ? "" : lbChooseParent.Text))
                            {
                                endThis = true;
                                tempStartInt = i;
                                _idChoose[0] = i;
                            }

                            i++;
                        }

                        if (i == _positions.Count)
                        {
                            _idChoose[0] = -1;
                            ChooseParent(false, false);
                        }
                        else
                        {
                            i = tempStartInt;
                            lbChooseParent.Items.Clear();
                            endThis = false;
                            var prevParent = "";
                            while (!endThis && i < _positions.Count)
                            {
                                if (_positions[i][4] != _positions[_idChoose[0]][4])
                                {
                                    endThis = true;
                                }
                                else
                                {
                                    if (_positions[i][5] != prevParent)
                                    {
                                        prevParent = _positions[i][5];
                                        lbChooseParent.Items.Add(
                                            _positions[i][5] == "" ? "Отдельные" : _positions[i][5]);
                                    }
                                }

                                i++;
                            }

                            if (lbChooseParent.Items.Count == 1)
                                //go deeper
                            {
                                lbChooseParent.SelectedIndex = 0;
                            }
                            else
                            {
                                var tempEndInt = i;
                                i = tempStartInt;
                                lbChooseName.Items.Clear();
                                _idChooseName.Clear();
                                endThis = false;
                                while (!endThis && i < tempEndInt)
                                {
                                    if (_positions[i][1] != _positions[_idChoose[0]][4])
                                    {
                                        endThis = true;
                                    }
                                    else
                                    {
                                        lbChooseName.Items.Add(_positions[i][5] +
                                                               (_positions[i][6].Trim() != "Отдельные"
                                                                   ? " | " + _positions[i][6]
                                                                   : "") +
                                                               (_positions[i][7].Trim() != "Отдельные"
                                                                   ? " | " + _positions[i][7]
                                                                   : "") +
                                                               " | " + _positions[i][3] +
                                                               (_positions[i][10] != ""
                                                                   ? " - " + _positions[i][10]
                                                                   : ""));
                                        _idChooseName.Add(i);
                                    }

                                    i++;
                                }
                            }
                        }

                        break;
                    case 1:
                        _currentChoose = 2;
                        endThis = false;
                        tempStartInt = 0;
                        i = 0;
                        while (!endThis && i < _positions.Count)
                        {
                            if (i == 13)
                                Text = "12";
                            if (_positions[i][4] == (_idChoose[0] == -1 ? "" : _positions[_idChoose[0]][4]) &&
                                _positions[i][5] == (lbChooseParent.Text == "Отдельные" ? "" : lbChooseParent.Text))
                            {
                                endThis = true;
                                tempStartInt = i;
                                _idChoose[1] = i;
                            }

                            i++;
                        }

                        if (i == _positions.Count)
                        {
                            _idChoose[1] = -1;
                            ChooseParent(false, false);
                        }
                        else
                        {
                            i = tempStartInt;
                            lbChooseParent.Items.Clear();
                            endThis = false;
                            var prevParent = "";
                            while (!endThis && i < _positions.Count)
                            {
                                if (_positions[i][5] != _positions[_idChoose[1]][5])
                                {
                                    endThis = true;
                                }
                                else
                                {
                                    if (_positions[i][6] != prevParent)
                                    {
                                        prevParent = _positions[i][6];
                                        lbChooseParent.Items.Add(
                                            _positions[i][6] == "" ? "Отдельные" : _positions[i][6]);
                                    }
                                }

                                i++;
                            }

                            if (lbChooseParent.Items.Count == 1)
                                //go deeper
                            {
                                lbChooseParent.SelectedIndex = 0;
                            }
                            else
                            {
                                var tempEndInt = i;
                                i = tempStartInt;
                                lbChooseName.Items.Clear();
                                _idChooseName.Clear();
                                endThis = false;
                                while (!endThis && i < tempEndInt)
                                {
                                    if (_positions[i][5] != _positions[_idChoose[1]][5])
                                    {
                                        endThis = true;
                                    }
                                    else
                                    {
                                        lbChooseName.Items.Add(_positions[i][5] +
                                                               (_positions[i][6].Trim() != "Отдельные"
                                                                   ? " | " + _positions[i][6]
                                                                   : "") +
                                                               (_positions[i][7].Trim() != "Отдельные"
                                                                   ? " | " + _positions[i][7]
                                                                   : "") +
                                                               " | " + _positions[i][3] +
                                                               (_positions[i][10] != ""
                                                                   ? " - " + _positions[i][10]
                                                                   : ""));
                                        _idChooseName.Add(i);
                                    }

                                    i++;
                                }
                            }
                        }

                        break;
                    case 2:
                        _currentChoose = 3;
                        endThis = false;
                        tempStartInt = 0;
                        i = 0;
                        while (!endThis && i < _positions.Count)
                        {
                            if (_positions[i][4] == (_idChoose[0] == -1 ? "" : _positions[_idChoose[0]][4]) &&
                                _positions[i][5] == (_idChoose[1] == -1 ? "" : _positions[_idChoose[1]][5]) &&
                                _positions[i][6] == (lbChooseParent.Text == "Отдельные" ? "" : lbChooseParent.Text))
                            {
                                endThis = true;
                                tempStartInt = i;
                                _idChoose[2] = i;
                            }

                            i++;
                        }

                        if (i == _positions.Count)
                        {
                            //go deeper
                            _idChoose[2] = -1;
                            ChooseParent(false, false);
                        }
                        else
                        {
                            i = tempStartInt;
                            lbChooseParent.Items.Clear();
                            endThis = false;
                            var prevParent = "";
                            while (!endThis && i < _positions.Count)
                            {
                                if (_positions[i][6] != _positions[_idChoose[2]][6])
                                {
                                    endThis = true;
                                }
                                else
                                {
                                    if (_positions[i][7] != prevParent)
                                    {
                                        prevParent = _positions[i][7];
                                        lbChooseParent.Items.Add(
                                            _positions[i][7] == "" ? "Отдельные" : _positions[i][7]);
                                    }
                                }

                                i++;
                            }

                            if (lbChooseParent.Items.Count == 1)
                                //go deeper
                            {
                                lbChooseParent.SelectedIndex = 0;
                            }
                            else
                            {
                                var tempEndInt = i;
                                i = tempStartInt;
                                lbChooseName.Items.Clear();
                                _idChooseName.Clear();
                                endThis = false;
                                while (!endThis && i < tempEndInt)
                                {
                                    if (_positions[i][6] != _positions[_idChoose[2]][6])
                                    {
                                        endThis = true;
                                    }
                                    else
                                    {
                                        lbChooseName.Items.Add(_positions[i][5] +
                                                               (_positions[i][7].Trim() != "Отдельные"
                                                                   ? " | " + _positions[i][7]
                                                                   : "") +
                                                               " | " + _positions[i][3] +
                                                               (_positions[i][10] != "Отдельные"
                                                                   ? " - " + _positions[i][10]
                                                                   : ""));
                                        _idChooseName.Add(i);
                                    }

                                    i++;
                                }
                            }
                        }

                        break;
                    case 3:
                        _currentChoose = 4;
                        endThis = false;
                        tempStartInt = 0;
                        i = 0;
                        while (!endThis && i < _positions.Count)
                        {
                            if (_positions[i][4] == (_idChoose[0] == -1 ? "" : _positions[_idChoose[0]][4]) &&
                                _positions[i][5] == (_idChoose[1] == -1 ? "" : _positions[_idChoose[1]][5]) &&
                                _positions[i][6] == (_idChoose[2] == -1 ? "" : _positions[_idChoose[2]][6]) &&
                                _positions[i][7] == (lbChooseParent.Text == "Отдельные" ? "" : lbChooseParent.Text))
                            {
                                endThis = true;
                                tempStartInt = i;
                                _idChoose[3] = i;
                            }

                            i++;
                        }

                        i = tempStartInt;
                        endThis = false;
                        while (!endThis && i < _positions.Count)
                        {
                            if (_positions[i][7] != _positions[tempStartInt][7])
                                endThis = true;
                            i++;
                        }

                        var tempEndInt1 = i;
                        i = tempStartInt;
                        lbChooseName.Items.Clear();
                        _idChooseName.Clear();
                        endThis = false;
                        while (!endThis && i < tempEndInt1)
                        {
                            if (_positions[i][7] != _positions[_idChoose[3]][7])
                            {
                                endThis = true;
                            }
                            else
                            {
                                lbChooseName.Items.Add(_positions[i][3] +
                                                       (_positions[i][10] != ""
                                                           ? " - " + _positions[i][10]
                                                           : ""));
                                _idChooseName.Add(i);
                            }

                            i++;
                        }

                        break;
                }

                if (_currentChoose > 3)
                {
                    lbpChooseParent.Height = 0;
                    lbChooseName.Top = lbpChooseParent.Top;
                }
                else if (lbChooseParent.Items.Count > 0)
                {
                    lbpChooseParent.Height = 5 + lbChooseParent.Items.Count * (30 + 1);
                    lbChooseName.Top = lbpChooseParent.Top + lbChooseParent.Height;
                }
                else
                {
                    lbpChooseParent.Height = 0;
                    lbChooseName.Top = lbpChooseParent.Top;
                }

                lbChooseName.Height = pFindSchema.Height - lbChooseName.Top;
                lbChooseNameScroll.Top = lbChooseName.Top + 1;
                lbChooseNameScroll.Height = lbChooseName.Height - 2;
                lbChooseNameScroll.Maximum = lbChooseName.Items.Count - 1;
                lbChooseNameScroll.Visible = lbChooseNameScroll.Maximum * 30 > lbChooseName.Height;
                if (lbChooseNameScroll.Height < 5) lbChooseNameScroll.Height = 5;
                var step = lbChooseNameScroll.Maximum * 30 / lbChooseNameScroll.Height;
                if (step < 2) step = 2;
                step = Convert.ToInt32(lbChooseNameScroll.Height / step);
                if (step < 5) step = 5;
                lbChooseNameScroll.ThumbSize = step;
            }
        }

        private void Form1_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            //закрытие соединения с базой
            if (_sqlConnection != null && _sqlConnection.State != ConnectionState.Closed)
                _sqlConnection.Close();
            Application.Exit();
        }

        private void ChangeMenuSchema()
        {
            var pWidth = _menuSchema ? -160 : 160;
            bMenu.Image = _menuSchema ? Resources.sortmore : Resources.sortleft;
            pNavigationMenu.Width += pWidth;
            bFindLNumber.Text = _menuSchema ? "" : "        Поиск по ФИО";
            bFindSchema.Text = _menuSchema ? "" : "        Поиск по штату";
            bStatistic.Text = _menuSchema ? "" : "        Статистика";
            bOrderToData.Text = _menuSchema ? "" : "        Разнести приказ";
            bToExcel.Text = _menuSchema ? "" : "        Выгрузить в excel";
            bDoTasks.Width += pWidth;
            bEditBD.Width += pWidth;
            bEditBack.Width += pWidth;
            bFails.Width += pWidth;
            bPrint.Width += pWidth;
            pFindNumber.Left += pWidth;
            pFindNumber.Width -= pWidth;
            pFindSchema.Left = pFindNumber.Left;
            pFindSchema.Width = pFindNumber.Width;
            pStatistic.Left = pFindNumber.Left;
            pStatistic.Width = pFindNumber.Width;
            pOrderToData.Left = pFindNumber.Left;
            pOrderToData.Width = pFindNumber.Width;
            pToExcel.Left = pFindNumber.Left;
            pToExcel.Width = pFindNumber.Width;
            _menuSchema = !_menuSchema;
        }

        public void LoadFromConnect()
        {
            //начальное оформление
            _currentChoose = 0;
            pFindNumber.BringToFront();
            lbChooseExcelType.SelectedIndex = 0;
            lbCalcCategory.SelectedIndex = 1;
            _canToExcelDo = true;
            _sqlConnection = new SqlConnection(_sqlConnectionString);
            _sqlConnection.Open();
            SetUser();
            pFindSchema.Top = pFindNumber.Top;
            pFindSchema.Left = pFindNumber.Left;
            pFindSchema.Height = pFindNumber.Height;
            pFindSchema.Width = pFindNumber.Width;
            pStatistic.Top = pFindNumber.Top;
            pStatistic.Left = pFindNumber.Left;
            pStatistic.Height = pFindNumber.Height;
            pStatistic.Width = pFindNumber.Width;
            pOrderToData.Top = pFindNumber.Top;
            pOrderToData.Left = pFindNumber.Left;
            pOrderToData.Height = pFindNumber.Height;
            pOrderToData.Width = pFindNumber.Width;
            pToExcel.Top = pFindNumber.Top;
            pToExcel.Left = pFindNumber.Left;
            pToExcel.Height = pFindNumber.Height;
            pToExcel.Width = pFindNumber.Width;
        }

        /// <summary>
        ///     Открываем следующую форму
        /// </summary>
        /// <param name="stateType">Панель поиска по личному номеру или по штату</param>
        private void ChangeCurrentPosition(bool stateType)
        {
            //0 - 1 панель, 1 - вторая
            try
            {
                if (stateType && lbChoosePeopleFind.SelectedIndex < 0 ||
                    !stateType && _positions[_idChooseName[lbChooseName.SelectedIndex]][9] == "-1")
                    return;
                if (_userRights[2] == 0)
                {
                    //постановка задач гостями после активации
                    var tasksForm = new TasksForm(_positions)
                    { Left = Left, Top = Top, Height = Height, Width = Width };
                    tasksForm.LoadFromSQL(_sqlConnectionString, _userName, _userRights[4],
                        Convert.ToInt32(stateType
                            ? lbChoosePeopleFindId.Text
                            : _positions[_idChooseName[lbChooseName.SelectedIndex]][9]), stateType
                            ? lbChoosePeopleFind.Text
                            : lbChooseName.Text, _colorSchema);
                    Hide();
                    tasksForm.Closed += (s, args) =>
                    {
                        Left = tasksForm.Left;
                        Top = tasksForm.Top;
                        Height = tasksForm.Height;
                        Width = tasksForm.Width;
                        WindowState = tasksForm.WindowState;
                        Show();
                        Refresh();
                    };
                    tasksForm.Show();
                }
                else
                {
                    //редактирование человека
                    var editPeopleForm = new EditPeopleForm(_positions)
                        {Left = Left, Top = Top, Height = Height, Width = Width};
                    editPeopleForm.LoadFromSQL(_sqlConnectionString, _userName,
                        _userRights[4], Convert.ToInt32(stateType
                            ? lbChoosePeopleFindId.Text
                            : _positions[_idChooseName[lbChooseName.SelectedIndex]][9]), _colorSchema, _menuSchema);
                    Hide();
                    editPeopleForm.Closed += (s, args) =>
                    {
                        if (editPeopleForm._menuSchema != _menuSchema)
                            ChangeMenuSchema();
                        Left = editPeopleForm.Left;
                        Top = editPeopleForm.Top;
                        Height = editPeopleForm.Height;
                        Width = editPeopleForm.Width;
                        WindowState = editPeopleForm.WindowState;
                        //обновление должностей
                        SetPositions();
                        //RefreshPositions();
                        //tbPeopleChoose.Text = "";
                        //while (_currentChoose > 0)
                        //    ChooseParentUp();
                        Show();
                        Refresh();
                    };
                    editPeopleForm.Show(this);
                }
            }
            catch
            {
                // ignored
            }
        }

        private void BEditBack_Click(object sender, EventArgs e)
        {
            //экран добавления человека
            var editPeopleForm = new EditPeopleForm(_positions)
            {
                Left = Left,
                Top = Top,
                Height = Height,
                Width = Width
            };
            editPeopleForm.LoadFromSQL(_sqlConnectionString, _userName,
                _userRights[4], -1, _colorSchema, _menuSchema);
            Hide();
            editPeopleForm.Closed += (s, args) =>
            {
                if (editPeopleForm._menuSchema != _menuSchema)
                    ChangeMenuSchema();
                Left = editPeopleForm.Left;
                Top = editPeopleForm.Top;
                Height = editPeopleForm.Height;
                Width = editPeopleForm.Width;
                WindowState = editPeopleForm.WindowState;
                //обновление должностей
                SetPositions();
                //RefreshPositions();
                //tbPeopleChoose.Text = "";
                //while (_currentChoose > 0)
                //    ChooseParentUp();
                Show();
                Refresh();
            };
            editPeopleForm.Show(this);
        }

        private void LbChooseName_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ChooseParent(true, false);
            lbChooseNameScroll.Value = lbChooseName.SelectedIndex;
        }

        private void FindPeople()
        {
            //ввод имени человека для выбора из списка
            lbChoosePeopleFind.Items.Clear();
            lbChoosePeopleFindId.Items.Clear();
            var findString = tbPeopleChoose.Text.ToLower();
            for (var i = 0; i < _positions.Count; i++)
            {
                var complexNameString = _positions[i][10] + " | " + _positions[i][11];
                if (complexNameString.ToLower().IndexOf(findString, StringComparison.Ordinal) <= -1) continue;
                lbChoosePeopleFind.Items.Add(complexNameString);
                lbChoosePeopleFindId.Items.Add(_positions[i][9]);
            }

            lbChoosePeopleFindScroll.Maximum = lbChoosePeopleFind.Items.Count - 1;
            if (lbChoosePeopleFindScroll.Height < 5) lbChoosePeopleFindScroll.Height = 5;
            var step = lbChoosePeopleFindScroll.Maximum * 30 / lbChoosePeopleFindScroll.Height;
            if (step < 2) step = 2;
            step = Convert.ToInt32(lbChoosePeopleFindScroll.Height / step);
            if (step < 5) step = 5;
            lbChoosePeopleFindScroll.ThumbSize = step;
            lbChoosePeopleFindScroll.Visible = lbChoosePeopleFindScroll.Maximum * 30 > lbChoosePeopleFindScroll.Height;

            if (lbChoosePeopleFind.Items.Count > 0)
            {
                lbChoosePeopleFind.SelectedIndex = 0;
                lbChoosePeopleFind.Select();
            }
        }

        private void BChooseFind_Click(object sender, EventArgs e)
        {
            FindPeople();
        }

        private void BDoTasks_Click(object sender, EventArgs e)
        {
            //выполнение задач пользователями
            var tasksForm = new TasksForm(_positions) {Left = Left, Top = Top, Height = Height, Width = Width};
            tasksForm.LoadFromSQL(_sqlConnectionString, _userName,
                _userRights[4], -1, "", _colorSchema);
            Hide();
            tasksForm.Closed += (s, args) =>
            {
                Left = tasksForm.Left;
                Top = tasksForm.Top;
                Height = tasksForm.Height;
                Width = tasksForm.Width;
                WindowState = tasksForm.WindowState;
                Show();
                Refresh();
            };
            tasksForm.Show();
        }

        private void LbChoosePeopleFind_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbChoosePeopleFind.SelectedIndex <= -1) return;
            ChooseParent(true, true);
            lbChoosePeopleFindScroll.Value = lbChoosePeopleFind.SelectedIndex;
        }

        private void ExcelLoad()
        {
            var type = lbChooseExcelType.Text == "В штатку для листа Данные" ? 1 : 0;
            if (lbChooseExcelType.SelectedIndex == -1 && type != 1) return;
            if (!_categoryPeoples[0] && !_categoryPeoples[1] &&
                !_categoryPeoples[2] && !_categoryPeoples[3]) return;

            //добавляем id званий согласно выбранного списка
            lbChooseExcelPrimaryId.Items.Clear();
            _sqlCommand =
                new SqlCommand("SELECT [id], [type] FROM [Primary]",
                    _sqlConnection);
            _sqlReader = _sqlCommand.ExecuteReader();
            while (_sqlReader.Read())
                if (_categoryPeoples[Convert.ToInt32(_sqlReader["type"])])
                    lbChooseExcelPrimaryId.Items.Add(_sqlReader["id"].ToString());

            _sqlReader.Close();
            var clipString = "";
            var iTemp = 1;
            lbTemp.Items.Clear();
            lbTemp1.Items.Clear();
            if (type != 1)
                switch (lbChooseExcelType.SelectedIndex)
                {
                    //большая выборка
                    case 0:
                    case 1:
                    {
                        lbTemp2.Items.Clear();
                        lbTemp3.Items.Clear();
                        //читаем должности
                        _sqlCommand =
                            new SqlCommand(
                                "SELECT * FROM [Positions] ORDER BY [position]",
                                _sqlConnection);
                        _sqlReader = _sqlCommand.ExecuteReader();
                        while (_sqlReader.Read())
                        {
                            //подходит ли звание должности под выбранные звания
                            var addThis = false;
                            var i = 0;
                            while (!addThis && i < lbChooseExcelPrimaryId.Items.Count)
                            {
                                if (_sqlReader["primaryId"].ToString() == lbChooseExcelPrimaryId.Items[i].ToString())
                                    addThis = true;
                                i++;
                            }

                            if (!addThis) continue;
                            //если подходит собираем все в куски
                            lbTemp.Items.Add(iTemp + "\t" + _sqlReader["parent1"] + "\t" + _sqlReader["parent2"] +
                                             "\t" + _sqlReader["parent3"] + "\t" + _sqlReader["parent4"] + "\t" +
                                             _sqlReader["name"] + "\t");
                            //без вусов
                            if (lbChooseExcelType.SelectedIndex == 0)
                            {
                                lbTemp1.Items.Add(_sqlReader["fullName"] + "\t\t\t");
                            }
                            //с вусами
                            else
                            {
                                lbTemp3.Items.Add(_sqlReader["primaryId"].ToString());
                                lbTemp1.Items.Add(_sqlReader["fullName"] + "\t" + _sqlReader["vus"] + "\t" +
                                                  _sqlReader["tarif"] + "\t");
                            }

                            iTemp++;
                            lbTemp2.Items.Add(_sqlReader["id"].ToString());
                        }

                        _sqlReader.Close();
                        //преобразуем id званий в названия званий
                        if (lbChooseExcelType.SelectedIndex == 1)
                        {
                            for (var i = 0; i < lbTemp2.Items.Count; i++)
                            {
                                _sqlCommand =
                                    new SqlCommand("SELECT [name] FROM [Primary] WHERE [id]=@id",
                                        _sqlConnection);
                                _sqlCommand.Parameters.AddWithValue("id", lbTemp3.Items[i]);
                                _sqlReader = _sqlCommand.ExecuteReader();
                                _sqlReader.Read();
                                var primaryName = _sqlReader["name"].ToString().Replace("старший ", "ст.");
                                _sqlReader.Close();
                                primaryName = primaryName.Replace("младший ", "мл.");
                                lbTemp1.Items[i] += primaryName;
                            }

                            _sqlReader?.Close();
                        }

                        iTemp--;
                        //добавляем сведения о человеке к должности
                        while (iTemp > 0)
                        {
                            iTemp--;
                            _sqlCommand =
                                new SqlCommand(
                                    "SELECT * FROM [Peoples] WHERE [positionId]=@positionId",
                                    _sqlConnection);
                            _sqlCommand.Parameters.AddWithValue("positionId", Convert.ToInt32(lbTemp2.Items[iTemp]));
                            _sqlReader = _sqlCommand.ExecuteReader();
                            //если не вакант
                            if (_sqlReader.HasRows)
                            {
                                //ФИО и все что содержится в Peoples
                                _sqlReader.Read();
                                lbTemp2.Items[iTemp] = _sqlReader["fio0"] + " " + _sqlReader["fio1"] + " " +
                                                       _sqlReader["fio2"] + "\t\t" + " " + _sqlReader["lNumber"] +
                                                       "\t" +
                                                       Convert.ToDateTime(_sqlReader["dateBirthday"])
                                                           .ToString("dd.MM.yyyy") + "\t" +
                                                       _sqlReader["placeBirthday"] + "\t";
                                var peopleId = Convert.ToInt32(_sqlReader["id"]);
                                var primaryId = Convert.ToInt32(_sqlReader["primaryId"]);
                                var primaryOrderId = Convert.ToInt32(_sqlReader["primaryOrderId"]);
                                var positionOrderId = Convert.ToInt32(_sqlReader["positionOrderId"]);
                                var phoneNumber = _sqlReader["phoneNumber"].ToString();
                                var gender = Convert.ToBoolean(_sqlReader["gender"]) ? "Ж" : "М";
                                var start = Convert.ToDateTime(_sqlReader["start"]);
                                lbTemp2.Items[iTemp] = lbTemp2.Items[iTemp] + phoneNumber + "\t" +
                                                       gender + "\t" + start + "\t";
                                _sqlReader.Close();
                                //преобразуем звание
                                _sqlCommand =
                                    new SqlCommand("SELECT [name] FROM [Primary] WHERE [id]=@id",
                                        _sqlConnection);
                                _sqlCommand.Parameters.AddWithValue("id", primaryId);
                                _sqlReader = _sqlCommand.ExecuteReader();
                                _sqlReader.Read();
                                var primaryName = _sqlReader["name"].ToString().Replace("старший ", "ст.");
                                primaryName = primaryName.Replace("младший ", "мл.");

                                _sqlReader.Close();
                                //ищем приказ на звание
                                _sqlCommand =
                                    new SqlCommand("SELECT [name], [number], [date] FROM [Orders] WHERE [id]=@id",
                                        _sqlConnection);
                                _sqlCommand.Parameters.AddWithValue("id", primaryOrderId);
                                _sqlReader = _sqlCommand.ExecuteReader();
                                _sqlReader.Read();
                                var primaryOrder = _sqlReader.HasRows
                                    ? "Приказ " + _sqlReader["name"] + " от " +
                                      Convert.ToDateTime(_sqlReader["date"]).ToString("dd.MM.yyyy") +
                                      " №" + _sqlReader["number"]
                                    : "";

                                _sqlReader.Close();
                                //ищем приказ на должность
                                _sqlCommand =
                                    new SqlCommand("SELECT [name], [number], [date] FROM [Orders] WHERE [id]=@id",
                                        _sqlConnection);
                                _sqlCommand.Parameters.AddWithValue("id", positionOrderId);
                                _sqlReader = _sqlCommand.ExecuteReader();
                                _sqlReader.Read();
                                var positionOrder = _sqlReader.HasRows
                                    ? "Приказ " + _sqlReader["name"] + " от " +
                                      Convert.ToDateTime(_sqlReader["date"]).ToString("dd.MM.yyyy") +
                                      " №" + _sqlReader["number"]
                                    : "";

                                _sqlReader.Close();
                                //собираем контракт человека
                                _sqlCommand =
                                    new SqlCommand(
                                        "SELECT [slaveStart], [slaveEnd], [orderId] FROM [Slaves] WHERE [peopleId]=@peopleId ORDER BY slaveEnd DESC",
                                        _sqlConnection);
                                _sqlCommand.Parameters.AddWithValue("peopleId", peopleId);
                                _sqlReader = _sqlCommand.ExecuteReader();
                                var slaveStart = "";
                                var slaveEnd = "";
                                var slaveOrder = "";
                                if (_sqlReader.HasRows)
                                    while (_sqlReader.Read())
                                    {
                                        slaveStart = Convert.ToDateTime(_sqlReader["slaveStart"])
                                            .ToString("dd.MM.yyyy");
                                        slaveEnd = Convert.ToDateTime(_sqlReader["slaveEnd"]).ToString("dd.MM.yyyy");
                                        var slaveOrderId = _sqlReader["orderId"].ToString();
                                        _sqlReader.Close();
                                        //ищем приказ на контракт
                                        _sqlCommand =
                                            new SqlCommand(
                                                "SELECT [name], [number], [date] FROM [Orders] WHERE [id]=@id",
                                                _sqlConnection);
                                        _sqlCommand.Parameters.AddWithValue("id", slaveOrderId);
                                        _sqlReader = _sqlCommand.ExecuteReader();
                                        _sqlReader.Read();
                                        slaveOrder = _sqlReader.HasRows
                                            ? "Приказ " + _sqlReader["name"] + " от " +
                                              Convert.ToDateTime(_sqlReader["date"]).ToString("dd.MM.yyyy") +
                                              " №" + _sqlReader["number"]
                                            : "";
                                    }

                                _sqlReader.Close();
                                _sqlCommand =
                                    new SqlCommand(
                                        "SELECT [position], [name], [dateBirthday] FROM [Family] WHERE [peopleId]=@peopleId",
                                        _sqlConnection);
                                _sqlCommand.Parameters.AddWithValue("peopleId", peopleId);
                                _sqlReader = _sqlCommand.ExecuteReader();
                                var family = "";
                                if (_sqlReader.HasRows)
                                    //собираем всех членов семьи
                                    while (_sqlReader.Read())
                                    {
                                        if (family.Length > 0)
                                            family += "; ";
                                        family += _sqlReader["position"] + " – " +
                                                  _sqlReader["name"] + ", " +
                                                  Convert.ToDateTime(_sqlReader["dateBirthday"]).ToString("dd.MM.yyyy");
                                    }

                                _sqlReader.Close();
                                _sqlCommand =
                                    new SqlCommand(
                                        "SELECT [name], [dateText] FROM [Battlefields] WHERE [peopleId]=@peopleId",
                                        _sqlConnection);
                                _sqlCommand.Parameters.AddWithValue("peopleId", peopleId);
                                _sqlReader = _sqlCommand.ExecuteReader();
                                var battlefields = "";
                                if (_sqlReader.HasRows)
                                    //собираем все горячие точки
                                    while (_sqlReader.Read())
                                    {
                                        if (battlefields.Length > 0)
                                            battlefields += "; ";
                                        battlefields += _sqlReader["name"] + " " + _sqlReader["dateText"];
                                    }

                                _sqlReader.Close();
                                _sqlCommand =
                                    new SqlCommand(
                                        "SELECT [name], [orderId] FROM [Medals] WHERE [peopleId]=@peopleId ORDER BY [orderId]",
                                        _sqlConnection);
                                _sqlCommand.Parameters.AddWithValue("peopleId", peopleId);
                                _sqlReader = _sqlCommand.ExecuteReader();
                                var medals = "";
                                var medalsNames = new List<string>();
                                var medalsOrdersId = new List<int>();
                                if (_sqlReader.HasRows)
                                    //собираем все медали
                                    while (_sqlReader.Read())
                                    {
                                        medalsNames.Add(_sqlReader["name"].ToString());
                                        medalsOrdersId.Add(Convert.ToInt32(_sqlReader["orderId"]));
                                    }
                                _sqlReader.Close();

                                for (var i = 0; i < medalsNames.Count; i++)
                                {
                                    //ищем приказ на медаль
                                    _sqlCommand =
                                        new SqlCommand("SELECT [date] FROM [Orders] WHERE [id]=@id",
                                            _sqlConnection);
                                    _sqlCommand.Parameters.AddWithValue("id", medalsOrdersId[i]);
                                    _sqlReader = _sqlCommand.ExecuteReader();
                                    _sqlReader.Read();
                                    medals += _sqlReader.HasRows
                                        ? medalsNames[i] + (medals == "" ? "" : ", ") +
                                          Convert.ToDateTime(_sqlReader["date"]).ToString("yyyy") + " г."
                                        : "";
                                _sqlReader.Close();
                                }

                                //читаем последнее образование
                                _sqlCommand =
                                    new SqlCommand(
                                        "SELECT [name], [year] FROM [Educations] WHERE [peopleId]=@peopleId ORDER BY [year] DESC",
                                        _sqlConnection);
                                _sqlCommand.Parameters.AddWithValue("peopleId", peopleId);
                                _sqlReader = _sqlCommand.ExecuteReader();
                                _sqlReader.Read();
                                var educations = _sqlReader.HasRows
                                    ? "\t" + _sqlReader["name"] + "\t" + _sqlReader["year"]
                                    : "\t\t";
                                _sqlReader.Close();

                                //собираем все в кучу
                                lbTemp.Items[iTemp] +=
                                    primaryName + "\t" + lbTemp2.Items[iTemp] + positionOrder + "\t" +
                                    lbTemp1.Items[iTemp] + educations + "\t" + primaryOrder + "\t" +
                                    slaveStart + "\t" + slaveEnd + "\t" + slaveOrder + "\t" +
                                    family + "\t" + battlefields + "\t" + medals;
                            }
                            else
                            {
                                lbTemp.Items[iTemp] += "\tВАКАНТ";
                            }

                            lbTemp.Items[iTemp] += "\n";

                            _sqlReader?.Close();
                        }

                        break;
                    }

                    //основные сведения в шдс
                    case 2:
                    {
                        _sqlCommand =
                            new SqlCommand(
                                "SELECT [id], [parent1], [parent2], [parent3], [parent4], [name], [primaryId] FROM [Positions] ORDER BY [position]",
                                _sqlConnection);
                        _sqlReader = _sqlCommand.ExecuteReader();
                        while (_sqlReader.Read())
                        {
                            var addThis = false;
                            var i = 0;
                            while (!addThis && i < lbChooseExcelPrimaryId.Items.Count)
                            {
                                if (_sqlReader["primaryId"].ToString() == lbChooseExcelPrimaryId.Items[i].ToString())
                                    addThis = true;
                                i++;
                            }

                            if (!addThis) continue;
                            lbTemp.Items.Add(iTemp + "\t" + _sqlReader["parent1"] + "\t" + _sqlReader["parent2"] +
                                             "\t" +
                                             _sqlReader["parent3"] + "\t" + _sqlReader["parent4"] + "\t" +
                                             _sqlReader["name"] +
                                             "\t");
                            iTemp++;
                            lbTemp1.Items.Add(_sqlReader["id"].ToString());
                        }

                        _sqlReader.Close();
                        iTemp--;

                        while (iTemp > 0)
                        {
                            iTemp--;
                            _sqlCommand =
                                new SqlCommand(
                                    "SELECT [primaryId], [fio0], [fio1], [fio2] FROM [Peoples] WHERE [positionId]=@positionId",
                                    _sqlConnection);
                            _sqlCommand.Parameters.AddWithValue("positionId",
                                Convert.ToInt32(lbTemp1.Items[iTemp].ToString()));
                            _sqlReader = _sqlCommand.ExecuteReader();
                            if (_sqlReader.HasRows)
                            {
                                _sqlReader.Read();
                                lbTemp1.Items[iTemp] = "\t" + _sqlReader["fio0"] + " " + _sqlReader["fio1"] + " " +
                                                       _sqlReader["fio2"];
                                var primaryId = Convert.ToInt32(_sqlReader["primaryId"]);
                                _sqlReader.Close();
                                _sqlCommand =
                                    new SqlCommand("SELECT [name] FROM [Primary] WHERE [id]=@id",
                                        _sqlConnection);
                                _sqlCommand.Parameters.AddWithValue("id", primaryId);
                                _sqlReader = _sqlCommand.ExecuteReader();
                                _sqlReader.Read();
                                var primaryName = _sqlReader["name"].ToString().Replace("старший ", "ст.");
                                primaryName = primaryName.Replace("младший ", "мл.");
                                lbTemp1.Items[iTemp] = primaryName + lbTemp1.Items[iTemp];
                            }
                            else
                            {
                                lbTemp1.Items[iTemp] = "\tВАКАНТ";
                            }

                            _sqlReader.Close();

                            lbTemp.Items[iTemp] += lbTemp1.Items[iTemp] + "\n";
                        }

                        break;
                    }

                    //основные сведения в шдс (в читаемом виде)
                    case 3:
                    {
                        _sqlCommand =
                            new SqlCommand(
                                "SELECT [id], [parent1], [parent2], [parent3], [name], [primaryId] FROM [Positions] ORDER BY [position]",
                                _sqlConnection);
                        _sqlReader = _sqlCommand.ExecuteReader();
                        var prevParent1 = "-1";
                        var prevParent2 = "-1";
                        var prevParent3 = "-1";
                        var prevParent1Boolean = false;
                        while (_sqlReader.Read())
                        {
                            var addThis = false;
                            var i = 0;
                            while (!addThis && i < lbChooseExcelPrimaryId.Items.Count)
                            {
                                if (_sqlReader["primaryId"].ToString() == lbChooseExcelPrimaryId.Items[i].ToString())
                                    addThis = true;
                                i++;
                            }

                            //если звание подходит
                            if (!addThis) continue;
                            //если батальон не тот же что был раньше
                            if (prevParent1 != _sqlReader["parent1"].ToString() &&
                                _sqlReader["parent1"].ToString().Trim() != "" &&
                                _sqlReader["parent1"].ToString() != "Командование")
                            {
                                //пишем название батальона
                                lbTemp.Items.Add("\t" + _sqlReader["parent1"]);
                                lbTemp1.Items.Add("-1");
                                prevParent1 = _sqlReader["parent1"].ToString();
                            }

                            //если батальон не та же что была раньше
                            if (prevParent2 != _sqlReader["parent2"].ToString() &&
                                _sqlReader["parent2"].ToString().Trim() != "" &&
                                _sqlReader["parent2"].ToString() != "Командование")
                            {
                                //пишем название роты
                                lbTemp.Items.Add("\t" + _sqlReader["parent2"]);
                                lbTemp1.Items.Add("-1");
                                prevParent2 = _sqlReader["parent2"].ToString();
                            }

                            //если взвод не тот же что был раньше
                            //и батальон известен, а рота неизвестна (отдельный взвод в батальоне)
                            if (prevParent3 != _sqlReader["parent3"].ToString() &&
                                _sqlReader["parent3"].ToString().Trim() != "" &&
                                _sqlReader["parent1"].ToString().Trim() != "" &&
                                _sqlReader["parent2"].ToString().Trim() == "")
                            {
                                //если надписи "отдельные взвода" нет
                                if (prevParent1Boolean == false)
                                {
                                    lbTemp.Items.Add("\tОтдельные взвода");
                                    lbTemp1.Items.Add("-1");
                                }

                                prevParent1Boolean = true;
                            }
                            else
                            {
                                prevParent1Boolean = false;
                            }
                            
                            //если взвод не тот же что был раньше
                            //и батальон и рота пустые (отдельный взвод в бригаде)
                            if (prevParent3 != _sqlReader["parent3"].ToString() &&
                                _sqlReader["parent3"].ToString().Trim() != "" &&
                                _sqlReader["parent1"].ToString().Trim() == "" &&
                                _sqlReader["parent2"].ToString().Trim() == "")
                            {
                                lbTemp.Items.Add("\t" + _sqlReader["parent3"]);
                                lbTemp1.Items.Add("-1");
                                prevParent3 = _sqlReader["parent3"].ToString();
                            }

                            //пишем название
                            lbTemp.Items.Add(iTemp + "\t" + _sqlReader["name"]);
                            //id вместо номера по порядку
                            //lbTemp.Items.Add(_sqlReader["id"] + "\t" + _sqlReader["name"]);
                            iTemp++;
                            lbTemp1.Items.Add(_sqlReader["id"].ToString());
                        }

                        _sqlReader.Close();

                        for (var i = 0; i < lbTemp1.Items.Count; i++)
                        {
                            if ((string) lbTemp1.Items[i] != "-1")
                            {
                                _sqlCommand =
                                    new SqlCommand(
                                        "SELECT [primaryId], [fio0], [fio1], [fio2] FROM [Peoples] WHERE [positionId]=@positionId",
                                        _sqlConnection);
                                _sqlCommand.Parameters.AddWithValue("positionId",
                                    Convert.ToInt32(lbTemp1.Items[i].ToString()));
                                _sqlReader = _sqlCommand.ExecuteReader();
                                if (_sqlReader.HasRows)
                                {
                                    _sqlReader.Read();
                                    lbTemp1.Items[i] = "\t" + _sqlReader["fio0"] + " " + _sqlReader["fio1"] + " " +
                                                       _sqlReader["fio2"];
                                    var primaryId = Convert.ToInt32(_sqlReader["primaryId"]);
                                    _sqlReader.Close();
                                    _sqlCommand =
                                        new SqlCommand("SELECT [name] FROM [Primary] WHERE [id]=@id",
                                            _sqlConnection);
                                    _sqlCommand.Parameters.AddWithValue("id", primaryId);
                                    _sqlReader = _sqlCommand.ExecuteReader();
                                    _sqlReader.Read();
                                    var primaryName = _sqlReader["name"].ToString().Replace("старший ", "ст.");
                                    primaryName = primaryName.Replace("младший ", "мл.");
                                    lbTemp1.Items[i] = "\t" + primaryName + lbTemp1.Items[i];
                                }
                                else
                                {
                                    lbTemp1.Items[i] = "\t\tВАКАНТ";
                                }

                                lbTemp.Items[i] += lbTemp1.Items[i].ToString();
                                _sqlReader.Close();
                            }

                            lbTemp.Items[i] += "\n";
                        }

                        break;
                    }

                    //штатка в шдс (с раскрытием до конца)
                    case 4:
                    {
                        _sqlCommand =
                            new SqlCommand(
                                "SELECT [id], [parent1], [parent2], [parent3], [parent4], [name], [primaryId] FROM [Positions] ORDER BY [position]",
                                _sqlConnection);
                        _sqlReader = _sqlCommand.ExecuteReader();
                        var prevParent1 = "-1";
                        var prevParent2 = "-1";
                        var prevParent3 = "-1";
                        var prevParent4 = "-1";
                        while (_sqlReader.Read())
                        {
                            var addThis = false;
                            var i = 0;
                            while (!addThis && i < lbChooseExcelPrimaryId.Items.Count)
                            {
                                if (_sqlReader["primaryId"].ToString() == lbChooseExcelPrimaryId.Items[i].ToString())
                                    addThis = true;
                                i++;
                            }

                            //если звание подходит
                            if (!addThis) continue;
                            //если батальон тот же что был раньше
                            if (prevParent1 != _sqlReader["parent1"].ToString() &&
                                _sqlReader["parent1"].ToString().Trim() != "" &&
                                _sqlReader["parent1"].ToString() != "Командование")
                            {
                                lbTemp.Items.Add("\t" + _sqlReader["parent1"]);
                                lbTemp1.Items.Add("-1");
                                prevParent1 = _sqlReader["parent1"].ToString();
                            }

                            //рота
                            if (prevParent2 != _sqlReader["parent2"].ToString() &&
                                _sqlReader["parent2"].ToString().Trim() != "" &&
                                _sqlReader["parent2"].ToString() != "Командование")
                            {
                                lbTemp.Items.Add("\t" + _sqlReader["parent2"]);
                                lbTemp1.Items.Add("-1");
                                prevParent2 = _sqlReader["parent2"].ToString();
                            }

                            //взвод
                            if (prevParent3 != _sqlReader["parent3"].ToString() &&
                                _sqlReader["parent3"].ToString().Trim() != "" &&
                                _sqlReader["parent3"].ToString() != "Командование")
                            {
                                lbTemp.Items.Add("\t" + _sqlReader["parent3"]);
                                lbTemp1.Items.Add("-1");
                                prevParent3 = _sqlReader["parent3"].ToString();
                            }

                            //отделение
                            if (prevParent4 != _sqlReader["parent4"].ToString() &&
                                _sqlReader["parent4"].ToString().Trim() != "")
                            {
                                lbTemp.Items.Add("\t" + _sqlReader["parent4"]);
                                lbTemp1.Items.Add("-1");
                                prevParent4 = _sqlReader["parent4"].ToString();
                            }

                            lbTemp.Items.Add(_sqlReader["id"] + "\t" + _sqlReader["name"]);
                            iTemp++;
                            lbTemp1.Items.Add(_sqlReader["id"].ToString());
                        }

                        _sqlReader.Close();

                        for (var i = 0; i < lbTemp1.Items.Count; i++)
                        {
                            if ((string) lbTemp1.Items[i] != "-1")
                            {
                                _sqlCommand =
                                    new SqlCommand(
                                        "SELECT [primaryId], [fio0], [fio1], [fio2] FROM [Peoples] WHERE [positionId]=@positionId",
                                        _sqlConnection);
                                _sqlCommand.Parameters.AddWithValue("positionId",
                                    Convert.ToInt32(lbTemp1.Items[i].ToString()));
                                _sqlReader = _sqlCommand.ExecuteReader();
                                if (_sqlReader.HasRows)
                                {
                                    _sqlReader.Read();
                                    lbTemp1.Items[i] = "\t" + _sqlReader["fio0"] + " " + _sqlReader["fio1"] + " " +
                                                       _sqlReader["fio2"];
                                    var primaryId = Convert.ToInt32(_sqlReader["primaryId"]);
                                    _sqlReader.Close();
                                    _sqlCommand =
                                        new SqlCommand("SELECT [name] FROM [Primary] WHERE [id]=@id",
                                            _sqlConnection);
                                    _sqlCommand.Parameters.AddWithValue("id", primaryId);
                                    _sqlReader = _sqlCommand.ExecuteReader();
                                    _sqlReader.Read();
                                    var primaryName = _sqlReader["name"].ToString().Replace("старший ", "ст.");
                                    primaryName = primaryName.Replace("младший ", "мл.");
                                    lbTemp1.Items[i] = "\t" + primaryName + lbTemp1.Items[i];
                                }
                                else
                                {
                                    lbTemp1.Items[i] = "\t\tВАКАНТ";
                                }

                                lbTemp.Items[i] += lbTemp1.Items[i].ToString();
                                _sqlReader.Close();
                            }

                            lbTemp.Items[i] += "\n";
                        }

                        break;
                    }
                    
                    case 6:
                    {
                        lbTemp2.Items.Clear();
                        lbTemp3.Items.Clear();
                        //читаем должности
                        _sqlCommand =
                            new SqlCommand(
                                "SELECT * FROM [Positions] ORDER BY [position]",
                                _sqlConnection);
                        _sqlReader = _sqlCommand.ExecuteReader();
                        while (_sqlReader.Read())
                        {
                            //подходит ли звание должности под выбранные звания
                            var addThis = false;
                            var i = 0;
                            while (!addThis && i < lbChooseExcelPrimaryId.Items.Count)
                            {
                                if (_sqlReader["primaryId"].ToString() == lbChooseExcelPrimaryId.Items[i].ToString())
                                    addThis = true;
                                i++;
                            }

                            if (!addThis) continue;
                            //если подходит собираем все в куски
                            lbTemp.Items.Add(iTemp + "\t" + _sqlReader["parent1"] + "\t" + _sqlReader["parent2"] +
                                             "\t" + _sqlReader["parent3"] + "\t" + _sqlReader["parent4"] + "\t" +
                                             _sqlReader["name"] + "\t");
                            lbTemp1.Items.Add(_sqlReader["fullName"]);
                            iTemp++;
                            lbTemp2.Items.Add(_sqlReader["id"].ToString());
                        }

                        _sqlReader.Close();

                        iTemp--;
                        LoadDictionary();
                        //добавляем сведения о человеке к должности
                        while (iTemp > 0)
                        {
                            iTemp--;
                            _sqlCommand =
                                new SqlCommand(
                                    "SELECT * FROM [Peoples] WHERE [positionId]=@positionId",
                                    _sqlConnection);
                            _sqlCommand.Parameters.AddWithValue("positionId", Convert.ToInt32(lbTemp2.Items[iTemp]));
                            _sqlReader = _sqlCommand.ExecuteReader();
                            //если не вакант
                            if (_sqlReader.HasRows)
                            {
                                //ФИО и все что содержится в Peoples
                                _sqlReader.Read();
                                string[] fio = {_sqlReader["fio0"].ToString(),
                                    _sqlReader["fio1"].ToString(),
                                    _sqlReader["fio2"].ToString()};
                                lbTemp2.Items[iTemp] = fio[0] + " " +  fio[1] + " " + fio[2] + "\t\t" + 
                                                       " " + _sqlReader["lNumber"] + "\t" +
                                                       Convert.ToDateTime(_sqlReader["dateBirthday"])
                                                           .ToString("dd.MM.yyyy") + "\t";
                                var primaryId = Convert.ToInt32(_sqlReader["primaryId"]);
                                _sqlReader.Close();
                                //преобразуем звание
                                _sqlCommand =
                                    new SqlCommand("SELECT [name] FROM [Primary] WHERE [id]=@id",
                                        _sqlConnection);
                                _sqlCommand.Parameters.AddWithValue("id", primaryId);
                                _sqlReader = _sqlCommand.ExecuteReader();
                                _sqlReader.Read();
                                var primaryName = _sqlReader["name"].ToString();

                                //собираем все в кучу
                                lbTemp.Items[iTemp] +=
                                    primaryName + "\t" + lbTemp2.Items[iTemp] + lbTemp1.Items[iTemp] + "\t";
                                for (var decline = 2; decline < 4; decline++)
                                {
                                    lbTemp.Items[iTemp] += PrimaryDecline(primaryName, decline) + "\t";
                                    var fioNames = new Decliner().Decline(fio[0], fio[1], fio[2], decline);
                                    lbTemp.Items[iTemp] += fioNames[0] + " " + fioNames[1] + " " + fioNames[2] + "\t";
                                    lbTemp.Items[iTemp] += PositionDecline(lbTemp1.Items[iTemp].ToString(), decline);
                                    if (decline == 2)
                                        lbTemp.Items[iTemp] += "\t";
                                }
                            }
                            else
                            {
                                lbTemp.Items[iTemp] += "\tВАКАНТ";
                            }

                            lbTemp.Items[iTemp] += "\n";

                            _sqlReader.Close();
                        }

                        break;
                    }
                }

            //таблица "Данные" для штатки
            if (type == 1)
            {
                _sqlCommand =
                    new SqlCommand(
                        "SELECT [id], [primaryId] FROM [Positions] ORDER BY [position]",
                        _sqlConnection);
                _sqlReader = _sqlCommand.ExecuteReader();
                while (_sqlReader.Read())
                {
                    var addThis = false;
                    var i = 0;
                    while (!addThis && i < lbChooseExcelPrimaryId.Items.Count)
                    {
                        if (_sqlReader["primaryId"].ToString() == lbChooseExcelPrimaryId.Items[i].ToString())
                            addThis = true;
                        i++;
                    }

                    if (!addThis) continue;
                    iTemp++;
                    lbTemp.Items.Add(_sqlReader["id"].ToString());
                }

                _sqlReader.Close();
                iTemp--;

                while (iTemp > 0)
                {
                    iTemp--;
                    _sqlCommand =
                        new SqlCommand(
                            "SELECT * FROM [Peoples] WHERE [positionId]=@positionId",
                            _sqlConnection);
                    _sqlCommand.Parameters.AddWithValue("positionId", Convert.ToInt32(lbTemp.Items[iTemp]));
                    _sqlReader = _sqlCommand.ExecuteReader();
                    if (_sqlReader.HasRows)
                    {
                        _sqlReader.Read();
                        lbTemp.Items[iTemp] = _sqlReader["fio0"] + " " + _sqlReader["fio1"] + " " +
                                              _sqlReader["fio2"] + "\t\t" + _sqlReader["phoneNumber"] + "\t" +
                                              _sqlReader["lNumber"] + "\t" +
                                              Convert.ToDateTime(_sqlReader["dateBirthday"])
                                                  .ToString("dd.MM.yyyy") + "\t";
                        var peopleId = Convert.ToInt32(_sqlReader["id"]);
                        var primaryId = Convert.ToInt32(_sqlReader["primaryId"]);
                        var primaryOrderId = Convert.ToInt32(_sqlReader["primaryOrderId"]);
                        var genderBool = Convert.ToBoolean(_sqlReader["gender"]);
                        var positionOrderId = Convert.ToInt32(_sqlReader["positionOrderId"]);
                        var placeBirthday = _sqlReader["placeBirthday"] + "\t";
                        var start = _sqlReader["start"] + "\t";

                        _sqlReader.Close();
                        _sqlCommand =
                            new SqlCommand("SELECT [name] FROM [Primary] WHERE [id]=@id",
                                _sqlConnection);
                        _sqlCommand.Parameters.AddWithValue("id", primaryId);
                        _sqlReader = _sqlCommand.ExecuteReader();
                        _sqlReader.Read();
                        var primaryName = _sqlReader["name"].ToString().Replace("старший ", "ст.");
                        primaryName = primaryName.Replace("младший ", "мл.") + "\t";

                        _sqlReader.Close();
                        _sqlCommand =
                            new SqlCommand("SELECT [name], [number], [date] FROM [Orders] WHERE [id]=@id",
                                _sqlConnection);
                        _sqlCommand.Parameters.AddWithValue("id", positionOrderId);
                        _sqlReader = _sqlCommand.ExecuteReader();
                        _sqlReader.Read();
                        var positionOrder = _sqlReader.HasRows
                            ? "Приказ " + _sqlReader["name"] + " от " +
                              Convert.ToDateTime(_sqlReader["date"]).ToString("dd.MM.yyyy") +
                              " №" + _sqlReader["number"] + "\t"
                            : "\t";

                        _sqlReader.Close();
                        _sqlCommand =
                            new SqlCommand("SELECT [name], [number], [date] FROM [Orders] WHERE [id]=@id",
                                _sqlConnection);
                        _sqlCommand.Parameters.AddWithValue("id", primaryOrderId);
                        _sqlReader = _sqlCommand.ExecuteReader();
                        _sqlReader.Read();
                        var primaryOrder = _sqlReader.HasRows
                            ? "Приказ " + _sqlReader["name"] + " от " +
                              Convert.ToDateTime(_sqlReader["date"]).ToString("dd.MM.yyyy") +
                              " №" + _sqlReader["number"] + "\t"
                            : "\t";

                        lbTemp.Items[iTemp] = primaryName + lbTemp.Items[iTemp] + positionOrder + start;
                        lbTemp.Items[iTemp] += primaryOrder == ""
                            ? "\t"
                            : Convert.ToDateTime(_sqlReader["date"]).ToString("dd.MM.yyyy") + "\t";

                        _sqlReader.Close();
                        _sqlCommand =
                            new SqlCommand("SELECT [slaveStart], [slaveEnd], [orderId] FROM [Slaves] " +
                                           "WHERE [peopleId]=@peopleId", _sqlConnection);
                        _sqlCommand.Parameters.AddWithValue("peopleId", peopleId);
                        _sqlReader = _sqlCommand.ExecuteReader();
                        _sqlReader.Read();
                        var slaveStartEnd = _sqlReader.HasRows
                            ? Convert.ToDateTime(_sqlReader["slaveStart"]).ToString("dd.MM.yyyy") + "\t" +
                              Convert.ToDateTime(_sqlReader["slaveEnd"]).ToString("dd.MM.yyyy") + "\t"
                            : "\t\t";
                        var slaveOrderId = _sqlReader.HasRows
                            ? Convert.ToInt32(_sqlReader["orderId"])
                            : -1;

                        _sqlReader.Close();
                        _sqlCommand =
                            new SqlCommand("SELECT [name], [number], [date] FROM [Orders] WHERE [id]=@id",
                                _sqlConnection);
                        _sqlCommand.Parameters.AddWithValue("id", slaveOrderId);
                        _sqlReader = _sqlCommand.ExecuteReader();
                        _sqlReader.Read();
                        var slaveOrder = _sqlReader.HasRows
                            ? "Приказ " + _sqlReader["name"] + " от " +
                              Convert.ToDateTime(_sqlReader["date"]).ToString("dd.MM.yyyy") +
                              " №" + _sqlReader["number"] + "\t"
                            : "\t";

                        _sqlReader.Close();

                        _sqlCommand =
                            new SqlCommand(
                                "SELECT [name], [year], [special] FROM [Educations] WHERE [peopleId]=@peopleId ORDER BY [year] DESC",
                                _sqlConnection);
                        _sqlCommand.Parameters.AddWithValue("peopleId", peopleId);
                        _sqlReader = _sqlCommand.ExecuteReader();
                        _sqlReader.Read();
                        var educations = _sqlReader.HasRows
                            ? _sqlReader["name"] + "\t" + _sqlReader["year"] + "\t"
                            : "\t\t";
                        var special = _sqlReader.HasRows
                            ? _sqlReader["special"] + "\t"
                            : "\t";

                        _sqlReader.Close();

                        _sqlCommand =
                            new SqlCommand(
                                "SELECT [position], [name], [dateBirthday] FROM [Family] WHERE [peopleId]=@peopleId",
                                _sqlConnection);
                        _sqlCommand.Parameters.AddWithValue("peopleId", peopleId);
                        _sqlReader = _sqlCommand.ExecuteReader();
                        var family = "";
                        if (_sqlReader.HasRows)
                            while (_sqlReader.Read())
                            {
                                if (family.Length > 0)
                                    family += "; ";
                                family += _sqlReader["position"] + " – " +
                                          _sqlReader["name"] + ", " +
                                          Convert.ToDateTime(_sqlReader["dateBirthday"]).ToString("dd.MM.yyyy");
                            }

                        family += "\t";

                        lbTemp.Items[iTemp] += slaveStartEnd + educations;
                        lbTemp.Items[iTemp] += genderBool ? "Ж\t" : "М\t";
                        lbTemp.Items[iTemp] += primaryOrder + placeBirthday + special + slaveOrder + family;
                    }
                    else
                    {
                        lbTemp.Items[iTemp] = "\tВАКАНТ";
                    }

                    lbTemp.Items[iTemp] += "\n";

                    _sqlReader.Close();
                }
            }

            lbTemp.Items[lbTemp.Items.Count - 1] = lbTemp.Items[lbTemp.Items.Count - 1].ToString().Replace("\n", "");

            //загрузить все строки в буфер обмена
            foreach (var t in lbTemp.Items)
                clipString += t;
            Clipboard.SetText(clipString);

            //открыть файл или сделать другое завершающее действие
            if (type != 1)
                switch (lbChooseExcelType.SelectedIndex)
                {
                    case 0:
                    case 1:
                        new ExcelClassBig().CreatePackage(@"C:\temp\Выгрузка из базы.xlsx");
                        Process.Start(@"C:\temp\Выгрузка из базы.xlsx");
                        break;
                    case 2:
                        new GeneratedClassShdk().CreatePackage(@"C:\temp\Выгрузка из базы.xlsx");
                        Process.Start(@"C:\temp\Выгрузка из базы.xlsx");
                        break;
                    case 3:
                    case 4:
                        new ExcelClassShdkShort().CreatePackage(@"C:\temp\Выгрузка из базы.xlsx");
                        Process.Start(@"C:\temp\Выгрузка из базы.xlsx");
                        break;
                    case 6:
                        new ExcelClassDecline().CreatePackage(@"C:\temp\Выгрузка из базы.xlsx");
                        Process.Start(@"C:\temp\Выгрузка из базы.xlsx");
                        break;
                }
            else
            {
                var dialogForm = new DialogForm {bCancel = {Visible = false}, bOk = {Width = 200}};
                dialogForm.ShowDialog();
            }
               /* MessageBox.Show("Скопировано в буфер обмена", "Успешно",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);*/
        }

        private void LbChooseExcelType_SelectedIndexChanged(object sender, EventArgs e)
        {
            _canToExcelDo = lbChooseExcelType.SelectedIndex > -1;
            if (lbChooseExcelType.Text != "В штатку для листа Данные") return;
            _categoryPeoples[0] = true;
            tbChooseExcelPrimary0.ForeColor = _secondColor[_colorSchema];
            tbChooseExcelPrimary0.Image = Resources.check1;
            _categoryPeoples[1] = true;
            tbChooseExcelPrimary1.ForeColor = _secondColor[_colorSchema];
            tbChooseExcelPrimary1.Image = Resources.check1;
            _categoryPeoples[2] = false;
            tbChooseExcelPrimary2.ForeColor = _foreColor[_colorSchema];
            tbChooseExcelPrimary2.Image = Resources.check;
            _categoryPeoples[3] = false;
            tbChooseExcelPrimary3.ForeColor = _foreColor[_colorSchema];
            tbChooseExcelPrimary3.Image = Resources.check;
        }

        private void StartForm_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void lbChoosePeopleFind_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Enter
            if (e.KeyChar == '\r')
                ChangeCurrentPosition(true);
        }

        private void tbPeopleChoose_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Enter
            if (e.KeyChar == '\r')
                FindPeople();
        }

        private void CalcStats()
        {
            var numbers = new List<int>();
            var primaryTypeIdList = new List<int[]>();
            //категории военнослужащих
            bool[] categoryPeoples =
            {
                false, false, false, false
            };
            SetPositions();
            lbCalcResult.Items.Clear();
            switch (lbCalcCategory.SelectedIndex)
            {
                case 0:
                    categoryPeoples[0] = true;
                    categoryPeoples[1] = true;
                    categoryPeoples[2] = true;
                    categoryPeoples[3] = true;
                    lbCalcResult.Items.Add("Военнослужащих по штату – ");
                    lbCalcResult.Items.Add("Военнослужащих по списку – ");
                    lbCalcResult.Items.Add("Вакантных должностей – ");
                    break;
                case 1:
                    categoryPeoples[0] = true;
                    lbCalcResult.Items.Add("Всего офицеров по штату – ");
                    lbCalcResult.Items.Add("Всего офицеров по списку (на должностях офицеров) – ");
                    lbCalcResult.Items.Add("Всего офицеров не хватает – ");
                    break;
                case 2:
                    categoryPeoples[1] = true;
                    lbCalcResult.Items.Add("Всего прапорщиков по штату – ");
                    lbCalcResult.Items.Add("Всего прапорщиков по списку (на должностях прапорщиков) – ");
                    lbCalcResult.Items.Add("Всего прапорщиков не хватает – ");
                    break;
                case 3:
                    categoryPeoples[2] = true;
                    categoryPeoples[3] = true;
                    lbCalcResult.Items.Add("Всего должностей сержантов и солдат по штату – ");
                    lbCalcResult.Items.Add("Всего должностей сержантов и солдат по списку (на должностях с/с) – ");
                    lbCalcResult.Items.Add("Всего сержантов и солдат не хватает – ");
                    break;
                case 4:
                    categoryPeoples[2] = true;
                    lbCalcResult.Items.Add("Всего сержантских должностей по штату – ");
                    lbCalcResult.Items.Add("Всего сержантских должностей по списку (на должностях сержантов) – ");
                    lbCalcResult.Items.Add("Всего сержантов не хватает – ");
                    break;
                case 5:
                    categoryPeoples[3] = true;
                    lbCalcResult.Items.Add("Всего солдатских должностей по штату – ");
                    lbCalcResult.Items.Add("Всего солдатских должностей по списку (на должностях солдат) – ");
                    lbCalcResult.Items.Add("Всего солдат не хватает – ");
                    break;
            }

            //добавляем id званий
            _sqlCommand =
                new SqlCommand("SELECT [id], [type] FROM [Primary]",
                    _sqlConnection);
            _sqlReader = _sqlCommand.ExecuteReader();
            while (_sqlReader.Read())
                primaryTypeIdList.Add(new[]
                {
                    Convert.ToInt32(_sqlReader["id"]),
                    Convert.ToInt32(_sqlReader["type"])
                });

            _sqlReader.Close();
            foreach (var t in _positions)
            {
                var addThis = false;
                foreach (var t1 in primaryTypeIdList)
                {
                    if (addThis || Convert.ToInt32(t[8]) != t1[0]) continue;
                    addThis = true;
                    t[8] = t1[1].ToString();
                    if (t[8] == "3")
                        t[8] = "3";
                    //0 id, 1 parentMain, 2 parentOther, 3 name, 4 p1, 5 p2, 6 p3, 7 p4, 8 primId
                    //9 id, 10 fio, 11 lnumber, 12 gender, 13 primId
                }

                addThis = false;
                foreach (var t1 in primaryTypeIdList)
                {
                    if (addThis || t[9] == "-1" || Convert.ToInt32(t[13]) != t1[0]) continue;
                    addThis = true;
                    t[13] = t1[1].ToString();
                }
            }

            switch (lbCalcCategory.SelectedIndex)
            {
                //все
                case 0:
                    numbers.Add(0);
                    foreach (var t in _positions)
                    {
                        //0 id, 1 parentMain, 2 parentOther, 3 name, 4 p1, 5 p2, 6 p3, 7 p4, 8 primId
                        //9 id, 10 fio, 11 lnumber, 12 gender, 13 primId
                        if (t[0] == "2360" || t[3] == "В распоряжении") continue;
                        numbers[0]++;
                    }

                    lbCalcResult.Items[0] += numbers[0].ToString();
                    numbers.Add(0);
                    foreach (var t in _positions)
                    {
                        if (t[0] == "2360" || t[3] == "В распоряжении" || t[9] == "-1") continue;
                        numbers[1]++;
                    }

                    lbCalcResult.Items[1] += numbers[1].ToString();
                    numbers.Add(0);
                    numbers[2] = numbers[0] - numbers[1];
                    lbCalcResult.Items[2] += numbers[2].ToString();
                    numbers.Add(0);
                    foreach (var t in _positions)
                    {
                        if (t[3] != "В распоряжении" || t[9] == "-1") continue;
                        numbers[3]++;
                    }

                    lbCalcResult.Items.Add("Военнослужащих в распоряжении – " + numbers[3]);
                    numbers.Add(0);
                    foreach (var t in _positions)
                    {
                        if (t[0] == "2360" || t[3] == "В распоряжении" || t[9] == "-1" || t[12] != "1") continue;
                        numbers[4]++;
                    }

                    lbCalcResult.Items.Add("Военнослужащих женского пола – " + numbers[4]);
                    break;
                //офицеры
                case 1:
                    numbers.Add(0);
                    foreach (var t in _positions)
                    {
                        //0 id, 1 parentMain, 2 parentOther, 3 name, 4 p1, 5 p2, 6 p3, 7 p4, 8 primId
                        //9 id, 10 fio, 11 lnumber, 12 gender, 13 primId
                        if (t[0] == "2360" || t[3] == "В распоряжении" || t[8] != "0") continue;
                        numbers[0]++;
                    }

                    lbCalcResult.Items[0] += numbers[0].ToString();
                    numbers.Add(0);
                    foreach (var t in _positions)
                    {
                        if (t[0] == "2360" || t[3] == "В распоряжении" || t[8] != "0" ||
                            t[9] == "-1" || t[13] != "0") continue;
                        numbers[1]++;
                    }

                    lbCalcResult.Items[1] += numbers[1].ToString();
                    numbers.Add(0);
                    numbers[2] = numbers[0] - numbers[1];
                    lbCalcResult.Items[2] += numbers[2].ToString();
                    numbers.Add(0);
                    foreach (var t in _positions)
                    {
                        if (t[0] == "2360" || t[3] == "В распоряжении" || t[8] != "0" ||
                            t[9] == "-1" || t[13] != "1") continue;
                        numbers[3]++;
                    }

                    lbCalcResult.Items.Add("Прапорщиков на офицерских должностях – " + numbers[3]);
                    numbers.Add(0);
                    numbers[4] = numbers[0] - numbers[1] - numbers[3];
                    lbCalcResult.Items.Add("По штату – (По списку + Прапорщиков на офицерских) = " + numbers[4]);
                    numbers.Add(0);
                    foreach (var t in _positions)
                    {
                        //0 id, 1 parentMain, 2 parentOther, 3 name, 4 p1, 5 p2, 6 p3, 7 p4, 8 primId
                        //9 id, 10 fio, 11 lnumber, 12 gender, 13 primId
                        if (t[0] == "2360" || t[3] == "В распоряжении" || t[8] != "0" ||
                            t[9] == "-1" || t[13] != "2" && t[13] != "3") continue;
                        numbers[5]++;
                    }

                    lbCalcResult.Items.Add("Офицеров на должностях сержантов, солдат – " + numbers[5]);
                    numbers.Add(0);
                    foreach (var t in _positions)
                    {
                        if (t[0] == "2360" || t[3] != "В распоряжении" || t[9] == "-1" || t[13] != "0") continue;
                        numbers[6]++;
                    }

                    lbCalcResult.Items.Add("Офицеров в распоряжении – " + numbers[6]);
                    numbers.Add(0);
                    numbers[7] = numbers[6] + numbers[5] + numbers[1];
                    lbCalcResult.Items.Add("Всего офицеров в бригаде – " + numbers[7]);
                    numbers.Add(0);
                    foreach (var t in _positions)
                    {
                        if (t[0] == "2360" || t[3] == "В распоряжении" || t[9] == "-1" || t[12] != "1" ||
                            t[13] != "0") continue;
                        numbers[8]++;
                    }

                    lbCalcResult.Items.Add("Всего офицеров женского пола – " + numbers[8]);
                    break;
                //прапорщики
                case 2:
                    numbers.Add(0);
                    foreach (var t in _positions)
                    {
                        //0 id, 1 parentMain, 2 parentOther, 3 name, 4 p1, 5 p2, 6 p3, 7 p4, 8 primId
                        //9 id, 10 fio, 11 lnumber, 12 gender, 13 primId
                        if (t[0] == "2360" || t[3] == "В распоряжении" || t[8] != "1") continue;
                        numbers[0]++;
                    }

                    lbCalcResult.Items[0] += numbers[0].ToString();
                    numbers.Add(0);
                    foreach (var t in _positions)
                    {
                        if (t[0] == "2360" || t[3] == "В распоряжении" || t[8] != "1" ||
                            t[9] == "-1" || t[13] != "1") continue;
                        numbers[1]++;
                    }

                    lbCalcResult.Items[1] += numbers[1].ToString();
                    numbers.Add(0);
                    numbers[2] = numbers[0] - numbers[1];
                    lbCalcResult.Items[2] += numbers[2].ToString();
                    numbers.Add(0);
                    foreach (var t in _positions)
                    {
                        if (t[0] == "2360" || t[3] == "В распоряжении" || t[8] != "1" ||
                            t[9] == "-1" || t[13] != "2" && t[13] != "3") continue;
                        numbers[3]++;
                    }

                    lbCalcResult.Items.Add("Сержантов и солдат на должностях прапорщиков – " + numbers[3]);
                    numbers.Add(0);
                    numbers[4] = numbers[0] - numbers[1] - numbers[3];
                    lbCalcResult.Items.Add("По штату – (По списку + Сержанты и солдаты) = " + numbers[4]);
                    numbers.Add(0);
                    foreach (var t in _positions)
                    {
                        //0 id, 1 parentMain, 2 parentOther, 3 name, 4 p1, 5 p2, 6 p3, 7 p4, 8 primId
                        //9 id, 10 fio, 11 lnumber, 12 gender, 13 primId
                        if (t[0] == "2360" || t[3] == "В распоряжении" || t[8] != "2" && t[8] != "3" ||
                            t[9] == "-1" || t[13] != "1") continue;
                        numbers[5]++;
                    }

                    lbCalcResult.Items.Add("Прапорщиков на должностях сержантов, солдат – " + numbers[5]);
                    numbers.Add(0);
                    foreach (var t in _positions)
                    {
                        if (t[3] != "В распоряжении" || t[9] == "-1" || t[13] != "1") continue;
                        numbers[6]++;
                    }

                    lbCalcResult.Items.Add("Прапорщиков в распоряжении – " + numbers[6]);
                    numbers.Add(0);
                    numbers[7] = numbers[6] + numbers[5] + numbers[1];
                    lbCalcResult.Items.Add("Всего прапорщиков в бригаде – " + numbers[7]);
                    numbers.Add(0);
                    foreach (var t in _positions)
                    {
                        if (t[0] == "2360" || t[3] == "В распоряжении" || t[9] == "-1" || t[12] != "1" ||
                            t[13] != "1") continue;
                        numbers[8]++;
                    }

                    lbCalcResult.Items.Add("Всего прапорщиков женского пола – " + numbers[8]);
                    break;
                //с.с
                case 3:
                    numbers.Add(0);
                    foreach (var t in _positions)
                    {
                        //0 id, 1 parentMain, 2 parentOther, 3 name, 4 p1, 5 p2, 6 p3, 7 p4, 8 primId
                        //9 id, 10 fio, 11 lnumber, 12 gender, 13 primId
                        if (t[0] == "2360" || t[3] == "В распоряжении" || t[8] != "2" && t[8] != "3") continue;
                        numbers[0]++;
                    }

                    lbCalcResult.Items[0] += numbers[0].ToString();
                    numbers.Add(0);
                    foreach (var t in _positions)
                    {
                        if (t[0] == "2360" || t[3] == "В распоряжении" || t[8] != "2" && t[8] != "3" ||
                            t[9] == "-1" || t[13] != "2" && t[13] != "3") continue;
                        numbers[1]++;
                    }

                    lbCalcResult.Items[1] += numbers[1].ToString();
                    numbers.Add(0);
                    numbers[2] = numbers[0] - numbers[1];
                    lbCalcResult.Items[2] += numbers[2].ToString();
                    numbers.Add(0);
                    foreach (var t in _positions)
                    {
                        if (t[3] != "В распоряжении" || t[9] == "-1" || t[13] != "2" && t[13] != "3") continue;
                        numbers[3]++;
                    }

                    lbCalcResult.Items.Add("Сержантов и солдат в распоряжении – " + numbers[3]);
                    numbers.Add(0);
                    numbers[4] = numbers[1] + numbers[3];
                    lbCalcResult.Items.Add("Всего сержантов и солдат в бригаде – " + numbers[4]);
                    numbers.Add(0);
                    foreach (var t in _positions)
                    {
                        if (t[0] == "2360" || t[3] == "В распоряжении" || t[9] == "-1" ||
                            t[12] != "1" || t[13] != "2" && t[13] != "3") continue;
                        numbers[5]++;
                    }

                    lbCalcResult.Items.Add("Всего сержантов и солдат женского пола – " + numbers[5]);
                    break;
                //сержанты
                case 4:
                    numbers.Add(0);
                    foreach (var t in _positions)
                    {
                        //0 id, 1 parentMain, 2 parentOther, 3 name, 4 p1, 5 p2, 6 p3, 7 p4, 8 primId
                        //9 id, 10 fio, 11 lnumber, 12 gender, 13 primId
                        if (t[0] == "2360" || t[3] == "В распоряжении" || t[8] != "2") continue;
                        numbers[0]++;
                    }

                    lbCalcResult.Items[0] += numbers[0].ToString();
                    numbers.Add(0);
                    foreach (var t in _positions)
                    {
                        if (t[0] == "2360" || t[3] == "В распоряжении" || t[8] != "2" ||
                            t[9] == "-1" || t[13] != "2") continue;
                        numbers[1]++;
                    }

                    lbCalcResult.Items[1] += numbers[1].ToString();
                    numbers.Add(0);
                    numbers[2] = numbers[0] - numbers[1];
                    lbCalcResult.Items[2] += numbers[2].ToString();
                    numbers.Add(0);
                    foreach (var t in _positions)
                    {
                        if (t[0] == "2360" || t[3] == "В распоряжении" || t[8] != "2" ||
                            t[9] == "-1" || t[13] != "0" && t[13] != "1") continue;
                        numbers[3]++;
                    }

                    lbCalcResult.Items.Add("Офицеров и прапорщиков на должностях сержантов – " + numbers[3]);
                    numbers.Add(0);
                    foreach (var t in _positions)
                    {
                        if (t[0] == "2360" || t[3] == "В распоряжении" || t[8] != "2" ||
                            t[9] == "-1" || t[13] != "3") continue;
                        numbers[4]++;
                    }

                    lbCalcResult.Items.Add("Рядовых на должностях сержантов – " + numbers[4]);
                    numbers.Add(0);
                    numbers[5] = numbers[0] - numbers[1] - numbers[3] - numbers[4];
                    lbCalcResult.Items.Add("По штату – (По списку + Прапорщики + Офицеры + Рядовые) = " + numbers[5]);
                    numbers.Add(0);
                    foreach (var t in _positions)
                    {
                        //0 id, 1 parentMain, 2 parentOther, 3 name, 4 p1, 5 p2, 6 p3, 7 p4, 8 primId
                        //9 id, 10 fio, 11 lnumber, 12 gender, 13 primId
                        if (t[0] == "2360" || t[3] == "В распоряжении" || t[8] != "0" && t[8] != "1" ||
                            t[9] == "-1" || t[13] != "2") continue;
                        numbers[6]++;
                    }

                    lbCalcResult.Items.Add("Сержантов на должностях прапорщиков и офицеров – " + numbers[6]);
                    numbers.Add(0);
                    foreach (var t in _positions)
                    {
                        //0 id, 1 parentMain, 2 parentOther, 3 name, 4 p1, 5 p2, 6 p3, 7 p4, 8 primId
                        //9 id, 10 fio, 11 lnumber, 12 gender, 13 primId
                        if (t[0] == "2360" || t[3] == "В распоряжении" || t[8] != "3" ||
                            t[9] == "-1" || t[13] != "2") continue;
                        numbers[7]++;
                    }

                    lbCalcResult.Items.Add("Сержантов на должностях рядовых – " + numbers[7]);
                    numbers.Add(0);
                    foreach (var t in _positions)
                    {
                        if (t[3] != "В распоряжении" || t[9] == "-1" || t[13] != "2") continue;
                        numbers[8]++;
                    }

                    lbCalcResult.Items.Add("Сержантов в распоряжении – " + numbers[8]);
                    numbers.Add(0);
                    numbers[9] = numbers[6] + numbers[7] + numbers[8];
                    lbCalcResult.Items.Add("Сержантов не на должностях сержантов – " + numbers[9]);
                    numbers.Add(0);
                    numbers[10] = numbers[9] + numbers[1];
                    lbCalcResult.Items.Add("Всего сержантов в бригаде – " + numbers[10]);
                    numbers.Add(0);
                    foreach (var t in _positions)
                    {
                        if (t[0] == "2360" || t[3] == "В распоряжении" || t[9] == "-1" || t[12] != "1" ||
                            t[13] != "2") continue;
                        numbers[11]++;
                    }

                    lbCalcResult.Items.Add("Всего сержантов женского пола – " + numbers[11]);
                    break;
                //солдаты
                case 5:
                    numbers.Add(0);
                    foreach (var t in _positions)
                    {
                        //0 id, 1 parentMain, 2 parentOther, 3 name, 4 p1, 5 p2, 6 p3, 7 p4, 8 primId
                        //9 id, 10 fio, 11 lnumber, 12 gender, 13 primId
                        if (t[0] == "2360" || t[3] == "В распоряжении" || t[8] != "3") continue;
                        numbers[0]++;
                    }

                    lbCalcResult.Items[0] += numbers[0].ToString();
                    numbers.Add(0);
                    foreach (var t in _positions)
                    {
                        if (t[0] == "2360" || t[3] == "В распоряжении" || t[8] != "3" ||
                            t[9] == "-1" || t[13] != "3") continue;
                        numbers[1]++;
                    }

                    lbCalcResult.Items[1] += numbers[1].ToString();
                    numbers.Add(0);
                    numbers[2] = numbers[0] - numbers[1];
                    lbCalcResult.Items[2] += numbers[2].ToString();
                    numbers.Add(0);
                    foreach (var t in _positions)
                    {
                        if (t[0] == "2360" || t[3] == "В распоряжении" || t[8] != "3" ||
                            t[9] == "-1" || t[13] != "1") continue;
                        numbers[3]++;
                    }

                    lbCalcResult.Items.Add("Прапорщиков на должностях рядовых – " + numbers[3]);
                    numbers.Add(0);
                    foreach (var t in _positions)
                    {
                        if (t[0] == "2360" || t[3] == "В распоряжении" || t[8] != "3" ||
                            t[9] == "-1" || t[13] != "2") continue;
                        numbers[4]++;
                    }

                    lbCalcResult.Items.Add("Сержантов на должностях рядовых – " + numbers[4]);
                    numbers.Add(0);
                    numbers[5] = numbers[0] - numbers[1] - numbers[3] - numbers[4];
                    lbCalcResult.Items.Add("По штату – (По списку + Прапорщики + Сержанты) = " + numbers[5]);
                    numbers.Add(0);
                    foreach (var t in _positions)
                    {
                        //0 id, 1 parentMain, 2 parentOther, 3 name, 4 p1, 5 p2, 6 p3, 7 p4, 8 primId
                        //9 id, 10 fio, 11 lnumber, 12 gender, 13 primId
                        if (t[0] == "2360" || t[3] == "В распоряжении" || t[8] != "2" ||
                            t[9] == "-1" || t[13] != "3") continue;
                        numbers[6]++;
                    }

                    lbCalcResult.Items.Add("Рядовых на должностях прапорщиков – " + numbers[6]);
                    numbers.Add(0);
                    foreach (var t in _positions)
                    {
                        //0 id, 1 parentMain, 2 parentOther, 3 name, 4 p1, 5 p2, 6 p3, 7 p4, 8 primId
                        //9 id, 10 fio, 11 lnumber, 12 gender, 13 primId
                        if (t[0] == "2360" || t[3] == "В распоряжении" || t[8] != "2" ||
                            t[9] == "-1" || t[13] != "3") continue;
                        numbers[7]++;
                    }

                    lbCalcResult.Items.Add("Рядовых на должностях сержантов – " + numbers[7]);
                    numbers.Add(0);
                    foreach (var t in _positions)
                    {
                        if (t[3] != "В распоряжении" || t[9] == "-1" || t[13] != "3") continue;
                        numbers[8]++;
                    }

                    lbCalcResult.Items.Add("Рядовых в распоряжении – " + numbers[8]);
                    numbers.Add(0);
                    numbers[9] = numbers[6] + numbers[7] + numbers[8];
                    lbCalcResult.Items.Add("Рядовых не на рядовых должностях – " + numbers[9]);
                    numbers.Add(0);
                    numbers[10] = numbers[9] + numbers[1];
                    lbCalcResult.Items.Add("Всего рядовых в бригаде – " + numbers[10]);
                    numbers.Add(0);
                    foreach (var t in _positions)
                    {
                        if (t[0] == "2360" || t[3] == "В распоряжении" || t[9] == "-1" || t[12] != "1" ||
                            t[13] != "3") continue;
                        numbers[11]++;
                    }

                    lbCalcResult.Items.Add("Всего рядовых женского пола – " + numbers[11]);
                    break;
            }
        }

        private void bCalcStats_Click(object sender, EventArgs e)
        {
            CalcStats();
        }

        private void CalcCopy()
        {
            //загрузить все строки в буфер обмена
            var clipString = "";
            foreach (var t in lbCalcResult.Items)
                clipString += t + "\n";
            Clipboard.SetText(clipString);
        }

        private void bCalcCopy_Click(object sender, EventArgs e)
        {
            CalcCopy();
        }

        private void lbChooseParent_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbChooseParent.SelectedIndex != -1)
                ChooseParent(false, false);
        }

        private void bUpParent1_Click(object sender, EventArgs e)
        {
            ChooseParentUp();
        }

        private void NavigationFocus(int idItem, bool setFocus)
        {
            switch (idItem)
            {
                case 0:
                    bpFindLNumber.BackColor = setFocus
                        ? bFindLNumber.BackColor != _secondColor[_colorSchema]
                            ? _secondColor[_colorSchema]
                            : _secondHoverColor[_colorSchema]
                        : bFindLNumber.BackColor != _secondColor[_colorSchema]
                            ? _mainColor[_colorSchema]
                            : _secondColor[_colorSchema];
                    break;
                case 1:
                    bpFindSchema.BackColor = setFocus
                        ? bFindSchema.BackColor != _secondColor[_colorSchema]
                            ? _secondColor[_colorSchema]
                            : _secondHoverColor[_colorSchema]
                        : bFindSchema.BackColor != _secondColor[_colorSchema]
                            ? _mainColor[_colorSchema]
                            : _secondColor[_colorSchema];
                    break;
                case 2:
                    bpStatistic.BackColor = setFocus
                        ? bStatistic.BackColor != _secondColor[_colorSchema]
                            ? _secondColor[_colorSchema]
                            : _secondHoverColor[_colorSchema]
                        : bStatistic.BackColor != _secondColor[_colorSchema]
                            ? _mainColor[_colorSchema]
                            : _secondColor[_colorSchema];
                    break;
                case 3:
                    bpOrderToData.BackColor = setFocus
                        ? bOrderToData.BackColor != _secondColor[_colorSchema]
                            ? _secondColor[_colorSchema]
                            : _secondHoverColor[_colorSchema]
                        : bOrderToData.BackColor != _secondColor[_colorSchema]
                            ? _mainColor[_colorSchema]
                            : _secondColor[_colorSchema];
                    break;
                case 4:
                    bpToExcel.BackColor = setFocus
                        ? bToExcel.BackColor != _secondColor[_colorSchema]
                            ? _secondColor[_colorSchema]
                            : _secondHoverColor[_colorSchema]
                        : bToExcel.BackColor != _secondColor[_colorSchema]
                            ? _mainColor[_colorSchema]
                            : _secondColor[_colorSchema];
                    break;
            }
        }

        private void GoPanel(int typePanel, bool direction)
        {
            for (var i = 0; i < 61; i++)
            {
                if (typePanel == 0)
                    bpFindLNumber.Top += direction ? 1 : -1;
                else if (typePanel == 1)
                    bpFindSchema.Top += direction ? 1 : -1;
                else if (typePanel == 2)
                    bpStatistic.Top += direction ? 1 : -1;
                else if (typePanel == 3)
                    bpOrderToData.Top += direction ? 1 : -1;
                else if (typePanel == 4)
                    bpToExcel.Top += direction ? 1 : -1;
                Application.DoEvents();
            }
        }

        private void SelectPanel(int idPanel)
        {
            if (idPanel == 0 && bpFindLNumber.Top == 0 ||
                idPanel == 1 && bpFindSchema.Top == 0 ||
                idPanel == 2 && bpStatistic.Top == 0 ||
                idPanel == 3 && bpOrderToData.Top == 0 ||
                idPanel == 4 && bpToExcel.Top == 0) return;
            var numbersGo = 0;
            if (idPanel == 0 && bpFindLNumber.Top == 61 ||
                idPanel == 1 && bpFindSchema.Top == 61 ||
                idPanel == 2 && bpStatistic.Top == 61 ||
                idPanel == 3 && bpOrderToData.Top == 61 ||
                idPanel == 4 && bpToExcel.Top == 61)
                numbersGo = 1;
            else if (idPanel == 0 && bpFindLNumber.Top == 122 ||
                     idPanel == 1 && bpFindSchema.Top == 122 ||
                     idPanel == 2 && bpStatistic.Top == 122 ||
                     idPanel == 3 && bpOrderToData.Top == 122 ||
                     idPanel == 4 && bpToExcel.Top == 122)
                numbersGo = 2;
            else if (idPanel == 0 && bpFindLNumber.Top == 183 ||
                     idPanel == 1 && bpFindSchema.Top == 183 ||
                     idPanel == 2 && bpStatistic.Top == 183 ||
                     idPanel == 3 && bpOrderToData.Top == 183 ||
                     idPanel == 4 && bpToExcel.Top == 183)
                numbersGo = 3;
            else if (idPanel == 0 && bpFindLNumber.Top == 244 ||
                     idPanel == 1 && bpFindSchema.Top == 244 ||
                     idPanel == 2 && bpStatistic.Top == 244 ||
                     idPanel == 3 && bpOrderToData.Top == 244 ||
                     idPanel == 4 && bpToExcel.Top == 244)
                numbersGo = 4;
            for (var i = 0; i < numbersGo; i++)
                GoPanel(idPanel, false);
            if (numbersGo > 3)
            {
                if (bpFindLNumber.Top == 183)
                    GoPanel(0, true);
                else if (bpFindSchema.Top == 183)
                    GoPanel(1, true);
                else if (bpStatistic.Top == 183)
                    GoPanel(2, true);
                else if (bpOrderToData.Top == 183)
                    GoPanel(3, true);
                else if (bpToExcel.Top == 183)
                    GoPanel(4, true);
            }
            if (numbersGo > 2)
            {
                if (bpFindLNumber.Top == 122)
                    GoPanel(0, true);
                else if (bpFindSchema.Top == 122)
                    GoPanel(1, true);
                else if (bpStatistic.Top == 122)
                    GoPanel(2, true);
                else if (bpOrderToData.Top == 122)
                    GoPanel(3, true);
                else if (bpToExcel.Top == 122)
                    GoPanel(4, true);
            }

            if (numbersGo > 1)
            {
                if (bpFindLNumber.Top == 61)
                    GoPanel(0, true);
                else if (bpFindSchema.Top == 61)
                    GoPanel(1, true);
                else if (bpStatistic.Top == 61)
                    GoPanel(2, true);
                else if (bpOrderToData.Top == 61)
                    GoPanel(3, true);
                else if (bpToExcel.Top == 61)
                    GoPanel(4, true);
            }

            if (idPanel != 0 && bpFindLNumber.Top == 0)
                GoPanel(0, true);
            else if (idPanel != 1 && bpFindSchema.Top == 0)
                GoPanel(1, true);
            else if (idPanel != 2 && bpStatistic.Top == 0)
                GoPanel(2, true);
            else if (idPanel != 3 && bpOrderToData.Top == 0)
                GoPanel(3, true);
            else if (idPanel != 4 && bpToExcel.Top == 0)
                GoPanel(4, true);

            bool[] tabstops = { false, false, false, false, false };
            
            bFindLNumber.BackColor = _backColor[_colorSchema];
            bFindSchema.BackColor = _backColor[_colorSchema];
            bStatistic.BackColor = _backColor[_colorSchema];
            bOrderToData.BackColor = _backColor[_colorSchema];
            bToExcel.BackColor = _backColor[_colorSchema];
            switch (idPanel)
            {
                case 0:
                    bFindLNumber.BackColor = _secondColor[_colorSchema];
                    bpFindSchema.BackColor = _mainColor[_colorSchema];
                    bpStatistic.BackColor = _mainColor[_colorSchema];
                    bpOrderToData.BackColor = _mainColor[_colorSchema];
                    bpToExcel.BackColor = _mainColor[_colorSchema];
                    tabstops[0] = true;
                    pFindNumber.BringToFront();
                    tbPeopleChoose.Select();
                    break;
                case 1:
                    bFindSchema.BackColor = _secondColor[_colorSchema];
                    bpFindLNumber.BackColor = _mainColor[_colorSchema];
                    bpStatistic.BackColor = _mainColor[_colorSchema];
                    bpOrderToData.BackColor = _mainColor[_colorSchema];
                    bpToExcel.BackColor = _mainColor[_colorSchema];
                    tabstops[1] = true;
                    pFindSchema.BringToFront();
                    lbChooseParent.Select();
                    break;
                case 2:
                    bStatistic.BackColor = _secondColor[_colorSchema];
                    bpFindLNumber.BackColor = _mainColor[_colorSchema];
                    bpFindSchema.BackColor = _mainColor[_colorSchema];
                    bpOrderToData.BackColor = _mainColor[_colorSchema];
                    bpToExcel.BackColor = _mainColor[_colorSchema];
                    tabstops[2] = true;
                    pStatistic.BringToFront();
                    lbCalcCategory.Select();
                    break;
                case 3:
                    bOrderToData.BackColor = _secondColor[_colorSchema];
                    bpFindLNumber.BackColor = _mainColor[_colorSchema];
                    bpFindSchema.BackColor = _mainColor[_colorSchema];
                    bpStatistic.BackColor = _mainColor[_colorSchema];
                    bpToExcel.BackColor = _mainColor[_colorSchema];
                    tabstops[3] = true;
                    pOrderToData.BringToFront();
                    tbPrimaryOrderName.Select();
                    break;
                case 4:
                    bToExcel.BackColor = _secondColor[_colorSchema];
                    bpFindLNumber.BackColor = _mainColor[_colorSchema];
                    bpFindSchema.BackColor = _mainColor[_colorSchema];
                    bpStatistic.BackColor = _mainColor[_colorSchema];
                    bpOrderToData.BackColor = _mainColor[_colorSchema];
                    tabstops[4] = true;
                    pToExcel.BringToFront();
                    tbChooseExcelPrimary0.Select();
                    break;
            }

            foreach (Control c in pFindNumber.Controls)
                c.TabStop = tabstops[0];
            foreach (Control c in pFindSchema.Controls)
                c.TabStop = tabstops[1];
            foreach (Control c in pStatistic.Controls)
                c.TabStop = tabstops[2];
            foreach (Control c in pOrderToData.Controls)
                c.TabStop = tabstops[3];
            foreach (Control c in pToExcel.Controls)
                c.TabStop = tabstops[4];
        }

        private void bFindLNumber_MouseEnter(object sender, EventArgs e)
        {
            NavigationFocus(0, true);
        }

        private void bFindLNumber_MouseLeave(object sender, EventArgs e)
        {
            NavigationFocus(0, false);
        }

        private void bFindSchema_Enter(object sender, EventArgs e)
        {
            NavigationFocus(1, true);
        }

        private void bFindSchema_Leave(object sender, EventArgs e)
        {
            NavigationFocus(1, false);
        }

        private void bStatistic_Enter(object sender, EventArgs e)
        {
            NavigationFocus(2, true);
        }

        private void bStatistic_Leave(object sender, EventArgs e)
        {
            NavigationFocus(2, false);
        }

        private void bToExcel_Enter(object sender, EventArgs e)
        {
            NavigationFocus(4, true);
        }

        private void bToExcel_Leave(object sender, EventArgs e)
        {
            NavigationFocus(4, false);
        }

        private void bFindLNumber_Click(object sender, EventArgs e)
        {
            SelectPanel(0);
        }

        private void bFindSchema_Click(object sender, EventArgs e)
        {
            SelectPanel(1);
        }

        private void bStatistic_Click(object sender, EventArgs e)
        {
            SelectPanel(2);
        }

        private void bToExcel_Click(object sender, EventArgs e)
        {
            SelectPanel(4);
        }

        private void bExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
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

        private void pWindowState_MouseDown(object sender, MouseEventArgs e)
        {
            pWindowState.Capture = false;
            var m = Message.Create(Handle, 0xa1, new IntPtr(2), IntPtr.Zero);
            WndProc(ref m);
        }

        private void StartForm_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, ClientRectangle, _borderColor[_colorSchema], ButtonBorderStyle.Solid);
        }

        private void tbPeopleChoose_Enter(object sender, EventArgs e)
        {
            ElementFocus("tbPeopleChoose", true);
        }

        private void tbPeopleChoose_Leave(object sender, EventArgs e)
        {
            ElementFocus("tbPeopleChoose", false);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
        }

        private void lbChoosePeopleFind_Enter(object sender, EventArgs e)
        {
            ElementFocus("lbChoosePeopleFind", true);
        }

        private void lbChoosePeopleFind_Leave(object sender, EventArgs e)
        {
            ElementFocus("lbChoosePeopleFind", false);
        }

        private void bEditNextLNumber_BackColorChanged(object sender, EventArgs e)
        {
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
        }

        private void lbChooseParent_Enter(object sender, EventArgs e)
        {
            ElementFocus("lbChooseParent", true);
        }

        private void lbChooseName_Enter(object sender, EventArgs e)
        {
            ElementFocus("lbChooseName", true);
        }

        private void lbChooseName_Leave(object sender, EventArgs e)
        {
            ElementFocus("lbChooseName", false);
        }

        private void bUpParent_Enter(object sender, EventArgs e)
        {
            if (_idChoose[0] == -1 || _currentChoose == 0) return;
            bUpParent.BackColor = _secondColor[_colorSchema];
        }

        private void bUpParent_Leave(object sender, EventArgs e)
        {
            bUpParent.BackColor = _mainColor[_colorSchema];
        }

        private void lbCalcCategory_Enter(object sender, EventArgs e)
        {
            ElementFocus("lbCalcCategory", true);
        }

        private void lbCalcCategory_Leave(object sender, EventArgs e)
        {
            ElementFocus("lbCalcCategory", false);
        }

        private void lbCalcResult_Enter(object sender, EventArgs e)
        {
            ElementFocus("lbCalcResult", true);
        }

        private void lbCalcResult_Leave(object sender, EventArgs e)
        {
            ElementFocus("lbCalcResult", false);
        }

        private void lbCalcCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void bEditNextSchema_Click(object sender, EventArgs e)
        {
            ChangeCurrentPosition(false);
        }

        private void bEditNextLNumber_Click(object sender, EventArgs e)
        {
            ChangeCurrentPosition(true);
        }

        private void lbChooseName_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Enter
            if (e.KeyChar == '\r')
                ChangeCurrentPosition(false);
        }

        private void lbCalcCategory_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Enter
            if (e.KeyChar == '\r')
                CalcStats();
        }

        private void lbCalcResult_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void button22_Click(object sender, EventArgs e)
        {
            var bdEditForm = new BDEditForm {Left = Left, Top = Top, Height = Height, Width = Width};
            bdEditForm.LoadFromSQL(_sqlConnectionString, _userName);
            bdEditForm.Show(this);
        }

        private void button21_Click(object sender, EventArgs e)
        {
            if (_canToExcelDo)
                ExcelLoad();
        }

        private void lbChooseExcelType_Enter(object sender, EventArgs e)
        {
            ElementFocus("lbChooseExcelType", true);
        }

        private void lbChooseExcelType_Leave(object sender, EventArgs e)
        {
            ElementFocus("lbChooseExcelType", false);
        }

        private void tbChooseExcelPrimary0_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (int) Keys.Space && e.KeyChar != '\r') return;
            if (_categoryPeoples[0])
            {
                _categoryPeoples[0] = false;
                tbChooseExcelPrimary0.ForeColor = _foreColor[_colorSchema];
                tbChooseExcelPrimary0.Image = Resources.check;
            }
            else
            {
                _categoryPeoples[0] = true;
                tbChooseExcelPrimary0.ForeColor = _secondColor[_colorSchema];
                tbChooseExcelPrimary0.Image = Resources.check1;
            }
        }

        private void tbChooseExcelPrimary1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (int) Keys.Space && e.KeyChar != '\r') return;
            if (_categoryPeoples[1])
            {
                _categoryPeoples[1] = false;
                tbChooseExcelPrimary1.ForeColor = _foreColor[_colorSchema];
                tbChooseExcelPrimary1.Image = Resources.check;
            }
            else
            {
                _categoryPeoples[1] = true;
                tbChooseExcelPrimary1.ForeColor = _secondColor[_colorSchema];
                tbChooseExcelPrimary1.Image = Resources.check1;
            }
        }

        private void tbChooseExcelPrimary2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (int) Keys.Space && e.KeyChar != '\r') return;
            if (_categoryPeoples[2])
            {
                _categoryPeoples[2] = false;
                tbChooseExcelPrimary2.ForeColor = _foreColor[_colorSchema];
                tbChooseExcelPrimary2.Image = Resources.check;
            }
            else
            {
                _categoryPeoples[2] = true;
                tbChooseExcelPrimary2.ForeColor = _secondColor[_colorSchema];
                tbChooseExcelPrimary2.Image = Resources.check1;
            }
        }

        private void tbChooseExcelPrimary3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (int) Keys.Space && e.KeyChar != '\r') return;
            if (_categoryPeoples[3])
            {
                _categoryPeoples[3] = false;
                tbChooseExcelPrimary3.ForeColor = _foreColor[_colorSchema];
                tbChooseExcelPrimary3.Image = Resources.check;
            }
            else
            {
                _categoryPeoples[3] = true;
                tbChooseExcelPrimary3.ForeColor = _secondColor[_colorSchema];
                tbChooseExcelPrimary3.Image = Resources.check1;
            }
        }

        private void tbChooseExcelPrimary0_Enter(object sender, EventArgs e)
        {
            tbChooseExcelPrimary0.ForeColor = _changerColor[_colorSchema];
            tbChooseExcelPrimary0.Image = Resources.check2;
        }

        private void tbChooseExcelPrimary0_Leave(object sender, EventArgs e)
        {
            if (_categoryPeoples[0])
            {
                tbChooseExcelPrimary0.ForeColor = _secondColor[_colorSchema];
                tbChooseExcelPrimary0.Image = Resources.check1;
            }
            else
            {
                tbChooseExcelPrimary0.ForeColor = _foreColor[_colorSchema];
                tbChooseExcelPrimary0.Image = Resources.check;
            }
        }

        private void tbChooseExcelPrimary1_Enter(object sender, EventArgs e)
        {
            tbChooseExcelPrimary1.ForeColor = _changerColor[_colorSchema];
            tbChooseExcelPrimary1.Image = Resources.check2;
        }

        private void tbChooseExcelPrimary1_Leave(object sender, EventArgs e)
        {
            if (_categoryPeoples[1])
            {
                tbChooseExcelPrimary1.ForeColor = _secondColor[_colorSchema];
                tbChooseExcelPrimary1.Image = Resources.check1;
            }
            else
            {
                tbChooseExcelPrimary1.ForeColor = _foreColor[_colorSchema];
                tbChooseExcelPrimary1.Image = Resources.check;
            }
        }

        private void tbChooseExcelPrimary2_Enter(object sender, EventArgs e)
        {
            tbChooseExcelPrimary2.ForeColor = _changerColor[_colorSchema];
            tbChooseExcelPrimary2.Image = Resources.check2;
        }

        private void tbChooseExcelPrimary2_Leave(object sender, EventArgs e)
        {
            if (_categoryPeoples[2])
            {
                tbChooseExcelPrimary2.ForeColor = _secondColor[_colorSchema];
                tbChooseExcelPrimary2.Image = Resources.check1;
            }
            else
            {
                tbChooseExcelPrimary2.ForeColor = _foreColor[_colorSchema];
                tbChooseExcelPrimary2.Image = Resources.check;
            }
        }

        private void tbChooseExcelPrimary3_Enter(object sender, EventArgs e)
        {
            tbChooseExcelPrimary3.ForeColor = _changerColor[_colorSchema];
            tbChooseExcelPrimary3.Image = Resources.check2;
        }

        private void tbChooseExcelPrimary3_Leave(object sender, EventArgs e)
        {
            if (_categoryPeoples[3])
            {
                tbChooseExcelPrimary3.ForeColor = _secondColor[_colorSchema];
                tbChooseExcelPrimary3.Image = Resources.check1;
            }
            else
            {
                tbChooseExcelPrimary3.ForeColor = _foreColor[_colorSchema];
                tbChooseExcelPrimary3.Image = Resources.check;
            }
        }

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

        private void lbChooseParent_Resize(object sender, EventArgs e)
        {
        }

        private void tbChooseExcelPrimary0_MouseClick(object sender, EventArgs e)
        {
            _categoryPeoples[0] = !_categoryPeoples[0];
            if (!_categoryPeoples[0])
            {
                tbChooseExcelPrimary0.ForeColor = _foreColor[_colorSchema];
                tbChooseExcelPrimary0.Image = Resources.check;
            }
            else
            {
                tbChooseExcelPrimary0.ForeColor = _secondColor[_colorSchema];
                tbChooseExcelPrimary0.Image = Resources.check1;
            }
        }

        private void button21_Click_1(object sender, EventArgs e)
        {
            _categoryPeoples[1] = !_categoryPeoples[1];
            if (!_categoryPeoples[1])
            {
                tbChooseExcelPrimary1.ForeColor = _foreColor[_colorSchema];
                tbChooseExcelPrimary1.Image = Resources.check;
            }
            else
            {
                tbChooseExcelPrimary1.ForeColor = _secondColor[_colorSchema];
                tbChooseExcelPrimary1.Image = Resources.check1;
            }
        }

        private void button22_Click_1(object sender, EventArgs e)
        {
            _categoryPeoples[2] = !_categoryPeoples[2];
            if (!_categoryPeoples[2])
            {
                tbChooseExcelPrimary2.ForeColor = _foreColor[_colorSchema];
                tbChooseExcelPrimary2.Image = Resources.check;
            }
            else
            {
                tbChooseExcelPrimary2.ForeColor = _secondColor[_colorSchema];
                tbChooseExcelPrimary2.Image = Resources.check1;
            }
        }

        private void button24_Click(object sender, EventArgs e)
        {
            _categoryPeoples[3] = !_categoryPeoples[3];
            if (!_categoryPeoples[3])
            {
                tbChooseExcelPrimary3.ForeColor = _foreColor[_colorSchema];
                tbChooseExcelPrimary3.Image = Resources.check;
            }
            else
            {
                tbChooseExcelPrimary3.ForeColor = _secondColor[_colorSchema];
                tbChooseExcelPrimary3.Image = Resources.check1;
            }
        }

        private void lbChoosePeopleFindScroll_Load(object sender, EventArgs e)
        {
        }

        private void lbChoosePeopleFindScroll_Scroll(object sender, ScrollEventArgs e)
        {
            if (lbChoosePeopleFind.Items.Count > lbChoosePeopleFindScroll.Value)
                lbChoosePeopleFind.SelectedIndex = lbChoosePeopleFindScroll.Value;
        }

        private void lbChooseNameScroll_Scroll(object sender, ScrollEventArgs e)
        {
            if (lbChooseName.Items.Count > lbChooseNameScroll.Value)
                lbChooseName.SelectedIndex = lbChooseNameScroll.Value;
        }

        private void lbChooseName_LocationChanged(object sender, EventArgs e)
        {
        }

        private void bFlash_Click(object sender, EventArgs e)
        {
            ColorSchemaSet(this);
            bFlash.Image = _colorSchema == 0 ? Resources.unsun : Resources.sun;
            _colorSchema = _colorSchema == 0 ? 1 : 0;
        }

        private void bMax_MouseEnter(object sender, EventArgs e)
        {
            bMax.Image = WindowState == FormWindowState.Normal ? Resources.fullin30_1 : Resources.fulloff30_1;
        }

        private void bMax_MouseLeave(object sender, EventArgs e)
        {
            bMax.Image = WindowState == FormWindowState.Normal ? Resources.fullin30 : Resources.fulloff30;
        }

        private void bMin_MouseEnter(object sender, EventArgs e)
        {
            bMin.Image = Resources.minimum1;
        }

        private void bMin_MouseLeave(object sender, EventArgs e)
        {
            bMin.Image = Resources.minimum;
        }

        private void bFlash_Enter(object sender, EventArgs e)
        {
            bFlash.Image = _colorSchema == 0 ? Resources.sun1 : Resources.unsun1;
        }

        private void bFlash_MouseLeave(object sender, EventArgs e)
        {
            bFlash.Image = _colorSchema == 0 ? Resources.sun : Resources.unsun;
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //экран добавления человека
            var printForm = new PrintForm()
            {
                Left = 700,
                Top = 500
            };
            printForm.LoadFromSQL(_sqlConnectionString, _userName);
            Hide();
            printForm.Closed += (s, args) =>
            {
                //обновление должностей
                SetPositions();
                RefreshPositions();
                Show();
                Refresh();
            };
            printForm.Show(this);
        }

        private void bMenu_Click(object sender, EventArgs e)
        {
            ChangeMenuSchema();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            }

        private void panel2_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bOrderToData_Enter(object sender, EventArgs e)
        {
            NavigationFocus(3, true);
        }

        private void bOrderToData_Leave(object sender, EventArgs e)
        {
            NavigationFocus(3, false);
        }

        private void bOrderToData_Click(object sender, EventArgs e)
        {
            SelectPanel(3);
        }
    }
}