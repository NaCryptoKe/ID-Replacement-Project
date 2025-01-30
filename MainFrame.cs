using System;
using System.Windows.Forms;
using ID_Replacement.Data.Models;

namespace ID_Replacement
{
    public partial class MainFrame : Form
    {
        private readonly Student _loggedInStudent;

        public MainFrame(Student student)
        {
            InitializeComponent();
            _loggedInStudent = student;
        }
    }
}
