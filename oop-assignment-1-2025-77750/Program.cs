namespace oop_assignment_1_2025_77750
{
    // Interface for validation
    public interface IValidatable
    {
        bool IsValid();
    }

    //  classes
    public abstract class AbstractTool
    {
        protected string manufacturer;
        protected string model;
        protected double price;
        protected bool borrowed;

        public AbstractTool(string manufacturer, string model, double price)
        {
            this.manufacturer = manufacturer;
            this.model = model;
            this.price = price;
            this.borrowed = false;
        }

        // Abstract methods to be implemented by subclasses
        public abstract void Display();
        public abstract string GetToolType();
    }

    // Main PowerTool class
    public class PowerTool : AbstractTool, IValidatable
    {
       
        private static readonly string[] VALID_TOOL_TYPES = { "Drill", "Saw", "Sander", "Grinder" };
        private const double MIN_PRICE = 0.0;

        
        private string toolType;
        private string serialNumber;

        
        public string Manufacturer => manufacturer;
        public string Model => model;
        public string ToolType => toolType;
        public string SerialNumber => serialNumber;
        
        public PowerTool(string manufacturer, string model, string toolType,
                        string serialNumber, double price, bool borrowed)
                        : base(manufacturer, model, price)
        {
            this.toolType = ValidateToolType(toolType) ? toolType : "Drill";
            this.serialNumber = serialNumber;
            this.borrowed = borrowed;
        }

      
        public PowerTool(string manufacturer, string model, string toolType,
                        string serialNumber, double price)
                        : this(manufacturer, model, toolType, serialNumber, price, false)
        {
        }
        
        public PowerTool(string manufacturer, string model, string serialNumber)
                        : this(manufacturer, model, "Drill", serialNumber, 0.0, false)
        {
        }
        
        private bool ValidateToolType(string toolType)
        {
            foreach (string validType in VALID_TOOL_TYPES)
            {
                if (validType.Equals(toolType, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

        private bool ValidatePrice(double price)
        {
            return price >= MIN_PRICE;
        }

        public bool IsValid()
        {
            return !string.IsNullOrEmpty(manufacturer) &&
                   !string.IsNullOrEmpty(model) &&
                   !string.IsNullOrEmpty(serialNumber) &&
                   ValidateToolType(toolType) &&
                   ValidatePrice(price);
        }

      

        public void Borrow()
        {
            if (!borrowed)
            {
                borrowed = true;
                Console.WriteLine($"Tool {serialNumber} has been successfully borrowed.");
            }
            else
            {
                Console.WriteLine($"Error: The tool {serialNumber} is already on loan.");
            }
        }

        public void ReturnTool()
        {
            borrowed = false;
            Console.WriteLine($"Tool {serialNumber} has been returned.");
        }

        public void ChangePrice(double newPrice)
        {
            if (ValidatePrice(newPrice))
            {
                this.price = newPrice;
                Console.WriteLine($"Price updated to: ${newPrice:F2}");
            }
            else
            {
                Console.WriteLine("Error: Invalid price. Price must be non-negative.");
            }
        }

        public double CheckPrice()
        {
            return price;
        }

        public bool CheckBorrowed()
        {
            return borrowed;
        }

        public override void Display()
        {
            Console.WriteLine("╔══════════════════════════════════╗");
            Console.WriteLine("║           POWER TOOL INFO        ║");
            Console.WriteLine("╠══════════════════════════════════╣");
            Console.WriteLine($"║ {"Manufacturer",-15}: {manufacturer,-18} ║");
            Console.WriteLine($"║ {"Model",-15}: {model,-18} ║");
            Console.WriteLine($"║ {"Tool Type",-15}: {toolType,-18} ║");
            Console.WriteLine($"║ {"Serial Number",-15}: {serialNumber,-18} ║");
            Console.WriteLine($"║ {"Price",-15}: ${price,-17:F2} ║");
            Console.WriteLine($"║ {"Borrowed",-15}: {(borrowed ? "Yes" : "No"),-18} ║");
            Console.WriteLine("╚══════════════════════════════════╝");
            Console.WriteLine();
        }

        public override string GetToolType()
        {
            return toolType;
        }
    }

    
    public class PowerToolDriver
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("=== POWER TOOL MANAGEMENT SYSTEM ===\n");

           
            PowerTool tool1 = new PowerTool("DeWalt", "DCD771", "Drill",
                                           "DW12345", 89.99, false);

            PowerTool tool2 = new PowerTool("Makita", "XRJ04Z", "Saw",
                                           "MK67890", 149.99);

            PowerTool tool3 = new PowerTool("Milwaukee", "2850-20", "Grinder",
                                           "MW24680", 199.99, true);

            PowerTool tool4 = new PowerTool("Bosch", "ROS20VSC", "ROS12345");

            //  all tools
            Console.WriteLine("INITIAL TOOL INVENTORY:");
            Console.WriteLine("=======================");
            tool1.Display();
            tool2.Display();
            tool3.Display();
            tool4.Display();

          

            
            Console.WriteLine("TESTING BORROW METHOD:");
            Console.WriteLine("======================");
            tool1.Borrow();  
            tool1.Borrow();  // Should show error 

            
            Console.WriteLine("\nTESTING RETURN METHOD:");
            Console.WriteLine("======================");
            tool3.ReturnTool();
            tool3.Borrow();      

            
            Console.WriteLine("\nTESTING PRICE CHANGE:");
            Console.WriteLine("=====================");
            Console.WriteLine($"Tool 2 current price: ${tool2.CheckPrice():F2}");
            tool2.ChangePrice(159.99);
            Console.WriteLine($"Tool 2 new price: ${tool2.CheckPrice():F2}");

            // Test invalid price (-)
            tool2.ChangePrice(-50.0);

            // Test CheckPrice() and CheckBorrowed() 
            Console.WriteLine("\nTESTING STATUS CHECKS:");
            Console.WriteLine("======================");
            Console.WriteLine($"Tool 1 - Price: ${tool1.CheckPrice():F2}, Borrowed: {tool1.CheckBorrowed()}");
            Console.WriteLine($"Tool 4 - Price: ${tool4.CheckPrice():F2}, Borrowed: {tool4.CheckBorrowed()}");

            
            tool4.ChangePrice(79.99);
            tool4.Borrow();

            
            Console.WriteLine("\nFINAL TOOL INVENTORY:");
            Console.WriteLine("=====================");
            tool1.Display();
            tool2.Display();
            tool3.Display();
            tool4.Display();

            // Test validation
            Console.WriteLine("VALIDATION STATUS:");
            Console.WriteLine("==================");
            Console.WriteLine($"Tool 1 valid: {tool1.IsValid()}");
            Console.WriteLine($"Tool 4 valid: {tool4.IsValid()}");

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
