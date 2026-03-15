while (true)
{
    Console.Write("Введите первую строку: ");
    string firstString = Console.ReadLine() ?? "";

    if (firstString == "exit")
    {
        break;
    }

    Console.Write("Введите вторую строку: ");
    string secondString = Console.ReadLine() ?? "";

    WriteDistance(firstString, secondString);
    Console.WriteLine();
}

static void WriteDistance(string firstString, string secondString)
{
    int distance = Distance(firstString, secondString);
    Console.WriteLine("Расстояние Дамерау-Левенштейна: " + distance);
}

static int Distance(string str1Param, string str2Param)
{
    int str1Len = str1Param.Length;
    int str2Len = str2Param.Length;

    if (str1Len == 0 && str2Len == 0)
    {
        return 0;
    }

    if (str1Len == 0)
    {
        return str2Len;
    }

    if (str2Len == 0)
    {
        return str1Len;
    }

    string str1 = str1Param.ToUpper();
    string str2 = str2Param.ToUpper();

    int[,] matrix = new int[str1Len + 1, str2Len + 1];

    for (int i = 0; i <= str1Len; i++)
    {
        matrix[i, 0] = i;
    }

    for (int j = 0; j <= str2Len; j++)
    {
        matrix[0, j] = j;
    }

    for (int i = 1; i <= str1Len; i++)
    {
        for (int j = 1; j <= str2Len; j++)
        {
            int symbolEqual = 1;

            if (str1[i - 1] == str2[j - 1])
            {
                symbolEqual = 0;
            }

            int insertValue = matrix[i, j - 1] + 1;
            int deleteValue = matrix[i - 1, j] + 1;
            int replaceValue = matrix[i - 1, j - 1] + symbolEqual;

            matrix[i, j] = Math.Min(Math.Min(insertValue, deleteValue), replaceValue);

            if (i > 1 &&
                j > 1 &&
                str1[i - 1] == str2[j - 2] &&
                str1[i - 2] == str2[j - 1])
            {
                matrix[i, j] = Math.Min(matrix[i, j], matrix[i - 2, j - 2] + symbolEqual);
            }
        }
    }

    return matrix[str1Len, str2Len];
}
