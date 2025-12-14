namespace VEHICLE_RENTAL_MANAGMENT_SYSTEM_tester
{
    class Program
    {
        // taking static instance of rental manager to be used in all methods
        static RentalManager RM = new RentalManager();

        //creating some sample data to test the system
        static void InitializeSampleData()
        {
            var bike1 = new Motorbike("BIKE002", "Yamaha R1", 300);
            RM.AddVehicle(bike1);

            var car1 = new Car("CAR001", "Toyota Corolla", 150);
            RM.AddVehicle(car1);

            var customer = new Customer("CUST001", "John Smith", "0123456789");
            RM.AddCustomer(customer);
        }

        ////////////////create method to wait for enter key press to return to main menu
        public static void WaitForEnter()
        {
            Console.Write("\nPress Enter to return to main menu");
            while (true)//infinite loop to keep waiting 
            {
                //read key from user
                ConsoleKeyInfo key = Console.ReadKey();
                if (key.Key == ConsoleKey.Enter)//check for enter key only
                {
                    Console.WriteLine();
                    return;// exit the method
                }
            }
        }

        //////////create method to read input to enable  cancel operation at any time
        public static string GetInput(string output)
        {
            Console.Write(output);
            string input = Console.ReadLine();

            // Check if user wants to cancel
            if (input == "cancel")
            {
                Console.WriteLine("\nOperation cancelled!");
                return "canceled";
            }

            return input;
        }

        static void AddVehicle()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("---------- Adding Vehicle! ----------");
            Console.ResetColor();
            Console.WriteLine("Choose vehicle type:");
            Console.WriteLine("1. Car");
            Console.WriteLine("2. Motorbike");
            Console.Write("Enter your choice (1 or 2): ");
            string typeChoice;
            while (true)
            {
                typeChoice = GetInput("");
                if (typeChoice == "canceled") return;
                else if (string.IsNullOrWhiteSpace(typeChoice))
                {
                    Console.WriteLine("Cannot be empty!");
                    continue;
                }
                else if (typeChoice != "1" && typeChoice != "2")
                {
                    Console.WriteLine("Invalid choice!");
                    continue;
                }
                else
                {
                    break; // Valid choice entered, exit the loop
                }
            }
            //take vehicle details
            string Id;
            while (true)
            {
                Id = GetInput("Enter Vehicle ID: ");

                if (string.IsNullOrWhiteSpace(Id))
                {
                    Console.WriteLine("Cannot be empty!");
                    continue;
                }
                else if (Id == "canceled") return;
                else
                {
                    break; // Valid ID entered, exit the loop
                }
            }

            string model;
            while (true)
            {
                model = GetInput("Enter Vehicle Model: ");

                if (string.IsNullOrWhiteSpace(model))
                {
                    Console.WriteLine("Cannot be empty!");
                    continue;
                }
                else if (model == "canceled") return;
                else
                {
                    break; // Valid model entered, exit the loop
                }
            }

            decimal price;  // to hold valid price input
            //check for valid price input
            while (true)
            {
                string priceinput = GetInput("Enter Rent Price per Day: ");
                if (string.IsNullOrWhiteSpace(priceinput))
                {
                    Console.WriteLine("Cannot be empty!");
                    continue;
                }
                else if (priceinput == "canceled") return;
                else if (decimal.TryParse(priceinput, out price)) //using try parse to avoid exception 
                {
                    break; //if true exit the while loop
                }
                else
                {
                    Console.WriteLine("Invalid price entered! Please try again.");
                }
            }

            if (typeChoice == "1")
            {
                //create new car object
                Vehicle newVehicle = new Car(Id, model, price);
                RM.AddVehicle(newVehicle);//store it in rental manager
                Console.WriteLine($"\n{newVehicle.GetVehicleType()} {newVehicle.GetModel()} (ID: {newVehicle.GetVehicleID()}) added successfully!");
            }
            else if (typeChoice == "2")
            {
                Vehicle newVehicle = new Motorbike(Id, model, price);
                RM.AddVehicle(newVehicle);
                Console.WriteLine($"\n{newVehicle.GetVehicleType()} {newVehicle.GetModel()} (ID: {newVehicle.GetVehicleID()}) added successfully!");

            }
            WaitForEnter(); // to wait for enter after successful addition
        }

        static void AddCustomer()
        {
            Console.Clear();
            //take customer details
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("---------- Adding A Customer! ----------");
            Console.ResetColor();

            string customerId;
            while (true)
            {
                customerId = GetInput("Enter Customer ID: ");
                if (customerId == "canceled") return;
                else if (string.IsNullOrWhiteSpace(customerId))
                {
                    Console.WriteLine("Cannot be empty!");
                    continue;
                }
                else
                {
                    break; // Valid ID entered, exit the loop
                }
            }

            string name;
            while (true)
            {
                name = GetInput("Enter Customer Name: ");
                if (name == "canceled") return;
                else if (string.IsNullOrWhiteSpace(name))
                {
                    Console.WriteLine("Cannot be empty!");
                    continue;
                }
                else
                {
                    break; // Valid name entered, exit the loop
                }
            }

            string phone;
            while (true)
            {
                phone = GetInput("Enter Phone Number: ");
                if (phone == "canceled") return;
                else if (string.IsNullOrWhiteSpace(phone))
                {
                    Console.WriteLine("Cannot be empty!");
                    continue;
                }
                else
                {
                    break; // Valid phone entered, exit the loop
                }
            }

            Customer newCustomer = new Customer(customerId, name, phone);
            RM.AddCustomer(newCustomer);
            //Console.WriteLine($"\nCustomer {newCustomer.GetCustomerName()} added successfully!");
            WaitForEnter(); // to wait for enter after successful addition
        }

        static void RentVehicle()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("---------- Rent A Vehicle! ----------");
            Console.ResetColor();

            string customerId;
            while (true)
            {
                customerId = GetInput("Enter Customer ID: ");
                if (string.IsNullOrWhiteSpace(customerId))
                {
                    Console.WriteLine("Cannot be empty!");
                    continue;
                }
                else if (customerId == "canceled") return;
                else
                {
                    break; // Valid ID entered, exit the loop
                }
            }

            string vehicleId;
            while (true)
            {
                vehicleId = GetInput("Enter Vehicle ID: ");
                if (string.IsNullOrWhiteSpace(vehicleId))
                {
                    Console.WriteLine("Cannot be empty!");
                    continue;
                }
                else if (vehicleId == "canceled") return;
                else
                {
                    break; // Valid ID entered, exit the loop
                }
            }

            int days;// to hold valid days input
                     //check for valid days input
            while (true)
            {
                string daysInput = GetInput("Enter Rental Days: ");
                if (string.IsNullOrWhiteSpace(daysInput))
                {
                    Console.WriteLine("Cannot be empty!");
                    continue;
                }
                else if (daysInput == "canceled") return;
                else if (!int.TryParse(daysInput, out days) || days <= 0)
                {
                    Console.WriteLine("Invalid number of days! please try again");
                    continue;
                }
                break;// if valid days exit the while loop
            }

            Console.WriteLine("\nChecking availability...");
            if (!RM.CheckVehicleAvailability(vehicleId))
            {
                Console.WriteLine("Vehicle is not available!");
                WaitForEnter();
                return;
            }

            Console.WriteLine("Vehicle is available!");
            Vehicle vehicle = RM.FindVehicleById(vehicleId);
            Customer customer = RM.FindCustomerById(customerId);
            Rental newrent = new Rental(customer, vehicle, days);
            decimal totalPrice = newrent.GetTotalPrice();

            Console.WriteLine("\nRental Summary:");
            Console.WriteLine(newrent.GetRentalSummary());

            Console.Write("\nConfirm Rental? (y/n): ");
            string confirm = Console.ReadLine();

            if (confirm.ToLower() == "y")
            {
                RM.RentVehicle(customerId, vehicleId, days);
                //Console.WriteLine("\nVehicle rented successfully!");
            }
            else
            {
                Console.WriteLine("Rental cancelled.");
            }
            WaitForEnter();
        }

        static void ReturnVehicle()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("---------- Returning A Vehicle! ----------");
            Console.ResetColor();

            string vehicleId = GetInput("Enter Vehicle ID: ");
            if (vehicleId == "canceled") return;
            if (string.IsNullOrWhiteSpace(vehicleId))
            {
                Console.WriteLine("Cannot be empty!");
                WaitForEnter();
                return;
            }

            Vehicle vehicle = RM.FindVehicleById(vehicleId);
            if (vehicle == null)
            {
                Console.WriteLine("Vehicle not found!");
                WaitForEnter();
                return;
            }

            RM.ReturnVehicle(vehicleId);
            //Console.WriteLine($"\nVehicle {vehicle.GetModel()} returned successfully!");
            WaitForEnter();
        }

        static void ViewAvailableVehicles()
        {
            Console.Clear();
            RM.ViewAvailableVehicles();
            WaitForEnter();
        }

        static void ViewRentalHistory()
        {
            Console.Clear();
            RM.ViewRentalHistory();
            WaitForEnter();
        }

        static void Main(string[] args)
        {
            InitializeSampleData();

            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("---------- Welcome to Vehicle Rental Management System! ----------");
                Console.ResetColor();
                Console.WriteLine("1. Add Vehicle");
                Console.WriteLine("2. Add Customer");
                Console.WriteLine("3. Rent Vehicle");
                Console.WriteLine("4. Return Vehicle");
                Console.WriteLine("5. View Available Vehicles");
                Console.WriteLine("6. View Rental History");
                Console.WriteLine("7. Exit");
                Console.WriteLine("======================================");
                Console.Write("Type '");
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write("cancel");
                Console.ResetColor();
                Console.WriteLine("' anytime to cancel operation");
                Console.WriteLine("======================================");
                Console.Write("Enter your choice (1-7): ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddVehicle();
                        break;
                    case "2":
                        AddCustomer();
                        break;
                    case "3":
                        RentVehicle();
                        break;
                    case "4":
                        ReturnVehicle();
                        break;
                    case "5":
                        ViewAvailableVehicles();
                        break;
                    case "6":
                        ViewRentalHistory();
                        break;
                    case "7":
                        Console.WriteLine("\nExiting Vehicle Rental Management System. Goodbye!");
                        return;
                    default:
                        Console.WriteLine("Invalid choice! Please enter a number from 1 to 7.");
                        WaitForEnter();
                        break;
                }
            }
        }
    }

    //parent class
    // ========== vehicle class ==========
    public abstract class Vehicle
    {
        // private fields
        private string Id;
        private string model;
        private decimal rentPricePerDay;
        private bool available;
        private string vehicleType;

        // constructor of base class
        public Vehicle(string Id, string model, decimal rentPricePerDay, string vehicleType)
        {
            this.Id = Id;
            this.model = model;
            this.rentPricePerDay = rentPricePerDay;
            this.vehicleType = vehicleType;
            available = true;
        }

        //desired Methods 
        public string GetVehicleID()
        {
            return Id;
        }

        public string GetModel()
        {
            return model;
        }

        public decimal GetRentPrice()
        {
            return rentPricePerDay;
        }

        public string GetVehicleType()
        {
            return vehicleType;
        }

        // Methods للتحقق والتحديث
        //check availability
        public bool Isavailable()
        {
            return available;
        }

        //change availability
        public void SetAvailability(bool status)
        {
            available = status;
        }

        //will be used in derived classes(overriden)
        public abstract decimal CalculateInsurance();
    }

    //child classes
    // ========== car class ==========
    public class Car : Vehicle
    {
        public Car(string Id, string model, decimal rentPricePerDay) : base(Id, model, rentPricePerDay, "Car")
        { }

        //abstract Method from base class
        public override decimal CalculateInsurance()
        {
            return GetRentPrice() * 0.1m;
        }
    }

    // ========== motorbike class ==========
    public class Motorbike : Vehicle
    {
        public Motorbike(string Id, string model, decimal rentPricePerDay) : base(Id, model, rentPricePerDay, "Motorbike")
        { }

        //abstract Method from base class
        public override decimal CalculateInsurance()
        {
            return GetRentPrice() * 0.15m;
        }
    }

    // ========== Customer class ==========
    public class Customer
    {
        // private fields
        private string customerId;
        private string name;
        private string phoneNumber;

        // constructor
        public Customer(string customerId, string name, string phoneNumber)
        {
            this.customerId = customerId;
            this.name = name;
            this.phoneNumber = phoneNumber;
        }

        //desired Methods
        public string GetCustomerId()
        {
            return customerId;
        }

        public string GetCustomerName()
        {
            return name;
        }

        public string GetPhoneNumber()
        {
            return phoneNumber;
        }

        public void UpdatePhoneNumber(string newPhone)
        {
            phoneNumber = newPhone;
        }
    }

    // ==========Rental class ==========
    public class Rental
    {
        //privatefields
        private static int rentalCounter = 0;
        private Customer customer;
        private Vehicle vehicle;
        private int days;
        private decimal totalPrice;
        private bool isCompleted;

        //constructor
        public Rental(Customer customer, Vehicle vehicle, int Days)
        {
            rentalCounter++;
            this.customer = customer;
            this.vehicle = vehicle;
            this.days = Days;
            isCompleted = false;
            CalculateTotalPrice();
        }

        // desired methods
        public static Rental CreateRental(Customer customer, Vehicle vehicle, int days)
        {
            Rental newrent = new Rental(customer, vehicle, days);
            return newrent;
        }

        public void CalculateTotalPrice()
        {
            decimal rentalCost = vehicle.GetRentPrice() * days;
            decimal insuranceCost = vehicle.CalculateInsurance() * days;
            totalPrice = rentalCost + insuranceCost;
        }

        //after rental is completed
        public void CompleteRental()
        {
            isCompleted = true;
            vehicle.SetAvailability(true);
        }

        public string GetRentalSummary()
        {
            //ternary operator for updating rental status
            string rentalstatus = isCompleted ? "Completed" : "Active";
            return $"Customer: {customer.GetCustomerName()} - Vehicle: {vehicle.GetModel()} - Days: {days} - Total: {totalPrice} - Status: {rentalstatus}";
        }

        // Methods for getting private fields
        public Customer GetCustomer()
        {
            return customer;
        }

        public Vehicle GetVehicle()
        {
            return vehicle;
        }

        public int GetDays()
        {
            return days;
        }

        public decimal GetTotalPrice()
        {
            return totalPrice;
        }

        public bool GetIsCompleted()
        {
            return isCompleted;
        }
    }

    // ==========RentalManager class   ==========
    public class RentalManager
    {
        //defining constant counters for arrays to managing data and assuming max limits =100
        private const int MAX_VEHICLES = 100;
        private const int MAX_CUSTOMERS = 100;
        private const int MAX_RENTALS = 100;

        // declare arrays to store data for manage data
        private Vehicle[] vehicles;
        private Customer[] customers;
        private Rental[] rentals;

        //counters to track current counters 
        private int vehicleCount;
        private int customerCount;
        private int rentalCount;

        //default constructor
        public RentalManager()
        {
            // initializing arrays and counters
            vehicles = new Vehicle[MAX_VEHICLES];
            customers = new Customer[MAX_CUSTOMERS];
            rentals = new Rental[MAX_RENTALS];

            vehicleCount = 0;
            customerCount = 0;
            rentalCount = 0;
        }

        //desired Methods 
        // Methods for managing vehicles
        public void AddVehicle(Vehicle vehicle)
        {
            //check if there is space in vehicle array
            if (vehicleCount < MAX_VEHICLES)
            {
                //store vehicle to the array of the system
                vehicles[vehicleCount] = vehicle;
                vehicleCount++;
                Console.WriteLine($"Vehicle added successfully! ");
            }
            else
            {
                Console.WriteLine("Cannot add more vehicles. system is full!");
            }
        }

        // Methods for managing customers
        public void AddCustomer(Customer customer)
        {
            //check if there is space in customer array
            if (customerCount < MAX_CUSTOMERS)
            {
                //store customer to the array of the system
                customers[customerCount] = customer;
                customerCount++;
                Console.WriteLine($"Customer added successfully!");
            }
            else
            {
                Console.WriteLine("Cannot add more customers. system is full!");
            }
        }

        // Methods for managing rentals
        public void RentVehicle(string customerId, string vehicleId, int days)
        {
            //check if customer and vehicle exist in the system
            Customer customer = FindCustomerById(customerId);
            Vehicle vehicle = FindVehicleById(vehicleId);

            //if not found
            if (customer == null || vehicle == null)
            {
                Console.WriteLine("Customer or Vehicle not found!");
                return;
            }

            //check if there is space in rental array
            if (rentalCount < MAX_RENTALS)
            {
                //create new rental and store it to the array of the system
                Rental newrental = Rental.CreateRental(customer, vehicle, days);
                rentals[rentalCount] = newrental;
                rentalCount++;

                //update vehicle availability
                vehicle.SetAvailability(false);
                Console.WriteLine($"Rental created successfully! ");
            }
            else
            {
                Console.WriteLine("Cannot create more rentals. system is full!");
            }
        }

        public void ReturnVehicle(string vehicleId)
        {
            //check for active rental by vehicle id
            for (int i = 0; i < rentalCount; i++)
            {
                if (rentals[i] != null &&
                    rentals[i].GetVehicle().GetVehicleID() == vehicleId &&
                    !rentals[i].GetIsCompleted())
                {
                    rentals[i].CompleteRental();
                    Console.WriteLine($"Vehicle  returned successfully!");
                    return;
                }
            }
            Console.WriteLine("No active rental found for this vehicle!");
        }

        // Methods للعرض
        public void ViewAvailableVehicles()
        {
            Console.WriteLine("\nAvailable Vehicles List:");
            Console.WriteLine("========================");

            bool found = false; //initialize found flag 
            for (int i = 0; i < vehicleCount; i++)
            {
                //check every vehicle existance and availability
                if (vehicles[i] != null && vehicles[i].Isavailable())
                {
                    Console.WriteLine($"{i + 1}. {vehicles[i].GetVehicleType()} - {vehicles[i].GetModel()} - ID: {vehicles[i].GetVehicleID()} - Price/Day: {vehicles[i].GetRentPrice():C}");
                    found = true;//change found to true if at least one vehicle is available
                }
            }

            if (!found)
            {
                Console.WriteLine("No vehicles available at the moment.");
            }
            Console.WriteLine("========================");
        }

        public void ViewRentalHistory()
        {
            Console.WriteLine("\nRental History:");
            Console.WriteLine("================");

            if (rentalCount == 0)
            {
                Console.WriteLine("No rentals found.");
            }
            else
            {
                //check each rental in the array
                for (int i = 0; i < rentalCount; i++)
                {
                    if (rentals[i] != null)
                    {
                        Console.WriteLine($"{i + 1}. {rentals[i].GetRentalSummary()}");
                    }
                }
            }
            Console.WriteLine("================");
        }

        // Methods للبحث
        public Vehicle FindVehicleById(string vehicleId)
        {
            // check each vehicle in the array
            for (int i = 0; i < vehicleCount; i++)
            {
                //using logical AND to avoid null reference exception
                if (vehicles[i] != null && vehicles[i].GetVehicleID() == vehicleId)
                {
                    return vehicles[i];
                }
            }
            return null;
        }

        public Customer FindCustomerById(string customerId)
        {
            // check each customer in the array
            for (int i = 0; i < customerCount; i++)
            {
                if (customers[i] != null && customers[i].GetCustomerId() == customerId)
                {
                    return customers[i];
                }
            }
            return null;
        }

        public bool CheckVehicleAvailability(string vehicleId)
        {
            Vehicle vehicle = FindVehicleById(vehicleId);
            return vehicle != null && vehicle.Isavailable();
        }
    }
}
