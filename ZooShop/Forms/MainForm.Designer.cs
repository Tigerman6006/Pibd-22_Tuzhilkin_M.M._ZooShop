using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using Font = System.Drawing.Font;

namespace ZooShop
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblTitle;
        private Label lblSubtitle;
        private Label lblDate;
        private Label lblStatus;

        private GroupBox grpDirectories;
        private GroupBox grpWarehouse;
        private GroupBox grpSales;

        private Button btnProducts;
        private Button btnCategories;
        private Button btnAnimals;
        private Button btnSuppliers;

        private Button btnStockControl;
        private Button btnReplenishment;

        private Button btnSales;
        private Button btnSalesReport;

        private Button btnExit;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblTitle = new Label();
            lblSubtitle = new Label();
            lblDate = new Label();
            lblStatus = new Label();
            grpDirectories = new GroupBox();
            btnProducts = new Button();
            btnCategories = new Button();
            btnAnimals = new Button();
            btnSuppliers = new Button();
            grpWarehouse = new GroupBox();
            btnStockControl = new Button();
            btnReplenishment = new Button();
            grpSales = new GroupBox();
            btnSales = new Button();
            btnSalesReport = new Button();
            btnExit = new Button();
            menuStripMain = new MenuStrip();
            directoryToolStripMenuItem = new ToolStripMenuItem();
            productToolStripMenuItem = new ToolStripMenuItem();
            categoriesToolStripMenuItem = new ToolStripMenuItem();
            animalToolStripMenuItem = new ToolStripMenuItem();
            supplierToolStripMenuItem = new ToolStripMenuItem();
            storageToolStripMenuItem = new ToolStripMenuItem();
            storageControlToolStripMenuItem = new ToolStripMenuItem();
            replenishmentRequestsToolStripMenuItem = new ToolStripMenuItem();
            salesToolStripMenuItem = new ToolStripMenuItem();
            reportToolStripMenuItem = new ToolStripMenuItem();
            salesReportsToolStripMenuItem = new ToolStripMenuItem();
            grpDirectories.SuspendLayout();
            grpWarehouse.SuspendLayout();
            grpSales.SuspendLayout();
            menuStripMain.SuspendLayout();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 21F, FontStyle.Bold);
            lblTitle.Location = new Point(34, 27);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(167, 47);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "ZooShop";
            // 
            // lblSubtitle
            // 
            lblSubtitle.AutoSize = true;
            lblSubtitle.Font = new Font("Segoe UI", 10F);
            lblSubtitle.Location = new Point(39, 87);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(294, 23);
            lblSubtitle.TabIndex = 1;
            lblSubtitle.Text = "Система управления зоомагазином";
            // 
            // lblDate
            // 
            lblDate.AutoSize = true;
            lblDate.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblDate.Location = new Point(743, 47);
            lblDate.Name = "lblDate";
            lblDate.Size = new Size(100, 23);
            lblDate.TabIndex = 2;
            lblDate.Text = "00.00.0000";
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Font = new Font("Segoe UI", 9F);
            lblStatus.Location = new Point(34, 700);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(181, 20);
            lblStatus.TabIndex = 6;
            lblStatus.Text = "Система готова к работе";
            // 
            // grpDirectories
            // 
            grpDirectories.Controls.Add(btnProducts);
            grpDirectories.Controls.Add(btnCategories);
            grpDirectories.Controls.Add(btnAnimals);
            grpDirectories.Controls.Add(btnSuppliers);
            grpDirectories.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            grpDirectories.Location = new Point(34, 153);
            grpDirectories.Margin = new Padding(3, 4, 3, 4);
            grpDirectories.Name = "grpDirectories";
            grpDirectories.Padding = new Padding(3, 4, 3, 4);
            grpDirectories.Size = new Size(400, 307);
            grpDirectories.TabIndex = 3;
            grpDirectories.TabStop = false;
            grpDirectories.Text = "Справочники";
            // 
            // btnProducts
            // 
            btnProducts.Font = new Font("Segoe UI", 10F);
            btnProducts.Location = new Point(23, 53);
            btnProducts.Margin = new Padding(3, 4, 3, 4);
            btnProducts.Name = "btnProducts";
            btnProducts.Size = new Size(166, 87);
            btnProducts.TabIndex = 0;
            btnProducts.Text = "Товары";
            btnProducts.Click += btnProducts_Click;
            // 
            // btnCategories
            // 
            btnCategories.Font = new Font("Segoe UI", 10F);
            btnCategories.Location = new Point(206, 53);
            btnCategories.Margin = new Padding(3, 4, 3, 4);
            btnCategories.Name = "btnCategories";
            btnCategories.Size = new Size(166, 87);
            btnCategories.TabIndex = 1;
            btnCategories.Text = "Категории";
            btnCategories.Click += btnCategories_Click;
            // 
            // btnAnimals
            // 
            btnAnimals.Font = new Font("Segoe UI", 10F);
            btnAnimals.Location = new Point(23, 167);
            btnAnimals.Margin = new Padding(3, 4, 3, 4);
            btnAnimals.Name = "btnAnimals";
            btnAnimals.Size = new Size(166, 87);
            btnAnimals.TabIndex = 2;
            btnAnimals.Text = "Животные";
            btnAnimals.Click += btnAnimals_Click;
            // 
            // btnSuppliers
            // 
            btnSuppliers.Font = new Font("Segoe UI", 10F);
            btnSuppliers.Location = new Point(206, 167);
            btnSuppliers.Margin = new Padding(3, 4, 3, 4);
            btnSuppliers.Name = "btnSuppliers";
            btnSuppliers.Size = new Size(166, 87);
            btnSuppliers.TabIndex = 3;
            btnSuppliers.Text = "Поставщики";
            btnSuppliers.Click += btnSuppliers_Click;
            // 
            // grpWarehouse
            // 
            grpWarehouse.Controls.Add(btnStockControl);
            grpWarehouse.Controls.Add(btnReplenishment);
            grpWarehouse.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            grpWarehouse.Location = new Point(469, 153);
            grpWarehouse.Margin = new Padding(3, 4, 3, 4);
            grpWarehouse.Name = "grpWarehouse";
            grpWarehouse.Padding = new Padding(3, 4, 3, 4);
            grpWarehouse.Size = new Size(400, 307);
            grpWarehouse.TabIndex = 4;
            grpWarehouse.TabStop = false;
            grpWarehouse.Text = "Склад и поставки";
            // 
            // btnStockControl
            // 
            btnStockControl.Font = new Font("Segoe UI", 10F);
            btnStockControl.Location = new Point(23, 53);
            btnStockControl.Margin = new Padding(3, 4, 3, 4);
            btnStockControl.Name = "btnStockControl";
            btnStockControl.Size = new Size(166, 87);
            btnStockControl.TabIndex = 0;
            btnStockControl.Text = "Контроль склада";
            btnStockControl.Click += btnStockControl_Click;
            // 
            // btnReplenishment
            // 
            btnReplenishment.Font = new Font("Segoe UI", 10F);
            btnReplenishment.Location = new Point(206, 53);
            btnReplenishment.Margin = new Padding(3, 4, 3, 4);
            btnReplenishment.Name = "btnReplenishment";
            btnReplenishment.Size = new Size(166, 87);
            btnReplenishment.TabIndex = 1;
            btnReplenishment.Text = "Заявки на\nпополнение";
            btnReplenishment.Click += btnReplenishment_Click;
            // 
            // grpSales
            // 
            grpSales.Controls.Add(btnSales);
            grpSales.Controls.Add(btnSalesReport);
            grpSales.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            grpSales.Location = new Point(34, 493);
            grpSales.Margin = new Padding(3, 4, 3, 4);
            grpSales.Name = "grpSales";
            grpSales.Padding = new Padding(3, 4, 3, 4);
            grpSales.Size = new Size(834, 167);
            grpSales.TabIndex = 5;
            grpSales.TabStop = false;
            grpSales.Text = "Продажи и отчёты";
            // 
            // btnSales
            // 
            btnSales.Font = new Font("Segoe UI", 10F);
            btnSales.Location = new Point(23, 53);
            btnSales.Margin = new Padding(3, 4, 3, 4);
            btnSales.Name = "btnSales";
            btnSales.Size = new Size(229, 80);
            btnSales.TabIndex = 0;
            btnSales.Text = "Продажи";
            btnSales.Click += btnSales_Click;
            // 
            // btnSalesReport
            // 
            btnSalesReport.Font = new Font("Segoe UI", 10F);
            btnSalesReport.Location = new Point(274, 53);
            btnSalesReport.Margin = new Padding(3, 4, 3, 4);
            btnSalesReport.Name = "btnSalesReport";
            btnSalesReport.Size = new Size(229, 80);
            btnSalesReport.TabIndex = 1;
            btnSalesReport.Text = "Отчёты о продажах";
            btnSalesReport.Click += btnSalesReport_Click;
            // 
            // btnExit
            // 
            btnExit.Font = new Font("Segoe UI", 10F);
            btnExit.Location = new Point(720, 680);
            btnExit.Margin = new Padding(3, 4, 3, 4);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(149, 53);
            btnExit.TabIndex = 7;
            btnExit.Text = "Выход";
            btnExit.Click += btnExit_Click;
            // 
            // menuStripMain
            // 
            menuStripMain.ImageScalingSize = new Size(20, 20);
            menuStripMain.Items.AddRange(new ToolStripItem[] { directoryToolStripMenuItem, storageToolStripMenuItem, reportToolStripMenuItem });
            menuStripMain.Location = new Point(0, 0);
            menuStripMain.Name = "menuStripMain";
            menuStripMain.Size = new Size(914, 28);
            menuStripMain.TabIndex = 8;
            menuStripMain.Text = "menuStripMain";
            // 
            // directoryToolStripMenuItem
            // 
            directoryToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { productToolStripMenuItem, categoriesToolStripMenuItem, animalToolStripMenuItem, supplierToolStripMenuItem });
            directoryToolStripMenuItem.Name = "справочникиToolStripMenuItem";
            directoryToolStripMenuItem.Size = new Size(117, 24);
            directoryToolStripMenuItem.Text = "Справочники";
            // 
            // productToolStripMenuItem
            // 
            productToolStripMenuItem.Name = "productToolStripMenuItem";
            productToolStripMenuItem.Size = new Size(224, 26);
            productToolStripMenuItem.Text = "Товары";
            productToolStripMenuItem.Click += btnProducts_Click;
            // 
            // categoriesToolStripMenuItem
            // 
            categoriesToolStripMenuItem.Name = "categoriesToolStripMenuItem";
            categoriesToolStripMenuItem.Size = new Size(224, 26);
            categoriesToolStripMenuItem.Text = "Категории";
            categoriesToolStripMenuItem.Click += btnCategories_Click;
            // 
            // animalToolStripMenuItem
            // 
            animalToolStripMenuItem.Name = "animalToolStripMenuItem";
            animalToolStripMenuItem.Size = new Size(224, 26);
            animalToolStripMenuItem.Text = "Животные";
            animalToolStripMenuItem.Click += btnAnimals_Click;
            // 
            // supplierToolStripMenuItem
            // 
            supplierToolStripMenuItem.Name = "supplierToolStripMenuItem";
            supplierToolStripMenuItem.Size = new Size(224, 26);
            supplierToolStripMenuItem.Text = "Поставщики";
            supplierToolStripMenuItem.Click += btnSuppliers_Click;
            // 
            // storageToolStripMenuItem
            // 
            storageToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { storageControlToolStripMenuItem, replenishmentRequestsToolStripMenuItem, salesToolStripMenuItem });
            storageToolStripMenuItem.Name = "storageToolStripMenuItem";
            storageToolStripMenuItem.Size = new Size(95, 24);
            storageToolStripMenuItem.Text = "Операции";
            // 
            // storageControlToolStripMenuItem
            // 
            storageControlToolStripMenuItem.Name = "storageControlToolStripMenuItem";
            storageControlToolStripMenuItem.Size = new Size(252, 26);
            storageControlToolStripMenuItem.Text = "Контроль склада";
            storageControlToolStripMenuItem.Click += btnStockControl_Click;
            // 
            // replenishmentRequestsToolStripMenuItem
            // 
            replenishmentRequestsToolStripMenuItem.Name = "replenishmentRequestsToolStripMenuItem";
            replenishmentRequestsToolStripMenuItem.Size = new Size(252, 26);
            replenishmentRequestsToolStripMenuItem.Text = "Заявки на пополнение";
            replenishmentRequestsToolStripMenuItem.Click += btnReplenishment_Click;
            // 
            // salesToolStripMenuItem
            // 
            salesToolStripMenuItem.Name = "salesToolStripMenuItem";
            salesToolStripMenuItem.Size = new Size(252, 26);
            salesToolStripMenuItem.Text = "Продажи";
            salesToolStripMenuItem.Click += btnSales_Click;
            // 
            // reportToolStripMenuItem
            // 
            reportToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { salesReportsToolStripMenuItem });
            reportToolStripMenuItem.Name = "reportToolStripMenuItem";
            reportToolStripMenuItem.Size = new Size(73, 24);
            reportToolStripMenuItem.Text = "Отчеты";
            // 
            // salesReportsToolStripMenuItem
            // 
            salesReportsToolStripMenuItem.Name = "salesReportsToolStripMenuItem";
            salesReportsToolStripMenuItem.Size = new Size(228, 26);
            salesReportsToolStripMenuItem.Text = "Отчеты о продажах";
            salesReportsToolStripMenuItem.Click += btnSalesReport_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(914, 767);
            Controls.Add(lblTitle);
            Controls.Add(lblSubtitle);
            Controls.Add(lblDate);
            Controls.Add(grpDirectories);
            Controls.Add(grpWarehouse);
            Controls.Add(grpSales);
            Controls.Add(lblStatus);
            Controls.Add(btnExit);
            Controls.Add(menuStripMain);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MainMenuStrip = menuStripMain;
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ZooShop — Главное меню";
            FormClosing += MainForm_FormClosing;
            grpDirectories.ResumeLayout(false);
            grpWarehouse.ResumeLayout(false);
            grpSales.ResumeLayout(false);
            menuStripMain.ResumeLayout(false);
            menuStripMain.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }
        private MenuStrip menuStripMain;
        private ToolStripMenuItem directoryToolStripMenuItem;
        private ToolStripMenuItem productToolStripMenuItem;
        private ToolStripMenuItem categoriesToolStripMenuItem;
        private ToolStripMenuItem animalToolStripMenuItem;
        private ToolStripMenuItem supplierToolStripMenuItem;
        private ToolStripMenuItem storageToolStripMenuItem;
        private ToolStripMenuItem storageControlToolStripMenuItem;
        private ToolStripMenuItem replenishmentRequestsToolStripMenuItem;
        private ToolStripMenuItem salesToolStripMenuItem;
        private ToolStripMenuItem reportToolStripMenuItem;
        private ToolStripMenuItem salesReportsToolStripMenuItem;
    }
}
