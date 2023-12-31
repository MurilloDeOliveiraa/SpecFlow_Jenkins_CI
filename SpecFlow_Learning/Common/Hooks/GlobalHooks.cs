﻿using NUnit.Framework;
using SpecFlow_Learning.Classes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using TechTalk.SpecFlow;

namespace SpecFlow_Learning
{
    [Binding]
    public  class GlobalHooks : GlobalConfig 
    {
        public GlobalHooks(ScenarioContext scenarioContext) : base(scenarioContext) { }

        [BeforeScenario]
        public void BeforeScenario()
        {            
            List<string> tags = Scenario.ScenarioInfo.Tags.ToList();  //Checking if the scenario contains the current tag
            bool result = tags.Contains(ConfigurationManager.AppSettings["Environment"]);
            if (!result)
            {
                Console.WriteLine("The test: " + Scenario.ScenarioInfo.Title + " is NOT applicable to this current environment!");
                Assert.Ignore();                
            }
            else
            {
                Console.WriteLine("TEST INITIALIZED!");
                Console.WriteLine($"TEST SCENARIO NAME = " + Scenario.ScenarioInfo.Title.ToString());
                Console.WriteLine("-------------------------------------------------------------------------");
                SetBrowser();
            }                        
        }


        [AfterScenario]
        public void AfterScenario()
        {
            Console.WriteLine("TEST FINISHED!");
            Console.WriteLine("-------------------------------------------------------------------------");
            if (!getDriver().Equals(null))
            {
                getDriver().Close();
                getDriver().Quit();
            }
        }
    }
}