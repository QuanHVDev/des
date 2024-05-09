internal class Program
{

    // m= 00123456789ABCDE k=0133457799BBCDFF => 1ABFF69D5A93E80B
    // m= 0123D56789ABCDE8 k=183457799B3CDFF2
    // m= 02468aceeca86420 k=0f1571c947d9e859 => DA02CE3A89ECAC3B

    private static void Main(string[] args)
    {
        string plainText = string.Empty;
        string key = string.Empty;
        
        DES des;
        do
        {
            Console.WriteLine("Nhap \"1\": Bat dau ma hoa");
            Console.WriteLine("Nhap \"2\": Bat dau giai ma");
            string value = Console.ReadLine();
            switch (value)
            {
                case "1":
                    Console.WriteLine("Enter Plain Text:");
                    plainText = Console.ReadLine();
                    Console.WriteLine("Enter Key :");
                    key = Console.ReadLine();
                    des = new DES(plainText, key);
                    break;
                case "2":
                    Console.WriteLine("Enter DES Text:");
                    plainText = Console.ReadLine();
                    Console.WriteLine("Enter Key :");
                    key = Console.ReadLine();
                    des = new DES(key);
                    DecodeDES decodeDES = new DecodeDES(plainText, des.GetDictionaryKey());
                    break;
                default:
                    Console.WriteLine("Thoat Chuong Trinh!");
                    return;
            }

        } while (false);

    }
}

