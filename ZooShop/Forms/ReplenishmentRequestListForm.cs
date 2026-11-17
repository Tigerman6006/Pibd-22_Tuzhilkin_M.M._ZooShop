using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Windows.Forms;
using ZooShop;
using ZooShop.Entities;
using ZooShop.Enums;
using ZooShop.Repositories.Interfaces;

namespace ZooShop
{
    public partial class ReplenishmentRequestListForm : Form
    {
        private readonly IReplenishmentRequestRepository _requestRepository;
        private readonly IServiceProvider _serviceProvider;

        public ReplenishmentRequestListForm(
            IReplenishmentRequestRepository requestRepository,
            IServiceProvider serviceProvider)
        {
            InitializeComponent();

            _requestRepository = requestRepository;
            _serviceProvider = serviceProvider;

            LoadStatusFilter();
            LoadRequests();
        }

        private void LoadStatusFilter()
        {
            cmbStatusFilter.Items.Clear();
            cmbStatusFilter.Items.Add("Все статусы");

            foreach (Status status in Enum.GetValues(typeof(Status)))
                cmbStatusFilter.Items.Add(status.ToString());

            cmbStatusFilter.SelectedIndex = 0;
        }

        private void LoadRequests()
        {
            try
            {
                var requests = _requestRepository.GetAll().ToList();

                string search = txtSearch.Text.Trim()
                    .ToLowerInvariant();

                if (!string.IsNullOrWhiteSpace(search))
                {
                    requests = requests.Where(request =>
                        request.RequestNumber.ToString().Contains(search) ||
                        (request.Product?.Name ?? "")
                            .ToLowerInvariant().Contains(search) ||
                        (request.Supplier?.Name ?? "")
                            .ToLowerInvariant().Contains(search))
                        .ToList();
                }

                if (cmbStatusFilter.SelectedIndex > 0)
                {
                    string selectedStatus =
                        cmbStatusFilter.SelectedItem?.ToString() ?? "";

                    requests = requests.Where(request =>
                        request.Status.ToString() == selectedStatus)
                        .ToList();
                }

                dgvRequests.Rows.Clear();

                foreach (ReplenishmentRequest request in requests)
                {
                    int rowIndex = dgvRequests.Rows.Add(
                        request.RequestNumber,
                        request.CreationDate.ToString("dd.MM.yyyy"),
                        request.Product?.Name ?? "Не указан",
                        request.RequiredQuantity,
                        request.Supplier?.Name ?? "Не назначен",
                        request.Status.ToString());

                    dgvRequests.Rows[rowIndex].Tag = request;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Не удалось загрузить заявки.\n{ex.Message}",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using var form =
                _serviceProvider.GetRequiredService<ReplenishmentRequestAddForm>();

            if (form.ShowDialog(this) == DialogResult.OK)
                LoadRequests();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            cmbStatusFilter.SelectedIndex = 0;
            LoadRequests();
        }
        private void btnComplete_Click(object sender, EventArgs e)
        {
            if (dgvRequests.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    "Выберите заявку.",
                    "Внимание",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            var request = dgvRequests.SelectedRows[0].Tag
                as ReplenishmentRequest;

            if (request is null)
                return;

            if (request.Status != Status.InOrder)
            {
                MessageBox.Show(
                    "Выполнить можно только заявку со статусом InOrder.",
                    "Внимание",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            var result = MessageBox.Show(
                $"Добавить на склад {request.RequiredQuantity} ед. товара «{request.Product?.Name}»?",
                "Подтверждение выполнения",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
                return;

            try
            {
                _requestRepository.CompleteRequest(request.RequestNumber);

                MessageBox.Show(
                    "Заявка успешно выполнена. Остаток товара обновлён.",
                    "Успешно",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                LoadRequests();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Не удалось выполнить заявку.\n{ex.Message}",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        private void btnReceive_Click(object sender, EventArgs e)
        {
            if (dgvRequests.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    "Выберите заявку.",
                    "Внимание",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            var request = dgvRequests.SelectedRows[0].Tag
                as ReplenishmentRequest;

            if (request == null)
                return;

            if (request.Status != Status.Sent)
            {
                MessageBox.Show(
                    "Получить можно только заявку со статусом Sent.",
                    "Внимание",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            var result = MessageBox.Show(
                $"Перевести заявку №{request.RequestNumber} в статус InOrder?",
                "Подтверждение",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
                return;

            try
            {
                _requestRepository.ReceiveRequest(request.RequestNumber);

                MessageBox.Show(
                    "Заявка успешно получена.",
                    "Успешно",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                LoadRequests();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Не удалось получить заявку.\n{ex.Message}",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadRequests();
        }

        private void cmbStatusFilter_SelectedIndexChanged(
            object sender,
            EventArgs e)
        {
            LoadRequests();
        }
    }
}
