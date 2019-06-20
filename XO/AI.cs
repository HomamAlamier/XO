using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XO
{
    class AI
    {
        static int RecursiveScore = 0;
        static int m_Score;
        public enum difficulty { Easy = 0, Hard = 9 }
        
        public static int[] GetFromWinArray(int x)
        {
            int[,] winsArray = new int[8, 3]
            {
            {0,4,8 },
            {2,4,6 },
            {0,3,6 },
            {1,4,7},
            {2,5,8 },
            {0,1,2 },
            {3,4,5 },
            {6,7,8 },
            };
            int[] t = new int[3];
            for (int i = 0; i < 3; i++)
            {
                t[i] = winsArray[x, i];
            }
            return t;
        }
        static int GetScoreForOneLine(GFX.Player[] values, bool m_TurnForPlayerX)
        {
            int countX = 0, countO = 0;
            foreach (GFX.Player v in values)
            {
                if (v == GFX.Player.X)
                    countX++;
                else if (v == GFX.Player.O)
                    countO++;
            }


            //The player who has turn should have more advantage.
            //What we should have done
            int advantage = 1;
            if (countO == 0)
            {
                if (m_TurnForPlayerX)
                    advantage = 3;
                return -(int)System.Math.Pow(10, countX) * advantage;
            }
            else if (countX == 0)
            {
                if (!m_TurnForPlayerX)
                    advantage = 3;
                return (int)System.Math.Pow(10, countO) * advantage;
            }
            return 0;
        }

        static void ComputeScore(GFX.Player[] board, bool ifX)
        {
            int ret = 0;
            int[,] lines = { { 0, 1, 2 },
                           { 3, 4, 5 },
                           { 6, 7, 8 },
                           { 0, 3, 6 },
                           { 1, 4, 7 },
                           { 2, 5, 8 },
                           { 0, 4, 8 },
                           { 2, 4, 6 }
                           };

            for (int i = lines.GetLowerBound(0); i <= lines.GetUpperBound(0); i++)
            {
                ret += GetScoreForOneLine(new GFX.Player[] { board[lines[i, 0]], board[lines[i, 1]], board[lines[i, 2]] }, ifX);
            }
            m_Score = ret;
        }

        public static int GetNextMove(difficulty dif,GFX.Player p = GFX.Player.O)
        {
            int d;
            if (dif == difficulty.Easy) d = 1;
            else d = 9;
            GFX.Player[] b;
            GFX.Player[] old = Logic.board;
            minimax(d, int.MinValue + 1, int.MaxValue - 1, true, p, Logic.board, out b);
            if (b != null) Logic.board = b;
            for (int i = 0; i < 9; i++)
            {
                if (old[i] != b[i]) return i;
            }
            return -1;
        }
        public static int minimax(int depth, int alpha, int beta, bool maxing, GFX.Player ai, GFX.Player[] board, out GFX.Player[] bestOut)
        {
            bestOut = null;

            if (checkWin(board, ai) == Logic.WinState.Win || checkWin(board, ai) == Logic.WinState.Draw || depth == 0)
            {
                RecursiveScore = m_Score;
                return m_Score;
            }
            // System.Diagnostics.Debug.WriteLine("Minimaxing...");
            for (int i = 0; i < 9; i++)
            {
                //  if (depth == 0) break;
                if (board[i] == GFX.Player.Empty)
                {
                    GFX.Player[] newboard = GetChild(board, ai, i);
                    GFX.Player[] dummy;
                    int sc = minimax(depth - 1, alpha, beta, !maxing, OppesitePlayer(ai), newboard, out dummy);
                    if (!maxing)
                    {
                        if (beta > sc)
                        {
                            beta = sc;
                            bestOut = newboard;
                            if (alpha >= beta) break;
                        }
                    }
                    else
                    {
                        if (alpha < sc)
                        {
                            alpha = sc;
                            bestOut = newboard;
                            if (alpha >= beta)
                            {
                                break;
                            }
                        }
                    }
                }
            }

            RecursiveScore = maxing ? alpha : beta;
            return RecursiveScore;
        }
        public static GFX.Player OppesitePlayer(GFX.Player p)
        {
            if (p == GFX.Player.X) return GFX.Player.O;
            else return GFX.Player.X;
        }
        public static GFX.Player[] GetChild(GFX.Player[] board, GFX.Player player, int i)
        {
            GFX.Player[] p = Clone(board);
            p[i] = player;
            bool ifX;
            if (player == GFX.Player.X) ifX = true;
            else ifX = false;
            ComputeScore(p, ifX);
            return p;

        }
        public static GFX.Player[] Clone(GFX.Player[] b)
        {
            GFX.Player[] t = new GFX.Player[b.Length];
            for (int i = 0; i < b.Length; i++)
            {
                t[i] = b[i];
            }
            return t;
        }
        public static Logic.WinState checkWin(GFX.Player[] board, GFX.Player p)
        {
            // int[] temp = new int[3];
            bool full = true;
            for (int i = 0; i < 8; i++)
            {
                int[] z = GetFromWinArray(i);
                if (board[z[0]] == board[z[1]] && board[z[0]] == board[z[2]] && board[z[0]] != GFX.Player.Empty)
                {
                    return Logic.WinState.Win;
                }
            }
            for (int i = 0; i < 9; i++)
            {
                if (board[i] == GFX.Player.Empty)
                {
                    full = false;
                    break;
                }
            }
            if (full == true) return Logic.WinState.Draw;
            return Logic.WinState.Nothing;
        }

    }
}
