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

        public PointF[] Points = new PointF[] { new PointF(3,3), new PointF(40,35), new PointF(35, 40),
                                                new PointF(250,250), new PointF(200,250), new PointF(220, 210)};
        // First three are first triangle

        public MyEdge Head;
        public MyEdge[] GET;
        public MyEdge AET;

        // D - zaburzenie
        float[] D = { 0, 0, 0 };

        // L - wersor do światła
        float[] L = { 0, 0, 1 };

        // N - wektor normalny 
        float[] N = { 0, 0, 1 };

        // Np - wektor normalny z zaburzeniem
        float[] Np = { 0, 0, 1 };

        // lightPos - położenie źródła światła
        float[] lightPos = { 0, 0, 1 };

        DirectBitmap objColText;
        DirectBitmap normVectText;
        DirectBitmap disturbText;
        DirectBitmap bubble;

        int iChosen;
        bool isChosen = false;

        bool isBeingMoved = false;
        bool isTriangleBeingMoved = false;
        Point mousePos;

        float angle = 0;
        float rad;
        int r = 10;
        
        
        public Form1()
        {
            InitializeComponent();
            WorkspacePictureBox.Image = new Bitmap(WorkspacePictureBox.Width, WorkspacePictureBox.Height);
            WorkspacePictureBox.Refresh();
            Head = null;
            GET = new MyEdge[WorkspacePictureBox.Height];
            objColText = new DirectBitmap(Properties.Resources.brick_normalmap.Width, Properties.Resources.brick_normalmap.Height);
            objColText.LoadBitmap(Properties.Resources.brick_normalmap);
            ObjColTextPB.BackgroundImage = objColText.Bitmap;
            normVectText = new DirectBitmap(Properties.Resources.normal_map.Width, Properties.Resources.normal_map.Height);
            normVectText.LoadBitmap(Properties.Resources.normal_map);
            NormVectTextPB.BackgroundImage = normVectText.Bitmap;
            disturbText = new DirectBitmap(Properties.Resources.brick_normalmap.Width, Properties.Resources.brick_normalmap.Height);
            disturbText.LoadBitmap(Properties.Resources.brick_normalmap);
            DisturbTextPB.BackgroundImage = disturbText.Bitmap;
            bubble = new DirectBitmap(75, 75);
            bubble.LoadBitmap(new Bitmap(ScaleImage(Properties.Resources.normal_map, 75, 75)));
            FillScanLines();
            
        }
        public static Image ScaleImage(Image image, int maxWidth, int maxHeight)
        {
            double ratioX = (double)maxWidth / image.Width;
            double ratioY = (double)maxHeight / image.Height;
            double ratio = Math.Min(ratioX, ratioY);

            int newWidth = (int)(image.Width * ratio);
            int newHeight = (int)(image.Height * ratio);

            Bitmap newImage = new Bitmap(newWidth, newHeight);

            using (Graphics graphics = Graphics.FromImage(newImage))
                graphics.DrawImage(image, 0, 0, newWidth, newHeight);

            return newImage;
        }

        private void RedrawGraph()
        {
            //   WorkspacePictureBox.Refresh();
            Pen pen = new Pen(Color.Black, 3);
            RectangleF circ = new RectangleF();
            SolidBrush brush = new SolidBrush(Color.Black);
            circ.Size = new Size(r, r);

            FillScanLines();
            
            brush.Dispose();
            pen.Dispose();
        }

        private void WorkspacePictureBox_SizeChanged(object sender, EventArgs e)
        {
            Image nIm = new Bitmap(WorkspacePictureBox.Width, WorkspacePictureBox.Height);

            using (Graphics g = Graphics.FromImage(nIm))
            {
                g.DrawImage(WorkspacePictureBox.Image, new Point(0, 0));
                FillScanLines();
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
                    }
                    WorkspacePictureBox.Refresh();
                    FillScanLines();
                }
                mousePos = e.Location;
            }
            else if(e.Button == MouseButtons.Right)
            {

            }
            
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
                    
                    WorkspacePictureBox.Refresh();
                    FillScanLines();
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
                
                //WorkspacePictureBox.Refresh();

                FillScanLines();
            }
        }
        
        public void UpdateEdges()
        {
            float x1 = Points[0].X, y1 = Points[0].Y;
            float x2 = Points[1].X, y2 = Points[1].Y;
            float coef = (x2-x1)/(y2-y1);
            MyEdge temp = new MyEdge(Math.Max(Points[0].Y, Points[1].Y), Points[Points[0].Y < Points[1].Y ? 0 : 1].X, coef, null);
            int iNext;

            for (int i = 0; i < GET.Length; i++)
                GET[i] = null; 
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
                        temp.Next = new MyEdge(Math.Max(Points[i].Y, Points[iNext].Y), Points[Points[i].Y < Points[iNext].Y ? i : iNext].X, coef, null);
                    }
                }
            }
            
        }

        public void FillScanLines()
        {
            int y = 0;
            int R, G, B;

            float cosNL;
            float lighVectLen;
            float normVectLen;
            float distLen;

            Color objCol;
            Color ligCol = LightColorBoxPB.BackColor;
            Color normVectCol;
            Color distPix;
            Color distNX;
            Color distNY;

            MyEdge temp = null;
            UpdateEdges();
            Bitmap bitmap = new Bitmap(WorkspacePictureBox.Width, WorkspacePictureBox.Height);
            while (GET[y] == null) 
                y++;
            AET = null;
            while (y < WorkspacePictureBox.Height) 
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
                    
                    while (temp != null && temp.Next != null)
                    {
                        for (int i = (int)temp.XMin; i < temp.Next.XMin; i++)
                        {
                            if (ObjColConstRB.Checked)
                                objCol = ColorBoxPB.BackColor;
                            else    // From texture
                                objCol = objColText.GetPixel(i % (objColText.Width - 1) + 1, y % (objColText.Height - 1) + 1); 

                            if(NormalVectTextRB.Checked)
                            {
                                // Bąbelek aka "bubble"
                                if ((i - PointToClient(MousePosition).X) * (i - PointToClient(MousePosition).X) + (y - PointToClient(MousePosition).Y) * (y - PointToClient(MousePosition).Y) <= 30 * 30)
                                {
                                    normVectCol = bubble.GetPixel(bubble.Width / 2 + (i - PointToClient(MousePosition).X), bubble.Height / 2 + (y - PointToClient(MousePosition).Y));
                                }
                                else
                                    normVectCol = normVectText.GetPixel(i % (normVectText.Width), y % (normVectText.Height));

                                N[2] = normVectCol.B;
                                N[0] = (normVectCol.R - 127) / N[2];
                                N[1] = (normVectCol.G - 127) / N[2];
                                N[2] /= N[2];
                            }


                            if (DisturbTextRB.Checked)
                            {
                                distPix = disturbText.GetPixel(i % (disturbText.Width), y % (disturbText.Height));
                                distNX = disturbText.GetPixel((i + 1) % (disturbText.Width), y % (disturbText.Height));
                                distNY = disturbText.GetPixel(i % (disturbText.Width), (y + 1) % (disturbText.Height));

                                D[0] = 1 * ((distNX.R + distNX.G + distNX.B) - (distPix.R + distPix.G + distPix.B)) / 3;
                                D[1] = 1 * ((distNY.R + distNY.G + distNY.B) - (distPix.R + distPix.G + distPix.B)) / 3;
                                D[2] = -N[0] * ((distNX.R + distNX.G + distNX.B) - (distPix.R + distPix.G + distPix.B)) / 3 - N[1] * ((distNY.R + distNY.G + distNY.B) - (distPix.R + distPix.G + distPix.B)) / 3;
                                distLen = (float)Math.Sqrt(D[0] * D[0] + D[1] * D[1] + D[2] * D[2]);
                                if (distLen != 0)
                                {
                                    D[0] /= distLen;
                                    D[1] /= distLen;
                                    D[2] /= distLen;
                                }
                            }
                            

                            Np[0] = N[0] + D[0];
                            Np[1] = N[1] + D[1];
                            Np[2] = N[2] + D[2];
                            normVectLen = (float)Math.Sqrt(Np[0] * Np[0] + Np[1] * Np[1] + Np[2] * Np[2]);

                            Np[0] /= normVectLen;
                            Np[1] /= normVectLen;
                            Np[2] /= normVectLen;

                            if(LighSourVectAnimRB.Checked) 
                            {
                                L[0] = lightPos[0] - i;
                                L[1] = lightPos[1] - y;
                                L[2] = 0.0000005f;
                                lighVectLen = (float)Math.Sqrt(L[0] * L[0] + L[1] * L[1] + L[2] * L[2]);

                                L[0] /= lighVectLen;
                                L[1] /= lighVectLen;
                                L[2] /= lighVectLen;
                            }

                            cosNL = Math.Max(0,(Np[0] * L[0] + Np[1] * L[1] + Np[2] * L[2]));

                            // Normalized (both divided by 255) and denormalized (result multiplied by 255)
                            R = (int)(objCol.R / 255f * ligCol.R * cosNL);
                            G = (int)(objCol.G / 255f * ligCol.G * cosNL);
                            B = (int)(objCol.B / 255f * ligCol.B * cosNL);

                            bitmap.SetPixel(i, y, Color.FromArgb(R,G,B));
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
            WorkspacePictureBox.Image.Dispose();
            WorkspacePictureBox.Image = bitmap;
            
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

        private void WorkspacePictureBox_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void ColorBoxPB_Click(object sender, EventArgs e)
        {
            if (ObjColConstRB.Checked)
            {
                ColorDialog cd = new ColorDialog();

                if (cd.ShowDialog() == DialogResult.OK)
                {
                    ColorBoxPB.BackColor = cd.Color;

                    FillScanLines();
                }
            }
        }

        private void LightColorBoxPB_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();

            if (cd.ShowDialog() == DialogResult.OK)
            {
                LightColorBoxPB.BackColor = cd.Color;

                FillScanLines();

            }
        }

        private void ObjColTextRB_CheckedChanged(object sender, EventArgs e)
        {
            FillScanLines();
        }

        private void DisturbNoRB_CheckedChanged(object sender, EventArgs e)
        {
            if (DisturbNoRB.Checked)
            {
                for (int i = 0; i < D.Length; i++)
                {
                    D[i] = 0;
                    Np[i] = N[i] / N[2];
                }
                FillScanLines();
            }
        }

        private void NormalVectConstRB_CheckedChanged(object sender, EventArgs e)
        {
            if(NormalVectConstRB.Checked)
            {
                N = new float[] { 0, 0, 1 };

                for (int i = 0; i<Np.Length; i++)
                    Np[i] = N[i] + D[i];

                for (int i = 0; i < Np.Length; i++)
                    Np[i] = Np[i] / Np[2];

                FillScanLines();
            }
        }
        
        private void LighSourVectConstRB_CheckedChanged(object sender, EventArgs e)
        {
            if (LighSourVectConstRB.Checked)
            {
                timer1.Stop();
                L = new float[] { 0, 0, 1 };
                FillScanLines();
            }
        }

        private void LighSourVectAnimRB_CheckedChanged(object sender, EventArgs e)
        {
            if(LighSourVectAnimRB.Checked)
            {
                lightPos = new float[] { 0.5f, 0.25f, 1 };
                timer1.Start();
                FillScanLines();
            }
        }

        public Bitmap OpenImage()
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Title = "Open Image";
                dlg.Filter = "bmp files (*.bmp)|*.bmp|All files (*.*)|*.*";

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    return new Bitmap(dlg.FileName);
                }
            }
            return Properties.Resources.brick_normalmap;
        }

        private void ObjColTextPB_Click(object sender, EventArgs e)
        {
            if (ObjColTextRB.Checked)
            {
                // TODO
                objColText.LoadBitmap(OpenImage());
                ObjColTextPB.BackgroundImage = objColText.Bitmap;
                FillScanLines();
            }
        }

        private void NormVectTextPB_Click(object sender, EventArgs e)
        {
            if (NormalVectTextRB.Checked)
            {
                //    normVectText = OpenImage();
                //    NormVectTextPB.BackgroundImage = normVectText;
                normVectText.LoadBitmap(OpenImage());
                NormVectTextPB.BackgroundImage = normVectText.Bitmap;
                FillScanLines();
            }
        }

        private void DisturbTextPB_Click(object sender, EventArgs e)
        {
            if (DisturbTextRB.Checked)
            {
                disturbText.LoadBitmap(OpenImage());
                DisturbTextPB.BackgroundImage = disturbText.Bitmap;
                FillScanLines();
            }
        }
        

        private void timer1_Tick(object sender, EventArgs e)
        {
            rad = Math.Min(WorkspacePictureBox.Height, WorkspacePictureBox.Width) / 3;
            
            lightPos[0] = (float)(rad * Math.Cos(angle * Math.PI / 180f)) + WorkspacePictureBox.Width / 2;
            lightPos[1] = (float)(rad * Math.Sin(angle * Math.PI / 180f)) + WorkspacePictureBox.Height / 2;
            if (angle < 360)
                angle += 10f;
            else
                angle = 0;
            FillScanLines();

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
