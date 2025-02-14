using Microsoft.Data.SqlClient;
using System.Data;
using System.Text.RegularExpressions;

namespace bikestoreTornado
{
    public partial class bikestorTornado : Form
    {
        // Connection string to establish a connection to the SQL Server database
        private readonly string connectionString = "Server=DESKTOP-O8UJAOD;Database=Bikestore;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True;";

        // SQL connection object
        private SqlConnection _connection;

        // Constructor for the bikestorTornado class
        public bikestorTornado()
        {
            // Initialize the UI components of the Windows Forms application
            InitializeComponent();

            // Create a new SQL connection using the connection string
            _connection = new SqlConnection(connectionString);

            try
            {
                // Attempt to open the database connection
                _connection.Open();

                // Notify the user that the connection was successful
                MessageBox.Show("Connection successful!");
            }
            catch (Exception ex)
            {
                // Display an error message if the connection fails
                MessageBox.Show($"Failed to connect to database: {ex.Message}");
            }

            // Load bike types into the corresponding ComboBox
            LoadComboBoxBikeData("SELECT BikeTypeID, TypeName FROM BikeTypes", bikeTypeComboBox, _bikeTypes, "bike types");

            // Load bike colors into the corresponding ComboBox
            LoadComboBoxBikeData("SELECT ColorID, ColorName FROM Colors", bikeColorCombobox, _bikeColors, "bike colors");

            // Load bike sizes into the corresponding ComboBox
            LoadComboBoxBikeData("SELECT SizeID, SizeDescription FROM Sizes", bikeSizeComboBox, _bikeSizes, "bike sizes");

            // Load supplier countries into the corresponding ComboBox
            LoadComboBoxBikeData("SELECT SupplierID, Country FROM Suppliers", madeInComboBox, _bikeMadeIn, "bike countries");

            // Retrieve the current bike ID
            GetCurrentBikeID();

            // Load client data into the UI
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
        //
        // Variable initialization
        //

        private Dictionary<string, int> _bikeTypes = new Dictionary<string, int>(); // Dictionary to store BikeTypeID
        private Dictionary<string, int> _bikeColors = new Dictionary<string, int>(); // Dictionary to store ColorID
        private Dictionary<string, int> _bikeSizes = new Dictionary<string, int>(); // Dictionary to store SizeID
        private Dictionary<string, int> _bikeMadeIn = new Dictionary<string, int>(); // Dictionary to store SupplierID
        private int _currentBikeID; // Variable to store the current BikeID

        //
        // Methods for loading data from the database
        //

        private void LoadComboBoxBikeData(string query, ComboBox comboBox, Dictionary<string, int> dictionary, string dataName)
        {
            // Clear existing items in the ComboBox
            comboBox.Items.Clear();

            // Clear existing data in the dictionary
            dictionary.Clear();

            try
            {
                // Create a new SQL connection within a using block to ensure proper disposal
                using (SqlConnection connection = new SqlConnection(connectionString))
                // Create a SQL command using the provided query
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Open the database connection
                    connection.Open();

                    // Execute the query and retrieve results using a SqlDataReader
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Iterate through the result set
                        while (reader.Read())
                        {
                            // Read the first column as an integer (ID)
                            int id = reader.GetInt32(0);

                            // Read the second column as a string (Name)
                            string name = reader.GetString(1);

                            // Add the retrieved name to the ComboBox
                            comboBox.Items.Add(name);

                            // Store the name and its corresponding ID in the dictionary
                            dictionary.Add(name, id);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Display an error message if the data loading fails
                MessageBox.Show($"Error loading {dataName}: {ex.Message}");
            }
        }


        // Method to load customer data into multiple ComboBoxes
        private void LoadComboBoxClientData()
        {
            try
            {
                // Create a new SQL connection within a using block to ensure proper disposal
                using (SqlConnection connection = new SqlConnection(connectionString))
                // Create a SQL command to select customer data
                using (SqlCommand command = new SqlCommand("SELECT CustomerID, FullName, Phone, Email FROM Customers", connection))
                {
                    // Open the database connection
                    connection.Open();

                    // Execute the query and retrieve results using a SqlDataReader
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Iterate through the result set
                        while (reader.Read())
                        {
                            // Read CustomerID as an integer
                            int customerID = reader.GetInt32(0);

                            // Read FullName as a string
                            string fullName = reader.GetString(1);

                            // Handle possible NULL values for Phone and Email
                            string phone = reader.IsDBNull(2) ? "" : reader.GetString(2);
                            string email = reader.IsDBNull(3) ? "" : reader.GetString(3);

                            // Convert CustomerID to string and add it to the corresponding ComboBox
                            customerIdComboBox.Items.Add(customerID.ToString());

                            // Add FullName to the corresponding ComboBox
                            fullNameComboBox.Items.Add(fullName);

                            // Add Phone number to the corresponding ComboBox
                            phoneComboBox.Items.Add(phone);

                            // Add Email to the corresponding ComboBox
                            emailComboBox.Items.Add(email);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Display an error message if the data loading fails
                MessageBox.Show($"Error loading client data: {ex.Message}");
            }
        }

        //
        //
        // Event handlers
        //
        // These event handlers trigger GetCurrentBikeID() whenever the selected item in the respective ComboBox changes
        private void bikeTypeComboBox_SelectedIndexChanged(object sender, EventArgs e) => GetCurrentBikeID();
        private void bikeColorCombobox_SelectedIndexChanged(object sender, EventArgs e) => GetCurrentBikeID();
        private void bikeSizeComboBox_SelectedIndexChanged(object sender, EventArgs e) => GetCurrentBikeID();
        private void madeInComboBox_SelectedIndexChanged(object sender, EventArgs e) => GetCurrentBikeID();

        // This event handler triggers the AddNewCustomer() method when the "Add New Customer" button is clicked
        private void AddNewCustomerButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("To сreate a new customer, please fill in the Full Name, Phone" +
                " and Email fields, and then click the \"Create Customer\" button. ");
            AddNewCustomer();
        }

        // These event handlers trigger FillComboBoxes() when a selection is made in any of the customer-related ComboBoxes
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

        // Method to handle the clearing of form fields when the clear button is clicked
        private void clearButton_Click(object sender, EventArgs e)
        {
            // Reset all ComboBoxes to their default state (no selection)
            bikeTypeComboBox.SelectedIndex = -1;
            bikeColorCombobox.SelectedIndex = -1;
            bikeSizeComboBox.SelectedIndex = -1;
            madeInComboBox.SelectedIndex = -1;
            customerIdComboBox.SelectedIndex = -1;
            fullNameComboBox.SelectedIndex = -1;
            phoneComboBox.SelectedIndex = -1;
            emailComboBox.SelectedIndex = -1;

            // Clear the text in the quantity ComboBox
            quantityComboBox.Text = string.Empty;

            // Reset the current Bike ID to -1 (indicating no selected bike)
            _currentBikeID = -1;
        }

        private void clearDataGridButton_Click(object sender, EventArgs e)
        {
            orderDataGridView.Rows.Clear();
        }

        // Method to handle the sale button click event
        private void saleButton_Click(object sender, EventArgs e)
        {
            // Check if a customer and quantity have been selected
            if (customerIdComboBox.SelectedItem == null || quantityComboBox.SelectedItem == null)
            {
                // Display a message if either is missing
                MessageBox.Show("Please select a customer and quantity.");
                return;
            }

            // Get the selected customer name and the current bike ID
            string customerName = fullNameComboBox.SelectedItem.ToString();
            int bikeID = _currentBikeID;
            int quantity = (int)quantityComboBox.SelectedItem;

            // Check if a bike has been selected
            if (bikeID == -1)
            {
                MessageBox.Show("No bike selected.");
                return;
            }

            // Get the unit price of the selected bike
            decimal unitPrice = GetBikePrice(bikeID);

            // Create an order object with the customer, bike, and quantity details
            OrderData order = new OrderData(customerName, bikeID, unitPrice, quantity);

            // Build a confirmation message to show to the user before placing the order
            string message = $"Check the details before placing the order:\n" +
                             $"Customer: {order.CustomerName}\n" +
                             $"BikeID: {order.BikeID}\n" +
                             $"Quantity: {order.Quantity}\n" +
                             $"Unit Price: {order.UnitPrice:C}\n" +
                             $"Total Price: {order.TotalPrice:C}";

            // Show a confirmation dialog to the user
            DialogResult result = MessageBox.Show(message, "Confirm Order", MessageBoxButtons.OKCancel);

            // If the user clicks "OK", save the order to the database and proceed
            if (result == DialogResult.OK)
            {
                SaveOrderToDatabase(order); // Save the order
                sendTriggerIf(bikeID); // Check if restocking is needed and send a trigger
                RefreshComboBoxes(); // Refresh the ComboBoxes to reflect the new state
            }
        }

        private void loadOrdPerCustButton_Click(object sender, EventArgs e)
        {
            // Clear DataGridView before loading new data
            orderDataGridView.Rows.Clear();

            // Check if a customer is selected
            if (customerIdComboBox.SelectedItem == null)
            {
                MessageBox.Show("Choose a client.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Convert the selected CustomerID to an integer
            if (!int.TryParse(customerIdComboBox.SelectedItem.ToString(), out int customerID))
            {
                MessageBox.Show("An error occurred while obtaining the customer ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    // Set the parameter for CustomerID
                    command.Parameters.AddWithValue("@CustomerID", customerID);

                    // Open database connection
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Read data and add it to DataGridView
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



        //
        // Other methods
        //



        // Method to fill all related ComboBoxes based on the selected value in one of them
        private void FillComboBoxes(ComboBox changedComboBox)
        {
            // If no item is selected, exit the method
            if (changedComboBox.SelectedItem == null) return;

            // Retrieve the selected value as a string
            string selectedValue = changedComboBox.SelectedItem.ToString();

            // Determine the column index based on the ComboBox that triggered the event
            int columnIndex;
            if (changedComboBox == customerIdComboBox) columnIndex = 0;
            else if (changedComboBox == fullNameComboBox) columnIndex = 1;
            else if (changedComboBox == phoneComboBox) columnIndex = 2;
            else if (changedComboBox == emailComboBox) columnIndex = 3;
            else return; // Exit if an unknown ComboBox is used

            try
            {
                // Create a new SQL connection within a using block to ensure proper disposal
                using (SqlConnection connection = new SqlConnection(connectionString))
                // Create a SQL command to select all customers
                using (SqlCommand command = new SqlCommand("SELECT CustomerID, FullName, Phone, Email FROM Customers", connection))
                {
                    // Open the database connection
                    connection.Open();

                    // Execute the query and retrieve results using a SqlDataReader
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Iterate through the result set
                        while (reader.Read())
                        {
                            // Read CustomerID as an integer
                            int customerID = reader.GetInt32(0);

                            // Read FullName as a string
                            string fullName = reader.GetString(1);

                            // Handle possible NULL values for Phone and Email
                            string phone = reader.IsDBNull(2) ? "" : reader.GetString(2);
                            string email = reader.IsDBNull(3) ? "" : reader.GetString(3);

                            // Check if the selected value matches any of the retrieved data
                            if ((columnIndex == 0 && customerID.ToString() == selectedValue) ||
                                (columnIndex == 1 && fullName == selectedValue) ||
                                (columnIndex == 2 && phone == selectedValue) ||
                                (columnIndex == 3 && email == selectedValue))
                            {
                                // Update all ComboBoxes with the corresponding values
                                customerIdComboBox.SelectedItem = customerID.ToString();
                                fullNameComboBox.SelectedItem = fullName;
                                phoneComboBox.SelectedItem = phone;
                                emailComboBox.SelectedItem = email;
                                break; // Exit the loop after finding a match
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Display an error message if an exception occurs
                MessageBox.Show($"Error filling combo boxes: {ex.Message}");
            }
        }


        // Method to get the current Bike ID based on selected values in the ComboBoxes
        private void GetCurrentBikeID()
        {
            // Check if any of the ComboBoxes are not selected, if so, reset the Bike ID and clear the quantity ComboBox
            if (bikeTypeComboBox.SelectedItem == null || bikeColorCombobox.SelectedItem == null ||
                bikeSizeComboBox.SelectedItem == null || madeInComboBox.SelectedItem == null)
            {
                _currentBikeID = -1; // Reset the Bike ID to -1 indicating no selection
                quantityComboBox.Items.Clear(); // Clear the quantity ComboBox
                return; // Exit the method since the selection is incomplete
            }

            // Retrieve the selected IDs from the ComboBoxes by looking them up in the dictionaries
            int selectedBikeTypeID = _bikeTypes[bikeTypeComboBox.SelectedItem.ToString()];
            int selectedColorID = _bikeColors[bikeColorCombobox.SelectedItem.ToString()];
            int selectedSizeID = _bikeSizes[bikeSizeComboBox.SelectedItem.ToString()];
            int selectedSupplierID = _bikeMadeIn[madeInComboBox.SelectedItem.ToString()];

            try
            {
                // Create a new SQL connection within a using block to ensure proper disposal
                using (SqlConnection connection = new SqlConnection(connectionString))
                // Create a SQL command to retrieve the BikeID based on the selected parameters
                using (SqlCommand command = new SqlCommand("SELECT BikeID FROM Bikes WHERE BikeTypeID = @BikeTypeID AND ColorID = @ColorID AND SizeID = @SizeID AND SupplierID = @SupplierID", connection))
                {
                    // Open the database connection
                    connection.Open();

                    // Add the parameters to the SQL command to prevent SQL injection
                    command.Parameters.AddWithValue("@BikeTypeID", selectedBikeTypeID);
                    command.Parameters.AddWithValue("@ColorID", selectedColorID);
                    command.Parameters.AddWithValue("@SizeID", selectedSizeID);
                    command.Parameters.AddWithValue("@SupplierID", selectedSupplierID);

                    // Execute the query and get the result (the BikeID)
                    object result = command.ExecuteScalar();
                    // If result is null, set the BikeID to -1 (not found), otherwise store the BikeID
                    _currentBikeID = result == null ? -1 : (int)result;
                }
            }
            catch (Exception ex)
            {
                // Display an error message if an exception occurs and reset the BikeID
                MessageBox.Show($"Error getting BikeID: {ex.Message}");
                _currentBikeID = -1;
            }

            // Display the selected BikeID in a message box
            MessageBox.Show($"Selected Bike ID is: {_currentBikeID}");
            // Call a method to populate the quantity ComboBox based on the selected Bike ID
            PopulateQuantityComboBox(_currentBikeID);
        }

        // Method to populate the quantity ComboBox based on the selected Bike ID
        private void PopulateQuantityComboBox(int bikeID)
        {
            // Clear the ComboBox before adding new items
            quantityComboBox.Items.Clear();

            // If the Bike ID is invalid (e.g., -1), exit the method
            if (bikeID == -1) return;

            try
            {
                // Create a new SQL connection within a using block to ensure proper disposal
                using (SqlConnection connection = new SqlConnection(connectionString))
                // Create a SQL command to retrieve the quantity from the 'Quantity' table based on the BikeID
                using (SqlCommand command = new SqlCommand("SELECT Quantity FROM Quantity WHERE BikeID = @BikeID", connection))
                {
                    // Open the database connection
                    connection.Open();

                    // Add the BikeID parameter to the SQL command
                    command.Parameters.AddWithValue("@BikeID", bikeID);

                    // Execute the query and read the result
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // If the reader has data and the Quantity is not NULL, retrieve the quantity value
                        if (reader.Read() && !reader.IsDBNull(0))
                        {
                            int quantity = reader.GetInt32(0);

                            // Populate the ComboBox with numbers from 1 to the retrieved quantity
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
                // Display an error message if an exception occurs
                MessageBox.Show($"Error loading quantities: {ex.Message}");
            }

            // If there are any items in the ComboBox, select the first item by default
            if (quantityComboBox.Items.Count > 0)
            {
                quantityComboBox.SelectedIndex = 0;
            }
        }


        // Method to get the price of a bike based on its BikeID
        private decimal GetBikePrice(int bikeID)
        {
            decimal price = 0;

            try
            {
                // Establish a connection to the database
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand("SELECT SalePrice FROM Bikes WHERE BikeID = @BikeID", connection))
                {
                    connection.Open(); // Open the connection
                    command.Parameters.AddWithValue("@BikeID", bikeID); // Add the BikeID parameter to the query

                    // Execute the query and retrieve the sale price
                    object result = command.ExecuteScalar();

                    // If a result is returned, cast it to a decimal, otherwise, set price to 0
                    price = result != null ? (decimal)result : 0;
                }
            }
            catch (Exception ex)
            {
                // Show an error message if the query fails
                MessageBox.Show($"Error retrieving bike price: {ex.Message}");
            }

            // Return the price of the bike
            return price;
        }

        // Method to check the stock of a specific bike and place a supplier order if necessary
        private void sendTriggerIf(int bikeID)
        {
            // Establish a connection to the database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Check the remaining stock for the given BikeID
                int remainingQuantity = 0;
                using (SqlCommand checkStock = new SqlCommand("SELECT Quantity FROM Quantity WHERE BikeID = @BikeID", connection))
                {
                    // Add the BikeID parameter to the SQL command
                    checkStock.Parameters.AddWithValue("@BikeID", bikeID);

                    // Execute the query and retrieve the quantity
                    object result = checkStock.ExecuteScalar();
                    remainingQuantity = result != null ? Convert.ToInt32(result) : 0;
                }

                // If the stock is below 5, place a new supplier order
                if (remainingQuantity < 5)
                {
                    int supplierID = 0;
                    string supplierName = "";
                    string country = "";
                    decimal supplierPrice = 0;
                    int quantity = 20; // Order 20 new bikes

                    // Retrieve supplier details and bike price
                    using (SqlCommand getSupplier = new SqlCommand(
                        "SELECT s.SupplierID, s.SupplierName, s.Country, b.SupplierPrice " +
                        "FROM Bikes b " +
                        "JOIN Suppliers s ON b.SupplierID = s.SupplierID " +
                        "WHERE b.BikeID = @BikeID", connection))
                    {
                        // Add the BikeID parameter to the SQL command
                        getSupplier.Parameters.AddWithValue("@BikeID", bikeID);

                        // Execute the query and read the results
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

                    // If a valid supplier is found, create a new supplier order
                    if (supplierID > 0)
                    {
                        decimal totalPrice = supplierPrice * quantity;

                        // Insert a new order into the SupplierOrders table
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

                            // Execute the insertion command
                            insertOrder.ExecuteNonQuery();
                        }

                        // Notify the user that a new supplier order has been placed
                        MessageBox.Show($"There are less than 5 units of {bikeID} bike type in the warehouse!\n" +
                                        "A new batch of bicycles of this type is ordered from the supplier today.",
                                        "Low Stock Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }


        // Method to save an order to the database
        private void SaveOrderToDatabase(OrderData order)
        {
            // Establish a database connection
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                // Begin a SQL transaction to ensure atomicity
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    // 1. Insert a new order into the Orders table and retrieve the generated OrderID
                    int orderID;
                    using (SqlCommand cmd = new SqlCommand(
                        "INSERT INTO Orders (OrderDate, CustomerID) OUTPUT INSERTED.OrderID " +
                        "VALUES (@OrderDate, (SELECT CustomerID FROM Customers WHERE FullName = @CustomerName))",
                        connection, transaction))
                    {
                        // Add parameters for the order date and customer name
                        cmd.Parameters.AddWithValue("@OrderDate", order.OrderDate);
                        cmd.Parameters.AddWithValue("@CustomerName", order.CustomerName);

                        // Execute the query and retrieve the generated OrderID
                        object result = cmd.ExecuteScalar();
                        orderID = (result != null) ? Convert.ToInt32(result) : -1;

                        // If OrderID was not generated, throw an error
                        if (orderID == -1)
                        {
                            throw new Exception("OrderID was not generated.");
                        }
                    }

                    // 2. Insert order details into the OrderDetails table (excluding TotalPrice)
                    using (SqlCommand cmd = new SqlCommand(
                        "INSERT INTO OrderDetails (OrderDate, CustomerID, OrderID, BikeID, Quantity, UnitPrice) " +
                        "VALUES (@OrderDate, (SELECT CustomerID FROM Customers WHERE FullName = @CustomerName), @OrderID, @BikeID, @Quantity, @UnitPrice)",
                        connection, transaction))
                    {
                        // Add parameters for the order details
                        cmd.Parameters.AddWithValue("@OrderID", orderID);
                        cmd.Parameters.AddWithValue("@BikeID", order.BikeID);
                        cmd.Parameters.AddWithValue("@Quantity", order.Quantity > 0 ? order.Quantity : 1); // Prevents NULL value
                        cmd.Parameters.AddWithValue("@UnitPrice", order.UnitPrice);
                        cmd.Parameters.AddWithValue("@OrderDate", order.OrderDate);
                        cmd.Parameters.AddWithValue("@CustomerName", order.CustomerName);

                        // Execute the query and check if any rows were affected
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected == 0)
                        {
                            throw new Exception("Failed to insert OrderDetails.");
                        }
                    }

                    // 3. Update the stock quantity in the Quantity table
                    using (SqlCommand cmd = new SqlCommand(
                        "UPDATE Quantity SET Quantity = Quantity - @Quantity WHERE BikeID = @BikeID",
                        connection, transaction))
                    {
                        // Add parameters for updating the quantity
                        cmd.Parameters.AddWithValue("@BikeID", order.BikeID);
                        cmd.Parameters.AddWithValue("@Quantity", order.Quantity);

                        // Execute the update query
                        cmd.ExecuteNonQuery();
                    }

                    // Commit the transaction if all operations are successful
                    transaction.Commit();

                    // Update the DataGridView manually, calculating TotalPrice
                    decimal totalPrice = order.Quantity * order.UnitPrice;
                    orderDataGridView.Rows.Add(order.OrderDate, order.CustomerName, order.BikeID, order.UnitPrice, order.Quantity, totalPrice);

                    // Display a success message to the user
                    MessageBox.Show($"Order saved successfully!\n\nCustomer: {order.CustomerName}\nBikeID: {order.BikeID}\nTotal Price: {totalPrice:C}",
                                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    // Rollback the transaction if an error occurs
                    transaction.Rollback();
                    MessageBox.Show($"Error saving order: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Method to add a new customer to the database
        private void AddNewCustomer()
        {
            // Retrieve and trim input values from ComboBoxes
            string fullName = fullNameComboBox.Text.Trim();
            string phone = phoneComboBox.Text.Trim();
            string email = emailComboBox.Text.Trim();

            // Check if the full name is empty
            if (string.IsNullOrEmpty(fullName))
            {
                MessageBox.Show("Full name cannot be empty!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validate phone number: must contain at least 7 digits
            if (!Regex.IsMatch(phone, @"\d{7,}"))
            {
                MessageBox.Show("Phone number must contain at least 7 digits!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validate email: must contain '@' symbol
            if (!email.Contains("@"))
            {
                MessageBox.Show("Invalid email address!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Establish a connection to the database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Check if a customer with the same full name already exists
                using (SqlCommand checkCommand = new SqlCommand(
                    "SELECT COUNT(*) FROM Customers WHERE FullName = @FullName", connection))
                {
                    checkCommand.Parameters.AddWithValue("@FullName", fullName);
                    int count = (int)checkCommand.ExecuteScalar();

                    // If the customer already exists, show a warning message
                    if (count > 0)
                    {
                        MessageBox.Show("A customer with this name already exists!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                // Insert a new customer into the Customers table
                using (SqlCommand insertCommand = new SqlCommand(
                    "INSERT INTO Customers (FullName, Phone, Email) VALUES (@FullName, @Phone, @Email)", connection))
                {
                    // Add parameters to the SQL query, handling empty values with DBNull
                    insertCommand.Parameters.AddWithValue("@FullName", fullName);
                    insertCommand.Parameters.AddWithValue("@Phone", string.IsNullOrEmpty(phone) ? (object)DBNull.Value : phone);
                    insertCommand.Parameters.AddWithValue("@Email", string.IsNullOrEmpty(email) ? (object)DBNull.Value : email);

                    // Execute the insert command and check if rows were affected
                    int rowsAffected = insertCommand.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("New Customer created successfully!" +
                            "To make a sale to this customer please go to the \\\"Customer ID\\\" field," +
                            " select the lowest number in the list, and then choose a bike.\"", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Refresh the ComboBoxes to include the newly added customer
                        RefreshComboBoxes();
                    }
                    else
                    {
                        MessageBox.Show("Failed to create new customer.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }


        // Method to refresh all ComboBoxes with updated customer and bike quantity data
        private void RefreshComboBoxes()
        {
            // Clear all ComboBoxes before populating them with new data
            fullNameComboBox.Items.Clear();
            phoneComboBox.Items.Clear();
            emailComboBox.Items.Clear();
            quantityComboBox.Items.Clear();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Retrieve customer data (CustomerID, FullName, Phone, Email) from the Customers table
                using (SqlCommand command = new SqlCommand("SELECT CustomerID, FullName, Phone, Email FROM Customers", connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int customerID = reader.GetInt32(0);
                        string fullName = reader["FullName"].ToString();
                        string phone = reader["Phone"] != DBNull.Value ? reader["Phone"].ToString() : "";
                        string email = reader["Email"] != DBNull.Value ? reader["Email"].ToString() : "";

                        // Add unique CustomerID, FullName, Phone, and Email to respective ComboBoxes
                        if (!customerIdComboBox.Items.Contains(customerID)) customerIdComboBox.Items.Add(customerID);
                        if (!fullNameComboBox.Items.Contains(fullName)) fullNameComboBox.Items.Add(fullName);
                        if (!phoneComboBox.Items.Contains(phone) && !string.IsNullOrEmpty(phone)) phoneComboBox.Items.Add(phone);
                        if (!emailComboBox.Items.Contains(email) && !string.IsNullOrEmpty(email)) emailComboBox.Items.Add(email);
                    }
                }

                // Retrieve bike quantities (BikeID, Quantity) from the Quantity table
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

                    // If a specific bike is selected (_currentBikeID is valid), populate the quantity ComboBox
                    if (_currentBikeID != -1 && bikeQuantities.ContainsKey(_currentBikeID))
                    {
                        for (int i = 1; i <= bikeQuantities[_currentBikeID]; i++)
                        {
                            quantityComboBox.Items.Add(i);
                        }
                    }
                }
            }

            // Reset selections to ensure the user selects values manually
            fullNameComboBox.SelectedIndex = -1;
            fullNameComboBox.Text = string.Empty;
            phoneComboBox.SelectedIndex = -1;
            phoneComboBox.Text = string.Empty;
            emailComboBox.SelectedIndex = -1;
            emailComboBox.Text = string.Empty;
            quantityComboBox.SelectedIndex = -1;
            quantityComboBox.Text = string.Empty;
        }

        private void viewOrdersButton_Click(object sender, EventArgs e)
        {
            // Clear DataGridView before loading new data
            orderDataGridView.Rows.Clear();

            // Get selected dates
            DateTime fromDate = fromDatePicker.Value;
            DateTime toDate = toDatePicker.Value;

            // Validate date range
            if (fromDate > toDate)
            {
                MessageBox.Show("The initial date cannot be later than the final date.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    // Set parameters for date range
                    command.Parameters.AddWithValue("@FromDate", fromDate);
                    command.Parameters.AddWithValue("@ToDate", toDate);

                    // Open database connection
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Read data and add it to DataGridView
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



