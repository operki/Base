using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WindowsFormsApp1.Properties;
//установка шрифта
using System.Drawing.Text;

namespace WindowsFormsApp1
{
    public partial class BDEditForm : Form
    {
        private string _userName;
        private SqlDataReader _sqlReader;
        private SqlCommand _sqlCommand;
        private SqlConnection _sqlConnection;

        //установка шрифта
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont,
            IntPtr pdv, [System.Runtime.InteropServices.In] ref uint pcFonts);
        private readonly PrivateFontCollection _fonts = new PrivateFontCollection();
        Font _roboto;

        public BDEditForm()
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
            _roboto = new Font(_fonts.Families[0], 9.0F);
            foreach (Control control in Controls)
                FontSet(control, _roboto);
        }

        private void FontSet(Control control, Font font)
        {
            foreach (Control c in control.Controls)
                FontSet(c, font);
            control.Font = font;
        }

        /// <summary>
        /// Подключение к MSSQL SERVER
        /// </summary>
        /// <param name="sqlConnectionString">Строка подключения</param>
        /// <param name="formUserName">Имя пользователя</param>
        public void LoadFromSQL(string sqlConnectionString, string formUserName)
        {
            _userName = formUserName;
            _sqlConnection = new SqlConnection(sqlConnectionString);
            _sqlConnection.Open();
            for (var i = 0; i < lbBDTableChoose.Items.Count; i++)
            {
                lbBDTableChoose.SelectedIndex = i;
                BDLoadTables();
            }
        }

        /// <summary>
        /// Загрузка выбранной таблицы
        /// </summary>
        private void BDLoadTables()
        {
            _sqlCommand = new SqlCommand("SELECT * FROM [" + lbBDTableChoose.Text + "]", _sqlConnection);
            switch (lbBDTableChoose.Text)
            {
                case "Battlefields":
                    dgvBDBattlefields.Rows.Clear();
                    break;
                case "Dictionary":
                    dgvBDDictionary.Rows.Clear();
                    break;
                case "Educations":
                    dgvBDEducations.Rows.Clear();
                    break;
                case "Family":
                    dgvBDFamily.Rows.Clear();
                    break;
                case "History":
                    dgvBDHistory.Rows.Clear();
                    break;
                case "Medals":
                    dgvBDMedals.Rows.Clear();
                    break;
                case "Memory":
                    dgvBDMemory.Rows.Clear();
                    break;
                case "Orders":
                    dgvBDOrders.Rows.Clear();
                    break;
                case "Peoples":
                    dgvBDPeoples.Rows.Clear();
                    break;
                case "Positions":
                    dgvBDPositions.Rows.Clear();
                    break;
                case "Primary":
                    dgvBDPrimary.Rows.Clear();
                    break;
                case "Settings":
                    dgvBDSettings.Rows.Clear();
                    break;
                case "Slaves":
                    dgvBDSlaves.Rows.Clear();
                    break;
                case "Tasks":
                    dgvBDTasks.Rows.Clear();
                    break;
                case "Users":
                    dgvBDUsers.Rows.Clear();
                    break;
            }

            _sqlReader = _sqlCommand.ExecuteReader();
            while (_sqlReader.Read())
            {
                switch (lbBDTableChoose.Text)
                {
                    case "Battlefields":
                        dgvBDBattlefields.Rows.Add(
                            _sqlReader["id"].ToString(), _sqlReader["peopleId"].ToString(),
                            _sqlReader["name"].ToString(), _sqlReader["dateText"].ToString(),
                            _sqlReader["action"].ToString(), _sqlReader["actionUser"].ToString());
                        break;
                    case "Dictionary":
                        dgvBDDictionary.Rows.Add(
                            _sqlReader["id"].ToString(), _sqlReader["name"].ToString(),
                            _sqlReader["decline1"].ToString(), _sqlReader["decline2"].ToString(),
                            _sqlReader["action"].ToString(), _sqlReader["actionUser"].ToString());
                        break;
                    case "Educations":
                        dgvBDEducations.Rows.Add(
                            _sqlReader["id"].ToString(), _sqlReader["peopleId"].ToString(),
                            _sqlReader["name"].ToString(), _sqlReader["year"].ToString(), _sqlReader["special"].ToString(),
                            _sqlReader["action"].ToString(), _sqlReader["actionUser"].ToString());
                        break;
                    case "Family":
                        dgvBDFamily.Rows.Add(
                            _sqlReader["id"].ToString(), _sqlReader["peopleId"].ToString(),
                            _sqlReader["position"].ToString(),
                            _sqlReader["name"].ToString(), _sqlReader["dateBirthday"].ToString(),
                            _sqlReader["action"].ToString(), _sqlReader["actionUser"].ToString());
                        break;
                    case "History":
                        dgvBDHistory.Rows.Add(
                            _sqlReader["id"].ToString(), _sqlReader["peopleId"].ToString(),
                            _sqlReader["name"].ToString(), _sqlReader["orderId"].ToString(),
                            _sqlReader["action"].ToString(), _sqlReader["actionUser"].ToString());
                        break;
                    case "Medals":
                        dgvBDMedals.Rows.Add(
                            _sqlReader["id"].ToString(), _sqlReader["peopleId"].ToString(),
                            _sqlReader["name"].ToString(),
                            _sqlReader["orderId"].ToString(),
                            _sqlReader["action"].ToString(), _sqlReader["actionUser"].ToString());
                        break;
                    case "Memory":
                        dgvBDMemory.Rows.Add(
                            _sqlReader["id"].ToString(), _sqlReader["peopleId"].ToString(),
                            _sqlReader["type"].ToString(), _sqlReader["dateStart"].ToString(),
                            _sqlReader["dateEnd"].ToString(), _sqlReader["isLast"].ToString(),
                            _sqlReader["variety"].ToString(), _sqlReader["text"].ToString(), 
                            _sqlReader["action"].ToString(), _sqlReader["actionUser"].ToString());
                        break;
                    case "Orders":
                        dgvBDOrders.Rows.Add(
                            _sqlReader["id"].ToString(), _sqlReader["name"].ToString(),
                            _sqlReader["number"].ToString(), _sqlReader["date"].ToString(),
                            _sqlReader["action"].ToString(), _sqlReader["actionUser"].ToString());
                        break;
                    case "Peoples":
                        dgvBDPeoples.Rows.Add(
                            _sqlReader["id"].ToString(), _sqlReader["fio0"].ToString(),
                            _sqlReader["fio1"].ToString(), _sqlReader["fio2"].ToString(),
                            _sqlReader["gender"].ToString(),
                            _sqlReader["phoneNumber"].ToString(),
                            _sqlReader["lNumber"].ToString(),
                            _sqlReader["tableNumber"].ToString(),
                            _sqlReader["dateBirthday"].ToString(),
                            _sqlReader["placeBirthday"].ToString(),
                            _sqlReader["primaryId"].ToString(),
                            _sqlReader["primaryDate"].ToString(),
                            _sqlReader["primaryOrderId"].ToString(),
                            _sqlReader["positionId"].ToString(),
                            _sqlReader["positionOrderId"].ToString(),
                            _sqlReader["damages"].ToString(),
                            _sqlReader["numberNIS"].ToString(),
                            _sqlReader["start"].ToString(),
                            _sqlReader["startThis"].ToString(),
                            _sqlReader["action"].ToString(), _sqlReader["actionUser"].ToString());
                        break;
                    case "Positions":
                        dgvBDPositions.Rows.Add(
                            _sqlReader["position"].ToString(),
                            _sqlReader["id"].ToString(), _sqlReader["parent1"].ToString(),
                            _sqlReader["parent2"].ToString(), _sqlReader["parent3"].ToString(),
                            _sqlReader["parent4"].ToString(), _sqlReader["name"].ToString(),
                            _sqlReader["fullName"].ToString(),
                            _sqlReader["vus"].ToString(), _sqlReader["primaryId"].ToString(),
                            _sqlReader["tarif"].ToString(),
                            _sqlReader["action"].ToString(), _sqlReader["actionUser"].ToString());
                        break;
                    case "Primary":
                        dgvBDPrimary.Rows.Add(
                            _sqlReader["id"].ToString(), _sqlReader["type"].ToString(), _sqlReader["name"].ToString(),
                            _sqlReader["action"].ToString(), _sqlReader["actionUser"].ToString());
                        break;
                    case "Settings":
                        dgvBDSettings.Rows.Add(
                            _sqlReader["id"].ToString(), _sqlReader["nshId"].ToString(),
                            _sqlReader["nokId"].ToString(), _sqlReader["action"].ToString(),
                            _sqlReader["actionUser"].ToString());
                        break;
                    case "Slaves":
                        dgvBDSlaves.Rows.Add(
                            _sqlReader["id"].ToString(), _sqlReader["peopleId"].ToString(),
                            _sqlReader["slaveStart"].ToString(), _sqlReader["slaveEnd"].ToString(),
                            _sqlReader["orderId"].ToString(), _sqlReader["action"].ToString(),
                            _sqlReader["actionUser"].ToString());
                        break;
                    case "Tasks":
                        dgvBDTasks.Rows.Add(
                            _sqlReader["id"].ToString(), _sqlReader["destination"].ToString(),
                            _sqlReader["name"].ToString(), _sqlReader["peopleId"].ToString(),
                            _sqlReader["isWork"].ToString(), _sqlReader["action"].ToString(),
                            _sqlReader["actionUser"].ToString(),
                            (_sqlReader["nameWork"] == null) ? "" : _sqlReader["nameWork"].ToString(),
                            (_sqlReader["dateWork"] == null) ? "" : _sqlReader["dateWork"].ToString());
                        break;
                    case "Users":
                        dgvBDUsers.Rows.Add(
                            _sqlReader["id"].ToString(), _sqlReader["name"].ToString(),
                            _sqlReader["password"].ToString(), _sqlReader["rights"].ToString(),
                            _sqlReader["action"].ToString(),
                            _sqlReader["actionUser"].ToString());
                        break;
                }
            }
            _sqlReader?.Close();
        }

        private void BDEditForm_Load(object sender, EventArgs e)
        {
            foreach (var dataGridView in panel1.Controls.OfType<DataGridView>())
                dataGridView.Dock = DockStyle.Fill;
            lbBDTableChoose.SelectedIndex = 8;
            dgvBDPeoples.Visible = true;
        }

        private void BBDLoad_Click(object sender, EventArgs e)
        {
            BDLoadTables();
        }

        /// <summary>
        /// Вставка в таблицу из буфера обмена
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BBDPaste_Click(object sender, EventArgs e)
        {
            cbBDSaveTable.Checked = true;
            char[] rowSplitter = { '\r', '\n' };
            char[] columnSplitter = { '\t' };

            //get the text from clipboard
            var dataInClipboard = Clipboard.GetDataObject();
            var stringInClipboard = (string)dataInClipboard.GetData(DataFormats.Text);

            //split it into lines
            var rowsInClipboard = stringInClipboard.Split(rowSplitter, StringSplitOptions.RemoveEmptyEntries);

            //get the row and column of selected cell in grid
            var r = 0;
            var c = 0;
            switch (lbBDTableChoose.Text)
            {
                case "Battlefields":
                    r = dgvBDBattlefields.SelectedCells[0].RowIndex;
                    c = dgvBDBattlefields.SelectedCells[0].ColumnIndex;
                    break;
                case "Dictionary":
                    r = dgvBDDictionary.SelectedCells[0].RowIndex;
                    c = dgvBDDictionary.SelectedCells[0].ColumnIndex;
                    break;
                case "Educations":
                    r = dgvBDEducations.SelectedCells[0].RowIndex;
                    c = dgvBDEducations.SelectedCells[0].ColumnIndex;
                    break;
                case "Family":
                    r = dgvBDFamily.SelectedCells[0].RowIndex;
                    c = dgvBDFamily.SelectedCells[0].ColumnIndex;
                    break;
                case "History":
                    r = dgvBDHistory.SelectedCells[0].RowIndex;
                    c = dgvBDHistory.SelectedCells[0].ColumnIndex;
                    break;
                case "Medals":
                    r = dgvBDMedals.SelectedCells[0].RowIndex;
                    c = dgvBDMedals.SelectedCells[0].ColumnIndex;
                    break;
                case "Memory":
                    r = dgvBDMemory.SelectedCells[0].RowIndex;
                    c = dgvBDMemory.SelectedCells[0].ColumnIndex;
                    break;
                case "Orders":
                    r = dgvBDOrders.SelectedCells[0].RowIndex;
                    c = dgvBDOrders.SelectedCells[0].ColumnIndex;
                    break;
                case "Peoples":
                    r = dgvBDPeoples.SelectedCells[0].RowIndex;
                    c = dgvBDPeoples.SelectedCells[0].ColumnIndex;
                    break;
                case "Positions":
                    r = dgvBDPositions.SelectedCells[0].RowIndex;
                    c = dgvBDPositions.SelectedCells[0].ColumnIndex;
                    break;
                case "Primary":
                    r = dgvBDPrimary.SelectedCells[0].RowIndex;
                    c = dgvBDPrimary.SelectedCells[0].ColumnIndex;
                    break;
                case "Settings":
                    r = dgvBDSettings.SelectedCells[0].RowIndex;
                    c = dgvBDSettings.SelectedCells[0].ColumnIndex;
                    break;
                case "Slaves":
                    r = dgvBDSlaves.SelectedCells[0].RowIndex;
                    c = dgvBDSlaves.SelectedCells[0].ColumnIndex;
                    break;
                case "Tasks":
                    r = dgvBDTasks.SelectedCells[0].RowIndex;
                    c = dgvBDTasks.SelectedCells[0].ColumnIndex;
                    break;
                case "Users":
                    r = dgvBDUsers.SelectedCells[0].RowIndex;
                    c = dgvBDUsers.SelectedCells[0].ColumnIndex;
                    break;
            }

            //add rows into grid to fit clipboard lines
            switch (lbBDTableChoose.Text)
            {
                case "Battlefields":
                    if (dgvBDBattlefields.Rows.Count < (r + rowsInClipboard.Length))
                        dgvBDBattlefields.Rows.Add(r + rowsInClipboard.Length - dgvBDBattlefields.Rows.Count);
                    break;
                case "Dictionary":
                    if (dgvBDDictionary.Rows.Count < (r + rowsInClipboard.Length))
                        dgvBDDictionary.Rows.Add(r + rowsInClipboard.Length - dgvBDDictionary.Rows.Count);
                    break;
                case "Educations":
                    if (dgvBDEducations.Rows.Count < (r + rowsInClipboard.Length))
                        dgvBDEducations.Rows.Add(r + rowsInClipboard.Length - dgvBDEducations.Rows.Count);
                    break;
                case "Family":
                    if (dgvBDFamily.Rows.Count < (r + rowsInClipboard.Length))
                        dgvBDFamily.Rows.Add(r + rowsInClipboard.Length - dgvBDFamily.Rows.Count);
                    break;
                case "History":
                    if (dgvBDHistory.Rows.Count < (r + rowsInClipboard.Length))
                        dgvBDHistory.Rows.Add(r + rowsInClipboard.Length - dgvBDHistory.Rows.Count);
                    break;
                case "Orders":
                    if (dgvBDOrders.Rows.Count < (r + rowsInClipboard.Length))
                        dgvBDOrders.Rows.Add(r + rowsInClipboard.Length - dgvBDOrders.Rows.Count);
                    break;
                case "Medals":
                    if (dgvBDMedals.Rows.Count < (r + rowsInClipboard.Length))
                        dgvBDMedals.Rows.Add(r + rowsInClipboard.Length - dgvBDMedals.Rows.Count);
                    break;
                case "Memory":
                    if (dgvBDMemory.Rows.Count < (r + rowsInClipboard.Length))
                        dgvBDMemory.Rows.Add(r + rowsInClipboard.Length - dgvBDMemory.Rows.Count);
                    break;
                case "Peoples":
                    if (dgvBDPeoples.Rows.Count < (r + rowsInClipboard.Length))
                        dgvBDPeoples.Rows.Add(r + rowsInClipboard.Length - dgvBDPeoples.Rows.Count);
                    break;
                case "Positions":
                    if (dgvBDPositions.Rows.Count < (r + rowsInClipboard.Length))
                        dgvBDPositions.Rows.Add(r + rowsInClipboard.Length - dgvBDPositions.Rows.Count);
                    break;
                case "Primary":
                    if (dgvBDPrimary.Rows.Count < (r + rowsInClipboard.Length))
                        dgvBDPrimary.Rows.Add(r + rowsInClipboard.Length - dgvBDPrimary.Rows.Count);
                    break;
                case "Settings":
                    if (dgvBDSettings.Rows.Count < (r + rowsInClipboard.Length))
                        dgvBDSettings.Rows.Add(r + rowsInClipboard.Length - dgvBDSettings.Rows.Count);
                    break;
                case "Slaves":
                    if (dgvBDSlaves.Rows.Count < (r + rowsInClipboard.Length))
                        dgvBDSlaves.Rows.Add(r + rowsInClipboard.Length - dgvBDSlaves.Rows.Count);
                    break;
                case "Tasks":
                    if (dgvBDTasks.Rows.Count < (r + rowsInClipboard.Length))
                        dgvBDTasks.Rows.Add(r + rowsInClipboard.Length - dgvBDTasks.Rows.Count);
                    break;
                case "Users":
                    if (dgvBDUsers.Rows.Count < (r + rowsInClipboard.Length))
                        dgvBDUsers.Rows.Add(r + rowsInClipboard.Length - dgvBDUsers.Rows.Count);
                    break;
            }

            // loop through the lines, split them into cells and place the values in the corresponding cell.
            pbPaste.Maximum = rowsInClipboard.Length;
            pbPaste.Value = 0;
            panel1.Visible = false;
            for (var iRow = 0; iRow < rowsInClipboard.Length; iRow++)
            {
                pbPaste.Value++;
                //split row into cell values
                var valuesInRow = rowsInClipboard[iRow].Split(columnSplitter);

                //cycle through cell values
                for (int iCol = 0; iCol < valuesInRow.Length; iCol++)
                {
                    //assign cell value, only if it within columns of the grid
                    switch (lbBDTableChoose.Text)
                    {
                        case "Battlefields":
                            if (dgvBDBattlefields.ColumnCount - 1 >= c + iCol)
                                dgvBDBattlefields.Rows[r + iRow].Cells[c + iCol].Value = valuesInRow[iCol];
                            break;
                        case "Dictionary":
                            if (dgvBDDictionary.ColumnCount - 1 >= c + iCol)
                                dgvBDDictionary.Rows[r + iRow].Cells[c + iCol].Value = valuesInRow[iCol];
                            break;
                        case "Educations":
                            if (dgvBDEducations.ColumnCount - 1 >= c + iCol)
                                dgvBDEducations.Rows[r + iRow].Cells[c + iCol].Value = valuesInRow[iCol];
                            break;
                        case "Orders":
                            if (dgvBDOrders.ColumnCount - 1 >= c + iCol)
                                dgvBDOrders.Rows[r + iRow].Cells[c + iCol].Value = valuesInRow[iCol];
                            break;
                        case "Family":
                            if (dgvBDFamily.ColumnCount - 1 >= c + iCol)
                                dgvBDFamily.Rows[r + iRow].Cells[c + iCol].Value = valuesInRow[iCol];
                            break;
                        case "History":
                            if (dgvBDHistory.ColumnCount - 1 >= c + iCol)
                                dgvBDHistory.Rows[r + iRow].Cells[c + iCol].Value = valuesInRow[iCol];
                            break;
                        case "Medals":
                            if (dgvBDMedals.ColumnCount - 1 >= c + iCol)
                                dgvBDMedals.Rows[r + iRow].Cells[c + iCol].Value = valuesInRow[iCol];
                            break;
                        case "Memory":
                            if (dgvBDMemory.ColumnCount - 1 >= c + iCol)
                                dgvBDMemory.Rows[r + iRow].Cells[c + iCol].Value = valuesInRow[iCol];
                            break;
                        case "Peoples":
                            if (dgvBDPeoples.ColumnCount - 1 >= c + iCol)
                                dgvBDPeoples.Rows[r + iRow].Cells[c + iCol].Value = valuesInRow[iCol];
                            break;
                        case "Positions":
                            if (dgvBDPositions.ColumnCount - 1 >= c + iCol)
                                dgvBDPositions.Rows[r + iRow].Cells[c + iCol].Value = valuesInRow[iCol];
                            break;
                        case "Primary":
                            if (dgvBDPrimary.ColumnCount - 1 >= c + iCol)
                                dgvBDPrimary.Rows[r + iRow].Cells[c + iCol].Value = valuesInRow[iCol];
                            break;
                        case "Settings":
                            if (dgvBDSettings.ColumnCount - 1 >= c + iCol)
                                dgvBDSettings.Rows[r + iRow].Cells[c + iCol].Value = valuesInRow[iCol];
                            break;
                        case "Slaves":
                            if (dgvBDSlaves.ColumnCount - 1 >= c + iCol)
                                dgvBDSlaves.Rows[r + iRow].Cells[c + iCol].Value = valuesInRow[iCol];
                            break;
                        case "Tasks":
                            if (dgvBDTasks.ColumnCount - 1 >= c + iCol)
                                dgvBDTasks.Rows[r + iRow].Cells[c + iCol].Value = valuesInRow[iCol];
                            break;
                        case "Users":
                            if (dgvBDUsers.ColumnCount - 1 >= c + iCol)
                                dgvBDUsers.Rows[r + iRow].Cells[c + iCol].Value = valuesInRow[iCol];
                            break;
                    }
                }
            }
            panel1.Visible = true;
        }

        /// <summary>
        /// Сохранение таблицы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BBDSave_Click(object sender, EventArgs e)
        {
            var rowCount = 0;
            switch (lbBDTableChoose.Text)
            {
                case "Battlefields":
                    rowCount = dgvBDBattlefields.RowCount;
                    break;
                case "Dictionary":
                    rowCount = dgvBDDictionary.RowCount;
                    break;
                case "Educations":
                    rowCount = dgvBDEducations.RowCount;
                    break;
                case "Family":
                    rowCount = dgvBDFamily.RowCount;
                    break;
                case "History":
                    rowCount = dgvBDHistory.RowCount;
                    break;
                case "Orders":
                    rowCount = dgvBDOrders.RowCount;
                    break;
                case "Medals":
                    rowCount = dgvBDMedals.RowCount;
                    break;
                case "Memory":
                    rowCount = dgvBDMemory.RowCount;
                    break;
                case "Peoples":
                    rowCount = dgvBDPeoples.RowCount;
                    break;
                case "Positions":
                    rowCount = dgvBDPositions.RowCount;
                    break;
                case "Primary":
                    rowCount = dgvBDPrimary.RowCount;
                    break;
                case "Settings":
                    rowCount = dgvBDSlaves.RowCount;
                    break;
                case "Slaves":
                    rowCount = dgvBDSlaves.RowCount;
                    break;
                case "Tasks":
                    rowCount = dgvBDTasks.RowCount;
                    break;
                case "Users":
                    rowCount = dgvBDUsers.RowCount;
                    break;
            }
            if (!cbBDSaveTable.Checked)
                rowCount--;

            for (var i = 0; i < rowCount; i++)
            {
                switch (lbBDTableChoose.Text)
                {
                    case "Dictionary":
                        if (dgvBDDictionary.Rows[i].Cells[0].Value == null ||
                            dgvBDDictionary.Rows[i].Cells[0].Value.ToString() == "")
                            _sqlCommand = new SqlCommand("INSERT INTO [" +
                                                        lbBDTableChoose.Text +
                                                        "] (name, decline1, decline2, action, actionUser) " +
                                                        "VALUES (@name, @decline1, @decline2, @action, @actionUser)",
                                _sqlConnection);
                        else
                        {
                            _sqlCommand = new SqlCommand("UPDATE [" +
                                                        lbBDTableChoose.Text +
                                                        "] SET [name]=@name, [decline1]=@decline1, [decline2]=@decline2, " +
                                                        "[action]=@action, [actionUser]=@actionUser WHERE [id]=@id",
                                _sqlConnection);
                            _sqlCommand.Parameters.AddWithValue("id",
                                Convert.ToInt32(dgvBDDictionary.Rows[i].Cells[0].Value));
                        }

                        _sqlCommand.Parameters.AddWithValue("name", dgvBDDictionary.Rows[i].Cells[1].Value);
                        _sqlCommand.Parameters.AddWithValue("decline1", dgvBDDictionary.Rows[i].Cells[2].Value);
                        _sqlCommand.Parameters.AddWithValue("decline2", dgvBDDictionary.Rows[i].Cells[3].Value);
                        _sqlCommand.Parameters.AddWithValue("action", DateTime.Now);
                        _sqlCommand.Parameters.AddWithValue("actionUser", _userName);
                        break;
                    case "Battlefields":
                        if (dgvBDBattlefields.Rows[i].Cells[0].Value == null ||
                            dgvBDBattlefields.Rows[i].Cells[0].Value.ToString() == "")
                            _sqlCommand = new SqlCommand("INSERT INTO [" +
                                                        lbBDTableChoose.Text +
                                                        "] (peopleId, name, dateText, action, actionUser) " +
                                                        "VALUES (@peopleId, @name, @dateText, @action, @actionUser)",
                                _sqlConnection);
                        else
                        {
                            _sqlCommand = new SqlCommand("UPDATE [" +
                                                        lbBDTableChoose.Text +
                                                        "] SET [peopleId]=@peopleId, [name]=@name, [dateText]=@dateText, " +
                                                        "[action]=@action, [actionUser]=@actionUser WHERE [id]=@id",
                                _sqlConnection);
                            _sqlCommand.Parameters.AddWithValue("id",
                                Convert.ToInt32(dgvBDBattlefields.Rows[i].Cells[0].Value));
                        }

                        _sqlCommand.Parameters.AddWithValue("peopleId",
                            Convert.ToInt32(dgvBDBattlefields.Rows[i].Cells[1].Value));
                        _sqlCommand.Parameters.AddWithValue("name", dgvBDBattlefields.Rows[i].Cells[2].Value);
                        _sqlCommand.Parameters.AddWithValue("dateText", dgvBDBattlefields.Rows[i].Cells[3].Value);
                        _sqlCommand.Parameters.AddWithValue("action", DateTime.Now);
                        _sqlCommand.Parameters.AddWithValue("actionUser", _userName);
                        break;
                    case "Educations":
                        if (dgvBDEducations.Rows[i].Cells[0].Value == null ||
                            dgvBDEducations.Rows[i].Cells[0].Value.ToString() == "")
                            _sqlCommand = new SqlCommand("INSERT INTO [" +
                                                        lbBDTableChoose.Text +
                                                        "] (peopleId, name, year, special, action, actionUser) " +
                                                        "VALUES (@peopleId, @name, @year, @special, @action, @actionUser)",
                                _sqlConnection);
                        else
                        {
                            _sqlCommand = new SqlCommand("UPDATE [" +
                                                        lbBDTableChoose.Text +
                                                        "] SET [peopleId]=@peopleId, [name]=@name, [year]=@year, [special]=@special, " +
                                                        "[action]=@action, [actionUser]=@actionUser WHERE [id]=@id",
                                _sqlConnection);
                            _sqlCommand.Parameters.AddWithValue("id",
                                Convert.ToInt32(dgvBDEducations.Rows[i].Cells[0].Value));
                        }

                        _sqlCommand.Parameters.AddWithValue("peopleId",
                            Convert.ToInt32(dgvBDEducations.Rows[i].Cells[1].Value));
                        _sqlCommand.Parameters.AddWithValue("name", dgvBDEducations.Rows[i].Cells[2].Value);
                        _sqlCommand.Parameters.AddWithValue("year",
                            Convert.ToInt32(dgvBDEducations.Rows[i].Cells[3].Value));
                        _sqlCommand.Parameters.AddWithValue("special", dgvBDEducations.Rows[i].Cells[4].Value);
                        _sqlCommand.Parameters.AddWithValue("action", DateTime.Now);
                        _sqlCommand.Parameters.AddWithValue("actionUser", _userName);
                        break;
                    case "Family":
                        if (dgvBDFamily.Rows[i].Cells[0].Value == null ||
                            dgvBDFamily.Rows[i].Cells[0].Value.ToString() == "")
                            _sqlCommand = new SqlCommand("INSERT INTO [" +
                                                        lbBDTableChoose.Text +
                                                        "] (peopleId, position, name, dateBirthday, " +
                                                        "action, actionUser) " +
                                                        "VALUES (@peopleId, @position, @name, @dateBirthday, " +
                                                        "@action, @actionUser)",
                                _sqlConnection);
                        else
                        {
                            _sqlCommand = new SqlCommand("UPDATE [" +
                                                        lbBDTableChoose.Text +
                                                        "] SET [peopleId]=@peopleId, [position]=@position, [name]=@name, " +
                                                        "[dateBirthday]=@dateBirthday, " +
                                                        "[action]=@action, [actionUser]=@actionUser WHERE [id]=@id",
                                _sqlConnection);
                            _sqlCommand.Parameters.AddWithValue("id",
                                Convert.ToInt32(dgvBDFamily.Rows[i].Cells[0].Value));
                        }

                        _sqlCommand.Parameters.AddWithValue("peopleId",
                            Convert.ToInt32(dgvBDFamily.Rows[i].Cells[1].Value));
                        _sqlCommand.Parameters.AddWithValue("position", dgvBDFamily.Rows[i].Cells[2].Value);
                        _sqlCommand.Parameters.AddWithValue("name", dgvBDFamily.Rows[i].Cells[3].Value);
                        _sqlCommand.Parameters.AddWithValue("dateBirthday",
                            Convert.ToDateTime(dgvBDFamily.Rows[i].Cells[4].Value));
                        _sqlCommand.Parameters.AddWithValue("action", DateTime.Now);
                        _sqlCommand.Parameters.AddWithValue("actionUser", _userName);
                        break;
                    case "History":
                        if (dgvBDHistory.Rows[i].Cells[0].Value == null ||
                            dgvBDHistory.Rows[i].Cells[0].Value.ToString() == "")
                            _sqlCommand = new SqlCommand("INSERT INTO [" +
                                                        lbBDTableChoose.Text +
                                                        "] (peopleId, name, orderId, " +
                                                        "action, actionUser) " +
                                                        "VALUES (@peopleId, @name, @orderId, " +
                                                        "@action, @actionUser)",
                                _sqlConnection);
                        else
                        {
                            _sqlCommand = new SqlCommand("UPDATE [" +
                                                        lbBDTableChoose.Text +
                                                        "] SET [peopleId]=@peopleId, [name]=@name, " +
                                                        "[orderId]=@orderId, " +
                                                        "[action]=@action, [actionUser]=@actionUser WHERE [id]=@id",
                                _sqlConnection);
                            _sqlCommand.Parameters.AddWithValue("id",
                                Convert.ToInt32(dgvBDHistory.Rows[i].Cells[0].Value));
                        }

                        _sqlCommand.Parameters.AddWithValue("peopleId",
                            Convert.ToInt32(dgvBDHistory.Rows[i].Cells[1].Value));
                        _sqlCommand.Parameters.AddWithValue("name", dgvBDHistory.Rows[i].Cells[2].Value);
                        _sqlCommand.Parameters.AddWithValue("orderId",
                            Convert.ToInt32(dgvBDHistory.Rows[i].Cells[3].Value));
                        _sqlCommand.Parameters.AddWithValue("action", DateTime.Now);
                        _sqlCommand.Parameters.AddWithValue("actionUser", _userName);
                        break;
                    case "Medals":
                        if (dgvBDMedals.Rows[i].Cells[0].Value == null ||
                            dgvBDMedals.Rows[i].Cells[0].Value.ToString() == "")
                            _sqlCommand = new SqlCommand("INSERT INTO [" +
                                                        lbBDTableChoose.Text +
                                                        "] (peopleId, name, orderId, action, actionUser) " +
                                                        "VALUES (@peopleId, @type, @name, @orderId, @action, @actionUser)",
                                _sqlConnection);
                        else
                        {
                            _sqlCommand = new SqlCommand("UPDATE [" +
                                                        lbBDTableChoose.Text +
                                                        "] SET [peopleId]=@peopleId, [name]=@name, [orderId]=@orderId, " +
                                                        "[action]=@action, [actionUser]=@actionUser WHERE [id]=@id",
                                _sqlConnection);
                            _sqlCommand.Parameters.AddWithValue("id",
                                Convert.ToInt32(dgvBDMedals.Rows[i].Cells[0].Value));
                        }

                        _sqlCommand.Parameters.AddWithValue("peopleId",
                            Convert.ToInt32(dgvBDMedals.Rows[i].Cells[1].Value));
                        _sqlCommand.Parameters.AddWithValue("name", dgvBDMedals.Rows[i].Cells[3].Value);
                        _sqlCommand.Parameters.AddWithValue("orderId",
                            Convert.ToInt32(dgvBDMedals.Rows[i].Cells[4].Value));
                        _sqlCommand.Parameters.AddWithValue("action", DateTime.Now);
                        _sqlCommand.Parameters.AddWithValue("actionUser", _userName);
                        break;
                    case "Memory":
                        if (dgvBDMemory.Rows[i].Cells[0].Value == null ||
                            dgvBDMemory.Rows[i].Cells[0].Value.ToString() == "")
                            _sqlCommand = new SqlCommand("INSERT INTO [" +
                                                        lbBDTableChoose.Text +
                                                        "] (peopleId, type, dateStart, dateEnd, isLast, variety, text, action, actionUser) " +
                                                        "VALUES (@peopleId, @type, @dateStart, @dateEnd, @isLast, @variety, @text, " +
                                                        "@action, @actionUser)",
                                _sqlConnection);
                        else
                        {
                            _sqlCommand = new SqlCommand("UPDATE [" +
                                                        lbBDTableChoose.Text +
                                                        "] SET [peopleId]=@peopleId, [type]=@type, [dateStart]=@dateStart, " +
                                                        "[dateEnd]=@dateEnd, [isLast]=@isLast, [variety]=@variety, [text]=@text, " +
                                                        "[action]=@action, [actionUser]=@actionUser WHERE [id]=@id",
                                _sqlConnection);
                            _sqlCommand.Parameters.AddWithValue("id",
                                Convert.ToInt32(dgvBDMemory.Rows[i].Cells[0].Value));
                        }

                        _sqlCommand.Parameters.AddWithValue("peopleId",
                            Convert.ToInt32(dgvBDMemory.Rows[i].Cells[1].Value));
                        _sqlCommand.Parameters.AddWithValue("type",
                            Convert.ToInt32(dgvBDMemory.Rows[i].Cells[2].Value));
                        _sqlCommand.Parameters.AddWithValue("dateStart", 
                            Convert.ToDateTime(dgvBDMemory.Rows[i].Cells[3].Value));
                        _sqlCommand.Parameters.AddWithValue("dateEnd",
                            Convert.ToDateTime(dgvBDMemory.Rows[i].Cells[4].Value));
                        _sqlCommand.Parameters.AddWithValue("isLast",
                            Convert.ToInt32(dgvBDMemory.Rows[i].Cells[5].Value));
                        _sqlCommand.Parameters.AddWithValue("variety",
                            Convert.ToSingle(dgvBDMemory.Rows[i].Cells[6].Value));
                        _sqlCommand.Parameters.AddWithValue("text", dgvBDMemory.Rows[i].Cells[7].Value);
                        _sqlCommand.Parameters.AddWithValue("action", DateTime.Now);
                        _sqlCommand.Parameters.AddWithValue("actionUser", _userName);
                        break;
                    case "Orders":
                        if (dgvBDOrders.Rows[i].Cells[0].Value == null || dgvBDOrders.Rows[i].Cells[0].Value.ToString() == "")
                            _sqlCommand = new SqlCommand("INSERT INTO [" +
                                                        lbBDTableChoose.Text +
                                                        "] (name, number, date, action, actionUser) " +
                                                        "VALUES (@name, @number, @date, @action, @actionUser)",
                                _sqlConnection);
                        else
                        {
                            _sqlCommand = new SqlCommand("UPDATE [" +
                                                        lbBDTableChoose.Text +
                                                        "] SET [name]=@name, [number]=@number, [date]=@date, " +
                                                        "[action]=@action, [actionUser]=@actionUser WHERE [id]=@id",
                                _sqlConnection);
                            _sqlCommand.Parameters.AddWithValue("id",
                                Convert.ToInt32(dgvBDOrders.Rows[i].Cells[0].Value));
                        }

                        _sqlCommand.Parameters.AddWithValue("name", dgvBDOrders.Rows[i].Cells[1].Value);
                        _sqlCommand.Parameters.AddWithValue("number", dgvBDOrders.Rows[i].Cells[2].Value);
                        _sqlCommand.Parameters.AddWithValue("date",
                            Convert.ToDateTime(dgvBDOrders.Rows[i].Cells[3].Value));
                        _sqlCommand.Parameters.AddWithValue("action", DateTime.Now);
                        _sqlCommand.Parameters.AddWithValue("actionUser", _userName);
                        break;
                    case "Peoples":
                        if (dgvBDPeoples.Rows[i].Cells[0].Value == null ||
                            dgvBDPeoples.Rows[i].Cells[0].Value.ToString() == "")
                            _sqlCommand =
                                new SqlCommand(
                                    "INSERT INTO [" + lbBDTableChoose.Text +
                                    "] " +
                                    "(fio0, fio1, fio2, gender, phoneNumber, lNumber, tableNumber, " +
                                    "dateBirthday, placeBirthday, primaryId, primaryOrderId, positionId, " +
                                    "positionOrderId, damages, numberNIS, start, startThis, action, actionUser) " +
                                    "VALUES (@fio0, @fio1, @fio2, @gender, @phoneNumber, @lNumber, @tableNumber, " +
                                    "@dateBirthday, @placeBirthday, @primaryId, @primaryOrderId, @positionId, " +
                                    "@positionOrderId, @damages, @numberNIS, @start, startThis, @action, @actionUser)",
                                    _sqlConnection);
                        else
                        {
                            _sqlCommand = 
                                new SqlCommand("UPDATE [" +
                                                        lbBDTableChoose.Text +
                                                        "] SET [fio0]=@fio0, [fio1]=@fio1, [fio2]=@fio2, [gender]=@gender, [phoneNumber]=@phoneNumber, " +
                                                        "[lNumber]=@lNumber, [tableNumber]=@tableNumber, " +
                                                        "[dateBirthday]=@dateBirthday, [placeBirthday]=@placeBirthday, " +
                                                        "[primaryId]=@primaryId, [primaryOrderId]=@primaryOrderId, [positionId]=@positionId, " +
                                                        "[positionOrderId]=@positionOrderId, [damages]=@damages, [numberNIS]=@numberNIS, " +
                                                        "[start]=@start, [startThis]=@startThis, [action]=@action, [actionUser]=@actionUser WHERE [id]=@id",
                                _sqlConnection);
                            _sqlCommand.Parameters.AddWithValue("id",
                                Convert.ToInt32(dgvBDPeoples.Rows[i].Cells[0].Value));
                        }

                        _sqlCommand.Parameters.AddWithValue("fio0", dgvBDPeoples.Rows[i].Cells[1].Value.ToString());
                        _sqlCommand.Parameters.AddWithValue("fio1", dgvBDPeoples.Rows[i].Cells[2].Value.ToString());
                        _sqlCommand.Parameters.AddWithValue("fio2", dgvBDPeoples.Rows[i].Cells[3].Value.ToString());
                        _sqlCommand.Parameters.AddWithValue("gender", dgvBDPeoples.Rows[i].Cells[4].Value.ToString());
                        _sqlCommand.Parameters.AddWithValue("phoneNumber", dgvBDPeoples.Rows[i].Cells[5].Value.ToString());
                        _sqlCommand.Parameters.AddWithValue("lNumber", dgvBDPeoples.Rows[i].Cells[6].Value.ToString());
                        _sqlCommand.Parameters.AddWithValue("tableNumber", 
                            Convert.ToInt32(dgvBDPeoples.Rows[i].Cells[7].Value));
                        _sqlCommand.Parameters.AddWithValue("dateBirthday",
                            Convert.ToDateTime(dgvBDPeoples.Rows[i].Cells[8].Value));
                        _sqlCommand.Parameters.AddWithValue("placeBirthday", dgvBDPeoples.Rows[i].Cells[9].Value.ToString());
                        _sqlCommand.Parameters.AddWithValue("primaryId",
                            Convert.ToInt32(dgvBDPeoples.Rows[i].Cells[10].Value));
                        _sqlCommand.Parameters.AddWithValue("primaryDate",
                            Convert.ToDateTime(dgvBDPeoples.Rows[i].Cells[11].Value));
                        _sqlCommand.Parameters.AddWithValue("primaryOrderId",
                            Convert.ToInt32(dgvBDPeoples.Rows[i].Cells[12].Value));
                        _sqlCommand.Parameters.AddWithValue("positionId",
                            Convert.ToInt32(dgvBDPeoples.Rows[i].Cells[13].Value));
                        _sqlCommand.Parameters.AddWithValue("positionOrderId",
                            Convert.ToInt32(dgvBDPeoples.Rows[i].Cells[14].Value));
                        _sqlCommand.Parameters.AddWithValue("damages", dgvBDPeoples.Rows[i].Cells[15].Value.ToString());
                        _sqlCommand.Parameters.AddWithValue("numberNIS", dgvBDPeoples.Rows[i].Cells[16].Value.ToString());
                        _sqlCommand.Parameters.AddWithValue("start",
                            Convert.ToDateTime(dgvBDPeoples.Rows[i].Cells[17].Value));
                        _sqlCommand.Parameters.AddWithValue("startThis",
                            Convert.ToDateTime(dgvBDPeoples.Rows[i].Cells[18].Value));
                        _sqlCommand.Parameters.AddWithValue("action", DateTime.Now);
                        _sqlCommand.Parameters.AddWithValue("actionUser", _userName);
                        break;
                    case "Positions":
                        if (dgvBDPositions.Rows[i].Cells[1].Value == null ||
                            dgvBDPositions.Rows[i].Cells[1].Value.ToString() == "")
                            _sqlCommand = new SqlCommand("INSERT INTO [" +
                                                        lbBDTableChoose.Text +
                                                        "] (position, parent1, parent2, parent3, parent4, name, fullName, " +
                                                        "vus, primaryId, tarif, " +
                                                        "action, actionUser) VALUES (@position, @parent1, @parent2, @parent3," +
                                                        "@parent4, @name, @fullName, " +
                                                        "@vus, @primaryId, @tarif, @action, @actionUser)",
                                _sqlConnection);
                        else
                        {
                            _sqlCommand = new SqlCommand("UPDATE [" + lbBDTableChoose.Text +
                                                        "] SET [position]=@position, [parent1]=@parent1, [parent2]=@parent2, [parent3]=@parent3," +
                                                        "[parent4]=@parent4, [name]=@name, [fullName]=@fullName, [vus]=@vus, " +
                                                        "[primaryId]=@primaryId, [tarif]=@tarif, [action]=@action, [actionUser]=@actionUser" +
                                                        " WHERE [id]=@id", _sqlConnection);
                            _sqlCommand.Parameters.AddWithValue("id",
                                Convert.ToInt32(dgvBDPositions.Rows[i].Cells[1].Value));
                        }
                        _sqlCommand.Parameters.AddWithValue("position", 
                            Convert.ToSingle(dgvBDPositions.Rows[i].Cells[0].Value));
                        _sqlCommand.Parameters.AddWithValue("parent1", dgvBDPositions.Rows[i].Cells[2].Value.ToString());
                        _sqlCommand.Parameters.AddWithValue("parent2", dgvBDPositions.Rows[i].Cells[3].Value.ToString());
                        _sqlCommand.Parameters.AddWithValue("parent3", dgvBDPositions.Rows[i].Cells[4].Value.ToString());
                        _sqlCommand.Parameters.AddWithValue("parent4", dgvBDPositions.Rows[i].Cells[5].Value.ToString());
                        _sqlCommand.Parameters.AddWithValue("name", dgvBDPositions.Rows[i].Cells[6].Value.ToString());
                        _sqlCommand.Parameters.AddWithValue("fullName", dgvBDPositions.Rows[i].Cells[7].Value.ToString());
                        _sqlCommand.Parameters.AddWithValue("vus", dgvBDPositions.Rows[i].Cells[8].Value.ToString());
                        _sqlCommand.Parameters.AddWithValue("primaryId",
                            Convert.ToInt32(dgvBDPositions.Rows[i].Cells[9].Value));
                        _sqlCommand.Parameters.AddWithValue("tarif",
                            Convert.ToInt32(dgvBDPositions.Rows[i].Cells[10].Value));
                        _sqlCommand.Parameters.AddWithValue("action", DateTime.Now);
                        _sqlCommand.Parameters.AddWithValue("actionUser", _userName);
                        break;
                    case "Primary":
                        if (dgvBDPrimary.Rows[i].Cells[0].Value == null || dgvBDPrimary.Rows[i].Cells[0].Value.ToString() == "")
                            _sqlCommand = new SqlCommand("INSERT INTO [" + lbBDTableChoose.Text +
                                                        "] (type, name) " +
                                                        "VALUES (@type, @name)", _sqlConnection);
                        else
                        {
                            _sqlCommand = new SqlCommand("UPDATE [" + lbBDTableChoose.Text +
                                                        "] SET [type]=@type, [name]=@name, [action]=@action, [actionUser]=@actionUser" +
                                                        " WHERE [id]=@id", _sqlConnection);
                            _sqlCommand.Parameters.AddWithValue("id",
                                Convert.ToInt32(dgvBDPrimary.Rows[i].Cells[0].Value));
                        }
                        _sqlCommand.Parameters.AddWithValue("type", dgvBDPrimary.Rows[i].Cells[1].Value.ToString());
                        _sqlCommand.Parameters.AddWithValue("name", dgvBDPrimary.Rows[i].Cells[2].Value.ToString());
                        _sqlCommand.Parameters.AddWithValue("action", DateTime.Now);
                        _sqlCommand.Parameters.AddWithValue("actionUser", _userName);
                        break;
                    case "Settings":
                        if (dgvBDSettings.Rows[i].Cells[0].Value == null || dgvBDSettings.Rows[i].Cells[0].Value.ToString() == "")
                            _sqlCommand = new SqlCommand("INSERT INTO [" + lbBDTableChoose.Text +
                                                        "] (nshId, nokId, action, actionUser) VALUES (@nshId, @nokId, @action," +
                                                        "@actionUser)", _sqlConnection);
                        else
                        {
                            _sqlCommand = new SqlCommand("UPDATE [" + lbBDTableChoose.Text +
                                                        "] SET [nshId]=@nshId, [nokId]=@nokId, [action]=@action," +
                                                        "[actionUser]=@actionUser WHERE [id]=@id", _sqlConnection);
                            _sqlCommand.Parameters.AddWithValue("id",
                                Convert.ToInt32(dgvBDSettings.Rows[i].Cells[0].Value));
                        }
                        _sqlCommand.Parameters.AddWithValue("nshId", 
                            Convert.ToInt32(dgvBDSettings.Rows[i].Cells[1].Value));
                        _sqlCommand.Parameters.AddWithValue("nokId", 
                            Convert.ToInt32(dgvBDSettings.Rows[i].Cells[2].Value));
                        _sqlCommand.Parameters.AddWithValue("action", DateTime.Now);
                        _sqlCommand.Parameters.AddWithValue("actionUser", _userName);
                        break;
                    case "Slaves":
                        if (dgvBDSlaves.Rows[i].Cells[0].Value == null || dgvBDSlaves.Rows[i].Cells[0].Value.ToString() == "")
                            _sqlCommand = new SqlCommand("INSERT INTO [" + lbBDTableChoose.Text +
                                                        "] (peopleId, slaveStart, slaveEnd, orderId, action," +
                                                        "actionUser) VALUES (@peopleId, @slaveStart, @slaveEnd, @orderId, @action," +
                                                        "@actionUser)", _sqlConnection);
                        else
                        {
                            _sqlCommand = new SqlCommand("UPDATE [" + lbBDTableChoose.Text +
                                                        "] SET [peopleId]=@peopleId, [slaveStart]=@slaveStart, " +
                                                        "[slaveEnd]=@slaveEnd, [orderId]=@orderId, [action]=@action," +
                                                        "[actionUser]=@actionUser WHERE [id]=@id", _sqlConnection);
                            _sqlCommand.Parameters.AddWithValue("id",
                                Convert.ToInt32(dgvBDSlaves.Rows[i].Cells[0].Value));
                        }
                        _sqlCommand.Parameters.AddWithValue("peopleId", dgvBDSlaves.Rows[i].Cells[1].Value.ToString());
                        _sqlCommand.Parameters.AddWithValue("slaveStart", 
                            Convert.ToDateTime(dgvBDSlaves.Rows[i].Cells[2].Value));
                        _sqlCommand.Parameters.AddWithValue("slaveEnd",
                            Convert.ToDateTime(dgvBDSlaves.Rows[i].Cells[3].Value));
                        _sqlCommand.Parameters.AddWithValue("orderId",
                            Convert.ToInt32(dgvBDSlaves.Rows[i].Cells[4].Value));
                        _sqlCommand.Parameters.AddWithValue("action", DateTime.Now);
                        _sqlCommand.Parameters.AddWithValue("actionUser", _userName);
                        break;
                    case "Tasks":
                        if (dgvBDTasks.Rows[i].Cells[0].Value == null || dgvBDTasks.Rows[i].Cells[0].Value.ToString() == "")
                            _sqlCommand = new SqlCommand("INSERT INTO [" + lbBDTableChoose.Text +
                                                        "] (destination, name, peopleId, isWork, action," +
                                                        "actionUser, nameWork, dateWork) " +
                                                        "VALUES (@destination, @name, @peopleId, @isWork, @action," +
                                                        "@actionUser, @nameWork, @dateWork)", _sqlConnection);
                        else
                        {
                            _sqlCommand = new SqlCommand("UPDATE [" + lbBDTableChoose.Text +
                                                        "] SET [destination]=@destination, [name]=@name, " +
                                                        "[peopleId]=@peopleId, [isWork]=@isWork, [action]=@action," +
                                                        "[actionUser]=@actionUser, [nameWork]=@nameWork, " +
                                                        "[dateWork]=@dateWork WHERE [id]=@id", _sqlConnection);
                            _sqlCommand.Parameters.AddWithValue("id",
                                Convert.ToInt32(dgvBDTasks.Rows[i].Cells[0].Value));
                        }
                        _sqlCommand.Parameters.AddWithValue("destination", dgvBDTasks.Rows[i].Cells[1].Value.ToString());
                        _sqlCommand.Parameters.AddWithValue("name", dgvBDTasks.Rows[i].Cells[2].Value);
                        _sqlCommand.Parameters.AddWithValue("peopleId",
                            Convert.ToInt32(dgvBDTasks.Rows[i].Cells[3].Value));
                        _sqlCommand.Parameters.AddWithValue("isWork",
                            Convert.ToInt32(dgvBDTasks.Rows[i].Cells[4].Value));
                        _sqlCommand.Parameters.AddWithValue("action", DateTime.Now);
                        _sqlCommand.Parameters.AddWithValue("actionUser", _userName);
                        _sqlCommand.Parameters.AddWithValue("nameWork", dgvBDTasks.Rows[i].Cells[7].Value.ToString());
                        _sqlCommand.Parameters.AddWithValue("dateWork",
                            Convert.ToDateTime(dgvBDTasks.Rows[i].Cells[8].Value));
                        break;
                    case "Users":
                        if (dgvBDUsers.Rows[i].Cells[0].Value == null || dgvBDUsers.Rows[i].Cells[0].Value.ToString() == "")
                            _sqlCommand = new SqlCommand("INSERT INTO [" + lbBDTableChoose.Text +
                                                        "] (name, password, rights, action, actionUser) " +
                                                        "VALUES (@name, @password, @rights, @action," +
                                                        "@actionUser)", _sqlConnection);
                        else
                        {
                            _sqlCommand = new SqlCommand("UPDATE [" + lbBDTableChoose.Text +
                                                        "] SET [name]=@name, " +
                                                        "[password]=@password, [rights]=@rights, [action]=@action," +
                                                        "[actionUser]=@actionUser WHERE [id]=@id", _sqlConnection);
                            _sqlCommand.Parameters.AddWithValue("id",
                                Convert.ToInt32(dgvBDUsers.Rows[i].Cells[0].Value));
                        }
                        _sqlCommand.Parameters.AddWithValue("name", dgvBDUsers.Rows[i].Cells[1].Value);
                        _sqlCommand.Parameters.AddWithValue("password", dgvBDUsers.Rows[i].Cells[2].Value);
                        _sqlCommand.Parameters.AddWithValue("rights", dgvBDUsers.Rows[i].Cells[3].Value);
                        _sqlCommand.Parameters.AddWithValue("action", DateTime.Now);
                        _sqlCommand.Parameters.AddWithValue("actionUser", _userName);
                        break;
                }
                _sqlCommand.ExecuteNonQuery();
            }
            new DialogForm
            {
                lText = {Text = "Сохранено в базе"},
                bCancel = {Visible = false},
                bOk = {Width = 200}
            }.ShowDialog();
        }

        private void LbBDTableChoose_SelectedIndexChanged(object sender, EventArgs e)
        {
            //скрыть все кроме активной
            foreach (var dataGridView in panel1.Controls.OfType<DataGridView>())
                dataGridView.Visible = dataGridView.Name == "dgvBD" + lbBDTableChoose.Text;
        }

        /// <summary>
        /// Очистка таблицы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BBDClear_Click(object sender, EventArgs e)
        {
            var dialogForm = new DialogForm
            {
                lText = {Text = "Сохранено в базе"},
                bCancel = {Visible = false},
                bOk = {Width = 200}
            };
            var dr = dialogForm.ShowDialog();
            if (dr != DialogResult.OK) return;
            _sqlCommand = new SqlCommand("DELETE FROM [" + lbBDTableChoose.Text + "]", _sqlConnection);
            _sqlCommand.ExecuteNonQuery();
        }

        /// <summary>
        /// Копирование выбранной информации в буфер обмена
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BBDCopy_Click(object sender, EventArgs e)
        {
            
            var clipString = "";
            switch (lbBDTableChoose.Text)
            {
                case "Dictionary":
                    for (var i = 0; i < dgvBDDictionary.RowCount - 1; i++)
                    {
                        for (var j = 0; j < dgvBDDictionary.ColumnCount; j++)
                        {
                            if (j != 0)
                                clipString += "\t";
                            clipString += dgvBDDictionary[j, i].Value;
                        }

                        clipString += "\n";
                    }
                    break;
                case "Battlefields":
                    for (var i = 0; i < dgvBDBattlefields.RowCount - 1; i++)
                    {
                        for (var j = 0; j < dgvBDBattlefields.ColumnCount; j++)
                        {
                            if (j != 0)
                                clipString += "\t";
                            clipString += dgvBDBattlefields[j, i].Value;
                        }

                        clipString += "\n";
                    }
                    break;
                case "Educations":
                    for (var i = 0; i < dgvBDEducations.RowCount - 1; i++)
                    {
                        for (var j = 0; j < dgvBDEducations.ColumnCount; j++)
                        {
                            if (j != 0)
                                clipString += "\t";
                            clipString += dgvBDEducations[j, i].Value;
                        }

                        clipString += "\n";
                    }
                    break;
                case "Family":
                    for (var i = 0; i < dgvBDFamily.RowCount - 1; i++)
                    {
                        for (var j = 0; j < dgvBDFamily.ColumnCount; j++)
                        {
                            if (j != 0)
                                clipString += "\t";
                            clipString += dgvBDFamily[j, i].Value;
                        }

                        clipString += "\n";
                    }
                    break;
                case "History":
                    for (var i = 0; i < dgvBDHistory.RowCount - 1; i++)
                    {
                        for (var j = 0; j < dgvBDHistory.ColumnCount; j++)
                        {
                            if (j != 0)
                                clipString += "\t";
                            clipString += dgvBDHistory[j, i].Value;
                        }

                        clipString += "\n";
                    }
                    break;
                case "Medals":
                    for (var i = 0; i < dgvBDMedals.RowCount - 1; i++)
                    {
                        for (var j = 0; j < dgvBDMedals.ColumnCount; j++)
                        {
                            if (j != 0)
                                clipString += "\t";
                            clipString += dgvBDMedals[j, i].Value;
                        }

                        clipString += "\n";
                    }
                    break;
                case "Memory":
                    for (var i = 0; i < dgvBDMemory.RowCount - 1; i++)
                    {
                        for (var j = 0; j < dgvBDMemory.ColumnCount; j++)
                        {
                            if (j != 0)
                                clipString += "\t";
                            clipString += dgvBDMemory[j, i].Value;
                        }

                        clipString += "\n";
                    }
                    break;
                case "Orders":
                    for (var i = 0; i < dgvBDOrders.RowCount - 1; i++)
                    {
                        for (var j = 0; j < dgvBDOrders.ColumnCount; j++)
                        {
                            if (j != 0)
                                clipString += "\t";
                            clipString += dgvBDOrders[j, i].Value;
                        }

                        clipString += "\n";
                    }
                    break;
                case "Peoples":
                    for (var i = 0; i < dgvBDPeoples.RowCount - 1; i++)
                    {
                        for (var j = 0; j < dgvBDPeoples.ColumnCount; j++)
                        {
                            if (j != 0)
                                clipString += "\t";
                            clipString += dgvBDPeoples[j, i].Value;
                        }

                        clipString += "\n";
                    }
                    break;
                case "Positions":
                    for (var i = 0; i < dgvBDPositions.RowCount - 1; i++)
                    {
                        for (var j = 0; j < dgvBDPositions.ColumnCount; j++)
                        {
                            if (j != 0)
                                clipString += "\t";
                            clipString += dgvBDPositions[j, i].Value;
                        }

                        clipString += "\n";
                    }
                    break;
                case "Primary":
                    for (var i = 0; i < dgvBDPrimary.RowCount - 1; i++)
                    {
                        for (var j = 0; j < dgvBDPrimary.ColumnCount; j++)
                        {
                            if (j != 0)
                                clipString += "\t";
                            clipString += dgvBDPrimary[j, i].Value;
                        }
                        clipString += "\n";
                    }
                    break;
                case "Settings":
                    for (var i = 0; i < dgvBDSettings.RowCount - 1; i++)
                    {
                        for (var j = 0; j < dgvBDSettings.ColumnCount; j++)
                        {
                            if (j != 0)
                                clipString += "\t";
                            clipString += dgvBDSettings[j, i].Value;
                        }
                        clipString += "\n";
                    }
                    break;
                case "Slaves":
                    for (var i = 0; i < dgvBDSlaves.RowCount - 1; i++)
                    {
                        for (var j = 0; j < dgvBDSlaves.ColumnCount; j++)
                        {
                            if (j != 0)
                                clipString += "\t";
                            clipString += dgvBDSlaves[j, i].Value;
                        }
                        clipString += "\n";
                    }
                    break;
                case "Tasks":
                    for (var i = 0; i < dgvBDTasks.RowCount - 1; i++)
                    {
                        for (var j = 0; j < dgvBDTasks.ColumnCount; j++)
                        {
                            if (j != 0)
                                clipString += "\t";
                            clipString += dgvBDTasks[j, i].Value;
                        }
                        clipString += "\n";
                    }
                    break;
                case "Users":
                    for (var i = 0; i < dgvBDUsers.RowCount - 1; i++)
                    {
                        for (var j = 0; j < dgvBDUsers.ColumnCount; j++)
                        {
                            if (j != 0)
                                clipString += "\t";
                            clipString += dgvBDUsers[j, i].Value;
                        }
                        clipString += "\n";
                    }
                    break;

            }

            Clipboard.SetText(clipString);
            new DialogForm
            {
                lText = {Text = "Скопировано в буфер"},
                bCancel = {Visible = false},
                bOk = {Width = 200}
            }.ShowDialog();
        }

        private void bClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
