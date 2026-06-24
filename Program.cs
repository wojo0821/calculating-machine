class Program
{
    static void Main()
    {
        while (true)
        {
            Console.Write("공식을 입력하세요 : ");
            string s = Console.ReadLine();
            if (s == "")
            {
                return;
            }
            for (int i = 0; i < s.Length; i++)
            {
                char[] cArray = s.ToCharArray();
                if (cArray[i] == '(')
                {
                    float parenSAnswer;
                    int openParenIndex = i;
                    int closeParenIndex = 0;
                    string beforeOpenParenS = "";
                    string afterCloseParenS = "";
                    string parenS = "";
                    for (int j = i + 1; j < cArray.Length; j++)
                    {
                        if (cArray[j] == ')')
                        {
                            closeParenIndex = j;
                            break;
                        }
                    }
                    for (int j = openParenIndex + 1; j < closeParenIndex; j++)
                    {
                        parenS += cArray[j];
                    }
                    for (int j = 0; j < openParenIndex; j++)
                    {
                        beforeOpenParenS += cArray[j];
                    }
                    for (int j = closeParenIndex + 1; j < cArray.Length; j++)
                    {
                        afterCloseParenS += cArray[j];
                    }

                    parenSAnswer = Calculate(parenS);
                    s = beforeOpenParenS + parenSAnswer + afterCloseParenS;
                }
            }
            if (Calculate(s) != 0) Console.WriteLine(Calculate(s));
        }
    }


    static float Calculate(string s)
    {
        float num = 0;
        List<float> fList = new List<float>();
        List<char> cList = new List<char>();
        List<string> sList = new List<string>();
        sList.Add("");
        int sListCount = 0;
        char[] cArray = s.ToCharArray();

        for (int i = 0; i < cArray.Length; i++)
        {
            if (char.IsDigit(cArray[i]))
            {
                sList[sListCount] += cArray[i];
            }
            else if (cArray[i] == ' ' && i == 0)
            {
                for (int j = i + 1; j < cArray.Length; j++)
                {
                    if (cArray[j] == ' ') continue;
                    else if (cArray[j] == '-' || cArray[j] == '+')
                    {
                        sList[sListCount] += cArray[j];
                        break;
                    }
                    else break;
                }
            }
            else if ((cArray[i] == '-' || cArray[i] == '+') && i == 0)
            {
                sList[sListCount] += cArray[i];
            }
            else if (cArray[i] == '*' || cArray[i] == '/' || cArray[i] == '%' || cArray[i] == '+' || cArray[i] == '-' || cArray[i] == '^')
            {
                if (cArray[i] == '-' || cArray[i] == '+')
                {
                    for (int j = i - 1; j >= 0; j--)
                    {
                        if (cArray[j] == ' ') continue;
                        else if (cArray[j] == '*' || cArray[j] == '/' || cArray[j] == '%' || cArray[j] == '+' || cArray[j] == '-' || cArray[j] == '^')
                        {
                            sList[sListCount] += cArray[i];
                            break;
                        }
                        else if (!char.IsDigit(cArray[j]))
                        {
                            sList[sListCount] += cArray[i];
                            break;
                        }
                        else
                        {
                            cList.Add(cArray[i]);
                            fList.Add(float.Parse(sList[sListCount]));
                            sList.Add("");
                            sListCount++;
                            break;
                        }
                    }
                }
                else
                {
                    cList.Add(cArray[i]);
                    fList.Add(float.Parse(sList[sListCount]));
                    sList.Add("");
                    sListCount++;
                }
            }
            else if (cArray[i] == '.')
            {
                sList[sListCount] += cArray[i];
            }
            else if (cArray[i] == ' ') { }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
                return num;
            }
        }
        if (sList[sListCount] != "")
        {
            fList.Add(float.Parse(sList[sListCount]));
        }
        else
        {
            Console.WriteLine("잘못된 입력입니다.");
            return num;
        }
        if (fList.Count == 1) return fList[0];

        for (int i = 0; i < cList.Count; i++)
        {
            if (cList.Count <= 1) break;
            if (cList[i] == '*' || cList[i] == '/' || cList[i] == '%')
            {
                if (cList[i] == '*') fList.Add(fList[i] * fList[i + 1]);
                if (cList[i] == '/') fList.Add(fList[i] / fList[i + 1]);
                if (cList[i] == '%') fList.Add(fList[i] % fList[i + 1]);
                fList.Remove(fList[i + 1]);
                fList.Remove(fList[i]);
                cList.Remove(cList[i]);
            }
        }

        for (int i = 0; i < cList.Count; i++)
        {
            if (i == 0)
            {
                if (cList[i] == '+') num = fList[i] + fList[i + 1];
                if (cList[i] == '-') num = fList[i] - fList[i + 1];
                if (cList[i] == '*') num = fList[i] * fList[i + 1];
                if (cList[i] == '/') num = fList[i] / fList[i + 1];
                if (cList[i] == '%') num = fList[i] % fList[i + 1];
                if (cList[i] == '^') num = (float)Math.Pow(fList[i], fList[i + 1]);
            }
            else
            {
                if (cList[i] == '+') num = num + fList[i + 1];
                if (cList[i] == '-') num = num - fList[i + 1];
                if (cList[i] == '*') num = num * fList[i + 1];
                if (cList[i] == '/') num = num / fList[i + 1];
                if (cList[i] == '%') num = num % fList[i + 1];
                if (cList[i] == '^') num = (float)Math.Pow(num, fList[i + 1]);
            }
        }
        return num;
    }
}
