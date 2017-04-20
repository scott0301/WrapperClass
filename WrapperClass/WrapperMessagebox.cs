using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Wrapper
{
    public static class WrapperMessagebox
    {
        public static void MessageboxSelectionErr()
        {
            MessageBox.Show("작업 진행에 필요한 테이터를 선택하여 주세요.");
        }

        public static bool MessageBoxInvalidFileType(string s)
        {
            DialogResult res = MessageBox.Show("파일의 형식이 올바르지 않습니다.\n\n"+ s, "에러", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (res == DialogResult.Yes)
                return true;
            else return false;
        }
        public static bool MessageboxExit()
        {
            DialogResult res = MessageBox.Show("프로그램을 종료하시겠습니까?", "프로그램 종료", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (res == DialogResult.Yes)
                return true;
            else return false;
        }
        public static bool MessageboxModify()
        {
           DialogResult res =  MessageBox.Show("데이터를 수정하시겠습니까?", "데이터 수정", MessageBoxButtons.YesNo, MessageBoxIcon.Question) ;

            if( res == DialogResult.Yes )
                 return true;
            else return false;
        }
        public static bool MessageboxLoad()
        {
            DialogResult res = MessageBox.Show("데이터를 로드하시겠습니까?", "데이터 수정", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (res == DialogResult.Yes)
                return true;
            else return false;
        }
        public static bool MessageboxApply()
        {
            DialogResult res = MessageBox.Show("결과를 적용하시겠습니까?", "데이터 적용", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (res == DialogResult.Yes)
                return true;
            else return false;
        }
 
        public static bool MessageboxDelete()
        {
            DialogResult res = MessageBox.Show("데이터를 삭제 하시겠습니까?", "데이터 삭제", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (res == DialogResult.Yes)
                return true;
            else return false;
        }

   
        public static bool MessageboxSave()
        {
            DialogResult res = MessageBox.Show("데이터를 저장 하시겠습니까?", "데이터 저장", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (res == DialogResult.Yes)
                return true;
            else return false;
        }

        public static bool MessageboxInputError()
        {
            DialogResult res = MessageBox.Show("입력값이 잘못 되었습니다. 입력 데이터를 확인해 주세요.", "입력에러", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            if (res == DialogResult.Yes)
                return true;
            else return false;
        }
        public static bool MessageboxNotyet()
        {
            DialogResult res = MessageBox.Show("아직 지원하지 않는 기능입니다.", "알림", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (res == DialogResult.Yes)
                return true;
            else return false;
        }
        public static bool MessageboxAdd()
        {
            DialogResult res = MessageBox.Show("데이터를 추가하시겠습니까?", "데이터 추가", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (res == DialogResult.Yes)
                return true;
            else return false;
        } 
          
        public static bool MessageboxDump()
        {
            DialogResult res = MessageBox.Show("엑셀 파일로 배출하겠습니까?", "진행 취소", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (res == DialogResult.Yes)
                return true;
            else return false;
        }
        public static void MessageBoxNetError()
        {
            MessageBox.Show("네트워크에 이상이 있는 것 같습니다.\n 네트워크 상태를 확인하여 주십시오.", "네트워크 이상", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
