using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace maze2
{
    public partial class Form1 : Form
    {
        int[,] matrix;
        int saygac = 0;
        int x = 0;
        int y = 0;
        int[][][] mm;
        int[][][] mm_yol_tap;
        int[][][] mm_visited;
        string[] istiqamet = { "sag","asagi","sol","yuxari"};
        int[,] istq = { {1,0},{0,1},{-1,0},{0,-1}};
        string s = "";
        static int veziyyet = 0;
        public Form1()
        {
            InitializeComponent();
        } 
        private void Form1_Load(object sender, EventArgs e)
        { 
        }

        private void draw()
        {
            using (Bitmap bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height))
            {
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    graphics.FillRectangle(Brushes.Gray, 0, 0, pictureBox1.Width, pictureBox1.Height);
                    Pen blackPen = new Pen(Color.FromArgb(255, 50, 50, 0), 2);
                    Pen redPen = new Pen(Color.FromArgb(255, 255, 0, 0), 2);
                    graphics.FillRectangle(Brushes.Aqua, 0, 0, pictureBox1.Width, pictureBox1.Height);
                    for (int i = 0; i < 50; i++)
                    {
                        for (int j = 0; j < 50; j++)
                        {
                            if (mm[i][j][2] == 0)
                            {
                                graphics.DrawLine(blackPen, mm[i][j][0] +5, mm[i][j][1] -5, mm[i][j][0] +5, mm[i][j][1] +5);
                            }
                            if (mm[i][j][3] == 0)
                            {
                                graphics.DrawLine(blackPen, mm[i][j][0] +5, mm[i][j][1] +5, mm[i][j][0] -5, mm[i][j][1] +5);
                            }
                            if (mm[i][j][4] == 0)
                            {
                                graphics.DrawLine(blackPen, mm[i][j][0] -5, mm[i][j][1] +5, mm[i][j][0] -5, mm[i][j][1] -5);
                            }
                            if (mm[i][j][5] == 0)
                            {
                                graphics.DrawLine(blackPen, mm[i][j][0] -5, mm[i][j][1] -5, mm[i][j][0] +5, mm[i][j][1] -5);
                            }
                            if (mm_yol_tap[i][j][2] == 2)
                            {

                                graphics.DrawLine(redPen, mm[i][j][0] +5, mm[i][j][1] -5, mm[i][j][0] +5, mm[i][j][1] +10);
                            }
                            if (mm_yol_tap[i][j][3] == 2)
                            {
                                graphics.DrawLine(redPen, mm[i][j][0] +5, mm[i][j][1] +10, mm[i][j][0] -5, mm[i][j][1] +5);
                            }
                            if (mm_yol_tap[i][j][4] == 2)
                            {
                                graphics.DrawLine(redPen, mm[i][j][0] -5, mm[i][j][1] +5, mm[i][j][0] -5, mm[i][j][1] -5);
                            }
                            if (mm_yol_tap[i][j][5] == 2)
                            {
                                graphics.DrawLine(redPen, mm[i][j][0] -5, mm[i][j][1] -5, mm[i][j][0] +5, mm[i][j][1] -5);
                            }


                        }
                    }
                }
                pictureBox1.Image = (Image)bitmap.Clone();
            }

        } 

        private void logic()
        {
            Stack<int> stx = new Stack<int>();
            Stack<int> sty = new Stack<int>();
            if (veziyyet==0)
            { 
            int x = 0, y = 0;
            mm_visited[x][y][0] = 1;
            Random rnd = new Random();
            int w = 0;
            
            for (int i = 0; i < 5000; i++)
            {
            huw:  List<int> gg = new List<int>();
                if (w<9)
                {
                    w++;
                } 
                for (int j = 0; j < 4; j++)
                { 
                    if (x + istq[j, 0] >= 0 && x + istq[j, 0]<50 && y + istq[j, 1]>= 0 && y + istq[j, 1]<50)
                    {
                        if (mm_visited[x + istq[j, 0]][y + istq[j, 1]][0] == 0)
                        {
                            gg.Add(j);
                            s += j;
                        }
                    }  
                } 
                ///////////////////////////////////////////////////////////////
                int a = rnd.Next(0, gg.Count);

                if (gg.Count == 0)
                {  
                    veziyyet = 1;
                    if (stx.Count>0)
                    {
                        x=stx.Pop();
                        y=sty.Pop();
                    }
                    else
                    {
                        goto hu1;
                    }
                    
                    goto huw;
                    
                }
                else
                {
                 stx.Push(x);
                 sty.Push(y);
                } 

                if (x + istq[gg[a], 0] >= 0 && x + istq[gg[a], 0] <=50 && y + istq[gg[a], 1] >= 0 && y + istq[gg[a], 1] <=50)
                { 
                    mm_visited[x + istq[gg[a], 0]][y + istq[gg[a], 1]][0] = 1;
                    mm[x][y][2 + gg[a]] = 1;
                    mm[x + istq[gg[a], 0]][y + istq[gg[a], 1]][2 * ((2 + gg[a] + 2) / 6) + (2 + gg[a] + 2) % 6] = 1;
                    x = x + istq[gg[a], 0];
                    y = y + istq[gg[a], 1]; 
                } 
            }  
        }
        hu1: veziyyet = 1; 
        } 
        private void draw_yol()
        {
            using (Bitmap bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height))
            {
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    graphics.FillRectangle(Brushes.Gray, 0, 0, pictureBox1.Width, pictureBox1.Height);
                    Pen blackPen = new Pen(Color.FromArgb(255, 50, 50, 0), 2);
                    Pen redPen = new Pen(Color.FromArgb(255, 255, 0, 0), 2);
                    graphics.FillRectangle(Brushes.Aqua, 0, 0, pictureBox1.Width, pictureBox1.Height);
                    graphics.DrawRectangle(blackPen, mm[0][10][0], mm[0][10][1], 3, 3);
                    for (int i = 0; i < 50; i++)
                    {
                        for (int j = 0; j < 50; j++)
                        {
                            if (mm[i][j][2] == 0)
                            {
                                graphics.DrawLine(blackPen, mm[i][j][0] +5, mm[i][j][1] -5, mm[i][j][0] +5, mm[i][j][1] +5);
                            }
                            if (mm[i][j][3] == 0)
                            {
                                graphics.DrawLine(blackPen, mm[i][j][0] +5, mm[i][j][1] +5, mm[i][j][0] -5, mm[i][j][1] +5);
                            }
                            if (mm[i][j][4] == 0)
                            {
                                graphics.DrawLine(blackPen, mm[i][j][0] -5, mm[i][j][1] +5, mm[i][j][0] -5, mm[i][j][1] -5);
                            }
                            if (mm[i][j][5] == 0)
                            {
                                graphics.DrawLine(blackPen, mm[i][j][0] -5, mm[i][j][1] -5, mm[i][j][0] +5, mm[i][j][1] -5);
                            }
                            if (mm_yol_tap[i][j][2] == 2)
                            {

                                graphics.DrawLine(redPen, mm[i][j][0] +5, mm[i][j][1] -5, mm[i][j][0] +5, mm[i][j][1] +5);
                            }
                            if (mm_yol_tap[i][j][3] == 2)
                            {
                                graphics.DrawLine(redPen, mm[i][j][0] +5, mm[i][j][1] +5, mm[i][j][0] -5, mm[i][j][1] +5);
                            }
                            if (mm_yol_tap[i][j][4] == 2)
                            {
                                graphics.DrawLine(redPen, mm[i][j][0] -5, mm[i][j][1] +5, mm[i][j][0] -5, mm[i][j][1] -5);
                            }
                            if (mm_yol_tap[i][j][5] == 2)
                            {
                                graphics.DrawLine(redPen, mm[i][j][0] -5, mm[i][j][1] -5, mm[i][j][0] +5, mm[i][j][1] -5);
                            }


                        }
                    }
                }
                pictureBox1.Image = (Image)bitmap.Clone();
            }

        } 
        

       static Stack<int> stcx = new Stack<int>();
       static Stack<int> stcy = new Stack<int>();
       List<int> nnx;
       List<int> nny;
        private void logic_yol_tapmaq_3()
        {

            int hedef_x = mm[0][10][0];
            int hedef_y = mm[0][10][1];

             stcx = new Stack<int>();
             stcy = new Stack<int>();
            for (int i = 0; i < mm.Length; i++)
            {
                for (int j = 0; j < mm[0].Length; j++)
                {
                    for (int k = 0; k < mm[0][0].Length; k++)
                    {
                        mm_yol_tap[i][j][k] = mm[i][j][k];
                    }
                }
            }
            int x = 0;
            int y = 0;
            for (int i = 0; i <100*100; i++)
            {
            huw: List<int> gg = new List<int>();

                for (int j = 0; j < 4; j++)
                {
                    if (x + istq[j, 0] >= 0 && x + istq[j, 0] < 50 && y + istq[j, 1] >= 0 && y + istq[j, 1] < 50)
                    {
                        if (mm_yol_tap[x][y][2 + j] == 1)
                        {
                            gg.Add(j);

                        }
                    }
                }
                if (gg.Count == 0)
                { 
                    veziyyet = 1;
                    if (stcx.Count > 0)
                    {
                        x = stcx.Pop();
                        y = stcy.Pop();
                    }
                    else
                    {
                        goto hu1;
                    }

                    goto huw;

                }
                else
                {
                    stcx.Push(x);
                    stcy.Push(y);
                }
                int a = 0; 
                if (x + istq[gg[a], 0] >= 0 && x + istq[gg[a], 0] <= 50 && y + istq[gg[a], 1] >= 0 && y + istq[gg[a], 1] <= 50)
                { 
                    mm_visited[x + istq[gg[a], 0]][y + istq[gg[a], 1]][0] = 1; 
                    mm_yol_tap[x][y][2 + gg[a]] = 2;
                    mm_yol_tap[x + istq[gg[a], 0]][y + istq[gg[a], 1]][2 * ((2 + gg[a] + 2) / 6) + (2 + gg[a] + 2) % 6] = 2;

                    x = x + istq[gg[a], 0];
                    y = y + istq[gg[a], 1];
                }
                if (x==0 && y==10)
                { 
                    goto hu1;
                } 
                nnx  = stcx. ToList();
                nny =  stcy.ToList(); 
            }
        hu1: veziyyet = 1;
       
        } 
        private void button3_Click(object sender, EventArgs e)
        {
            logic_yol_tapmaq_3(); 
            using (Bitmap bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height))
            {
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    graphics.FillRectangle(Brushes.Gray, 0, 0, pictureBox1.Width, pictureBox1.Height);
                    Pen blackPen = new Pen(Color.FromArgb(255, 50, 50, 0), 2);
                    Pen redPen = new Pen(Color.FromArgb(255, 255, 0, 0), 2);
                    graphics.FillRectangle(Brushes.Aqua, 0, 0, pictureBox1.Width, pictureBox1.Height);
                    graphics.DrawRectangle(blackPen,mm[0][10][0],mm[0][10][1],3,3);
                    for (int i = 0; i < 50; i++)
                    {
                        for (int j = 0; j < 50; j++)
                        {
                            if (mm[i][j][2] == 0)
                            {
                                graphics.DrawLine(blackPen, mm[i][j][0] +5, mm[i][j][1] -5, mm[i][j][0] +5, mm[i][j][1] +5);
                            }
                            if (mm[i][j][3] == 0)
                            {
                                graphics.DrawLine(blackPen, mm[i][j][0] +5, mm[i][j][1] +5, mm[i][j][0] -5, mm[i][j][1] +5);
                            }
                            if (mm[i][j][4] == 0)
                            {
                                graphics.DrawLine(blackPen, mm[i][j][0] -5, mm[i][j][1] +5, mm[i][j][0] -5, mm[i][j][1] -5);
                            }
                            if (mm[i][j][5] == 0)
                            {
                                graphics.DrawLine(blackPen, mm[i][j][0] -5, mm[i][j][1] -5, mm[i][j][0] +5, mm[i][j][1] -5);
                            } 

                        }
                    } 
                    Point p1=new Point();
                    Point p2=new Point();
                    for (int i = 0; i < nnx.Count-1; i++)
                    {
                        int wx = nnx[i];
                        int wy = nny[i]; 
                         p1 = new Point(mm[nnx[i]][nny[i]][0], mm[nnx[i]][nny[i]][1]);
                         p2 =new Point(mm[nnx[i+1]][nny[i+1]][0],mm[nnx[i+1]][nny[i+1]][1]);
                        graphics.DrawLine(redPen,p1,p2);
                    }
                    Point son=new Point(mm[0][10][0],mm[0][10][1]);
                    p2 = new Point(mm[nnx[0]][nny[0]][0], mm[nnx[0]][nny[0]][1]);
                    graphics.DrawLine(redPen, p2, son); 
                }
                pictureBox1.Image = (Image)bitmap.Clone();
            }  
        }

      
        private void button1_Click(object sender, EventArgs e)
        {

            timer1.Start();
            matrix = new int[50, 50];
            for (int i = 0; i < 50; i++)
            {
                for (int j = 0; j < 50; j++)
                {
                    matrix[i, j] = 0;
                }
            }
            mm = new int[50][][];
            mm_visited = new int[50][][];
            mm_yol_tap = new int[50][][]; ;

            for (int i = 0; i < mm.Length; i++)
            {
                mm[i] = new int[50][];
                mm_visited[i] = new int[50][];
                mm_yol_tap[i] = new int[50][]; ;
            }
            for (int i = 0; i < 50; i++)
            {
                for (int j = 0; j < 50; j++)
                {
                    mm[i][j] = new int[6];
                    mm_visited[i][j] = new int[1];
                    mm_yol_tap[i][j] = new int[6]; ;
                }
            }
            for (int i = 0; i < 50; i++)
            {
                for (int j = 0; j < 50; j++)
                {
                    for (int k = 0; k < 2; k++)
                    {
                        mm[i][j][0] = (i + 1) * 10;
                        mm[i][j][1] = (j + 1) * 10;
                    }
                }
            }
            logic();
            saygac++;
            draw();
            veziyyet = 1; 
        } 
    }
}
