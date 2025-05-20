namespace lab11
{

    class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }

        public override string ToString()
        {
            return $"\t{Id}, {Name}, {Quantity}, {Price}";
        }
    }

    class Program
    {
        // sales of products on the file (TXT) --> CSV files

        public static Product UserProductCollection()
        {
            Product product = new Product();

            Console.WriteLine("Enter the Product ID: ");
            product.Id = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter the product name: ");
            product.Name = Console.ReadLine();

            Console.WriteLine("Enter the quantity of the product: ");
            product.Quantity = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter the price: ");
            product.Price = double.Parse(Console.ReadLine());

            return product;

        }

        // user's menu
        public static string Menu()
        {
            Console.WriteLine("=-=-=-=-=-=-=- Sales Records =-=-=-=-=-=-=-");
            Console.WriteLine("1- Record new sales to a new file");
            Console.WriteLine("2- Record new sales to an existing file");
            Console.WriteLine("3- View existing sales from a file");
            Console.WriteLine("4- View the list of existing file(s)");
            Console.WriteLine("5- Replace (update) one sale on an existing file");
            Console.WriteLine("6- Exit");
            Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-");
            Console.WriteLine("Enter 1, 2, 3, 4, 5, or 6: ");
            return Console.ReadLine();
        }

        static void Main(string[] args)
        {
            while (true)
            {
                var choice = Menu();
                if (choice == "1") { RecordNewSalesNewFile(); }
                else if (choice == "2") { RecordNewSalesExistingFile(); }
                else if (choice == "3") { ViewExistingSales(); }
                else if (choice == "4") { ViewExistingFiles(); }
                else if (choice == "5") { UpdatingExistingFiles(); }
                else if (choice == "6")
                {
                    Console.WriteLine("Exiting..."); break;
                }
                else { Console.WriteLine("INVALID INPUT!"); }
            }
        }

        // methods:::
        // -----------------------------------------------------------------
        private static void UpdatingExistingFiles()
        {
            Console.WriteLine("List of existing files");
            ViewExistingFiles();
            Console.WriteLine("Enter the name for your file (without extensions): ");
            var fileName = Console.ReadLine() + ".csv";

            if (File.Exists(fileName))
            {
                List<string> lines = File.ReadAllLines(fileName).ToList();

                Console.WriteLine("Enter the product ID to update: ");
                string proID = Console.ReadLine();

                bool found = false;

                // find the record and update
                for (int i = 0; i < lines.Count; i++)
                {
                    string[] data = lines[i].Split(',');

                    if (data[0] == proID) //check for ID match
                    {
                        Console.WriteLine("Existing record: "+ lines[i]);

                        Product updatedProduct = UserProductCollection();
                        lines[i] = $"{updatedProduct.Id},{updatedProduct.Name},{updatedProduct.Quantity},{updatedProduct.Price}";

                        found = true;
                        break;
                    } 
                }

                if (found)
                {
                    File.WriteAllLines(fileName, lines);
                    Console.WriteLine("Record updated successfully!");
                }
                else
                {
                    Console.WriteLine("Product ID not found in the file!");
                }
            }
            else
            {
                Console.WriteLine("File does NOT exist with the same name! Use Menu to create a new file.");
            }
        }

        // -----------------------------------------------------------------
        private static void ViewExistingSales()
        {
            Console.WriteLine("List of existing files");
            ViewExistingFiles();
            Console.WriteLine("Enter the name for your file (without extensions): ");
            var fileName = Console.ReadLine() + ".csv";

            if (File.Exists(fileName))
            {
                using (StreamReader sr = new StreamReader(fileName))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] output = line.Split(',');
                        Console.WriteLine($"\tID: {output[0]}, Name: {output[1]}, Quantity: {output[2]}, Price: {output[3]}");
                    }
                }
            }
            else
            {
                Console.WriteLine("File does NOT exist with the same name! Use Menu to create a new file.");
            }
        }

        // -----------------------------------------------------------------
        private static void ViewExistingFiles()
        {
            string[] files = Directory.GetFiles("./", ".csv"); // gets list of all the files from the root directory

            foreach (var file in files)
            {
                Console.WriteLine(file.Split("/").Last());
            }
        }

        // -----------------------------------------------------------------
        private static void RecordNewSalesExistingFile()
        {
            Console.WriteLine("Enter the name of your file (without extensions): ");
            var fileName = Console.ReadLine() + ".csv"; // to concat the file format name

            if (File.Exists(fileName))
            {
                Console.WriteLine("Enter the number of sales: ");
                var nSale = int.Parse(Console.ReadLine());

                using (StreamWriter sw = new StreamWriter(fileName))
                {
                    for (int i = 0; i < nSale; i++)
                    {
                        Product product = UserProductCollection();
                        sw.WriteLine(product); // ToString()
                    }
                    // sw.Close();

                } // closes the file automatically
            }
            else
            {
                Console.WriteLine("File does NOT exist with the same name! Use Menu to create a new file.");
            }
        }

        // -----------------------------------------------------------------
        private static void RecordNewSalesNewFile()
        {
            Console.WriteLine("Set a name for your file (without extensions): ");
            var fileName = Console.ReadLine() + ".csv"; // to concat the file format name

            if (!File.Exists(fileName))
            {
                Console.WriteLine("Enter the number of sales: ");
                var nSale = int.Parse(Console.ReadLine());

                using (StreamWriter sw = new StreamWriter(fileName))
                {
                    for (int i = 0; i < nSale; i++)
                    {
                        Product product = UserProductCollection();
                        sw.WriteLine(product); // ToString()
                    }
                    // sw.Close();

                } // closes the file automatically
            }
            else
            {
                Console.WriteLine("Duplicated File Name detected! Choose another name or use Menu to edit the existing file.");
            }
        }
    }
}