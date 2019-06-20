using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
namespace XO
{
    public class GFX
    {
        int thirdWidth;
        int thirdHeight;
        int allHeight;
        int allWidth;
        public Graphics g;
        public Pen dPen;
        Color dColor;
        public enum Player { Empty = 0, X = 1, O = 2 };
        Box[] boardBox = new Box[9];
        struct Box
        {
            public int x1;
            public int x2;
            public int y1;
            public int y2;
        }
        public GFX(Form GFXForm)
        {
            thirdWidth = GFXForm.Width / 3;
            thirdHeight = GFXForm.Height / 3; thirdHeight -= 10;
            g = GFXForm.CreateGraphics();
            allHeight = GFXForm.Height;
            allWidth = GFXForm.Width;
            dColor = GFXForm.BackColor;
            dPen = new Pen(Color.Black);
            dPen.Width = 3;
            for (int q = 0; q < 9; q++)
            {
                boardBox[q] = new Box();
            }
            int y = 1; int i = 0;
            do
            {
                for (int j = 1; j <= 3; j++)
                {
                    boardBox[i].x1 = thirdWidth * (j - 1);
                    boardBox[i].y1 = thirdHeight * (y - 1);
                    boardBox[i].x2 = thirdWidth * j;
                    boardBox[i].y2 = thirdHeight * y;
                    i++;
                }
                y++;
            } while (i < 9);
        }
        public void DrawBoard(bool clear = true)
        {
            int x1, y1, x2, y2;
            if (clear == true) g.Clear(dColor);
            //Horizontall Line's
            x1 = 0; y1 = thirdHeight; x2 = allWidth; y2 = y1;
            g.DrawLine(dPen, x1, y1, x2, y2);
            y1 = y2 = thirdHeight * 2;
            g.DrawLine(dPen, x1, y1, x2, y2);
            //Vertical Line's
            x1 = thirdWidth; y1 = 0; x2 = x1; y2 = allHeight;
            g.DrawLine(dPen, x1, y1, x2, y2);
            x1 = x1 * 2; x2 = x1;
            g.DrawLine(dPen, x1, y1, x2, y2);
        }
        public int getBox(int x, int y)
        {
            for (int i = 0; i < 9; i++)
            {
                if (x > boardBox[i].x1 && x < boardBox[i].x2 && y > boardBox[i].y1 && y < boardBox[i].y2)
                {
                    return i;
                }
            }
            return -1;
        }
        public void DrawAll()
        {
            DrawBoard();
            if (Logic.board == null) return;
            for (int i = 0; i < Logic.board.Length; i++)
            {

                if (Logic.board[i] == Player.X)
                    DrawPlayer(Player.X, i,true);
                if (Logic.board[i] == Player.O)
                    DrawPlayer(Player.O, i,true);
            }
        }
        public void DrawPlayer(Player p, int Box, bool fast = false)
        {
            if (Box == -1 || Box > 8) return;
            int h = boardBox[Box].y2 - boardBox[Box].y1;
            int w = boardBox[Box].x2 - boardBox[Box].x1;
            Color c = Color.Black;
            if (p == Player.X) { c = Color.Red; }
            else if (p == Player.O) { c = Color.Blue; }
            Pen pen = new Pen(c);
            pen.Width = 10;
            if (p == Player.X)
            {
                if (fast == true)
                {
                    g.DrawLine(pen, boardBox[Box].x1 + 15, boardBox[Box].y1 + 15, boardBox[Box].x2 - 15, boardBox[Box].y2 - 15);
                    g.DrawLine(pen, boardBox[Box].x2 - 15, boardBox[Box].y1 + 15, boardBox[Box].x1 + 15, boardBox[Box].y2 - 15);
                }
                else
                {
                    Form1.getAnime(Player.X, new Point(boardBox[Box].x1 + 15, boardBox[Box].y1 + 15), new Point(boardBox[Box].x2 + 15, boardBox[Box].y2 - 15), new Point(), new Point(boardBox[Box].x2 - 15, boardBox[Box].y1 + 15), new Point(boardBox[Box].x1 + 15, boardBox[Box].y2 - 15));
                    //Form1.p_ani = Player.X;
                   // Form1.ani.Enabled = true;
                }
            }
            if (p == Player.O)
            {
                if (fast == true) g.DrawEllipse(pen, boardBox[Box].x1 + 15, boardBox[Box].y1 + 15, w - 30, h - 30);
                else
                    {

                    Form1.getAnime(Player.O, new Point(boardBox[Box].x1 + 15, boardBox[Box].y1 + 15), new Point(w - 30, h - 30), new Point((w - 30) / 2, (h - 30) / 2), new Point((w - 30) / 2, (h - 30) / 2));
                    }
            }
        }
        public void DrawText(int Box, string text, int size)
        {
            if (Box == -1 || Box > 8) return;
            int h = boardBox[Box].y2 - boardBox[Box].y1;
            int w = boardBox[Box].x2 - boardBox[Box].x1;
            RectangleF rec = new RectangleF(boardBox[Box].x1, boardBox[Box].y1, w, h);
            g.DrawString(text, new Font("Arial", size), new SolidBrush(Color.Cyan), rec);
        }
        public void DrawWin(int s, int e)
        {
            int x1 = 0, x2 = 0, y1 = 0, y2 = 0;
            //Diagonals
            if (s == 0 && e == 8)
            {
                x1 = boardBox[s].x1;
                y1 = boardBox[s].y1;
                x2 = boardBox[e].x2;
                y2 = boardBox[e].y2;
            }
            else if (s == 2 && e == 6)
            {
                x1 = boardBox[s].x2;
                y1 = boardBox[s].y1;
                x2 = boardBox[e].x1;
                y2 = boardBox[e].y2;
            }
            else if (e - s == 2)
            {
                int newy = boardBox[s].y2 - boardBox[s].y1;
                newy = newy / 2;
                newy = boardBox[s].y1 + newy;
                newy += 1;
                x1 = boardBox[s].x1;
                y1 = newy;
                x2 = boardBox[e].x2;
                y2 = newy;
            }
            else if (e - s == 6)
            {
                int newx = boardBox[s].x2 - boardBox[s].x1;
                newx = newx / 2;
                newx = boardBox[s].x1 + newx;
                newx++;
                x1 = newx;
                y1 = boardBox[s].y1;
                x2 = newx;
                y2 = boardBox[e].y2;
            }
            //Draw
            Pen tpen = new Pen(Color.Green);
            tpen.Width = 9;
            g.DrawLine(tpen, x1, y1, x2, y2);
        }
    }
}
