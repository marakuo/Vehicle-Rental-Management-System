using System;

namespace csharpcode
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("---------- Welcome to Vehicle Rental Management System! ----------");
            Console.ResetColor();
            ////
            RentalManager rentalManager = new RentalManager();
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("1. Add Vehicle");
                Console.WriteLine("2. Add Customer");
                Console.WriteLine("3. Rent Vehicle");
                Console.WriteLine("4. Return Vehicle");
                Console.WriteLine("5. View Available Vehicles");
                Console.WriteLine("6. View Rental History");
                Console.WriteLine("7. EXIT");
                Console.WriteLine();
                Console.Write("Enter your choice: ");
                Console.WriteLine();
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        addVehicle(rentalManager); break;
                    case "2" :
                        addCustomer(rentalManager); break;
                    case "3" :
                        addRental(rentalManager); break;
                    case "4" :
                        returnVehicle(rentalManager); break;
                    case "5":
                        rentalManager.ViewAvailableVehicles(); break;
                    case "6" :
                        rentalManager.ViewRentalHistory(); break;
                    case "7" :
                        exit = true;
                        Console.WriteLine("Exiting Vehicle Rental Management System. Goodbye!");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                        
                    
                }
                
            }
            
           


            
            

        }
        // methods used in main 
        // adding vehicle 
            static void addVehicle(RentalManager rentalManager)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("---------- Adding Vehicle! ----------");
                Console.ResetColor();
                Console.WriteLine("Choose Vehicle Type: ");
                Console.WriteLine("1. Car");
                Console.WriteLine("2. Motorbike");
                Console.Write("Enter Your choice: ");
                string typeChoice = Console.ReadLine();
            
                Console.Write("Enter Vehicle ID: ");
                string id = Console.ReadLine();
            
                Console.Write("Enter Vehicle Model: ");
                string model = Console.ReadLine();
                // rent price per day => check the validation of the input 
                Console.Write("Enter The Rent Price Per Day: ");
                if (!decimal.TryParse(Console.ReadLine(), out decimal price))
                {
                    price = 800.00m; // set as a default 
                    
                }
                // checking the typed choice 
                if (typeChoice == "1")
                {
                    Console.Write("Enter the number of seats: ");
                    int seats = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Has Bluetooth? (Y/N): ");
                    // FIXED: Convert Y/N to boolean properly
                    string bluetoothInput = Console.ReadLine().ToUpper();
                    bool hasBluetooth = (bluetoothInput == "Y" || bluetoothInput == "YES");
                    Console.Write("Has GPS? (Y/N): ");
                    // FIXED: Convert Y/N to boolean properly
                    string gpsInput = Console.ReadLine().ToUpper();
                    bool hasGPS = (gpsInput == "Y" || gpsInput == "YES");
                    Console.Write("Enter the car color: ");
                    string color = Console.ReadLine();
                    // creating a car object 
                    Car newCar = new Car(id, model, price, seats, hasBluetooth, hasGPS, color);
                    rentalManager.addVehicle(newCar);
                    Console.WriteLine($"Car {model} was added successfully");
                    
                   

                }else if (typeChoice == "2")
                {
                    Console.Write("Enter the Bike Type: ");
                    string bikeType = Console.ReadLine();
                    Console.Write("Enter the engine capacity: ");
                    int engineCapacity = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Has a storage box? (Y/N): ");
                    // FIXED: Convert Y/N to boolean properly
                    string storageInput = Console.ReadLine().ToUpper();
                    bool hasStorageBox = (storageInput == "Y" || storageInput == "YES");
                    // creating a motorbike object 
                    Motorbike newMotorBike = new Motorbike(id, model, price, engineCapacity, hasStorageBox, bikeType);
                    rentalManager.addVehicle(newMotorBike);
                    Console.WriteLine($"Motorbike of type {bikeType} was added successfully");
                    
                }
                else
                {
                    Console.WriteLine("Invalid choice! Please try again!");
                }
                

            }
            // add customer 
            static void addCustomer(RentalManager rentalManager)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("---------- Adding A Customer! ----------");
                Console.ResetColor();
                // customer data 
                Console.Write("Enter the customer ID: ");
                string customerID = Console.ReadLine();
                Console.Write("Enter the customer name: ");
                string customerName = Console.ReadLine();
                Console.Write("Enter the customer phone number: ");
                string  customerPhoneNumber = Console.ReadLine();
                
                // creating a customer object 
                Customer newCustomer = new Customer(customerID, customerName, customerPhoneNumber);
                rentalManager.addCustomer(newCustomer);
                Console.WriteLine($"Customer {customerName} was added successfully");
            }
            // add rental 
            static void addRental(RentalManager rentalManager)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("---------- Rent A Vehicle! ----------");
                Console.ResetColor();
                // details 
                Console.Write("Enter the Customer ID: ");
                string customerID = Console.ReadLine();
                Console.Write("Enter the vehicle ID: ");
                string vehicleID = Console.ReadLine();
                Console.Write("Enter Rental Days: ");
                if (!int.TryParse(Console.ReadLine(), out int days) || days <= 0)
                {
                    Console.WriteLine("Invalid number of days! Using 1 day.");
                    days = 1; // default 
                }
                Console.WriteLine("\nChecking Availability...");
                Vehicle vehicle = rentalManager.FindVehicleById(vehicleID);
                if (vehicle == null)
                {
                    Console.WriteLine("Vehicle Not Found");
                    return;
                }

                if (!vehicle.IsAvailable())
                {
                    Console.WriteLine("Vehicle Not Available");
                    return;
                }

                Customer customer = rentalManager.FindCustomerById(customerID);
                if (customer == null)
                {
                    Console.WriteLine("Customer Not Found");
                    return;
                }

                Console.WriteLine("Vehicle is Available!!");
                //
                rentalManager.RentVehicle(vehicleID, customerID, days);
                
               

            }
            // return a vehicle 
            static void returnVehicle(RentalManager rentalManager)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("---------- Returning A Vehicle! ----------");
                Console.ResetColor();
                // details 
                Console.Write("Enter the vehicle ID: ");
                string vehicleID = Console.ReadLine();
                rentalManager.ReturnVehicle(vehicleID);
               
                    
                    
            }
            
    }
    // classes 
    public class Vehicle
    {
        // protected variables 
        protected string vehicleID;
        protected string model;
        protected decimal rentPrice;
        protected bool isAvailable;
        protected string vehicleType;

        // base Constructor
        public Vehicle(string _vehicleId, string _model, decimal _rentPrice, string _vehicleType)
        {
            vehicleID = _vehicleId;
            model = _model;
            rentPrice = _rentPrice;
            isAvailable = true; // Automatically set to true
            vehicleType = _vehicleType;
        }

        public string getVehicleID() { return vehicleID; }
        public string getModel() { return model; }
        public decimal getRentPrice() { return rentPrice; }
        public string getVehicleType() { return vehicleType; }

        // will be overridden 
        public virtual decimal CalculateInsurance()
        {
            return rentPrice * 0.1m; 
        }

        public bool IsAvailable()
        {
            return isAvailable;
        }

        public void SetAvailablity(bool isAvailable)
        {
            this.isAvailable = isAvailable;
        }
        
        // display vehicle information 
        public virtual string DisplayVehicleInfo()
        {
            string availability= isAvailable ? "Yes" : "No";
            return $"ID:{vehicleID} || Model: {model} || Type: {vehicleType}  || Available: {availability}";
        }
    }
    

    // Subclass
    class Car : Vehicle
    {
        public int NumberOfSeats { get; set; }
        public bool HasBluetooth { get; set; }
        public bool HasGPS { get; set; }
        public string Color { get; set; }

        // CAR Constructor
        public Car(string id, string model, decimal price, int seats, bool bluetooth, bool gps, string color)
            : base(id, model, price, "Car") 
        {
            NumberOfSeats = seats;
            HasBluetooth = bluetooth;
            HasGPS = gps;
            Color = color;
        }
        
        // turning thr Air conditioner 
        public void TurnAC(int temperature)
        {
            Console.WriteLine("Turning AC...");
            Console.WriteLine($"Successfully Setting The AC to {temperature} degree ");
        }
        
        // connection to the phone 
        public void ConnectionToPhone()
        {
            if(HasBluetooth) Console.WriteLine("The Phone is connected Successfully");
            else Console.WriteLine($"Error this Model: {model} Doesn't support Bluetooth");
            
        }
        
        // overriding CalculateInsurance()
        public override decimal CalculateInsurance()
        {
            return rentPrice * 0.2m; // 20% insurance 
        }
    }
    
    
    // Subclass

    class Motorbike : Vehicle
    {
        public int EngineCapacity { get; set; }
        public string BikeType { get; set; }
        public bool HasStorageBox { get; set; }

        public Motorbike(string id, string model, decimal price, int engineCapacity, bool hasBox, string bikeType)
            : base(id, model, price, "Motorbike")
        {
            EngineCapacity  = engineCapacity;
            BikeType = bikeType;
            HasStorageBox = hasBox;
        }
        
        // overriding CalculateInsurance()
        public override decimal CalculateInsurance()
        {
            return rentPrice * 0.15m; // 15% insurance 
        }
        
        // ability to perform Wheelie
        
        public void PerfomWheelie()
        {
            if (EngineCapacity > 250)
            {
                Console.WriteLine("Perform wheelie!");
            }
            else
            {
                Console.WriteLine("Engine too small... cannot do a wheelie safely.");
            }
        }
        
        
    }
    // customer class
    class Customer
    {
        protected string customerID;
        protected string customerName;
        protected string phoneNumber;

        public Customer(string customerID, string customerName, string phoneNumber)
        {
            this.customerID = customerID;
            this.customerName = customerName;
            this.phoneNumber = phoneNumber;
            
        }
        
        // get id 
        public string getCustomerID() { return customerID; }
        public string getCustomerName() { return customerName; }
        public string getPhoneNumber() { return phoneNumber; }
        // update phone number 

        public void updatePhoneNumber(string newPhoneNumber)
        {
            phoneNumber = newPhoneNumber;
        }
    }
    
    // rental class

    class Rental
    {
       
       private Customer customer; // the entire customer object
       private Vehicle vehicle; // the entire vehicle object
       private decimal totalPrice;
       private int rentaldays;
       private bool isCompleted;

       public void CreateRental (Customer customer, Vehicle vehicle,  int rentaldays)
       {
           this.customer = customer;
           this.vehicle = vehicle;
           this.rentaldays = rentaldays;
           isCompleted = false; // the customer returned the vehicle after the rental ends
           // FIXED: Mark vehicle as unavailable when rented
           vehicle.SetAvailablity(false);
           
       }

       public bool IsCompleted()
       {
           return isCompleted;
       }

       public Vehicle getVehicle()
       {
           return vehicle;
       }

       public decimal CalculateTotalPrice()
       {
           decimal vehicleCost = vehicle.getRentPrice() * rentaldays;
           decimal insuranceCost = vehicle.CalculateInsurance() * rentaldays;
           return insuranceCost + vehicleCost;
       }
       
       // mark rental as completed 
       public void CompleteRental()
       {
           isCompleted = true;
           // FIXED: Mark vehicle as available when returned
           vehicle.SetAvailablity(true);
           
       }
       // rental summary

       public void getRentalSummary()
       {
           string status =  isCompleted ? "Completed" : "Not Completed" ;
           Console.WriteLine("=== Rental Summary ===");
           Console.WriteLine($"Rental Status: {status} and Rental Date {DateTime.Now.ToString()}");
           Console.WriteLine($"Rental Duration: {rentaldays} Days");
           Console.WriteLine("=== Customer Data ===");
           Console.WriteLine($"Customer Name: {customer.getCustomerName()}" +
                             $"Customer ID: {customer.getCustomerID()}" +
                             $"Customer Phone: {customer.getPhoneNumber()}");
           Console.WriteLine("=== Vehicle details ===");
           Console.WriteLine($"Vehicle Type: {vehicle.getVehicleType()}" +
                             $"Vehicle ID: {vehicle.getVehicleID()}" +
                             $"Vehicle Model: {vehicle.getModel()}");
           Console.WriteLine($"Total Price: {CalculateTotalPrice()} USD");
           
           
          
       }
       

    }
    // rental manager 
    class RentalManager
    {
        private Vehicle [] vehicles = new  Vehicle[100];
        private Customer[] customers = new Customer[100];
        private Rental[] rentals = new Rental[100];
        // counters to track if there are any empty spaces in the list 
        public int vehicleCount = 0;
        public int customerCount = 0;
        public int rentalCount = 0;    
        // adding vehicles using array
        public void addVehicle(Vehicle vehicle)
        {
            if (vehicleCount < vehicles.Length)
            {
                vehicles[vehicleCount] = vehicle;
                vehicleCount++;
            }
            else
            {
                Console.WriteLine("The Vehicle list is FULL!");
            }
        }
        // adding customer using array 
        public void addCustomer(Customer customer)
        {
            if (customerCount < customers.Length)
            {
                customers[customerCount] = customer;
                customerCount++;
            }
            else
            {
                Console.WriteLine("The Customer list is FULL!");
            }
        }
        
        // find customer by id 
        public Customer FindCustomerById(string customerId)
        {
            for (int i = 0; i < customerCount; i++)
            {
                if (customers[i].getCustomerID() == customerId) return customers[i];
            }
            return null;
        } 
        // find vehicle by id 
        public Vehicle FindVehicleById(string vehicleId)
        {
            for (int i = 0; i < vehicleCount; i++)
            {
                if(vehicles[i].getVehicleID() == vehicleId) return vehicles[i];
            }
            return null;
        }
        // check vehicle availability 
        public bool CheckVehicleAvailability(string id)
        {
            Vehicle vehicle = FindVehicleById(id); // search for the availability 
           return  vehicle  !=null && vehicle.IsAvailable();
           /*
            * returns true if the vehicle is available and it's found 
            */
        }
        // rent a vehicle 
        // FIXED: Changed parameter order to match the call in addRental method
        public void RentVehicle(string vehicleId, string customerId, int days)
        {
            // check if the both customer and vehicle ids exist 
            Customer customer = FindCustomerById(customerId);
            Vehicle vehicle = FindVehicleById(vehicleId);
            if(vehicle == null){ 
                Console.WriteLine("Vehicle not found!");
                return;
            }
            if(customer == null){ Console.WriteLine("Customer not found!"); return; }
            if(!vehicle.IsAvailable()){ Console.WriteLine("Vehicle is not available!"); return; }
            
            // if all validations went well => performing the rental
            Rental rental = new Rental();
            rental.CreateRental(customer,vehicle,days);
            if (rentalCount < rentals.Length)
            {
                rentals[rentalCount++] = rental; // assign and increment at the same line
            }
            else
            {
                Console.WriteLine("Rental is full!");
                return;
            }

            Console.WriteLine("----- Rental Summary -----");
            Console.WriteLine();
            rental.getRentalSummary();
            
           



        }
        //return vehicle 
        public void ReturnVehicle(string vehicleId)
        {
            for (int i = 0; i < rentalCount; i++)
            {
                if (rentals[i].getVehicle().getVehicleID() == vehicleId && !rentals[i].IsCompleted())
                {
                    rentals[i].CompleteRental();
                    Console.WriteLine("Vehicle returned successfully!");
                    return;
                }
            }
            // FIXED: Moved this outside the loop so it only prints if no rental found
            Console.WriteLine("No active rental is FOUND for this vehicle!");
        }
        
        // display available vehicles
        public void ViewAvailableVehicles()
        {

            bool anyAvailable = false;
            Console.WriteLine("\n--------- Available Vehicles ---------");
            for (int i = 0; i < vehicleCount; i++)
            {
                if (vehicles[i].IsAvailable())
                {
                    Console.WriteLine(vehicles[i].DisplayVehicleInfo());
                    anyAvailable = true;
                }
            }
            // FIXED: Moved this outside the loop so it only prints once
            if(!anyAvailable) 
            {
                Console.WriteLine("No available vehicles To View!");
            }
            Console.WriteLine("--------------------------------------");
        }
        // view rental history 
        public void ViewRentalHistory()
        {
            Console.WriteLine("\n---------- Rental History ----------");
            if(rentalCount == 0)
            { 
                Console.WriteLine("No rental history available!"); 
                return;
            }

            for (int i = 0; i < rentalCount; i++)
            {
                rentals[i].getRentalSummary();
            }
            Console.WriteLine("--------------------------------------");
        }




    }
    
}
