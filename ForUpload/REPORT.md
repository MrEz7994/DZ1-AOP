# Отчет по домашнему заданию №1

## Тема

Реализация алгоритма поиска с опечатками на языке C#.

## Постановка задачи

Необходимо создать консольное приложение на C#, которое:

- запрашивает у пользователя две строки;
- вычисляет расстояние между ними;
- выводит результат;
- работает в бесконечном цикле;
- завершает работу, если первая строка равна `exit`.

## Описание программы

Программа реализована с помощью операторов верхнего уровня.

Основная логика:

1. Ввод первой строки.
2. Проверка на `exit`.
3. Ввод второй строки.
4. Вычисление расстояния Дамерау-Левенштейна.
5. Вывод результата.
6. Повторение цикла.

Для вычисления расстояния используется матричный алгоритм Вагнера-Фишера.

## Текст программы

```csharp
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
```

## Результаты выполнения

```text
Введите первую строку: пример
Введите вторую строку: пример12
Расстояние Дамерау-Левенштейна: 2

Введите первую строку: прИМер
Введите вторую строку: прМИер
Расстояние Дамерау-Левенштейна: 1

Введите первую строку: exit
```

## Ответы на контрольные вопросы

### 1. Что такое расстояние Левенштейна?

Расстояние Левенштейна — это минимальное количество операций редактирования, необходимых для преобразования одной строки в другую. Учитываются вставка, удаление и замена символов.

### 2. Что такое расстояние Дамерау-Левенштейна?

Расстояние Дамерау-Левенштейна — это расстояние Левенштейна, дополненное операцией транспозиции, то есть перестановкой двух соседних символов.

### 3. Объясните алгоритм Вагнера-Фишера для вычисления расстояния Дамерау-Левенштейна.

Алгоритм Вагнера-Фишера строит матрицу, в которой по строкам и столбцам откладываются символы двух строк. Каждая ячейка содержит минимальную стоимость преобразования части первой строки в часть второй. Для вычисления берутся минимум из вставки, удаления и замены. Если возможно, дополнительно проверяется транспозиция соседних символов. Ответ находится в правой нижней ячейке матрицы.
