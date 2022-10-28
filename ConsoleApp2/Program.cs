using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Program
    {
        const string queueName = @".\private$\TestQueue";
        public static int custNum = 0;
        public static string ItemOrder = string.Empty;
        public static int PosCounter = 0;
        public static int MrDCounter = 0;
        

        //public static string ShopOpen = "09:00:00.01";//DateTime.Now.TimeOfDay.ToString();
        //public static string ShopClosed = DateTime.Now.TimeOfDay.ToString();

        static void Main(string[] args)
        {  
            PlaceOrderToQueue(queueName);
            ProcessOrderFromQueue(queueName);


        }

        public static void ProcessOrder(DateTime d)
        {
            var seconds = 10;
            var processingTime = new DateTime(d.Year, d.Month, d.Day, 0, 0, seconds);

            for (int i = 0; i <= seconds; i++)
            {
                processingTime = processingTime.AddSeconds(-1);
                System.Threading.Thread.Sleep(1000);
            }
        }
        public static int CustomerRadgenerator()
        {
            Random rnd = new Random();
            int cust = rnd.Next(1, 15000);

            return cust;
        }

        public static string MenuItemRad()
        {
            var random = new Random();
            var list = new List<String> { "Burger", "Soda", "Chips", "Smiley Meal" };         
            int index = random.Next(list.Count);
            
            return list[index];
        }

        public static void BirdmanToyCounter()
        {
            int toyCounter = 50;

            if (toyCounter == 0)
            {
                Console.WriteLine("We have runout of Badman toys for the day,Sorry ");
            }
            else {
                toyCounter--;

            }
        }
        private static void PlaceOrderToQueue(string queueName)
        {
            // check if queue exists, if not create it
            MessageQueue msMq = null;

            if (!MessageQueue.Exists(queueName))
            {
                msMq = MessageQueue.Create(queueName);
            }
            else
            {
                msMq = new MessageQueue(queueName);
            }

            custNum = CustomerRadgenerator();
            ItemOrder = MenuItemRad();
            try
            {
                //for (int i = 1; i <= 2; i++)
                //{
                Order order = new Order()
                {
                    OrderID = custNum,
                    OrderTimeCreated = DateTime.Now,
                    IsPOS = true,
                    ItemsOrdered = new List<MenuItem>()
                    {
                        new MenuItem() { Desc = ItemOrder }                       
                    }                    
                }; 

                if (custNum < 10000)
                {
                    PosCounter++;
                }
                else {
                    MrDCounter++;
                    order.IsPOS = false;
                }

                if (ItemOrder == "Smiley Meal")
                {
                    BirdmanToyCounter();
                }

                msMq.Send(order);
                //}                               
            }
            catch (MessageQueueException ee)
            {
                Console.Write(ee.ToString());
            }

            catch (Exception eee)
            {
                Console.Write(eee.ToString());
            }
            finally
            {
                msMq.Close();
            }
        }

        private static void ProcessOrderFromQueue(string queueName)
        {
            int PosTot = PosCounter;
            int MrDTot = MrDCounter;
            MessageQueue msMq = msMq = new MessageQueue(queueName);
            try
            {
                for (int x = 0; x <= msMq.GetAllMessages().Length; x++)
                {
                    var queueCounter = msMq.GetAllMessages();
                    msMq.Formatter = new XmlMessageFormatter(new Type[] { typeof(Order) });
                    //process order
                    var message = (Order)msMq.Receive().Body;

                    ProcessOrder(message.OrderTimeCreated);
                    
                    Console.WriteLine("Order Number: " + custNum.ToString());
                    Console.WriteLine("Order Date Time: " + message.OrderTimeCreated);
                    Console.WriteLine("Collection Date Time: " + DateTime.Now);
                    Console.WriteLine("Customer: " + message.IsPOS);
                }
            }

            catch (MessageQueueException ee)
            {
                Console.Write(ee.ToString());
            }
            catch (Exception eee)
            {
                Console.Write(eee.ToString());
            }
            finally
            {
                msMq.Close();
            }
        }


    }
} 
