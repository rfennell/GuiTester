using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GuiTester.TestAttributes;
using GuiTester.TestFramework;
using System.Reflection.Emit; // used for lightweight codegen

namespace TestProject
{
    /// <summary>
    /// An attempt to access the GUITest harness from with VS.NET
    /// </summary>
    [TestClass]
    public class UnitTest1
    {
        TestDataStore[] tests;
        object obj;

        public UnitTest1()
        {
            // find all the tests in the assembly under test
            tests = TestControl.FindTests(@"C:\projects\GUITester\SampleApp\bin\Debug\SampleApp.exe");
            if ((tests != null) && (tests.Length > 0))
            {
                // create a working object to test
                obj = Activator.CreateInstance(tests[0].ContainingClassType);

                #region ilEmitTrial
                /*
                DynamicMethod dm = new DynamicMethod(
                                        "DynamicTestWrapper1",
                                        typeof(void),
                                        null,
                                        this.GetType());

                
                ILGenerator il = dm.GetILGenerator();
                
                
                il.Emit(OpCodes.Nop);
                il.Emit(OpCodes.Ldarg_0);
                il.Emit(OpCodes.Ldarg_0);
                il.Emit(OpCodes.Ldfld ,"class [GUITestLibrary]GuiTester.TestFramework.TestDataStore[] TestProject.UnitTest1::tests");
                il.Emit(OpCodes.Ldc_I4_0);
                il.Emit(OpCodes.Ldelem_Ref);
                il.Emit(OpCodes.Call, "instance void TestProject.UnitTest1::DoTest(class [GUITestLibrary]GuiTester.TestFramework.TestDataStore)");
                il.Emit(OpCodes.Nop);
                il.Emit(OpCodes.Ret);
                */



    
                //foreach (TestDataStore testDetails in tests)
                //{
                //    //System.Reflection.Emit.MethodBuilder mb = new System.Reflection.Emit.MethodBuilder();


                //}
                #endregion

            }
            
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion
        
        [TestMethod]
        public void TestUsingAsserts()
        {
            foreach (TestDataStore test in tests)
            {
                DoAssertBasedTest(test);
            }
        }

        [TestMethod]
        public void TestMethodAsBatch()
        {
            bool overallresult = true;
            string resultsText = string.Empty;
            foreach (TestDataStore test in tests)
            {
                string resultDetails = string.Empty;
                bool testresult = DoBooleanBasedTest(test, ref resultDetails);
                resultsText +=resultDetails + "\r\n";
                if (overallresult == true)
                {
                    // we only really need the first failure
                    overallresult = testresult;
                }
            }

            if (overallresult == false)
            {
                Assert.Fail(resultsText);
            }

        }

        /// <summary>
        /// A single method that can run any given test
        /// It uses asserts so returns on the first failure
        /// </summary>
        /// <param name="testDetails">The test to run</param>
        private void DoAssertBasedTest(TestDataStore testDetails)
        {
            bool result = true; // assume a pass and look for failures

            
            // check we have an application to run the test on
            if (obj == null)
            {
                Assert.Fail("No Application object created for test");
            }

            try
            {
                if (testDetails.ControlInfo != null)
                {
                    // it is a control based test
                    result = ((GuiTestAttribute)testDetails.TestAttribute).DoTest(obj, testDetails.ControlInfo);
                }
                else
                {
                    // it is a class wide test
                    result = ((GuiTestAttribute)testDetails.TestAttribute).DoTest(obj);
                }
            }
            catch (TestFailedException ex)
            {
                Assert.Fail(testDetails.TestAttribute.ToString() + " failed with " + ex.Message);
            }
            catch (Exception ex)
            {
                Assert.Fail(testDetails.TestAttribute.ToString() + " failed with " + ex.Message);
            }

            if (result == false)
            {
                Assert.Fail(testDetails.TestAttribute.ToString() + " failed");
            }

        } // end method


        /// <summary>
        /// A single method that can run any given test
        /// It uses boolean flags to allow a test batch to be produced
        /// </summary>
        /// <param name="testDetails">The test to run</param>
        private bool DoBooleanBasedTest(TestDataStore testDetails, ref string result)
        {
            
            bool testResult = false;


            // check we have an application to run the test on
            if (obj == null)
            {
               result = "No Application object created for test";
            }

            try
            {
                if (testDetails.ControlInfo != null)
                {
                    // it is a control based test
                    testResult = ((GuiTestAttribute)testDetails.TestAttribute).DoTest(obj, testDetails.ControlInfo);
                }
                else
                {
                    // it is a class wide test
                    testResult = ((GuiTestAttribute)testDetails.TestAttribute).DoTest(obj);
                }

                if (testResult == false)
                {
                    result = testDetails.TestAttribute.ToString() + " failed";
                }
                else
                {
                    result = testDetails.TestAttribute.ToString() + " passed";
                }

            }
            catch (TestFailedException ex)
            {
                result = testDetails.TestAttribute.ToString() + " failed with " + ex.Message;
            }
            catch (Exception ex)
            {
                result = testDetails.TestAttribute.ToString() + " failed with " + ex.Message;
            }

            return testResult;

        } // end method

    } // end class
}  // end ns
