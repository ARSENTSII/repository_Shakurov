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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ContactsApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ContactRepository contactrepository = new ContactRepository();

        public MainWindow()
        {
            InitializeComponent();
            ContactsGrid.ItemsSource = contactrepository.GetAll();
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var contacts = contactrepository.GetAll();
            var filteredContacts = contacts.Where(contact => contact.Name.IndexOf(SearchBox.Text, StringComparison.OrdinalIgnoreCase) >= 0);
            ContactsGrid.ItemsSource = filteredContacts.ToList();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Contact contact = new Contact();

            contact.Name = NameBox.Text;
            contact.PhoneNumber = PhoneBox.Text;
            
            if (string.IsNullOrWhiteSpace(NameBox.Text))
            {
                MessageBox.Show("Введите имя контакта!");
                return;
            }

            else
            {
                contactrepository.Add(contact);
                ContactsGrid.ItemsSource = contactrepository.GetAll();
            }

            NameBox.Clear();
            PhoneBox.Clear();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Contact contact = ContactsGrid.SelectedItem as Contact;

            if (contact == null)
            {
                MessageBox.Show("Выберите контакт для удаления!");
                return;
            }

            else
            {
                contactrepository.Delete(contact);
                ContactsGrid.ItemsSource = contactrepository.GetAll();
            }
        }

        private void ContactsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Contact contact = ContactsGrid.SelectedItem as Contact;

            if (contact != null)
            {
                NameBox.Text = contact.Name;
                PhoneBox.Text = contact.PhoneNumber;
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Contact contact = ContactsGrid.SelectedItem as Contact;

            if (contact == null)
            {
                MessageBox.Show("Выберите контакт для редактирования!");
                return;
            }

            contact.Name = NameBox.Text;
            contact.PhoneNumber = PhoneBox.Text;
            contactrepository.Update(contact);
            ContactsGrid.ItemsSource = contactrepository.GetAll();
        }
    }
}
