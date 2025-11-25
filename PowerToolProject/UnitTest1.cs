using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class PowerToolTests
{
    [TestMethod]
    public void TestConstructorWithAllParameters()
    {
        PowerTool tool = new PowerTool("DeWalt", "DCD771", "Drill", "DW12345", 89.99, false);
        
        Assert.AreEqual("DeWalt", tool.Manufacturer);
        Assert.AreEqual("DCD771", tool.Model);
        Assert.AreEqual("Drill", tool.ToolType);
        Assert.AreEqual("DW12345", tool.SerialNumber);
        Assert.AreEqual(89.99, tool.CheckPrice());
        Assert.IsFalse(tool.CheckBorrowed());
    }

    [TestMethod]
    public void TestBorrow()
    {
        PowerTool tool = new PowerTool("Makita", "XRJ04Z", "Saw", "MK67890", 149.99);
        
        // First borrow should succeed
        tool.Borrow();
        Assert.IsTrue(tool.CheckBorrowed());
    }

    [TestMethod]
    public void TestBorrowWhenAlreadyBorrowed()
    {
        PowerTool tool = new PowerTool("Makita", "XRJ04Z", "Saw", "MK67890", 149.99, true);
        
        // Should remain borrowed (error handled in method)
        tool.Borrow();
        Assert.IsTrue(tool.CheckBorrowed());
    }

    [TestMethod]
    public void TestReturnTool()
    {
        PowerTool tool = new PowerTool("Milwaukee", "2850-20", "Grinder", "MW24680", 199.99, true);
        
        Assert.IsTrue(tool.CheckBorrowed());
        tool.ReturnTool();
        Assert.IsFalse(tool.CheckBorrowed());
    }

    [TestMethod]
    public void TestChangePrice()
    {
        PowerTool tool = new PowerTool("Bosch", "ROS20VSC", "Sander", "BOS123", 79.99);
        
        Assert.AreEqual(79.99, tool.CheckPrice());
        tool.ChangePrice(89.99);
        Assert.AreEqual(89.99, tool.CheckPrice());
    }

    [TestMethod]
    public void TestChangePriceWithInvalidValue()
    {
        PowerTool tool = new PowerTool("Bosch", "ROS20VSC", "Sander", "BOS123", 79.99);
        
        // Attempt to set negative price
        tool.ChangePrice(-50.0);
        // Price should remain unchanged
        Assert.AreEqual(79.99, tool.CheckPrice());
    }

    [TestMethod]
    public void TestCheckPrice()
    {
        PowerTool tool = new PowerTool("DeWalt", "DCD771", "Drill", "DW12345", 89.99);
        Assert.AreEqual(89.99, tool.CheckPrice());
    }

    [TestMethod]
    public void TestCheckBorrowed()
    {
        PowerTool tool = new PowerTool("Makita", "XRJ04Z", "Saw", "MK67890", 149.99, true);
        Assert.IsTrue(tool.CheckBorrowed());
        
        PowerTool tool2 = new PowerTool("Bosch", "ROS20VSC", "Sander", "BOS123", 79.99, false);
        Assert.IsFalse(tool2.CheckBorrowed());
    }

    [TestMethod]
    public void TestConstructorWithSubset()
    {
        PowerTool tool = new PowerTool("DeWalt", "DCD771", "Drill", "DW12345", 89.99);
        
        Assert.AreEqual("DeWalt", tool.Manufacturer);
        Assert.AreEqual("Drill", tool.ToolType);
        Assert.AreEqual(89.99, tool.CheckPrice());
        Assert.IsFalse(tool.CheckBorrowed()); // Should default to false
    }

    [TestMethod]
    public void TestConstructorWithEssentialAttributes()
    {
        PowerTool tool = new PowerTool("Bosch", "ROS20VSC", "ROS12345");
        
        Assert.AreEqual("Bosch", tool.Manufacturer);
        Assert.AreEqual("ROS20VSC", tool.Model);
        Assert.AreEqual("ROS12345", tool.SerialNumber);
        Assert.AreEqual("Drill", tool.ToolType); // Should default to "Drill"
        Assert.AreEqual(0.0, tool.CheckPrice()); // Should default to 0.0
        Assert.IsFalse(tool.CheckBorrowed()); // Should default to false
    }

    [TestMethod]
    public void TestIsValid()
    {
        // Valid tool
        PowerTool validTool = new PowerTool("DeWalt", "DCD771", "Drill", "DW12345", 89.99);
        Assert.IsTrue(validTool.IsValid());
    }

    [TestMethod]
    public void TestIsValidWithInvalidData()
    {
        // Invalid tool (empty manufacturer)
        PowerTool invalidTool = new PowerTool("", "DCD771", "Drill", "DW12345", 89.99);
        Assert.IsFalse(invalidTool.IsValid());
    }

    [TestMethod]
    public void TestToolTypeValidation()
    {
        // Valid tool type
        PowerTool validTool = new PowerTool("DeWalt", "DCD771", "Saw", "DW12345", 89.99);
        Assert.IsTrue(validTool.IsValid());
        
        // Invalid tool type should default to "Drill"
        PowerTool invalidToolType = new PowerTool("DeWalt", "DCD771", "InvalidType", "DW12345", 89.99);
        Assert.AreEqual("Drill", invalidToolType.ToolType);
    }
}