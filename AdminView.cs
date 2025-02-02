using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using ID_Replacement.Data.Models;
using ID_Replacement.Services.Interface;

namespace ID_Replacement
{
    public partial class AdminView : Form
    {
        private readonly IAdminViewModelService _adminViewModelService;
        private IEnumerable<AdminViewModel> students;

        public AdminView(IAdminViewModelService adminViewModelService)
        {
            _adminViewModelService = adminViewModelService ?? throw new ArgumentNullException(nameof(adminViewModelService));
            students = _adminViewModelService.GetAllStudents();
            InitializeComponent();
        }

        private void AdminView_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = new BindingList<AdminViewModel>(students.ToList());
        }

    }
}
