using bikestoreTornado;
using Microsoft.Data.SqlClient;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace bikestoreTornado
{
    public partial class bikestorTornado : Form
    {
        private readonly string connectionString = "Server=DESKTOP-O8UJAOD;Database=Bikestore;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True;";
        private SqlConnection _connection;
        public bikestorTornado()
        {
            InitializeComponent();
            _connection = new SqlConnection(connectionString);
            try
            {
                _connection.Open();
                MessageBox.Show("Connection successful!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to connect to database: {ex.Message}");
            }

            LoadComboBoxBikeData("SELECT BikeTypeID, TypeName FROM BikeTypes", bikeTypeComboBox, _bikeTypes, "bike types");
            LoadComboBoxBikeData("SELECT ColorID, ColorName FROM Colors", bikeColorCombobox, _bikeColors, "bike colors");
            LoadComboBoxBikeData("SELECT SizeID, SizeDescription FROM Sizes", bikeSizeComboBox, _bikeSizes, "bike sizes");
            LoadComboBoxBikeData("SELECT SupplierID, Country FROM Suppliers", madeInComboBox, _bikeMadeIn, "bike countries");
            GetCurrentBikeID();
            LoadComboBoxClientData();
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (_connection != null && _connection.State == ConnectionState.Open)
            {
                _connection.Close();
            }
            base.OnFormClosing(e);
        }
        // Variable initialization
        private Dictionary<string, int> _bikeTypes = new Dictionary<string, int>(); // Dictionary to store BikeTypeID
        private Dictionary<string, int> _bikeColors = new Dictionary<string, int>(); // Dictionary to store ColorID
        private Dictionary<string, int> _bikeSizes = new Dictionary<string, int>(); // Dictionary to store SizeID
        private Dictionary<string, int> _bikeMadeIn = new Dictionary<string, int>(); // Dictionary to store SupplierID
        private int _currentBikeID; // Variable to store the current BikeID

        // Methods for loading data from the database

        private void LoadComboBoxBikeData(string query, ComboBox comboBox, Dictionary<string, int> dictionary, string dataName)
        {
            comboBox.Items.Clear();
            dictionary.Clear();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string name = reader.GetString(1);

                            comboBox.Items.Add(name);
                            dictionary.Add(name, id);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading {dataName}: {ex.Message}");
            }
        }

        private void LoadComboBoxClientData()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand("SELECT CustomerID, FullName, Phone, Email FROM Customers", connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int customerID = reader.GetInt32(0);
                            string fullName = reader.GetString(1);
                            string phone = reader.IsDBNull(2) ? "" : reader.GetString(2); // Обработка NULL
                            string email = reader.IsDBNull(3) ? "" : reader.GetString(3); // Обработка NULL

                            // Приводим все значения к типу string перед добавлением в ComboBox
                            customerIdComboBox.Items.Add(customerID.ToString());
                            fullNameComboBox.Items.Add(fullName);
                            phoneComboBox.Items.Add(phone);
                            emailComboBox.Items.Add(email);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading client data: {ex.Message}");
            }
        }




        // Event handlers


        private void bikeTypeComboBox_SelectedIndexChanged(object sender, EventArgs e) => GetCurrentBikeID();
        private void bikeColorCombobox_SelectedIndexChanged(object sender, EventArgs e) => GetCurrentBikeID();
        private void bikeSizeComboBox_SelectedIndexChanged(object sender, EventArgs e) => GetCurrentBikeID();
        private void madeInComboBox_SelectedIndexChanged(object sender, EventArgs e) => GetCurrentBikeID();

        private void AddNewCustomerButton_Click(object sender, EventArgs e)
        {
            AddNewCustomer();
        }

        private void customerIdComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillComboBoxes(customerIdComboBox);
        }

        private void fullNameComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillComboBoxes(fullNameComboBox);
        }

        private void phoneComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillComboBoxes(phoneComboBox);
        }

        private void emailComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillComboBoxes(emailComboBox);
        }


        // Other methods



        private void FilterComboBox(ComboBox changedComboBox, ComboBox customerIdComboBox, ComboBox fullNameComboBox, ComboBox phoneComboBox, ComboBox emailComboBox)
        {
            string filterText = changedComboBox.Text.ToLower();

            // Определяем индекс столбца в базе данных
            int columnIndex;
            if (changedComboBox == customerIdComboBox) columnIndex = 0;
            else if (changedComboBox == fullNameComboBox) columnIndex = 1;
            else if (changedComboBox == phoneComboBox) columnIndex = 2;
            else if (changedComboBox == emailComboBox) columnIndex = 3;
            else return; // Если вызван неизвестный ComboBox, просто выходим

            // Очищаем все ComboBox'ы перед загрузкой новых данных
            customerIdComboBox.Items.Clear();
            fullNameComboBox.Items.Clear();
            phoneComboBox.Items.Clear();
            emailComboBox.Items.Clear();

            // Проверяем, что filterText не пустая строка
            if (!string.IsNullOrEmpty(filterText))
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    using (SqlCommand command = new SqlCommand("SELECT CustomerID, FullName, Phone, Email FROM Customers", connection)) // Без параметров пока
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int customerID = reader.GetInt32(0);
                                string fullName = reader.GetString(1);
                                string phone = reader.IsDBNull(2) ? "" : reader.GetString(2);
                                string email = reader.IsDBNull(3) ? "" : reader.GetString(3);

                                // Фильтруем данные по выбранному столбцу, используя Contains и ToLower
                                if ((columnIndex == 0 && customerID.ToString().ToLower().Contains(filterText)) ||
                                    (columnIndex == 1 && fullName.ToLower().Contains(filterText)) ||
                                    (columnIndex == 2 && phone.ToLower().Contains(filterText)) ||
                                    (columnIndex == 3 && email.ToLower().Contains(filterText)))
                                {
                                    customerIdComboBox.Items.Add(customerID.ToString());
                                    fullNameComboBox.Items.Add(fullName);

                                    if (!string.IsNullOrEmpty(phone)) phoneComboBox.Items.Add(phone);
                                    if (!string.IsNullOrEmpty(email)) emailComboBox.Items.Add(email);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error filtering client data: {ex.Message}");
                }
            }
        }

        private void FillComboBoxes(ComboBox changedComboBox)
        {
            if (changedComboBox.SelectedItem == null) return;

            string selectedValue = changedComboBox.SelectedItem.ToString();
            int columnIndex;

            if (changedComboBox == customerIdComboBox) columnIndex = 0;
            else if (changedComboBox == fullNameComboBox) columnIndex = 1;
            else if (changedComboBox == phoneComboBox) columnIndex = 2;
            else if (changedComboBox == emailComboBox) columnIndex = 3;
            else return;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand("SELECT CustomerID, FullName, Phone, Email FROM Customers", connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int customerID = reader.GetInt32(0);
                            string fullName = reader.GetString(1);
                            string phone = reader.IsDBNull(2) ? "" : reader.GetString(2);
                            string email = reader.IsDBNull(3) ? "" : reader.GetString(3);

                            if ((columnIndex == 0 && customerID.ToString() == selectedValue) ||
                                (columnIndex == 1 && fullName == selectedValue) ||
                                (columnIndex == 2 && phone == selectedValue) ||
                                (columnIndex == 3 && email == selectedValue))
                            {
                                customerIdComboBox.SelectedItem = customerID.ToString();
                                fullNameComboBox.SelectedItem = fullName;
                                phoneComboBox.SelectedItem = phone;
                                emailComboBox.SelectedItem = email;
                                break; // Выходим из цикла после нахождения соответствия
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error filling combo boxes: {ex.Message}");
            }
        }

        private void GetCurrentBikeID()
        {
            if (bikeTypeComboBox.SelectedItem == null || bikeColorCombobox.SelectedItem == null ||
                bikeSizeComboBox.SelectedItem == null || madeInComboBox.SelectedItem == null)
            {
                _currentBikeID = -1;
                quantityComboBox.Items.Clear();
                return;
            }

            int selectedBikeTypeID = _bikeTypes[bikeTypeComboBox.SelectedItem.ToString()];
            int selectedColorID = _bikeColors[bikeColorCombobox.SelectedItem.ToString()];
            int selectedSizeID = _bikeSizes[bikeSizeComboBox.SelectedItem.ToString()];
            int selectedSupplierID = _bikeMadeIn[madeInComboBox.SelectedItem.ToString()];

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand("SELECT BikeID FROM Bikes WHERE BikeTypeID = @BikeTypeID AND ColorID = @ColorID AND SizeID = @SizeID AND SupplierID = @SupplierID", connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@BikeTypeID", selectedBikeTypeID);
                    command.Parameters.AddWithValue("@ColorID", selectedColorID);
                    command.Parameters.AddWithValue("@SizeID", selectedSizeID);
                    command.Parameters.AddWithValue("@SupplierID", selectedSupplierID);

                    object result = command.ExecuteScalar();
                    _currentBikeID = result == null ? -1 : (int)result;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error getting BikeID: {ex.Message}");
                _currentBikeID = -1;
            }

            MessageBox.Show($"Selected Bike ID is: {_currentBikeID}");
            PopulateQuantityComboBox(_currentBikeID);
        }
        private void PopulateQuantityComboBox(int bikeID)
        {
            quantityComboBox.Items.Clear();

            if (bikeID == -1) return;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand("SELECT Quantity FROM Quantity WHERE BikeID = @BikeID", connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@BikeID", bikeID);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read() && !reader.IsDBNull(0))
                        {
                            int quantity = reader.GetInt32(0);

                            // Заполняем ComboBox числами от 1 до quantity
                            for (int i = 1; i <= quantity; i++)
                            {
                                quantityComboBox.Items.Add(i);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading quantities: {ex.Message}");
            }

            if (quantityComboBox.Items.Count > 0)
            {
                quantityComboBox.SelectedIndex = 0;
            }
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            bikeTypeComboBox.SelectedIndex = -1;
            bikeColorCombobox.SelectedIndex = -1;
            bikeSizeComboBox.SelectedIndex = -1;
            madeInComboBox.SelectedIndex = -1;
            customerIdComboBox.SelectedIndex = -1;
            fullNameComboBox.SelectedIndex = -1;
            phoneComboBox.SelectedIndex = -1;
            emailComboBox.SelectedIndex = -1;
            quantityComboBox.Text = string.Empty;
            _currentBikeID = -1;
        }

        private void saleButton_Click(object sender, EventArgs e)
        {
            if (customerIdComboBox.SelectedItem == null || quantityComboBox.SelectedItem == null)
            {
                MessageBox.Show("Please select a customer and quantity.");
                return;
            }

            string customerName = fullNameComboBox.SelectedItem.ToString();
            int bikeID = _currentBikeID;
            int quantity = (int)quantityComboBox.SelectedItem;

            if (bikeID == -1)
            {
                MessageBox.Show("No bike selected.");
                return;
            }

            decimal unitPrice = GetBikePrice(bikeID);
            OrderData order = new OrderData(customerName, bikeID, unitPrice, quantity);


            string message = $"Check the details before placing the order:\n" +
                             $"Customer: {order.CustomerName}\n" +
                             $"BikeID: {order.BikeID}\n" +
                             $"Quantity: {order.Quantity}\n" +
                             $"Unit Price: {order.UnitPrice:C}\n" +
                             $"Total Price: {order.TotalPrice:C}";

            DialogResult result = MessageBox.Show(message, "Confirm Order", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                SaveOrderToDatabase(order);
                sendTriggerIf(bikeID);
                RefreshComboBoxes();


            }
        }

        private decimal GetBikePrice(int bikeID)
        {
            decimal price = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand("SELECT SalePrice FROM Bikes WHERE BikeID = @BikeID", connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@BikeID", bikeID);
                    object result = command.ExecuteScalar();
                    price = result != null ? (decimal)result : 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving bike price: {ex.Message}");
            }
            return price;
        }

          private int GetCustomerID(string customerName)
          {
              int customerID = -1;
              using (SqlConnection connection = new SqlConnection(connectionString))
              using (SqlCommand command = new SqlCommand("SELECT CustomerID FROM Customers WHERE FullName = @FullName", connection))
              {
                  command.Parameters.AddWithValue("@FullName", customerName);
                  connection.Open();
                  object result = command.ExecuteScalar();
                  if (result != null) customerID = (int)result;
              }
              return customerID;
          }
        private void sendTriggerIf(int bikeID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Проверяем, сколько осталось велосипедов данного типа
                int remainingQuantity = 0;
                using (SqlCommand checkStock = new SqlCommand("SELECT Quantity FROM Quantity WHERE BikeID = @BikeID", connection))
                {
                    checkStock.Parameters.AddWithValue("@BikeID", bikeID);
                    object result = checkStock.ExecuteScalar();
                    remainingQuantity = result != null ? Convert.ToInt32(result) : 0;
                }

                // Если меньше 5, создаем заказ у поставщика
                if (remainingQuantity < 5)
                {
                    int supplierID = 0;
                    string supplierName = "";
                    string country = "";
                    decimal supplierPrice = 0;
                    int quantity = 20;

                    // Получаем данные о поставщике и цене велосипеда
                    using (SqlCommand getSupplier = new SqlCommand(
                        "SELECT s.SupplierID, s.SupplierName, s.Country, b.SupplierPrice " +
                        "FROM Bikes b " +
                        "JOIN Suppliers s ON b.SupplierID = s.SupplierID " +
                        "WHERE b.BikeID = @BikeID", connection))
                    {
                        getSupplier.Parameters.AddWithValue("@BikeID", bikeID);
                        using (SqlDataReader reader = getSupplier.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                supplierID = reader.GetInt32(0);
                                supplierName = reader.GetString(1);
                                country = reader.GetString(2);
                                supplierPrice = reader.GetDecimal(3);
                            }
                        }
                    }

                    if (supplierID > 0)
                    {
                        decimal totalPrice = supplierPrice * quantity;

                        // Создаем заказ поставщику
                        using (SqlCommand insertOrder = new SqlCommand(
                            "INSERT INTO SupplierOrders (SupplierOrderDate, BikeID, SupplierID, SupplierName, Country, SupplierPrice, Quantity) " +
                            "VALUES (@SupplierOrderDate, @BikeID, @SupplierID, @SupplierName, @Country, @SupplierPrice, @Quantity)", connection))
                        {
                            insertOrder.Parameters.AddWithValue("@SupplierOrderDate", DateTime.Now);
                            insertOrder.Parameters.AddWithValue("@BikeID", bikeID);
                            insertOrder.Parameters.AddWithValue("@SupplierID", supplierID);
                            insertOrder.Parameters.AddWithValue("@SupplierName", supplierName);
                            insertOrder.Parameters.AddWithValue("@Country", country);
                            insertOrder.Parameters.AddWithValue("@SupplierPrice", supplierPrice);
                            insertOrder.Parameters.AddWithValue("@Quantity", quantity);
                           // insertOrder.Parameters.AddWithValue("@TotalPrice", totalPrice);

                            insertOrder.ExecuteNonQuery();
                        }

                        MessageBox.Show($"There are less than 5 units of {bikeID} bike type in the warehouse!\n" +
                                        "A new batch of bicycles of this type is ordered from the supplier today.",
                                        "Low Stock Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void SaveOrderToDatabase(OrderData order)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    // 1. Добавляем заказ в Orders и получаем OrderID
                    int orderID;
                    using (SqlCommand cmd = new SqlCommand(
                        "INSERT INTO Orders (OrderDate, CustomerID) OUTPUT INSERTED.OrderID " +
                        "VALUES (@OrderDate, (SELECT CustomerID FROM Customers WHERE FullName = @CustomerName))", connection, transaction))
                    {
                        cmd.Parameters.AddWithValue("@OrderDate", order.OrderDate);
                        cmd.Parameters.AddWithValue("@CustomerName", order.CustomerName);
                        object result = cmd.ExecuteScalar();
                        orderID = (result != null) ? Convert.ToInt32(result) : -1;

                        if (orderID == -1)
                        {
                            throw new Exception("OrderID was not generated.");
                        }
                    }

                    // 2. Добавляем детали заказа в OrderDetails (без TotalPrice)
                    using (SqlCommand cmd = new SqlCommand(
                        "INSERT INTO OrderDetails (OrderDate, CustomerID, OrderID, BikeID, Quantity, UnitPrice) " +
                        "VALUES (@OrderDate, (SELECT CustomerID FROM Customers WHERE FullName = @CustomerName), @OrderID, @BikeID, @Quantity, @UnitPrice)", connection, transaction))
                    {
                        cmd.Parameters.AddWithValue("@OrderID", orderID);
                        cmd.Parameters.AddWithValue("@BikeID", order.BikeID);
                        cmd.Parameters.AddWithValue("@Quantity", order.Quantity > 0 ? order.Quantity : 1); // Предотвращает NULL
                        cmd.Parameters.AddWithValue("@UnitPrice", order.UnitPrice);
                        cmd.Parameters.AddWithValue("@OrderDate", order.OrderDate);
                        cmd.Parameters.AddWithValue("@CustomerName", order.CustomerName);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected == 0)
                        {
                            throw new Exception("Failed to insert OrderDetails.");
                        }
                    }

                    // 3. Обновляем количество товара
                    using (SqlCommand cmd = new SqlCommand(
                        "UPDATE Quantity SET Quantity = Quantity - @Quantity WHERE BikeID = @BikeID", connection, transaction))
                    {
                        cmd.Parameters.AddWithValue("@BikeID", order.BikeID);
                        cmd.Parameters.AddWithValue("@Quantity", order.Quantity);
                        cmd.ExecuteNonQuery();
                    }

                    transaction.Commit();

                    // Обновление DataGridView (вычисляем TotalPrice вручную)
                    decimal totalPrice = order.Quantity * order.UnitPrice;
                    orderDataGridView.Rows.Add(order.OrderDate, order.CustomerName, order.BikeID, order.UnitPrice, order.Quantity, totalPrice);

                    MessageBox.Show($"Order saved successfully!\n\nCustomer: {order.CustomerName}\nBikeID: {order.BikeID}\nTotal Price: {totalPrice:C}",
                                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show($"Error saving order: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void AddNewCustomer()
        {
            string fullName = fullNameComboBox.Text.Trim();
            string phone = phoneComboBox.Text.Trim();
            string email = emailComboBox.Text.Trim();

            if (string.IsNullOrEmpty(fullName))
            {
                MessageBox.Show("Full name cannot be empty!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!Regex.IsMatch(phone, @"\d{7,}"))
            {
                MessageBox.Show("Phone number must contain at least 7 digits!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!email.Contains("@"))
            {
                MessageBox.Show("Invalid email address!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand checkCommand = new SqlCommand("SELECT COUNT(*) FROM Customers WHERE FullName = @FullName", connection))
                {
                    checkCommand.Parameters.AddWithValue("@FullName", fullName);
                    int count = (int)checkCommand.ExecuteScalar();
                    if (count > 0)
                    {
                        MessageBox.Show("A customer with this name already exists!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                int newCustomerId = 0;

                // Вставляем нового клиента и получаем его ID
                using (SqlCommand insertCommand = new SqlCommand(
    "INSERT INTO Customers (FullName, Phone, Email) OUTPUT INSERTED.CustomerID VALUES (@FullName, @Phone, @Email)",
    connection))

                {
                    insertCommand.Parameters.AddWithValue("@FullName", fullName);
                    insertCommand.Parameters.AddWithValue("@Phone", string.IsNullOrEmpty(phone) ? (object)DBNull.Value : phone);
                    insertCommand.Parameters.AddWithValue("@Email", string.IsNullOrEmpty(email) ? (object)DBNull.Value : email);

                    object result = insertCommand.ExecuteScalar();
                    if (result != null)
                    {
                        newCustomerId = Convert.ToInt32(result);
                    }
                }

                if (newCustomerId > 0)
                {
                    MessageBox.Show("New Customer created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RefreshComboBoxes(); // Перезагружаем ComboBox

                    // Устанавливаем нового клиента в ComboBox
                    fullNameComboBox.SelectedValue = newCustomerId;
                    customerIdComboBox.SelectedValue = newCustomerId;
                }
                else
                {
                    MessageBox.Show("Failed to create new customer.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        /* private void AddNewCustomer()
         {
             string fullName = fullNameComboBox.Text.Trim();
             string phone = phoneComboBox.Text.Trim();
             string email = emailComboBox.Text.Trim();

             // Проверка, что имя не пустое
             if (string.IsNullOrEmpty(fullName))
             {
                 MessageBox.Show("Full name cannot be empty!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                 return;
             }

             // Проверка, что телефон содержит минимум 7 цифр
             if (!Regex.IsMatch(phone, @"\d{7,}"))
             {
                 MessageBox.Show("Phone number must contain at least 7 digits!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                 return;
             }

             // Проверка, что email содержит "@"
             if (!email.Contains("@"))
             {
                 MessageBox.Show("Invalid email address!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                 return;
             }

             using (SqlConnection connection = new SqlConnection(connectionString))
             {
                 connection.Open();

                 // Проверяем, существует ли клиент с таким именем
                 using (SqlCommand checkCommand = new SqlCommand("SELECT COUNT(*) FROM Customers WHERE FullName = @FullName", connection))
                 {
                     checkCommand.Parameters.AddWithValue("@FullName", fullName);
                     int count = (int)checkCommand.ExecuteScalar();
                     if (count > 0)
                     {
                         MessageBox.Show("A customer with this name already exists!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                         return;
                     }
                 }

                 // Вставляем нового клиента
                 using (SqlCommand insertCommand = new SqlCommand("INSERT INTO Customers (FullName, Phone, Email) VALUES (@FullName, @Phone, @Email)", connection))
                 {
                     insertCommand.Parameters.AddWithValue("@FullName", fullName);
                     insertCommand.Parameters.AddWithValue("@Phone", string.IsNullOrEmpty(phone) ? (object)DBNull.Value : phone);
                     insertCommand.Parameters.AddWithValue("@Email", string.IsNullOrEmpty(email) ? (object)DBNull.Value : email);

                     int rowsAffected = insertCommand.ExecuteNonQuery();
                     if (rowsAffected > 0)
                     {
                         MessageBox.Show("New Customer created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                         RefreshComboBoxes();
                     }
                     else
                     {
                         MessageBox.Show("Failed to create new customer.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                     }
                 }
             }
         }*/

        private void RefreshComboBoxes()
        {
            // Очищаем все ComboBox'ы
            fullNameComboBox.Items.Clear();
            phoneComboBox.Items.Clear();
            emailComboBox.Items.Clear();
            quantityComboBox.Items.Clear();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Обновляем данные о клиентах
                using (SqlCommand command = new SqlCommand("SELECT CustomerID, FullName, Phone, Email FROM Customers", connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int customerID = reader.GetInt32(0);
                        string fullName = reader["FullName"].ToString();
                        string phone = reader["Phone"] != DBNull.Value ? reader["Phone"].ToString() : "";
                        string email = reader["Email"] != DBNull.Value ? reader["Email"].ToString() : "";

                        if (!customerIdComboBox.Items.Contains(customerID)) customerIdComboBox.Items.Add(customerID);
                        if (!fullNameComboBox.Items.Contains(fullName)) fullNameComboBox.Items.Add(fullName);
                        if (!phoneComboBox.Items.Contains(phone) && !string.IsNullOrEmpty(phone)) phoneComboBox.Items.Add(phone);
                        if (!emailComboBox.Items.Contains(email) && !string.IsNullOrEmpty(email)) emailComboBox.Items.Add(email);
                    }
                }

                // Обновляем количество доступных велосипедов
                using (SqlCommand command = new SqlCommand("SELECT BikeID, Quantity FROM Quantity", connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    Dictionary<int, int> bikeQuantities = new Dictionary<int, int>();

                    while (reader.Read())
                    {
                        int bikeID = reader.GetInt32(0);
                        int quantity = reader.GetInt32(1);
                        bikeQuantities[bikeID] = quantity;
                    }

                    if (_currentBikeID != -1 && bikeQuantities.ContainsKey(_currentBikeID))
                    {
                        for (int i = 1; i <= bikeQuantities[_currentBikeID]; i++)
                        {
                            quantityComboBox.Items.Add(i);
                        }
                    }
                }
            }

            // Оставляем выбор пустым, чтобы пользователь сам выбрал
            fullNameComboBox.SelectedIndex = -1;
            fullNameComboBox.Text = string.Empty;
            phoneComboBox.SelectedIndex = -1;
            phoneComboBox.Text = string.Empty;
            emailComboBox.SelectedIndex = -1;
            emailComboBox.Text = string.Empty;
            quantityComboBox.SelectedIndex = -1;
            quantityComboBox.Text = string.Empty;
        }

        private void loadOrdPerCustButton_Click(object sender, EventArgs e)
        {
            // Очищаем DataGridView перед загрузкой новых данных
            orderDataGridView.Rows.Clear();

            // Проверяем, выбран ли клиент
            if (customerIdComboBox.SelectedItem == null)
            {
                MessageBox.Show("Choose a client.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Преобразуем выбранный ID в число
            if (!int.TryParse(customerIdComboBox.SelectedItem.ToString(), out int customerID))
            {
                MessageBox.Show("An error of obtaining customer ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand(@"
            SELECT o.OrderDate, c.FullName, b.BikeID, od.UnitPrice, od.Quantity, (od.Quantity * od.UnitPrice) AS TotalPrice
            FROM Orders o
            JOIN Customers c ON o.CustomerID = c.CustomerID
            JOIN OrderDetails od ON o.OrderID = od.OrderID
            JOIN Bikes b ON od.BikeID = b.BikeID
            WHERE c.CustomerID = @CustomerID", connection))
                {
                    command.Parameters.AddWithValue("@CustomerID", customerID);

                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            orderDataGridView.Rows.Add(
                                reader.GetDateTime(0).ToShortDateString(), // OrderDate
                                reader.GetString(1), // CustomerName
                                reader.GetInt32(2), // BikeID
                                reader.GetDecimal(3), // UnitPrice
                                reader.GetInt32(4), // Quantity
                                reader.GetDecimal(5) // TotalPrice
                            );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Order loading error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void clearDataGridButton_Click(object sender, EventArgs e)
        {
            orderDataGridView.Rows.Clear();
        }

        private void viewOrdersButton_Click(object sender, EventArgs e)
        {
            // Очищаем DataGridView перед загрузкой новых данных
            orderDataGridView.Rows.Clear();

            // Получаем выбранные даты
            DateTime fromDate = fromDatePicker.Value;
            DateTime toDate = toDatePicker.Value;

            // Проверяем корректность дат
            if (fromDate > toDate)
            {
                MessageBox.Show("The initial date cannot be final date.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand(@"
            SELECT o.OrderDate, c.FullName, b.BikeID, od.UnitPrice, od.Quantity, (od.Quantity * od.UnitPrice) AS TotalPrice
            FROM Orders o
            JOIN Customers c ON o.CustomerID = c.CustomerID
            JOIN OrderDetails od ON o.OrderID = od.OrderID
            JOIN Bikes b ON od.BikeID = b.BikeID
            WHERE o.OrderDate BETWEEN @FromDate AND @ToDate", connection))
                {
                    command.Parameters.AddWithValue("@FromDate", fromDate);
                    command.Parameters.AddWithValue("@ToDate", toDate);

                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            orderDataGridView.Rows.Add(
                                reader.GetDateTime(0).ToShortDateString(), // OrderDate
                                reader.GetString(1), // CustomerName
                                reader.GetInt32(2), // BikeID
                                reader.GetDecimal(3), // UnitPrice
                                reader.GetInt32(4), // Quantity
                                reader.GetDecimal(5) // TotalPrice
                            );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Order loading error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}



