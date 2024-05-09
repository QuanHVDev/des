using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

public class DES
{
    readonly int[] PC1_Table = {
    57, 49, 41, 33, 25, 17, 9, 1,
    58, 50, 42, 34, 26, 18, 10, 2,
    59, 51, 43, 35, 27, 19, 11, 3,
    60, 52, 44, 36, 63, 55, 47, 39,
    31, 23, 15, 7, 62, 54, 46, 38,
    30, 22, 14, 6, 61, 53, 45, 37,
    29, 21, 13, 5, 28, 20, 12, 4
    };

    readonly int[] PC2_Table = {
    14, 17, 11, 24, 1, 5,
    3, 28, 15, 6, 21, 10,
    23, 19, 12, 4, 26, 8,
    16, 7, 27, 20, 13, 2,
    41, 52, 31, 37, 47, 55,
    30, 40, 51, 45, 33, 48,
    44, 49, 39, 56, 34, 53,
    46, 42, 50, 36, 29, 32
    };

    public static readonly int[] IP_Table = {
    58, 50, 42, 34, 26, 18, 10, 2,
    60, 52, 44, 36, 28, 20, 12, 4,
    62, 54, 46, 38, 30, 22, 14, 6,
    64, 56, 48, 40, 32, 24, 16, 8,
    57, 49, 41, 33, 25, 17, 9, 1,
    59, 51, 43, 35, 27, 19, 11, 3,
    61, 53, 45, 37, 29, 21, 13, 5,
    63, 55, 47, 39, 31, 23, 15, 7
};

    public static readonly int[] IP_Table_1 = {
    40, 8, 48, 16, 56, 24, 64, 32,
    39, 7, 47, 15, 55, 23, 63, 31,
    38, 6, 46, 14, 54, 22, 62, 30,
    37, 5, 45, 13, 53, 21, 61, 29,
    36, 4, 44, 12, 52, 20, 60, 28,
    35, 3, 43, 11, 51, 19, 59, 27,
    34, 2, 42, 10, 50, 18, 58, 26,
    33, 1, 41, 9, 49, 17, 57, 25
    };

    public static readonly int[] EP_Table = {
    32, 1, 2, 3, 4, 5,
    4, 5, 6, 7, 8, 9,
    8, 9, 10, 11, 12, 13,
    12, 13, 14, 15, 16, 17,
    16, 17, 18, 19, 20, 21,
    20, 21, 22, 23, 24, 25,
    24, 25, 26, 27, 28, 29,
    28, 29, 30, 31, 32, 1
};

    public static readonly int[,] S_Box_1 = {
    {14, 4, 13, 1, 2, 15, 11, 8, 3, 10, 6, 12, 5, 9, 0, 7},
    {0, 15, 7, 4, 14, 2, 13, 1, 10, 6, 12, 11, 9, 5, 3, 8},
    {4, 1, 14, 8, 13, 6, 2, 11, 15, 12, 9, 7, 3, 10, 5, 0},
    {15, 12, 8, 2, 4, 9, 1, 7, 5, 11, 3, 14, 10, 0, 6, 13}
};
    public static readonly int[,] S_Box_2 = {
    {15,  1,  8, 14,  6, 11,  3,  4,  9,  7,  2, 13, 12,  0,  5, 10},
    { 3, 13,  4,  7, 15,  2,  8, 14, 12,  0,  1, 10,  6,  9, 11,  5},
    { 0, 14,  7, 11, 10,  4, 13,  1,  5,  8, 12,  6,  9,  3,  2, 15},
    {13,  8, 10,  1,  3, 15,  4,  2, 11,  6,  7, 12,  0,  5, 14,  9}
};
    public static readonly int[,] S_Box_3 = {
    {10,  0,  9, 14,  6,  3, 15,  5,  1, 13, 12,  7, 11,  4,  2,  8},
    {13,  7,  0,  9,  3,  4,  6, 10,  2,  8,  5, 14, 12, 11, 15,  1},
    {13,  6,  4,  9,  8, 15,  3,  0, 11,  1,  2, 12,  5, 10, 14,  7},
    { 1, 10, 13,  0,  6,  9,  8,  7,  4, 15, 14,  3, 11,  5,  2, 12}
};
    public static readonly int[,] S_Box_4 = {
    { 7, 13, 14,  3,  0,  6,  9, 10,  1,  2,  8,  5, 11, 12,  4, 15},
    {13,  8, 11,  5,  6, 15,  0,  3,  4,  7,  2, 12,  1, 10, 14,  9},
    {10,  6,  9,  0, 12, 11,  7, 13, 15,  1,  3, 14,  5,  2,  8,  4},
    { 3, 15,  0,  6, 10,  1, 13,  8,  9,  4,  5, 11, 12,  7,  2, 14}
};

    public static readonly int[,] S_Box_5 = {
    { 2, 12,  4,  1,  7, 10, 11,  6,  8,  5,  3, 15, 13,  0, 14,  9},
    {14, 11,  2, 12,  4,  7, 13,  1,  5,  0, 15, 10,  3,  9,  8,  6},
    { 4,  2,  1, 11, 10, 13,  7,  8, 15,  9, 12,  5,  6,  3,  0, 14},
    {11,  8, 12,  7,  1, 14,  2, 13,  6, 15,  0,  9, 10,  4,  5,  3}
};

    public static readonly int[,] S_Box_6 = {
    {12,  1, 10, 15,  9,  2,  6,  8,  0, 13,  3,  4, 14,  7,  5, 11},
    {10, 15,  4,  2,  7, 12,  9,  5,  6,  1, 13, 14,  0, 11,  3,  8},
    { 9, 14, 15,  5,  2,  8, 12,  3,  7,  0,  4, 10,  1, 13, 11,  6},
    { 4,  3,  2, 12,  9,  5, 15, 10, 11, 14,  1,  7,  6,  0,  8, 13}
};
    public static readonly int[,] S_Box_7 = {
    { 4, 11,  2, 14, 15,  0,  8, 13,  3, 12,  9,  7,  5, 10,  6,  1},
    {13,  0, 11,  7,  4,  9,  1, 10, 14,  3,  5, 12,  2, 15,  8,  6},
    { 1,  4, 11, 13, 12,  3,  7, 14, 10, 15,  6,  8,  0,  5,  9,  2},
    { 6, 11, 13,  8,  1,  4, 10,  7,  9,  5,  0, 15, 14,  2,  3, 12}
};
    public static readonly int[,] S_Box_8 = {
    {13,  2,  8,  4,  6, 15, 11,  1, 10,  9,  3, 14,  5,  0, 12,  7},
    { 1, 15, 13,  8, 10,  3,  7,  4, 12,  5,  6, 11,  0, 14,  9,  2},
    { 7, 11,  4,  1,  9, 12, 14,  2,  0,  6, 10, 13, 15,  3,  5,  8},
    { 2,  1, 14,  7,  4, 10,  8, 13, 15, 12,  9,  0,  3,  5,  6, 11}
};

    public static readonly int[] P_Table = { 16,  7, 20, 21, 29, 12, 28, 17, 1, 15, 23, 26, 5, 18, 31, 10, 2,  8, 24, 14, 32, 27, 3,  9, 19, 13, 30,  6, 22, 11,  4, 25};


    readonly int[] dichTraiCnDn = { 1, 1, 2, 2, 2, 2, 2, 2, 1, 2, 2, 2, 2, 2, 2, 1 };
    Dictionary<int, (string subkey, string Li, string Ri, string Ei, string Ei_XOr_Ki, string S_Box, string P_S_Box, string P_Xor_Li)> kqDictionary;
    public DES(string plainText, string key)
    {
        kqDictionary = new Dictionary<int, (string subkey, string Li, string Ri, string Ei, string Ei_XOr_Ki, string S_Box, string P_S_Box, string P_Xor_Li)>();
        
        // Sinh Khoa
        CreateKey(key);

        Console.WriteLine("==========================");

        // Plain Text
        EncodePlainText(plainText);
    }

    public DES(string key){
        kqDictionary = new Dictionary<int, (string subkey, string Li, string Ri, string Ei, string Ei_XOr_Ki, string S_Box, string P_S_Box, string P_Xor_Li)>();

        CreateKey(key);
    }

    public Dictionary<int, (string subkey, string Li, string Ri, string Ei, string Ei_XOr_Ki, string S_Box, string P_S_Box, string P_Xor_Li)> GetDictionaryKey()
    {
        return kqDictionary;
    }

    public void CreateKey(string k)
    {
        //string k = "133457799BBCDFF1"; // 0f1571c947d9e859
        string[] keys = SinhKhoa(k);
        for (int i = 1; i <= keys.Length; i++)
        {
            kqDictionary.Add(i, (keys[i - 1], string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty));
        }
    }

    

    private void EncodePlainText(string m)
    {
        //string m = "0123456789ABCDEF"; // 02468aceeca86420
        string binary = ChangeKeyKToBinary(m);

        string hoanviIP = "";
        foreach (var index in IP_Table)
        {
            hoanviIP += binary[index - 1];
        }
        //Console.WriteLine("hoanviIP" + hoanviIP);

        string L0 = hoanviIP.Substring(0, hoanviIP.Length / 2);
        string R0 = hoanviIP.Substring(hoanviIP.Length / 2, hoanviIP.Length / 2);
        kqDictionary.Add(0, (string.Empty, ChangeBinaryToHexa(L0), ChangeBinaryToHexa(R0), string.Empty, string.Empty, string.Empty, string.Empty, string.Empty));

        // thuc hien ma hoa
        int indexMaHoa = 1;
        do
        {
            var item = kqDictionary[indexMaHoa];

            string keyBinary = ChangeKeyKToBinary(item.subkey);
            item.Li = kqDictionary[indexMaHoa - 1].Ri;

            string ER0 = CodingWith_EPTable(ChangeKeyKToBinary(item.Li));
            item.Ei = ChangeBinaryToHexa(ER0);

            // xor k1 va er0
            for (int i = 0; i < ER0.Length / 8; i++)
            {
                string ER0_i = ER0.Substring(i * 8, 8);
                string Key_i = keyBinary.Substring(i * 8, 8);

                item.Ei_XOr_Ki += Do_XOR(ER0_i, Key_i);
            }
            //Console.WriteLine($"chia 6 bit va Xor voi E(R0) = {result}");

            item.S_Box = TraBangSBoxI(item.Ei_XOr_Ki);
            item.P_S_Box = TraBangP(item.S_Box);

            item.P_Xor_Li = ChangeBinaryToHexa(Do_XOR(ChangeKeyKToBinary(kqDictionary[indexMaHoa - 1].Li), item.P_S_Box));
            
            item.Ei_XOr_Ki = ChangeBinaryToHexa(item.Ei_XOr_Ki);
            item.S_Box = ChangeBinaryToHexa(item.S_Box);
            item.P_S_Box = ChangeBinaryToHexa(item.P_S_Box);
            item.Ri = item.P_Xor_Li;

            kqDictionary[indexMaHoa] = item;
            if (true)
            {
                Console.WriteLine($"kqDictionary[{indexMaHoa}]: " +
                $"\n kqDictionary[{indexMaHoa}].subkey:{(kqDictionary[indexMaHoa].subkey)} " +
                $"\n kqDictionary[{indexMaHoa}].Li:{(kqDictionary[indexMaHoa].Li)}" +
                $"\n kqDictionary[{indexMaHoa}].Ri:{(kqDictionary[indexMaHoa].Ri)}" +
                $"\n kqDictionary[{indexMaHoa}].Ei:{(kqDictionary[indexMaHoa].Ei)}" +
                $"\n kqDictionary[{indexMaHoa}].Ei_XOr_Ki:{(kqDictionary[indexMaHoa].Ei_XOr_Ki)}" +
                $"\n kqDictionary[{indexMaHoa}].S_Box:{(kqDictionary[indexMaHoa].S_Box)}" +
                $"\n kqDictionary[{indexMaHoa}].P_S_Box:{(kqDictionary[indexMaHoa].P_S_Box)}" +
                $"\n kqDictionary[{indexMaHoa}].P_Xor_Li:{(kqDictionary[indexMaHoa].P_Xor_Li)}");
            }
            else
            {
                Console.WriteLine($"kqDictionary[{indexMaHoa}]: " +
                $"\n kqDictionary[{indexMaHoa}].subkey:{ChangeKeyKToBinary(kqDictionary[indexMaHoa].subkey)} " +
                $"\n kqDictionary[{indexMaHoa}].Li:{ChangeKeyKToBinary(kqDictionary[indexMaHoa].Li)}" +
                $"\n kqDictionary[{indexMaHoa}].Ri:{ChangeKeyKToBinary(kqDictionary[indexMaHoa].Ri)}" +
                $"\n kqDictionary[{indexMaHoa}].Ei:{ChangeKeyKToBinary(kqDictionary[indexMaHoa].Ei)}" +
                $"\n kqDictionary[{indexMaHoa}].Ei_XOr_Ki:{ChangeKeyKToBinary(kqDictionary[indexMaHoa].Ei_XOr_Ki)}" +
                $"\n kqDictionary[{indexMaHoa}].S_Box:{ChangeKeyKToBinary(kqDictionary[indexMaHoa].S_Box)}" +
                $"\n kqDictionary[{indexMaHoa}].P_S_Box:{ChangeKeyKToBinary(kqDictionary[indexMaHoa].P_S_Box)}" +
                $"\n kqDictionary[{indexMaHoa}].P_Xor_Li:{ChangeKeyKToBinary(kqDictionary[indexMaHoa].P_Xor_Li)}");
            }
            
            Console.WriteLine($"=================");
            // =========ma hoa xong===========

            // set tiep gia tri cho vong lap moi
            indexMaHoa++;

        } while (indexMaHoa <= targetLoop);

        // 32 bit swap

        L0 = kqDictionary[targetLoop].Ri;
        R0 = kqDictionary[targetLoop].Li;
        string swap = L0 + R0;
        swap = ChangeKeyKToBinary(swap);

        string result = CodingWith_IP_Table_1(swap);

        Console.WriteLine($"ket qua sau khi ma hoa: {ChangeBinaryToHexa(result)}");
    }

    public static string CodingWith_IP_Table_1(string input)
    {
        string result = string.Empty;
        foreach (int index in IP_Table_1)
        {
            result += input[index - 1];
        }

        return result;
    }

    public static string CodingWith_EPTable(string input)
    {
        return CodingWithTable(input, EP_Table);
    }

    public static string CodingWithTable(string input, int[] table)
    {
        string output = string.Empty;
        foreach (var index in EP_Table)
        {
            output += input[index - 1];
        }
        return output;
    }


    public static int targetLoop = 16;

    static bool isDebugDoXor = false;
    public static string Do_XOR(string binary1, string binary2)
    {
        int maxLength = Math.Max(binary1.Length, binary2.Length);
        binary1 = binary1.PadLeft(maxLength, '0');
        binary2 = binary2.PadLeft(maxLength, '0');
        string result = string.Empty;
        for (int j = 0; j < maxLength; j++)
        {
            // Chuyển đổi ký tự sang giá trị số nguyên và thực hiện phép XOR
            int bit1 = binary1[j] - '0';
            int bit2 = binary2[j] - '0';
            int xorResult = bit1 ^ bit2;
            WriteDebug(isDebugDoXor, $"bit1: {bit1} | bit2: {bit2} | xorResult: {xorResult}");
            result += Convert.ToString(xorResult, 2);
        }
        return result;
    }

    static bool isDebugTraBangP = false;
    public static string TraBangP(string input)
    {
        string result = "";
        foreach (int index in P_Table) {
            result += input[index - 1];
        }

        WriteDebug(isDebugTraBangP, $"TraBangP: {result}");
        return result;
    }

    private static void WriteDebug(bool isWrite, string mess)
    {
        if (!isWrite) return;

        Console.WriteLine(mess);
    }

    static bool isDebugTraBangSBoxI = false;
    public static string TraBangSBoxI(string ER0_Xor_K1)
    {
        string kq = "";
        for (int i = 0; i < ER0_Xor_K1.Length / 6; i++)
        {
            string sixBit = ER0_Xor_K1.Substring(i * 6, 6);
            WriteDebug(isDebugTraBangSBoxI, $"sixBit: {sixBit}");
            string rowStr = $"{sixBit[0]}{sixBit[sixBit.Length - 1]}";
            WriteDebug(isDebugTraBangSBoxI, $"rowStr: {rowStr}");
            int row = Convert.ToInt32(rowStr, 2);
            int col = Convert.ToInt32(sixBit.Substring(1, 4), 2);

            WriteDebug(isDebugTraBangSBoxI, $"row: {row} | col: {col}");

            int result = -1;
            switch (i)
            {
                case 0:
                    result = S_Box_1[row, col];
                    break;
                case 1:
                    result = S_Box_2[row, col];
                    break;
                case 2:
                    result = S_Box_3[row, col];
                    break;
                case 3:
                    result = S_Box_4[row, col];
                    break;
                case 4:
                    result = S_Box_5[row, col];
                    break;
                case 5:
                    result = S_Box_6[row, col]; 
                    break;
                case 6:
                    result = S_Box_7[row, col]; 
                    break;
                case 7:
                    result = S_Box_8[row, col]; 
                    break;
                default:
                    Console.WriteLine($"ERROR: TraBangSBoxI missing with i > 7 {ER0_Xor_K1}");
                    break;
            }

            WriteDebug(isDebugTraBangSBoxI, $"result: {result}");
            kq += Convert.ToString(result, 2).PadLeft(4, '0');
        }

        WriteDebug(isDebugTraBangSBoxI, $"kq: {kq}");

        return kq;
    }

    string[] SinhKhoa(string k)
    {
        string binary = ChangeKeyKToBinary(k);
        string hoaviPC1 = "";
        foreach (var index in PC1_Table)
        {
            hoaviPC1 += binary[index - 1];
        }
        Console.WriteLine("hoaviPC1:" + hoaviPC1);

        string[] mahoaK = SinhKhoaCon(hoaviPC1);
        foreach (var index in mahoaK)
        {
            Console.WriteLine(index);
        }

        return mahoaK;
    }


    bool isDebugSinhKhoaCon = false;
    string[] SinhKhoaCon(string input)
    {
        string[] keys = new string[dichTraiCnDn.Length];
        for (int i = 0; i < dichTraiCnDn.Length; i++)
        {
            if(isDebugSinhKhoaCon) Console.WriteLine($"SinhKhoaCon i: {i} with input {input} - {input.Length}");
            string resultWithRoundNumber_1 = ChiaDoiVaDichBit(i + 1, input);
            if(isDebugSinhKhoaCon) Console.WriteLine("ChiaDoiVaDichBit:" + resultWithRoundNumber_1);

            string hoaviPC2 = "";
            foreach (var index in PC2_Table)
            {
                hoaviPC2 += resultWithRoundNumber_1[index - 1];
            }

            if (isDebugSinhKhoaCon) Console.WriteLine("hoaviPC2:" + hoaviPC2);
            input = resultWithRoundNumber_1;

            string resultHexaPC2 = ChangeBinaryToHexa(hoaviPC2);
            if (isDebugSinhKhoaCon) Console.WriteLine("resultHexaPC2:" + resultHexaPC2);
            if (isDebugSinhKhoaCon) Console.WriteLine("================================");
            keys[i] = resultHexaPC2;
        }

        return keys;
    }


    public static string ChangeBinaryToHexa(string input)
    {
        string output = "";

        if (input.Length % 4 != 0)
        {
            input = input.PadLeft(input.Length + (4 - input.Length % 4), '0');
        }

        int chunks = input.Length / 4;
        string[] binaryChunks = new string[chunks];
        for (int i = 0; i < chunks; i++)
        {
            binaryChunks[i] = input.Substring(i * 4, 4);
        }

        // Chuyển từng phần tử nhị phân thành số thập lục phân
        foreach (string chunk in binaryChunks)
        {
            int decimalValue = Convert.ToInt32(chunk, 2);
            output += decimalValue.ToString("X");
        }

        return output;
    }

    public static string ChangeKeyKToBinary(string k)
    {
        //Console.WriteLine("key k:" + k);
        string binary = "";
        foreach (var c in k)
        {
            int val = Convert.ToInt32(c.ToString(), 16);
            binary += Convert.ToString(val, 2).PadLeft(4, '0');
        }

        return binary;
    }

    string ChiaDoiVaDichBit(int roundNumber, string input)
    {
        int index = roundNumber - 1;
        string ci = input.Substring(0, input.Length / 2);
        string di = input.Substring(input.Length / 2, input.Length / 2);

        for (int i = 0; i < dichTraiCnDn[index]; i++)
        {
            string c1 = ci[0].ToString();
            ci = ci.Remove(0, 1);
            ci += c1;

            string d1 = di[0].ToString();
            di = di.Remove(0, 1);
            di += d1;
        }

        return ci + di;
    }
}
