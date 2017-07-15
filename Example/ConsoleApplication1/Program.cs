using System;
using System.Collections.Generic;
using System.IO;
using TemplateEngine;

namespace ConsoleApplication1
{
    public class Program
    {
        static void Main()
        {
            var te = new TemplateGenerator();
            CustomTemplateBase template;
            using (var sr = new StreamReader(File.OpenRead("sample-template.cshtml")))
            {
                template = te.Generate("OrderInfoTemplate", "Dave", sr, new [] {"ConsoleApplication1.exe"});
            }           
            for (var i = 0; i < 4; i++)
            {
                template.Reset();
                template.Model = SampleOrder();
                template.Execute();                    

                Console.WriteLine("-------");
                Console.WriteLine(template.Body.Trim());
                Console.WriteLine("-------");                   
            }            
        }

        static Order SampleOrder()
        {
            return new Order()
            {
                CustomerID = 5,
                CustomerName = "Dave",
                ID = 1,
                LineItems = new List<OrderLineItem>() {new OrderLineItem()
                {
                    Price = 5,
                    ProductName = "DogBones",
                    Quantity = 10
                }},
                Tax = 5,
                Total = 100
            };            
        }
    }


}
