using DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class ReceiptForm : Form
    {
        public ReceiptForm(string machineName, TimeSpan playDuration, decimal totalMachineAmount, decimal totalFoodAmount, decimal serviceFee, decimal totalPayment, List<Order> orders)
        {
            InitializeComponent();
            // Thiết lập thông tin tổng quan
            lblMachineName.Text = $"{machineName}";
            lblPlayDuration.Text = $" {playDuration.ToString(@"hh\:mm\:ss")}";
            lblTotalMachineAmount.Text = $" {totalMachineAmount:F0} VND";
            lblTotalPayment.Text = $"Tổng: {totalPayment:F0} VND";

            // Cấu hình DataGridView với các cột
            dataGridViewOrders.Columns.Add("ItemNameColumn", "Tên Món");
            dataGridViewOrders.Columns.Add("QuantityColumn", "Số Lượng");
            dataGridViewOrders.Columns.Add("UnitPriceColumn", "Đơn Giá");
            dataGridViewOrders.Columns.Add("PriceColumn", "Thành Tiền");

            // Thêm dữ liệu vào DataGridView
            foreach (var order in orders)
            {
                dataGridViewOrders.Rows.Add(order.FoodItem.Name, order.Quantity, order.Price.ToString("F0") + " VND", (order.Quantity * order.Price).ToString("F0") + " VND");
            }
        }

        

        private void btnPrint_Click(object sender, EventArgs e)
        {
            PrintDocument printDocument = new PrintDocument();
            printDocument.PrintPage += new PrintPageEventHandler(printDocument_PrintPage);
            PrintPreviewDialog previewDialog = new PrintPreviewDialog
            {
                Document = printDocument
            };
            previewDialog.ShowDialog();
        }
        
        private void printDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            int fixedWidth = 50; // Độ dài cố định để căn chỉnh cho toàn bộ hóa đơn
            string separator = new string('-', fixedWidth); // Đường gạch ngang ngăn cách với độ dài vừa phải

            // Căn giữa các tiêu đề
            string title = "CYBERGAME".PadLeft((fixedWidth + "CYBERGAME".Length) / 2).PadRight(fixedWidth);
            string machineInfoHeader = "THÔNG TIN MÁY".PadLeft((fixedWidth + "THÔNG TIN MÁY".Length) / 2).PadRight(fixedWidth);
            string orderDetailsHeader = "CHI TIẾT DỊCH VỤ".PadLeft((fixedWidth + "CHI TIẾT ĐƠN HÀNG".Length) / 2).PadRight(fixedWidth);
            string thankYouMessage = "Cảm ơn quý khách đã sử dụng dịch vụ của CYBERGAME!".PadLeft((fixedWidth + "Cảm ơn quý khách đã sử dụng dịch vụ của CYBERGAME!".Length) / 2).PadRight(fixedWidth);

            // Xây dựng nội dung hóa đơn
            string receiptText = $"{title}\n" +       // Tiêu đề căn giữa
                                 $"{separator}\n" +   // Đường phân cách
                                 $"{machineInfoHeader}\n" +
                                 $"{separator}\n" +
                                 $"Tên máy       : {lblMachineName.Text}\n" +
                                 $"Thời gian chơi: {lblPlayDuration.Text}\n" +
                                 $"Tổng tiền máy : {lblTotalMachineAmount.Text}\n" +
                                 $"{separator}\n\n" +
                                 $"{orderDetailsHeader}\n" +
                                 $"{separator}\n";

            // Lặp qua từng đơn hàng trong DataGridView và định dạng cột
            foreach (DataGridViewRow row in dataGridViewOrders.Rows)
            {
                if (!row.IsNewRow)
                {
                    receiptText += $"{row.Cells["ItemNameColumn"].Value,-20} " +        // Tên hàng (căn trái)
                                   $"giá: {row.Cells["UnitPriceColumn"].Value,8} " +  // Đơn giá (căn phải)
                                   $"SL: {row.Cells["QuantityColumn"].Value,-5} \n";      // Số lượng (căn trái)                                
                }
            }

            // Thêm phần tổng tiền thanh toán cuối cùng
            receiptText += $"\n{separator}\n" +
                           "TỔNG TIỀN THANH TOÁN\n" +
                           $"{lblTotalPayment.Text}\n" +
                           $"{separator}\n\n" +
                           $"{thankYouMessage}";

            // Thiết lập font chữ và vị trí in
            Font printFont = new Font("Arial", 12);
            PointF startPoint = new PointF(80, 80);  // Tọa độ in ở phía trên trang giấy
            e.Graphics.DrawString(receiptText, printFont, Brushes.Black, startPoint);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close(); // Đóng form ReceiptForm
        }
    }
}
