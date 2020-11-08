using System;
using System.Collections.Generic;
using System.Text;

namespace Definitions.Static_Text
{
    public static class Static_Text
    {
        public static class RedirectURL
        {
            public const string redirectToProductList = "/Backoffice/Product/Product-list.aspx";
        }

        public static class ICON_URL
        {
            public const string PDF_URL = "https://aladinxcite.blob.core.windows.net/sw-claim-images/unnamed.png";
        }

        public static class TEXT_WARNING
        {
            public const string MSG_DUPLICATED_EMAIL = "อีเมล์ซ้ำ";
            public const string MSG_FILE_NOT_ALLOWED = "เฉพาไฟล์ .xlsx, .xls หรือ .xlsb เท่านั้น";

            public const string MSG_PLEASE_CHOOSE_FILE = "เลือกอย่างน้อย 1 ไฟล์";
            public const string MSG_PLEASE_SELECT_SYMPTOM = "เลือกอย่างน้อย 1 อาการ";
            public const string MSG_MAXIMUM_FILE = "อัพโหลดได้มากสุด 11 ไฟล์ต่อ 1 สินค้า";
            public const string MSG_MAXIMUM_PROMOTION_FILE = "อัพโหลดได้มากสุด 10 ไฟล์";
            public const string MSG_NO_ITEM_ADD = "กรุณาเพิ่มรายการ";
            public const string MSG_DUPLICATED_PART_NO = "serial: ";
            public const string MSG_DUPLICATED_PART_NO_2 = "serial ซ้ำ, Coverage Code ซ้ำ <br>";
            public const string MSG_PLEASE_CHECK_FILE = " กรุณาตรวจสอบเอกสารใหม่";
            public const string MSG_EMPTY_BRANCH_CODE = "คอลัมน์ Branch(Code) เป็นค่าว่าง";
            public const string MSG_EMPTY_BRAND = "คอลัมน์ Brand เป็นค่าว่าง";
            public const string MSG_EMPTY_CATEGORY = "คอลัมน์ Category เป็นค่าว่าง";
            public const string MSG_EMPTY_SERIAL = "คอลัมน์ Serial เป็นค่าว่าง";
            public const string MSG_EMPTY_BILL_NO = "คอลัมน์ Bill No เป็นค่าว่าง";
            public const string MSG_NOTFOUND_BRANCH = "ไม่พบสาขา: ";
            public const string MSG_NOTFOUND_BRAND = "ไม่พบ Brand: ";
            public const string MSG_NOTFOUND_CATEGORY = "ไม่พบ: Category: ";
            public const string MSG_NOTFOUND_WARRANTY = "ไม่พบ: Warranty Code: ";

            public const string MSG_MAXIMUM_CHARACTER_BILL_NO_20 = "เลขที่ Bill No เกิน 20 ตัวอักษร";

            public const string MSG_NOT_FOUND_USER = "ไม่พบชื่อผู้ใช้ในระบบ";
            public const string MSG_INVALID_USER = "ชื่อผู้ใช้ไม่ถูกต้อง";
            public const string MSG_INVALID_PASSWORD = "รหัสผ่านไม่ถูกต้อง";

            public const string MSG_EMPTY_INQUIRY = "คอลัมน์ Inquiry เป็นค่าว่าง";
            public const string MSG_EMPTY_IMEI = "คอลัมน์ IMEI เป็นค่าว่าง";
            public const string MSG_EMPTY_INQUIRY_CLAIM_DATE = "คอลัมน์ Inquiry Claim Date เป็นค่าว่าง";
            public const string MSG_EMPTY_INVALID_FORMAT_DATE = "ฟอร์แมทวันที่ไม่ถูกต้อง";
            public const string MSG_EMPTY_INVALID_CLAIM_DATE = "มีวันที่แจ้งเคลมก่อน วันที่สร้างประกัน";
        }

        public static class MESSAGE_TRANSACTION
        {
            public const string MSG_SUCCESS_TRANSACTION = "ทำรายการสำเร็จ";
            public const string MSG_FAIL_TRANSACTION = "ทำรายการไม่สำเร็จ";
            public const string MSG_FAIL_TRANSACTION_OR_DUPLICATE = "ทำรายการไม่สำเร็จ หรือข้อมูลซ้ำ";
        }

        public static class DEFAULT_VALUE
        {
            public const float DEFAULT_PRICE = 0;
            public const string DEFAULT_REPLACE_STRING_EMPTY = "-";

            public const int DEFAULT_BRANCH_FOR_ADMIN = 1;
        }
    }
}
