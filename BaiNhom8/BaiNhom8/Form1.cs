using BaiNhom8.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaiNhom8
{
    public partial class Form1 : Form
    {
        Model1 db = new Model1();
        public Form1()
        {
            InitializeComponent();
        }


        private void btn_Them_Click(object sender, EventArgs e)
        {
            var find = db.NhanViens.FirstOrDefault(s => (string)(s.MaNV) == tb_ma.Text);
            //NhanVien nv = new NhanVien();
            if (find == null)
            {
                find = new NhanVien()
                {
                    MaNV = tb_ma.Text,
                    TenNV = tb_ten.Text,
                    NgaySinh = DateTime.Parse(dtp.Text),
                    MaPB = cb_pb.Text == "Kế Toán" ? "KT" : "KD"
                };
                db.NhanViens.Add(find);
            }else
            {
                find.MaNV = tb_ma.Text;   
                find.TenNV = tb_ten.Text;
                find.NgaySinh = DateTime.Parse(dtp.Text);
                find.MaPB = cb_pb.Text == "Kế Toán" ? "KT" : "KD";
            }
            db.SaveChanges();
            List<NhanVien> ls = db.NhanViens.ToList();
            BindData(ls);   
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            List<PhongBan> pb = db.PhongBans.ToList();
            List<NhanVien> nv = db.NhanViens.ToList();
            addToComboBox(pb);
            BindData(nv);
        }

        private void addToComboBox (List<PhongBan> list)
        {
            cb_pb.DataSource = list;
            cb_pb.DisplayMember = "TenPB";
            cb_pb.ValueMember = "MaPB";
        }
        private void BindData (List<NhanVien> nv)
        {
            dataGridView1.Rows.Clear();
            foreach(NhanVien nhanvien in nv)
            {
                int idx = dataGridView1.Rows.Add();
                dataGridView1.Rows[idx].Cells[0].Value = nhanvien.MaNV;
                dataGridView1.Rows[idx].Cells[1].Value = nhanvien.TenNV;
                dataGridView1.Rows[idx].Cells[2].Value = nhanvien.NgaySinh;
                dataGridView1.Rows[idx].Cells[3].Value = nhanvien.PhongBan.TenPB;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int idx = e.RowIndex;
            tb_ma.Text = dataGridView1.Rows[idx].Cells[0].Value.ToString();
            tb_ten.Text = dataGridView1.Rows[idx].Cells[1].Value.ToString();
            dtp.Text = dataGridView1.Rows[idx].Cells[2].Value.ToString();
            cb_pb.Text = dataGridView1.Rows[idx].Cells[3].Value.ToString();
        }

        private void btn_xoa_Click(object sender, EventArgs e)
        {
            var find = db.NhanViens.FirstOrDefault(s => s.MaNV == tb_ma.Text);
            if (find != null)
            {
                var ask = MessageBox.Show("Ban co muon xoa ? ", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (ask == DialogResult.Yes)
                {
                    db.NhanViens.Remove(find);
                }
                db.SaveChanges();
                List<NhanVien> nv = db.NhanViens.ToList(); 
                BindData(nv);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var ask = MessageBox.Show("Ban co muon thoat", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (ask == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}
