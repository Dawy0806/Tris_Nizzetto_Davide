using Spectre.Console;
var scelta = string.Empty;
do
{
    scelta = AnsiConsole.Prompt(
        new SelectionPrompt<string>()
            .Title("Menu")
            .PageSize(10)
            .AddChoices(new[] {
            "Avvia Partita", "Esci"
            }));
    if (scelta == "Avvia Partita")
    {
        int[,] valori = new int[3, 3]{
                                {1, 1, 1},
                                {1, 1, 1},
                                {1, 1, 1}
                            };
        int[,] payoff = new int[3, 3]{
                                {0, 0, 0},
                                {0, 0, 0},
                                {0, 0, 0}
                            };
        char[,] partita = new char[3, 3]{
                                {'N', 'N', 'N'},
                                {'N', 'N', 'N'},
                                {'N', 'N', 'N'}
                            };
        for (int cont = 0; cont < 10; cont++)
        {
            (int, int)[] salvavaloriperX = new (int, int)[5];
            (int, int)[] salvavaloriperO = new (int, int)[4];
            int k = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (cont % 2 == 0)
                    {
                        if (partita[i, j] == 'X')
                        {
                            valori[i, j] = 1;
                            salvavaloriperX[k].Item1 = i;
                            salvavaloriperX[k].Item2 = j;
                            k++;
                        }
                    }
                    else
                    {
                        if (partita[i, j] == 'O')
                        {
                            valori[i, j] = 1;
                            salvavaloriperO[k].Item1 = i;
                            salvavaloriperO[k].Item2 = j;
                            k++;
                        }
                    }
                }
            }
            for (int i = 0; i < 3; i++)
            {
                if (valori[i, 0] == 1 && valori[i, 1] == 1 && valori[i, 2] == 1)
                {
                    payoff[i, 0] += 3;
                }
                if (valori[0, i] == 1 && valori[1, i] == 1 && valori[2, i] == 1)
                {
                    payoff[0, i] += 3;
                }
                if (valori[0, 1] == 1 && valori[1, 1] == 1 && valori[2, 1] == 1)
                {
                    payoff[i, 1] += 3;
                }
                if (valori[0, 0] == 1 && valori[1, 1] == 1 && valori[2, 2] == 1)
                {
                    payoff[i, i] += 3;
                }
            }
            for (int i = 2; i >= 0; i--)
            {
                if (valori[i, 0] == 1 && valori[i, 1] == 1 && valori[i, 2] == 1)
                {
                    payoff[i, 2] += 3;
                }
                if (valori[0, i] == 1 && valori[1, i] == 1 && valori[2, i] == 1)
                {
                    payoff[2, i] += 3;
                }
                if (valori[1, 0] == 1 && valori[1, 1] == 1 && valori[1, 2] == 1)
                {
                    payoff[1, i] += 3;
                }
            }
            if (valori[0, 2] == 1 && valori[1, 1] == 1 && valori[2, 0] == 1)
            {
                payoff[0, 2] += 3;
                payoff[1, 1] += 3;
                payoff[2, 0] += 3;
            }
            for (int i = 0; i < 3; i++)
            {
                //righe
                if (valori[i, 0] == 0 && valori[i, 1] == 0)
                {
                    payoff[i, 2] = 100;
                }
                if (valori[i, 0] == 0 && valori[i, 2] == 0)
                {
                    payoff[i, 1] = 100;
                }

                if (valori[i, 1] == 0 && valori[i, 2] == 0)
                {
                    payoff[i, 0] = 100;
                }
                //colonne
                if (valori[0, i] == 0 && valori[1, i] == 0)
                {
                    payoff[2,i] = 100;
                }
                if (valori[0, 1] == 0 && valori[2, i] == 0)
                {
                    payoff[1, i] = 100;
                }

                if (valori[1, i] == 0 && valori[2, i] == 0)
                {
                    payoff[0, i] = 100;
                }
            }
            //diagonale sinistra
            if (valori[1, 1] == 0 && valori[2, 2] == 0)
            {
                payoff[0, 0] = 100;
            }
            if (valori[0, 0] == 0 && valori[2, 2] == 0)
            {
                payoff[1, 1] = 100;
            }

            if (valori[0, 0] == 0 && valori[1, 1] == 0)
            {
                payoff[2, 2] = 100;
            }
            //diagonale destra
            if (valori[0, 2] == 0 && valori[1, 1] == 0)
            {
                payoff[2, 0] = 100;
            }
            if (valori[2, 0] == 0 && valori[1, 1] == 0)
            {
                payoff[0, 2] = 100;
            }
            if (valori[2, 0] == 0 && valori[0, 2] == 0)
            {
                payoff[1, 1] = 100;
            }
            for (int i = 0; i < k; i++)
            {
                if (cont % 2 == 0)
                {
                    valori[salvavaloriperX[i].Item1, salvavaloriperX[i].Item2] = 0;
                    payoff[salvavaloriperX[i].Item1, salvavaloriperX[i].Item2] = 0;
                }
                else
                {
                    valori[salvavaloriperO[i].Item1, salvavaloriperO[i].Item2] = 0;
                    payoff[salvavaloriperO[i].Item1, salvavaloriperO[i].Item2] = 0;
                }
            }
            int modifica = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (payoff[i, j] > modifica)
                    {
                        modifica = payoff[i, j];
                    }
                }
            }
            bool flag = false;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (payoff[i, j] == modifica && valori[i, j] == 1)
                    {
                        if (cont % 2 == 0)
                        {
                            partita[i, j] = 'X';
                            valori[i, j] = 0;
                            flag = true;
                        }
                        else
                        {
                            partita[i, j] = 'O';
                            valori[i, j] = 0;
                            flag = true;
                        }
                        break;
                    }
                }
                if (flag)
                {
                    break;
                }
            }
            flag = false;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Console.Write(partita[i, j] + " ");
                    payoff[i, j] = 0;
                }
                Console.WriteLine("");
            }
            Console.WriteLine("");
            Console.WriteLine("");
        }
        Console.ReadKey();
        Console.Clear();
    }
} while (scelta != "Esci");