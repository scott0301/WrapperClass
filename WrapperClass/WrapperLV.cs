using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections;

using System.Windows.Forms;

namespace WrapperUnion
{
    public static class WrapperLV
    {

        public static void ScrollDown(ListView lv)
        {
            try
            {
                lv.Items[lv.Items.Count - 1].EnsureVisible();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            
        }
        public static void DisplayData(ListView lv, WrapperExcel excel)
        {
            lv.Clear();

            lv.View = View.Details;

            int nColLength = excel.COL;

            lv.BeginUpdate();

            for (int cols = 0; cols <= nColLength; cols++)
            {
                lv.Columns.Add((cols.ToString()));
            }

            for (int rows = 0; rows < excel.ROW; rows++)
            {
                ListViewItem lvi = new ListViewItem();

                lvi.Text = rows.ToString("0000");
                for (int cols = 0; cols < nColLength; cols++)
                {
                    lvi.SubItems.Add(excel.GetCol(rows, cols));
                }
                lv.Items.Add(lvi);
            }
            lv.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);

            lv.EndUpdate();
        }
        public static void DisplayData(ListView lv, string [] ListHeader, WrapperExcel excel)
        {
            lv.Clear();

            lv.View = View.Details;

            int nColLength = excel.COL;

            lv.BeginUpdate();

            for (int cols = 0; cols < ListHeader.Count(); cols++)
            {
                lv.Columns.Add((ListHeader.ElementAt(cols)));

            }

            for (int rows = 0; rows < excel.ROW; rows++)
            {
                ListViewItem lvi = new ListViewItem();

                lvi.Text = rows.ToString("0000");
                for (int cols = 0; cols < nColLength; cols++)
                {
                    lvi.SubItems.Add(excel.GetCol(rows, cols));
                }
                lv.Items.Add(lvi);
            }

            lv.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);

            lv.EndUpdate();
        }
        public static void DisplayData(ListView lv, string[] ListHeader, int [] listWidth, WrapperExcel excel)
        {
            int nColLength = excel.COL;
            if (nColLength > 0)
            {
                lv.Invoke(new MethodInvoker(
                 delegate()
                 {
                     lv.Clear();
                     lv.View = View.Details;

                     lv.BeginUpdate();

                     for (int cols = 0; cols < ListHeader.Count(); cols++)
                     {
                         lv.Columns.Add((ListHeader.ElementAt(cols)));
                         lv.Columns[cols].Width = listWidth[cols];
                     }


                     for (int rows = 0; rows < excel.ROW; rows++)
                     {
                         ListViewItem lvi = new ListViewItem();

                         lvi.Text = rows.ToString("0000");

                         for (int cols = 0; cols < nColLength; cols++)
                         {
                             lvi.SubItems.Add(excel.GetCol(rows, cols));
                         }

                         lv.Items.Add(lvi);
                     }

                     //lv.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);


                     lv.EndUpdate();
                 }));
            }
            
        }
     
        public static void DisplayData(ListView lv, string[] ListHeader, int [] width,  WrapperExcel excel, string filter)
        {
            lv.Clear();

            lv.View = View.Details;

            int nColLength = excel.COL;

            lv.BeginUpdate();

            for (int cols = 0; cols < ListHeader.Count(); cols++)
            {
                lv.Columns.Add((ListHeader.ElementAt(cols)));
                lv.Columns[cols].Width = width[cols];
            }

            for (int rows = 0; rows < excel.ROW; rows++)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = rows.ToString("0000");

                bool bFiltered = false;

                for (int cols = 0; cols < nColLength; cols++)
                {
                    string temp = excel.GetCol(rows, cols);

                    if (temp == filter) { bFiltered = true;  }
                    lvi.SubItems.Add( temp );
                }
                if( bFiltered == true ) lv.Items.Add(lvi);
            }
            lv.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);

            lv.EndUpdate();
        }

        /// <summary>
        /// 리스트뷰에 모든 데이터를 획득한다. string[] 형태로 획득한다.
        /// return :: List<string[]> 
        /// </summary>
        /// <param name="lv"></param>
        /// <returns></returns>
        public static List<string[]> GetAllData(ListView lv)
        {
            List<string[]> data = new List<string[]>();

            int nRowCount = lv.Items.Count;

            
            if (nRowCount > 0) // 뭔가 들어있으면
            {
                //  항목이 몇개냐

                int nColCount = lv.Items[0].SubItems.Count;

                for( int row = 0; row < nRowCount; row++)
                {
                    string[] item = new string[nColCount];

                    for( int col = 0; col < nColCount; col++)
                    {
                        item[col] = lv.Items[row].SubItems[col].Text;
                    }
                    data.Add(item);
                }
            }

            return data;
        }
        public static List<int> GetSelectedIndexList(ListView lv)
        {
            List<int> selectedList = new List<int>();

            int nTotal = lv.Items.Count;

                for (int i = 0; i < nTotal; i++)
                {
                    if (lv.Items[i].Selected == true)
                    {
                        selectedList.Add(i);
                    }
                }

            return selectedList;
        }
        /// <summary>
        /// 모든 체크박스의 선택을 입력값을 통해 통일하여 선택한다.
        /// </summary>
        /// <param name="lv"></param>
        /// <param name="bStatus"></param>
        public static void SetCheckAll(ListView lv, bool bStatus)
        {
            foreach (ListViewItem item in lv.Items)
            {
                item.Checked = bStatus;
            }
        }
        /// <summary>
        /// 체크박스가 선택된 Row 의 카운트를 받는다.
        /// </summary>
        /// <param name="lv"></param>
        /// <returns></returns>
        public static int CountCheckRows(ListView lv)
        {
            int nCount = 0;
            foreach (ListViewItem item in lv.Items)
            {
                if (item.Checked == true) nCount++;
            }
            return nCount; ;
        }
        /// <summary>
        /// Listview에서 checked rows의 인덱스를 리스트로  list<int>로 가져온다
        /// </summary>
        /// <param name="lv"></param>
        /// <returns></returns>
        public static List<int> GetCheckedRowIndexList(ListView lv)
        {
            List<int> list = new List<int>();

            for (int i = 0; i < lv.Items.Count; i++  )
            {
                ListViewItem item = lv.Items[i];
                if( item.Checked == true )
                {
                    list.Add(Convert.ToInt32(item.Text));
                }
            }
            return list;
        }
        public static List<string[]> GetCheckedRowList(ListView lv)
        {
            List<string[]> list = new List<string[]>();

            for (int i = 0; i < lv.Items.Count; i++)
            {
                ListViewItem item = lv.Items[i];
                if (item.Checked == true)
                {
                    int nCols = lv.Items[0].SubItems.Count;
                                            
                    List<String> oneItem = new List<string>();
                    for (int j = 1; j < nCols; j++)
                    {
                        oneItem.Add(lv.Items[i].SubItems[j].Text);
                    }
                    string[] arr = oneItem.ToArray();
                    list.Add(arr);
                }
            }
            return list;
        }
        public static List<string[]>GetSelectedItemList(ListView lv)
        {
            List<int> indexList = GetSelectedIndexList(lv);
            
            List<string[]> allData = WrapperLV.GetAllData(lv);
            List<string[]> itemList = new List<string[]>();

            int nItemCount = lv.Items.Count;


            int nCols = lv.Items[0].SubItems.Count;
            for(int i = 0; i < indexList.Count; i++)
            {
                int itemIndex = indexList.ElementAt(i);
                List<String> oneItem = new List<string>();
                for (int j = 0; j < nCols; j++)
                {
                    oneItem.Add(lv.Items[itemIndex].SubItems[j].Text);
                }
                string[] arr = oneItem.ToArray();
                itemList.Add(arr);
            }

            return itemList;
        }
         
        /// <summary>
        /// 선택된 column에 대해서 정렬을 한다. 정렬방법은 내림/오름 자동 순환 선택.
        /// </summary>
        /// <param name="lv"></param>
        /// <param name="nIndex"></param>
        public static void SortData(ListView lv,  int nIndex)
        {
            try
            {
                lv.BeginUpdate();
                if (lv.Sorting == SortOrder.Ascending || lv.Sorting == SortOrder.None)
                {
                    lv.ListViewItemSorter = new LV_Comparer(nIndex, "desc");
                    lv.Sorting = SortOrder.Descending;
                    lv.Columns[nIndex].Text = lv.Columns[nIndex].Text;
                }
                else
                {
                    lv.ListViewItemSorter = new LV_Comparer(nIndex, "asc");
                    lv.Sorting = SortOrder.Ascending;
                    lv.Columns[nIndex].Text = lv.Columns[nIndex].Text;
                }

                lv.Sort();
                lv.EndUpdate();
                lv.Refresh();
            }
            catch(Exception ex)
            { 
                //Console.WriteLine(ex.ToString()); 
                MessageBox.Show(ex.ToString());
            }

        }
        public class LV_Comparer : IComparer
        {
            private int col;
            public string sort = "asc";
            public LV_Comparer()
            {
                col = 0;
            }

            public LV_Comparer(int column, string sort)
            {
                col = column;
                this.sort = sort;
            }

            public int Compare(object x, object y)
            {
                int nRes = 0;

                double fResX = 0;
                double fResY = 0;
                bool bResX = false;
                bool bResY = false;

                string strX = ((ListViewItem)x).SubItems[col].Text;
                string strY = ((ListViewItem)y).SubItems[col].Text;

                bResX = double.TryParse( strX, out fResX);
                bResY = double.TryParse( strY, out fResY);

                if( bResX == true && bResY == true)  // 숫자면
                {
                    if( sort == "asc")
                    {
                        if (fResX > fResY)
                            nRes = 1;
                        else nRes = -1;
                    }
                    else
                    {
                        if (fResX < fResY) nRes = 1;
                        else nRes = -1;
                    }
                }
                else // 문자면 
                {
                    try
                    {
                        if (sort == "asc")
                        {
                            nRes = String.Compare( strX, strY);
                        }
                        else
                        {
                            nRes = String.Compare(strY, strX);
                        }
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }
                    

                

                return nRes;
            }
        }
    }
}

