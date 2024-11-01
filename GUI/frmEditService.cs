using BUS;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class frmEditService : Form
    {
        private ServiceService serviceService; // Tham chiếu đến lớp ServiceService
        private FoodItem selectedFoodItem; // Lưu món ăn đang được chọn để chỉnh sửa
        private string selectedImagePath; // Đường dẫn ảnh đã chọn

        public event EventHandler ServiceUpdated; // Sự kiện khi dịch vụ được cập nhật
        public frmEditService()
        {
            InitializeComponent();
            serviceService = new ServiceService(); // Khởi tạo ServiceService để làm việc với cơ sở dữ liệu
            LoadFoodItemsWithImages(); // Tải danh sách món ăn khi form mở
        }

        // Phương thức tải danh sách món ăn kèm hình ảnh
        private void LoadFoodItemsWithImages()
        {
            flpFoodItems.Controls.Clear(); // Xóa danh sách cũ

            var foodItems = serviceService.GetFoodItems(); // Lấy danh sách món ăn từ ServiceService
            foreach (var item in foodItems)
            {
                FlowLayoutPanel foodPanel = new FlowLayoutPanel
                {
                    Width = 150,
                    Height = 200,
                    FlowDirection = FlowDirection.TopDown,
                    BorderStyle = BorderStyle.FixedSingle,
                    Padding = new Padding(5),
                    AutoSize = true,
                    Tag = item // Gán FoodItem vào thuộc tính Tag để tiện truy xuất sau này
                };

                // Hiển thị hình ảnh món ăn
                PictureBox pictureBox = new PictureBox
                {
                    Width = 120,
                    Height = 120,
                    SizeMode = PictureBoxSizeMode.Zoom
                };

                if (File.Exists(item.ImagePath)) // Kiểm tra xem ảnh có tồn tại không
                {
                    pictureBox.Image = Image.FromFile(item.ImagePath);
                }

                foodPanel.Controls.Add(pictureBox); // Thêm ảnh vào FlowLayoutPanel

                // Hiển thị tên món ăn
                Label lblName = new Label
                {
                    Text = item.Name,
                    AutoSize = true,
                    TextAlign = ContentAlignment.MiddleCenter
                };
                foodPanel.Controls.Add(lblName);

                // Hiển thị giá món ăn
                Label lblPrice = new Label
                {
                    Text = $"Giá: {item.Price.ToString("F0")} VND",
                    AutoSize = true,
                    TextAlign = ContentAlignment.MiddleCenter
                };
                foodPanel.Controls.Add(lblPrice);

                // Sự kiện click để chọn món ăn chỉnh sửa
                foodPanel.Click += (s, e) => SelectFoodItem(item);

                // Thêm panel dịch vụ vào FlowLayoutPanel
                flpFoodItems.Controls.Add(foodPanel);
            }
        }

        // Phương thức chọn món ăn để chỉnh sửa
        private void SelectFoodItem(FoodItem foodItem)
        {
            selectedFoodItem = foodItem; // Lưu món ăn được chọn
            if (selectedFoodItem != null)
            {
                // Hiển thị thông tin món ăn lên các trường dữ liệu
                txtName.Text = selectedFoodItem.Name; // Đảm bảo hiển thị tên món ăn
                txtPrice.Text = selectedFoodItem.Price.ToString();
                cbbFoodRoles.SelectedItem = selectedFoodItem.Foodroles; // Chọn loại món ăn trong ComboBox
                selectedImagePath = selectedFoodItem.ImagePath;

                // Hiển thị ảnh đã chọn
                if (File.Exists(selectedImagePath))
                {
                    ptbServices.Image = Image.FromFile(selectedImagePath);
                }
                else
                {
                    ptbServices.Image = null; // Nếu ảnh không tồn tại, xóa ảnh cũ
                }
            }
        }


        private void cbbFootRoles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbFoodRoles.SelectedItem != null) // Kiểm tra nếu có loại món ăn được chọn
            {
                string selectedRole = cbbFoodRoles.SelectedItem.ToString(); // Lấy loại món ăn được chọn        
                //LoadFoodItemsByRole(selectedRole); // Tải các món ăn theo loại
            }
        }

        // Phương thức tải món ăn theo loại
        private void LoadFoodItemsByRole(string role)
        {
            flpFoodItems.Controls.Clear(); // Xóa các món ăn cũ
            var foodItems = serviceService.GetFoodItemsByRole(role); // Lấy món ăn theo loại từ ServiceService

            foreach (var item in foodItems)
            {
                FlowLayoutPanel foodPanel = new FlowLayoutPanel
                {
                    Width = 150,
                    Height = 200,
                    FlowDirection = FlowDirection.TopDown,
                    BorderStyle = BorderStyle.FixedSingle,
                    Padding = new Padding(5),
                    AutoSize = true,
                    Tag = item
                };

                PictureBox pictureBox = new PictureBox
                {
                    Width = 120,
                    Height = 120,
                    SizeMode = PictureBoxSizeMode.Zoom
                };

                if (File.Exists(item.ImagePath))
                {
                    pictureBox.Image = Image.FromFile(item.ImagePath);
                }

                foodPanel.Controls.Add(pictureBox);

                Label lblName = new Label
                {
                    Text = item.Name,
                    AutoSize = true,
                    TextAlign = ContentAlignment.MiddleCenter
                };
                foodPanel.Controls.Add(lblName);

                Label lblPrice = new Label
                {
                    Text = $"Giá: {item.Price.ToString("F0")} VND",
                    AutoSize = true,
                    TextAlign = ContentAlignment.MiddleCenter
                };
                foodPanel.Controls.Add(lblPrice);

                foodPanel.Click += (s, e) => SelectFoodItem(item); // Đảm bảo click để chọn món ăn

                flpFoodItems.Controls.Add(foodPanel);
            }
        }


        private void btnAddPicture_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    selectedImagePath = openFileDialog.FileName; // Lưu đường dẫn ảnh đã chọn
                    ptbServices.Image = Image.FromFile(selectedImagePath); // Hiển thị ảnh đã chọn
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim(); // Cắt bỏ khoảng trắng
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Vui lòng nhập tên món ăn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal price;
            if (!decimal.TryParse(txtPrice.Text.Trim(), out price)) // Kiểm tra giá nhập vào
            {
                MessageBox.Show("Giá không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cbbFoodRoles.SelectedItem == null) // Kiểm tra loại món ăn
            {
                MessageBox.Show("Vui lòng chọn loại món ăn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string role = cbbFoodRoles.SelectedItem.ToString();

            var newFoodItem = new FoodItem
            {
                Name = name,
                Price = price,
                Foodroles = role,
                ImagePath = selectedImagePath
            };

            serviceService.AddFoodItem(newFoodItem); // Thêm dịch vụ vào cơ sở dữ liệu
            ServiceUpdated?.Invoke(this, EventArgs.Empty); // Gọi sự kiện để cập nhật danh sách trong form chính
            MessageBox.Show("Thêm món thành công!");
            LoadFoodItemsWithImages(); // Tải lại danh sách sau khi thêm
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (selectedFoodItem != null)
            {
                // Cập nhật các thông tin dịch vụ
                selectedFoodItem.Name = txtName.Text.Trim();
                decimal price;
                if (!decimal.TryParse(txtPrice.Text.Trim(), out price))
                {
                    MessageBox.Show("Giá không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                selectedFoodItem.Price = price;

                // Kiểm tra loại món ăn
                if (cbbFoodRoles.SelectedItem == null)
                {
                    MessageBox.Show("Vui lòng chọn loại món ăn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                selectedFoodItem.Foodroles = cbbFoodRoles.SelectedItem.ToString();
                selectedFoodItem.ImagePath = selectedImagePath;

                // Cập nhật món ăn trong cơ sở dữ liệu
                try
                {
                    serviceService.UpdateFoodItem(selectedFoodItem);
                    MessageBox.Show("Cập nhật món thành công!");

                    // Kích hoạt sự kiện để thông báo cho form chính
                    ServiceUpdated?.Invoke(this, EventArgs.Empty);
                    LoadFoodItemsWithImages();          // Đóng form sau khi cập nhật
                  
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Có lỗi xảy ra khi cập nhật món: {ex.Message}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn món ăn cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }






        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (selectedFoodItem != null)
            {
                try
                {
                    serviceService.DeleteFoodItem(selectedFoodItem.FoodItemId); // Xóa món ăn khỏi cơ sở dữ liệu
                    ServiceUpdated?.Invoke(this, EventArgs.Empty); // Gọi sự kiện để cập nhật danh sách trong form chính
                    MessageBox.Show("Xóa món thành công!");
                    LoadFoodItemsWithImages(); // Tải lại danh sách sau khi xóa

                    // Xóa thông tin trên các textbox và hình ảnh
                    txtName.Clear();
                    txtPrice.Clear();
                    cbbFoodRoles.SelectedItem = null;
                    ptbServices.Image = null;
                    selectedFoodItem = null; // Đặt lại selectedFoodItem để không còn chọn món ăn nào
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Có lỗi xảy ra khi xóa món: {ex.Message}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn món ăn cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void frmEditService_Load(object sender, EventArgs e)
        {
            var roles = serviceService.GetFoodRoles(); // Lấy danh sách loại món ăn
            cbbFoodRoles.Items.AddRange(roles.ToArray()); // Đổ dữ liệu vào ComboBox
        }


        ///Reset lại
        private void btnReset_Click(object sender, EventArgs e)
        {
            // Xóa các trường nhập liệu và hình ảnh
            txtName.Clear();
            txtPrice.Clear();
            cbbFoodRoles.SelectedItem = null; // Thiết lập lại ComboBox
            ptbServices.Image = null; // Xóa ảnh hiển thị
            selectedFoodItem = null; // Xóa đối tượng món ăn đã chọn
            selectedImagePath = null; // Đặt lại đường dẫn ảnh đã chọn
        }
    }
}
