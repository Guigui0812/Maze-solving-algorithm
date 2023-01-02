using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Pathfinder_ROHEE_Guillaume_Version_Graph
{
    static class Pathfinder
    {

        private static int Calc_Origin(int[] originXY, Case actualCase)
        {
            int distanceX = 0;
            int distanceY = 0;

            // Calcul de la distance en abscisse selon la valeur de x.
            if (actualCase.X > originXY[0])
            {
                distanceX = actualCase.X - originXY[0];
            }
            else if (actualCase.X < originXY[0])
            {
                distanceX = originXY[0] - actualCase.X;
            }

            // Calcul de la distance en ordonnée selon la valeur de y.
            if (actualCase.Y > originXY[1])
            {
                distanceY = actualCase.Y - originXY[1];
            }
            else if (actualCase.Y < originXY[1])
            {
                distanceY = originXY[1] - actualCase.Y;
            }

            // Addition pour obtenir la distance totale.
            return distanceX + distanceY + 1;
        }


        // Retourne la distance de la case : on la retiendra que si elle est inférieure ou égale à celles préexistantes.
        private static int Calc_Dest(Case actualCase, int[] destXY)
        {
            int distanceX = 0;
            int distanceY = 0;

            // Calcul de la distance en abscisse selon la valeur de x.
            if (actualCase.X > destXY[0])
            {
                distanceX = actualCase.X - destXY[0];
            }
            else if (actualCase.X < destXY[0])
            {
                distanceX = destXY[0] - actualCase.X;
            }

            // Calcul de la distance en ordonnée selon la valeur de y.
            if (actualCase.Y > destXY[1])
            {
                distanceY = actualCase.Y - destXY[1];
            }
            else if (actualCase.Y < destXY[1])
            {
                distanceY = destXY[1] - actualCase.Y;
            }

            // Addition pour obtenir la distance totale.
            return distanceX + distanceY + 1;

        }

        private static void Calc_CaseScore(Case actualCase, int[] destXY, int[] originXY, int weight)
        {
            int originDist = Calc_Origin(originXY, actualCase);
            int destDist = Calc_Dest(actualCase, destXY);

            actualCase.Score = originDist + destDist + weight;
        }

        // Test du voisin de droite.
        private static void CheckNeighborRight(Button[,] Grid, int SizeGrid, List<Case> caseToTest, Case caseTest, int[] originXY, int[] destXY, List<Case> alrdyTest)
        {
            if ((caseTest.X + 1 < SizeGrid && caseTest.Y < SizeGrid) && (caseTest.X >= 0 && caseTest.Y >= 0))
            {
                Case tmpCase = new Case();
                tmpCase.X = caseTest.X + 1;
                tmpCase.Y = caseTest.Y;

                if(Grid[tmpCase.X, tmpCase.Y].Text != "D" && Grid[tmpCase.X, tmpCase.Y].Text != "A")
                {
                    string caseStr = Grid[tmpCase.X, tmpCase.Y].Text;
                    int weight = Convert.ToInt32(caseStr);
                    Calc_CaseScore(tmpCase, destXY, originXY, weight);
                }

                // Test d'éligibilité de la case voisine : non obstacle.
                if (Grid[tmpCase.X, tmpCase.Y].BackColor != Color.Gray)
                {
                    // Si la case n'est dans aucune des listes elle est ajoutée aux cases à tester, aux chemins possibles et affichée sur la grille.
                    if (!alrdyTest.Any(c => c.X == tmpCase.X && c.Y == tmpCase.Y) && !caseToTest.Any(c => c.X == tmpCase.X && c.Y == tmpCase.Y))
                    {
                        tmpCase.chemin = new List<Case>(caseTest.chemin);
                        tmpCase.chemin.Add(tmpCase);
                        caseToTest.Add(tmpCase);
                    }
                }
            }
        }

        // Test du voisin du dessus.
        private static void CheckNeighborTop(Button[,] Grid, int SizeGrid, List<Case> caseToTest, Case caseTest, int[] originXY, int[] destXY, List<Case> alrdyTest)
        {
            if ((caseTest.X < SizeGrid && caseTest.Y < SizeGrid) && (caseTest.X >= 0 && caseTest.Y - 1 >= 0))
            {

                Case tmpCase = new Case();
                tmpCase.X = caseTest.X;
                tmpCase.Y = caseTest.Y - 1;

                if (Grid[tmpCase.X, tmpCase.Y].Text != "D" && Grid[tmpCase.X, tmpCase.Y].Text != "A")
                {
                    string caseStr = Grid[tmpCase.X, tmpCase.Y].Text;
                    int weight = Convert.ToInt32(caseStr);
                    Calc_CaseScore(tmpCase, destXY, originXY, weight);
                }

                // Test d'éligibilité de la case voisine : non obstacle.
                if (Grid[tmpCase.X, tmpCase.Y].BackColor != Color.Gray)
                {
                    // Si la case n'est dans aucune des listes elle est ajoutée aux cases à tester, aux chemins possibles et affichée sur la grille.
                    if (!alrdyTest.Any(c => c.X == tmpCase.X && c.Y == tmpCase.Y) && !caseToTest.Any(c => c.X == tmpCase.X && c.Y == tmpCase.Y))
                    {
                        tmpCase.chemin = new List<Case>(caseTest.chemin);
                        tmpCase.chemin.Add(tmpCase);
                        caseToTest.Add(tmpCase);
                    }
                }
            }
        }

        // Test du voisin de gauche.
        private static void CheckNeighborLeft(Button[,] Grid, int SizeGrid, List<Case> caseToTest, Case caseTest, int[] originXY, int[] destXY, List<Case> alrdyTest)
        {

            if ((caseTest.X < SizeGrid && caseTest.Y < SizeGrid) && (caseTest.X - 1 >= 0 && caseTest.Y >= 0))
            {
                Case tmpCase = new Case();
                tmpCase.X = caseTest.X - 1;
                tmpCase.Y = caseTest.Y;

                if (Grid[tmpCase.X, tmpCase.Y].Text != "D" && Grid[tmpCase.X, tmpCase.Y].Text != "A")
                {
                    string caseStr = Grid[tmpCase.X, tmpCase.Y].Text;
                    int weight = Convert.ToInt32(caseStr);
                    Calc_CaseScore(tmpCase, destXY, originXY, weight);
                }

                // Test d'éligibilité de la case voisine : non obstacle.
                if (Grid[tmpCase.X, tmpCase.Y].BackColor != Color.Gray)
                {
                    // Si la case n'est dans aucune des listes elle est ajoutée aux cases à tester, aux chemins possibles et affichée sur la grille.
                    if (!alrdyTest.Any(c => c.X == tmpCase.X && c.Y == tmpCase.Y) && !caseToTest.Any(c => c.X == tmpCase.X && c.Y == tmpCase.Y))
                    {
                        tmpCase.chemin = new List<Case>(caseTest.chemin);
                        tmpCase.chemin.Add(tmpCase);
                        caseToTest.Add(tmpCase);
                    }
                }
            }
        }

        // Test du voisin du dessous.
        private static void CheckNeighborBot(Button[,] Grid, int SizeGrid, List<Case> caseToTest, Case caseTest, int[] originXY, int[] destXY, List<Case> alrdyTest)
        {

            if ((caseTest.X < SizeGrid && caseTest.Y + 1 < SizeGrid) && (caseTest.X >= 0 && caseTest.Y >= 0))
            {

                Case tmpCase = new Case();
                tmpCase.X = caseTest.X;
                tmpCase.Y = caseTest.Y + 1;

                if (Grid[tmpCase.X, tmpCase.Y].Text != "D" && Grid[tmpCase.X, tmpCase.Y].Text != "A")
                {
                    string caseStr = Grid[tmpCase.X, tmpCase.Y].Text;
                    int weight = Convert.ToInt32(caseStr);
                    Calc_CaseScore(tmpCase, destXY, originXY, weight);
                }

                // Test d'éligibilité de la case voisine : non obstacle.
                if (Grid[tmpCase.X, tmpCase.Y].BackColor != Color.Gray)
                {
                    // Si la case n'est dans aucune des listes elle est ajoutée aux cases à tester, aux chemins possibles et affichée sur la grille.
                    if (!alrdyTest.Any(c => c.X == tmpCase.X && c.Y == tmpCase.Y) && !caseToTest.Any(c => c.X == tmpCase.X && c.Y == tmpCase.Y))
                    {
                        tmpCase.chemin = new List<Case>(caseTest.chemin);
                        tmpCase.chemin.Add(tmpCase);
                        caseToTest.Add(tmpCase);
                    }
                }
            }
        }

        // Boucle permettant de trouver les cases voisines pour les tester ensuite.
        private static List<Case> Identifiy_Neigh(Button[,] Grid, int SizeGrid, List<Case> caseToTest, Case caseTest, int[] originXY, int[] destXY, List<Case> alrdyTest)
        {
            CheckNeighborRight(Grid, SizeGrid, caseToTest, caseTest, originXY, destXY, alrdyTest);
            CheckNeighborBot(Grid, SizeGrid, caseToTest, caseTest, originXY, destXY, alrdyTest);
            CheckNeighborLeft(Grid, SizeGrid, caseToTest, caseTest, originXY, destXY, alrdyTest);
            CheckNeighborTop(Grid, SizeGrid, caseToTest, caseTest, originXY, destXY, alrdyTest);

            return caseToTest;
        }

        // Affiche une dernière fois la grille et affiche le meilleur chemin.
        private static void Display_Result(Button[,] Grid, Case caseTest)
        {
            List<Case> FinalPath = caseTest.chemin;

            // Affiche le chemin dans l'interface.
            for (int i = 0; i < FinalPath.Count; i++)
            {
                Case tmpCase = FinalPath[i];
                Grid[tmpCase.X, tmpCase.Y].BackColor = Color.Green;
            }
        }

        // Boucle principale de l'algo.
        public static void MainLoop(Button[,] Grid, int sizeGrid, int[] originXY, int[] destXY)
        {
            bool noPath = false;
            // Initialise la première case dont les voisins vont être testés. 
            Case caseTest = new Case();
            caseTest.X = originXY[0];
            caseTest.Y = originXY[1];

            // Place les caractère pour l'interface.
            Grid[originXY[0], originXY[1]].Text = "D";
            Grid[destXY[0], destXY[1]].Text = "A";

            List<Case> caseToTest = new List<Case>();
            List<Case> alrdyTest = new List<Case>();

            // Ajout de la première case à la liste des cases à tester. 
            caseTest.chemin = new List<Case>() { caseTest };

            while ((caseTest.X != destXY[0] || caseTest.Y != destXY[1]) || noPath == true)
            {
                // Appel méthode permettant de tester tous les voisins d'une case. 
                caseToTest = Identifiy_Neigh(Grid, sizeGrid, caseToTest, caseTest, originXY, destXY, alrdyTest);

                // Ajout de la case testée à la liste des cases déjà testées. 
                alrdyTest.Add(caseTest);

                
                // Structure d'exception gérant l'impossibilité de trouver un chemin valide à emprunter. 
                try
                {
                    // Récupération de la case possédant le plus petit score.
                    int lowestScore = caseToTest.Min(Case => Case.Score);
                    caseTest = caseToTest.First(Case => Case.Score == lowestScore);
                }
                catch
                {
                    // Message d'erreur. 
                    MessageBox.Show("Aucun chemin ne mène à la destination. Réinitialisez le programme.", "Aucun chemin");
                    noPath = true;
                }
                
                // Suppression de la case dans la liste des cases à tester prochainement. 
                if (caseToTest.Count() > 0)
                {
                    caseToTest.Remove(caseTest);
                }
            }

            // Affichage du chemin uniquement si il existe. 
            if (!noPath)
            {
                Display_Result(Grid, caseTest);
            }
        }
    }
}
