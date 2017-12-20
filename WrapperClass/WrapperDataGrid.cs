using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

using System.ComponentModel;

namespace WrapperUnion
{
    public class WrapperDGView // 171010
    {
        public DataGridView m_dgView = new DataGridView();

        public int COLS { get { return m_dgView.ColumnCount; } }
        public int ROWS { get { return m_dgView.RowCount; } }

        public int SELECTED_COL { get; set; }
        public int SELECTED_ROW { get; set; }

        public WrapperDGView()
        {

        }
      


        public void SetControl(DataGridView view)
        {
            m_dgView = view;
        }
        public void Clear()
        {
            m_dgView.Rows.Clear();
        }

        public void SetHeaderNames(string[] arrHeader)
        {
            int nCount = arrHeader.Length;

            // set column count
            m_dgView.ColumnCount = nCount;

            for (int i = 0; i < nCount; i++)
            {
                m_dgView.Columns[i].Name = arrHeader[i];
            }
        }
        public List<string> GetHeaderNames()
        {
            List<string> list = new List<string>();

            for (int i = 0; i < COLS; i++)
            {
                list.Add(m_dgView.Columns[i].Name);
            }
            return list;
        }

        public void InsertData(string[] data)
        {
            string[] temp = new string[COLS];

            // exception for empty data input
            if (data.Length == 0) return;

            // exception for long data input
            if (data.Length > temp.Length)
            {
                for (int i = 0; i < temp.Length; i++)
                {
                    temp[i] = data[i];
                }
            }
            // in case of short input or normal case 
            if (data.Length <= temp.Length)
            {
                for (int i = 0; i < data.Length; i++)
                {
                    temp[i] = data[i];
                }
            }
            m_dgView.Rows.Add(temp);
        }
        public void SortData(int nColumn, bool bAscending = true)
        {
            if (COLS >= nColumn)
            {
                m_dgView.Sort(m_dgView.Columns[nColumn], bAscending  == true ? ListSortDirection.Ascending : ListSortDirection.Descending );
            }
        }

        public void Column_Increse()
        {
            int ncols = COLS;
            m_dgView.ColumnCount = ncols + 1;
        }
        public void Column_Decrese()
        {
            int ncols = COLS;
            m_dgView.ColumnCount = ncols - 1;
        }

        public void Delete_Row(int nIndex) { if (ROWS-1 > nIndex) { m_dgView.Rows.RemoveAt(nIndex); } }
        public void Delete_Col(int nIndex) { if (COLS-1 > nIndex) { m_dgView.Columns.RemoveAt(nIndex); } }

        public void Delete_Row_Selected()
        {
            foreach (DataGridViewRow row in m_dgView.SelectedRows)
            {
                if (!row.IsNewRow) m_dgView.Rows.Remove(row);
            }
        }
        public void Delete_Col_Selected()
        {
            int nSelected = SELECTED_COL;

            List<string[]> list_old = ToStringArray();
            List<string[]> list_new = new List<string[]>();

            string[] header_old = GetHeaderNames().ToArray();
            string[] header_new = new string[COLS - 1];
            header_new = header_new.Select(element => element = "").ToArray();

            for (int y = 0; y < ROWS; y++)
            {
                string[] row_new = new string[COLS - 1];
                string[] row_old = GetRow(y);

                for (int x = 0, newIndex = 0; x < COLS; x++)
                {
                    if(x == nSelected) continue;

                    row_new[newIndex] = row_old[x];
                    header_old[newIndex] = header_old[x];
                    newIndex++;
                }
                list_new.Add(row_new);
            }
            SetHeaderNames(header_new);
            DisplayData(list_new);

        }

        public void MovePos_Row_Back()
        {
            int nPos = GetSelectedIndex_Row();

            if (nPos == 0) return; // border exception 171011
            m_dgView.ClearSelection();
            m_dgView.Rows[nPos - 1].Selected = true;
        }
        public void MovePos_Row_Forward()
        {
            int nPos = GetSelectedIndex_Row();

            if (nPos == ROWS - 1) return; // border exception 171011

            m_dgView.ClearSelection();
            m_dgView.Rows[nPos + 1].Selected = true;
        }

        public int GetSelectedIndex_Row()
        {
            if (m_dgView.SelectedCells.Count == 0) return 0; // empty exception 171011
            int currentRowindex = m_dgView.SelectedCells[0].OwningRow.Index;

            return currentRowindex;
        }
        public void InsertEmptyRow(int nIndex)
        {
            m_dgView.Rows.Insert(nIndex+1, 1);
        }
        public void InsertEmpty_Col(int nIndex)
        {
            nIndex += 1;

            List<string[]> list = ToStringArray();
            List<string[]> listExpand = new List<string[]>();

            string[] headers_Old = GetHeaderNames().ToArray();
            string[] headers_New = new string[COLS + 1];

            for (int row = 0; row < list.Count; row++)
            {
                string[] rowData_New = new string[COLS + 1];

                string [] rowData_Old = list.ElementAt(row);

                if (nIndex == 0)
                {
                    for (int j = 0; j < COLS; j++)
                    {
                        rowData_New[j + 1] = rowData_Old[j];
                        headers_New[j + 1] = headers_Old[j];
                    }
                }
                else
                {
                    // head 
                    for (int j = 0; j < nIndex; j++)
                    {
                        rowData_New[j] = rowData_Old[j];
                        headers_New[j] = headers_Old[j];
                    }
                    // set empty string
                    rowData_New[nIndex] = string.Empty;

                    // tail
                    for (int j = nIndex; j < COLS; j++)
                    {
                        rowData_New[j + 1] = rowData_Old[j];
                        headers_New[j + 1] = headers_Old[j];
                    }
                }
                listExpand.Add(rowData_New);
            }

            SetHeaderNames(headers_New);
            DisplayData(listExpand);

            

        }

        public void DisplayData(List<string[]> list)
        {
            if (list.Count != 0)
            {
                int nMaxCols = 0;

                for (int x = 0; x < list.Count; x++)
                {
                    if (list.ElementAt(0).Length >= nMaxCols)
                    {
                        nMaxCols = list.ElementAt(x).Length;
                    }
                }

                int nRows = list.Count;

                Clear();

                m_dgView.ColumnCount = nMaxCols;

                for (int y = 0; y < nRows; y++)
                {
                    InsertData(list.ElementAt(y));
                }

            }
        }

        public string[] GetRow(int nIndex)
        {
            string[] arr = new string[COLS];

            for (int i = 0; i < COLS; i++)
            {
                DataGridViewCell cell = m_dgView[i, nIndex];
                if (cell.Value == null)
                {
                    arr[i] = "";
                }
                else
                {
                    arr[i] = cell.Value.ToString();
                }
            }
            return arr;
        }

        public List<string[]> ToStringArray()
        {
            List<string[]> list = new List<string[]>();

            for (int row = 0; row < ROWS-1; row++)
            {
                string[] single = GetRow(row);

                list.Add(single);
            }
            return list;
        }

        public void event_CellClick(DataGridViewCellEventArgs e)
        {
            SELECTED_COL = e.ColumnIndex;
            SELECTED_ROW = e.RowIndex;
            for (int x = 0; x < COLS; x++)
            {
                DataGridViewColumn firstColumn = m_dgView.Columns[x];
                DataGridViewCellStyle cellStyle = new DataGridViewCellStyle();

                if (x == e.ColumnIndex)
                {

                    cellStyle.BackColor = Color.DodgerBlue;
                    cellStyle.ForeColor = Color.White;
                    firstColumn.DefaultCellStyle = cellStyle;
                }
                else
                {
                    cellStyle.BackColor = Color.White;
                    cellStyle.ForeColor = Color.Black;
                    firstColumn.DefaultCellStyle = cellStyle;
                }
            }

            //touch one cell
            //for (int y = 0; y < ROWS; y++)
            //{
            //    for (int x = 0; x < COLS; x++)
            //    {
            //        if (e.ColumnIndex == x && e.RowIndex == y)
            //        {
            //            DataGridViewCellStyle cellStyle = new DataGridViewCellStyle();
            //            cellStyle.BackColor = Color.Orange;
            //            cellStyle.ForeColor = Color.White;
            //
            //            DataGridViewCell cell = m_dgView[x, y];
            //            cell.Style = cellStyle;
            //        }
            //    }
            //}
        }
        public void event_KeyDown(KeyEventArgs e)
        {
            e.SuppressKeyPress = true;

            if (e.Modifiers == Keys.Control)
            {
                switch (e.KeyCode)
                {
                    case Keys.C:
                        CopyToClipboard();
                        break;
                    case Keys.V:
                        PasteClipboardValue();
                        break;
                }
            }
            else if (e.KeyCode == Keys.Up)
            {
                MovePos_Row_Back();
            }
            else if (e.KeyCode == Keys.Down)
            {
                MovePos_Row_Forward();
            }
            else if (e.KeyCode == Keys.Insert)
            {
                int nIndex = SELECTED_ROW;
                InsertEmptyRow(nIndex);
            }
            else if (e.KeyCode == Keys.Delete)
            {
                Delete_Row_Selected();
            }
        }
        private void CopyToClipboard()
        {
            //Copy to clipboard
            DataObject dataObj = m_dgView.GetClipboardContent();

            if (dataObj != null)
            {
                Clipboard.SetDataObject(dataObj);
            }
        }

        private void PasteClipboardValue()
        {
            //Show Error if no cell is selected
            if (m_dgView.SelectedCells.Count == 0)
            {
                MessageBox.Show("Please select a cell", "Paste", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //Get the satring Cell
            DataGridViewCell startCell = GetStartCell(m_dgView);

            string strClipboard = Clipboard.GetText();
            //Get the clipboard value in a dictionary
            Dictionary<int, Dictionary<int, string>> cbValue = ClipBoardValues(strClipboard);

            int rows = startCell.RowIndex;

            for (int nItem = 0; nItem < cbValue.Count; nItem++)
            {
                InsertEmptyRow(rows);
            }


            for (int y = 0; y < cbValue.Count; y++)
            {
                int cols = 0;
                rows++; // insert from the next position  one by one

                // enlarge data table if new data width has long  width compared with previous data table width;
                int nItemCount = cbValue[y].Count;
                if (COLS < nItemCount) m_dgView.ColumnCount = nItemCount;

                for (int x = 0; x < cbValue[y].Count; x++)
                {
                    // array[0] has empty value, so start from 1

                    DataGridViewCell cell = m_dgView[cols++, rows];
                    cell.Value = cbValue[y][x];
                }
            }
        }
        private DataGridViewCell GetStartCell(DataGridView dgView)
        {
            //get the smallest row,column index
            if (dgView.SelectedCells.Count == 0)
                return null;

            int rows = dgView.Rows.Count - 1;
            int cols = dgView.Columns.Count - 1;

            foreach (DataGridViewCell dgvCell in dgView.SelectedCells)
            {
                if (dgvCell.RowIndex < rows)
                    rows = dgvCell.RowIndex;
                if (dgvCell.ColumnIndex < cols)
                    cols = dgvCell.ColumnIndex;
            }
            return dgView[cols, rows];
        }

        private Dictionary<int, Dictionary<int, string>> ClipBoardValues(string clipboardValue)
        {
            Dictionary<int, Dictionary<int, string>> copyValues = new Dictionary<int, Dictionary<int, string>>();

            String[] lines = clipboardValue.Split('\n');

            for (int i = 0; i <= lines.Length - 1; i++)
            {
                String[] lineContent = lines[i].Split('\t');

                // empty line verification : 
                // - pure datagridview, when ctrl + c pressed --> first item is entered as empty""
                
                List<string> listBuffer = new List<string>();
                for (int item = 0; item < lineContent.Length; item++)
                {
                    if (lineContent.ElementAt(item) != "")
                    {
                        listBuffer.Add(lineContent[item]);
                    }
                }
                lineContent = listBuffer.ToArray();

                copyValues[i] = new Dictionary<int, string>();

                //if an empty cell value copied, then set the dictionay with an empty string
                //else Set value to dictionary

                if (lineContent.Length == 0)
                {
                    copyValues[i][0] = string.Empty;
                }
                else
                {
                    for (int j = 0; j <= lineContent.Length - 1; j++)
                    {
                        copyValues[i][j] = lineContent[j];
                    }
                }
            }
            return copyValues;
        }

    }
}
