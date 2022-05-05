using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace WindowsFormsApplication3
{
    public partial class Form1 : Form
    {
        bool FS = false;
        bool FEL=true;
        bool FI = false;
        bool FI2 = false;
        const int maxcolbot=100;
        const int colstartbot=40;
        const int colgen=200;
        const int pgen=40;
        const int sizex = 100;
        const int sizey = 60;
        int colbot;
        int numer = 0;
        int n;
        int time;
        const int genomsize = pgen + colgen;
        int[, ,] info = new int[2, colgen, maxcolbot];
        int[,,] map = new int[2,sizex, sizey];
        int life = 0;
        int timerek = 0;
        const int maxHP = 200;
        const int maxMP = 200;
        Random rand = new Random();
        SolidBrush br = new SolidBrush(Color.White);
        Pen p = new Pen(Color.White, 2);
        

        public Form1()
        {
            InitializeComponent();
            Lifetik.Interval = 1;
            steptik.Interval = 1;
            

            for (int x=0 ;x<sizex;x++)
            {
                map[0, x, 0] = 3;
                map[0, x, sizey-1] = 3;

            }
            for (int y = 0; y < sizey; y++)
            {
                map[0, 0, y] = 3;
                map[0,sizex-1 , y] = 3;
            }


            for (int num = 0; num < 4; num++)
            {
                for (int gen = 0; gen < colgen; gen++)
                { 
                    info[0, gen, num] = rand.Next(genomsize); 
                }
                    
            }
            
           
        }

        private void Startbut_Click(object sender, EventArgs e)
        {
            if(FS==false)
            {
                //Lifetik.Enabled = true;
                FS = true;
                ThreadStart w = new ThreadStart(lifel);
                Thread t = new Thread(w);
                t.Start();
            }
        }

        private void Lifetik_Tick(object sender, EventArgs e)
        {}

        public void lifel()
    {
          /*  if (FEL == true)
            {
                FEL = false;
                life++;*/
             lifel1:   colbot = colstartbot;
             time = 0;
                for (int x = 1; x < sizex-1; x++)
                {
                    for (int y = 1; y < sizey-1; y++)
                    {
                        map[0, x, y] = rand.Next(4);
                        if (FI == true)
                        {
                            while (FI2 == true)
                            { }
                            FI2 = true;
                            graph(x, y, map[0, x, y],10);
                            FI2 = false;
                        }
                    }
                }
                

                for (int num = 0; num < colbot; num++)
                {
                    info[1, 0, num] = (num%10)*10+5;//x
                    info[1, 1, num] = (num/10)*10+5 ;//y
                    info[1, 2, num] = 0;//step
                    info[1, 3, num] = 100;//hp
                    info[1, 4, num] = 100;//mp
                    info[1, 5, num] = num;//startnum
                    info[1, 6, num] = life;//startlife
                    info[1, 7, num] = 6;//color
                    info[1, 8, num] = 2;//rotate
                    info[1, 9, num] = 0;
                    map[0, info[1, 0, num], info[1, 1, num]] = info[1, 7, num];
                    if (FI == true)
                    {
                        while (FI2==true)
                        { }
                        FI2 = true;
                        graph(info[1, 0, num], info[1, 1, num], map[0, info[1, 0, num], info[1, 1, num]], info[1, 8, num]);
                        FI2 = false;
                    }
                }

                for (int num = 4; num < colbot; num++)
                {
                    for (int gen = 0; gen < colgen; gen++)
                    {
                        info[0, gen, num] = info[0, gen, num % 4];
                    }

                    for (int i = 0; i < rand.Next(20); i++)
                    {
                        info[0, rand.Next(colgen), num] = rand.Next(genomsize);
                    }
                }
                steptik.Enabled = true;
                stepp();
                if (timerek < time)
                {
                    timerek = time;
                    textBox2.Text = timerek.ToString();
                }
                life++;

                goto lifel1;
                
            

        }

        private void Imageswich_Click(object sender, EventArgs e)
        {
            if(FI==false)
            {
                while (FI2 == true)
                { }
                FI2 = true;
                FI = true;
                for (int x=0;x<sizex;x++)
                {
                    for (int y = 0; y < sizey; y++)
                    {
                        graph(x, y, map[0,x, y],10);
                    }
                }
                FI2 = false;
            }
            else { FI = false; }
        }

        private void steptik_Tick(object sender, EventArgs e)
        {
        }

        public void stepp()
        {

           stepp1: n = 0;
           //textBox2.Text = numer.ToString();
          // textBox3.Text = info[1, 3, numer].ToString();
           

            do
            {
               if(info[1, 5, numer]>0)
                { 
                if (info[0, info[1, 2, numer], numer] < colgen)
                {
                    info[1, 2, numer] = info[0, info[1, 2, numer], numer];
                    n++;
                  

                }
                else
                {
                    stepbot(info[0, info[1, 2, numer], numer] - colgen);
                }
               }
               else
               { n += 10; }
                if (info[1, 2, numer] >= colgen)
                { info[1, 2, numer] -= colgen; }
            }
            while (n < 10);
            
            info[1, 3, numer]--;
            int hp = info[1, 3, numer];
            if (info[1, 3, numer]<= 0)
            {
                colbot--;
                for (int num = numer; num < colbot;num++ )
                {
                    for (int g = 0; g < colgen; g++)
                    {
                        info[0, g, num] = info[0, g, num+1];
                        info[1, g, num] = info[1, g, num + 1];
                    }
                }
                    map[0, info[1, 0, numer], info[1, 1, numer]] = 2;
                map[1, info[1, 0, numer], info[1, 1, numer]] = 1;
                if (FI == true)
                {
                    while (FI2 == true)
                    { }
                    FI2 = true;
                    graph(info[1, 0, numer], info[1, 1, numer], map[0, info[1, 0, numer], info[1, 1, numer]],10);
                    FI2 = false;
                  
                }
                /*Смещение данных ботов,изменение на карте*/

                if (colbot == 4)
                {
                    //steptik.Enabled = false;
                    //FEL = true;
                    goto stepp2;
                }
            }
            else
            {
                if (info[1, 3, numer]>maxHP)
                { info[1, 3, numer] = maxHP; }
                numer++;
            }
            if (numer >= colbot)
            {
                numer = 0;
                time++;
                textBox1.Text = time.ToString();
            }
            int numr = numer;
            goto stepp1;
        stepp2: ;
        }

        public void graph (int x, int y, int col,int targ)
        {
            
           // Brush b = new Brush(Color.Black);
           // if(pictureBox1.InvokeRequired==true)
            {
                Graphics gr = Graphics.FromHwnd(pictureBox1.Handle);
                switch (col)
                {
                    case 0:
                        br.Color = Color.White;
                        break;
                    case 1:
                        br.Color = Color.Green;
                        break;
                    case 2:
                        br.Color = Color.Red;
                        break;
                    case 3:
                        br.Color = Color.Blue;
                        break;
                    case 4:
                        br.Color = Color.Brown;
                        break;
                    case 5:
                        //br.Color = Color.Green;
                        break;
                    case 6:
                        br.Color = Color.Black;
                        break;
                }
                gr.FillRectangle(br, x * 10, y * 10, 8, 8);
                if (col == 6)
                {
                    int targx = 0;
                    int targy = 0;
                    switch (targ)
                    {
                        case 0:
                            targx = x * 10 + 3;
                            targy = y * 10 + 6;
                            break;
                        case 1:
                            targx = x * 10 + 6;
                            targy = y * 10 + 6;
                            break;
                        case 2:
                            targx = x * 10 + 6;
                            targy = y * 10 + 3;
                            break;
                        case 3:
                            targx = x * 10 + 6;
                            targy = y * 10 + 0;
                            break;
                        case 4:
                            targx = x * 10 + 3;
                            targy = y * 10 + 0;
                            break;
                        case 5:
                            targx = x * 10 + 0;
                            targy = y * 10 + 0;
                            break;
                        case 6:
                            targx = x * 10 + 0;
                            targy = y * 10 + 3;
                            break;
                        case 7:
                            targx = x * 10 + 0;
                            targy = y * 10 + 6;
                            break;

                    }
                    gr.DrawRectangle(p, targx, targy, 2, 2);
                }
            }
            
            
            

           
        }

        public void stepbot(int gen)
        {
            if (0 <= gen & gen < 10)
            {
                dostep(gen%8);
               // Thread.Sleep(10);
            }
            else
            {
                if (10 <= gen & gen < 20)
                {
                    dosee((gen - 10)%8);
                }
               else
                {
                    if (20 <= gen & gen < 30)
                    {
                        doup((gen - 20)%8);
                    }
                    else
                    {
                        if (30 <= gen & gen < 40)
                        {
                            dorotate(gen - 30);
                        }
                    }
                }
            }
            
        }

        public void coordinate(ref int x, ref int y,int gen)
        {
            switch (gen)
            {
                case 0:
                    y += 1;
                    break;
                case 1:
                    x += 1;
                    y += 1;
                    break;
                case 2:
                    x += 1;
                    break;
                case 3:
                    x += 1;
                    y -= 1;
                    break;
                case 4:
                    y -= 1;
                    break;
                case 5:
                    x -= 1;
                    y -= 1;
                    break;
                case 6:
                    x -= 1;
                    break;
                case 7:
                    x -= 1;
                    y += 1;
                    break;
            }
        }


        public void dostep(int gen)
        {
            info[1, 4, numer]-=2;
            info[1, 2, numer]++;
            n += 10;
            int x = info[1, 0, numer];
            int y = info[1, 1, numer];
            map[0, x, y] = 0;
            if (FI == true)
            {
                while (FI2 == true)
                { }
                FI2 = true;
                graph(x, y,0, 0);
                FI2 = false;
                Thread.Sleep(trackBar1.Value);
            }
            int e1=info[1, 8, numer];
            int e2 = (info[1, 8, numer] + gen) % 8;
           coordinate(ref x, ref y, (info[1, 8, numer] + gen) % 8);
            switch(map[0,x,y])
            {
                case 0:
                    info[1, 0, numer]=x;
                    info[1, 1, numer]=y;
                    break;
                case 1:
                    info[1, 0, numer]=x;
                    info[1, 1, numer]=y;
                    info[1, 3, numer] += 10;
                    break;
                case 2:
                    info[1, 0, numer]=x;
                    info[1, 1, numer]=y;
                    info[1, 3, numer] = 0;
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    break;    
            }
            map[0, info[1, 0, numer], info[1, 1, numer]] = info[1, 7, numer];
            if (FI == true)
            {
                while (FI2 == true)
                { }
                FI2 = true;
                graph(info[1, 0, numer], info[1, 1, numer], map[0, info[1, 0, numer], info[1, 1, numer]], info[1, 8, numer]);
                FI2=false;
                Thread.Sleep(trackBar1.Value);
            }
            
                  
        }


        public void dosee(int gen)
        {
            info[1, 4, numer] -= 1;
            n ++;
            int x = info[1, 0, numer];
            int y = info[1, 1, numer];
            
            coordinate(ref x, ref y, (info[1, 8, numer] + gen) % 8);
            switch (map[0, x, y])
            {
                case 0:
                    info[1, 2, numer]++;
                    break;
                case 1:
                    info[1, 2, numer]+=2;
                    break;
                case 2:
                    info[1, 2, numer] += 3;
                    break;
                case 3:
                    info[1, 2, numer] += 4;
                    break;
                case 4:
                    info[1, 2, numer] += 5;
                    break;
                case 5:
                    info[1, 2, numer] += 6;
                    break;
                case 6:
                    info[1, 2, numer] += 7;
                    break;
            }
           

        }


        public void doup(int gen)
        {
            info[1, 4, numer]-=2;
            info[1, 2, numer]++;
            n += 10;
            int x = info[1, 0, numer];
            int y = info[1, 1, numer];
           
           coordinate(ref x, ref y, (info[1, 8, numer] + gen) % 8);
            switch(map[0,x,y])
            {
                case 0:
                   
                    break;
                case 1:
                    info[1, 3, numer] += 10;
                    map[0,x,y] = 0;
                    if (FI == true)
                    {
                        while (FI2 == true)
                        { }
                        FI2 = true;
                        graph(x,y,map[0,x,y],10);
                        FI2 = false;
                        Thread.Sleep(trackBar1.Value);
                    }
                    break;
                case 2:
                    map[0,x,y] = 1;
                    if (FI == true)
                    {
                        while (FI2 == true)
                        { }
                        FI2 = true;
                        graph(x,y,map[0,x,y],10);
                        Thread.Sleep(trackBar1.Value);
                        FI2 = false;
                    }
                    break;
                case 3:
                     info[1, 4, numer] += 10;
                     if (info[1, 4, numer] > maxMP)
                     { info[1, 4, numer] = maxMP; }
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    break;    
            }       
        }


        public void dorotate(int gen)
        {
            n++;
            info[1, 4, numer] --;
            info[1, 8, numer] = (info[1, 8, numer] + gen) % 8;
            info[1, 2, numer]++;
            if (FI == true)
            {
                while (FI2 == true)
                { }
                FI2 = true;
                graph(info[1, 0, numer], info[1, 1, numer], map[0, info[1, 0, numer], info[1, 1, numer]], info[1, 8, numer]);
                FI2 = false;
            }
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            //pictureBox1.Location.X = trackBar2.Value;
            pictureBox1.Location = new Point(trackBar2.Value*10-8, pictureBox1.Location.Y);
        }


    }
}