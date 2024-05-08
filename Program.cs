internal class Program
{
    private static void Main(string[] args)
    {

        do
        {
            Console.WriteLine("Nhap \"1\": Bat dau ma hoa");
            Console.WriteLine("Nhap \"2\": Bat dau giai ma");
            int value = Console.Read();
            string plainText = string.Empty;
            string key = string.Empty;
            DES des = null;
            switch (value)
            {
                case 1:
                    Console.WriteLine("Enter Plain Text:");
                    plainText = Console.ReadLine();
                    Console.WriteLine("Enter Key :");
                    key = Console.ReadLine();
                    des = new DES(plainText, key);
                    break;
                case 2:
                    Console.WriteLine("Enter DES Text:");
                    plainText = Console.ReadLine();
                    Console.WriteLine("Enter Key :");
                    key = Console.ReadLine();
                    des = new DES(key);
                    DecodeDES decodeDES = new DecodeDES(des.GetDictionaryKey());
                    break;
                default: 
                    Console.WriteLine("Thoat Chuong Trinh!");
                    return;
            }

        } while (true);
    }
}