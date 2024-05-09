using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class DecodeDES{

    public class Element
    {
        public string subkey;
        public string Li;
        public string Ri;
        public string Ei;
        public string Ei_XOr_Ki;
        public string S_Box;
        public string P_S_Box;
        public string P_Xor_Li;

        public void Write()
        {
            if (true)
            {
                if (!string.IsNullOrEmpty(subkey)) Console.WriteLine($"subkey: {DES.ChangeKeyKToBinary(subkey)}");
                if (!string.IsNullOrEmpty(Li)) Console.WriteLine($"Li: {Li}");
                if (!string.IsNullOrEmpty(Ri)) Console.WriteLine($"Ri: {Ri}");
                if (!string.IsNullOrEmpty(Ei)) Console.WriteLine($"Ei: {Ei}");
                if (!string.IsNullOrEmpty(Ei_XOr_Ki)) Console.WriteLine($"Ei_XOr_Ki: {Ei_XOr_Ki}");
                if (!string.IsNullOrEmpty(S_Box)) Console.WriteLine($"S_Box: {S_Box}");
                if (!string.IsNullOrEmpty(P_S_Box)) Console.WriteLine($"P_S_Box: {P_S_Box}");
                if (!string.IsNullOrEmpty(P_Xor_Li)) Console.WriteLine($"P_Xor_Li: {P_Xor_Li}");
            }
            else
            {
                if (!string.IsNullOrEmpty(subkey)) Console.WriteLine($"subkey: {subkey}");
                if (!string.IsNullOrEmpty(Li)) Console.WriteLine($"Li: {DES.ChangeKeyKToBinary(Li)}");
                if (!string.IsNullOrEmpty(Ri)) Console.WriteLine($"Ri: {DES.ChangeKeyKToBinary(Ri)}");
                if (!string.IsNullOrEmpty(Ei)) Console.WriteLine($"Ei: {DES.ChangeKeyKToBinary(Ei)}");
                if (!string.IsNullOrEmpty(Ei_XOr_Ki)) Console.WriteLine($"Ei_XOr_Ki: {DES.ChangeKeyKToBinary(Ei_XOr_Ki)}");
                if (!string.IsNullOrEmpty(S_Box)) Console.WriteLine($"S_Box: {DES.ChangeKeyKToBinary(S_Box)}");
                if (!string.IsNullOrEmpty(P_S_Box)) Console.WriteLine($"P_S_Box: {DES.ChangeKeyKToBinary(P_S_Box)}");
                if (!string.IsNullOrEmpty(P_Xor_Li)) Console.WriteLine($"P_Xor_Li: {DES.ChangeKeyKToBinary(P_Xor_Li)}");
            }
           
        }
    }

    public DecodeDES (string desText, Dictionary<int, (string subkey, string Li, string Ri, string Ei, string Ei_XOr_Ki, string S_Box, string P_S_Box, string P_Xor_Li)> kqDictionary)
    {
        foreach (var item in kqDictionary)
        {
            Console.WriteLine(item.Value.subkey);
        }

        Dictionary<int, Element> dic = new Dictionary<int, Element>();
        foreach (var item in kqDictionary)
        {
            dic.Add(item.Key, new Element() { subkey = item.Value.subkey });
        }

        DoDecode(desText, ref dic);
    }

    private void DoDecode(string desText, ref Dictionary<int, Element> kqDictionary)
    {
        if (kqDictionary.Count <= 0) return;

        string binary = DES.ChangeKeyKToBinary(desText);
        Console.WriteLine($"binary: {binary}");

        string result = DecodeWithIPTable_1(binary);
        Console.WriteLine($"ket qua sau khi ma hoa: {result}");
        
        string L18 = result.Substring(0, result.Length / 2);
        string R18 = result.Substring(result.Length / 2, result.Length / 2);

        int index = DES.targetLoop;
        kqDictionary[index].Li = R18;
        kqDictionary[index].Ri = L18;
        kqDictionary[index].Write();
        do
        {
            index--;

            Console.WriteLine($"-----index{index}------");
            if (!kqDictionary.TryGetValue(index, out var x))
            {
                kqDictionary.Add(0, new Element());
                x = kqDictionary[index];
                Console.WriteLine($"missing kqDictionary with {index}");
            }

            x.Ri = kqDictionary[index + 1].Li;
            x.Ei = DES.CodingWith_EPTable(x.Ri);
            x.Ei_XOr_Ki = DES.Do_XOR(x.Ei, DES.ChangeKeyKToBinary(kqDictionary[index + 1].subkey));
            x.S_Box = DES.TraBangSBoxI(x.Ei_XOr_Ki);
            x.P_S_Box = DES.TraBangP(x.S_Box);
            x.Li = DES.Do_XOR(x.P_S_Box, kqDictionary[index + 1].Ri);

            kqDictionary[index] = x;
            x.Write();
            Console.WriteLine($"-----==========------");

        } while (index > 0);

        string resultEncode = DecodeWithIPTable(kqDictionary[0].Li + kqDictionary[0].Ri);
        Console.WriteLine("resultEncode: " + DES.ChangeBinaryToHexa(resultEncode));
    }
    public static string DecodeWithEPTable(string output)
    {
        return DecodeTable(output, DES.EP_Table);
    }

    public static string DecodeWithIPTable(string output)
    {
        return DecodeTable(output, DES.IP_Table);
    }

    public static string DecodeWithIPTable_1(string output)
    {
        return DecodeTable(output, DES.IP_Table_1);
    }

    private static string DecodeTable(string output, int[] table)
    {
        char[] result = new char[table.Length];
        int i = 0;
        foreach (int index in table)
        {
            result[index - 1] = output[i];
            i++;
        }

        string originalInput = string.Empty;
        foreach (char c in result)
        {
            originalInput += c;
        }

        return originalInput;
    }

}
