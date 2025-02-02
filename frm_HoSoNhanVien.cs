using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections;
using COFFEE_SHOP.ObjectLayer;
using COFFEE_SHOP.BussinessLayer;
//using System.Data.SqlClient.SqlDataReader;

namespace COFFEE_SHOP
{
    public partial class frm_HoSoNhanVien : Form 
    {

        BL_NhanVien bl_nhanvien = new BL_NhanVien();   
        public frm_HoSoNhanVien()
        {
            InitializeComponent();
        }
        //bien chung

        public static SqlDataAdapter da;
        public static SqlDataReader dr;
        public static SqlCommand cmd;
        public static SqlCommandBuilder cmdb;
        public static DataSet ds;
        public static DataTable dt;
        public static string sql;
        ArrayList lstChucvu = new ArrayList();
         //public static  SqlDataReader rd;

        public void Fill_DataSet()
        {
            Connect_DB.Ketnoi_CSDL();
            da = new SqlDataAdapter(sql, Connect_DB.ketnoi);
            ds = new DataSet();
            da.Fill(ds);
            da.Dispose();
            Connect_DB.ketnoi.Close();

        }
       
        
        private void frm_HoSoNhanVien_Load(object sender, EventArgs e)
        {

            //=======================
            Form frm = new frm_HoSoNhanVien();
           
            
            
            this.AcceptButton = btn_KT;
            this.AcceptButton = btn_KT1;
            txt_ID.ShortcutsEnabled = false;
            txt_ID1.ShortcutsEnabled = false;
            Hide_Control();
            Hide_Control1();
            Load_NewChucVu();
            Load_ChucVu();
            ////
            dgrid_DSNV.DataSource = bl_nhanvien.sp_select_NhanVienALL().Tables[0];
            dgrid_DSNV.Columns["SoCMND"].HeaderText = "Số CMND";
            dgrid_DSNV.Columns["SoCMND"].Width = 100;
            dgrid_DSNV.Columns["TenNhanVien"].HeaderText = "Tên Nhân Viên";
            dgrid_DSNV.Columns["TenNhanVien"].Width = 160;
            dgrid_DSNV.Columns["TenChucVu"].HeaderText = "Tên Chức Vụ";
            dgrid_DSNV.Columns["TenChucVu"].Width = 100;
            dgrid_DSNV.AllowUserToAddRows = false;

            
        }
        //Load Chức Vụ lên Combobox

        // mới
        public void Load_NewChucVu()
        {
            this.cmb_NewChucVu.SelectedIndexChanged -= new EventHandler(this.cmb_NewChucVu_SelectedIndexChanged);
            
            cmb_NewChucVu.DataSource = bl_nhanvien.sp_select_ChucVu().Tables[0];
            cmb_NewChucVu.DisplayMember = "TenChucVu";
            cmb_NewChucVu.ValueMember = "MaChucVu";
            cmb_NewChucVu.SelectedIndex = -1;
            this.cmb_NewChucVu.SelectedIndexChanged += new EventHandler(this.cmb_NewChucVu_SelectedIndexChanged);
        }
        public static int MaCVNew;
        private void cmb_NewChucVu_SelectedIndexChanged(object sender, EventArgs e)
        {


            if (cmb_NewChucVu.SelectedIndex != -1)
            {
                MaCVNew = Convert.ToInt16(cmb_NewChucVu.SelectedValue.ToString());
            }
        }

        //Cũ
        public void Load_ChucVu()
        {
            this.cmb_ChucVu.SelectedIndexChanged -= new EventHandler(this.cmb_ChucVu_SelectedIndexChanged);
            
            cmb_ChucVu.DataSource =bl_nhanvien.sp_select_ChucVu().Tables[0];
            cmb_ChucVu.DisplayMember = "TenChucVu";
            cmb_ChucVu.ValueMember = "MaChucVu";
            cmb_ChucVu.SelectedIndex = -1;
            this.cmb_ChucVu.SelectedIndexChanged += new EventHandler(this.cmb_ChucVu_SelectedIndexChanged);
        }
        public static int MaCV;
        
        
        //đóng cu
        private void btn_Dong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //đóng mới
        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //kiểm tra mới
        private void btn_KT1_Click(object sender, EventArgs e)
        {
         
        
            if(txt_ID1.Text == "") 
            {
                MessageBox.Show("Bạn phải nhập Số CMND ", "Thông báo");
                txt_ID1.Focus();
                return;
            }
            else
            {
                      if (txt_ID1.Text.Length != 9)
                        {
                            MessageBox.Show("Số CMND phải có 9 số", "Thông báo");
                            txt_ID.Focus();
                            return;
                        }
                        else
                        {
                            
                            if (bl_nhanvien.sp_select_CMND(txt_ID1.Text.Trim()).Tables[0].Rows.Count>0)
                            {
                                MessageBox.Show("Số CMND này đã lưu. Muốn thay đổi thông tin hãy chọn chức năng Xem Hồ Sơ Cá nhân để chỉnh sửa");
                                txt_ID1.Text = "";
                                txt_ID1.Focus();
                                return;
                            }
                            else
                            {
                                UserName = txt_ID1.Text;
                                MessageBox.Show("Số CMND này hợp lệ");
                            }
                        }
                  
                    
                     

            }
            lbl_NewCMND.Text = txt_ID1.Text;
            Hien_Control1();
            // load Chức vụ 
            //******************************************************
            
        }
        //Kiểm tra cũ
        
       
        //kiểm tra số CMND
       

        public void Hien_Control()
        {
            txt_Name.Enabled = true;
            dtpic_Ngaysinh.Enabled = true;
            lbl_CMND.Enabled = true;
            rdb_Nam.Enabled = true;
            rdb_Nu.Enabled = true;
            txt_Address.Enabled = true;
            txt_Phone.Enabled = true;
            cmb_ChucVu.Enabled = true;
            chk_TToan.Enabled = true;
            lbl_Money.Enabled = true;
            lbl_Date.Enabled = true;
            btn_Luu.Enabled = true;
            btn_Xoa.Enabled = true;
        }
        public void Hien_Control1()
        {
            lbl_NewCMND.Enabled = true;
            txt_NewName.Enabled = true;
            dtpic_NewDate.Enabled = true;
            rdb_NewGril.Enabled = true;
            rdb_NewBoy.Enabled = true;
            txt_NewAddress.Enabled = true;
            txt_NewFone.Enabled = true;
            cmb_NewChucVu.Enabled = true;
            dtpic_NewIn.Enabled = true;
            btn_LuuMoi.Enabled = true;
            btn_Huy.Enabled = true;
        }

        public void Hide_Control()
        {
            txt_Name.Enabled = false;
            dtpic_Ngaysinh.Enabled = false;
            lbl_CMND.Enabled = false;
            rdb_Nam.Enabled = false;
            rdb_Nu.Enabled = false;
            txt_Address.Enabled = false;
            txt_Phone.Enabled = false;
            cmb_ChucVu.Enabled = false;
            chk_TToan.Enabled = false;
            lbl_Money.Enabled = false;
            lbl_Date.Enabled = false;
            btn_Luu.Enabled = false;
            btn_Xoa.Enabled = false;
        }
        public void Hide_Control1()
        {
            lbl_NewCMND.Enabled = false;
            txt_NewName.Enabled = false;
            dtpic_NewDate.Enabled = false;
            rdb_NewGril.Enabled = false;
            rdb_NewBoy.Enabled = false;
            txt_NewAddress.Enabled = false;
            txt_NewFone.Enabled = false;
            cmb_NewChucVu.Enabled = false;
            dtpic_NewIn.Enabled = false;
            btn_LuuMoi.Enabled = false;
            btn_Huy.Enabled = false;
        }
       
        public void settextboxnull()
        {
            //txt_NewCMND.Text = "";
            txt_NewName.Text = "";
            dtpic_NewDate.Text = Convert.ToDateTime(DateTime.Now).ToShortDateString();
            dtpic_NewIn.Text = Convert.ToDateTime(DateTime.Now).ToShortDateString();
            rdb_NewGril.Checked = false;
            rdb_NewBoy.Checked = false;
            txt_NewAddress.Text = "";
            txt_NewFone.Text = "";
            cmb_NewChucVu.Text = "";
           
            //cmb_NewChucVu.Enabled = false;
            //dtpic_NewIn.Enabled = false;
        }
        public void settextnull_tt()
        {
            txt_Address.Text = "";
            lbl_CMND.Text = "";
            txt_Name.Text = "";
            txt_Phone.Text = "";
            rdb_Nam.Checked = false ;
            rdb_Nu.Checked = false;
            cmb_ChucVu.Text = "";
            lbl_Date.Text = "";
            lbl_Money.Text = "";
            dtpic_Ngaysinh.Text = Convert.ToDateTime(DateTime.Now).ToShortDateString();
        }
        
        private void btn_Huy_Click(object sender, EventArgs e)
        {
            settextboxnull();
        }

        public static string UserName;
        public static int User_MCV;
        // Lưu Thông tin Nhân viên mới
        private void btn_LuuMoi_Click(object sender, EventArgs e)
        {
            tbl_HoSoNhanVien tbl = new tbl_HoSoNhanVien();

            tbl.SoCMND = lbl_NewCMND.Text.Trim();
            //MessageBox.Show("So CMND moi : " + tbl.SoCMND);
            if (txt_NewName.Text == "")
            {
                MessageBox.Show("Nhập Họ Tên ", "Thông báo");
                txt_Name.Focus();
                return;
            }
            else
            {
                tbl.TenNhanVien = txt_NewName.Text.ToUpper();
            }
            tbl.NgaySinh= dtpic_NewDate.Value;
            if ((!rdb_NewBoy.Checked) && (!rdb_NewGril.Checked))
            {
                MessageBox.Show("Chọn giới tính", "Thông báo");
                rdb_NewGril.Focus();
                return;
            }
            else
            {
                if (rdb_NewBoy.Checked)
                {
                    tbl.GioiTinh = true;
                }
                else
                {
                    tbl.GioiTinh = false;
                }
            }
            if (txt_NewAddress.Text == "") 
            {
                MessageBox.Show("Nhập Địa Chỉ", "Thông báo");
                txt_NewAddress.Focus();
                return;
            }
            else
            {
                tbl.DiaChi = txt_NewAddress.Text.ToUpper();
                
            }
            
            if (txt_NewFone.Text == "")
            {
                MessageBox.Show("Nhập số diện thoại", "Thông báo");
                txt_NewFone.Focus();
                return;
            }
            else
            {
                
                        tbl.SoDienThoai= txt_NewFone.Text;
                    
                
            }
            //Ch"ức vụ 
            if  (cmb_NewChucVu.SelectedIndex == -1)
            {
                MessageBox.Show("Chon Chức vụ ", "Thông Báo");
                cmb_NewChucVu.Focus();
                return;
            }
            else
            {      
                tbl.MaChucVu= MaCVNew;
                User_MCV = MaCVNew;
               
            }
           tbl.DaThanhToan=false;
           tbl.TienLuongThangTruoc = 0;

            tbl.NgayVaoLam = dtpic_NewIn.Value;
            //insert 
            //tbl.SoCMND = "201537486";
            //tbl.TenNhanVien ="Oanh";
            //tbl.GioiTinh = true;
            //tbl.DiaChi = txt_NewAddress.Text.ToUpper();
            //tbl.SoDienThoai = txt_NewFone.Text;
            //tbl.MaChucVu = MaCVNew;
            //tbl.DaThanhToan = false;
            //tbl.TienLuongThangTruoc = 0;

            //
           // MessageBox.Show("SO CMND MOI INSERT :" + tbl.SoCMND);
            bl_nhanvien.sp_insert_NhanVien(tbl);


             
            
            // tao tai khoan 

            tbl_Account tbl_ac = new tbl_Account();
            tbl_ac.User = UserName;
            tbl_ac.Pass = "123456";
            tbl_ac.MaCV = User_MCV;
            tbl_ac.TT = false;
            bl_nhanvien.sp_insert_Account(tbl_ac); 
            MessageBox.Show("Thêm thành công");
            cmb_NewChucVu.SelectedIndex = -1;
            lbl_NewCMND.Text = "";
            txt_ID1.Text = "";
            settextboxnull();
            Hide_Control1();
            Load_DSNV();
           
        }

        //load Danh Sách Nhân Viên SOOCMND, Tên , Chức Vụ
        public void Load_DSNV()
        {
            dgrid_DSNV.DataSource = bl_nhanvien.sp_select_NhanVienALL().Tables[0];
           
        }

        private void btn_Xem_Click(object sender, EventArgs e)
        {

            Load_DSNV();
        }
        //Kiểm tra Nhân Viên đã có
        private void btn_KT_Click(object sender, EventArgs e)
        {
            if (txt_ID.Text == "")
            {
                MessageBox.Show("Bạn phải nhập Số CMND ", "Thông báo");
                txt_ID.Focus();
                return;
            }
            else
            {
                    if (txt_ID.Text.Length != 9)
                    {
                        MessageBox.Show("Số CMND phải có 9 số", "Thông báo");
                        txt_ID.Focus();
                        return;
                    }
                    else
                        if (bl_nhanvien.sp_select_CMND(ID_KT).Tables[0].Rows.Count > 0)
                    {
                       // MessageBox.Show("Số CMND này hợp lệ");
                      //  MessageBox.Show(txt_ID.Text);
                        Hien_Control();

                        DataTable dt2 = bl_nhanvien.sp_select_HoSoNhanVien(Convert.ToString(ID_KT)).Tables[0];

                        if (dt2.Rows.Count>0)
                        {
                            lbl_CMND.Text = dt2.Rows[0][1].ToString();
                            txt_Name.Text = dt2.Rows[0][2].ToString();
                           // dtpic_Ngaysinh.Text = Convert.ToDateTime(dt.Rows[0][4].ToString());

                            dtpic_Ngaysinh.CustomFormat = "dd/mm/yyyy";
                            string ns = dt2.Rows[0][4].ToString();
                            dtpic_Ngaysinh.Text = Convert.ToDateTime(ns).ToShortDateString();

                            txt_Address.Text = dt2.Rows[0][5].ToString();
                            txt_Phone.Text = dt2.Rows[0][6].ToString();

                            lbl_Date.Text = dt2.Rows[0][9].ToString();
                            if (dt2.Rows[0][10].ToString() == "True")
                            {
                                rdb_Nam.Focus();
                            }
                            else
                                rdb_Nu.Focus() ;

                            lbl_Money.Text = dt2.Rows[0][8].ToString();

                            if (dt2.Rows[0][7].ToString() == "True")
                            {
                                chk_TToan.Checked = true; 
                            }
                            else
                            {
                                chk_TToan.Checked = false;
                            }

                        }
                       
                        //load Chưc vỤ
                       
                        DataTable dt1 = bl_nhanvien.sp_select_TenChucVu(Convert.ToString(txt_ID.Text.Trim())).Tables[0];
                        if (dt1.Rows.Count>0)
                        {
                            cmb_ChucVu.Text = dt1.Rows[0][2].ToString();

                        }


                        
                        Hien_Control();
                        
                    }
                    else
                    {
                        MessageBox.Show("count : " + bl_nhanvien.sp_select_CMND(txt_ID.Text).Tables[0].Rows.Count+ " so CMND :" + txt_ID.Text);
                        MessageBox.Show("Số CMND chưa có, nếu muốn tạo Hồ Sơ Mới thì chọn chức năng tạo mới");
                        txt_ID.Text = "";
                        txt_ID.Focus();
                    }
                


            }
                

       }

            //Load_NhanVien();

        private void btn_Luu_Click(object sender, EventArgs e)
        {
           
           // DataSet ds = bl_nhanvien.sp_select_HoSoNhanVien(txt_ID.Text.Trim());

            tbl_HoSoNhanVien tbl_hsnv = new tbl_HoSoNhanVien();
            if (txt_Name.Text != "")
            {
                tbl_hsnv.TenNhanVien = txt_Name.Text.ToUpper();
               
                //da.Update(ds);
            }
            else
            {
                MessageBox.Show("Nhập Họ tên Vào !", "Thông Báo");
                txt_Name.Focus();
                return;
            }
            tbl_hsnv.NgaySinh = dtpic_Ngaysinh.Value;
            // da.Update(ds);
            if ((!rdb_Nam.Checked) && (!rdb_Nu.Checked))
            {
                MessageBox.Show("Chọn giới tính", "Thông báo");
                rdb_Nu.Focus();
                return;
            }
            else
            {
                if (rdb_Nam.Checked)
                {
                   tbl_hsnv.GioiTinh =true;
                    //da.Update(ds);
                }
                else
                {
                    tbl_hsnv.GioiTinh = false;
                    //  da.Update(ds);
                }
            }

            if (txt_Address.Text != "")
            {
                tbl_hsnv.DiaChi = txt_Address.Text.ToUpper();
                //da.Update(ds);
            }
            else
            {
                MessageBox.Show("Nhập Địa Chỉ !", "Thông Báo");
                txt_Name.Focus();
                return;
            }

            if (txt_Phone.Text != "")
            {
               tbl_hsnv.SoDienThoai = txt_Phone.Text.ToUpper();
                // da.Update(ds);
            }
            else
            {
                MessageBox.Show("Nhập Số Đn Thoại", "Thông Báo");
                txt_Name.Focus();
                return;
            }
            if (cmb_ChucVu.SelectedIndex == -1)
            {
                MessageBox.Show("Chon Chức vụ ", "Thông Báo");
                cmb_ChucVu.Focus();
                return;
            }
            else
            {
                tbl_hsnv.MaChucVu= MaCV;
                

            }
            if (chk_TToan.Checked == true)
            {
                tbl_hsnv.DaThanhToan= true;
               
            }
            else
            {
                tbl_hsnv.DaThanhToan = false;
                
            }
            tbl_hsnv.SoCMND = txt_ID.Text.Trim();
           //update
            MessageBox.Show(MaCV.ToString());
            bl_nhanvien.sp_update_HoSoNhanVien(tbl_hsnv);

            MessageBox.Show("Sửa thành công");
            cmb_ChucVu.SelectedIndex = -1;

            txt_ID.Text = "";
            settextnull_tt();
            Hide_Control();
            Load_DSNV();
     
           
    }
        public static string CMND;
        private void btn_Xoa_Click(object sender, EventArgs e)
        {
          
           CMND = txt_ID.Text.ToString();            
            bl_nhanvien.sp_delete_HoSoCaNhan(CMND); 

            // xóa trong tbl_Tài Khoản
            //=======================
            bl_nhanvien.sp_delete_Account(CMND.Trim());
            MessageBox.Show("Xóa thành công ");
            cmb_ChucVu.SelectedIndex = -1;
            settextnull_tt();
            txt_ID.Text = "";
            Hide_Control();
            Load_DSNV();
        }

        private void txt_ID1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txt_ID_KeyPress(object sender, KeyPressEventArgs e)
        {
        //    if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
        //        e.Handled = true;
        }

        private void txt_Phone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txt_NewFone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txt_CMND_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void btn_ref_Click(object sender, EventArgs e)
        {
            Load_DSNV();

        }
        public static string ID_KT;

        private void dgrid_DSNV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
            ID_KT = dgrid_DSNV.CurrentRow.Cells["SoCMND"].Value.ToString().Trim();
            txt_ID.Text = ID_KT;

            if (txt_ID.Text == "")
            {
                MessageBox.Show("Bạn phải nhập Số CMND ", "Thông báo");
                txt_ID.Focus();
                return;
            }
            else
            {
                if (txt_ID.Text.Length != 9)
                {
                    MessageBox.Show("Số CMND phải có 9 số", "Thông báo");
                    txt_ID.Focus();
                    return;
                }
                else
                    if (bl_nhanvien.sp_select_CMND(ID_KT).Tables[0].Rows.Count > 0)
                    {
                        // MessageBox.Show("Số CMND này hợp lệ");
                        //  MessageBox.Show(txt_ID.Text);
                        Hien_Control();

                        DataTable dt2 = bl_nhanvien.sp_select_HoSoNhanVien(Convert.ToString(ID_KT)).Tables[0];

                        if (dt2.Rows.Count > 0)
                        {
                            lbl_CMND.Text = dt2.Rows[0][1].ToString();
                            txt_Name.Text = dt2.Rows[0][2].ToString();
                            // dtpic_Ngaysinh.Text = Convert.ToDateTime(dt.Rows[0][4].ToString());

                            dtpic_Ngaysinh.CustomFormat = "dd/mm/yyyy";
                            string ns = dt2.Rows[0][4].ToString();
                            dtpic_Ngaysinh.Text = Convert.ToDateTime(ns).ToShortDateString();

                            txt_Address.Text = dt2.Rows[0][5].ToString();
                            txt_Phone.Text = dt2.Rows[0][6].ToString();

                            lbl_Date.Text = dt2.Rows[0][9].ToString();
                            if (dt2.Rows[0][10].ToString() == "True")
                            {
                                rdb_Nam.Focus();
                            }
                            else
                                rdb_Nu.Focus();

                            lbl_Money.Text = dt2.Rows[0][8].ToString();

                            if (dt2.Rows[0][7].ToString() == "True")
                            {
                                chk_TToan.Checked = true;
                            }
                            else
                            {
                                chk_TToan.Checked = false;
                            }

                        }

                        //load Chưc vỤ

                        DataTable dt_cv = bl_nhanvien.sp_select_TenChucVu(ID_KT).Tables[0];
                       // MessageBox.Show(dt_cv.Rows.Count.ToString());
                        if (dt_cv.Rows.Count > 0)
                        {
                           cmb_ChucVu.Text = dt_cv.Rows[0][2].ToString();

                        }
                          Hien_Control();

                    }
                    else
                    {
                     //   MessageBox.Show("count : " + bl_nhanvien.sp_select_CMND(txt_ID.Text).Tables[0].Rows.Count + " so CMND :" + txt_ID.Text);
                        MessageBox.Show("Số CMND chưa có, nếu muốn tạo Hồ Sơ Mới thì chọn chức năng tạo mới");
                        txt_ID.Text = "";
                        txt_ID.Focus();
                    }



            }
            
        }

        private void rdb_Nam_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rdb_Nu_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void cmb_ChucVu_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cmb_ChucVu.SelectedIndex != -1)
            {
               // MessageBox.Show(cmb_ChucVu.SelectedValue.ToString());
                MaCV = Convert.ToInt16(cmb_ChucVu.SelectedValue.ToString());
            }
        
        }

       
      
       
       
        
        

        
       

       

     

       
            
        //}

       

        
        



        

        //------------

        
        //----------------
       
    }
}