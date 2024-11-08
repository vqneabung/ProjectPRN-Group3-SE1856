using BussinessObjects;
using System.IO;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Services;
using Microsoft.Extensions.DependencyInjection;

namespace WPFApp.Assignment_Management
{
    /// <summary>
    /// Interaction logic for SubmitAssignmentWindow.xaml
    /// </summary>
    public partial class SubmitAssignmentWindow : Window
    {
        private readonly IService<Document> _service;
        private readonly IService<Submission> _submissionService;
        public SubmitAssignmentWindow()
        {
            InitializeComponent();
            _service = App.ServiceProvider.GetService<IService<BussinessObjects.Document>>();
            _submissionService = App.ServiceProvider.GetService<IService<BussinessObjects.Submission>>();
        }
        public Assignment assignement {  get; set; }
        public int studentId { get; set; }
        public int courseId { get; set; }

        private byte[] fileByte;

        private void SubmitAssignment_Click(object sender, RoutedEventArgs e)
        {
            Document doc = new();
            var allDocuments = _service.GetAll();
            int lastIdDoc = 0; // Mặc định nếu không có dữ liệu

            if (allDocuments.Any())  // Kiểm tra nếu có ít nhất 1 phần tử
            {
                lastIdDoc = allDocuments.Last().DocumentId;
            }

            doc.DocumentId = lastIdDoc + 1;
            doc.CourseId = courseId;
            doc.Title = txtTitle.Text;
            doc.Description = txtDescription.Text;
            doc.FileAttachment = fileByte;

            // Lưu file vào database với thông tin bổ sung
            _service.Add(doc);


            int lstSubmis = _submissionService.GetAll().First().SubmissionId;
            int newId = lstSubmis + 1;
            Submission submission = new();
            submission.SubmissionId = newId;
            submission.StudentId = studentId;
            submission.AssignmentId = assignement.AssignmentId;
            submission.Assignment = assignement;
            submission.SubmissionDate = DateOnly.FromDateTime(DateTime.Now);
            submission.Grade = null;

            _submissionService.Add(submission);

            MessageBox.Show("Done Submit!");
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void UploadFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "All Files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                txtFilePath.Text = filePath;

                // Đọc file thành mảng byte
                byte[] fileBytes = File.ReadAllBytes(filePath);

                fileByte = fileBytes;
                MessageBox.Show("File uploaded successfully!");
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtAssignmentId.Text = assignement.AssignmentId.ToString();
            txtCourseId.Text = courseId.ToString();
        }
    }
}
