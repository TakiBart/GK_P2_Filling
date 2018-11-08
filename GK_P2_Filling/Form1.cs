using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Drawing.Drawing2D;

namespace GK_P2_Filling
{

    public partial class Form1 : Form
    {

        public PointF[] Points = new PointF[] { new PointF(3,3), new PointF(200,15), new PointF(15, 200),
                                                new PointF(400,500), new PointF(80,500), new PointF(200, 100)};
        // First three are first triangle

        public MyEdge Head;
        public MyEdge[] GET;
        public MyEdge AET;
        int iChosen;
        bool isChosen = false;
        //bool isEdgeChosen = false;

        bool isBeingMoved = false;
        bool isTriangleBeingMoved = false;
        Point mousePos;
        int r = 10;
        
        public Form1()
        {
            InitializeComponent();
            WorkspacePictureBox.Image = new Bitmap(WorkspacePictureBox.Width, WorkspacePictureBox.Height);
            Head = null;
            GET = new MyEdge[WorkspacePictureBox.Height];
            //UpdateEdges();
            
            //Points = {new PointF(3,3), new}
            //OpenButton_Click(this, new EventArgs());
            // Default polygon
            //0,141,96,1,1,4,2
            //1,248,135,2,3,0,1
            //2,331,168,3,2,1,3
            //3,175,192,4,1,2,2
            //4,240,132,0,2,3,1

        }

        // TODO - might be useful

        //private void ColorButton_Click(object sender, EventArgs e)
        //{
        //    ColorDialog cd = new ColorDialog();

        //    if (cd.ShowDialog() == DialogResult.OK)
        //    {
        //        ColorPictureBox.BackColor = cd.Color;
        //        if(isChosen)
        //        {
        //            int i = Points.IndexOf(chosen);
        //            Points[i] = chosen;

        //            RedrawGraph(Graphics.FromImage(WorkspacePictureBox.Image));
        //        }
        //    }
        //}

        private void RedrawGraph(Graphics g)
        {
            Pen pen = new Pen(Color.Black, 3);
            RectangleF circ = new RectangleF();
            SolidBrush brush = new SolidBrush(Color.Black);
            circ.Size = new Size(r, r);

            FillScanLines();

            using (g)
            {

                //TODO - change drawing
                //for (int i = 0; i < Points.Length/2; i++)
                //{
                //    if (Points[i].X < WorkspacePictureBox.Width && Points[i].Y < WorkspacePictureBox.Height)
                //    {
                //        {
                //            BresenhamLine((int)(Points[i].X + r / 2), (int)(Points[i].Y + r / 2), (int)(Points[(i + 1) % 3].X + r / 2), (int)(Points[(i + 1) % 3].Y + r / 2), pen.Color);
                //            BresenhamLine((int)(Points[i + Points.Length / 2].X + r / 2), (int)(Points[i + Points.Length / 2].Y + r / 2), (int)(Points[(i + 1) % 3 + Points.Length / 2].X + r / 2), (int)(Points[(i + 1) % 3 + Points.Length / 2].Y + r / 2), pen.Color);
                //        }
                //    }
                //}
                
                //WorkspacePictureBox.Refresh();
            }
            brush.Dispose();
            pen.Dispose();
        }

        private void WorkspacePictureBox_SizeChanged(object sender, EventArgs e)
        {
            Image nIm = new Bitmap(WorkspacePictureBox.Width, WorkspacePictureBox.Height);

            using (Graphics g = Graphics.FromImage(nIm))
            {
                g.DrawImage(WorkspacePictureBox.Image, new Point(0, 0));
                RedrawGraph(g);
            }
            WorkspacePictureBox.Image.Dispose();
            WorkspacePictureBox.Image = nIm;
        }


        private void WorkspacePictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            Graphics g = Graphics.FromImage(WorkspacePictureBox.Image);
            Point location = new Point(e.X - r / 2, e.Y - r / 2);
            if (e.Button == MouseButtons.Left)
            {

                if (Points.Length <= 0)
                    return;

                Rectangle circ = new Rectangle();
                //MyPoint closest = Points[0];
                // #1 ========|v|=========|^|
                int iClosest = 0;
                int closestDist = (int)(Math.Pow(Math.Abs(Points[iClosest].X + r / 2 - e.X), 2) + Math.Pow(Math.Abs(Points[iClosest].Y + r / 2 - e.Y), 2));

                circ.Size = new Size(r, r);
                using (g)
                {
                    // TODO - change chosing vertex
                    //
                    for(int i = 0; i < Points.Length; i++)
                    {
                        if (Math.Pow(Math.Abs(Points[i].X + r / 2 - e.X), 2) + Math.Pow(Math.Abs(Points[i].Y + r / 2 - e.Y), 2) < closestDist)
                        {
                            iClosest = i;
                            closestDist = (int)(Math.Pow(Math.Abs(Points[i].X + r / 2 - e.X), 2) + Math.Pow(Math.Abs(Points[i].Y + r / 2 - e.Y), 2));
                        }

                    }

                    iChosen = iClosest;
                    isBeingMoved = true;

                    if (closestDist < 800)
                    {
                        isTriangleBeingMoved = false;
                        isChosen = true;
                        //iChosen = iClosest;
                        //isBeingMoved = true;
                    }
                    else
                    {
                        isTriangleBeingMoved = true;
                        isChosen = false;

                        // Checking whether we're choosing edge - useful?
                        //
                        //foreach (KeyValuePair<int, MyPoint> point in Points)
                        //{

                        //    if (IsOnLine(point.Value.Loc, Points[point.Value.INext].Loc, e.Location, 17))
                        //    {
                        //        isChosen = false;
                        //        iChosen = point.Key;
                        //        
                        //    }
                        //}

                    }
                    WorkspacePictureBox.Refresh();
                    RedrawGraph(g);
                }
                mousePos = e.Location;
            }
            else if(e.Button == MouseButtons.Right)
            {

            }
            
        }
        
        private void SaveButton_Click(object sender, EventArgs e)
        {

            // TODO - UPDATE SAVE & OPEN according to structure of triangles

            SaveFileDialog save = new SaveFileDialog
            {
                FileName = "default.graph",
                Filter = "Graph files (*.graph)|*.graph"
            };

            if (save.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter(save.FileName))
                    {
                        //foreach (KeyValuePair<int, MyPoint> point in Points)
                        //    sw.WriteLine("{0},{1},{2},{3},{4}", point.Key, point.Value.Loc.X, point.Value.Loc.Y, point.Value.INext, point.Value.IPrev);
                    }
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(GlobalStrings.SaveErrorMessageBox+$"\n{ex.Message}", "ERROR");
                    MessageBox.Show("Saving error:\n" + ex.Message, "ERROR");
                }
            }
            save.Dispose();

        }

        private void OpenButton_Click(object sender, EventArgs e)
        {

            // TODO - UPDATE SAVE & OPEN according to structure of triangles

            OpenFileDialog open = new OpenFileDialog
            {
                Filter = "Graph files (*.graph)|*.graph"
            };
            if (open.ShowDialog() == DialogResult.OK && File.Exists(open.FileName))
            {
                try
                {
                    string[] pts = new string[7];
                    string word = "";
                    using (StreamReader sr = new StreamReader(open.FileName))
                    {
                        //Points.Clear();
                        //isChosen = false;
                        //iChosen = -1;
                        //WorkspacePictureBox.Image.Dispose();
                        //WorkspacePictureBox.Image = new Bitmap(WorkspacePictureBox.Width, WorkspacePictureBox.Height);

                        //while ((word = sr.ReadLine()) != null)
                        //{
                        //    pts = word.Split(',', '\n');
                        //    Points.Add(Int32.Parse(pts[0]), new MyPoint(new Point(Int32.Parse(pts[1]), Int32.Parse(pts[2])), Int32.Parse(pts[3]), Int32.Parse(pts[4])));
                        //    if (maxKey < Int32.Parse(pts[0]))
                        //        maxKey = Int32.Parse(pts[0]);
                        //}
                        //maxKey++;
                        //WorkspacePictureBox.Image.Dispose();
                        //WorkspacePictureBox.Image = new Bitmap(WorkspacePictureBox.Width, WorkspacePictureBox.Height);
                        //RedrawGraph(Graphics.FromImage(WorkspacePictureBox.Image));
                    }
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(GlobalStrings.OpenErrorMessageBox+"\n" + ex.Message, "ERROR");
                    MessageBox.Show("Loading error:\n" + ex.Message, "ERROR");
                }
            }
            open.Dispose();
        }
        
        private void WorkspacePictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {

                if (isBeingMoved || isTriangleBeingMoved)   // && isChosen
                {
                    isBeingMoved = false;
                    isTriangleBeingMoved = false;
                    float nX = Points[iChosen].X;
                    float nY = Points[iChosen].Y;
                    int i = iChosen;
                    if (Points[iChosen].X < 0)
                        nX = -r / 2;
                    else if (Points[iChosen].X > WorkspacePictureBox.Width)
                        nX = WorkspacePictureBox.Width - r / 2;

                    if (Points[iChosen].Y < 0)
                        nY = -r / 2;
                    else if (Points[iChosen].Y > WorkspacePictureBox.Height)
                        nY = WorkspacePictureBox.Height - r / 2;

                    Points[iChosen].X = nX;
                    Points[iChosen].Y = nY;

                    mousePos = e.Location;
                    WorkspacePictureBox.Image.Dispose();
                    WorkspacePictureBox.Image = new Bitmap(WorkspacePictureBox.Width, WorkspacePictureBox.Height);

                    RedrawGraph(Graphics.FromImage(WorkspacePictureBox.Image));
                }
                
            }
        }

        private void WorkspacePictureBox_MouseMove(object sender, MouseEventArgs e)
        {

            if (isBeingMoved)
            {
                if (isChosen)
                {
                    int i = iChosen;
                    float nX = Points[iChosen].X - mousePos.X + e.X;
                    float nY = Points[iChosen].Y - mousePos.Y + e.Y;
                    Points[iChosen].X = nX;
                    Points[iChosen].Y = nY;
                }
                else if (isTriangleBeingMoved)    // Moving all triangle
                {
                    int ind = iChosen < 3 ? 0 : 3;
                    for (int i = 0; i < Points.Length / 2; i++)
                    {
                        Points[i + ind].X = Points[i + ind].X - mousePos.X + e.X;
                        Points[i + ind].Y = Points[i + ind].Y - mousePos.Y + e.Y;
                    }
                }
                mousePos = e.Location;
                //UpdateEdges();

                WorkspacePictureBox.Image.Dispose();
                WorkspacePictureBox.Image = new Bitmap(WorkspacePictureBox.Width, WorkspacePictureBox.Height);

                RedrawGraph(Graphics.FromImage(WorkspacePictureBox.Image));
            }

        }


        // Checking whether edge has been chosen - useful?
        //public bool IsOnLine(Point p1, Point p2, Point p, int width = 1)
        //{
        //    bool isOnLine = false;
        //    using (GraphicsPath path = new GraphicsPath())
        //    {
        //        using (var pen = new Pen(Brushes.Black, width))
        //        {
        //            path.AddLine(new Point(p1.X + r / 2, p1.Y + r / 2), new Point(p2.X + r / 2, p2.Y + r / 2));
        //            isOnLine = path.IsOutlineVisible(p, pen);
        //        }
        //    }
        //    return isOnLine;
        //}

        // Drawing string - useful?
        //
        //public void DrawString(string s, Point p, Color color)
        //{
        //    Graphics g = Graphics.FromImage(WorkspacePictureBox.Image);
        //    Font drawFont = new Font("Consolas", 10);
        //    SolidBrush drawBrush = new SolidBrush(color);
        //    StringFormat drawFormat = new StringFormat();
        //    g.DrawString(s, drawFont, drawBrush, p, drawFormat);
        //    drawFont.Dispose();
        //    drawBrush.Dispose();
        //    g.Dispose();
        //}

        public void BresenhamLine(int x, int y, int x2, int y2, Color color)
        {
            Graphics g = Graphics.FromImage(WorkspacePictureBox.Image);
            int w = x2 - x;
            int h = y2 - y;
            int dx1 = 0, dy1 = 0, dx2 = 0, dy2 = 0;
            if (w < 0) dx1 = -1; else if (w > 0) dx1 = 1;
            if (h < 0) dy1 = -1; else if (h > 0) dy1 = 1;
            if (w < 0) dx2 = -1; else if (w > 0) dx2 = 1;
            int longest = Math.Abs(w);
            int shortest = Math.Abs(h);
            if (!(longest > shortest))
            {
                longest = Math.Abs(h);
                shortest = Math.Abs(w);
                if (h < 0) dy2 = -1; else if (h > 0) dy2 = 1;
                dx2 = 0;
            }
            int numerator = longest >> 1;
            for (int i = 0; i <= longest; i++)
            {
                //putpixel(x, y, color);
                g.FillRectangle(new SolidBrush(color), x, y, 3, 3);
                numerator += shortest;
                if (!(numerator < longest))
                {
                    numerator -= longest;
                    x += dx1;
                    y += dy1;
                }
                else
                {
                    x += dx2;
                    y += dy2;
                }
            }
        }

        public void UpdateEdges()
        {
            float x1 = Points[0].X, y1 = Points[0].Y;
            float x2 = Points[1].X, y2 = Points[1].Y;
            float coef = (x2-x1)/(y2-y1);
            MyEdge temp = new MyEdge(Math.Max(Points[0].Y, Points[1].Y), Math.Min(Points[0].X, Points[1].X), coef, null);
            int iNext;
            //Head = temp;
            GET[(int)Math.Min(Points[0].Y, Points[1].Y)] = temp;
            for (int i = 1; i < Points.Length; i++)
            {
                if (i == 2)
                    iNext = 0;
                else if (i == 5)
                    iNext = 3;
                else
                    iNext = i + 1;

                x1 = Points[i].X;
                y1 = Points[i].Y;
                x2 = Points[iNext].X;
                y2 = Points[iNext].Y;
                coef = (x2 - x1) / (y2 - y1);
                //temp.Next = new MyEdge(Math.Max(Points[i].Y, Points[iNext].Y), Math.Min(Points[i].X, Points[iNext].X), coef, null);

                int ind = (int)Math.Min(Points[i].Y, Points[iNext].Y);
                if (Points[i].Y != Points[iNext].Y)
                {
                    if (GET[ind] == null)
                        GET[ind] = new MyEdge(Math.Max(Points[i].Y, Points[iNext].Y), Points[Points[i].Y < Points[iNext].Y ? i : iNext].X, coef, null);
                    else
                    {
                        temp = GET[ind];
                        while (temp.Next != null)
                        {
                            temp = temp.Next;
                        }
                        temp.Next = new MyEdge(Math.Max(Points[i].Y, Points[iNext].Y), Math.Min(Points[i].X, Points[iNext].X), coef, null);
                    }
                }
            }
            
        }

        public void FillScanLines()
        {
            int y = 0;
            MyEdge temp = null;
            UpdateEdges();
            while (GET[y] != null)
                y++;
            AET = null;
            while ( y < WorkspacePictureBox.Height) // || AET != null ?
            {
                // Add lists from y bucket to AET
                if (GET[y] != null && AET == null)
                    AET = GET[y];
                else if (AET != null)
                {
                    if (GET[y] != null)
                    {
                        temp = AET;
                        while (temp.Next != null)
                            temp = temp.Next;
                        temp.Next = GET[y];
                    }


                    // Sort AET by x
                    AET = MergeSort(AET);

                    // TO_TEST Remove from AET elems for which y = yMax
                    temp = AET;
                    while (temp != null && temp.Next != null)
                    {
                        if (temp.Next.YMax <= y)
                            temp.Next = temp.Next.Next;
                        temp = temp.Next;
                    }

                    if (AET.YMax <= y)
                        AET = AET.Next;

                    if (AET!= null && AET.YMax <= y)
                        AET = AET.Next;

                    // Fill pixs between crossings
                    temp = AET;
                    while (temp != null)
                    {
                        for (int i = (int)temp.XMin; i < temp.Next.XMin; i++)
                        {
                            using (Graphics g = WorkspacePictureBox.CreateGraphics())
                            {
                                g.FillRectangle(new SolidBrush(Color.Black), i, y, 1, 1);
                            }
                        }
                        temp = temp.Next.Next;
                    }

                   

                    // For each edge in AET update x (x+=1/m)
                    temp = AET;
                    while (temp != null)
                    {
                        temp.XMin += temp.Coefficient;
                        temp = temp.Next;
                    }
                }
                y++;
            }
        }

        public MyEdge MergeSort(MyEdge head)
        {
            if (head == null || head.Next == null) { return head; }
            MyEdge middle = GetMiddle(head);      //get the middle of the list
            MyEdge sHalf = middle.Next;
            middle.Next = null;   //split the list into two halfs

            return Merge(MergeSort(head), MergeSort(sHalf));  //recurse on that
        }

        //Merge subroutine to merge two sorted lists
        public MyEdge Merge(MyEdge a, MyEdge b)
        {
            MyEdge dummyHead = new MyEdge();
            MyEdge curr = dummyHead;
            while (a != null && b != null)
            {
                // TODO - Shouldn't x be current instead of min?
                if (a.XMin <= b.XMin) { curr.Next = a; a = a.Next; }
                else { curr.Next = b; b = b.Next; }
                curr = curr.Next;
            }
            curr.Next = (a == null) ? b : a;
            return dummyHead.Next;
        }

        //Finding the middle element of the list for splitting
        public MyEdge GetMiddle(MyEdge head)
        {
            if (head == null) { return head; }
            MyEdge slow, fast; slow = fast = head;
            while (fast.Next != null && fast.Next.Next != null)
            {
                slow = slow.Next; fast = fast.Next.Next;
            }
            return slow;
        }

    }

    public class MyPoint
    {
        public PointF Loc;
        public int INext { get; set; }      // index of next point
        public int IPrev { get; set; }      // index of previous point

        public MyPoint()
        {
            Loc = new PointF();
            INext = -1;
            IPrev = -1;
        }

        public MyPoint(PointF _point, int _iNext, int _iPrev)
        {
            Loc = _point;
            INext = _iNext;
            IPrev = _iPrev;
        }
    }

    public static class Prompt
    {
        // TODO - adjust to show "bubble"
        //
        public static double ShowDialog(string text, string caption, double length)
        {
            Form prompt = new Form();
            prompt.Width = 300;
            prompt.Height = 150;
            prompt.Text = caption;
            Label textLabel = new Label() { Left = 50, Top = 20, Text = text };
            NumericUpDown inputBox = new NumericUpDown() { Left = 50, Top = 50, Width = 200, Minimum = 1, Maximum = 9999, Value = (decimal)length };
            Button confirmation = new Button() { Text = "Ok", Left = 150, Width = 100, Top = 70 };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.Controls.Add(inputBox);
            prompt.ShowDialog();
            return Decimal.ToDouble(inputBox.Value);
        }
    }
}
