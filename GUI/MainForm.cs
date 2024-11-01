using BUS;
using DAL;
using DAL.Entities;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GUI
{
    public partial class MainForm : Form
    {

        private UserService userService;
        private ZoneService zoneService;
        private MachineService machineService;
        private ServiceService serviceService;
        private User _currentUser;
        private List<Machine> _selectedMachines;
        private List<FoodItem> _selectedServices;
        private List<Order> orders = new List<Order>();
        public List<MachineSession> GetMachineSessions = new List<MachineSession>();
        private BUS.ReportService reportService;

        private CyBerModel _context;


        public MainForm(User user)
        {
            InitializeComponent();
            _currentUser = user;
            userService = new UserService();
            zoneService = new ZoneService();
            machineService = new MachineService();
            serviceService = new ServiceService();
            _selectedMachines = new List<Machine>();
            _selectedServices = new List<FoodItem>(); // Khởi tạo danh sách món ăn đã chọn
            LoadZones();
            LoadServices();
            reportService = new BUS.ReportService(); // Khởi tạo ReportService
            _context = new CyBerModel();
            DisplayReport();
        }


        /////////// TAB TRANG MÁY TRẠM ///////////

        private void LoadZones()
        {
            try
            {
                var zones = zoneService.GetAllZones();
                if (zones == null || zones.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy zone nào.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                comboBoxZones.DataSource = zones;
                comboBoxZones.DisplayMember = "Name";
                comboBoxZones.ValueMember = "ZoneId";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi xảy ra khi tải các zone: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboBoxZones_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxZones.SelectedValue != null)
            {
                int selectedZoneId = (int)comboBoxZones.SelectedValue;
                LoadMachines(selectedZoneId);
            }
        }

        private void LoadMachines(int zoneId)
        {
            flpDatmay.Controls.Clear();
            _selectedMachines.Clear();
            try
            {
                var machines = machineService.GetMachinesByZone(zoneId);
                if (machines.Count == 0)
                {
                    MessageBox.Show("Không có máy nào trong zone này.");
                    return;
                }

                foreach (var machine in machines)
                {
                    Button machineButton = CreateMachineButton(machine);
                    flpDatmay.Controls.Add(machineButton);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi xảy ra khi tải máy: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Button CreateMachineButton(Machine machine)
        {
            Button machineButton = new Button
            {
                Text = $"{machine.Name} - {machine.Status}",
                Tag = machine,
                Width = 150,
                Height = 100,
                BackColor = machine.Status == "Online" ? Color.LightGreen : Color.Gray
            };

            machineButton.Click += (s, e) => ToggleMachineSelection(machine, machineButton);

            return machineButton;
        }

        private void ToggleMachineSelection(Machine machine, Button machineButton)
        {
            if (_selectedMachines.Contains(machine))
            {
                _selectedMachines.Remove(machine);
                machineButton.BackColor = machine.Status == "Online" ? Color.LightGreen : Color.Gray;
                lblMachineInfo.Text = "Chọn một máy để xem thông tin."; // Reset khi bỏ chọn
                ClearListView();
            }
            else
            {
                _selectedMachines.Add(machine);
                machineButton.BackColor = Color.Red;

                // Cập nhật thông tin máy
                UpdateMachineInfo(machine);  // Gọi cập nhật thông tin máy
                LoadSelectedOrdersForMachine(machine);
            }
        }





        private void UpdateMachineInfo(Machine machine)
        {
            if (_selectedMachines.Contains(machine))
            {
                int sessionId = machineService.GetCurrentSessionIdForMachine(machine.MachineId);
                if (sessionId > 0)
                {
                    // Lấy thông tin phiên hoạt động
                    var session = machineService.GetSessionBy(sessionId);

                    if (session != null)
                    {
                        // Tính toán thời gian chơi và số tiền phải thanh toán
                        TimeSpan duration = DateTime.Now - session.StartTime;
                        decimal totalAmount = (decimal)duration.TotalHours * machine.PricePerHour;

                        lblMachineInfo.Text = $"Tên máy: {machine.Name}\n" +
                                              $"Trạng thái: {machine.Status}\n" +
                                              $"ID máy: {machine.MachineId}\n" +
                                              $"Giá: {machine.PricePerHour:F0} VND\n" +
                                              $"Thời gian bắt đầu: {session.StartTime.ToString("HH:mm:ss dd/MM/yyyy")}\n" +
                                              $"Thời gian chơi: {duration.TotalHours:F2} giờ\n" +
                                              $"Tổng tiền phải thanh toán: {totalAmount:F0} VNĐ";
                    }
                }
                else
                {
                    lblMachineInfo.Text = $"Tên máy: {machine.Name}\n" +
                                          $"Trạng thái: {machine.Status}\n" +
                                          $"ID máy: {machine.MachineId}\n" +
                                          $"Giá: {machine.PricePerHour:F0} VNĐ\n" +
                                          "Chưa có phiên hoạt động.";
                }
            }
            else
            {
                lblMachineInfo2.Text = "Chọn một máy để xem thông tin.";
            }
        }



        private void ToggleMachineStatus(Machine machine)
        {
            // Kiểm tra trạng thái máy
            if (machine.Status == "Online")
            {
                // Tắt máy
                machine.Status = "Offline";

                // Tìm phiên máy hiện tại
                var currentSession = _context.MachineSessions.FirstOrDefault(s => s.MachineId == machine.MachineId && s.EndTime == null);
                if (currentSession != null)
                {
                    // Cập nhật thời gian kết thúc
                    currentSession.EndTime = DateTime.Now;

                    // Lấy giá tiền của máy
                    var machinePricePerHour = machine.PricePerHour; // Giả sử Machine có thuộc tính PricePerHour lưu giá theo giờ

                    // Tính thời gian sử dụng (giờ)
                    var duration = (currentSession.EndTime.Value - currentSession.StartTime).TotalHours;

                    // Tính tổng tiền (có thể làm tròn thời gian)
                    currentSession.TotalAmount = (decimal)duration * machinePricePerHour;


                    // Cập nhật phiên làm việc vào database
                    _context.SaveChanges();
                }
            }
            else
            {
                // Bật máy
                machine.Status = "Online";

                // Tạo một phiên làm việc mới cho máy
                var newSession = new MachineSession
                {
                    MachineId = machine.MachineId,
                    StartTime = DateTime.Now,
                    TotalAmount = 0 // Khởi tạo tổng tiền là 0
                };

                _context.MachineSessions.Add(newSession);
                _context.SaveChanges();
            }

            // Cập nhật trạng thái máy vào database
            _context.SaveChanges();
        }



        private void EndMachineSession(Machine machine)
        {
            // Lấy phiên hiện tại của máy
            int sessionId = machineService.GetCurrentSessionIdForMachine(machine.MachineId);
            if (sessionId > 0)
            {
                var session = machineService.GetSessionBy(sessionId);
                if (session != null)
                {
                    // Kết thúc phiên và cập nhật trạng thái máy
                    session.EndTime = DateTime.Now; // Cập nhật thời gian kết thúc
                    session.TotalAmount = 0; // Đặt lại tổng tiền về 0

                    // Cập nhật trạng thái máy về offline
                    machineService.UpdateMachineStatus(machine.MachineId, "Offline");

                    // Lưu thay đổi vào cơ sở dữ liệu
                    _context.SaveChanges();
                }
            }
        }




        private void StartNewMachineSession(Machine machine)
        {
            var session = new MachineSession
            {
                MachineId = machine.MachineId,
                StartTime = DateTime.Now,
                EndTime = null,
                TotalAmount = 0
            };

            // Lưu phiên mới vào cơ sở dữ liệu
            machineService.StartNewSession(session);
        }




        private void btnDatmay_Click(object sender, EventArgs e)
        {
            if (_selectedMachines.Count > 0)
            {
                try
                {
                    List<string> offlineMachines = new List<string>();

                    foreach (var machine in _selectedMachines)
                    {
                        if (machine.Status == "Offline") // Nếu máy offline thì bật máy
                        {
                            machineService.TurnOnMachine(machine.MachineId); // Bật máy
                            machine.Status = "Online"; // Cập nhật trạng thái máy
                            StartNewMachineSession(machine); // Bắt đầu phiên hoạt động mới
                            offlineMachines.Add(machine.Name); // Thêm vào danh sách máy đã bật
                        }
                        else if (machine.Status == "Online")
                        {
                            // Nếu máy đã online, chỉ cập nhật thông tin.
                            UpdateMachineInfo(machine);
                        }
                    }

                    LoadMachines((int)comboBoxZones.SelectedValue); // Tải lại danh sách máy sau khi cập nhật

                    // Hiển thị thông báo nếu có máy đã được bật
                    if (offlineMachines.Count > 0)
                    {
                        MessageBox.Show($"Các máy sau đã được bật: {string.Join(", ", offlineMachines)}", "Thông báo", MessageBoxButtons.OK);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Có lỗi xảy ra khi thao tác với máy: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn ít nhất một máy để đặt.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        ///////// TAB DỊCH VỤ //////////
        private void LoadServices(string searchQuery = "", string selectedCategory = "Tất cả")
        {
            flpService.Controls.Clear();
            try
            {
                var services = serviceService.GetFoodItems();

                // Lọc theo danh mục được chọn (nếu không phải là "Tất cả")
                if (selectedCategory != "Tất cả")
                {
                    services = services.Where(s => s.Foodroles.Equals(selectedCategory, StringComparison.OrdinalIgnoreCase)).ToList();
                }

                // Nếu có từ khóa tìm kiếm, thực hiện tìm kiếm theo chuỗi con trong tên dịch vụ
                if (!string.IsNullOrEmpty(searchQuery))
                {
                    services = services.Where(s => s.Name.ToLower().Contains(searchQuery)).ToList();
                }
                foreach (var service in services)
                {
                    FlowLayoutPanel servicePanel = new FlowLayoutPanel
                    {
                        Width = 150,
                        Height = 280,
                        FlowDirection = FlowDirection.TopDown,
                        BorderStyle = BorderStyle.FixedSingle,
                        Padding = new Padding(5),
                        AutoSize = true
                    };

                    Button serviceButton = CreateServiceButton(service);
                    serviceButton.Text = "";
                    servicePanel.Controls.Add(serviceButton);

                    Label serviceNameLabel = new Label
                    {
                        Text = service.Name,
                        AutoSize = true,
                        TextAlign = ContentAlignment.MiddleCenter,
                        Padding = new Padding(0, 5, 0, 0)
                    };
                    servicePanel.Controls.Add(serviceNameLabel);

                    Label servicePriceLabel = new Label
                    {
                        Text = $"Giá: {service.Price.ToString("F0")} VND",
                        AutoSize = true,
                        TextAlign = ContentAlignment.MiddleCenter,
                        Padding = new Padding(0, 0, 0, 5)
                    };
                    servicePanel.Controls.Add(servicePriceLabel);

                    NumericUpDown quantitySelector = new NumericUpDown
                    {
                        Minimum = 1,
                        Maximum = 100,
                        Value = 1,
                        Width = 140,
                        Tag = service
                    };
                    quantitySelector.ValueChanged += (s, e) => UpdateSelectedItemsAndTotal();
                    servicePanel.Controls.Add(quantitySelector);

                    flpService.Controls.Add(servicePanel);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi xảy ra khi tải dịch vụ: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Hàm tìm kiếm update LoadService 
        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            // Lấy từ khóa tìm kiếm từ TextBox và danh mục đã chọn
            string searchQuery = txtTimKiem.Text.Trim().ToLower();
            string selectedCategory = cbbPhanLoai.SelectedItem.ToString();

            // Gọi lại LoadServices với từ khóa tìm kiếm và danh mục được chọn
            LoadServices(searchQuery, selectedCategory);
        }
        //Hàm phân loại update LoadService 
        private void cbbPhanLoai_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Lấy từ khóa tìm kiếm từ TextBox và danh mục đã chọn
            string searchQuery = txtTimKiem.Text.Trim().ToLower();
            string selectedCategory = cbbPhanLoai.SelectedItem.ToString();

            // Gọi lại LoadServices với từ khóa tìm kiếm và danh mục được chọn
            LoadServices(searchQuery, selectedCategory);
        }

        private Button CreateServiceButton(FoodItem service)
        {
            Button serviceButton = new Button
            {
                Width = 140,
                Height = 140,
                Tag = service,
                BackgroundImageLayout = ImageLayout.Zoom
            };

            string fullImagePath = GetFullImagePath(service.ImagePath);
            if (IsImagePathValid(fullImagePath))
            {
                try
                {
                    serviceButton.BackgroundImage = Image.FromFile(fullImagePath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Có lỗi khi tải ảnh: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            serviceButton.Click += (s, e) => ToggleServiceSelection(service, serviceButton);

            return serviceButton;
        }

        private void ToggleServiceSelection(FoodItem service, Button serviceButton)
        {
            if (_selectedServices.Contains(service))
            {
                _selectedServices.Remove(service);
                serviceButton.BackColor = Color.LightGray;
            }
            else
            {
                _selectedServices.Add(service);
                serviceButton.BackColor = Color.LightBlue;
            }

            // Cập nhật danh sách và tổng tiền
            UpdateSelectedItemsAndTotal();

        }


        private void UpdateSelectedItemsAndTotal()
        {
            // Xóa danh sách hiện tại
            lsbDichvu.Items.Clear();

            // Cập nhật danh sách các món đã chọn
            decimal totalAmount = 0;
            foreach (FlowLayoutPanel servicePanel in flpService.Controls)
            {
                var service = (FoodItem)servicePanel.Controls.OfType<Button>().FirstOrDefault()?.Tag;
                var quantitySelector = servicePanel.Controls.OfType<NumericUpDown>().FirstOrDefault();

                if (service != null && quantitySelector != null && _selectedServices.Contains(service))
                {
                    int quantity = (int)quantitySelector.Value;
                    lsbDichvu.Items.Add($"{service.Name} - {service.Price.ToString("F0")} x {quantity} VND");

                    totalAmount += service.Price * quantity;
                }
            }

            // Hiển thị tổng tiền service 
            lblTotalAmount.Text = $"Tổng tiền: {totalAmount.ToString("F0")} VND";
        }

        private bool IsImagePathValid(string imagePath)
        {
            if (string.IsNullOrWhiteSpace(imagePath) || !File.Exists(imagePath))
                return false;

            string extension = Path.GetExtension(imagePath).ToLower();
            return extension == ".jpg" || extension == ".jpeg" || extension == ".png";
        }

        private string GetFullImagePath(string imagePath)
        {
            if (imagePath.Contains("Images"))
            {
                return Path.Combine(Application.StartupPath, imagePath);
            }
            return imagePath;

        }


        private void btnOrder_Click(object sender, EventArgs e)
        {
            if (_selectedMachines.Count > 0) // Kiểm tra xem có máy nào đã chọn không
            {
                try
                {
                    List<string> offlineMachines = new List<string>();
                    List<string> orderedMachines = new List<string>();

                    foreach (var machine in _selectedMachines)
                    {
                        if (machine.Status != "Online")
                        {
                            offlineMachines.Add(machine.Name);
                        }
                    }

                    if (offlineMachines.Count > 0)
                    {
                        MessageBox.Show($"Máy: {string.Join(", ", offlineMachines)} không online, không thể thêm dịch vụ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Lặp qua tất cả các máy đã chọn
                    foreach (var machine in _selectedMachines)
                    {
                        if (machine.Status == "Online")
                        {
                            int sessionId = machineService.GetCurrentSessionIdForMachine(machine.MachineId);

                            if (sessionId <= 0)
                            {
                                MessageBox.Show($"Máy {machine.Name} không có phiên hoạt động hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                continue;
                            }

                            // Lặp qua tất cả các FlowLayoutPanel trong flpService
                            foreach (FlowLayoutPanel servicePanel in flpService.Controls)
                            {
                                var service = (FoodItem)servicePanel.Controls.OfType<Button>().FirstOrDefault()?.Tag;
                                var quantitySelector = servicePanel.Controls.OfType<NumericUpDown>().FirstOrDefault();

                                if (service != null && quantitySelector != null && _selectedServices.Contains(service))
                                {
                                    int quantity = (int)quantitySelector.Value;
                                    // Tạo đối tượng đơn hàng
                                    Order order = new Order
                                    {
                                        SessionId = sessionId,
                                        FoodItemId = service.FoodItemId,
                                        Quantity = quantity,
                                        Price = service.Price
                                    };

                                    try
                                    {
                                        // Lưu đơn hàng
                                        serviceService.SaveOrder(order);
                                        orderedMachines.Add(machine.Name); // Thêm máy đã đặt
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show($"Có lỗi xảy ra khi thêm dịch vụ cho máy {machine.Name}: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                            }
                        }
                    }
                    if (orderedMachines.Count > 0)
                    {
                        MessageBox.Show($"thêm dịch vụ thành công!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Có lỗi xảy ra: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn ít nhất một máy trước khi thêm dịch vụ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void btnThanhtoan_Click(object sender, EventArgs e)
        {
            
            if (_selectedMachines.Count > 0)
            {
                decimal serviceFee = 10.00m; // Hoặc lấy từ thông tin phí dịch vụ nhập vào

                foreach (var machine in _selectedMachines)
                {
                    HandlePayment(machine, serviceFee);
                }

                // Xóa danh sách đơn hàng cũ
                ClearListView();

                // Tải lại danh sách đơn hàng cho từng máy
                foreach (var machine in _selectedMachines)
                {
                    LoadSelectedOrdersForMachine(machine);
                }

                // Tải lại danh sách máy sau khi thanh toán
                LoadMachines((int)comboBoxZones.SelectedValue); // Giả sử bạn đang dùng ComboBox để chọn vùng
            }
            else
            {
                MessageBox.Show("Vui lòng chọn máy để thanh toán.", "Thông báo", MessageBoxButtons.OK);
            }
        }
        private void ClearListView()
        {
            lsbOrders.Items.Clear(); // Xóa tất cả các mục trong ListBox
        }



        private void HandlePayment(Machine machine, decimal serviceFee)
        {
            if (machine == null)
            {
                MessageBox.Show("Máy không hợp lệ.", "Lỗi", MessageBoxButtons.OK);
                return; // Dừng thực hiện nếu máy không hợp lệ
            }

            int sessionId = machineService.GetCurrentSessionIdForMachine(machine.MachineId);
            if (sessionId <= 0)
            {
                MessageBox.Show("Không có phiên hoạt động cho máy này.", "Lỗi", MessageBoxButtons.OK);
                return; // Dừng thực hiện nếu không có phiên
            }

            var session = machineService.GetSessionBy(sessionId);
            if (session == null)
            {
                MessageBox.Show("Phiên hoạt động không tồn tại.", "Lỗi", MessageBoxButtons.OK);
                return; // Dừng thực hiện nếu phiên không tồn tại
            }

            // Tính toán tổng số tiền cho máy, món ăn và phí dịch vụ
    TimeSpan duration = DateTime.Now - session.StartTime;
    decimal totalAmountForMachine = (decimal)duration.TotalHours * machine.PricePerHour;
    var orders = _context.Orders.Where(o => o.SessionId == sessionId).ToList();
    decimal totalFoodAmount = orders.Sum(order => order.Quantity * order.Price);
    decimal totalPayment = totalAmountForMachine + totalFoodAmount /*+ serviceFee*/;

    // Lưu tổng tiền vào phiên và hiển thị form hóa đơn
    session.TotalAmount = totalPayment;
    _context.SaveChanges();

    // Hiển thị ReceiptForm
    ReceiptForm receiptForm = new ReceiptForm(
        machine.Name,
        duration,
        totalAmountForMachine,
        totalFoodAmount,
        serviceFee,
        totalPayment,
        orders
    );
    receiptForm.ShowDialog();

    // Kết thúc phiên và cập nhật máy
    EndMachineSession(machine);
    UpdateMachineInfo(machine);
        }


        private void LoadSelectedOrdersForMachine(Machine machine)
        {
            if (machine == null)
            {
                MessageBox.Show("Máy không hợp lệ.", "Lỗi", MessageBoxButtons.OK);
                return;
            }

            // Xóa danh sách hiện tại trong ListBox
            lsbOrders.Items.Clear();

            try
            {
                // Lấy sessionId hiện tại cho máy
                int sessionId = machineService.GetCurrentSessionIdForMachine(machine.MachineId);

                if (sessionId <= 0)
                {
                    return;
                }

                // Gọi phương thức để lấy danh sách đơn hàng từ cơ sở dữ liệu cho phiên này
                var ordersFromDb = serviceService.GetOrdersForSession(sessionId); // Cập nhật phương thức này trong ServiceService

                if (ordersFromDb == null || !ordersFromDb.Any())
                {
                    return;
                }

                // Hiển thị danh sách món ăn đã đặt trong ListBox
                foreach (var order in ordersFromDb)
                {
                    // Tạo một hàng mới cho ListView
                    ListViewItem item = new ListViewItem(order.FoodItem.Name);
                    item.SubItems.Add(order.Price.ToString("F0") + (" VNĐ")); // Đơn giá
                    item.SubItems.Add(order.Quantity.ToString()); // Số lượng
                    item.SubItems.Add((order.Price * order.Quantity).ToString("F0") + (" VNĐ")); // Tổng tiền
                    lsbOrders.Items.Add(item); // Thêm mục vào ListView
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi xảy ra khi tải đơn hàng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //CHỈNH SỬA MENU
        private void btnEdit_Click(object sender, EventArgs e)
        {
            frmEditService editServiceForm = new frmEditService();
            editServiceForm.ServiceUpdated += OnServiceUpdated; // Đăng ký sự kiện
            editServiceForm.ShowDialog(); // Hiện Form chỉnh sửa
        }
        // Sự kiện để làm mới danh sách dịch vụ sau khi chỉnh sửa
        private void OnServiceUpdated(object sender, EventArgs e)
        {
            MessageBox.Show("Dịch vụ đã được cập nhật!");
            LoadServices(txtTimKiem.Text.Trim().ToLower(), cbbPhanLoai.SelectedItem?.ToString() ?? "Tất cả");
        }


        ///////// REPORT /////////
        private void MainForm_Load(object sender, EventArgs e)
        {

            try
            {
                cbbPhanLoai.SelectedIndex = 0;
                // Lấy danh sách báo cáo doanh thu từ ReportService
                List<RevenueReport> reportData = reportService.GetRevenueReport();

                // Tạo ReportDataSource và thiết lập dữ liệu cho ReportViewer
                ReportDataSource reportDataSource = new ReportDataSource("DoanhThu", reportData);

                // Xóa nguồn dữ liệu cũ
                Report.LocalReport.DataSources.Clear();

                // Thêm dữ liệu nguồn mới vào ReportViewer
                Report.LocalReport.DataSources.Add(reportDataSource);

                // Đặt đường dẫn tới file .rdlc
                Report.LocalReport.ReportPath = "Report.rdlc"; // Thay bằng đường dẫn đúng

                // Refresh lại ReportViewer để hiển thị dữ liệu
                Report.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải báo cáo: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public void UpdateAndDisplayReport(int sessionId, decimal updatedTotalAmount)
        {
            // Cập nhật dữ liệu vào cơ sở dữ liệu
            reportService.UpdateSessionAndGenerateReport(sessionId, updatedTotalAmount);

            // Sau khi cập nhật, hiển thị dữ liệu lên ReportViewer
            DisplayReport();
        }

        private void DisplayReport()
        {
            try
            {
                // Lấy danh sách báo cáo doanh thu từ ReportService
                List<RevenueReport> reportData = reportService.GetRevenueReport();

                // Kiểm tra xem có dữ liệu hay không
                if (reportData == null || !reportData.Any())
                {
                    MessageBox.Show("Không có dữ liệu để hiển thị trong báo cáo.", "Thông báo", MessageBoxButtons.OK);
                    return;
                }

                // Tạo ReportDataSource và thiết lập dữ liệu cho ReportViewer
                ReportDataSource reportDataSource = new ReportDataSource("DoanhThu", reportData);

                // Xóa nguồn dữ liệu cũ
                Report.LocalReport.DataSources.Clear();

                // Thêm dữ liệu nguồn mới vào ReportViewer
                Report.LocalReport.DataSources.Add(reportDataSource);

                // Đặt đường dẫn tới file .rdlc
                Report.LocalReport.ReportPath = "Report.rdlc"; // Thay bằng đường dẫn đúng

                // Refresh lại ReportViewer để hiển thị dữ liệu mới
                Report.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải báo cáo: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        


        /// MẤY CHỨC NĂNG LINH TINH ///
        private void btnThanhtoan_MouseEnter(object sender, EventArgs e)
        {
            btnThanhtoan.BackColor = Color.Green;
            btnThanhtoan.ForeColor = Color.White;
        }

        private void btnThanhtoan_MouseLeave(object sender, EventArgs e)
        {
            btnThanhtoan.BackColor = SystemColors.Control;
            btnThanhtoan.ForeColor = Color.Black;
        }

        private bool isLoggingOut = false;
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isLoggingOut)
            {
                //Hiển thị MessageBox
                DialogResult result = MessageBox.Show("Bạn có chắc muốn thoát không?",
                    "Xác nhận thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                // Kiểm tra lựa chọn của người dùng
                if (result == DialogResult.No)
                { // Hủy sự kiện đóng form nếu người dùng chọn No
                    e.Cancel = true;
                }
            }
        }


        private void btnLogout_Click(object sender, EventArgs e)
        {
            //Hiển thị MessageBox
            DialogResult logout = MessageBox.Show("Bạn có chắc muốn đăng xuất không?", "Xác nhận đăng xuất", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            // Kiểm tra lựa chọn của người dùng
            if (logout == DialogResult.Yes)
            { // Hủy sự kiện đóng form nếu người dùng chọn No
                isLoggingOut = true;
                this.Close();
                frmLogin frmLogin = new frmLogin();
                frmLogin.Show();
            }
        }
    }
}
        
    

