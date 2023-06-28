namespace CS_Basic.Module_7_Final
{
    abstract class Delivery
    {
        public string Address;
        /// <summary>
        /// Отправить посылку
        /// </summary>
        /// <param name="address"></param>
        public abstract void Send(string address);
    }

    public class Courier
    {
        string Name;
        public Courier(string name) { Name = name; }    
    }
    class Buyer
    {
        private string Name;
        private string Lastname;
        public string Home_Address;        

        public Buyer(string name) 
        { Name = name; }
        public Buyer(string name, string lastname) :this(name) 
        { Lastname = lastname;}        

        public (string type, string number)[] phones;
        string email;

        Product[] product_list = new Product[10];
        public void AddToCart(Product product)
        { 
            for(int i = 0; i < product_list.Length; i++) 
            { 
                if(product_list[i] is null)
                {
                    product_list[i] = product;
                    return;
                }
            }
        }
        public void RemoveFromCart(Product product) 
        {
            for (int i = product_list.Length - 1; i >= 0; i--)
            {
                if (product_list[i] == product)
                {
                    product_list[i] = null;
                    return;
                }
            }
        }
        private void PrintOrder()
        {
            Console.WriteLine(Name + " " + Lastname + " заказал:");
            for (int i = 0; i < product_list.Length; i++)
                if (product_list[i] is not null)
                    Console.WriteLine(product_list[i].Product_name);            
        }

        /// <summary>
        /// Заказать доставку
        /// </summary>
        public void CreateOrder<TDelivery>(string addrees) where TDelivery : Delivery
        {
            Order<TDelivery> order = new Order<TDelivery>();
            order.Products = this.product_list;
            order.Address = addrees;
            PrintOrder();
            Console.WriteLine("Создан заказ с доставкой на адрес " + addrees);
            
        }
    }

    class HomeDelivery : Delivery
    {
        Courier courier = new Courier("Red John");
        //Buyer buyer1 ;
        public override void Send(string address)
        { 
            Console.WriteLine("Выполняю доставку на дом по адресу: \r\n" + address);
        }
    }

    class PickPointDelivery : Delivery
    {
        public override void Send(string address)
        {
            Console.WriteLine("Выполняю доставку PickPoint по адресу: \r\n" + address);
        }
    }

    class ShopDelivery : Delivery
    {
        public override void Send(string address)
        {
            Console.WriteLine("Выполняю доставку в магазин по адресу: \r\n" + address);
        }
    }

    class Product
    {
        public string Product_name;
        string Product_description;
        /// <summary>
        /// Идентификатор товара
        /// </summary>
        string Product_ID; 
        public Product(string product_name) 
        { Product_name = product_name; }
    }

    class Order<TDelivery/*,TStruct*/> where TDelivery : Delivery //, new() - сделал для композиции, не работает
    {
        private TDelivery delivery;

        // Агрегация
        //public Order<TDelivery>(TDelivery delivery)
        //{
        //    this.delivery = delivery;
        //}

        // композиция
        //public Order<TDelivery>()
        //    {
        //        delivery = new <TDelivery>(); //так ошибка
        //    }

        public int Number;

        public string Address;

        public string Description;

        public Product[] Products;
        
        
        public void DisplayAddress()
        {
            Console.WriteLine(delivery.Address);
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Buyer Jane = new Buyer("Patric","Jane");
            //Jane.phones[] = Jane.phones[2];
            Jane.Home_Address = "CBI Sacramento";
            Jane.AddToCart(new Product("Сыр"));
            Jane.AddToCart(new Product("Чай"));
            Jane.AddToCart(new Product("Сэндвич"));
            Jane.CreateOrder<HomeDelivery>(Jane.Home_Address);

            Buyer Teresa = new Buyer("Teresa", "Lisbon");
            Teresa.Home_Address = "CBI Sacramento";
            Teresa.AddToCart(new Product("Салат"));
            Teresa.AddToCart(new Product("Кофе"));
            Teresa.AddToCart(new Product("Чизкейк"));
            Teresa.CreateOrder<ShopDelivery>("Chic av. 2" );

            Buyer Cho = new Buyer("Cho");
            Cho.Home_Address = "CBI Sacramento";
            Cho.AddToCart(new Product("Кола"));
            Cho.AddToCart(new Product("Гамбургер"));
            Cho.CreateOrder<PickPointDelivery>("5th av. 55");

            Console.ReadKey();

        }

    }
}