### This project has been moved from CodePlex as it is being closed down, it is not under active development ###

The graphical user interface of a computer system is one of the most troublesome sub-systems to test. Though some products have been marketed to address this area they have not been that well-received or effective. All such products have tended to be based on macro recording systems that are awkward to implement and keep up to date. The net effect of this is that the test engineer spends more time debugging their test scripts than they do testing their product, as the slightest change in a GUI layout will usually break the test harness. Hence up to now the most effective way to test a GUI is to get human testers to do it, though this is not without problems of its own, humans make mistakes, especially when doing boring repetitive tasks.

The lack of GUI testing is therefore a major, but currently unavoidable, hole in current software engineering processes. Only so much can be done when separating the GUI from the business logic and using unit testing (the testing of single methods or subsystem using many small tests usually implemented within the main body of code) with tools such as nunit (the .NET variant of the XUnit/JUnit testing kit)

A potential way to addressing this problem is with the advanced features of Microsoft .NET framework. .NET provides two technologies that can help address this problem:

Attributes – The ability for the developer to add custom meta data to their program assembly that can provide information on their intentions and the use of the assembly for both static analysis and at run time. 

Reflection – The ability of a .NET program to read the meta data of an assembly and use it to create objects and execute processes as required.
It is possible to provide meta data on Windows controls such that a test harness can load the Windows application and ‘watch for’ an appropriate response when it triggers given events e.g. when button X is pressed then the text Y should appear in textbox Z.

The system can be broken into two parts. Firstly a set of attributes have been developed that allow a .NET application to ‘marked up’ for testing as shown below.

```
/// 
/// Test application with GUI testing attributes
/// 
[GuiTestable]
[UserDefinedTest("User Test 1","UserTest1")]
public class Form1 : System.Windows.Forms.Form         { 
       [ClickTextTest("Button 1 Click Test 1","_label1","Hello World")] // false
       [ClickTextTest("Button 1 Click Test 2","_textBox1","Hello World")] // true
       [ClickDataGridCountTest("Button 1 Click DG Test 2","_dataGrid",5)] // true
       private System.Windows.Forms.Button _button1;
       // implementation of other methods……
}
```

These attributes define where the class should be inspected for tests [GuiTestable] and the tests the developer has specified for a given GUI feature in the application. In the sample listing there are three tests defined two of type [ClickTextTest] and one of type [ClickDataGridCountTest]. In essence all these attributes tell the test harness that there is a test of a given name and when the GUI control, _button1, is pressed in the test application the test harness is to look for the stated text, or number of rows in a defined GUI control.

A Window form Test Harness application has been developed. The Windows form application follows the same broad interface as the Nunit testing tool. The user loads the .NET assembly (.EXE or .DLL) they wish to test, using an option on the file menu. All the tests within any classes in the assembly marked as [GuiTestable] are shown. Pressing the ‘Run Test’ button will run the entire set of tests in the assembly. An indication is given for the pass or failure of each test run.. Detailed output is shown in a logger panel.

What is going on under the hood? The key to the operation of this system is the use of reflection by the test harness. The general principle is that the harness loads a .NET assembly file using a byte stream based method to avoid locking the file to other users. It then inspects all the classes in the assembly looking for the [GuiTestable] attribute. If this is found it then further inspects all the private, protected and public members of the class for any of the testing framework attributes. Any attributes found are added to the available list of tests.

When the ‘Run Test’ button is pressed an instance of the of class object under test, a Windows form, is created as a hidden background object. Then for each test, the test harness checks the state of the Windows control to monitor for the initial value, triggers the required event on the Windows control under test and finally checks the control being monitored to make sure the value has changed as required.

Does it succeed as a testing system? The measure of any testing system is whether the effort in setting up the tests and maintaining them outweighs the cost in doing manual tests. Many testing products fail this test; this is especially true for GUI testing. The key problem with GUI testing is that most tools rely on knowing where the controls under test are on the page. The testing model in the project does remove this requirement. The management of tests is based on the logical structure of the Windows form, not some XY grid locations. Given this fact then this testing model is a success, once the developer has added a test, hopefully, if using a ‘test driven development model’ before they implement the actual application code, then the test should remain valid for the lifetime of the control it tests. This should be irrespective of whether the control is moved, resized or any of its display properties changed.

However, this does not mean that this project offers an outright solution to all GUI testing. In essence it is a unit testing model, and all unit testing models have the issue that they test blocks of code statically. Usually a special instance of the class under test is created and the tests run. This means that often unit testing tends to be limited to testing libraries of functions that are easy to handle statically e.g. finance operations, if I have these parameters do I get the correct numeric answer? This becomes a problem for this GUI testing models, no GUI is static; its job is to dynamically display information generated by backend business logic. Care has to be taken when designing tests that the Windows form when created does have the required underlying business features, such as connections to databases, to allow meaningful tests.




The next step is for me to package you my test system such that it is easily usable and extensible by others. This should be done in the near future, other projects permitting! Keep an eye open on this blog for further postings

 

